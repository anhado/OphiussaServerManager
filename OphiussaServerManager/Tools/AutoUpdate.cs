﻿using CoreRCON;
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
        public event EventHandler<ProcessCopyEventArg> ProcessStarted;
        public event EventHandler<ProcessCopyEventArg> ProcessCompleted;
        public event EventHandler<ProcessCopyEventArg> ProcessError;

        protected virtual bool OnProgressChanged(ProcessEventArg e)
        {

            OphiussaLogger.logger.Info(e.Message);
            ProgressChanged?.Invoke(this, e);
            return true;
        }
        protected virtual void OnProcessCompleted(ProcessCopyEventArg e)
        {
            OphiussaLogger.logger.Info(e.Message);
            ProcessCompleted?.Invoke(this, e);
        }
        protected virtual void OnProcessStarted(ProcessCopyEventArg e)
        {
            OphiussaLogger.logger.Info(e.Message);
            ProcessStarted?.Invoke(this, e);
        }
        protected virtual void OnProcessError(ProcessCopyEventArg e)
        {
            OphiussaLogger.logger.Info(e.Message);
            OphiussaLogger.logger.Error(e.Message);
            ProcessError?.Invoke(this, e);
        }

        internal void RestartSingleServer(string profileKey, bool restartOnlyToUpdate)
        {
            try
            {
                OnProcessStarted(new ProcessCopyEventArg() { Message = "Process Started for server " + profileKey });
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
                        OphiussaLogger.logger.Debug("Server Still running");
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
                        OphiussaLogger.logger.Info("Update cache folder for game " + p.Type.KeyName);
                        NetworkTools.UpdateCacheFolder(serverType);
                    });
                    tasks.Add(t1);

                    Task.WaitAll(tasks.ToArray());

                    UpdateServer(p, Settings, System.IO.Path.Combine(Settings.DataFolder, "cache", p.Type.KeyName), true);
                }

                if (p.AutoManageSettings.ShutdownServer1Restart && !restartOnlyToUpdate)
                {
                    if (!p.IsRunning) p.StartServer(Settings);
                }

            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                OnProcessError(new ProcessCopyEventArg() { Message = ex.Message, Sucessful = false });
            }
        }
        public void UpdateAllServers()
        {
            try
            {
                OnProcessStarted(new ProcessCopyEventArg() { Message = "Process Started" });
                Common.Models.Settings Settings = JsonConvert.DeserializeObject<Common.Models.Settings>(File.ReadAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")));
                string dir = Settings.DataFolder + "Profiles\\";
                if (!Directory.Exists(dir))
                {
                    return;
                }

                List<Profile> profiles = new List<Profile>();
                List<CacheServerTypes> servers = new List<CacheServerTypes>();

                OphiussaLogger.logger.Info("Loading Profiles");
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
                        OphiussaLogger.logger.Info("Update cache folder for game " + p.Type.KeyName);
                        NetworkTools.UpdateCacheFolder(p);
                    });
                    tasks.Add(t);
                }

                Task.WaitAll(tasks.ToArray());

                foreach (var t in profiles)
                {
                    if (t.AutoManageSettings.IncludeInAutoUpdate)
                    {
                        OphiussaLogger.logger.Info("Checking updated for server " + t.Name);
                        UpdateServer(t, Settings, System.IO.Path.Combine(Settings.DataFolder, "cache", t.Type.KeyName), false);
                    }
                    else if(t.AutoManageSettings.RestartIfShutdown)
                    {
                        if (!t.IsRunning)
                        {
                            t.StartServer(Settings);
                        }
                    }
                }
                OnProcessCompleted(new ProcessCopyEventArg() { Message = "Process Ended", Sucessful = true });

            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                OnProcessError(new ProcessCopyEventArg() { Message = ex.Message, Sucessful = false });
            }
        }

        private string GetCacheBuild(Profile p, string CacheFolder)
        {

            string fileName = "appmanifest_2430930.acf";
            if (p.Type.ServerType == EnumServerType.ArkSurviveEvolved) fileName = "appmanifest_376030.acf";
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

            OphiussaLogger.logger.Info("Cache Build : " + currentCacheBuild);
            OphiussaLogger.logger.Info("Server Build : " + currentServerBuild);
            //if (currentServerBuild == currentCacheBuild)
            //{

            //    if (p.IsRunning || p.AutoManageSettings.AutoStartServer) Utils.ExecuteAsAdmin(System.IO.Path.Combine(p.InstallLocation, p.Type.ExecutablePath), p.ARKConfiguration.GetCommandLinesArguments(Settings, p, p.ARKConfiguration.Administration.LocalIP), false);
            //    return;
            //}


            OnProgressChanged(new ProcessEventArg() { Message = "Comparing cache with production files", IsStarting = false, ProcessedFileCount = 0, Sucessful = false, TotalFiles = 0 });
            List<FileInfo> changedFiles = new List<FileInfo>();
            if (Settings.UseSmartCopy)
            {
                changedFiles = Utils.CompareFolderContent(p.InstallLocation, CacheFolder, new List<string> { "Saved", "genosl", "Privacy" });
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

            if (changedFiles.Count == 0) OnProgressChanged(new ProcessEventArg() { Message = "No changed detected", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0 });
            else OnProgressChanged(new ProcessEventArg() { Message = $"Detected changes in {changedFiles.Count} files for server {p.Name}", IsStarting = true, ProcessedFileCount = 0, Sucessful = false, TotalFiles = changedFiles.Count, SendToDiscord = true });

            OnProgressChanged(new ProcessEventArg() { Message = $"Mod list check", IsStarting = true, ProcessedFileCount = 0, Sucessful = false, TotalFiles = 0 });

            List<CurseForgeFileDetail> curseForgeFileDetails = new List<CurseForgeFileDetail>();
            List<WorkshopFileDetail> steamFileDetails = new List<WorkshopFileDetail>();

            bool needToUpdate = false;
            switch (p.Type.ModsSource)
            {
                case ModSource.SteamWorkshop:
                    needToUpdate = CheckSteamMods(p, Settings, CacheFolder);
                    if (needToUpdate) OnProgressChanged(new ProcessEventArg() { Message = "Detected mod Updates", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
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
                    OphiussaLogger.logger.Info("Curse Mod changed:" + item.name);
                }
            }
            if (steamFileDetails.Count > 0)
            {
                needToUpdate = true;
                foreach (var item in steamFileDetails)
                {
                    OphiussaLogger.logger.Info("Steam Mod changed:" + item.title);
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
                        OphiussaLogger.logger.Debug("Server Still running");
                        Thread.Sleep(5000);
                    }
                }
                //TODO:Update Steam Mods
                int i = 1;
                foreach (var file in changedFiles)
                {
                    System.IO.File.Copy(file.FullName, file.FullName.Replace(CacheFolder, p.InstallLocation), true);

                    OphiussaLogger.logger.Info("Server file changed:" + file.FullName);
                    OnProgressChanged(new ProcessEventArg() { Message = $"Copying files {i}/{changedFiles.Count}", IsStarting = false, ProcessedFileCount = i, Sucessful = false, TotalFiles = changedFiles.Count });

                    i++;
                }
                OnProgressChanged(new ProcessEventArg() { Message = $"Server {p.Name} updated", IsStarting = false, ProcessedFileCount = changedFiles.Count, Sucessful = true, TotalFiles = changedFiles.Count, SendToDiscord = true });

                if (!DontStartServer) if (IsRunning || p.AutoManageSettings.RestartIfShutdown) p.StartServer(Settings);
            }
            else
            {
                if (!DontStartServer) if (!p.IsRunning & p.AutoManageSettings.RestartIfShutdown) p.StartServer(Settings);
            }

        }

        public async Task CloseServer(Profile p, Settings settings)
        {
            OphiussaLogger.logger.Info("Closing server " + p.Name);
            await p.CloseServer(settings, OnProgressChanged); 
        }

        public bool CheckSteamMods(Profile p, Settings Settings, string CacheFolder)
        {
            return false;
        }

        public List<CurseForgeFileDetail> CheckSCurseForgeMods(Profile p, Settings Settings, string CacheFolder)
        {
            CurseForgeUtils curseForgeUtils = new CurseForgeUtils(Settings);
            CurseForgeFileDetailResponse mods = curseForgeUtils.GetCurseForgeModDetails(p.ARKConfiguration.Administration.ModIDs.FindAll(x => x != ""));

            string cache = System.IO.Path.Combine(Settings.DataFolder, "cache", "CFCache", p.ARKConfiguration.Administration.Branch);
            if (!Directory.Exists(cache))
            {
                Directory.CreateDirectory(cache);

                string jsonString = JsonConvert.SerializeObject(mods, Formatting.Indented);
                File.WriteAllText(System.IO.Path.Combine(cache, $"CurseForgeModCache_{p.Key}.json"), jsonString);
            }

            CurseForgeFileDetailResponse cachedmods = JsonConvert.DeserializeObject<CurseForgeFileDetailResponse>(System.IO.File.ReadAllText(System.IO.Path.Combine(cache, $"CurseForgeModCache_{p.Key}.json")));

            List<CurseForgeFileDetail> changedMods = cachedmods.data.FindAll(m => CheckMod(m, mods) != null);

            CurseForgeFileDetail CheckMod(CurseForgeFileDetail mod, CurseForgeFileDetailResponse modList)
            {
                CurseForgeFileDetail m = modList.data.Find(mm => mm.id == mod.id);
                if (m == null) return null;
                if (m.dateModified != mod.dateModified) return mod;
                else return null;
            }

            string jsonString1 = JsonConvert.SerializeObject(mods, Formatting.Indented);
            File.WriteAllText(System.IO.Path.Combine(cache, $"CurseForgeModCache_{p.Key}.json"), jsonString1);

            return changedMods;
        }
    }
}
