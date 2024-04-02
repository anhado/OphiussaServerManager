using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        public IProfile Profile         { get; set; } = new Profile();
        public string   GameType        { get; set; } = "Valheim";
        public string   GameName        { get; set; } = "Valheim";
        public TabPage  TabPage         { get; set; }
        public string   PluginVersion   { get; set; } = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
        public string   PluginName      { get; set; } = Path.GetFileName(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileName);
        public int      ServerProcessID => Utils.GetProcessRunning(Path.Combine(Profile.InstallationFolder, Profile.ExecutablePath)).Id;
        public bool     IsRunning       => Utils.GetProcessRunning(Path.Combine(Profile.InstallationFolder, Profile.ExecutablePath)) != null;

        public Process GetExeProcess() {
            return Utils.GetProcessRunning(Path.Combine(Profile.InstallationFolder, Profile.ExecutablePath));
        }

        public bool                    IsInstalled     => IsValidFolder(Profile.InstallationFolder);
        public List<FileInfo>          FilesToBackup   => throw new NotImplementedException();
        public List<CommandDefinition> DefaultCommands { get; set; }
        public List<CommandDefinition> CostumCommands  { get; set; }
        public ModProvider             ModProvider     { get; set; } = ModProvider.None;


        public bool Loaded { get; set; } = true;

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

        public async Task InstallServer() {
            InstallServerClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
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
                var p = JsonConvert.DeserializeObject<Profile>(profile.AdditionalSettings);
                Profile = p;

                if (profile.AdditionalCommands != null) DefaultCommands = JsonConvert.DeserializeObject<List<CommandDefinition>>(profile.AdditionalCommands) ?? DefaultCommands;

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

                Profile = p;

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
            return Utils.IsAValidFolder(Profile.InstallationFolder, new List<string> { "valheim_server_Data", "MonoBleedingEdge" });
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
            //TODO:(New Games)Save settings to disc
            return "NOT IMPLEMENTED"; //  throw new NotImplementedException();
        }

        public string GetBuild() {
            //TODO:(New Games)Save settings to disc
            return "NOT IMPLEMENTED"; //  throw new NotImplementedException();
        }

        public string GetCommandLinesArguments() {
            string cmd = string.Empty;

            var builder = new CommandBuilder();


            var hifenArgs = new List<string>();

            var profile = (Profile)Profile;


            builder.AddCommand(1, true, "-", "nographics", "",  "",                              true);
            builder.AddCommand(2, true, "-", "batchmode",  "",  "",                              true);
            builder.AddCommand(3, true, "-", "name",       " ", $"\"{profile.Name}\"",           true);
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
    }
}