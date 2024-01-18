using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CoreRCON;
using Newtonsoft.Json;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Ini;
using OphiussaServerManager.Common.Models.SupportedServers;

namespace OphiussaServerManager.Common.Models.Profiles {
    public class Profile : BaseProfile {
        [JsonIgnore] private readonly CancellationTokenSource _cancellationToken = new CancellationTokenSource();


        public Profile() {
        }

        public Profile(string key, string name, SupportedServersType type) {
            Key  = key;
            Name = name;
            Type = type;
            switch (type.ServerType) {
                case EnumServerType.ArkSurviveEvolved:
                    ArkConfiguration     = new ArkProfile();
                    ValheimConfiguration = null;
                    break;
                case EnumServerType.ArkSurviveAscended:
                    ArkConfiguration     = new ArkProfile();
                    ValheimConfiguration = null;
                    break;
                case EnumServerType.Valheim:
                    ArkConfiguration     = null;
                    ValheimConfiguration = new ValheimProfile.ValheimProfile();
                    break;
            }

            LoadProfile();
        }

        public Profile(string key, string name, SupportedServersType type, dynamic configuration) {
            Key              = key;
            Name             = name;
            Type             = type;
            ArkConfiguration = configuration;
            LoadProfile(false);
        }

        [JsonProperty("Key")]             public string               Key             { get; set; }
        [JsonProperty("Name")]            public string               Name            { get; set; }
        [JsonProperty("Version")]         public string               Version         { get; set; }
        [JsonProperty("InstallLocation")] public string               InstallLocation { get; set; }
        [JsonProperty("Type")]            public SupportedServersType Type            { get; set; }

        [JsonProperty("ARKConfiguration", NullValueHandling = NullValueHandling.Ignore)]
        public ArkProfile ArkConfiguration { get; set; } = new ArkProfile();

        [JsonProperty("ValheimConfiguration", NullValueHandling = NullValueHandling.Ignore)]
        public ValheimProfile.ValheimProfile ValheimConfiguration { get; set; } = new ValheimProfile.ValheimProfile();

        public AutoManageSettings AutoManageSettings { get; set; } = new AutoManageSettings();

        [JsonIgnore]
        public bool IsInstalled {
            get {
                switch (Type.ServerType) {
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

        [JsonIgnore] public bool IsRunning => Utils.GetProcessRunning(Path.Combine(InstallLocation, Type.ExecutablePath)) != null;

        public string GetBuild() {
            string fileName = Type.ManifestFileName;
            if (!File.Exists(Path.Combine(InstallLocation, "steamapps", fileName))) return "";

            string[] content = File.ReadAllText(Path.Combine(InstallLocation, "steamapps", fileName)).Split('\n');

            foreach (string item in content) {
                string[] t = item.Split('\t');

                if (item.Contains("buildid")) return t[3].Replace("\"", "");
            }

            return File.ReadAllText(Path.Combine(InstallLocation, "steamapps", fileName));
        }

        public string GetVersion() {
            if (!File.Exists(Path.Combine(InstallLocation, "version.txt"))) return "";

            return File.ReadAllText(Path.Combine(InstallLocation, "version.txt"));
        }

        public Process GetExeProcess() {
            string clientFile = Path.Combine(InstallLocation, Type.ExecutablePath);
            if (string.IsNullOrWhiteSpace(clientFile) || !File.Exists(clientFile))
                return null;
            string  a               = IOUtils.NormalizePath(clientFile);
            var     processesByName = Process.GetProcessesByName(Type.ProcessName);
            Process steamProcess    = null;
            foreach (var process in processesByName) {
                string mainModuleFilepath = ProcessUtils.GetMainModuleFilepath(process.Id);
                if (string.Equals(a, mainModuleFilepath, StringComparison.OrdinalIgnoreCase)) {
                    steamProcess = process;
                    break;
                }
            }

            return steamProcess;
        }

        public void SaveProfile(Settings sett) {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
            var    settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(fileName));
            string dir      = settings.DataFolder + "Profiles\\";
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            if (!Directory.Exists(InstallLocation)) Directory.CreateDirectory(InstallLocation);

            string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(dir + Key + ".json", jsonString);

            switch (Type.ServerType) {
                case EnumServerType.ArkSurviveEvolved:
                case EnumServerType.ArkSurviveAscended:
                    ArkConfiguration.SaveGameIni(this);

                    string priority = ArkConfiguration.CpuPriority.ToString().ToLower();
                    string affinity = GetCpuAffinity();

                    if (!string.IsNullOrEmpty(affinity)) affinity = "/affinity " + affinity;

                    File.WriteAllText(sett.DataFolder + $"StartServer\\Run_{Key.Replace("-", "")}.cmd",
                                      $"start \"{Name}\" /{priority} {affinity} \"{Path.Combine(InstallLocation, ArkConfiguration.UseServerApi ? Type.ExecutablePathApi : Type.ExecutablePath)}\" {ArkConfiguration.GetCommandLinesArguments(sett, this, ArkConfiguration.LocalIp)}");
                    break;
                case EnumServerType.Valheim:

                    string priorityV = ValheimConfiguration.Administration.CpuPriority.ToString().ToLower();
                    string affinityV = GetCpuAffinity();

                    var stringBuilder = new StringBuilder();
                    stringBuilder.AppendLine("@echo off");
                    stringBuilder.AppendLine("set SteamAppId=892970");
                    stringBuilder.AppendLine("");
                    stringBuilder.AppendLine("echo \"Starting server PRESS CTRL-C to exit\"");
                    stringBuilder.AppendLine("");
                    stringBuilder.AppendLine($"start \"{Name}\" /{priorityV} {affinityV} \"{Path.Combine(InstallLocation, Type.ExecutablePath)}\" {ValheimConfiguration.GetCommandLinesArguments(sett, this, ValheimConfiguration.Administration.LocalIp)}");

                    File.WriteAllText(sett.DataFolder + $"StartServer\\Run_{Key.Replace("-", "")}.bat", stringBuilder.ToString());
                    break;
            }
        }

        public void StartServer(Settings sett) {
            string file = "";
            switch (Type.ServerType) {
                case EnumServerType.ArkSurviveEvolved:
                case EnumServerType.ArkSurviveAscended:
                    file = sett.DataFolder + $"StartServer\\Run_{Key.Replace("-", "")}.cmd";
                    Utils.ExecuteAsAdmin(file, "", false);
                    break;
                case EnumServerType.Valheim:
                    file = sett.DataFolder + $"StartServer\\Run_{Key.Replace("-", "")}.bat";
                    Utils.ExecuteAsAdmin(file, "", false, false, true);
                    break;
            }
        }

        public async Task CloseServer(Settings sett, Func<ProcessEventArg, bool> onProgressChanged, bool forceKillProcess = false) {
            switch (Type.ServerType) {
                case EnumServerType.ArkSurviveEvolved:
                case EnumServerType.ArkSurviveAscended:
                    await CloseServerArk(sett, onProgressChanged, forceKillProcess);
                    break;
                case EnumServerType.Valheim:
                    if (forceKillProcess) {
                        if (forceKillProcess) onProgressChanged(new ProcessEventArg { Message = "Process didnt respond to command, the processed will be killed", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                        var pro = Utils.GetProcessRunning(Path.Combine(InstallLocation, Type.ExecutablePath));
                        pro.Kill();
                    }
                    else {
                        Utils.SendCloseCommandCtrlC(Utils.GetProcessRunning(Path.Combine(InstallLocation, Type.ExecutablePath)));
                    }  
                    break;
            }
        }

        public async Task CloseServerArk(Settings settings, Func<ProcessEventArg, bool> onProgressChanged, bool forceKillProcess = false) {
            if (!ArkConfiguration.UseRcon || forceKillProcess) {
                if (!ArkConfiguration.UseRcon) onProgressChanged(new ProcessEventArg { Message = "No RCON configured, server process will be killed", IsStarting              = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                if (forceKillProcess) onProgressChanged(new ProcessEventArg { Message          = "Process didnt respond to command, the processed will be killed", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                var pro = Utils.GetProcessRunning(Path.Combine(InstallLocation, Type.ExecutablePath));
                pro.Kill();
            }
            else {
                try {
                    var rcon = new RCON(IPAddress.Parse(ArkConfiguration.LocalIp), ushort.Parse(ArkConfiguration.RconPort), ArkConfiguration.ServerAdminPassword);
                    await rcon.ConnectAsync();


                    if (settings.PerformOnlinePlayerCheck) {
                        string respnose = await rcon.SendCommandAsync("ListPlayers");
                        if (respnose != "No Players Connected") {
                            //validate server have players 
                            if (settings.SendShutdowMessages) await rcon.SendCommandAsync($"Broadcast {settings.Message1.Replace("{minutes}", "15")}");
                            onProgressChanged(new ProcessEventArg { Message = settings.Message1.Replace("{minutes}", "15"), IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                            Thread.Sleep(300000);
                            if (settings.SendShutdowMessages) await rcon.SendCommandAsync($"Broadcast {settings.Message1.Replace("{minutes}", "10")}");
                            onProgressChanged(new ProcessEventArg { Message = settings.Message1.Replace("{minutes}", "10"), IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                            Thread.Sleep(300000);
                            if (settings.SendShutdowMessages) await rcon.SendCommandAsync($"Broadcast {settings.Message1.Replace("{minutes}", "5")}");
                            onProgressChanged(new ProcessEventArg { Message = settings.Message1.Replace("{minutes}", "5"), IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                            Thread.Sleep(240000);
                            if (settings.SendShutdowMessages) await rcon.SendCommandAsync($"Broadcast {settings.Message1.Replace("{minutes}", "1")}");
                            onProgressChanged(new ProcessEventArg { Message = settings.Message1.Replace("{minutes}", "1"), IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                            Thread.Sleep(60000);
                        }
                    }

                    if (settings.SendShutdowMessages) await rcon.SendCommandAsync($"Broadcast {settings.Message2}");
                    onProgressChanged(new ProcessEventArg { Message = settings.Message2, IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                    await rcon.SendCommandAsync("DoExit");
                }
                catch (Exception ex) {
                    OphiussaLogger.Logger.Error(ex);
                    throw ex;
                }
            }
        }

        public void LoadProfile(bool readFileDisk = true) {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
            var    settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(fileName));
            string dir      = settings.DataFolder + "Profiles\\";
            if (!Directory.Exists(dir)) {
                Directory.CreateDirectory(dir);
                string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(dir + Key + ".json", jsonString);
            }

            if (File.Exists(dir + Key + ".json") && readFileDisk) {
                var p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(dir + Key + ".json"));

                ArkConfiguration     = p.ArkConfiguration;
                ValheimConfiguration = p.ValheimConfiguration;
                AutoManageSettings   = p.AutoManageSettings;
            }
        }

        public string GetProfileSaveGamesPath(Profile profile) {
            return profile.GetProfileSaveGamesPath(profile?.InstallLocation);
        }

        public string GetProfileSaveGamesPath(string installDirectory) {
            return Path.Combine(installDirectory ?? string.Empty, Type.SavedRelativePath, Type.SaveGamesRelativePath);
        }

        public string GetProfileSavePath(Profile profile) {
            return profile.GetProfileSavePath(profile, profile?.InstallLocation, profile?.ArkConfiguration.AlternateSaveDirectoryName);
        }

        public string GetProfileSavePath(
            Profile profile,
            string  installDirectory,
            string  altSaveDirectoryName) {
            switch (profile.Type.ServerType) {
                case EnumServerType.ArkSurviveAscended:

                    if (!string.IsNullOrWhiteSpace(altSaveDirectoryName)) return Path.Combine(installDirectory ?? string.Empty, Type.SavedRelativePath, altSaveDirectoryName, profile.ArkConfiguration.MapName);

                    return Path.Combine(installDirectory ?? string.Empty, Type.SavedFilesRelativePath, profile.ArkConfiguration.MapName);

                case EnumServerType.ArkSurviveEvolved:
                    if (!string.IsNullOrWhiteSpace(altSaveDirectoryName)) return Path.Combine(installDirectory ?? string.Empty, Type.SavedRelativePath, altSaveDirectoryName);

                    return Path.Combine(installDirectory ?? string.Empty, Type.SavedFilesRelativePath);

                case EnumServerType.Valheim:
                    if (!string.IsNullOrEmpty(ValheimConfiguration.Administration.SaveLocation))
                        return Path.Combine(ValheimConfiguration.Administration.SaveLocation, "worlds_local\\");

                    return Path.Combine(Utils.GetLocalLowFolderPath(), "\\IronGate\\Valheim\\worlds_local\\");
            }

            if (!string.IsNullOrWhiteSpace(altSaveDirectoryName)) return Path.Combine(installDirectory ?? string.Empty, Type.SavedRelativePath, altSaveDirectoryName);

            return Path.Combine(installDirectory ?? string.Empty, Type.SavedFilesRelativePath);
        }

        public override string GetCommandLinesArguments(Settings settings, string locaIp) {
            switch (Type.ServerType) {
                case EnumServerType.ArkSurviveEvolved:
                case EnumServerType.ArkSurviveAscended:
                    return ArkConfiguration.GetCommandLinesArguments(settings, this, locaIp);
                case EnumServerType.Valheim:
                    return ValheimConfiguration.GetCommandLinesArguments(settings, this, locaIp);
            }

            return "";
        }

        public string GetCpuAffinity() {
            switch (Type.ServerType) {
                case EnumServerType.ArkSurviveEvolved:
                case EnumServerType.ArkSurviveAscended:
                    return base.GetCpuAffinity(ArkConfiguration.CpuAffinity, ArkConfiguration.CpuAffinityList);
                case EnumServerType.Valheim:
                    return base.GetCpuAffinity(ValheimConfiguration.Administration.CpuAffinity, ValheimConfiguration.Administration.CpuAffinityList);
            }

            return "";
        }

        public override async void BackupServer(Settings settings) {
            string saveGamesFolder = GetProfileSavePath(this);

            switch (Type.ServerType) {
                case EnumServerType.ArkSurviveEvolved:
                case EnumServerType.ArkSurviveAscended:
                    if (ArkConfiguration.UseRcon) {
                        Task t2 = Task.Run(() => ArkConfiguration.SaveWorldRcon(settings), _cancellationToken.Token);
                        t2.Wait();
                        Task t3 = Task.Run(() => CreateServerBackup(settings, saveGamesFolder), _cancellationToken.Token);
                        t3.Wait();
                        Task t4 = Task.Run(() => CreateProfileBackup(settings), _cancellationToken.Token);
                        t4.Wait();
                    }
                    else {
                        Task t3 = Task.Run(() => CreateServerBackup(settings, saveGamesFolder), _cancellationToken.Token);
                        t3.Wait();
                        Task t4 = Task.Run(() => CreateProfileBackup(settings), _cancellationToken.Token);
                        t4.Wait();
                    }

                    break;
                case EnumServerType.Valheim:
                    Task t5 = Task.Run(() => CreateServerBackup(settings, saveGamesFolder), _cancellationToken.Token);
                    t5.Wait();
                    Task t6 = Task.Run(() => CreateProfileBackup(settings), _cancellationToken.Token);
                    t6.Wait();
                    break;
            }
        }

        private async Task<bool> CreateProfileBackup(Settings settings) {
            try {
                var files = new List<string>();
                switch (Type.ServerType) {
                    case EnumServerType.ArkSurviveEvolved:
                    case EnumServerType.ArkSurviveAscended:
                        var systemIniFile = new SystemIniFile(InstallLocation);
                        foreach (var fName in systemIniFile.FileNames) files.Add(Path.Combine(InstallLocation, fName.Value));

                        break;
                    case EnumServerType.Valheim:
                        //nothing to ad
                        break;
                }

                string dirProfiles = settings.DataFolder + "Profiles\\";
                files.Add(Path.Combine(dirProfiles, Key + ".json"));
                files.Add(settings.DataFolder + $"StartServer\\Run_{Key.Replace("-", "")}.cmd");

                if (!Directory.Exists(settings.BackupDirectory)) Directory.CreateDirectory(settings.BackupDirectory);
                if (!Directory.Exists(Path.Combine(settings.BackupDirectory, "profiles", Key))) Directory.CreateDirectory(Path.Combine(settings.BackupDirectory, "profiles", Key));

                using (var zip = ZipFile.Open(Path.Combine(settings.BackupDirectory, "profiles", Key) + $"\\{Type.KeyName}_{Key}_{DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture)}.zip", ZipArchiveMode.Create)) {
                    foreach (string file in files) {
                        var f = new FileInfo(file);
                        zip.CreateEntryFromFile(file, file.Replace(InstallLocation, "").Replace(settings.DataFolder, ""));
                    }
                }

                //TODO:Delete old backups
                return true;
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                return false;
            }
        }

        private async Task<bool> CreateServerBackup(Settings settings, string saveGamesFolder) {
            try {
                var files = new List<string>();
                switch (Type.ServerType) {
                    case EnumServerType.ArkSurviveEvolved:
                    case EnumServerType.ArkSurviveAscended:
                        files.Add($"{saveGamesFolder}\\{ArkConfiguration.MapName}.ark");
                        if (settings.IncludeSaveGamesFolder) {
                            string                savegameFolder = Path.Combine(InstallLocation, "ShooterGame\\Saved\\SaveGames");
                            var                   dir1           = new DirectoryInfo(savegameFolder);
                            IEnumerable<FileInfo> list1          = dir1.GetFiles("*.*", SearchOption.AllDirectories);
                            foreach (var item in list1.ToList()) files.Add(item.FullName);
                        }

                        break;
                    case EnumServerType.Valheim:
                        files.Add($"{saveGamesFolder}{ValheimConfiguration.Administration.WordName}.db");
                        files.Add($"{saveGamesFolder}{ValheimConfiguration.Administration.WordName}.fwl");
                        break;
                }

                if (!Directory.Exists(settings.BackupDirectory)) Directory.CreateDirectory(settings.BackupDirectory);

                if (!Directory.Exists(Path.Combine(settings.BackupDirectory, "servers", Key))) Directory.CreateDirectory(Path.Combine(settings.BackupDirectory, "servers", Key));

                using (var zip = ZipFile.Open(Path.Combine(settings.BackupDirectory, "servers", Key) + $"\\{Type.KeyName}_{Key}_{DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture)}.zip", ZipArchiveMode.Create)) {
                    foreach (string file in files) {
                        var f = new FileInfo(file);
                        zip.CreateEntryFromFile(file, file.Replace(InstallLocation, "").Replace(saveGamesFolder, ""));
                    }
                }

                //TODO:Delete old backups
                return true;
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                return false;
            }
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e) {
        }

        public void LoadFromDisk() {
            switch (Type.ServerType) {
                case EnumServerType.ArkSurviveEvolved:
                case EnumServerType.ArkSurviveAscended:
                    ArkConfiguration.LoadFromDisk();

                    break;
                case EnumServerType.Valheim: 
                    //do Nothing
                    break;
            }
        }
    }
}