using Newtonsoft.Json;
using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models.SupportedServers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace OphiussaServerManager
{
    public class ProcessEventArg
    {
        public string Message { get; set; }
        public int TotalFiles { get; set; }
        public int ProcessedFileCount { get; set; }
        public bool Sucessful { get; set; }
        public bool IsStarting { get; set; }
    }
    public class ProcessCopyEventArg
    {
        public string Message { get; set; }
        public bool Sucessful { get; set; }
    }

    public class AutoUpdate
    {
        public event EventHandler<ProcessEventArg> ProgressChanged;
        public event EventHandler<ProcessCopyEventArg> ProcessStarted;
        public event EventHandler<ProcessCopyEventArg> ProcessCompleted;
        public event EventHandler<ProcessCopyEventArg> ProcessError;

        protected virtual void OnProgressChanged(ProcessEventArg e)
        {
            ProgressChanged?.Invoke(this, e);
        }
        protected virtual void OnProcessCompleted(ProcessCopyEventArg e)
        {
            ProcessCompleted?.Invoke(this, e);
        }
        protected virtual void OnProcessStarted(ProcessCopyEventArg e)
        {
            ProcessStarted?.Invoke(this, e);
        }
        protected virtual void OnProcessError(ProcessCopyEventArg e)
        {
            ProcessError?.Invoke(this, e);
        }

        public void UpdateServers()
        {
            try
            {
                OnProcessStarted(new ProcessCopyEventArg() { Message = "Process Started" });
                Common.Models.Settings Settings = JsonConvert.DeserializeObject<Common.Models.Settings>(File.ReadAllText("config.json"));
                string dir = Settings.DataFolder + "Profiles\\";
                if (!Directory.Exists(dir))
                {
                    return;
                }

                List<Profile> profiles = new List<Profile>();
                List<CacheServerTypes> servers = new List<CacheServerTypes>();

                string[] files = Directory.GetFiles(dir);
                foreach (string file in files)
                {
                    Profile p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(file));
                    if (p.AutoManageSettings.IncludeInAutoUpdate)
                    {
                        profiles.Add(p);
                    }
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
                        NetworkTools.UpdateCacheFolder(p);
                    });
                    tasks.Add(t);
                }

                Task.WaitAll(tasks.ToArray());

                foreach (var t in profiles)
                {
                    UpdateServer(t, Settings, System.IO.Path.Combine(Settings.DataFolder, "cache", t.Type.KeyName));
                }
                OnProcessCompleted(new ProcessCopyEventArg() { Message = "Process Ended", Sucessful = true });

            }
            catch (Exception ex)
            {
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

        private void UpdateServer(Profile p, Common.Models.Settings Settings, string CacheFolder)
        {
            OnProgressChanged(new ProcessEventArg() { Message = "Getting builds", IsStarting = true, ProcessedFileCount = 0, Sucessful = false, TotalFiles = 0 });
            string currentServerBuild = p.GetBuild();
            string currentCacheBuild = GetCacheBuild(p, CacheFolder);

            if (currentServerBuild == currentCacheBuild) return;

            OnProgressChanged(new ProcessEventArg() { Message = "Comparing cache with production files", IsStarting = false, ProcessedFileCount = 0, Sucessful = false, TotalFiles = 0 });
            List<FileInfo> changedFiles = new List<FileInfo>();
            if (Settings.UseSmartCopy)
            {
                changedFiles = Utils.CompareFolderContent(p.InstallLocation, CacheFolder);
            }
            else
            {
                System.IO.DirectoryInfo dir1 = new System.IO.DirectoryInfo(CacheFolder);
                // Take a snapshot of the file system.  
                changedFiles = dir1.GetFiles("*.*", System.IO.SearchOption.AllDirectories).ToList();
            }
            if (changedFiles.Count == 0) OnProgressChanged(new ProcessEventArg() { Message = "No changed detected", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0 });
            else OnProgressChanged(new ProcessEventArg() { Message = $"Detected changes in {changedFiles.Count} files", IsStarting = true, ProcessedFileCount = 0, Sucessful = false, TotalFiles = changedFiles.Count });

            OnProgressChanged(new ProcessEventArg() { Message = $"Mod list check", IsStarting = true, ProcessedFileCount = 0, Sucessful = false, TotalFiles = 0 });

            List<CurseForgeFileDetail> curseForgeFileDetails = new List<CurseForgeFileDetail>();

            bool haveModChanges = false;
            switch (p.Type.ModsSource)
            {
                case ModSource.SteamWorkshop:
                    haveModChanges = CheckSteamMods(p, Settings, CacheFolder);
                    if (haveModChanges) OnProgressChanged(new ProcessEventArg() { Message = "Detected mod Updates", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0 });
                    else OnProgressChanged(new ProcessEventArg() { Message = "No mods changed", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0 });
                    break;
                case ModSource.CurseForge:
                    curseForgeFileDetails = CheckSCurseForgeMods(p, Settings, CacheFolder);
                    if (curseForgeFileDetails.Count > 0) OnProgressChanged(new ProcessEventArg() { Message = "Detected mod Updates", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0 });
                    else OnProgressChanged(new ProcessEventArg() { Message = "No mods changed", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0 });
                    break;
            } 

            int i = 1;
            //TODO:Check if server is running
            foreach (var file in changedFiles)
            {
                System.IO.File.Copy(file.FullName, file.FullName.Replace(CacheFolder, p.InstallLocation));
                OnProgressChanged(new ProcessEventArg() { Message = $"Copying files {i}/{changedFiles.Count}", IsStarting = false, ProcessedFileCount = i, Sucessful = false, TotalFiles = changedFiles.Count });
                i++;
            }
            OnProgressChanged(new ProcessEventArg() { Message = "Server files updated", IsStarting = false, ProcessedFileCount = changedFiles.Count, Sucessful = true, TotalFiles = changedFiles.Count });

        }

        public bool CheckSteamMods(Profile p, Common.Models.Settings Settings, string CacheFolder)
        {
            return false;
        }

        public List<CurseForgeFileDetail> CheckSCurseForgeMods(Profile p, Common.Models.Settings Settings, string CacheFolder)
        {
            CurseForgeUtils curseForgeUtils = new CurseForgeUtils(Settings);
            CurseForgeFileDetailResponse mods = curseForgeUtils.GetCurseForgeModDetails(p.ARKConfiguration.Administration.ModIDs.FindAll(x => x != ""));

            string cache = System.IO.Path.Combine(Settings.DataFolder, "cache", "CFCache");
            if (!Directory.Exists(cache))
            {
                Directory.CreateDirectory(cache);

                string jsonString = JsonConvert.SerializeObject(mods, Formatting.Indented);
                File.WriteAllText(System.IO.Path.Combine(cache, "CurseForgeModCache.json"), jsonString);
            }

            CurseForgeFileDetailResponse cachedmods = JsonConvert.DeserializeObject<CurseForgeFileDetailResponse>(System.IO.File.ReadAllText(System.IO.Path.Combine(cache, "CurseForgeModCache.json")));

            List<CurseForgeFileDetail> changedMods = cachedmods.data.FindAll(m => CheckMod(m, mods) != null);

            CurseForgeFileDetail CheckMod(CurseForgeFileDetail mod, CurseForgeFileDetailResponse modList)
            {
                CurseForgeFileDetail m = modList.data.Find(mm => mm.id == mod.id);
                if (m == null) return null;
                if (m.dateModified != mod.dateModified) return mod;
                else return null;
            }

            return changedMods;
        }
    }
}
