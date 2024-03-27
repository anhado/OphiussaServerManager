using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using BasePlugin.Forms;
using Newtonsoft.Json;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.Enums;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;
using Message = OphiussaFramework.Models.Message;

namespace BasePlugin {
    public class ValheimPlugin : IPlugin {
        public ValheimPlugin() {
            DefaultCommands = new List<CommandDefinition>() { new CommandDefinition() { Order = 1, Name = "Test", NamePrefix = "?", AddSpaceInPrefix = false } }; 
        }
       // internal static readonly PluginType              Info = new PluginType { GameType = "Game1", Name = "Game 1 Name" };
        public IProfile                Profile         { get; set; } = new Profile();
        public string                  GameType        { get; set; } = "Valheim";
        public string                  GameName        { get; set; } = "Valheim";
        public TabPage                 TabPage         { get; set; }
        public string                  ExecutablePath  { get; set; } = "valheim_server.exe"; //THIS WILL OVERWRITE THE PROFILE, I JUST NEED THAT IN PROFILE TO AVOID Deserialize THE ADDITIONAL SETTINGS
        public string                  PluginVersion   { get; set; } = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
        public string                  PluginName      { get; set; } = Path.GetFileName(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileName); 
        public int                     ServerProcessID =>  Utils.GetProcessRunning(Path.Combine(Profile.InstallationFolder, Profile.ExecutablePath)).Id; 
        public bool                    IsRunning       =>  Utils.GetProcessRunning(Path.Combine(Profile.InstallationFolder, Profile.ExecutablePath)) != null;
        public Process                 GetExeProcess() => Utils.GetProcessRunning(Path.Combine(Profile.InstallationFolder, Profile.ExecutablePath));
        public bool                    IsInstalled     => IsValidFolder(Profile.InstallationFolder);
        public List<FileInfo>          FilesToBackup   => throw new NotImplementedException();
        public List<CommandDefinition> DefaultCommands { get; set; }
        public ModProvider             ModProvider     { get; set; } = ModProvider.None;


        public bool Loaded { get; set; } = true;

    public event EventHandler<OphiussaEventArgs>     BackupServerClick;
        public event EventHandler<OphiussaEventArgs>     StopServerClick;
        public event EventHandler<OphiussaEventArgs>     StartServerClick;
        public event EventHandler<OphiussaEventArgs>     InstallServerClick;
        public event EventHandler<OphiussaEventArgs>     SaveClick;
        public event EventHandler<OphiussaEventArgs>     ReloadClick;
        public event EventHandler<OphiussaEventArgs>     SyncClick;
        public event EventHandler<OphiussaEventArgs>     OpenRCONClick;
        public event EventHandler<OphiussaEventArgs>     ChooseFolderClick;
        public event EventHandler<OphiussaEventArgs>     TabHeaderChangeEvent;

        public PluginType GetInfo() {
            return new PluginType() { GameType = GameType, Name = GameName };
        }

        public IProfile GetProfile() {
            return Profile;
        }
         
        public Form GetConfigurationForm(TabPage tab) {
            TabPage = tab;
            return new FrmConfigurationForm(this, tab);
        }

        public void TabHeaderChange() {
            TabHeaderChangeEvent?.Invoke(this, new OphiussaEventArgs { Profile = Profile,Plugin = this});
        }

        public void InstallServer() {
            InstallServerClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        public void StartServer() {
            StartServerClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        public void StopServer() {
            StopServerClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        public void BackupServer() {
            BackupServerClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        public void Save() {
            SaveClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
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
           ChooseFolderClick.Invoke(this, new OphiussaEventArgs() { Profile = Profile, Plugin = this });
        }


        public Message SetProfile(IProfile profile) {
            try {

                var p = JsonConvert.DeserializeObject<Profile>(profile.AdditionalSettings.ToString());
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
            //TODO:(New Games)Valid folder installation 
            return Utils.IsAValidFolder(Profile.InstallationFolder,new List<string>(){"FolderDummy","FolderDummy2"});
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
            return new Message() { MessageText = "NOT USED", Success = true};
        }

        public string GetVersion() {
            //TODO:(New Games)Save settings to disc
            return "NOT IMPLEMENTED"; //  throw new NotImplementedException();
        }

        public string GetBuild() {
            //TODO:(New Games)Save settings to disc
            return "NOT IMPLEMENTED"; //  throw new NotImplementedException();
        }


        public string GetCommandLinesArguments(Settings settings, Profile profile, string locaIp) {
            string cmd = string.Empty;

            var hifenArgs = new List<string>();

            hifenArgs.Add(" -nographics");
            hifenArgs.Add(" -batchmode");
            hifenArgs.Add($" -name \"{profile.Name}\"");
            hifenArgs.Add($" -port {profile.ServerPort}");
            hifenArgs.Add($" -world \"{profile.WordName}\"");
            hifenArgs.Add($" -password \"{profile.ServerPassword}\"");

            if (!string.IsNullOrEmpty(profile.SaveLocation)) hifenArgs.Add($" -savedir \"{profile.SaveLocation}\"");

            hifenArgs.Add($" -public {(profile.Public ? 1 : 0)}");

            if (!string.IsNullOrEmpty(profile.LogFileLocation)) hifenArgs.Add($" -logFile \"{profile.LogFileLocation}\\VAL_{profile.Key}.log\"");

            hifenArgs.Add($" -saveinterval {profile.AutoSavePeriod * 60}");
            hifenArgs.Add($" -backups {profile.TotalBackups}");
            hifenArgs.Add($" -backupshort {profile.BackupShort * 60}");
            hifenArgs.Add($" -backuplong {profile.BackupLong * 60}");

            if (profile.Crossplay) hifenArgs.Add(" -crossplay");
            if (!string.IsNullOrEmpty(profile.InstanceId)) hifenArgs.Add($" -instanceid \"{profile.InstanceId}\"");

            hifenArgs.Add($" -preset {profile.Preset.ToString().ToLower()}");

            if (profile.Combat != Combat.Default) hifenArgs.Add($" -modifier combat {profile.Combat.ToString().ToLower()}");
            if (profile.DeathPenalty != DeathPenalty.Default) hifenArgs.Add($" -modifier deathpenalty {profile.DeathPenalty.ToString().ToLower()}");
            if (profile.Resources != Resources.Default) hifenArgs.Add($" -modifier resources {profile.Resources.ToString().ToLower()}");
            if (profile.Raids != Raids.Default) hifenArgs.Add($" -modifier raids {profile.Raids.ToString().ToLower()}");
            if (profile.Portals != Portals.Default) hifenArgs.Add($" -modifier portals {profile.Portals.ToString().ToLower()}");

            if (profile.NoBuildcost) hifenArgs.Add(" -setkey nobuildcost");
            if (profile.PlayerEvents) hifenArgs.Add(" -setkey playerevents");
            if (profile.PassiveMobs) hifenArgs.Add(" -setkey passivemobs");
            if (profile.NoMap) hifenArgs.Add(" -setkey nomap");
            if (profile.AllPiecesUnlocked) hifenArgs.Add(" -setkey AllPiecesUnlocked");
            if (profile.AllRecipesUnlocked) hifenArgs.Add(" -setkey AllRecipesUnlocked");
            if (profile.DeathDeleteItems) hifenArgs.Add(" -setkey DeathDeleteItems");
            if (profile.DeathDeleteUnequipped) hifenArgs.Add(" -setkey DeathDeleteUnequipped");
            if (profile.DeathKeepEquip) hifenArgs.Add(" -setkey DeathKeepEquip");
            if (profile.DeathSkillsReset) hifenArgs.Add(" -setkey DeathSkillsReset");
            if (profile.DungeonBuild) hifenArgs.Add(" -setkey DungeonBuild");
            if (profile.NoCraftCost) hifenArgs.Add(" -setkey NoCraftCost");
            if (profile.NoBossPortals) hifenArgs.Add(" -setkey NoBossPortals");
            if (profile.NoPortals) hifenArgs.Add(" -setkey NoPortals");
            if (profile.NoWorkbench) hifenArgs.Add(" -setkey NoWorkbench");
            if (profile.TeleportAll) hifenArgs.Add(" -setkey TeleportAll");

            if (profile.DamageTaken != 100f) hifenArgs.Add($" -setkey DamageTaken {profile.DamageTaken}");
            if (profile.EnemyDamage != 100f) hifenArgs.Add($" -setkey EnemyDamage {profile.EnemyDamage}");
            if (profile.EnemyLevelUpRate != 100f) hifenArgs.Add($" -setkey EnemyLevelUpRate {profile.EnemyLevelUpRate}");
            if (profile.EnemySpeedSize != 100f) hifenArgs.Add($" -setkey EnemySpeedSize {profile.EnemySpeedSize}");
            if (profile.EventRate != 100f) hifenArgs.Add($" -setkey EventRate {profile.EventRate}");
            if (profile.MoveStaminaRate != 100f) hifenArgs.Add($" -setkey MoveStaminaRate {profile.MoveStaminaRate}");
            if (profile.PlayerDamage != 100f) hifenArgs.Add($" -setkey PlayerDamage {profile.PlayerDamage}");
            if (profile.ResourceRate != 100f) hifenArgs.Add($" -setkey ResourceRate {profile.ResourceRate}");
            if (profile.SkillGainRate != 100f) hifenArgs.Add($" -setkey SkillGainRate {profile.SkillGainRate}");
            if (profile.SkillReductionRate != 100f) hifenArgs.Add($" -setkey SkillReductionRate {profile.SkillReductionRate}");
            if (profile.StaminaRate != 100f) hifenArgs.Add($" -setkey StaminaRate  {profile.StaminaRate}");
            if (profile.StaminaRegenRate != 100f) hifenArgs.Add($" -setkey StaminaRegenRate {profile.StaminaRegenRate}");

            cmd += string.Join("", hifenArgs.ToArray());

            return cmd;
        }
    }
}