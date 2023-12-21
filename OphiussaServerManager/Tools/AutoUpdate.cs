using CoreRCON;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models.SupportedServers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace OphiussaServerManager.Tools.Update
{
    public class AutoUpdate
    {

        public event EventHandler<ProcessEventArg> ProgressChanged;
        public event EventHandler<ProcessEventArg> ProcessStarted;
        public event EventHandler<ProcessEventArg> ProcessCompleted;
        public event EventHandler<ProcessEventArg> ProcessError;

        protected virtual bool OnProgressChanged(ProcessEventArg e)
        {

            OphiussaLogger.logger.Info(e.Message);
            ProgressChanged?.Invoke(this, e);
            return true;
        }
        protected virtual void OnProcessCompleted(ProcessEventArg e)
        {
            OphiussaLogger.logger.Info(e.Message);
            ProcessCompleted?.Invoke(this, e);
        }
        protected virtual void OnProcessStarted(ProcessEventArg e)
        {
            OphiussaLogger.logger.Info(e.Message);
            ProcessStarted?.Invoke(this, e);
        }
        protected virtual void OnProcessError(ProcessEventArg e)
        {
            OphiussaLogger.logger.Info(e.Message);
            OphiussaLogger.logger.Error(e.Message);
            ProcessError?.Invoke(this, e);
        }


        internal void UpdateSingleServerManually(string profileKey, bool UpdateCacheFolder, bool StartInTheEnd)
        {
            try
            {
                OnProcessStarted(new ProcessEventArg() { Message = "Process Started for server " + profileKey, Sucessful = true });
                Common.Models.Settings Settings = JsonConvert.DeserializeObject<Common.Models.Settings>(File.ReadAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")));
                string dir = Settings.DataFolder + "Profiles\\";
                if (!Directory.Exists(dir))
                {
                    return;
                }

                Profile p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(System.IO.Path.Combine(dir, profileKey + ".json")));

                if (p.IsRunning) OnProcessError(new ProcessEventArg() { SendToDiscord = false, Sucessful = false, Message = "Cannot update the server while running" });

                List<Task> tasks = new List<Task>();

                var serverType = new CacheServerTypes()
                {
                    Type = p.Type,
                    InstallCacheFolder = System.IO.Path.Combine(Settings.DataFolder, "cache", p.Type.KeyName)
                };

                if (UpdateCacheFolder)
                {
                    tasks = new List<Task>();
                    Task t1 = Task.Run(() =>
                    {
                        OnProgressChanged(new ProcessEventArg() { Message = "Update cache folder for game " + p.Type.KeyName, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                        NetworkTools.UpdateCacheFolder(serverType);
                    });
                    tasks.Add(t1);

                    Task.WaitAll(tasks.ToArray());

                }

                UpdateServer(p, Settings, System.IO.Path.Combine(Settings.DataFolder, "cache", p.Type.KeyName), true);


                if (StartInTheEnd)
                {
                    p.StartServer(Settings);
                }

            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                OnProcessError(new ProcessEventArg() { Message = ex.Message, Sucessful = false, isError = true });
            }
        }

        internal async void UpdateSingleServerJob1(string profileKey, bool restartOnlyToUpdate)
        {
            try
            {
                OnProcessStarted(new ProcessEventArg() { Message = "Process Started for server " + profileKey, Sucessful = true });
                Common.Models.Settings Settings = JsonConvert.DeserializeObject<Common.Models.Settings>(File.ReadAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")));
                string dir = Settings.DataFolder + "Profiles\\";
                if (!Directory.Exists(dir))
                {
                    return;
                }

                Profile p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(System.IO.Path.Combine(dir, profileKey + ".json")));

                List<Task> tasks = new List<Task>();
                if (!restartOnlyToUpdate)
                {
                    if (p.IsRunning)
                    {

                        Task t = Task.Run(async () =>
                        {
                            await CloseServer(p, Settings);
                        });

                        tasks.Add(t);

                        Task.WaitAll(tasks.ToArray());
                    }


                    DateTime startDate = DateTime.Now;

                    while (p.IsRunning)
                    {

                        OnProgressChanged(new ProcessEventArg() { Message = "Server Still running", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                        TimeSpan ts = DateTime.Now - startDate;
                        if (ts.TotalSeconds > 5) await CloseServer(p, Settings, true);
                        Thread.Sleep(5000);
                    }
                }

                var serverType = new CacheServerTypes()
                {
                    Type = p.Type,
                    InstallCacheFolder = System.IO.Path.Combine(Settings.DataFolder, "cache", p.Type.KeyName)
                };

                if (p.AutoManageSettings.ShutdownServer1PerformUpdate)
                {
                    tasks = new List<Task>();
                    Task t1 = Task.Run(() =>
                    {
                        OnProgressChanged(new ProcessEventArg() { Message = "Update cache folder for game " + p.Type.KeyName, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                        NetworkTools.UpdateCacheFolder(serverType);
                    });
                    tasks.Add(t1);

                    Task.WaitAll(tasks.ToArray());

                    UpdateServer(p, Settings, System.IO.Path.Combine(Settings.DataFolder, "cache", p.Type.KeyName), true);
                }

                if (p.AutoManageSettings.ShutdownServer1Restart && !restartOnlyToUpdate)
                {
                    if (!p.IsRunning)
                    {

                        OnProgressChanged(new ProcessEventArg() { Message = "Starting server " + p.Type.KeyName, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                        p.StartServer(Settings);

                    }
                }

            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                OnProcessError(new ProcessEventArg() { Message = ex.Message, Sucessful = false, isError = true });
            }
        }

        internal void UpdateSingleServerJob2(string profileKey, bool restartOnlyToUpdate)
        {
            try
            {
                OnProcessStarted(new ProcessEventArg() { Message = "Process Started for server " + profileKey, Sucessful = true });
                Common.Models.Settings Settings = JsonConvert.DeserializeObject<Common.Models.Settings>(File.ReadAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")));
                string dir = Settings.DataFolder + "Profiles\\";
                if (!Directory.Exists(dir))
                {
                    return;
                }

                Profile p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(System.IO.Path.Combine(dir, profileKey + ".json")));

                List<Task> tasks = new List<Task>();
                if (!restartOnlyToUpdate)
                {
                    if (p.IsRunning)
                    {

                        Task t = Task.Run(async () =>
                        {
                            await CloseServer(p, Settings);
                        });

                        tasks.Add(t);

                        Task.WaitAll(tasks.ToArray());
                    }

                    while (p.IsRunning)
                    {

                        OnProgressChanged(new ProcessEventArg() { Message = "Server Still running", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                        Thread.Sleep(5000);
                    }
                }

                var serverType = new CacheServerTypes()
                {
                    Type = p.Type,
                    InstallCacheFolder = System.IO.Path.Combine(Settings.DataFolder, "cache", p.Type.KeyName)
                };

                if (p.AutoManageSettings.ShutdownServer2PerformUpdate)
                {
                    tasks = new List<Task>();
                    Task t1 = Task.Run(() =>
                    {
                        OnProgressChanged(new ProcessEventArg() { Message = "Update cache folder for game " + p.Type.KeyName, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                        NetworkTools.UpdateCacheFolder(serverType);
                    });
                    tasks.Add(t1);

                    Task.WaitAll(tasks.ToArray());

                    UpdateServer(p, Settings, System.IO.Path.Combine(Settings.DataFolder, "cache", p.Type.KeyName), true);
                }

                if (p.AutoManageSettings.ShutdownServer2Restart && !restartOnlyToUpdate)
                {
                    if (!p.IsRunning)
                    {

                        OnProgressChanged(new ProcessEventArg() { Message = "Starting server " + p.Type.KeyName, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                        p.StartServer(Settings);

                    }
                }

            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                OnProcessError(new ProcessEventArg() { Message = ex.Message, Sucessful = false, isError = true });
            }
        }

        public void UpdateAllServers()
        {
            try
            {
                OnProcessStarted(new ProcessEventArg() { Message = "Process Started", Sucessful = true });
                Common.Models.Settings Settings = JsonConvert.DeserializeObject<Common.Models.Settings>(File.ReadAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")));
                string dir = Settings.DataFolder + "Profiles\\";
                if (!Directory.Exists(dir))
                {
                    return;
                }

                List<Profile> profiles = new List<Profile>();
                List<CacheServerTypes> servers = new List<CacheServerTypes>();

                OnProgressChanged(new ProcessEventArg() { Message = "Loading Profiles", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                string[] files = Directory.GetFiles(dir);
                foreach (string file in files)
                {
                    Profile p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(file));
                    //if (p.AutoManageSettings.IncludeInAutoUpdate)
                    //{
                    profiles.Add(p);
                    //}
                }
                foreach (var p in profiles)
                {
                    if (servers.Find(x3 => p.Type == x3.Type) == null)
                    {
                        servers.Add(new CacheServerTypes()
                        {
                            Type = p.Type,
                            InstallCacheFolder = System.IO.Path.Combine(Settings.DataFolder, "cache", p.Type.KeyName)
                        });
                    }
                }

                List<Task> tasks = new List<Task>();

                foreach (var p in servers)
                {
                    Task t = Task.Run(() =>
                    {
                        OnProgressChanged(new ProcessEventArg() { Message = "Update cache folder for game " + p.Type.KeyName, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                        NetworkTools.UpdateCacheFolder(p);
                    });
                    tasks.Add(t);
                }

                Task.WaitAll(tasks.ToArray());

                foreach (var t in profiles)
                {
                    if (t.AutoManageSettings.IncludeInAutoUpdate)
                    {
                        OnProgressChanged(new ProcessEventArg() { Message = "Checking updated for server " + t.Name, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                        UpdateServer(t, Settings, System.IO.Path.Combine(Settings.DataFolder, "cache", t.Type.KeyName), false);
                    }
                    else if (t.AutoManageSettings.RestartIfShutdown)
                    {
                        if (!t.IsRunning)
                        {
                            OnProgressChanged(new ProcessEventArg() { Message = "Starting server " + t.Name, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                            t.StartServer(Settings);
                        }
                    }
                }
                OnProcessCompleted(new ProcessEventArg() { Message = "Process Ended", Sucessful = true });

            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                OnProcessError(new ProcessEventArg() { Message = ex.Message, Sucessful = false, isError = true });
            }
        }

        private string GetCacheBuild(Profile p, string CacheFolder)
        {

            string fileName = p.Type.ManifestFileName;
            if (!File.Exists(System.IO.Path.Combine(CacheFolder, "steamapps", fileName))) return "";

            string[] content = File.ReadAllText(System.IO.Path.Combine(CacheFolder, "steamapps", fileName)).Split('\n');

            foreach (var item in content)
            {
                string[] t = item.Split('\t');

                if (item.Contains("buildid"))
                {
                    return t[3].Replace("\"", "");
                }

            }
            return System.IO.File.ReadAllText(System.IO.Path.Combine(CacheFolder, "steamapps", fileName));
        }

        private void UpdateServer(Profile p, Common.Models.Settings Settings, string CacheFolder, bool DontStartServer)
        {
            OnProgressChanged(new ProcessEventArg() { Message = "Getting builds", IsStarting = true, ProcessedFileCount = 0, Sucessful = false, TotalFiles = 0 });
            string currentServerBuild = p.GetBuild();
            string currentCacheBuild = GetCacheBuild(p, CacheFolder);


            OnProgressChanged(new ProcessEventArg() { Message = "Cache Build : " + currentCacheBuild, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
            OnProgressChanged(new ProcessEventArg() { Message = "Server Build : " + currentServerBuild, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

            //if (currentServerBuild == currentCacheBuild)
            //{

            //    if (p.IsRunning || p.AutoManageSettings.AutoStartServer) Utils.ExecuteAsAdmin(System.IO.Path.Combine(p.InstallLocation, p.Type.ExecutablePath), p.ARKConfiguration.GetCommandLinesArguments(Settings, p, p.ARKConfiguration.Administration.LocalIP), false);
            //    return;
            //}

            List<FileInfo> changedFiles = new List<FileInfo>();
            if (currentServerBuild != currentCacheBuild)
            {

                OnProgressChanged(new ProcessEventArg() { Message = "Comparing cache with production files", IsStarting = false, ProcessedFileCount = 0, Sucessful = false, TotalFiles = 0 });
                if (Settings.UseSmartCopy)
                {
                    List<string> ignoredFolders = new List<string>();
                    switch (p.Type.ServerType)
                    {
                        case EnumServerType.ArkSurviveEvolved:
                        case EnumServerType.ArkSurviveAscended:
                            ignoredFolders = new List<string> { "Saved", "genosl", "Privacy" };
                            break;
                        case EnumServerType.Valheim:
                            //see all
                            break;
                        default:
                            break;
                    }

                    changedFiles = Utils.CompareFolderContent(p.InstallLocation, CacheFolder, ignoredFolders);
                    if (currentServerBuild == currentCacheBuild && changedFiles.Count == 1)
                    {
                        changedFiles = new List<FileInfo>();
                    }
                }
                else
                {
                    if (currentServerBuild == currentCacheBuild)
                    {
                        changedFiles = new List<FileInfo>();
                    }
                    else
                    {
                        System.IO.DirectoryInfo dir1 = new System.IO.DirectoryInfo(CacheFolder);
                        // Take a snapshot of the file system.  
                        changedFiles = dir1.GetFiles("*.*", System.IO.SearchOption.AllDirectories).ToList();
                    }
                }
            }
            else
            {
                changedFiles = new List<FileInfo>();
            }

            if (changedFiles.Count == 0) OnProgressChanged(new ProcessEventArg() { Message = "No changed detected", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0 });
            else OnProgressChanged(new ProcessEventArg() { Message = $"Detected changes in {changedFiles.Count} files for server {p.Name}", IsStarting = true, ProcessedFileCount = 0, Sucessful = false, TotalFiles = changedFiles.Count, SendToDiscord = true });

            OnProgressChanged(new ProcessEventArg() { Message = $"Mod list check", IsStarting = true, ProcessedFileCount = 0, Sucessful = false, TotalFiles = 0 });

            List<CurseForgeFileDetail> curseForgeFileDetails = new List<CurseForgeFileDetail>();
            List<PublishedFileDetail> steamFileDetails = new List<PublishedFileDetail>();

            bool needToUpdate = false;
            switch (p.Type.ModsSource)
            {
                case ModSource.SteamWorkshop:
                    steamFileDetails = CheckSteamMods(p, Settings, CacheFolder);
                    if (steamFileDetails.Count > 0) OnProgressChanged(new ProcessEventArg() { Message = "Detected mod Updates", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                    else OnProgressChanged(new ProcessEventArg() { Message = "No mods changed", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0 });
                    break;
                case ModSource.CurseForge:
                    curseForgeFileDetails = CheckSCurseForgeMods(p, Settings, CacheFolder);
                    if (curseForgeFileDetails.Count > 0) OnProgressChanged(new ProcessEventArg() { Message = "Detected mod Updates", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                    else OnProgressChanged(new ProcessEventArg() { Message = "No mods changed", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0 });
                    break;
            }

            if (curseForgeFileDetails.Count > 0)
            {
                needToUpdate = true;
                foreach (var item in curseForgeFileDetails)
                {
                    OnProgressChanged(new ProcessEventArg() { Message = "Curse Mod changed:" + item.name, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                }
            }
            if (steamFileDetails.Count > 0)
            {
                needToUpdate = true;
                foreach (var item in steamFileDetails)
                {
                    OnProgressChanged(new ProcessEventArg() { Message = "Steam Mod changed:" + item.title, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                }
            }
            if (changedFiles.Count > 0) needToUpdate = true;

            bool IsRunning = p.IsRunning;

            if (needToUpdate)
            {

                if (needToUpdate && p.IsRunning)
                {
                    Task t = Task.Run(async () =>
                    {
                        await CloseServer(p, Settings);
                    });

                    Task.WaitAll(t);
                    while (p.IsRunning)
                    {
                        OnProgressChanged(new ProcessEventArg() { Message = "Server Still running", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                        Thread.Sleep(5000);
                    }
                }

                UpdateSteamMods(Settings, p, steamFileDetails);

                int i = 1;
                foreach (var file in changedFiles)
                {

                    string targetpath = file.FullName.Replace(CacheFolder, p.InstallLocation);

                    FileInfo file1 = new FileInfo(targetpath);

                    if (!Directory.Exists(file1.DirectoryName))
                    {
                        Directory.CreateDirectory(file1.DirectoryName);
                    }
                    bool notCopied = true;
                    int attempt = 1;
                    while (notCopied)
                    {
                        try
                        {

                            OnProgressChanged(new ProcessEventArg() { Message = $"Copying files {i}/{changedFiles.Count} => {file.FullName}", IsStarting = false, ProcessedFileCount = i, Sucessful = false, TotalFiles = changedFiles.Count });
                            System.IO.File.Copy(file.FullName, file.FullName.Replace(CacheFolder, p.InstallLocation), true);
                            notCopied = false;
                        }
                        catch (Exception ex)
                        {
                            OnProcessError(new ProcessEventArg() { Message = $"Error copying file {file.FullName} attempt {attempt}/5 => {ex.Message}", IsStarting = false, ProcessedFileCount = i, Sucessful = false, TotalFiles = changedFiles.Count });

                        }
                        if (attempt >= 5) notCopied = false;

                        attempt++;
                    }

                    i++;
                }
                OnProgressChanged(new ProcessEventArg() { Message = $"Server {p.Name} updated", IsStarting = false, ProcessedFileCount = changedFiles.Count, Sucessful = true, TotalFiles = changedFiles.Count, SendToDiscord = true });

                if (!DontStartServer) if (IsRunning || p.AutoManageSettings.RestartIfShutdown)
                    {
                        OnProgressChanged(new ProcessEventArg() { Message = "Starting server " + p.Name, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                        p.StartServer(Settings);
                    }
            }
            else
            {
                if (!DontStartServer) if (!p.IsRunning & p.AutoManageSettings.RestartIfShutdown)
                    {
                        OnProgressChanged(new ProcessEventArg() { Message = "Starting server " + p.Name, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                        p.StartServer(Settings);
                    }
            }
            OnProcessCompleted(new ProcessEventArg() { Message = $"Server {p.Name} updated", isError = false, IsStarting = false, SendToDiscord = true, Sucessful = true });
        }

        private void UpdateSteamMods(Settings settings, Profile p, List<PublishedFileDetail> steamFileDetails)
        {
            foreach (var mod in steamFileDetails)
            {
                OnProgressChanged(new ProcessEventArg() { Message = $"updating mod {mod.title}", IsStarting = false });

                var serverType = new CacheServerTypes()
                {
                    Type = p.Type,
                    InstallCacheFolder = System.IO.Path.Combine(settings.DataFolder, "cacheMods"),
                    ModId = mod.publishedfileid
                };

                NetworkTools.UpdateModCacheFolder(serverType);


                System.IO.DirectoryInfo dir1 = new System.IO.DirectoryInfo(System.IO.Path.Combine(serverType.InstallCacheFolder, $"steamapps\\workshop\\content\\{p.Type.SteamClientID}\\{mod.publishedfileid}"));
                // Take a snapshot of the file system.  
                var modFiles = dir1.GetFiles("*.*", System.IO.SearchOption.AllDirectories).ToList();

                int i = 1;
                foreach (var file in modFiles)
                {
                    string targetpath = file.FullName.Replace(System.IO.Path.Combine(serverType.InstallCacheFolder, $"steamapps\\workshop\\content\\{p.Type.SteamClientID}"), System.IO.Path.Combine(p.InstallLocation, "ShooterGame\\Content\\Mods"));

                    FileInfo file1 = new FileInfo(targetpath);

                    if (!Directory.Exists(file1.DirectoryName))
                    {
                        Directory.CreateDirectory(file1.DirectoryName);
                    }
                    System.IO.File.Copy(file.FullName, targetpath, true);
                    //OnProgressChanged(new ProcessEventArg() { Message = $"Copying files {i}/{modFiles.Count} => {file.FullName}", IsStarting = false, ProcessedFileCount = i, Sucessful = false, TotalFiles = modFiles.Count });

                    i++;
                }

                OnProgressChanged(new ProcessEventArg() { Message = $"updated mod {mod.title}", IsStarting = false, ProcessedFileCount = i, Sucessful = true, TotalFiles = modFiles.Count });

            }

        }

        public async Task CloseServer(Profile p, Settings settings, bool ForceKillProcess = false)
        {
            OnProgressChanged(new ProcessEventArg() { Message = "Closing server " + p.Name, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

            await p.CloseServer(settings, OnProgressChanged, ForceKillProcess);
        }

        public List<PublishedFileDetail> CheckSteamMods(Profile p, Settings Settings, string CacheFolder)
        {
            SteamUtils steamUtils = new SteamUtils(Settings);
            PublishedFileDetailsResponse mods = steamUtils.GetSteamModDetails(p.ARKConfiguration.Administration.ModIDs.FindAll(x => x != ""));
            bool isFirstRun = false;
            string cache = System.IO.Path.Combine(Settings.DataFolder, "cache", "SteamModsCache", p.ARKConfiguration.Administration.Branch);
            if (!Directory.Exists(cache))
            {
                Directory.CreateDirectory(cache);

                isFirstRun = true;
            }

            FileInfo fileInfo = new FileInfo((System.IO.Path.Combine(cache, $"SteamModCache_{p.Key}.json")));
            if (!fileInfo.Exists)
            {
                string jsonString = JsonConvert.SerializeObject(mods, Formatting.Indented);
                File.WriteAllText(System.IO.Path.Combine(cache, $"SteamModCache_{p.Key}.json"), jsonString);
            }


            PublishedFileDetailsResponse cachedmods = JsonConvert.DeserializeObject<PublishedFileDetailsResponse>(System.IO.File.ReadAllText(System.IO.Path.Combine(cache, $"SteamModCache_{p.Key}.json")));
            List<PublishedFileDetail> changedMods = new List<PublishedFileDetail>();
            if (isFirstRun)
            {
                changedMods = cachedmods.publishedfiledetails;
            }
            else
            {
                changedMods = cachedmods.publishedfiledetails.FindAll(m => CheckMod(m, mods) != null);

                PublishedFileDetail CheckMod(PublishedFileDetail mod, PublishedFileDetailsResponse modList)
                {
                    PublishedFileDetail m = modList.publishedfiledetails.Find(mm => mm.publishedfileid == mod.publishedfileid);
                    if (m == null) return null;
                    if (m.time_updated != mod.time_updated) return mod;
                    else return null;
                }

            }

            string jsonString1 = JsonConvert.SerializeObject(mods, Formatting.Indented);
            File.WriteAllText(System.IO.Path.Combine(cache, $"SteamModCache_{p.Key}.json"), jsonString1);

            return changedMods ?? new List<PublishedFileDetail>();
        }

        public List<CurseForgeFileDetail> CheckSCurseForgeMods(Profile p, Settings Settings, string CacheFolder)
        {
            CurseForgeUtils curseForgeUtils = new CurseForgeUtils(Settings);
            CurseForgeFileDetailResponse mods = curseForgeUtils.GetCurseForgeModDetails(p.ARKConfiguration.Administration.ModIDs.FindAll(x => x != ""));

            string cache = System.IO.Path.Combine(Settings.DataFolder, "cache", "CFCache", p.ARKConfiguration.Administration.Branch);
            if (!Directory.Exists(cache))
            {
                Directory.CreateDirectory(cache);

            }

            FileInfo fileInfo = new FileInfo((System.IO.Path.Combine(cache, $"CurseForgeModCache_{p.Key}.json")));
            if (!fileInfo.Exists)
            {
                string jsonString = JsonConvert.SerializeObject(mods, Formatting.Indented);
                File.WriteAllText(System.IO.Path.Combine(cache, $"CurseForgeModCache_{p.Key}.json"), jsonString);
            }

            CurseForgeFileDetailResponse cachedmods = JsonConvert.DeserializeObject<CurseForgeFileDetailResponse>(System.IO.File.ReadAllText(System.IO.Path.Combine(cache, $"CurseForgeModCache_{p.Key}.json")));

            List<CurseForgeFileDetail> changedMods = cachedmods.data?.FindAll(m => CheckMod(m, mods) != null);

            CurseForgeFileDetail CheckMod(CurseForgeFileDetail mod, CurseForgeFileDetailResponse modList)
            {
                CurseForgeFileDetail m = modList.data.Find(mm => mm.id == mod.id);
                if (m == null) return null;
                if (m.dateModified != mod.dateModified) return mod;
                else return null;
            }

            string jsonString1 = JsonConvert.SerializeObject(mods, Formatting.Indented);
            File.WriteAllText(System.IO.Path.Combine(cache, $"CurseForgeModCache_{p.Key}.json"), jsonString1);

            return changedMods ?? new List<CurseForgeFileDetail>();
        }
    }
}
