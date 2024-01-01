using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ionic.Zlib;
using Newtonsoft.Json;
using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models.SupportedServers;

namespace OphiussaServerManager.Tools.Update {
    public class AutoUpdate {
        public const string MODTYPE_UNKNOWN = "0";
        public const string MODTYPE_MAP     = "2";
        public const string MODTYPE_MAPEXT  = "4";
        public const string MODTYPE_MOD     = "1";
        public const string MODTYPE_TOTCONV = "3";

        private static List<string>                OfficialMods { get; set; } = new List<string>();
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


        internal void UpdateSingleServerManually(string profileKey, bool updateCacheFolder, bool startInTheEnd, bool forceUpdateMods) {
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

                UpdateServer(p, settings, Path.Combine(settings.DataFolder, "cache", p.Type.KeyName), true, forceUpdateMods);


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

                    UpdateServer(p, settings, Path.Combine(settings.DataFolder, "cache", p.Type.KeyName), true, false);
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

                    UpdateServer(p, settings, Path.Combine(settings.DataFolder, "cache", p.Type.KeyName), true, false);
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

                        UpdateServer(t, settings, Path.Combine(settings.DataFolder, "cache", t.Type.KeyName), false, false);
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

        private void UpdateServer(Profile p, Settings settings, string cacheFolder, bool dontStartServer, bool forceUpdateMods) {
            OnProgressChanged(new ProcessEventArg { Message = "Getting builds", IsStarting = true, ProcessedFileCount = 0, Sucessful = false, TotalFiles = 0 });
            string currentServerBuild = p.GetBuild();
            string currentCacheBuild  = GetCacheBuild(p, cacheFolder);


            OnProgressChanged(new ProcessEventArg { Message = "Cache Build : "  + currentCacheBuild, IsStarting  = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
            OnProgressChanged(new ProcessEventArg { Message = "Server Build : " + currentServerBuild, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

            //if (currentServerBuild == currentCacheBuild)
            //{

            //    if (p.IsRunning || p.AutoManageSettings.AutoStartServer) Utils.ExecuteAsAdmin(System.IO.Path.Combine(p.InstallLocation, p.Type.ExecutablePath), p.ARKConfiguration.GetCommandLinesArguments(Settings, p, p.ArkConfiguration.LocalIP), false);
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
                    steamFileDetails = CheckSteamMods(p, settings, cacheFolder, forceUpdateMods);
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

                if (p.Type.ModsSource == ModSource.SteamWorkshop) UpdateSteamMods(settings, p, steamFileDetails);

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

            OnProcessCompleted(new ProcessEventArg { Message = $"Server {p.Name} update finished", IsError = false, IsStarting = false, SendToDiscord = true, Sucessful = true });
        }

        public async Task CloseServer(Profile p, Settings settings, bool forceKillProcess = false) {
            OnProgressChanged(new ProcessEventArg { Message = "Closing server " + p.Name, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

            await p.CloseServer(settings, OnProgressChanged, forceKillProcess);
        }

        public List<PublishedFileDetail> CheckSteamMods(Profile p, Settings settings, string cacheFolder, bool forceUpdateMods) {
            var    steamUtils = new SteamUtils(settings);
            var    mods       = steamUtils.GetSteamModDetails(p.ArkConfiguration.ActiveMods.Split(',').ToList());
            bool   isFirstRun = false;
            string cache      = Path.Combine(settings.DataFolder, "cache", "SteamModsCache", p.ArkConfiguration.Branch);
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
                changedMods = cachedmods?.Publishedfiledetails?.FindAll(m => CheckMod(m, mods) != null);

                PublishedFileDetail CheckMod(PublishedFileDetail mod, PublishedFileDetailsResponse modList) {
                    var m = modList?.Publishedfiledetails.Find(mm => mm.Publishedfileid == mod.Publishedfileid);
                    if (m             == null) return null;
                    if (m.TimeUpdated != mod.TimeUpdated) return mod;
                    return null;
                }
            }

            if (forceUpdateMods) changedMods = mods.Publishedfiledetails;

            string jsonString1 = JsonConvert.SerializeObject(mods, Formatting.Indented);
            File.WriteAllText(Path.Combine(cache, $"SteamModCache_{p.Key}.json"), jsonString1);

            return changedMods ?? new List<PublishedFileDetail>();
        }

        public List<CurseForgeFileDetail> CheckSCurseForgeMods(Profile p, Settings settings, string cacheFolder) {
            var curseForgeUtils = new CurseForgeUtils(settings);
            var mods            = curseForgeUtils.GetCurseForgeModDetails(p.ArkConfiguration.ActiveMods.Split(',').ToList());

            string cache = Path.Combine(settings.DataFolder, "cache", "CFCache", p.ArkConfiguration.Branch);
            if (!Directory.Exists(cache)) Directory.CreateDirectory(cache);

            var fileInfo = new FileInfo(Path.Combine(cache, $"CurseForgeModCache_{p.Key}.json"));
            if (!fileInfo.Exists) {
                string jsonString = JsonConvert.SerializeObject(mods, Formatting.Indented);
                File.WriteAllText(Path.Combine(cache, $"CurseForgeModCache_{p.Key}.json"), jsonString);
            }

            var cachedmods = JsonConvert.DeserializeObject<CurseForgeFileDetailResponse>(File.ReadAllText(Path.Combine(cache, $"CurseForgeModCache_{p.Key}.json")));

            var changedMods = cachedmods?.Data?.FindAll(m => CheckMod(m, mods) != null);

            CurseForgeFileDetail CheckMod(CurseForgeFileDetail mod, CurseForgeFileDetailResponse modList) {
                var m = modList.Data.Find(mm => mm.Id == mod.Id);
                if (m              == null) return null;
                if (m.DateModified != mod.DateModified) return mod;
                return null;
            }

            string jsonString1               = JsonConvert.SerializeObject(mods, Formatting.Indented);
            File.WriteAllText(Path.Combine(cache, $"CurseForgeModCache_{p.Key}.json"), jsonString1);

            return changedMods ?? new List<CurseForgeFileDetail>();
        }

        private void UpdateSteamMods(Settings settings, Profile p, List<PublishedFileDetail> steamFileDetails) {
            string modPaths = Path.Combine(p.InstallLocation, "ShooterGame\\Content\\Mods");

            var d = new DirectoryInfo(modPaths);

            var modFiles = d.GetFiles("*.*", SearchOption.TopDirectoryOnly).ToList();
            //DELETE OLD MODS

            var modsToDelete = new List<string>();
            modFiles.ForEach(f => {
                                 var modDet = steamFileDetails.Find(m => m.Publishedfileid == f.Name.Replace(".mod", ""));
                                 if (modDet == null) modsToDelete.Add(f.Name.Replace(".mod", ""));
                             });

            foreach (string mod in modsToDelete) {
                if (mod == "111111111") continue;
                File.Delete(Path.Combine(modPaths,      mod + ".mod"));
                Directory.Delete(Path.Combine(modPaths, mod), true);
            }

            foreach (var mod in steamFileDetails) {
                OnProgressChanged(new ProcessEventArg { Message = $"updating mod {mod.Title}", IsStarting = false });

                var serverType = new CacheServerTypes {
                                                          Type               = p.Type,
                                                          InstallCacheFolder = Path.Combine(settings.DataFolder, "cacheMods"),
                                                          ModId              = mod.Publishedfileid
                                                      };

                NetworkTools.UpdateModCacheFolder(serverType);

                string OrignFolder = Path.Combine(serverType.InstallCacheFolder, $"steamapps\\workshop\\content\\{p.Type.SteamClientId}");

                string modCachePath  = GetModCachePath(serverType.InstallCacheFolder, mod.Publishedfileid, p.Type.SteamClientId.ToString());
                string cacheTimeFile = GetLatestModCacheTimeFile(serverType.InstallCacheFolder, mod.Publishedfileid, p.Type.SteamClientId.ToString());
                string modPath       = GetModPath(p.InstallLocation, mod.Publishedfileid);
                string modTimeFile   = GetLatestModTimeFile(p.InstallLocation, mod.Publishedfileid);

                CopyMod(modCachePath, modPath, mod.Publishedfileid);
                /*      var dir1 = new DirectoryInfo(Path.Combine(serverType.InstallCacheFolder, $"steamapps\\workshop\\content\\{p.Type.SteamClientId}\\{mod.Publishedfileid}"));
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
      */
                OnProgressChanged(new ProcessEventArg { Message = $"updated mod {mod.Title}", IsStarting = false, Sucessful = true });
            }
        }


        public static void CopyMod(string sourceFolder, string destinationFolder, string modId) {
            if (string.IsNullOrWhiteSpace(sourceFolder) || !Directory.Exists(sourceFolder))
                throw new DirectoryNotFoundException($"Source folder was not found.\r\n{sourceFolder}");

            string modSourceFolder = sourceFolder;

            // progressCallback?.Invoke(0, "Reading mod base information.");

            string fileName = IOUtils.NormalizePath(Path.Combine(modSourceFolder, "mod.info"));
            var    list     = new List<string>();
            ParseBaseInformation(fileName, list);

            //   progressCallback?.Invoke(0, "Reading mod meta information.");

            fileName = IOUtils.NormalizePath(Path.Combine(modSourceFolder, "modmeta.info"));
            var metaInformation = new Dictionary<string, string>();
            if (ParseMetaInformation(fileName, metaInformation))
                modSourceFolder = IOUtils.NormalizePath(Path.Combine(modSourceFolder, "WindowsNoEditor"));

            string modFile = $"{destinationFolder}.mod";

            //   progressCallback?.Invoke(0, "Deleting existing mod files.");

            // delete the server mod folder and mod file.
            if (Directory.Exists(destinationFolder))
                Directory.Delete(destinationFolder, true);
            if (File.Exists(modFile))
                File.Delete(modFile);

            //    progressCallback?.Invoke(0, "Copying mod files.");

            // update the mod files from the cache.
            bool flag = Copy(modSourceFolder, destinationFolder, true);

            if (metaInformation.Count == 0 && flag)
                metaInformation["ModType"] = "1";

            //    progressCallback?.Invoke(0, "Creating mod file.");

            // create the mod file.
            WriteModFile(modFile, modId, metaInformation, list);

            // copy the last updated file.
            fileName = IOUtils.NormalizePath(Path.Combine(sourceFolder, "LastUpdatedOSM.txt"));
            if (File.Exists(fileName)) {
                //       progressCallback?.Invoke(0, "Copying mod version file.");

                string tempFile = IOUtils.NormalizePath(fileName.Replace(sourceFolder, destinationFolder));
                File.Copy(fileName, tempFile, true);
            }
        }

        private static void UE4ChunkUnzip(string source, string destination) {
            using (var inReader = new BinaryReader(File.Open(source, FileMode.Open))) {
                using (var binaryWriter = new BinaryWriter(File.Open(destination, FileMode.Create))) {
                    var fcompressedChunkInfo1 = new FCompressedChunkInfo();
                    fcompressedChunkInfo1.Serialize(inReader);
                    var fcompressedChunkInfo2 = new FCompressedChunkInfo();
                    fcompressedChunkInfo2.Serialize(inReader);

                    long num1 = fcompressedChunkInfo1.CompressedSize;
                    long num2 = fcompressedChunkInfo1.UncompressedSize;
                    if (num2 == 2653586369L)
                        num2 = 131072L;
                    long length = (fcompressedChunkInfo2.UncompressedSize + num2 - 1L) / num2;

                    var  fcompressedChunkInfoArray = new FCompressedChunkInfo[length];
                    long val2                      = 0L;

                    for (int index = 0; index < length; ++index) {
                        fcompressedChunkInfoArray[index] = new FCompressedChunkInfo();
                        fcompressedChunkInfoArray[index].Serialize(inReader);
                        val2 = Math.Max(fcompressedChunkInfoArray[index].CompressedSize, val2);
                    }

                    for (long index = 0L; index < length; ++index) {
                        var    fcompressedChunkInfo3 = fcompressedChunkInfoArray[index];
                        byte[] buffer                = ZlibStream.UncompressBuffer(inReader.ReadBytes((int)fcompressedChunkInfo3.CompressedSize));
                        binaryWriter.Write(buffer);
                    }
                }
            }
        }

        public static bool Copy(string sourceFolder, string destinationFolder, bool copySubFolders) {
            if (!Directory.Exists(sourceFolder))
                return false;

            bool flag = false;

            foreach (string sourceFile in Directory.GetFiles(sourceFolder, "*.*", copySubFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)) {
                string modFile     = IOUtils.NormalizePath(sourceFile.Replace(sourceFolder, destinationFolder));
                string modFilePath = Path.GetDirectoryName(modFile);

                if (!Directory.Exists(modFilePath))
                    Directory.CreateDirectory(modFilePath);

                if (Path.GetFileNameWithoutExtension(sourceFile).Contains("PrimalGameData"))
                    flag = true;

                Copy(sourceFile, modFilePath);
            }

            return flag;
        }

        public static void Copy(string sourceFile, string destinationFolder) {
            string fileExtension = Path.GetExtension(sourceFile).ToUpper();

            if (string.Compare(fileExtension, ".uncompressed_size", StringComparison.OrdinalIgnoreCase) != 0) {
                string tempFile = Path.Combine(destinationFolder, Path.GetFileName(sourceFile));

                if (string.Compare(fileExtension, ".z", StringComparison.OrdinalIgnoreCase) == 0)
                    UE4ChunkUnzip(sourceFile, tempFile.Substring(0, tempFile.Length - 2));
                else
                    File.Copy(sourceFile, tempFile, true);
            }
        }

        public static void WriteModFile(string fileName, string modId, Dictionary<string, string> metaInformation, List<string> mapNames) {
            using (var outWriter = new BinaryWriter(File.Open(fileName, FileMode.Create))) {
                ulong num1 = ulong.Parse(modId);
                outWriter.Write(num1);
                WriteUE4String("ModName",    outWriter);
                WriteUE4String(string.Empty, outWriter);
                int count1 = mapNames.Count;
                outWriter.Write(count1);
                for (int index = 0; index < mapNames.Count; ++index) WriteUE4String(mapNames[index], outWriter);

                uint num2 = 4280483635U;
                outWriter.Write(num2);
                int num3 = 2;
                outWriter.Write(num3);
                byte num4 = metaInformation.ContainsKey("ModType") ? (byte)1 : (byte)0;
                outWriter.Write(num4);
                int count2 = metaInformation.Count;
                outWriter.Write(count2);
                foreach (var keyValuePair in metaInformation) {
                    WriteUE4String(keyValuePair.Key,   outWriter);
                    WriteUE4String(keyValuePair.Value, outWriter);
                }
            }
        }

        private static void WriteUE4String(string writeString, BinaryWriter writer) {
            byte[] bytes = Encoding.UTF8.GetBytes(writeString);
            int    num1  = bytes.Length + 1;
            writer.Write(num1);
            writer.Write(bytes);
            byte num2 = 0;
            writer.Write(num2);
        }

        public static bool ParseMetaInformation(string fileName, Dictionary<string, string> metaInformation) {
            if (!File.Exists(fileName))
                return false;

            using (var binaryReader = new BinaryReader(File.Open(fileName, FileMode.Open))) {
                int num = binaryReader.ReadInt32();
                for (int index1 = 0; index1 < num; ++index1) {
                    string index2 = string.Empty;
                    int    count1 = binaryReader.ReadInt32();
                    bool   flag1  = false;
                    if (count1 < 0) {
                        flag1  = true;
                        count1 = -count1;
                    }

                    if (!flag1 && count1 > 0) {
                        byte[] bytes = binaryReader.ReadBytes(count1);
                        index2 = Encoding.UTF8.GetString(bytes, 0, bytes.Length - 1);
                    }

                    string str    = string.Empty;
                    int    count2 = binaryReader.ReadInt32();
                    bool   flag2  = false;
                    if (count2 < 0) {
                        flag2  = true;
                        count2 = -count2;
                    }

                    if (!flag2 && count2 > 0) {
                        byte[] bytes = binaryReader.ReadBytes(count2);
                        str = Encoding.UTF8.GetString(bytes, 0, bytes.Length - 1);
                    }

                    metaInformation[index2] = str;
                }
            }

            return true;
        }

        public static bool ParseBaseInformation(string fileName, List<string> mapNames) {
            if (!File.Exists(fileName))
                return false;

            using (var reader = new BinaryReader(File.Open(fileName, FileMode.Open))) {
                string readString1;
                ReadUE4String(reader, out readString1);

                int num = reader.ReadInt32();
                for (int index = 0; index < num; ++index) {
                    string readString2;
                    ReadUE4String(reader, out readString2);
                    mapNames.Add(readString2);
                }
            }

            return true;
        }

        private static void ReadUE4String(BinaryReader reader, out string readString) {
            readString = string.Empty;
            int  count = reader.ReadInt32();
            bool flag  = false;
            if (count < 0) {
                flag  = true;
                count = -count;
            }

            if (flag || count <= 0)
                return;
            byte[] bytes = reader.ReadBytes(count);
            readString = Encoding.UTF8.GetString(bytes, 0, bytes.Length - 1);
        }

        public static string GetModCachePath(string installCacheDir, string modId, string appId) {
            string workshopPath = string.Format("steamapps\\workshop\\content\\{0}\\", appId);
            return IOUtils.NormalizePath(Path.Combine(installCacheDir, workshopPath, modId));
        }

        public static string GetModPath(string installDirectory, string modId) {
            return IOUtils.NormalizePath(Path.Combine(installDirectory, "ShooterGame\\Content\\Mods", modId));
        }

        public static string GetLatestModCacheTimeFile(string installCacheDir, string modId, string appId) {
            return IOUtils.NormalizePath(Path.Combine(GetModCachePath(installCacheDir, modId, appId), "LastUpdatedOSM.txt"));
        }

        public static string GetLatestModTimeFile(string installDirectory, string modId) {
            return IOUtils.NormalizePath(Path.Combine(installDirectory, "ShooterGame\\Content\\Mods", modId, "LastUpdatedOSM.txt"));
        }

        private class FCompressedChunkInfo {
            public const uint LOADING_COMPRESSION_CHUNK_SIZE = 131072U;
            public const uint PACKAGE_FILE_TAG               = 2653586369U;
            public const uint PACKAGE_FILE_TAG_SWAPPED       = 3246598814U;

            public long CompressedSize;
            public long UncompressedSize;

            public void Serialize(BinaryReader reader) {
                CompressedSize   = reader.ReadInt64();
                UncompressedSize = reader.ReadInt64();
            }
        }
    }
}