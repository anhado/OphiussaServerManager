using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models.SupportedServers;

namespace OphiussaServerManager.Tools.Update {
    public class AutoUpdate {
        public event EventHandler<ProcessEventArg> ProgressChanged;
        public event EventHandler<ProcessEventArg> ProcessStarted;
        public event EventHandler<ProcessEventArg> ProcessCompleted;
        public event EventHandler<ProcessEventArg> ProcessError;

        protected virtual bool OnProgressChanged(ProcessEventArg e) {
            OphiussaLogger.Logger.Info(e.Message);
            ProgressChanged?.Invoke(this, e);
            return true;
        }

        protected virtual void OnProcessCompleted(ProcessEventArg e) {
            OphiussaLogger.Logger.Info(e.Message);
            ProcessCompleted?.Invoke(this, e);
        }

        protected virtual void OnProcessStarted(ProcessEventArg e) {
            OphiussaLogger.Logger.Info(e.Message);
            ProcessStarted?.Invoke(this, e);
        }

        protected virtual void OnProcessError(ProcessEventArg e) {
            OphiussaLogger.Logger.Info(e.Message);
            OphiussaLogger.Logger.Error(e.Message);
            ProcessError?.Invoke(this, e);
        }


        internal void UpdateSingleServerManually(string profileKey, bool updateCacheFolder, bool startInTheEnd) {
            try {
                OnProcessStarted(new ProcessEventArg { Message = "Process Started for server " + profileKey, Sucessful = true });
                var    settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")));
                string dir      = settings.DataFolder + "Profiles\\";
                if (!Directory.Exists(dir)) return;

                var p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(Path.Combine(dir, profileKey + ".json")));

                if (p.IsRunning) OnProcessError(new ProcessEventArg { SendToDiscord = false, Sucessful = false, Message = "Cannot update the server while running" });

                var tasks = new List<Task>();

                var serverType = new CacheServerTypes {
                                                          Type               = p.Type,
                                                          InstallCacheFolder = Path.Combine(settings.DataFolder, "cache", p.Type.KeyName)
                                                      };

                if (updateCacheFolder) {
                    tasks = new List<Task>();
                    var t1 = Task.Run(() => {
                                          OnProgressChanged(new ProcessEventArg { Message = "Update cache folder for game " + p.Type.KeyName, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                                          NetworkTools.UpdateCacheFolder(serverType);
                                      });
                    tasks.Add(t1);

                    Task.WaitAll(tasks.ToArray());
                }

                UpdateServer(p, settings, Path.Combine(settings.DataFolder, "cache", p.Type.KeyName), true);


                if (startInTheEnd) p.StartServer(settings);
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                OnProcessError(new ProcessEventArg { Message = ex.Message, Sucessful = false, IsError = true });
            }
        }

        internal async void UpdateSingleServerJob1(string profileKey, bool restartOnlyToUpdate) {
            try {
                OnProcessStarted(new ProcessEventArg { Message = "Process Started for server " + profileKey, Sucessful = true });
                var    settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")));
                string dir      = settings.DataFolder + "Profiles\\";
                if (!Directory.Exists(dir)) return;

                var p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(Path.Combine(dir, profileKey + ".json")));

                var tasks = new List<Task>();
                if (!restartOnlyToUpdate) {
                    if (p.IsRunning) {
                        var t = Task.Run(async () => { await CloseServer(p, settings); });

                        tasks.Add(t);

                        Task.WaitAll(tasks.ToArray());
                    }


                    var startDate = DateTime.Now;

                    while (p.IsRunning) {
                        OnProgressChanged(new ProcessEventArg { Message = "Server Still running", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                        var ts = DateTime.Now - startDate;
                        if (ts.TotalMinutes > 5) await CloseServer(p, settings, true);
                        Thread.Sleep(5000);
                    }
                }

                var serverType = new CacheServerTypes {
                                                          Type               = p.Type,
                                                          InstallCacheFolder = Path.Combine(settings.DataFolder, "cache", p.Type.KeyName)
                                                      };

                if (p.AutoManageSettings.ShutdownServer1PerformUpdate) {
                    tasks = new List<Task>();
                    var t1 = Task.Run(() => {
                                          OnProgressChanged(new ProcessEventArg { Message = "Update cache folder for game " + p.Type.KeyName, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                                          NetworkTools.UpdateCacheFolder(serverType);
                                      });
                    tasks.Add(t1);

                    Task.WaitAll(tasks.ToArray());

                    UpdateServer(p, settings, Path.Combine(settings.DataFolder, "cache", p.Type.KeyName), true);
                }

                if (p.AutoManageSettings.ShutdownServer1Restart && !restartOnlyToUpdate)
                    if (!p.IsRunning) {
                        OnProgressChanged(new ProcessEventArg { Message = "Starting server " + p.Type.KeyName, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                        p.StartServer(settings);
                    }
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                OnProcessError(new ProcessEventArg { Message = ex.Message, Sucessful = false, IsError = true });
            }
        }

        internal void UpdateSingleServerJob2(string profileKey, bool restartOnlyToUpdate) {
            try {
                OnProcessStarted(new ProcessEventArg { Message = "Process Started for server " + profileKey, Sucessful = true });
                var    settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")));
                string dir      = settings.DataFolder + "Profiles\\";
                if (!Directory.Exists(dir)) return;

                var p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(Path.Combine(dir, profileKey + ".json")));

                var tasks = new List<Task>();
                if (!restartOnlyToUpdate) {
                    if (p.IsRunning) {
                        var t = Task.Run(async () => { await CloseServer(p, settings); });

                        tasks.Add(t);

                        Task.WaitAll(tasks.ToArray());
                    }

                    while (p.IsRunning) {
                        OnProgressChanged(new ProcessEventArg { Message = "Server Still running", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                        Thread.Sleep(5000);
                    }
                }

                var serverType = new CacheServerTypes {
                                                          Type               = p.Type,
                                                          InstallCacheFolder = Path.Combine(settings.DataFolder, "cache", p.Type.KeyName)
                                                      };

                if (p.AutoManageSettings.ShutdownServer2PerformUpdate) {
                    tasks = new List<Task>();
                    var t1 = Task.Run(() => {
                                          OnProgressChanged(new ProcessEventArg { Message = "Update cache folder for game " + p.Type.KeyName, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                                          NetworkTools.UpdateCacheFolder(serverType);
                                      });
                    tasks.Add(t1);

                    Task.WaitAll(tasks.ToArray());

                    UpdateServer(p, settings, Path.Combine(settings.DataFolder, "cache", p.Type.KeyName), true);
                }

                if (p.AutoManageSettings.ShutdownServer2Restart && !restartOnlyToUpdate)
                    if (!p.IsRunning) {
                        OnProgressChanged(new ProcessEventArg { Message = "Starting server " + p.Type.KeyName, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                        p.StartServer(settings);
                    }
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                OnProcessError(new ProcessEventArg { Message = ex.Message, Sucessful = false, IsError = true });
            }
        }

        public void UpdateAllServers() {
            try {
                OnProcessStarted(new ProcessEventArg { Message = "Process Started", Sucessful = true });
                var    settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")));
                string dir      = settings.DataFolder + "Profiles\\";
                if (!Directory.Exists(dir)) return;

                var profiles = new List<Profile>();
                var servers  = new List<CacheServerTypes>();

                OnProgressChanged(new ProcessEventArg { Message = "Loading Profiles", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                string[] files = Directory.GetFiles(dir);
                foreach (string file in files) {
                    var p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(file));
                    //if (p.AutoManageSettings.IncludeInAutoUpdate)
                    //{
                    profiles.Add(p);
                    //}
                }

                foreach (var p in profiles)
                    if (servers.Find(x3 => p.Type.ServerType == x3.Type.ServerType) == null)
                        servers.Add(new CacheServerTypes {
                                                             Type               = p.Type,
                                                             InstallCacheFolder = Path.Combine(settings.DataFolder, "cache", p.Type.KeyName)
                                                         });

                var tasks = new List<Task>();

                foreach (var p in servers) {
                    var t = Task.Run(() => {
                                         OnProgressChanged(new ProcessEventArg { Message = "Update cache folder for game " + p.Type.KeyName, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                                         NetworkTools.UpdateCacheFolder(p);
                                     });
                    tasks.Add(t);
                }

                Task.WaitAll(tasks.ToArray());

                foreach (var t in profiles)
                    if (t.AutoManageSettings.IncludeInAutoUpdate) {
                        OnProgressChanged(new ProcessEventArg { Message = "Checking updated for server " + t.Name, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                        UpdateServer(t, settings, Path.Combine(settings.DataFolder, "cache", t.Type.KeyName), false);
                    }
                    else if (t.AutoManageSettings.RestartIfShutdown) {
                        if (!t.IsRunning) {
                            OnProgressChanged(new ProcessEventArg { Message = "Starting server " + t.Name, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                            t.StartServer(settings);
                        }
                    }

                OnProcessCompleted(new ProcessEventArg { Message = "Process Ended", Sucessful = true });
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                OnProcessError(new ProcessEventArg { Message = ex.Message, Sucessful = false, IsError = true });
            }
        }

        private string GetCacheBuild(Profile p, string cacheFolder) {
            string fileName = p.Type.ManifestFileName;
            if (!File.Exists(Path.Combine(cacheFolder, "steamapps", fileName))) return "";

            string[] content = File.ReadAllText(Path.Combine(cacheFolder, "steamapps", fileName)).Split('\n');

            foreach (string item in content) {
                string[] t = item.Split('\t');

                if (item.Contains("buildid")) return t[3].Replace("\"", "");
            }

            return File.ReadAllText(Path.Combine(cacheFolder, "steamapps", fileName));
        }

        private void UpdateServer(Profile p, Settings settings, string cacheFolder, bool dontStartServer) {
            OnProgressChanged(new ProcessEventArg { Message = "Getting builds", IsStarting = true, ProcessedFileCount = 0, Sucessful = false, TotalFiles = 0 });
            string currentServerBuild = p.GetBuild();
            string currentCacheBuild  = GetCacheBuild(p, cacheFolder);


            OnProgressChanged(new ProcessEventArg { Message = "Cache Build : "  + currentCacheBuild, IsStarting  = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
            OnProgressChanged(new ProcessEventArg { Message = "Server Build : " + currentServerBuild, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

            //if (currentServerBuild == currentCacheBuild)
            //{

            //    if (p.IsRunning || p.AutoManageSettings.AutoStartServer) Utils.ExecuteAsAdmin(System.IO.Path.Combine(p.InstallLocation, p.Type.ExecutablePath), p.ARKConfiguration.GetCommandLinesArguments(Settings, p, p.ARKConfiguration.Administration.LocalIP), false);
            //    return;
            //}

            var changedFiles = new List<FileInfo>();
            if (currentServerBuild != currentCacheBuild) {
                OnProgressChanged(new ProcessEventArg { Message = "Comparing cache with production files", IsStarting = false, ProcessedFileCount = 0, Sucessful = false, TotalFiles = 0 });
                if (settings.UseSmartCopy) {
                    var ignoredFolders = new List<string>();
                    switch (p.Type.ServerType) {
                        case EnumServerType.ArkSurviveEvolved:
                        case EnumServerType.ArkSurviveAscended:
                            ignoredFolders = new List<string> { "Saved", "genosl", "Privacy" };
                            break;
                        case EnumServerType.Valheim:
                            //see all
                            break;
                    }

                    changedFiles = Utils.CompareFolderContent(p.InstallLocation, cacheFolder, ignoredFolders);
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
            else OnProgressChanged(new ProcessEventArg { Message                         = $"Detected changes in {changedFiles.Count} files for server {p.Name}", IsStarting = true, ProcessedFileCount  = 0, Sucessful = false, TotalFiles = changedFiles.Count, SendToDiscord = true });

            OnProgressChanged(new ProcessEventArg { Message = "Mod list check", IsStarting = true, ProcessedFileCount = 0, Sucessful = false, TotalFiles = 0 });

            var curseForgeFileDetails = new List<CurseForgeFileDetail>();
            var steamFileDetails      = new List<PublishedFileDetail>();

            bool needToUpdate = false;
            switch (p.Type.ModsSource) {
                case ModSource.SteamWorkshop:
                    steamFileDetails = CheckSteamMods(p, settings, cacheFolder);
                    if (steamFileDetails.Count > 0) OnProgressChanged(new ProcessEventArg { Message = "Detected mod Updates", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                    else OnProgressChanged(new ProcessEventArg { Message                            = "No mods changed", IsStarting      = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0 });
                    break;
                case ModSource.CurseForge:
                    curseForgeFileDetails = CheckSCurseForgeMods(p, settings, cacheFolder);
                    if (curseForgeFileDetails.Count > 0) OnProgressChanged(new ProcessEventArg { Message = "Detected mod Updates", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                    else OnProgressChanged(new ProcessEventArg { Message                                 = "No mods changed", IsStarting      = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0 });
                    break;
            }

            if (curseForgeFileDetails.Count > 0) {
                needToUpdate = true;
                foreach (var item in curseForgeFileDetails) OnProgressChanged(new ProcessEventArg { Message = "Curse Mod changed:" + item.Name, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
            }

            if (steamFileDetails.Count > 0) {
                needToUpdate = true;
                foreach (var item in steamFileDetails) OnProgressChanged(new ProcessEventArg { Message = "Steam Mod changed:" + item.Title, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
            }

            if (changedFiles.Count > 0) needToUpdate = true;

            bool isRunning = p.IsRunning;

            if (needToUpdate) {
                if (needToUpdate && p.IsRunning) {
                    var t = Task.Run(async () => { await CloseServer(p, settings); });

                    Task.WaitAll(t);
                    while (p.IsRunning) {
                        OnProgressChanged(new ProcessEventArg { Message = "Server Still running", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                        Thread.Sleep(5000);
                    }
                }

                UpdateSteamMods(settings, p, steamFileDetails);

                int i = 1;
                foreach (var file in changedFiles) {
                    string targetpath = file.FullName.Replace(cacheFolder, p.InstallLocation);

                    var file1 = new FileInfo(targetpath);

                    if (!Directory.Exists(file1.DirectoryName)) Directory.CreateDirectory(file1.DirectoryName);
                    bool notCopied = true;
                    int  attempt   = 1;
                    while (notCopied) {
                        try {
                            OnProgressChanged(new ProcessEventArg { Message = $"Copying files {i}/{changedFiles.Count} => {file.FullName}", IsStarting = false, ProcessedFileCount = i, Sucessful = false, TotalFiles = changedFiles.Count });
                            File.Copy(file.FullName, file.FullName.Replace(cacheFolder, p.InstallLocation), true);
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

                OnProgressChanged(new ProcessEventArg { Message = $"Server {p.Name} updated", IsStarting = false, ProcessedFileCount = changedFiles.Count, Sucessful = true, TotalFiles = changedFiles.Count, SendToDiscord = true });

                if (!dontStartServer)
                    if (isRunning || p.AutoManageSettings.RestartIfShutdown) {
                        OnProgressChanged(new ProcessEventArg { Message = "Starting server " + p.Name, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                        p.StartServer(settings);
                    }
            }
            else {
                if (!dontStartServer)
                    if (!p.IsRunning & p.AutoManageSettings.RestartIfShutdown) {
                        OnProgressChanged(new ProcessEventArg { Message = "Starting server " + p.Name, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                        p.StartServer(settings);
                    }
            }

            OnProcessCompleted(new ProcessEventArg { Message = $"Server {p.Name} updated", IsError = false, IsStarting = false, SendToDiscord = true, Sucessful = true });
        }

        private void UpdateSteamMods(Settings settings, Profile p, List<PublishedFileDetail> steamFileDetails) {
            foreach (var mod in steamFileDetails) {
                OnProgressChanged(new ProcessEventArg { Message = $"updating mod {mod.Title}", IsStarting = false });

                var serverType = new CacheServerTypes {
                                                          Type               = p.Type,
                                                          InstallCacheFolder = Path.Combine(settings.DataFolder, "cacheMods"),
                                                          ModId              = mod.Publishedfileid
                                                      };

                NetworkTools.UpdateModCacheFolder(serverType);


                var dir1 = new DirectoryInfo(Path.Combine(serverType.InstallCacheFolder, $"steamapps\\workshop\\content\\{p.Type.SteamClientId}\\{mod.Publishedfileid}"));
                // Take a snapshot of the file system.  
                var modFiles = dir1.GetFiles("*.*", SearchOption.AllDirectories).ToList();

                int i = 1;
                foreach (var file in modFiles) {
                    string targetpath = file.FullName.Replace(Path.Combine(serverType.InstallCacheFolder, $"steamapps\\workshop\\content\\{p.Type.SteamClientId}"), Path.Combine(p.InstallLocation, "ShooterGame\\Content\\Mods"));

                    var file1 = new FileInfo(targetpath);

                    if (!Directory.Exists(file1.DirectoryName)) Directory.CreateDirectory(file1.DirectoryName);
                    File.Copy(file.FullName, targetpath, true);
                    //OnProgressChanged(new ProcessEventArg() { Message = $"Copying files {i}/{modFiles.Count} => {file.FullName}", IsStarting = false, ProcessedFileCount = i, Sucessful = false, TotalFiles = modFiles.Count });

                    i++;
                }

                OnProgressChanged(new ProcessEventArg { Message = $"updated mod {mod.Title}", IsStarting = false, ProcessedFileCount = i, Sucessful = true, TotalFiles = modFiles.Count });
            }
        }

        public async Task CloseServer(Profile p, Settings settings, bool forceKillProcess = false) {
            OnProgressChanged(new ProcessEventArg { Message = "Closing server " + p.Name, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

            await p.CloseServer(settings, OnProgressChanged, forceKillProcess);
        }

        public List<PublishedFileDetail> CheckSteamMods(Profile p, Settings settings, string cacheFolder) {
            var    steamUtils = new SteamUtils(settings);
            var    mods       = steamUtils.GetSteamModDetails(p.ArkConfiguration.Administration.ModIDs.FindAll(x => x != ""));
            bool   isFirstRun = false;
            string cache      = Path.Combine(settings.DataFolder, "cache", "SteamModsCache", p.ArkConfiguration.Administration.Branch);
            if (!Directory.Exists(cache)) {
                Directory.CreateDirectory(cache);

                isFirstRun = true;
            }

            var fileInfo = new FileInfo(Path.Combine(cache, $"SteamModCache_{p.Key}.json"));
            if (!fileInfo.Exists) {
                string jsonString = JsonConvert.SerializeObject(mods, Formatting.Indented);
                File.WriteAllText(Path.Combine(cache, $"SteamModCache_{p.Key}.json"), jsonString);
            }


            var cachedmods  = JsonConvert.DeserializeObject<PublishedFileDetailsResponse>(File.ReadAllText(Path.Combine(cache, $"SteamModCache_{p.Key}.json")));
            var changedMods = new List<PublishedFileDetail>();
            if (isFirstRun) {
                changedMods = cachedmods.Publishedfiledetails;
            }
            else {
                changedMods = cachedmods.Publishedfiledetails.FindAll(m => CheckMod(m, mods) != null);

                PublishedFileDetail CheckMod(PublishedFileDetail mod, PublishedFileDetailsResponse modList) {
                    var m = modList.Publishedfiledetails.Find(mm => mm.Publishedfileid == mod.Publishedfileid);
                    if (m             == null) return null;
                    if (m.TimeUpdated != mod.TimeUpdated) return mod;
                    return null;
                }
            }

            string jsonString1 = JsonConvert.SerializeObject(mods, Formatting.Indented);
            File.WriteAllText(Path.Combine(cache, $"SteamModCache_{p.Key}.json"), jsonString1);

            return changedMods ?? new List<PublishedFileDetail>();
        }

        public List<CurseForgeFileDetail> CheckSCurseForgeMods(Profile p, Settings settings, string cacheFolder) {
            var curseForgeUtils = new CurseForgeUtils(settings);
            var mods            = curseForgeUtils.GetCurseForgeModDetails(p.ArkConfiguration.Administration.ModIDs.FindAll(x => x != ""));

            string cache = Path.Combine(settings.DataFolder, "cache", "CFCache", p.ArkConfiguration.Administration.Branch);
            if (!Directory.Exists(cache)) Directory.CreateDirectory(cache);

            var fileInfo = new FileInfo(Path.Combine(cache, $"CurseForgeModCache_{p.Key}.json"));
            if (!fileInfo.Exists) {
                string jsonString = JsonConvert.SerializeObject(mods, Formatting.Indented);
                File.WriteAllText(Path.Combine(cache, $"CurseForgeModCache_{p.Key}.json"), jsonString);
            }

            var cachedmods = JsonConvert.DeserializeObject<CurseForgeFileDetailResponse>(File.ReadAllText(Path.Combine(cache, $"CurseForgeModCache_{p.Key}.json")));

            var changedMods = cachedmods.Data?.FindAll(m => CheckMod(m, mods) != null);

            CurseForgeFileDetail CheckMod(CurseForgeFileDetail mod, CurseForgeFileDetailResponse modList) {
                var m = modList.Data.Find(mm => mm.Id == mod.Id);
                if (m              == null) return null;
                if (m.DateModified != mod.DateModified) return mod;
                return null;
            }

            string jsonString1 = JsonConvert.SerializeObject(mods, Formatting.Indented);
            File.WriteAllText(Path.Combine(cache, $"CurseForgeModCache_{p.Key}.json"), jsonString1);

            return changedMods ?? new List<CurseForgeFileDetail>();
        }
    }
}