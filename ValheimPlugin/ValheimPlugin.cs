using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using OphiussaFramework;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.Enums;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;
using ValheimPlugin.Forms;
using Message = OphiussaFramework.Models.Message;

namespace ValheimPlugin {
    public class ValheimPlugin : IPlugin {
        public ValheimPlugin() {
            DefaultCommands = new List<CommandDefinition>();
        }

        public string ExecutablePath { get; set; } = "valheim_server.exe"; //THIS WILL OVERWRITE THE PROFILE, I JUST NEED THAT IN PROFILE TO AVOID Deserialize THE ADDITIONAL SETTINGS

        // internal static readonly PluginType              Info = new PluginType { GameType = "Game1", Name = "Game 1 Name" };
        public IProfile     Profile                     { get; set; } = new Profile();
        public string       GameType                    { get; set; } = "Valheim";
        public string       GameName                    { get; set; } = "Valheim";
        public TabPage      TabPage                     { get; set; }
        public string       PluginVersion               { get; set; } = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
        public string       PluginName                  { get; set; } = Path.GetFileName(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileName);
        public List<string> IgnoredFoldersInComparision { get; set; } = new List<string>();
        public int          ServerProcessID             { get; set; }
        public bool         IsRunning                   { get; set; }
        public bool         IsInstalled                 { get; set; }
        public List<FilesToBackup> FilesToBackup {                   
            get {
                List<FilesToBackup> ret          =  new List<FilesToBackup>();
                string              saveFilePath = "";

                Profile prf = (Profile)Profile;

                saveFilePath = Path.Combine(Utils.GetLocalLowFolderPath(), "IronGate\\Valheim\\worlds_local\\");

                if (!string.IsNullOrEmpty(prf.SaveLocation))
                    saveFilePath = Path.Combine(prf.SaveLocation, "worlds_local\\");

                ret.Add(new FilesToBackup() { File = new FileInfo($"{saveFilePath}{prf.WordName}.db"), EntryName = $"worlds_local\\{prf.WordName}.db" });
                ret.Add(new FilesToBackup() { File = new FileInfo($"{saveFilePath}{prf.WordName}.fwl"), EntryName = $"worlds_local\\{prf.WordName}.fwl" }); 

                return ret;
            }
        } 
        public List<CommandDefinition>               DefaultCommands { get; set; }
        public List<CommandDefinition>               CustomCommands  { get; set; }
        public ModProvider                           ModProvider     { get; set; } = ModProvider.None;
        public bool                                  Loaded          { get; set; } = true;
        public string                                CacheFolder     { get; set; }
        public ServerStatus                          ServerStatus    { get; internal set; } 
        public event EventHandler<OphiussaEventArgs> BackupServerClick;
        public event EventHandler<OphiussaEventArgs> StopServerClick;
        public event EventHandler<OphiussaEventArgs> StartServerClick;
        public event EventHandler<OphiussaEventArgs> InstallServerClick;
        public event EventHandler<OphiussaEventArgs> SaveClick;
        public event EventHandler<OphiussaEventArgs> ReloadClick;
        public event EventHandler<OphiussaEventArgs> SyncClick;
        public event EventHandler<OphiussaEventArgs> OpenRCONClick;
        public event EventHandler<OphiussaEventArgs> ChooseFolderClick;
        public event EventHandler<OphiussaEventArgs> TabHeaderChangeEvent;
        public event EventHandler<OphiussaEventArgs> ServerStatusChangedEvent;

        //TODO:think in a way to do this in controller
        public void SetServerStatus(ServerStatus status, int serverProcessId) {
            switch (status) {
                case ServerStatus.NotInstalled:
                    IsInstalled     = false;
                    IsRunning       = false;
                    ServerProcessID = -1;
                    break;
                case ServerStatus.Running:
                    IsInstalled     = true;
                    IsRunning       = true;
                    ServerProcessID = serverProcessId;
                    break;
                case ServerStatus.Starting:
                    IsInstalled     = true;
                    IsRunning       = true;
                    ServerProcessID = serverProcessId;
                    break;
                case ServerStatus.Stopped:
                    IsInstalled     = true;
                    IsRunning       = false;
                    ServerProcessID = -1;
                    break;
                case ServerStatus.Stopping:
                    IsInstalled     = true;
                    IsRunning       = true;
                    ServerProcessID = serverProcessId;
                    break;
            }
             
            ServerStatus =status;
        }

        public Process GetExeProcess() {
            return Utils.GetProcessRunning(Path.Combine(Profile.InstallationFolder, Profile.ExecutablePath));
        }

        public PluginType GetInfo() {
            return new PluginType { GameType = GameType, Name = GameName };
        }

        public IProfile GetProfile() {
            return Profile;
        }

        public Form GetConfigurationForm(TabPage tab) {
            TabPage = tab;
            return new FrmValheim(this, tab);
        }

        public void TabHeaderChange() {
            TabHeaderChangeEvent?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        public async Task InstallServer(bool fromCache, bool showSteamCMD, bool startServerAtEnd) {
            if (!Directory.Exists(Profile.InstallationFolder)) Directory.CreateDirectory(Profile.InstallationFolder);

            if(Directory.GetDirectories(Profile.InstallationFolder).Any())
                if (!IsValidFolder(Profile.InstallationFolder))
                    throw new Exception("Invalid installation folder");

            InstallServerClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this, InstallFromCache = fromCache, ShowSteamCMD = showSteamCMD, StartServerAtEnd=startServerAtEnd });
        }

        public async Task StartServer() {
            StartServerClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        public async Task StopServer(bool force = false) {
            StopServerClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this, ForceStopServer = force });
        }

        public async Task BackupServer() {
            BackupServerClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        public void Save() {
            SaveClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });

            string priorityV = Profile.CpuPriority.ToString().ToLower();
            string affinityV = Utils.GetCpuAffinity(Profile.CpuAffinity, Profile.CpuAffinityList);

            if (!string.IsNullOrEmpty(affinityV)) affinityV = $"/affinity {affinityV}";


            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("@echo off");
            stringBuilder.AppendLine("set SteamAppId=892970");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("echo \"Starting server PRESS CTRL-C to exit\"");
            stringBuilder.AppendLine(""); 
            stringBuilder.AppendLine("@echo on");
            stringBuilder.AppendLine($"start \"{Profile.Name}\" /{priorityV} {affinityV} \"{Path.Combine(Profile.InstallationFolder, Profile.ExecutablePath)}\" {GetCommandLinesArguments()}");

            File.WriteAllText(ConnectionController.Settings.DataFolder + $"StartServer\\Run_{Profile.Key.Replace("-", "")}.bat", stringBuilder.ToString());
        }

        public void Reload() {
            ReloadClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        public void Sync() {
            SyncClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        public void OpenRCON() {
            OpenRCONClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        public void ChooseFolder() {
            ChooseFolderClick.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }


        public Message SetProfile(IProfile profile) {
            try {
                var p = profile.AdditionalSettings == null ? profile : JsonConvert.DeserializeObject<Profile>(profile.AdditionalSettings)==null ? profile: JsonConvert.DeserializeObject<Profile>(profile.AdditionalSettings);
                p.ServerBuildVersion = Utils.GetBuild(p);
                Profile              = p;

                if (profile.AdditionalCommands != null) DefaultCommands = JsonConvert.DeserializeObject<List<CommandDefinition>>(profile.AdditionalCommands) ?? DefaultCommands;
                CacheFolder = Path.Combine(ConnectionController.Settings.DataFolder, $"cache\\{Profile.Branch}\\{Profile.Type}");

                return new Message {
                                       MessageText = "Load Successful",
                                       Success     = true
                                   };
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                return new Message {
                                       Exception   = e,
                                       MessageText = e.Message,
                                       Success     = false
                                   };
            }
        }

        public Message SetProfile(string json) {
            try {
                var p = JsonConvert.DeserializeObject<Profile>(json);
                p.ServerBuildVersion = Utils.GetBuild(p);
                Profile              = p;
                if (Profile.AdditionalCommands != null) DefaultCommands = JsonConvert.DeserializeObject<List<CommandDefinition>>(Profile.AdditionalCommands) ?? DefaultCommands;
                CacheFolder = Path.Combine(ConnectionController.Settings.DataFolder, $"cache\\{Profile.Branch}\\{Profile.Type}");

                return new Message {
                                       MessageText = "Load Successful",
                                       Success     = true
                                   };
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                return new Message {
                                       Exception   = e,
                                       MessageText = e.Message,
                                       Success     = false
                                   };
            }
        }

        public bool IsValidFolder(string path) {
            return Utils.IsAValidFolder(path, new List<string> { "valheim_server_Data", "MonoBleedingEdge" });
        }

        public Message SetInstallFolder(string path) {
            try {
                if (IsValidFolder(path)) Profile.InstallationFolder = path;
                else throw new Exception("Invalid Installation folder");

                return new Message { MessageText = "Path Changed", Success = true };
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                return new Message { Exception = e, MessageText = e.Message, Success = false };
            }
        }

        public Message SaveSettingsToDisk() {
            //NOT USED
            return new Message { MessageText = "NOT USED", Success = true };
        }

        public string GetVersion() {
            return "";
        }

        public string GetBuild() {
            return Utils.GetBuild(Profile);
        }

        public string GetCommandLinesArguments() {
            string cmd = string.Empty;

            var builder = new CommandBuilder();


            var hifenArgs = new List<string>();

            var profile = (Profile)Profile;


            builder.AddCommand(1, true, "-", "nographics", "",  "",                              true);
            builder.AddCommand(2, true, "-", "batchmode",  "",  "",                              true);
            builder.AddCommand(3, true, "-", "name",       " ", $"\"{profile.ServerName}\"",           true);
            builder.AddCommand(4, true, "-", "port",       " ", $"\"{profile.ServerPort}\"",     true);
            builder.AddCommand(5, true, "-", "world",      " ", $"\"{profile.WordName}\"",       true);
            builder.AddCommand(6, true, "-", "password",   " ", $"\"{profile.ServerPassword}\"", true);
            if (!string.IsNullOrEmpty(profile.SaveLocation)) builder.AddCommand(7, true, "-", "savedir", " ", $"\"{profile.SaveLocation}\"", true);
            builder.AddCommand(8, true, "-", "public", " ", (profile.Public ? 1 : 0).ToString(), true);
            if (!string.IsNullOrEmpty(profile.LogFileLocation)) builder.AddCommand(9, true, "-", "logFile", " ", $"\"{profile.LogFileLocation}\\VAL_{profile.Key}.log\"", true);
            builder.AddCommand(10, true, "-", "saveinterval", " ", (profile.AutoSavePeriod * 60).ToString(), true);
            builder.AddCommand(11, true, "-", "backups",      " ", profile.TotalBackups.ToString(),          true);
            builder.AddCommand(12, true, "-", "backupshort",  " ", (profile.BackupShort * 60).ToString(),    true);
            builder.AddCommand(13, true, "-", "backuplong",   " ", (profile.BackupLong  * 60).ToString(),    true);
            if (profile.Crossplay) builder.AddCommand(14,                         true, "-", "crossplay",  "",  "",                          true);
            if (!string.IsNullOrEmpty(profile.InstanceId)) builder.AddCommand(15, true, "-", "instanceid", " ", $"\"{profile.InstanceId}\"", true);
            builder.AddCommand(16, true, "-", "preset", " ", profile.Preset.ToString().ToLower(), true);
            if (profile.Combat       != Combat.Default) builder.AddCommand(17,       true, "-", "modifier", " ", $"combat {profile.Combat.ToString().ToLower()}",             true);
            if (profile.DeathPenalty != DeathPenalty.Default) builder.AddCommand(18, true, "-", "modifier", " ", $"deathpenalty {profile.DeathPenalty.ToString().ToLower()}", true);
            if (profile.Resources    != Resources.Default) builder.AddCommand(19,    true, "-", "modifier", " ", $"resources {profile.Resources.ToString().ToLower()}",       true);
            if (profile.Raids        != Raids.Default) builder.AddCommand(20,        true, "-", "modifier", " ", $"raids {profile.Raids.ToString().ToLower()}",               true);
            if (profile.Portals      != Portals.Default) builder.AddCommand(21,      true, "-", "modifier", " ", $"portals {profile.Portals.ToString().ToLower()}",           true);


            if (profile.NoBuildcost) builder.AddCommand(22,           true, "-", "setkey", " ", "nobuildcost",           true);
            if (profile.PlayerEvents) builder.AddCommand(23,          true, "-", "setkey", " ", "playerevents",          true);
            if (profile.PassiveMobs) builder.AddCommand(24,           true, "-", "setkey", " ", "passivemobs",           true);
            if (profile.NoMap) builder.AddCommand(25,                 true, "-", "setkey", " ", "nomap",                 true);
            if (profile.AllPiecesUnlocked) builder.AddCommand(26,     true, "-", "setkey", " ", "AllPiecesUnlocked",     true);
            if (profile.AllRecipesUnlocked) builder.AddCommand(27,    true, "-", "setkey", " ", "AllRecipesUnlocked",    true);
            if (profile.DeathDeleteItems) builder.AddCommand(28,      true, "-", "setkey", " ", "DeathDeleteItems",      true);
            if (profile.DeathDeleteUnequipped) builder.AddCommand(29, true, "-", "setkey", " ", "DeathDeleteUnequipped", true);
            if (profile.DeathKeepEquip) builder.AddCommand(30,        true, "-", "setkey", " ", "DeathKeepEquip",        true);
            if (profile.DeathSkillsReset) builder.AddCommand(31,      true, "-", "setkey", " ", "DeathSkillsReset",      true);
            if (profile.DungeonBuild) builder.AddCommand(32,          true, "-", "setkey", " ", "DungeonBuild",          true);
            if (profile.NoCraftCost) builder.AddCommand(33,           true, "-", "setkey", " ", "NoCraftCost",           true);
            if (profile.NoBossPortals) builder.AddCommand(34,         true, "-", "setkey", " ", "NoBossPortals",         true);
            if (profile.NoPortals) builder.AddCommand(35,             true, "-", "setkey", " ", "NoPortals",             true);
            if (profile.NoWorkbench) builder.AddCommand(36,           true, "-", "setkey", " ", "NoWorkbench",           true);
            if (profile.TeleportAll) builder.AddCommand(37,           true, "-", "setkey", " ", "TeleportAll",           true);


            if (profile.DamageTaken        != 100f) builder.AddCommand(38, true, "-", "setkey", " ", $"DamageTaken {profile.DamageTaken}",               true);
            if (profile.EnemyDamage        != 100f) builder.AddCommand(39, true, "-", "setkey", " ", $"EnemyDamage {profile.EnemyDamage}",               true);
            if (profile.EnemyLevelUpRate   != 100f) builder.AddCommand(40, true, "-", "setkey", " ", $"EnemyLevelUpRate {profile.EnemyLevelUpRate}",     true);
            if (profile.EnemySpeedSize     != 100f) builder.AddCommand(41, true, "-", "setkey", " ", $"EnemySpeedSize {profile.EnemySpeedSize}",         true);
            if (profile.EventRate          != 100f) builder.AddCommand(42, true, "-", "setkey", " ", $"EventRate {profile.EventRate}",                   true);
            if (profile.MoveStaminaRate    != 100f) builder.AddCommand(43, true, "-", "setkey", " ", $"MoveStaminaRate {profile.MoveStaminaRate}",       true);
            if (profile.PlayerDamage       != 100f) builder.AddCommand(44, true, "-", "setkey", " ", $"PlayerDamage {profile.PlayerDamage}",             true);
            if (profile.ResourceRate       != 100f) builder.AddCommand(45, true, "-", "setkey", " ", $"ResourceRate {profile.ResourceRate}",             true);
            if (profile.SkillGainRate      != 100f) builder.AddCommand(46, true, "-", "setkey", " ", $"SkillGainRate {profile.SkillGainRate}",           true);
            if (profile.SkillReductionRate != 100f) builder.AddCommand(47, true, "-", "setkey", " ", $"SkillReductionRate {profile.SkillReductionRate}", true);
            if (profile.StaminaRate        != 100f) builder.AddCommand(48, true, "-", "setkey", " ", $"StaminaRate {profile.StaminaRate}",               true);
            if (profile.StaminaRegenRate   != 100f) builder.AddCommand(49, true, "-", "setkey", " ", $"StaminaRegenRate {profile.StaminaRegenRate}",     true);

            return builder.ToString();
        }

        public string GetServerName() => ((Profile)Profile).ServerName;
    }
}