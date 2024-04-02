using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using Microsoft.Win32.TaskScheduler;
using Newtonsoft.Json;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;
using Task = System.Threading.Tasks.Task;

namespace OphiussaFramework.ServerUtils {
    public static class ServerUtils {
        public static void InstallServerClick(object sender, OphiussaEventArgs e) {
            NetworkTools.UpdateGameFolder(e.Profile);
        }

        public static void BackupServerClick(object sender, OphiussaEventArgs e) {
            throw new NotImplementedException();
        }

        public static void StartServerClick(object sender, OphiussaEventArgs e) {
            string file = ConnectionController.Settings.DataFolder + $"StartServer\\Run_{e.Plugin.Profile.Key.Replace("-", "")}.bat";
            Utils.ExecuteAsAdmin(file, "", false, false, true);
        }

        public static void StopServerClick(object sender, OphiussaEventArgs e) {
            if (e.ForceStopServer) {
                var pro = Utils.GetProcessRunning(Path.Combine(e.Profile.InstallationFolder, e.Profile.ExecutablePath));
                pro.Kill();
            }
            else {
                Utils.SendCloseCommandCtrlC(Utils.GetProcessRunning(Path.Combine(e.Profile.InstallationFolder, e.Profile.ExecutablePath)));
            }
        }

        public static void ReloadServerClick(object sender, OphiussaEventArgs e) {
            throw new NotImplementedException();
        }

        public static void SyncServerClick(object sender, OphiussaEventArgs e) {
            throw new NotImplementedException();
        }

        public static void SaveServerClick(object sender, OphiussaEventArgs e) {
            var profile = e.Profile;
            profile.Type               = e.Plugin.GameType;
            profile.AdditionalSettings = null;
            profile.AdditionalCommands = null;
            profile.AdditionalSettings = JsonConvert.SerializeObject(e.Profile,                Formatting.Indented);
            profile.AdditionalCommands = JsonConvert.SerializeObject(e.Plugin.DefaultCommands, Formatting.Indented);
            ConnectionController.SqlLite.Upsert<IProfile>(profile);
            foreach (var opt in profile.AutoManagement) {
                opt.ServerKey = profile.Key;
                ConnectionController.SqlLite.Upsert<AutoManagement>(opt);
            }

            CreateWindowsTasks(e.Plugin);
        }

        private static void CreateWindowsTasks(IPlugin plugin) {
            #region AutoStartServer

            if (plugin.Profile.AutoStartServer) {
                string fileName = ConnectionController.Settings.DataFolder + $"StartServer\\Run_{plugin.Profile.Key.Replace("-", "")}.bat";
                string taskName = "OphiussaServerManager\\AutoStart_"      + plugin.Profile.Key;
                var    task     = TaskService.Instance.GetTask(taskName);
                if (task != null) {
                    task.Definition.Triggers.Clear();
                    if (plugin.Profile.StartOnBoot) {
                        var bt1 = new BootTrigger { Delay = TimeSpan.FromMinutes(1) };
                        task.Definition.Triggers.Add(bt1);
                    }
                    else {
                        var lt1 = new LogonTrigger { Delay = TimeSpan.FromMinutes(1) };
                        task.Definition.Triggers.Add(lt1);
                    }

                    task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                    task.Definition.Settings.Priority  = ProcessPriorityClass.Normal;
                    task.RegisterChanges();
                }
                else {
                    var td = TaskService.Instance.NewTask();
                    td.RegistrationInfo.Description = "Server Auto-Start - " + plugin.Profile.Name;
                    td.Principal.LogonType          = TaskLogonType.InteractiveToken;
                    if (plugin.Profile.StartOnBoot) {
                        var bt1 = new BootTrigger { Delay = TimeSpan.FromMinutes(1) };
                        td.Triggers.Add(bt1);
                    }
                    else {
                        var lt1 = new LogonTrigger { Delay = TimeSpan.FromMinutes(1) };
                        td.Triggers.Add(lt1);
                    }

                    td.Actions.Add(fileName);
                    td.Principal.RunLevel = TaskRunLevel.Highest;
                    td.Settings.Priority  = ProcessPriorityClass.Normal;
                    TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td);
                }
            }
            else {
                string taskName = "OphiussaServerManager\\AutoStart_" + plugin.Profile.Key;
                var    task     = TaskService.Instance.GetTask(taskName);
                if (task != null) TaskService.Instance.RootFolder.DeleteTask(taskName);
            }

            #endregion

            #region Shutdown

            foreach (var am in plugin.Profile.AutoManagement) {
                string fileName = Assembly.GetEntryAssembly().Location;
                string taskName = $"OphiussaServerManager\\AutoShutDown_{am.Id:000}_" + plugin.Profile.Key;
                var    task     = TaskService.Instance.GetTask(taskName);

                if (task != null) {
                    task.Definition.Triggers.Clear();

                    DaysOfTheWeek daysofweek = 0;

                    if (am.ShutdownMon) daysofweek += 2;
                    if (am.ShutdownTue) daysofweek += 4;
                    if (am.ShutdownWed) daysofweek += 8;
                    if (am.ShutdownThu) daysofweek += 16;
                    if (am.ShutdownFri) daysofweek += 32;
                    if (am.ShutdownSat) daysofweek += 64;
                    if (am.ShutdownSun) daysofweek += 1;
                    var tt                         = new WeeklyTrigger();

                    int hour   = short.Parse(am.ShutdownHour.Split(':')[0]);
                    int minute = short.Parse(am.ShutdownHour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek    = daysofweek;
                    task.Definition.Triggers.Add(tt);
                    task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                    task.Definition.Settings.Priority  = ProcessPriorityClass.Normal;
                    task.RegisterChanges();
                }
                else {
                    var td = TaskService.Instance.NewTask();
                    td.RegistrationInfo.Description = $"Server Auto-ShutDown {am.Id:000} - " + plugin.Profile.Name;
                    td.Principal.LogonType          = TaskLogonType.InteractiveToken;
                    DaysOfTheWeek daysofweek = 0;

                    if (am.ShutdownMon) daysofweek += 2;
                    if (am.ShutdownTue) daysofweek += 4;
                    if (am.ShutdownWed) daysofweek += 8;
                    if (am.ShutdownThu) daysofweek += 16;
                    if (am.ShutdownFri) daysofweek += 32;
                    if (am.ShutdownSat) daysofweek += 64;
                    if (am.ShutdownSun) daysofweek += 1;
                    var tt                         = new WeeklyTrigger();

                    int hour   = short.Parse(am.ShutdownHour.Split(':')[0]);
                    int minute = short.Parse(am.ShutdownHour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek    = daysofweek;
                    td.Triggers.Add(tt);
                    td.Actions.Add(fileName, $" -as_{am.Id:000}_" + plugin.Profile.Key);
                    td.Principal.RunLevel = TaskRunLevel.Highest;
                    td.Settings.Priority  = ProcessPriorityClass.Normal;

                    TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td);
                }
                //}
                //else {
                //    string taskName = "OphiussaServerManager\\AutoShutDown1_" + plugin.Profile.Key;
                //    var task = TaskService.Instance.GetTask(taskName);
                //    if (task != null) TaskService.Instance.RootFolder.DeleteTask(taskName);
                //}
            }

            #endregion
        }

        public static void OpenRCONClick(object sender, OphiussaEventArgs e) {
            throw new NotImplementedException();
        }

        public static void ChooseFolderClick(object sender, OphiussaEventArgs e) {
            throw new NotImplementedException();
        }

        public static async void RestartServerSingleServer(string serverKey) {
            try {
                string[] args = serverKey.Split('_');
                OphiussaLogger.Logger.Info($"Restarting server : {args[2]}");
                var am      = ConnectionController.SqlLite.GetRecord<AutoManagement>($"Id={args[1]}");
                var profile = ConnectionController.SqlLite.GetRecord<IProfile>($"Key='{args[2]}'");
                if (profile != null && am != null) {
                    if (!ConnectionController.Plugins.ContainsKey(profile.Type)) return;
                    var nCtrl = new PluginController(ConnectionController.Plugins[profile.Type].PluginLocation());
                    nCtrl.SetProfile(profile);
                    bool isRunning = nCtrl.IsRunning;

                    var tasks = new List<Task>();
                    if (isRunning) {
                        var t = Task.Run(async () => { await nCtrl.StopServer(); });
                        tasks.Add(t);
                    }

                    Task.WaitAll(tasks.ToArray());
                    OphiussaLogger.Logger.Info($"Stopped server : {args[2]}");

                    var startDate = DateTime.Now;
                    while (nCtrl.IsRunning) {
                        var ts = DateTime.Now - startDate;
                        if (ts.TotalMinutes > 5) await nCtrl.StopServer(true);
                        Thread.Sleep(5000);
                    }

                    if (am.UpdateServer) {
                        nCtrl.InstallServer(); //TODO: change this line to use the cache copy after auto-update 
                        OphiussaLogger.Logger.Info($"Updated server : {args[2]}");
                    }

                    if (isRunning || am.RestartServer) {
                        nCtrl.StartServer();

                        OphiussaLogger.Logger.Info($"Started server : {args[2]}");
                    }

                    OphiussaLogger.Logger.Info($"Auto Restarted server : {args[2]}");
                }
                else {
                    OphiussaLogger.Logger.Error($"Invalid Server {serverKey}");
                }
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
            }
        }
    }
}