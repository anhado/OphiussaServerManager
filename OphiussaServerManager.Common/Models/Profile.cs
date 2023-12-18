using CoreRCON;
using Newtonsoft.Json;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models.SupportedServers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common.Models.Profiles
{
    public class Profile
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
        [JsonProperty("Configuration")]
        public ArkProfile ARKConfiguration { get; set; } = new ArkProfile();
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
                }
                return false;
            }
        }

        [JsonIgnore]
        public bool IsRunning
        {
            get
            {
                return Utils.GetProcessRunning(Path.Combine(InstallLocation, Type.ExecutablePath)) != null;
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
                    this.ARKConfiguration = new ArkProfile();
                    this.ARKConfiguration.LoadNewArkProfile(key);
                    break;
                case EnumServerType.ArkSurviveAscended:
                    this.ARKConfiguration = new ArkProfile();
                    this.ARKConfiguration.LoadNewArkProfile(key);
                    break;
            }
            LoadProfile();
        }

        public string GetBuild()
        {
            string fileName = this.Type.ManifestFileName;
            if (!File.Exists(Path.Combine(this.InstallLocation, "steamapps", fileName))) return "";

            string[] content = File.ReadAllText(Path.Combine(this.InstallLocation, "steamapps", fileName)).Split('\n');

            foreach (var item in content)
            {
                string[] t = item.Split('\t');

                if (item.Contains("buildid"))
                {
                    return t[3].Replace("\"", "");
                }

            }
            return System.IO.File.ReadAllText(Path.Combine(this.InstallLocation, "steamapps", fileName));
        }

        public string GetVersion()
        {
            if (!File.Exists(Path.Combine(this.InstallLocation, "version.txt"))) return "";

            return System.IO.File.ReadAllText(Path.Combine(this.InstallLocation, "version.txt"));
        }

        public Process GetExeProcess()
        {
            string ClientFile = Path.Combine(this.InstallLocation, Type.ExecutablePath);
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
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
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
                    string affinity = ARKConfiguration.GetCPUAffinity();

                    if (!string.IsNullOrEmpty(affinity)) affinity = "/affinity " + affinity;

                    File.WriteAllText(sett.DataFolder + $"StartServer\\Run_{this.Key.Replace("-", "")}.cmd", $"start \"{this.Name}\" /{priority} {affinity} \"{Path.Combine(InstallLocation, (this.ARKConfiguration.Administration.UseServerAPI ? Type.ExecutablePathAPI : Type.ExecutablePath))}\" {this.ARKConfiguration.GetCommandLinesArguments(sett, this, this.ARKConfiguration.Administration.LocalIP)}");
                    break;
            }

        }

        public void StartServer(Settings sett)
        {
            string file = sett.DataFolder + $"StartServer\\Run_{this.Key.Replace("-", "")}.cmd";

            Utils.ExecuteAsAdmin(file, "", false);
        }

        public async Task CloseServer(Settings sett, Func<ProcessEventArg, bool> OnProgressChanged)
        {

            switch (this.Type.ServerType)
            {
                case EnumServerType.ArkSurviveEvolved:
                case EnumServerType.ArkSurviveAscended:
                    await CloseServerArk(sett, OnProgressChanged);
                    break;
            }
        }

        public async Task CloseServerArk(Settings settings, Func<ProcessEventArg, bool> OnProgressChanged)
        {
            if (this.ARKConfiguration.Administration.UseRCON)
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
                            Thread.Sleep(900000);
                            if (settings.SendShutdowMessages) await rcon.SendCommandAsync($"Broadcast {settings.Message1.Replace("{minutes}", "10")}");
                            OnProgressChanged(new ProcessEventArg() { Message = settings.Message1.Replace("{minutes}", "10"), IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                            Thread.Sleep(600000);
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
            else
            {
                OnProgressChanged(new ProcessEventArg() { Message = "No RCON configured, server process will be killed", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });
                Process pro = Utils.GetProcessRunning(System.IO.Path.Combine(this.InstallLocation, this.Type.ExecutablePath));
                pro.Kill();
            }
        }

        public void LoadProfile(bool readFileDisk = true)
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
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
            }
        }
        public string GetProfileSaveGamesPath(Profile profile) => profile.GetProfileSaveGamesPath(profile?.InstallLocation);

        public string GetProfileSaveGamesPath(string installDirectory) => Path.Combine(installDirectory ?? string.Empty, this.Type.SavedRelativePath, this.Type.SaveGamesRelativePath);

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
                        return Path.Combine(installDirectory ?? string.Empty, this.Type.SavedRelativePath, altSaveDirectoryName, profile.ARKConfiguration.Administration.MapName);
                    }
                    return Path.Combine(installDirectory ?? string.Empty, this.Type.SavedFilesRelativePath, profile.ARKConfiguration.Administration.MapName);
            }
            if (!string.IsNullOrWhiteSpace(altSaveDirectoryName))
            {
                return Path.Combine(installDirectory ?? string.Empty, this.Type.SavedRelativePath, altSaveDirectoryName);
            }
            return Path.Combine(installDirectory ?? string.Empty, this.Type.SavedFilesRelativePath);
        }
    }

}
