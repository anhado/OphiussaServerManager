using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clifton.Core.Pipes;
using CoreRCON;
using Newtonsoft.Json;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models.SupportedServers;

namespace OphiussaServerManager.Tools {
    internal class NotificationController {
        private readonly Dictionary<string, CancellationTokenSource> _cancelationTokens = new Dictionary<string, CancellationTokenSource>();

        private readonly CancellationTokenSource _cancellationTokenPipeServer = new CancellationTokenSource();
        private readonly List<Profile>           _profiles                    = new List<Profile>();
        private readonly ConcurrentQueue<string> _serversToReload             = new ConcurrentQueue<string>();
        private readonly Settings                _settings;
        private readonly List<Task>              _tasks = new List<Task>();
        private          ClientPipe              _clientPipe;

        private bool _closing;

        private ServerPipe _serverPipe;

        internal NotificationController() {
            _settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")));
            string dir = _settings.DataFolder + "Profiles\\";
            if (!Directory.Exists(dir)) return;

            string[] files = Directory.GetFiles(dir);
            foreach (string file in files) {
                var p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(file));
                _profiles.Add(p);
                _cancelationTokens.Add(p.Key, new CancellationTokenSource());
            }
        }

        internal bool IsClientConnected => _clientPipe == null ? false : _clientPipe.IsConnected;
        internal bool IsServerConnected => _serverPipe == null ? false : _serverPipe.IsConnected;

        internal bool SendCloseCommand() {
            try {
                if (!_clientPipe.IsConnected) ConnectClient();

                _clientPipe.WriteString("/close");

                return true;
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                return false;
            }
        }

        internal bool SendReloadCommand(string key) {
            try {
                if (!_clientPipe.IsConnected) ConnectClient();

                _clientPipe.WriteString($"/Reload=>{key}");

                return true;
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                return false;
            }
        }


        internal void ConnectClient() {
            _clientPipe = new ClientPipe(".", _settings.Guid, p => p.StartStringReaderAsync());
            _clientPipe.DataReceived += (sndr, args) => {
                                            OphiussaLogger.Logger.Info("Client recieved:" + args.String);
                                            MessageBox.Show(args.String);
                                        };
            _clientPipe.Connect();
        }

        internal void CloseClient() {
            _clientPipe.Close();
        }

        internal void StartServer() {
            try {
                OphiussaLogger.Logger.Info("Notifications Service is Starting");
                _serverPipe = new ServerPipe(_settings.Guid, p => p.StartStringReaderAsync());
                _serverPipe.DataReceived += (sndr, args) => {
                                                OphiussaLogger.Logger.Info("Server recieved:" + args.String);
                                                if (args.String == "/close") {
                                                    _serverPipe.WriteString("Notifications Service is Closing");
                                                    _closing = true;
                                                }
                                                else if (args.String.StartsWith("/Reload=>")) {
                                                    string[] str = args.String.Split('>');
                                                    _serversToReload.Enqueue(str[1]);
                                                }
                                                else {
                                                    OphiussaLogger.Logger.Info("Command not recognized:" + args.String);
                                                }
                                            };
                _serverPipe.Connected  += (sndr, args) => { OphiussaLogger.Logger.Info("Client Connected"); };
                _serverPipe.PipeClosed += (sndr, args) => { OphiussaLogger.Logger.Info("Server Closed"); };
                var t = Task.Run(() => {
                                     while (true) {
                                         if (_closing) _serverPipe.Close();
                                         //run until receive command to close;
                                         if (_closing) break;
                                         if (_cancellationTokenPipeServer.IsCancellationRequested) break;
                                         Thread.Sleep(1000);
                                     }
                                 }, _cancellationTokenPipeServer.Token);
                _tasks.Add(t);

                _profiles.ForEach(p => {
                                      switch (p.Type.ServerType) {
                                          case EnumServerType.ArkSurviveEvolved:
                                          case EnumServerType.ArkSurviveAscended:
                                              var t2 = Task.Run(() => SendArkNotifications(p), _cancellationTokenPipeServer.Token);
                                              _tasks.Add(t2);
                                              break;
                                          case EnumServerType.Valheim:
                                              //dont do nothing
                                              break;
                                      }
                                  });

                Task.WaitAll(_tasks.ToArray());
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
            }
        }

        private async void SendArkNotifications(Profile profile) {
            var lastSend = DateTime.Now;
            while (true)
                try {
                    var p = profile;
                    if (p.ArkConfiguration.Administration.EnableInterval)
                        if (p.IsRunning) {
                            foreach (string key in _serversToReload)
                                if (_serversToReload.TryDequeue(out string key2)) {
                                    if (key2 == p.Key)
                                        p.LoadProfile();
                                    else
                                        _serversToReload.Enqueue(key2);

                                    OphiussaLogger.Logger.Info("Reloaded Profile " + p.Name);
                                }

                            var ts = DateTime.Now - lastSend;
                            if (ts.TotalMinutes >= p.ArkConfiguration.Administration.ModInterval) {
                                var rcon = new RCON(IPAddress.Parse(p.ArkConfiguration.Administration.LocalIp), ushort.Parse(p.ArkConfiguration.Administration.RconPort), p.ArkConfiguration.Administration.ServerAdminPassword);
                                await rcon.ConnectAsync();

                                string respnose = await rcon.SendCommandAsync($"Broadcast {p.ArkConfiguration.Administration.Mod}");
                                lastSend = DateTime.Now;
                            }
                        }

                    if (_closing) break;
                    if (_cancelationTokens[p.Key].IsCancellationRequested) break;
                    Thread.Sleep(1000);
                }
                catch (Exception ex) {
                    OphiussaLogger.Logger.Error(ex.Message);
                }
        }
    }
}