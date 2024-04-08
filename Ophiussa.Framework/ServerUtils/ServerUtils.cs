using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

        public static event EventHandler<ProcessEventArg> ProgressChanged;
        public static event EventHandler<ProcessEventArg> ProcessStarted;
        public static event EventHandler<ProcessEventArg> ProcessCompleted;
        public static event EventHandler<ProcessEventArg> ProcessError;

        private static bool OnProgressChanged(ProcessEventArg e) {
            OphiussaLogger.Logger.Info(e.Message);
            ProgressChanged?.Invoke(null, e);
            return true;
        }

        private static void OnProcessCompleted(ProcessEventArg e) {
            OphiussaLogger.Logger.Info(e.Message);
            ProcessCompleted?.Invoke(null, e);
        }

        private static void OnProcessStarted(ProcessEventArg e) {
            OphiussaLogger.Logger.Info(e.Message);
            ProcessStarted?.Invoke(null, e);
        }

        private static void OnProcessError(ProcessEventArg e) {
            OphiussaLogger.Logger.Info(e.Message);
            OphiussaLogger.Logger.Error(e.Message);
            ProcessError?.Invoke(null, e);
        }
         
        public static void InstallServerClick(object sender, OphiussaEventArgs e) {
            try {
                OnProcessStarted(new ProcessEventArg { Message = "Process Started for server " + e.Profile.Key, Sucessful = true });
                if (!ConnectionController.Plugins.ContainsKey(e.Profile.Type)) return;
                var nCtrl = new PluginController(ConnectionController.Plugins[e.Profile.Type].PluginLocation());
                e.Profile.AdditionalSettings = JsonConvert.SerializeObject(e.Profile, Formatting.Indented);
                nCtrl.SetProfile(e.Profile);

                OnProgressChanged(new ProcessEventArg { Message = "New Plugin Controller Initialized" });

                OnProgressChanged(new ProcessEventArg { Message = "Updating Cache" });
                NetworkTools.UpdateCacheFolder(nCtrl);

                OnProgressChanged(new ProcessEventArg { Message = "Cache Updated" });
                UpdateServerFromCache(nCtrl);

            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                OnProcessError(new ProcessEventArg { Message = ex.Message, Sucessful = false, IsError = true });
            }
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
                string fileName = Assembly.GetEntryAssembly()?.Location;
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

        public static async void RestartServerSingleServer(string serverKey, bool IsTheAutoUpdateTask = false) {
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

                    if (am.UpdateServer || IsTheAutoUpdateTask) { 
                       UpdateServerFromCache(nCtrl);
                       OphiussaLogger.Logger.Info($"Updated server : {args[2]}");
                    }

                    if ((isRunning && am.RestartServer) || (profile.RestartIfShutdown && IsTheAutoUpdateTask)) {
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

        public static void UpdateServerFromCache(PluginController controller) {
            IProfile profile            = controller.GetProfile();
            string   cacheFolder        = controller.CacheFolder;
            string   currentServerBuild = Utils.GetBuild(profile);
            string   currentCacheBuild  = Utils.GetCacheBuild(profile, cacheFolder);
            bool     needToUpdate       = false;
            var      changedFiles       = new List<FileInfo>();

            OnProgressChanged(new ProcessEventArg { Message = "Cache Build : "  + currentCacheBuild, IsStarting  = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
            OnProgressChanged(new ProcessEventArg { Message = "Server Build : " + currentServerBuild, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

            if (currentServerBuild != currentCacheBuild) {
                if (ConnectionController.Settings.UseSmartCopy) {
                    OnProgressChanged(new ProcessEventArg { Message = "Comparing cache with production files", IsStarting = false, ProcessedFileCount = 0, Sucessful = false, TotalFiles = 0 });
                    var ignoredFolders = new List<string>();

                    changedFiles = Utils.CompareFolderContent(profile.InstallationFolder, cacheFolder, controller.IgnoredFoldersInComparision);
                    if (currentServerBuild == currentCacheBuild && changedFiles.Count == 1) changedFiles = new List<FileInfo>();
                }
                else {
                    if (currentServerBuild == currentCacheBuild) {
                        changedFiles = new List<FileInfo>();
                    }
                    else {
                        var dir1 = new DirectoryInfo(cacheFolder);
                        // Take a snapshot of the file system.  
                        changedFiles = dir1.GetFiles("*.*", SearchOption.AllDirectories).ToList();
                    }
                }
            }
            else {
                changedFiles = new List<FileInfo>();
            }

            if (changedFiles.Count == 0) OnProgressChanged(new ProcessEventArg { Message = "No changed detected", IsStarting                                                 = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles  = 0 });
            else OnProgressChanged(new ProcessEventArg { Message                         = $"Detected changes in {changedFiles.Count} files for server {profile.Name}", IsStarting = true, ProcessedFileCount  = 0, Sucessful = false, TotalFiles = changedFiles.Count, SendToDiscord = true });

            if (changedFiles.Count > 0) needToUpdate = true;

            //TODO:Check Mod Updates here

            int i = 1;
            foreach (var file in changedFiles) {
                string targetpath = file.FullName.Replace(cacheFolder, profile.InstallationFolder);

                var file1 = new FileInfo(targetpath);

                if (!Directory.Exists(file1.DirectoryName)) Directory.CreateDirectory(file1.DirectoryName);
                bool notCopied = true;
                int  attempt   = 1;
                while (notCopied) {
                    try {
                        OnProgressChanged(new ProcessEventArg { Message = $"Copying files {i}/{changedFiles.Count} => {file.FullName}", IsStarting = false, ProcessedFileCount = i, Sucessful = false, TotalFiles = changedFiles.Count });
                        File.Copy(file.FullName, file.FullName.Replace(cacheFolder, profile.InstallationFolder), true);
                        notCopied = false;
                    }
                    catch (Exception ex) {
                        OnProcessError(new ProcessEventArg { Message = $"Error copying file {file.FullName} attempt {attempt}/5 => {ex.Message}", IsStarting = false, ProcessedFileCount = i, Sucessful = false, TotalFiles = changedFiles.Count });
                    }

                    if (attempt >= 5) notCopied = false;

                    attempt++;
                }

                i++;
            }
            OnProgressChanged(new ProcessEventArg { Message = $"Server {profile.Name} updated", IsStarting = false, ProcessedFileCount = changedFiles.Count, Sucessful = true, TotalFiles = changedFiles.Count, SendToDiscord = true });

        }

        public static void UpdateAllServers() {
            var                                  servers     = new List<ServerCache>();
            Dictionary<string, PluginController> controllers = new Dictionary<string, PluginController>();

            var profiles = ConnectionController.SqlLite.GetRecords<IProfile>();
            profiles.ForEach(prf => {
                                 if (!prf.IncludeAutoUpdate) return;
                                 if (servers.Find(x => x.SteamServerId == prf.SteamServerId) != null) return;
                                 controllers.Add(prf.Key, new PluginController(ConnectionController.Plugins[prf.Type].PluginLocation()));
                                 controllers[prf.Key].SetProfile(prf);
                                 servers.Add(new ServerCache {
                                                                 Branch        = prf.Branch,
                                                                 SteamServerId = prf.SteamServerId,
                                                                 Type          = prf.Type,
                                                                 CacheFolder   = controllers[prf.Key].CacheFolder
                                });
                             });


            var tasks = new List<Task>();
            foreach (var server in servers) {
                var t = Task.Run(() => { NetworkTools.UpdateCacheFolder(server); });

                tasks.Add(t);
            }

            Task.WaitAll(tasks.ToArray());

            if (ConnectionController.Settings.UpdateSequencial)
                foreach (var key in controllers.Keys) {
                    RestartServerSingleServer(key,true);
                }
            else {
                tasks = new List<Task>();

                foreach (var key in controllers.Keys) {
                    var t = Task.Run(() => { RestartServerSingleServer(key,true); });

                    tasks.Add(t);
                } 
                Task.WaitAll(tasks.ToArray());

            }
        }

        public static void BackupAllServers() {
            throw new NotImplementedException();
        }
    }
}