using Clifton.Core.Pipes;
using CoreRCON;
using Newtonsoft.Json;
using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Properties;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipelines;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;

namespace OphiussaServerManager.Tools
{
    internal class NotificationController
    {
        Common.Models.Settings Settings;
        List<Profile> profiles = new List<Profile>();
        List<Task> tasks = new List<Task>();
        ConcurrentQueue<string> serversToReload = new ConcurrentQueue<string>();

        private bool closing = false;

        CancellationTokenSource cancellationTokenPipeServer = new CancellationTokenSource();
        Dictionary<string, CancellationTokenSource> cancelationTokens = new Dictionary<string, CancellationTokenSource>();

        ServerPipe serverPipe;
        ClientPipe clientPipe;

        internal bool IsClientConnected { get { return clientPipe == null ? false : (clientPipe.IsConnected); } }
        internal bool IsServerConnected { get { return serverPipe == null ? false : (serverPipe.IsConnected); } }

        internal NotificationController()
        {
            Settings = JsonConvert.DeserializeObject<Common.Models.Settings>(File.ReadAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")));
            string dir = Settings.DataFolder + "Profiles\\";
            if (!Directory.Exists(dir))
            {
                return;
            }

            string[] files = Directory.GetFiles(dir);
            foreach (string file in files)
            {
                Profile p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(file));
                profiles.Add(p);
                cancelationTokens.Add(p.Key, new CancellationTokenSource());
            }
        }

        internal bool SendCloseCommand()
        {
            try
            {
                if (!clientPipe.IsConnected) ConnectClient();

                clientPipe.WriteString("/close");

                return true;
            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                return false;
            }
        }

        internal bool SendReloadCommand(string key)
        {
            try
            {
                if (!clientPipe.IsConnected) ConnectClient();

                clientPipe.WriteString($"/Reload=>{key}");

                return true;
            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                return false;
            }
        }


        internal void ConnectClient()
        {
            clientPipe = new ClientPipe(".", Settings.GUID, p => p.StartStringReaderAsync());
            clientPipe.DataReceived += (sndr, args) =>
            {
                OphiussaLogger.logger.Info("Client recieved:" + args.String);
                MessageBox.Show(args.String);
            };
            clientPipe.Connect();
        }
        internal void CloseClient()
        {
            clientPipe.Close();
        }

        internal void StartServer()
        {
            try
            {
                OphiussaLogger.logger.Info("Notifications Service is Starting");
                serverPipe = new ServerPipe(Settings.GUID, p => p.StartStringReaderAsync());
                serverPipe.DataReceived += (sndr, args) =>
                {
                    OphiussaLogger.logger.Info("Server recieved:" + args.String);
                    if (args.String == "/close")
                    {
                        serverPipe.WriteString("Notifications Service is Closing");
                        closing = true;
                    }
                    else if (args.String.StartsWith("/Reload=>"))
                    {
                        string[] str = args.String.Split('>');
                        serversToReload.Enqueue(str[1]);
                    }
                    else
                    {
                        OphiussaLogger.logger.Info("Command not recognized:" + args.String);
                    }
                };
                serverPipe.Connected += (sndr, args) =>
                {
                    OphiussaLogger.logger.Info("Client Connected");
                };
                serverPipe.PipeClosed += (sndr, args) =>
                {
                    OphiussaLogger.logger.Info("Server Closed");
                };
                Task t = Task.Run(() =>
                {
                    while (true)
                    {
                        if (closing) serverPipe.Close();
                        //run until receive command to close;
                        if (closing) break;
                        if (cancellationTokenPipeServer.IsCancellationRequested) break;
                        Thread.Sleep(1000);
                    }
                }, cancellationTokenPipeServer.Token);
                tasks.Add(t);

                profiles.ForEach(p =>
                {
                    switch (p.Type.ServerType)
                    {
                        case Common.Models.SupportedServers.EnumServerType.ArkSurviveEvolved:
                        case Common.Models.SupportedServers.EnumServerType.ArkSurviveAscended:
                            Task t2 = Task.Run(() => SendArkNotifications(p), cancellationTokenPipeServer.Token);
                            tasks.Add(t2);
                            break;
                        case Common.Models.SupportedServers.EnumServerType.Valheim:
                            //dont do nothing
                            break;
                    }

                });

                Task.WaitAll(tasks.ToArray());

            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
            }
        }

        private async void SendArkNotifications(Profile profile)
        {
            DateTime lastSend = DateTime.Now;
            while (true)
            {
                try
                {
                    Profile p = profile;
                    if (p.ARKConfiguration.Administration.EnableInterval)
                    {
                        if (p.IsRunning)
                        {
                            foreach (var key in serversToReload)
                            {
                                if (serversToReload.TryDequeue(out var key2))
                                {
                                    if (key2 == p.Key) { p.LoadProfile(true); } else { serversToReload.Enqueue(key2); }

                                    OphiussaLogger.logger.Info("Reloaded Profile " + p.Name);
                                }
                            }
                            TimeSpan ts = DateTime.Now - lastSend;
                            if (ts.TotalMinutes >= p.ARKConfiguration.Administration.MODInterval)
                            {
                                RCON rcon = new RCON(IPAddress.Parse(p.ARKConfiguration.Administration.LocalIP), ushort.Parse(p.ARKConfiguration.Administration.RCONPort), p.ARKConfiguration.Administration.ServerAdminPassword);
                                await rcon.ConnectAsync();

                                string respnose = await rcon.SendCommandAsync($"Broadcast {p.ARKConfiguration.Administration.MOD}");
                                lastSend = DateTime.Now;
                            }
                        }
                    }

                    if (closing) break;
                    if (cancelationTokens[p.Key].IsCancellationRequested) break;
                    Thread.Sleep(1000);

                }
                catch (Exception ex)
                {
                    OphiussaLogger.logger.Error(ex.Message);
                }
            }
        }
    }
}
