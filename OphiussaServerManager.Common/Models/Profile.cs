using CoreRCON;
using FirewallManager;
using Newtonsoft.Json;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Ini;
using OphiussaServerManager.Common.Models.SupportedServers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace OphiussaServerManager.Common.Models.Profiles
{
    public class Profile : BaseProfile
    {
        [JsonProperty("Key")]
        public string Key { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Version")]
        public string Version { get; set; }
        [JsonProperty("InstallLocation")]
        public string InstallLocation { get; set; }
        [JsonProperty("Type")]
        public SupportedServersType Type { get; set; }
        [JsonProperty("ARKConfiguration", NullValueHandling = NullValueHandling.Ignore)]
        public ArkProfile.ArkProfile ARKConfiguration { get; set; } = new ArkProfile.ArkProfile();
        [JsonProperty("ValheimConfiguration", NullValueHandling = NullValueHandling.Ignore)]
        public ValheimProfile.ValheimProfile ValheimConfiguration { get; set; } = new ValheimProfile.ValheimProfile();
        public AutoManageSettings AutoManageSettings { get; set; } = new AutoManageSettings();

        [JsonIgnore]
        public bool IsInstalled
        {
            get
            {
                switch (Type.ServerType)
                {
                    case EnumServerType.ArkSurviveEvolved:
                    case EnumServerType.ArkSurviveAscended:
                        if (Utils.IsAValidFolder(InstallLocation, new List<string> { "Engine", "ShooterGame", "steamapps" })) return true;
                        break;
                    case EnumServerType.Valheim:
                        if (Utils.IsAValidFolder(InstallLocation, new List<string> { "MonoBleedingEdge", "valheim_server_Data" })) return true;
                        break;
                }
                return false;
            }
        }

        [JsonIgnore]
        public bool IsRunning
        {
            get
            {
                return Utils.GetProcessRunning(System.IO.Path.Combine(InstallLocation, Type.ExecutablePath)) != null;
            }
        }


        public Profile() { }

        public Profile(string key, string name, SupportedServersType type)
        {
            this.Key = key;
            this.Name = name;
            this.Type = type;
            switch (type.ServerType)
            {
                case EnumServerType.ArkSurviveEvolved:
                    this.ARKConfiguration = new ArkProfile.ArkProfile();
                    this.ValheimConfiguration = null;
                    break;
                case EnumServerType.ArkSurviveAscended:
                    this.ARKConfiguration = new ArkProfile.ArkProfile();
                    break;
                case EnumServerType.Valheim:
                    this.ARKConfiguration = null;
                    this.ValheimConfiguration = new ValheimProfile.ValheimProfile();
                    break;

            }
            LoadProfile();
        }

        public string GetBuild()
        {
            string fileName = this.Type.ManifestFileName;
            if (!File.Exists(System.IO.Path.Combine(this.InstallLocation, "steamapps", fileName))) return "";

            string[] content = File.ReadAllText(System.IO.Path.Combine(this.InstallLocation, "steamapps", fileName)).Split('\n');

            foreach (var item in content)
            {
                string[] t = item.Split('\t');

                if (item.Contains("buildid"))
                {
                    return t[3].Replace("\"", "");
                }

            }
            return System.IO.File.ReadAllText(System.IO.Path.Combine(this.InstallLocation, "steamapps", fileName));
        }

        public string GetVersion()
        {
            if (!File.Exists(System.IO.Path.Combine(this.InstallLocation, "version.txt"))) return "";

            return System.IO.File.ReadAllText(System.IO.Path.Combine(this.InstallLocation, "version.txt"));
        }

        public Process GetExeProcess()
        {
            string ClientFile = System.IO.Path.Combine(this.InstallLocation, Type.ExecutablePath);
            if (string.IsNullOrWhiteSpace(ClientFile) || !System.IO.File.Exists(ClientFile))
                return (Process)null;
            string a = IOUtils.NormalizePath(ClientFile);
            Process[] processesByName = Process.GetProcessesByName(Type.ProcessName);
            Process steamProcess = (Process)null;
            foreach (Process process in processesByName)
            {
                string mainModuleFilepath = ProcessUtils.GetMainModuleFilepath(process.Id);
                if (string.Equals(a, mainModuleFilepath, StringComparison.OrdinalIgnoreCase))
                {
                    steamProcess = process;
                    break;
                }
            }
            return steamProcess;
        }

        public Profile(string key, string name, SupportedServersType type, dynamic configuration)
        {
            this.Key = key;
            this.Name = name;
            this.Type = type;
            this.ARKConfiguration = configuration;
            LoadProfile(false);
        }

        public void SaveProfile(Settings sett)
        {
            string fileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
            Settings settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(fileName));
            string dir = settings.DataFolder + "Profiles\\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (!Directory.Exists(InstallLocation))
            {
                Directory.CreateDirectory(InstallLocation);
            }

            string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(dir + this.Key + ".json", jsonString);

            switch (this.Type.ServerType)
            {
                case EnumServerType.ArkSurviveEvolved:
                case EnumServerType.ArkSurviveAscended:
                    ARKConfiguration.SaveGameINI(this);

                    string priority = ARKConfiguration.Administration.CPUPriority.ToString().ToLower();
                    string affinity = GetCPUAffinity();

                    if (!string.IsNullOrEmpty(affinity)) affinity = "/affinity " + affinity;

                    File.WriteAllText(sett.DataFolder + $"StartServer\\Run_{this.Key.Replace("-", "")}.cmd", $"start \"{this.Name}\" /{priority} {affinity} \"{System.IO.Path.Combine(InstallLocation, (this.ARKConfiguration.Administration.UseServerAPI ? Type.ExecutablePathAPI : Type.ExecutablePath))}\" {this.ARKConfiguration.GetCommandLinesArguments(sett, this, this.ARKConfiguration.Administration.LocalIP)}");
                    break;
                case EnumServerType.Valheim:

                    string priorityV = ValheimConfiguration.Administration.CPUPriority.ToString().ToLower();
                    string affinityV = GetCPUAffinity();

                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine($"@echo off");
                    stringBuilder.AppendLine($"set SteamAppId=892970");
                    stringBuilder.AppendLine($"");
                    stringBuilder.AppendLine($"echo \"Starting server PRESS CTRL-C to exit\"");
                    stringBuilder.AppendLine($"");
                    stringBuilder.AppendLine($"start \"{this.Name}\" /{priorityV} {affinityV} \"{System.IO.Path.Combine(InstallLocation, Type.ExecutablePath)}\" {this.ValheimConfiguration.GetCommandLinesArguments(sett, this, this.ValheimConfiguration.Administration.LocalIP)}");

                    File.WriteAllText(sett.DataFolder + $"StartServer\\Run_{this.Key.Replace("-", "")}.bat", stringBuilder.ToString());
                    break;
            }

        }

        public void StartServer(Settings sett)
        {
            string file = "";
            switch (Type.ServerType)
            {
                case EnumServerType.ArkSurviveEvolved:
                case EnumServerType.ArkSurviveAscended:
                    file = sett.DataFolder + $"StartServer\\Run_{this.Key.Replace("-", "")}.cmd";
                    Utils.ExecuteAsAdmin(file, "", false);
                    break;
                case EnumServerType.Valheim:
                    file = sett.DataFolder + $"StartServer\\Run_{this.Key.Replace("-", "")}.bat";
                    Utils.ExecuteAsAdmin(file, "", false, false, true);
                    break;
                default:
                    break;
            }

        }

        public async Task CloseServer(Settings sett, Func<ProcessEventArg, bool> OnProgressChanged, bool ForceKillProcess = false)
        {

            switch (this.Type.ServerType)
            {
                case EnumServerType.ArkSurviveEvolved:
                case EnumServerType.ArkSurviveAscended:
                    await CloseServerArk(sett, OnProgressChanged, ForceKillProcess);
                    break;
                case EnumServerType.Valheim:
                    if (ForceKillProcess)
                    {
                        if (ForceKillProcess) OnProgressChanged(new ProcessEventArg() { Message = "Process didnt respond to command, the processed will be killed", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                        Process pro = Utils.GetProcessRunning(System.IO.Path.Combine(this.InstallLocation, this.Type.ExecutablePath));
                        pro.Kill();
                    }
                    else
                    {
                        Utils.SendCloseCommand(Utils.GetProcessRunning(System.IO.Path.Combine(InstallLocation, Type.ExecutablePath)));
                    }
                    break;
            }
        }

        public async Task CloseServerArk(Settings settings, Func<ProcessEventArg, bool> OnProgressChanged, bool ForceKillProcess = false)
        {
            if (!this.ARKConfiguration.Administration.UseRCON || ForceKillProcess)
            {
                if (!this.ARKConfiguration.Administration.UseRCON) OnProgressChanged(new ProcessEventArg() { Message = "No RCON configured, server process will be killed", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                if (ForceKillProcess) OnProgressChanged(new ProcessEventArg() { Message = "Process didnt respond to command, the processed will be killed", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                Process pro = Utils.GetProcessRunning(System.IO.Path.Combine(this.InstallLocation, this.Type.ExecutablePath));
                pro.Kill();
            }
            else
            {
                try
                {

                    RCON rcon = new RCON(IPAddress.Parse(this.ARKConfiguration.Administration.LocalIP), ushort.Parse(this.ARKConfiguration.Administration.RCONPort), this.ARKConfiguration.Administration.ServerAdminPassword);
                    await rcon.ConnectAsync();


                    if (settings.PerformOnlinePlayerCheck)
                    {

                        string respnose = await rcon.SendCommandAsync("ListPlayers");
                        if (respnose != "No Players Connected")
                        {
                            //validate server have players 
                            if (settings.SendShutdowMessages) await rcon.SendCommandAsync($"Broadcast {settings.Message1.Replace("{minutes}", "15")}");
                            OnProgressChanged(new ProcessEventArg() { Message = settings.Message1.Replace("{minutes}", "15"), IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                            Thread.Sleep(300000);
                            if (settings.SendShutdowMessages) await rcon.SendCommandAsync($"Broadcast {settings.Message1.Replace("{minutes}", "10")}");
                            OnProgressChanged(new ProcessEventArg() { Message = settings.Message1.Replace("{minutes}", "10"), IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                            Thread.Sleep(300000);
                            if (settings.SendShutdowMessages) await rcon.SendCommandAsync($"Broadcast {settings.Message1.Replace("{minutes}", "5")}");
                            OnProgressChanged(new ProcessEventArg() { Message = settings.Message1.Replace("{minutes}", "5"), IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                            Thread.Sleep(240000);
                            if (settings.SendShutdowMessages) await rcon.SendCommandAsync($"Broadcast {settings.Message1.Replace("{minutes}", "1")}");
                            OnProgressChanged(new ProcessEventArg() { Message = settings.Message1.Replace("{minutes}", "1"), IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                            Thread.Sleep(60000);
                        }
                    }
                    if (settings.SendShutdowMessages) await rcon.SendCommandAsync($"Broadcast {settings.Message2}");
                    OnProgressChanged(new ProcessEventArg() { Message = settings.Message2, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                    await rcon.SendCommandAsync($"DoExit");
                }
                catch (Exception ex)
                {
                    OphiussaLogger.logger.Error(ex);
                    throw ex;
                }
            }
        }

        public void LoadProfile(bool readFileDisk = true)
        {
            string fileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
            Settings settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(fileName));
            string dir = settings.DataFolder + "Profiles\\";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
                string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(dir + this.Key + ".json", jsonString);
            }
            if (File.Exists(dir + this.Key + ".json") && readFileDisk)
            {
                Profile p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(dir + this.Key + ".json"));

                this.ARKConfiguration = p.ARKConfiguration;
                this.ValheimConfiguration = p.ValheimConfiguration;
                this.AutoManageSettings = p.AutoManageSettings;
            }
        }
        public string GetProfileSaveGamesPath(Profile profile) => profile.GetProfileSaveGamesPath(profile?.InstallLocation);

        public string GetProfileSaveGamesPath(string installDirectory) => System.IO.Path.Combine(installDirectory ?? string.Empty, this.Type.SavedRelativePath, this.Type.SaveGamesRelativePath);

        public string GetProfileSavePath(Profile profile) => profile.GetProfileSavePath(profile, profile?.InstallLocation, profile?.ARKConfiguration.Administration.AlternateSaveDirectoryName);

        public string GetProfileSavePath(
          Profile profile,
          string installDirectory,
          string altSaveDirectoryName)
        {
            switch (profile.Type.ServerType)
            {
                case EnumServerType.ArkSurviveAscended:

                    if (!string.IsNullOrWhiteSpace(altSaveDirectoryName))
                    {
                        return System.IO.Path.Combine(installDirectory ?? string.Empty, this.Type.SavedRelativePath, altSaveDirectoryName, profile.ARKConfiguration.Administration.MapName);
                    }
                    return System.IO.Path.Combine(installDirectory ?? string.Empty, this.Type.SavedFilesRelativePath, profile.ARKConfiguration.Administration.MapName);

                case EnumServerType.ArkSurviveEvolved:
                    if (!string.IsNullOrWhiteSpace(altSaveDirectoryName))
                    {
                        return System.IO.Path.Combine(installDirectory ?? string.Empty, this.Type.SavedRelativePath, altSaveDirectoryName);
                    }
                    return System.IO.Path.Combine(installDirectory ?? string.Empty, this.Type.SavedFilesRelativePath);

                case EnumServerType.Valheim:
                    if (!string.IsNullOrEmpty(this.ValheimConfiguration.Administration.SaveLocation))
                        return System.IO.Path.Combine(this.ValheimConfiguration.Administration.SaveLocation, "worlds_local\\");

                    return System.IO.Path.Combine(Utils.GetLocalLowFolderPath(), "\\IronGate\\Valheim\\worlds_local\\");
            }
            if (!string.IsNullOrWhiteSpace(altSaveDirectoryName))
            {
                return System.IO.Path.Combine(installDirectory ?? string.Empty, this.Type.SavedRelativePath, altSaveDirectoryName);
            }
            return System.IO.Path.Combine(installDirectory ?? string.Empty, this.Type.SavedFilesRelativePath);
        }

        public override string GetCommandLinesArguments(Settings settings, string locaIP)
        {
            switch (this.Type.ServerType)
            {
                case EnumServerType.ArkSurviveEvolved:
                case EnumServerType.ArkSurviveAscended:
                    return ARKConfiguration.GetCommandLinesArguments(settings, this, locaIP);
                case EnumServerType.Valheim:
                    return ValheimConfiguration.GetCommandLinesArguments(settings, this, locaIP);
            }
            return "";
        }

        public string GetCPUAffinity()
        {
            switch (this.Type.ServerType)
            {
                case EnumServerType.ArkSurviveEvolved:
                case EnumServerType.ArkSurviveAscended:
                    return base.GetCPUAffinity(this.ARKConfiguration.Administration.CPUAffinity, this.ARKConfiguration.Administration.CPUAffinityList);
                case EnumServerType.Valheim:
                    return base.GetCPUAffinity(this.ValheimConfiguration.Administration.CPUAffinity, this.ValheimConfiguration.Administration.CPUAffinityList);
            }
            return "";
        }

        public override async void BackupServer(Settings settings)
        {
            string SaveGamesFolder = this.GetProfileSavePath(this);

            switch (this.Type.ServerType)
            {
                case EnumServerType.ArkSurviveEvolved:
                case EnumServerType.ArkSurviveAscended:
                    if (this.ARKConfiguration.Administration.UseRCON)
                    {
                        Task t2 = Task.Run(() => this.ARKConfiguration.SaveWorldRCON(settings), cancellationToken.Token);
                        t2.Wait();
                        Task t3 = Task.Run(() => CreateServerBackup(settings, SaveGamesFolder), cancellationToken.Token);
                        t3.Wait();
                        Task t4 = Task.Run(() => CreateProfileBackup(settings), cancellationToken.Token);
                        t4.Wait();
                    }
                    else
                    {
                        Task t3 = Task.Run(() => CreateServerBackup(settings, SaveGamesFolder), cancellationToken.Token);
                        t3.Wait();
                        Task t4 = Task.Run(() => CreateProfileBackup(settings), cancellationToken.Token);
                        t4.Wait();
                    }
                    break;
                case EnumServerType.Valheim:
                    Task t5 = Task.Run(() => CreateServerBackup(settings, SaveGamesFolder), cancellationToken.Token);
                    t5.Wait();
                    Task t6 = Task.Run(() => CreateProfileBackup(settings), cancellationToken.Token);
                    t6.Wait();
                    break;
            }
        }

        private async Task<bool> CreateProfileBackup(Settings settings)
        {
            try
            {
                List<string> files = new List<string>();
                switch (this.Type.ServerType)
                {
                    case EnumServerType.ArkSurviveEvolved:
                    case EnumServerType.ArkSurviveAscended:
                        SystemIniFile systemIniFile = new SystemIniFile(this.InstallLocation);
                        foreach (var fName in systemIniFile.FileNames)
                        {
                            files.Add(System.IO.Path.Combine(this.InstallLocation, fName.Value));
                        }
                        break;
                    case EnumServerType.Valheim:
                        //nothing to ad
                        break;
                }
                string dirProfiles = settings.DataFolder + "Profiles\\";
                files.Add(System.IO.Path.Combine(dirProfiles, this.Key + ".json"));
                files.Add(settings.DataFolder + $"StartServer\\Run_{this.Key.Replace("-", "")}.cmd");

                if (!Directory.Exists(settings.BackupDirectory)) Directory.CreateDirectory(settings.BackupDirectory);
                if (!Directory.Exists(System.IO.Path.Combine(settings.BackupDirectory, "profiles", this.Key))) Directory.CreateDirectory(System.IO.Path.Combine(settings.BackupDirectory, "profiles", this.Key));

                using (ZipArchive zip = ZipFile.Open(System.IO.Path.Combine(settings.BackupDirectory, "profiles", this.Key) + $"\\{this.Type.KeyName}_{this.Key}_{DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture)}.zip", ZipArchiveMode.Create))
                {
                    foreach (var file in files)
                    {
                        FileInfo f = new FileInfo(file);
                        zip.CreateEntryFromFile(file, file.Replace(this.InstallLocation, "").Replace(settings.DataFolder, ""));
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                return false;
            }
        }

        private async Task<bool> CreateServerBackup(Settings settings, string saveGamesFolder)
        {
            try
            {
                List<string> files = new List<string>();
                switch (this.Type.ServerType)
                {
                    case EnumServerType.ArkSurviveEvolved:
                    case EnumServerType.ArkSurviveAscended:
                        files.Add($"{saveGamesFolder}\\{this.ARKConfiguration.Administration.MapName}.ark");
                        if (settings.IncludeSaveGamesFolder)
                        {
                            string savegameFolder = System.IO.Path.Combine(this.InstallLocation, "ShooterGame\\Saved\\SaveGames");
                            System.IO.DirectoryInfo dir1 = new System.IO.DirectoryInfo(savegameFolder);
                            IEnumerable<System.IO.FileInfo> list1 = dir1.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
                            foreach (var item in list1.ToList())
                            {
                                files.Add(item.FullName);
                            }
                        }

                        break;
                    case EnumServerType.Valheim:
                        files.Add($"{saveGamesFolder}{this.ValheimConfiguration.Administration.WordName}.db");
                        files.Add($"{saveGamesFolder}{this.ValheimConfiguration.Administration.WordName}.fwl");
                        break;
                }

                if (!Directory.Exists(settings.BackupDirectory)) Directory.CreateDirectory(settings.BackupDirectory);

                if (!Directory.Exists(System.IO.Path.Combine(settings.BackupDirectory, "servers", this.Key))) Directory.CreateDirectory(System.IO.Path.Combine(settings.BackupDirectory, "servers", this.Key));

                using (ZipArchive zip = ZipFile.Open(System.IO.Path.Combine(settings.BackupDirectory, "servers", this.Key) + $"\\{this.Type.KeyName}_{this.Key}_{DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture)}.zip", ZipArchiveMode.Create))
                {
                    foreach (var file in files)
                    {
                        FileInfo f = new FileInfo(file);
                        zip.CreateEntryFromFile(file, file.Replace(this.InstallLocation, "").Replace(saveGamesFolder, ""));
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                return false;
            }
        }

        [JsonIgnore]
        CancellationTokenSource cancellationToken = new CancellationTokenSource();

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {

        }
    }

}
