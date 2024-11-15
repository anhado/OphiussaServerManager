﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Threading;
using Microsoft.Win32.TaskScheduler;
using Newtonsoft.Json;
using NLog.LayoutRenderers;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.Enums;
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
                OnProcessStarted(new ProcessEventArg { Message = "Install Process Started for server " + e.Profile.Key, Sucessful = true });
                if (!ConnectionController.Plugins.ContainsKey(e.Profile.Type)) return;
                var nCtrl = new PluginController(ConnectionController.Plugins[e.Profile.Type].PluginLocation());
                e.Profile.AdditionalSettings = JsonConvert.SerializeObject(e.Profile, Formatting.Indented);
                Message m = nCtrl.SetProfile(e.Profile);
                if (!m.Success) {
                    throw m.Exception;
                }
                else {
                    OnProgressChanged(new ProcessEventArg { Message = "New Plugin Controller Initialized" });

                    if (e.InstallFromCache) {
                        OnProgressChanged(new ProcessEventArg { Message = "Updating Cache" });
                        NetworkTools.UpdateCacheFolder(nCtrl, e.ShowSteamCMD);
                        OnProgressChanged(new ProcessEventArg { Message = "Cache Updated" });
                    }
                    else {
                        OnProgressChanged(new ProcessEventArg { Message = "Updating game folder" });
                        NetworkTools.UpdateGameFolder(e.Profile, e.ShowSteamCMD);
                        OnProgressChanged(new ProcessEventArg { Message = "Game Folder Updated" });
                    }

                    if (e.InstallFromCache) UpdateServerFromCache(nCtrl);
                    OnProcessCompleted(new ProcessEventArg { Message = "Installed server " + e.Profile.Key, Sucessful = true });

                    if (e.StartServerAtEnd) nCtrl.StartServer();
                }
            }
            catch (Exception ex) {
                OnProcessError(new ProcessEventArg { Message = ex.Message, Sucessful = false, IsError = true });
            }
        }

        public static void BackupServerClick(object sender, OphiussaEventArgs e) {

            try {

                PluginController nCtrl = new PluginController(ConnectionController.Plugins[e.Profile.Type].PluginLocation());
                Message m= nCtrl.SetProfile(e.Profile);
                if (!m.Success) {
                    throw m.Exception;
                }
                else {
                    OphiussaLogger.Logger.Debug($"Backup Server {nCtrl.Name}");
                    if (!Directory.Exists(ConnectionController.Settings.BackupFolder)) Directory.CreateDirectory(ConnectionController.Settings.BackupFolder);
                    if (!Directory.Exists(Path.Combine(ConnectionController.Settings.BackupFolder, "servers", e.Profile.Key))) Directory.CreateDirectory(Path.Combine(ConnectionController.Settings.BackupFolder, "servers", e.Profile.Key));
                    if (!Directory.Exists(Path.Combine(ConnectionController.Settings.BackupFolder, "servers", e.Profile.Key, "temp"))) Directory.CreateDirectory(Path.Combine(ConnectionController.Settings.BackupFolder, "servers", e.Profile.Key, "temp"));

                    string zipFileName = Path.Combine(ConnectionController.Settings.BackupFolder, "servers", e.Profile.Key) + $"\\{e.Profile.Type}_{e.Profile.Key}_{DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture)}.zip";
                    string tempFolder = Path.Combine(ConnectionController.Settings.BackupFolder, "servers", e.Profile.Key, "temp");

                    using (var zip = ZipFile.Open(zipFileName, ZipArchiveMode.Create)) {
                        foreach (var file in nCtrl.FilesToBackup) {
                            string fileName = file.File.FullName;
                            string tempFileName = Path.Combine(tempFolder, System.IO.Path.GetFileName(file.File.FullName));
                            bool copied = false;
                            int ntry = 0;
                            do {
                                try {
                                    File.Copy(fileName, tempFileName, true);
                                    copied = true;
                                    ntry = 99;
                                }
                                catch (Exception exception) {
                                    OphiussaLogger.Logger.Error(exception);
                                    Thread.Sleep(1000);
                                }
                                ntry++;
                            } while (!copied || ntry < 5);

                            zip.CreateEntryFromFile(tempFileName, file.EntryName);
                            System.IO.File.Delete(tempFileName);
                        }
                    }
                    OphiussaLogger.Logger.Debug($"Backup complete for server {nCtrl.Name} to {zipFileName}");

                    //Delete Old Backups
                    DirectoryInfo info = new DirectoryInfo(tempFolder);
                    List<FileInfo> files = info.GetFiles().OrderBy(p => p.CreationTime).ToList();
                    foreach (FileInfo file in files) {
                        var threshold = DateTime.Now.AddDays(-ConnectionController.Settings.DaysToKeep);
                        if (file.CreationTime <= threshold) {
                            System.IO.File.Delete(file.FullName);
                        }
                    }
                    OphiussaLogger.Logger.Debug($"Cleaned old backups");
                }

            }
            catch (Exception exception) {
                OphiussaLogger.Logger.Error(exception);
            }

        }

        public static void StartServerClick(object sender, OphiussaEventArgs e) {
            OphiussaLogger.Logger.Debug($"Starting Server {e.Plugin.Profile.Name}");
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
            OphiussaLogger.Logger.Debug("ReloadServerClick");
            throw new NotImplementedException();
        }

        public static void SyncServerClick(object sender, OphiussaEventArgs e) {
            OphiussaLogger.Logger.Debug("SyncServerClick");
            throw new NotImplementedException();
        }

        public static void SaveServerClick(object sender, OphiussaEventArgs e) {
            OphiussaLogger.Logger.Debug($"Saving server {e.Profile.Name}");
            var profile = e.Profile;
            profile.Type = e.Plugin.GameType;
            profile.AdditionalSettings = null;
            profile.AdditionalCommands = null;
            profile.AdditionalSettings = JsonConvert.SerializeObject(e.Profile, Formatting.Indented);
            profile.AdditionalCommands = JsonConvert.SerializeObject(e.Plugin.DefaultCommands, Formatting.Indented);
            ConnectionController.SqlLite.Upsert<IProfile>(profile);
            foreach (var opt in profile.AutoManagement) {
                opt.ServerKey = profile.Key;
                ConnectionController.SqlLite.Upsert<AutoManagement>(opt);
            }
            OphiussaLogger.Logger.Debug($"Saved server {e.Profile.Name}");

            CreateWindowsTasks(e.Plugin);
            OphiussaLogger.Logger.Debug($"Created task for server {e.Profile.Name}");
        }

        private static void CreateWindowsTasks(IPlugin plugin) {
            #region AutoStartServer

            if (plugin.Profile.AutoStartServer) {
                string fileName = ConnectionController.Settings.DataFolder + $"StartServer\\Run_{plugin.Profile.Key.Replace("-", "")}.bat";
                string taskName = "OphiussaServerManager\\AutoStart_" + plugin.Profile.Key;
                var task = TaskService.Instance.GetTask(taskName);
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
                    task.Definition.Settings.Priority = ProcessPriorityClass.Normal;
                    task.RegisterChanges();
                }
                else {
                    var td = TaskService.Instance.NewTask();
                    td.RegistrationInfo.Description = "Server Auto-Start - " + plugin.Profile.Name;
                    td.Principal.LogonType = TaskLogonType.InteractiveToken;
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
                    td.Settings.Priority = ProcessPriorityClass.Normal;
                    TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td);
                }
            }
            else {
                string taskName = "OphiussaServerManager\\AutoStart_" + plugin.Profile.Key;
                var task = TaskService.Instance.GetTask(taskName);
                if (task != null) TaskService.Instance.RootFolder.DeleteTask(taskName);
            }

            #endregion

            #region Shutdown

            foreach (var am in plugin.Profile.AutoManagement) {
                string fileName = Assembly.GetEntryAssembly()?.Location;
                string taskName = $"OphiussaServerManager\\AutoShutDown_{am.Id:000}_" + plugin.Profile.Key;
                var task = TaskService.Instance.GetTask(taskName);

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
                    var tt = new WeeklyTrigger();

                    int hour = short.Parse(am.ShutdownHour.Split(':')[0]);
                    int minute = short.Parse(am.ShutdownHour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek = daysofweek;
                    task.Definition.Triggers.Add(tt);
                    task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                    task.Definition.Settings.Priority = ProcessPriorityClass.Normal;
                    task.RegisterChanges();
                }
                else {
                    var td = TaskService.Instance.NewTask();
                    td.RegistrationInfo.Description = $"Server Auto-ShutDown {am.Id:000} - " + plugin.Profile.Name;
                    td.Principal.LogonType = TaskLogonType.InteractiveToken;
                    DaysOfTheWeek daysofweek = 0;

                    if (am.ShutdownMon) daysofweek += 2;
                    if (am.ShutdownTue) daysofweek += 4;
                    if (am.ShutdownWed) daysofweek += 8;
                    if (am.ShutdownThu) daysofweek += 16;
                    if (am.ShutdownFri) daysofweek += 32;
                    if (am.ShutdownSat) daysofweek += 64;
                    if (am.ShutdownSun) daysofweek += 1;
                    var tt = new WeeklyTrigger();

                    int hour = short.Parse(am.ShutdownHour.Split(':')[0]);
                    int minute = short.Parse(am.ShutdownHour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek = daysofweek;
                    td.Triggers.Add(tt);
                    td.Actions.Add(fileName, $" -as_{am.Id:000}_" + plugin.Profile.Key);
                    td.Principal.RunLevel = TaskRunLevel.Highest;
                    td.Settings.Priority = ProcessPriorityClass.Normal;

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
            OphiussaLogger.Logger.Debug("OpenRCONClick");
            throw new NotImplementedException();
        }

        public static void ChooseFolderClick(object sender, OphiussaEventArgs e) {
            OphiussaLogger.Logger.Debug("ChooseFolderClick");
            throw new NotImplementedException();
        }

        public static async void RestartServerSingleServer(string serverKey, bool IsTheAutoUpdateTask = false) {
            try {
                string[] args = serverKey.Split('_');
                if (!IsTheAutoUpdateTask) serverKey = args[2];
                var am = !IsTheAutoUpdateTask ? ConnectionController.SqlLite.GetRecord<AutoManagement>($"Id={args[1]}") : null;
                var profile = ConnectionController.SqlLite.GetRecord<IProfile>($"Key='{serverKey}'");
                if (profile != null && (am != null || IsTheAutoUpdateTask)) {
                    OphiussaLogger.Logger.Debug($"Checking Server {profile.Name}");
                    if (!ConnectionController.Plugins.ContainsKey(profile.Type)) return;
                    var nCtrl = new PluginController(ConnectionController.Plugins[profile.Type].PluginLocation());
                    Message m = nCtrl.SetProfile(profile);
                    if (!m.Success) {
                        throw m.Exception;
                    }
                    else {
                        if (IsTheAutoUpdateTask) {
                            string currentServerBuild = Utils.GetBuild(profile);
                            string currentCacheBuild = Utils.GetCacheBuild(profile, nCtrl.CacheFolder);


                            OnProgressChanged(new ProcessEventArg { Message = "Cache Build : " + currentCacheBuild, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                            OnProgressChanged(new ProcessEventArg { Message = "Server Build : " + currentServerBuild, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                            if (currentServerBuild == currentCacheBuild) {
                                OnProgressChanged(new ProcessEventArg { Message = "No update needed " , IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                                return;
                            }
                        }
                        bool isRunning = nCtrl.InternalIsServerRunning;

                        var tasks = new List<Task>();
                        if (isRunning) {
                            OnProcessStarted(new ProcessEventArg { Message = $"Restarting server : {profile.Name}", Sucessful = true });
                            var t = Task.Run(async () => { await nCtrl.StopServer(); });
                            tasks.Add(t);
                        }

                        Task.WaitAll(tasks.ToArray());
                        OnProgressChanged(new ProcessEventArg { Message = $"Stopped server : {profile.Name}", Sucessful = true });

                        var startDate = DateTime.Now;
                        while (nCtrl.InternalIsServerRunning) {
                            var ts = DateTime.Now - startDate;
                            if (ts.TotalMinutes > 5) await nCtrl.StopServer(true);
                            Thread.Sleep(5000);
                        }

                        if ((am != null && am.UpdateServer) || IsTheAutoUpdateTask) {
                            UpdateServerFromCache(nCtrl);
                            OnProgressChanged(new ProcessEventArg { Message = $"Updated server : {profile.Name}", Sucessful = true });
                        }

                        if ((isRunning && (am != null && am.RestartServer)) || (profile.RestartIfShutdown && IsTheAutoUpdateTask)) {
                            nCtrl.StartServer();

                            OnProgressChanged(new ProcessEventArg { Message = $"Started server : {profile.Name}", Sucessful = true });
                        }

                        OnProcessCompleted(new ProcessEventArg { Message = $"Auto Restarted server : {profile.Name}", Sucessful = true });
                    }
                }
                else {
                    OnProcessError(new ProcessEventArg { Message = $"Invalid Server : {serverKey}", Sucessful = false, IsError = true });
                }
            }
            catch (Exception e) {
                OnProcessError(new ProcessEventArg { Message = e.Message, Sucessful = false, IsError = true });
            }
        }

        public static void UpdateServerFromCache(PluginController controller) {
            OphiussaLogger.Logger.Debug($"Updating server {controller.Name} from cache");
            IProfile profile = controller.GetProfile();
            string cacheFolder = controller.CacheFolder;
            string currentServerBuild = Utils.GetBuild(profile);
            string currentCacheBuild = Utils.GetCacheBuild(profile, cacheFolder);
            bool needToUpdate = false;
            var changedFiles = new List<FileInfo>();

            OnProgressChanged(new ProcessEventArg { Message = "Cache Build : " + currentCacheBuild, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
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

            if (changedFiles.Count == 0) OnProgressChanged(new ProcessEventArg { Message = "No changed detected", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0 });
            else OnProgressChanged(new ProcessEventArg { Message = $"Detected changes in {changedFiles.Count} files for server {profile.Name}", IsStarting = true, ProcessedFileCount = 0, Sucessful = false, TotalFiles = changedFiles.Count, SendToDiscord = true });

            if (changedFiles.Count > 0) needToUpdate = true;

            //TODO:Check Mod Updates here

            int i = 1;
            foreach (var file in changedFiles) {
                string targetpath = file.FullName.Replace(cacheFolder, profile.InstallationFolder);

                var file1 = new FileInfo(targetpath);

                if (!Directory.Exists(file1.DirectoryName)) Directory.CreateDirectory(file1.DirectoryName);
                bool notCopied = true;
                int attempt = 1;
                while (notCopied) {
                    try {
                        //   OnProgressChanged(new ProcessEventArg { Message = $"Copying files {i}/{changedFiles.Count} => {file.FullName}", IsStarting = false, ProcessedFileCount = i, Sucessful = false, TotalFiles = changedFiles.Count });
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
            OphiussaLogger.Logger.Debug("Update all servers");
            var servers = new List<ServerCache>();
            Dictionary<string, PluginController> controllers = new Dictionary<string, PluginController>();

            var profiles = ConnectionController.SqlLite.GetRecords<IProfile>();
            profiles.ForEach(prf => {
                                    try {
                                        if (!prf.IncludeAutoUpdate) return;
                                        if (servers.Find(x => x.SteamServerId == prf.SteamServerId) != null) return;
                                        controllers.Add(prf.Key, new PluginController(ConnectionController.Plugins[prf.Type].PluginLocation()));
                                        controllers[prf.Key].SetProfile(prf);
                                        Message m = controllers[prf.Key].SetProfile(prf);
                                        if (!m.Success) {
                                            throw m.Exception;
                                        }
                                        else {
                                            servers.Add(new ServerCache {
                                                Branch = prf.Branch,
                                                SteamServerId = prf.SteamServerId,
                                                Type = prf.Type,
                                                CacheFolder = controllers[prf.Key].CacheFolder
                                            });
                                        }

                                    }
                                    catch (Exception e) {
                                        OphiussaLogger.Logger.Error(e);
                                    }
                             });


            var tasks = new List<Task>();
            foreach (var server in servers) {
                var t = Task.Run(() => { NetworkTools.UpdateCacheFolder(server, false); });

                tasks.Add(t);
            }

            Task.WaitAll(tasks.ToArray());

            if (ConnectionController.Settings.UpdateSequencial)
                foreach (var key in controllers.Keys) {
                    RestartServerSingleServer(key, true);
                }
            else {
                tasks = new List<Task>();

                foreach (var key in controllers.Keys) {
                    var t = Task.Run(() => { RestartServerSingleServer(key, true); });

                    tasks.Add(t);
                }
                Task.WaitAll(tasks.ToArray());

            }
            OphiussaLogger.Logger.Debug("Update all servers Completed");
        }

        public static void BackupAllServers() {

            OphiussaLogger.Logger.Debug("Backup all servers");
            var profiles = ConnectionController.SqlLite.GetRecords<IProfile>();

            profiles.ForEach(prf => {
                try {
                    if (!prf.IncludeAutoBackup) return;
                    var ctrl = new PluginController(ConnectionController.Plugins[prf.Type].PluginLocation());
                    Message m = ctrl.SetProfile(prf);
                    if (!m.Success) {
                        throw m.Exception;
                    }
                    else {
                        ctrl.BackupServer();
                    }
                }
                catch (Exception e) {
                    OphiussaLogger.Logger.Error(e);
                }
            });
            OphiussaLogger.Logger.Debug("Backup all servers Completed");
        }
    }
}