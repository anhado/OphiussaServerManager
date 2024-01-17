using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Web.Security;
using CoreRCON;
using Newtonsoft.Json;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Helpers.Ini;
using OphiussaServerManager.Common.Ini;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models.SupportedServers;

namespace OphiussaServerManager.Common.Models {
    public class ArkProfile : Configs {
        public ArkProfile() {
            var sw = new Stopwatch();

            sw.Start();

            PlayerBaseStatMultipliers                  = new StatsMultiplierFloatArray(nameof(PlayerBaseStatMultipliers),                  GameData.GetBaseStatMultipliers_Player,                 GameData.GetStatMultiplierInclusions_PlayerBase(),        true);
            PerLevelStatsMultiplier_Player             = new StatsMultiplierFloatArray(nameof(PerLevelStatsMultiplier_Player),             GameData.GetPerLevelStatsMultipliers_Player,            GameData.GetStatMultiplierInclusions_PlayerPerLevel(),    true);
            PerLevelStatsMultiplier_DinoWild           = new StatsMultiplierFloatArray(nameof(PerLevelStatsMultiplier_DinoWild),           GameData.GetPerLevelStatsMultipliers_DinoWild,          GameData.GetStatMultiplierInclusions_DinoWildPerLevel(),  true);
            PerLevelStatsMultiplier_DinoTamed          = new StatsMultiplierFloatArray(nameof(PerLevelStatsMultiplier_DinoTamed),          GameData.GetPerLevelStatsMultipliers_DinoTamed,         GameData.GetStatMultiplierInclusions_DinoTamedPerLevel(), true);
            PerLevelStatsMultiplier_DinoTamed_Add      = new StatsMultiplierFloatArray(nameof(PerLevelStatsMultiplier_DinoTamed_Add),      GameData.GetPerLevelStatsMultipliers_DinoTamedAdd,      GameData.GetStatMultiplierInclusions_DinoTamedAdd(),      true);
            PerLevelStatsMultiplier_DinoTamed_Affinity = new StatsMultiplierFloatArray(nameof(PerLevelStatsMultiplier_DinoTamed_Affinity), GameData.GetPerLevelStatsMultipliers_DinoTamedAffinity, GameData.GetStatMultiplierInclusions_DinoTamedAffinity(), true);
            MutagenLevelBoost                          = new StatsMultiplierIntegerArray(nameof(MutagenLevelBoost),      GameData.GetPerLevelMutagenLevelBoost_DinoWild,  GameData.GetMutagenLevelBoostInclusions_DinoWild(),  true);
            MutagenLevelBoost_Bred                     = new StatsMultiplierIntegerArray(nameof(MutagenLevelBoost_Bred), GameData.GetPerLevelMutagenLevelBoost_DinoTamed, GameData.GetMutagenLevelBoostInclusions_DinoTamed(), true);
            DinoSpawnWeightMultipliers                 = new AggregateIniValueList<DinoSpawn>(nameof(DinoSpawnWeightMultipliers), GameData.GetDinoSpawns);
            PreventDinoTameClassNames                  = new StringIniValueList(nameof(PreventDinoTameClassNames),    () => new string[0]);
            PreventBreedingForClassNames               = new StringIniValueList(nameof(PreventBreedingForClassNames), () => new string[0]);
            NPCReplacements                            = new AggregateIniValueList<NPCReplacement>(nameof(NPCReplacements), GameData.GetNPCReplacements);
            TamedDinoClassDamageMultipliers            = new AggregateIniValueList<ClassMultiplier>(nameof(TamedDinoClassDamageMultipliers),     GameData.GetDinoMultipliers);
            TamedDinoClassResistanceMultipliers        = new AggregateIniValueList<ClassMultiplier>(nameof(TamedDinoClassResistanceMultipliers), GameData.GetDinoMultipliers);
            DinoClassDamageMultipliers                 = new AggregateIniValueList<ClassMultiplier>(nameof(DinoClassDamageMultipliers),          GameData.GetDinoMultipliers);
            DinoClassResistanceMultipliers             = new AggregateIniValueList<ClassMultiplier>(nameof(DinoClassResistanceMultipliers),      GameData.GetDinoMultipliers);
            DinoSettings                               = new DinoSettingsList(DinoSpawnWeightMultipliers, PreventDinoTameClassNames, PreventBreedingForClassNames, NPCReplacements, TamedDinoClassDamageMultipliers, TamedDinoClassResistanceMultipliers, DinoClassDamageMultipliers, DinoClassResistanceMultipliers);
            OverrideNamedEngramEntries                 = new EngramEntryList(nameof(OverrideNamedEngramEntries));
            EngramEntryAutoUnlocks                     = new EngramAutoUnlockList(nameof(EngramEntryAutoUnlocks));
            EngramSettings                             = new EngramSettingsList(this.OverrideNamedEngramEntries, this.EngramEntryAutoUnlocks);
            PlayerBaseStatMultipliers.Clear();
            PerLevelStatsMultiplier_Player.Clear();
            PerLevelStatsMultiplier_DinoWild.Clear();
            PerLevelStatsMultiplier_DinoTamed.Clear();
            PerLevelStatsMultiplier_DinoTamed_Add.Clear();
            PerLevelStatsMultiplier_DinoTamed_Affinity.Clear();
            MutagenLevelBoost.Clear();
            MutagenLevelBoost_Bred.Clear();

            sw.Stop();

            Console.WriteLine("Init Profile={0}", sw.Elapsed.TotalSeconds);
        }

        public ArkProfile LoadGameIni(Profile prf) {
            var systemIniFile = new SystemIniFile(prf.InstallLocation);

            systemIniFile.Deserialize(this, new List<Enum>());

            return this;
        }

        internal void SaveGameIni(Profile profile) {
            var systemIniFile = new SystemIniFile(profile.InstallLocation);

            systemIniFile.Serialize(this, null);
        }

        public string GetCommandLinesArguments(Settings settings, Profile prf, string publicIp) {
            string cmd = string.Empty;

            var hifenArgs         = new List<string>();
            var interrogationArgs = new List<string>();
            interrogationArgs.Add("?listen");
            if (this.AllowFlyerSpeedLeveling) hifenArgs.Add(" -AllowFlyerSpeedLeveling");
            if (AlternateSaveDirectoryName.Trim() != "") interrogationArgs.Add($"?AltSaveDirectoryName=\"{AlternateSaveDirectoryName.Trim()}\"");
            if (this.EnableAutoDestroyStructures) hifenArgs.Add(" -AutoDestroyStructures");
            //if (this.Administration.AutoManagedMods) hifenArgs.Add(" -automanagedmods"); //Only ASE
            if (prf.Type.ServerType == EnumServerType.ArkSurviveEvolved)
                if (EnableCrossPlay)
                    hifenArgs.Add(" -crossplay"); //Only ASE
            if (prf.Type.ServerType == EnumServerType.ArkSurviveEvolved)
                if (EnablePublicIpForEpic)
                    hifenArgs.Add($" -PublicIPForEpic={publicIp}"); //Only For ASE
            //if (this.Administration.LogsLanguage) hifenArgs.Add($" -culture=<lang_code>");
            if (DisableCostumTributeFolders) hifenArgs.Add(" -DisableCustomFoldersInTributeInventories");
            if (DisablePvpRailGun) hifenArgs.Add(" -DisableRailgunPVP");
            if (EnablIdleTimeOut) hifenArgs.Add(" -EnableIdlePlayerKick");
            if (prf.Type.ServerType == EnumServerType.ArkSurviveEvolved)
                if (EpicStorePlayersOnly)
                    hifenArgs.Add(" -epiconly"); //Only ASE
            //if (this.Administration.EventColorsChanceOverride != "") InterrogationArgs.Add($"?EventColorsChanceOverride=\"{this.Administration.EventColorsChanceOverride.ToString()}\"");
            //if (this.Administration.ExclusiveJoin) hifenArgs.Add(" -exclusivejoin");
            if (this.EnableAllowCaveFlyers) hifenArgs.Add(" -ForceAllowCaveFlyers");
            if (EnableAutoForceRespawnDinos) hifenArgs.Add(" -ForceRespawnDinos");
            if (prf.Type.ServerType == EnumServerType.ArkSurviveAscended)
                if (!string.IsNullOrEmpty(ActiveMods))
                    hifenArgs.Add($" -mods={ActiveMods}");
            if (this.Imprintlimit != 1) hifenArgs.Add(" -imprintlimit=101");
            if (DisableVac) hifenArgs.Add(" -insecure");
            //if (this.Administration.MapModID) hifenArgs.Add(" -MapModID=<ModID>");//Dont Use This
            //if (this.Administration.MaxNumOfSaveBackups) hifenArgs.Add($" -MaxNumOfSaveBackups={this.Administration.MaxNumOfSaveBackups}");
            interrogationArgs.Add($"?ShowMapPlayerLocation=\"{(this.AllowMapPlayerLocation ? "True" : "False")}\"");
            if (FjordhawkInventoryCooldown != 3600) hifenArgs.Add($" -MinimumTimeBetweenInventoryRetrieval={FjordhawkInventoryCooldown}");
            if (LocalIp.Trim()             != "") interrogationArgs.Add($"?MultiHome={LocalIp.Trim()}");
            if (DisableAntiSpeedHackDetection) hifenArgs.Add(" -noantispeedhack");
            if (!EnableBattleEye) hifenArgs.Add(" -NoBattlEye");
            if (DisablePlayerMovePhysics) hifenArgs.Add(" -nocombineclientmoves");
            if (NoDinos) hifenArgs.Add(" -nodinos");
            if (NoHandDetection) hifenArgs.Add(" -NoHangDetection");
            if (LogAdminCommandsToAdmins) hifenArgs.Add(" -NotifyAdminCommandsInChat");
            if (NoUnderMeshChecking) hifenArgs.Add(" -noundermeshchecking");
            if (NoUnderMeshKilling) hifenArgs.Add(" -noundermeshkilling");
            if (!AllowTribeWarfare) hifenArgs.Add(" -pvedisallowtribewar");
            //if (this.Administration.SecureSendArKPayload) hifenArgs.Add(" -SecureSendArKPayload");
            if (prf.Type.ServerType == EnumServerType.ArkSurviveAscended)
                if (ServerAllowAnsel)
                    hifenArgs.Add(" -ServerAllowAnsel");
            if (EnableServerAdminLogs) hifenArgs.Add(" -servergamelog");
            if (ServerAdminLogsIncludeTribeLogs) hifenArgs.Add(" -servergamelogincludetribelogs");
            if (ServerRconOutputTribeLogs) hifenArgs.Add(" -ServerRCONOutputTribeLogs");
            if (AntiSpeedHackBias != 0) hifenArgs.Add($" -speedhackbias={AntiSpeedHackBias}");
            if (StasisKeepController) hifenArgs.Add(" -StasisKeepControllers");
            if (StructureMemoryOptimizations) hifenArgs.Add(" -structurememopts");
            if (TotalConversionId != "") hifenArgs.Add($" -TotalConversionMod={TotalConversionId}");
            //if (this.Administration.UseDynamicConfig) hifenArgs.Add($" -UseDynamicConfig");
            //if (this.Administration.UseItemDupeCheck) hifenArgs.Add($" -UseItemDupeCheck");
            //if (this.Administration.UseSecureSpawnRules) hifenArgs.Add($" -UseSecureSpawnRules");
            //if (this.Administration.usestore) hifenArgs.Add($" -usestore");/Dont do this
            //if (this.Administration.UseStructureStasisGrid) hifenArgs.Add($" -UseStructureStasisGrid");
            if (EnableVivox) hifenArgs.Add(" -UseVivox");
            //if (this.Administration.webalarm) hifenArgs.Add($" -webalarm");
            if (prf.Type.ServerType == EnumServerType.ArkSurviveAscended)
                if (MaxPlayers != 0)
                    hifenArgs.Add($" -WinLiveMaxPlayers={MaxPlayers}");
            if (prf.Type.ServerType == EnumServerType.ArkSurviveEvolved)
                if (MaxPlayers != 0)
                    hifenArgs.Add($" -MaxPlayers={MaxPlayers}");
            if (prf.Type.ServerType == EnumServerType.ArkSurviveEvolved)
                if (ClusterDirectoryOverride)
                    hifenArgs.Add($" -ClusterDirOverride=\"{settings.DataFolder}\"");
            if (prf.Type.ServerType == EnumServerType.ArkSurviveEvolved)
                if (ClusterId != "")
                    hifenArgs.Add($" -clusterid=\"{ClusterId}\"");
            if (ForceDirectX10) hifenArgs.Add(" -dx10");
            if (ForceLowMemory) hifenArgs.Add(" -lowmemory");
            if (ForceNoManSky) hifenArgs.Add(" -nomansky");
            if (!UseNoMemoryBias) hifenArgs.Add(" -nomemorybias");
            if (UseCache) hifenArgs.Add(" -usecache");
            if (NoTransferFromFiltering) hifenArgs.Add(" -NoTransferFromFiltering");
            //if (this.Administration.PreventHibernation) hifenArgs.Add($" -PreventHibernation");

            interrogationArgs.Add($"?Port={ServerPort}");
            interrogationArgs.Add($"?QueryPort={QueryPort}");

            hifenArgs.Add(" -nosteamclient");
            hifenArgs.Add(" -game");
            hifenArgs.Add(" -server");
            hifenArgs.Add(" -log");

            if (AllowCrateSpawnsOnTopOfStructures) interrogationArgs.Add("?AllowCrateSpawnsOnTopOfStructures=True");
            hifenArgs.Add(" -ForceAllowCaveFlyers"); //Remove later   
            //-exclusivejoin

            // Olympus?listen?MultiHome=192.168.1.250?Port=8799?QueryPort=27036?MaxPlayers=50?AllowCrateSpawnsOnTopOfStructures=True 
            //-ActiveEvent=FearEvolved -ForceAllowCaveFlyers -EnableIdlePlayerKick -clusterid=OphiussaPVECluster01 -ClusterDirOverride="G:\asmdata" 
            //-NoTransferFromFiltering -NoBattlEye -forcerespawndinos -servergamelog -servergamelogincludetribelogs -ServerRCONOutputTribeLogs 
            //-usecache -NoHangDetection -exclusivejoin -nosteamclient -game -server -log
            cmd += MapName + string.Join("", interrogationArgs.ToArray()) + string.Join("", hifenArgs.ToArray());

            return cmd;
        }

        internal async Task<bool> SaveWorldRcon(Settings settings) {
            try {
                var rcon = new RCON(IPAddress.Parse(LocalIp), ushort.Parse(RconPort), ServerAdminPassword);
                await rcon.ConnectAsync();
                await rcon.SendCommandAsync($"Broadcast {settings.WorldSaveMessage}");
                await rcon.SendCommandAsync("saveworld");
                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        public void LoadFromDisk() {
            //TODO:Read Exclusive Join and ETC
        }
    }

    public class Configs {
        /*
TODO:CHECK THIS OPTIONS
    [DataMember]
    public bool EnableCustomDynamicConfigUrl
    {
      get => (bool) this.GetValue(ServerProfile.EnableCustomDynamicConfigUrlProperty);
      set => this.SetValue(ServerProfile.EnableCustomDynamicConfigUrlProperty, (object) value);
    }

    [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "", ConditionedOn = "EnableCustomDynamicConfigUrl", QuotedString = QuotedStringType.True)]
    public string CustomDynamicConfigUrl
    {
      get => (string) this.GetValue(ServerProfile.CustomDynamicConfigUrlProperty);
      set => this.SetValue(ServerProfile.CustomDynamicConfigUrlProperty, (object) value);
    }

    [DataMember]
    public bool EnableCustomLiveTuningUrl
    {
      get => (bool) this.GetValue(ServerProfile.EnableCustomLiveTuningUrlProperty);
      set => this.SetValue(ServerProfile.EnableCustomLiveTuningUrlProperty, (object) value);
    }

    [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "", ConditionedOn = "EnableCustomLiveTuningUrl", QuotedString = QuotedStringType.True)]
    public string CustomLiveTuningUrl
    {
      get => (string) this.GetValue(ServerProfile.CustomLiveTuningUrlProperty);
      set => this.SetValue(ServerProfile.CustomLiveTuningUrlProperty, (object) value);
    }

        public bool EnableExtinctionEvent
    {
      get => (bool) this.GetValue(ServerProfile.EnableExtinctionEventProperty);
      set => this.SetValue(ServerProfile.EnableExtinctionEventProperty, (object) value);
    }

    [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "", ConditionedOn = "EnableExtinctionEvent")]
    public int ExtinctionEventTimeInterval
    {
      get => (int) this.GetValue(ServerProfile.ExtinctionEventTimeIntervalProperty);
      set => this.SetValue(ServerProfile.ExtinctionEventTimeIntervalProperty, (object) value);
    }

    [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Administration, "NextExtinctionEventUTC", ClearWhenOff = "EnableExtinctionEvent")]
    public int ExtinctionEventUTC
    {
      get => (int) this.GetValue(ServerProfile.ExtinctionEventUTCProperty);
      set => this.SetValue(ServerProfile.ExtinctionEventUTCProperty, (object) value);
    }
    [DataMember]
    public int MaxNumOfSaveBackups
    {
      get => (int) this.GetValue(ServerProfile.MaxNumOfSaveBackupsProperty);
      set => this.SetValue(ServerProfile.MaxNumOfSaveBackupsProperty, (object) value);
    }


    [DataMember]
    public bool EnableBadWordListURL
    {
      get => (bool) this.GetValue(ServerProfile.EnableBadWordListURLProperty);
      set => this.SetValue(ServerProfile.EnableBadWordListURLProperty, (object) value);
    }

    [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "", ConditionedOn = "EnableBadWordListURL", QuotedString = QuotedStringType.True)]
    public string BadWordListURL
    {
      get => (string) this.GetValue(ServerProfile.BadWordListURLProperty);
      set => this.SetValue(ServerProfile.BadWordListURLProperty, (object) value);
    }

    [DataMember]
    public bool EnableBadWordWhiteListURL
    {
      get => (bool) this.GetValue(ServerProfile.EnableBadWordWhiteListURLProperty);
      set => this.SetValue(ServerProfile.EnableBadWordWhiteListURLProperty, (object) value);
    }

    [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "", ConditionedOn = "EnableBadWordWhiteListURL", QuotedString = QuotedStringType.True)]
    public string BadWordWhiteListURL
    {
      get => (string) this.GetValue(ServerProfile.BadWordWhiteListURLProperty);
      set => this.SetValue(ServerProfile.BadWordWhiteListURLProperty, (object) value);
    }

    [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "bFilterTribeNames")]
    public bool FilterTribeNames
    {
      get => (bool) this.GetValue(ServerProfile.FilterTribeNamesProperty);
      set => this.SetValue(ServerProfile.FilterTribeNamesProperty, (object) value);
    }

    [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "bFilterCharacterNames")]
    public bool FilterCharacterNames
    {
      get => (bool) this.GetValue(ServerProfile.FilterCharacterNamesProperty);
      set => this.SetValue(ServerProfile.FilterCharacterNamesProperty, (object) value);
    }

    [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "bFilterChat")]
    public bool FilterChat
    {
      get => (bool) this.GetValue(ServerProfile.FilterChatProperty);
      set => this.SetValue(ServerProfile.FilterChatProperty, (object) value);
    }
         */

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_SessionSettings, ServerProfileCategory.Administration, "SessionName")]
        public string ServerName { get; set; } = "New Server";

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "ServerPassword")]
        public string ServerPassword { get; set; } = Membership.GeneratePassword(10, 6);

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_SessionSettings, ServerProfileCategory.Administration, "MultiHome")]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_MultiHome,       ServerProfileCategory.Administration, "MultiHome", WriteBoolValueIfNonEmpty = true)]
        public string LocalIp { get; set; } = NetworkTools.GetHostIp();

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_SessionSettings, ServerProfileCategory.Administration, "Port")]
        public string ServerPort { get; set; } = "7777";

        public string PeerPort { get; set; } = "7778";

        //public List<string> ModIDs { get; set; } = new List<string>();

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "ActiveMods")] //TODO:CHECK THIS BECAUSE IS SUPPOSED TO BE A STRING SPLITTED BY ,
        public string ActiveMods { get; set; } = "";


        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_GameSession, ServerProfileCategory.Administration, "MaxPlayers")]
        public int MaxPlayers { get; set; } = 70;

        public                       ProcessPriority         CpuPriority     { get; set; } = ProcessPriority.Normal;
        public                       string                  CpuAffinity     { get; set; } = "All";
        public                       List<ProcessorAffinity> CpuAffinityList { get; set; } = new List<ProcessorAffinity>();
        [DefaultValue(false)] public bool                    UseServerApi    { get; set; } = false;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "ServerAdminPassword")]
        public string ServerAdminPassword { get; set; } = Membership.GeneratePassword(10, 6);

        [DefaultValue("")]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "SpectatorPassword")]
        public string ServerSpectatorPassword { get; set; } = "";

        [DefaultValue("27015")]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_SessionSettings, ServerProfileCategory.Administration, "QueryPort")]
        public string QueryPort { get; set; } = "27015";

        [DefaultValue(false)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "RCONEnabled")]
        public bool UseRcon { get; set; } = false;

        [DefaultValue("32330")]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "RCONPort")]
        public string RconPort { get; set; } = "32330";

        [DefaultValue(600)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "RCONServerGameLogBuffer")]
        public int RconServerLogBuffer { get; set; } = 600;

        [DefaultValue("")] public string MapName { get; set; } = "";

        [DefaultValue("")] public string TotalConversionId { get; set; } = "";

        [DefaultValue("15")]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "AutoSavePeriodMinutes")]
        public int AutoSavePeriod { get; set; } = 15;

        [DefaultValue("")]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_MessageOfTheDay, ServerProfileCategory.Administration, "Message", ClearSection = true, Multiline = true, QuotedString = QuotedStringType.Remove)]
        public string Motd { get; set; } = "";

        [DefaultValue(20)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_MessageOfTheDay, ServerProfileCategory.Administration, "Duration")]
        public int ModDuration { get; set; } = 20;

        [DefaultValue(true)] public bool EnableInterval { get; set; } = true;

        [DefaultValue(60)] public int ModInterval { get; set; } = 60;

        [DefaultValue("Live")] public string Branch { get; set; } = "Live";

        [DefaultValue(true)] public bool EnablIdleTimeOut { get; set; } = true;

        [DefaultValue(3600)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "KickIdlePlayersPeriod")]
        public int IdleTimout { get; set; } = 3600;

        [DefaultValue(false)] public bool UseBanListUrl { get; set; } = false;

        [DefaultValue("http://arkdedicated.com/banlist.txt")]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, ConditionedOn = "UseBanListUrl", QuotedString = QuotedStringType.True)]
        public string BanListUrl { get; set; } = "http://arkdedicated.com/banlist.txt";

        [DefaultValue(false)] public bool DisableVac { get; set; } = false;

        [DefaultValue(false)] public bool EnableBattleEye { get; set; } = false;

        [DefaultValue(false)] public bool DisablePlayerMovePhysics { get; set; } = false;

        [DefaultValue(true)] public bool OutputLogToConsole { get; set; } = true;

        [DefaultValue(false)] public bool UseAllCores { get; set; } = false;

        [DefaultValue(false)] public bool UseCache { get; set; } = false;

        [DefaultValue(false)] public bool NoHandDetection { get; set; } = false;

        [DefaultValue(false)] public bool NoDinos { get; set; } = false;

        [DefaultValue(false)] public bool NoUnderMeshChecking { get; set; } = false;

        [DefaultValue(false)] public bool NoUnderMeshKilling { get; set; } = false;

        [DefaultValue(false)] public bool EnableVivox { get; set; } = false;

        [DefaultValue(true)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "AllowSharedConnections")]
        public bool AllowSharedConnections { get; set; } = true;

        [DefaultValue(false)] public bool RespawnDinosOnStartUp { get; set; } = false;


        [DefaultValue(false)] public bool EnableAutoForceRespawnDinos { get; set; } = false;


        [DefaultValue(24)] public int AutoForceRespawnDinosInterval { get; set; } = 24;


        [DefaultValue(false)] public bool DisableAntiSpeedHackDetection { get; set; } = false;


        [DefaultValue(1)] public int AntiSpeedHackBias { get; set; } = 1;


        [DefaultValue(false)] public bool ForceDirectX10 { get; set; } = false;


        [DefaultValue(false)] public bool ForceLowMemory { get; set; } = false;


        [DefaultValue(false)] public bool ForceNoManSky { get; set; } = false;


        [DefaultValue(false)] public bool UseNoMemoryBias { get; set; } = false;


        [DefaultValue(false)] public bool StasisKeepController { get; set; } = false;


        [DefaultValue(false)] public bool ServerAllowAnsel { get; set; } = false;


        [DefaultValue(false)] public bool StructureMemoryOptimizations { get; set; } = false;


        [DefaultValue(false)] public bool EnableCrossPlay { get; set; } = false;


        [DefaultValue(false)] public bool EnablePublicIpForEpic { get; set; } = false;


        [DefaultValue(false)] public bool EpicStorePlayersOnly { get; set; } = false;


        [DefaultValue("")] public string AlternateSaveDirectoryName { get; set; } = "";


        [DefaultValue("")] public string ClusterId { get; set; } = "";


        [DefaultValue(false)] public bool ClusterDirectoryOverride { get; set; } = false;


        [DefaultValue(true)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "servergamelog")]
        public bool EnableServerAdminLogs { get; set; } = true;


        [DefaultValue(true)] public bool ServerAdminLogsIncludeTribeLogs { get; set; } = true;


        [DefaultValue(true)] public bool ServerRconOutputTribeLogs { get; set; } = true;


        [DefaultValue(true)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "AllowHideDamageSourceFromLogs")]
        public bool AllowHideDamageSourceFromLogs { get; set; } = true;


        [DefaultValue(100)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Administration, "MaxTribeLogs")]
        public int MaximumTribeLogs { get; set; } = 100;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "AdminLogging")]
        public bool LogAdminCommandsToPublic { get; set; } = false;


        [DefaultValue(false)] public bool LogAdminCommandsToAdmins { get; set; } = false;


        [DefaultValue(true)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "TribeLogDestroyedEnemyStructures")]
        public bool TribeLogDestroyedEnemyStructures { get; set; } = true;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "ServerHardcore")]
        public bool EnableHardcoreMode { get; set; }


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "bPvEDisableFriendlyFire")]
        public bool DisablePveFriendlyFire { get; set; }


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "bDisableFriendlyFire")]
        public bool DisablePvpFriendlyFire { get; set; }


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "EnableExtraStructurePreventionVolumes")]
        public bool PreventBuildingInResourceRichAreas { get; set; }


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "bDisableLootCrates")]
        public bool DisableSupplyCrates { get; set; }


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "ServerPVE", InvertBoolean = true)]
        public bool EnablePvp { get; set; }


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "AllowCaveBuildingPvE")]
        public bool EnablePveCaveBuilding { get; set; }


        [DefaultValue(true)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "AllowCaveBuildingPvP")]
        public bool EnablePvpCaveBuilding { get; set; } = true;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "bUseSingleplayerSettings", ConditionedOn = "EnableSinglePlayerSettings")]
        public bool EnableSinglePlayerSettings { get; set; }


        [DefaultValue(true)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "AllowCrateSpawnsOnTopOfStructures", ConditionedOn = "AllowCrateSpawnsOnTopOfStructures")]
        public bool AllowCrateSpawnsOnTopOfStructures { get; set; } = true;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "bShowCreativeMode", WriteIfNotValue = false)]
        public bool EnableCreativeMode { get; set; } = false;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "EnableCryoSicknessPVE")]
        public bool EnablePveCryoSickness { get; set; } = false;


        [DefaultValue(false)] public bool DisablePvpRailGun { get; set; } = false;


        [DefaultValue(false)] public bool DisableCostumTributeFolders { get; set; } = false;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "RandomSupplyCratePoints")]
        public bool RandomSupplyCratePoints { get; set; } = false;


        [DefaultValue(1f)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "SupplyCrateLootQualityMultiplier", WriteIfNotValue = 1f)]
        public float SupplyCrateLootQualityMultiplier { get; set; } = 1;


        [DefaultValue(1f)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "FishingLootQualityMultiplier", WriteIfNotValue = 1f)]
        public float FishingLootQualityMultiplier { get; set; } = 1;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "bUseCorpseLocator")]
        public bool UseCorpseLocation { get; set; } = false;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "PreventSpawnAnimations")]
        public bool PreventSpawnAnimations { get; set; } = false;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "bAllowUnlimitedRespecs")]
        public bool AllowUnlimitedRespecs { get; set; } = false;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "bAllowPlatformSaddleMultiFloors")]
        public bool AllowPlatformSaddleMultiFloors { get; set; } = false;


        [DefaultValue(1f)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "PlatformSaddleBuildAreaBoundsMultiplier")]
        public float PlatformSaddleBuildAreaBoundsMultiplier { get; set; } = 1;


        [DefaultValue(2)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "MaxGateFrameOnSaddles")]
        public int MaxGatewaysOnSaddles { get; set; } = 2;


        [DefaultValue(false)] public bool EnableDifficultOverride { get; set; } = false;


        [DefaultValue(4)] //This is 4 because is the default value in settings instead of level
        public int MaxDinoLevel { get; set; } = 120;


        [DefaultValue(4f)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "OverrideOfficialDifficulty", ConditionedOn = "EnableDifficultOverride")]
        public float OverrideOfficialDifficulty { get; set; } = 4f;


        [DefaultValue(1f)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, ConditionedOn = "EnableDifficultOverride")]
        public float DifficultyOffset { get; set; } = 1;


        [DefaultValue(0)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "DestroyTamesOverLevelClamp", WriteIfNotValue = 0)]
        public int DestroyTamesOverLevel { get; set; } = 0;


        [DefaultValue(true)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "NoTributeDownloads", InvertBoolean = true)]
        public bool EnableTributeDownloads { get; set; } = false;


        [DefaultValue(true)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "PreventDownloadSurvivors", ConditionedOn = "EnableTributeDownloads")]
        public bool NoSurvivorDownloads { get; set; } = true;


        [DefaultValue(true)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "PreventDownloadItems", ConditionedOn = "EnableTributeDownloads")]
        public bool NoItemDownloads { get; set; } = true;


        [DefaultValue(true)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "PreventDownloadDinos", ConditionedOn = "EnableTributeDownloads")]
        public bool NoDinoDownloads { get; set; } = true;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "CrossARKAllowForeignDinoDownloads")]
        public bool AllowForeignDinoDownloads { get; set; } = false;


        [DefaultValue(true)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "PreventUploadSurvivors")]
        public bool NoSurvivorUploads { get; set; } = true;


        [DefaultValue(true)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "PreventUploadItems")]
        public bool NoItemUploads { get; set; } = true;


        [DefaultValue(true)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "PreventUploadDinos")]
        public bool NoDinoUploads { get; set; } = true;


        [DefaultValue(false)] public bool LimitMaxTributeDinos { get; set; } = false;


        [DefaultValue(0)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "MaxTributeDinos", ConditionedOn = "LimitMaxTributeDinos")]
        public int MaxTributeDinos { get; set; } = 0;


        [DefaultValue(false)] public bool LimitTributeItems { get; set; } = false;


        [DefaultValue(0)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "MaxTributeItems", ConditionedOn = "LimitTributeItems")]
        public int MaxTributeItems { get; set; } = 0;


        [DefaultValue(false)] public bool NoTransferFromFiltering { get; set; } = false;


        [DefaultValue(false)] public bool OverrideSurvivorUploadExpiration { get; set; } = false;


        [DefaultValue(86400)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "TributeCharacterExpirationSeconds", ConditionedOn = "OverrideSurvivorUploadExpiration")]
        public int OverrideSurvivorUploadExpirationValue { get; set; } = 86400;


        [DefaultValue(false)] public bool OverrideItemUploadExpiration { get; set; } = false;


        [DefaultValue(86400)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "TributeItemExpirationSeconds", ConditionedOn = "OverrideItemUploadExpiration")]
        public int OverrideItemUploadExpirationValue { get; set; } = 86400;


        [DefaultValue(false)] public bool OverrideDinoUploadExpiration { get; set; } = false;


        [DefaultValue(86400)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "TributeDinoExpirationSeconds", ConditionedOn = "OverrideDinoUploadExpiration")]
        public int OverrideDinoUploadExpirationValue { get; set; } = 86400;


        [DefaultValue(false)] public bool OverrideMinimumDinoReUploadInterval { get; set; } = false;


        [DefaultValue(43200)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "MinimumDinoReuploadInterval", ConditionedOn = "OverrideMinimumDinoReUploadInterval")]
        public int OverrideMinimumDinoReUploadIntervalValue { get; set; } = 43200;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "bAutoPvETimer")]
        public bool PveSchedule { get; set; } = false;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "bAutoPvEUseSystemTime", ConditionedOn = "PveSchedule")]
        public bool UseServerTime { get; set; } = false;


        [DefaultValue(0)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "AutoPvEStartTimeSeconds", ConditionedOn = "PveSchedule")]
        public int PvpStartTime { get; set; } = 0;


        [DefaultValue(0)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "AutoPvEStopTimeSeconds", ConditionedOn = "PveSchedule")]
        public int PvpEndTime { get; set; } = 0;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "PreventOfflinePvp")]
        public bool PreventOfflinePvp { get; set; } = false;


        [DefaultValue(900)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "PreventOfflinePvPInterval", ConditionedOn = "PreventOfflinePvp")]
        public int LogoutInterval { get; set; } = 900;


        [DefaultValue(5)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "PreventOfflinePvPConnectionInvincibleInterval", ConditionedOn = "PreventOfflinePvp")]
        public int ConnectionInvicibleInterval { get; set; } = 5;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "bIncreasePvPRespawnInterval")]
        public bool IncreasePvpRespawnInterval { get; set; } = false;


        [DefaultValue(300)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "IncreasePvPRespawnIntervalCheckPeriod", ConditionedOn = "IncreasePvpRespawnInterval")]
        public int IntervalCheckPeriod { get; set; } = 300;


        [DefaultValue(1f)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "IncreasePvPRespawnIntervalMultiplier", ConditionedOn = "IncreasePvpRespawnInterval")]
        public float IntervalMultiplier { get; set; } = 1;


        [DefaultValue(60)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "IncreasePvPRespawnIntervalBaseAmount", ConditionedOn = "IncreasePvpRespawnInterval")]
        public int IntervalBaseAmount { get; set; } = 60;


        [DefaultValue(70)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "MaxNumberOfPlayersInTribe")]
        public int MaxPlayersInTribe { get; set; } = 70;


        [DefaultValue(15)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "TribeNameChangeCooldown")]
        public int TribeNameChangeCooldDown { get; set; } = 15;


        [DefaultValue(0.0f)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "TribeSlotReuseCooldown", WriteIfNotValue = 0.0f)]
        public float TribeSlotReuseCooldown { get; set; } = 0.0f;


        [DefaultValue(true)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "PreventTribeAlliances", InvertBoolean = true)]
        public bool AllowTribeAlliances { get; set; } = true;


        [DefaultValue(10)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "MaxAlliancesPerTribe", ConditionedOn = "AllowTribeAlliances")]
        public int MaxAlliancesPerTribe { get; set; } = 10;


        [DefaultValue(10)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "MaxTribesPerAlliance", ConditionedOn = "AllowTribeAlliances")]
        public int MaxTribesPerAlliance { get; set; } = 10;


        [DefaultValue(true)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "bPvEAllowTribeWar")]
        public bool AllowTribeWarfare { get; set; } = true;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "bPvEAllowTribeWarCancel")]
        public bool AllowCancelingTribeWarfare { get; set; } = false;


        [DefaultValue(true)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "bAllowCustomRecipes")]
        public bool AllowCostumRecipes { get; set; } = true;


        [DefaultValue(1f)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "CustomRecipeEffectivenessMultiplier", WriteIfNotValue = 1f)]
        public float CostumRecipesEffectivenessMultiplier { get; set; } = 1f;


        [DefaultValue(1f)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "CustomRecipeSkillMultiplier", WriteIfNotValue = 1f)]
        public float CostumRecipesSkillMultiplier { get; set; } = 1f;


        [DefaultValue(true)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "PreventDiseases", InvertBoolean = true)]
        public bool EnableDiseases { get; set; } = true;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "NonPermanentDiseases", ConditionedOn = "EnableDiseases")]
        public bool NonPermanentDiseases { get; set; } = false;


        [DefaultValue(false)] public bool OverrideNpcNetworkStasisRangeScale { get; set; } = false;


        [DefaultValue(70)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "NPCNetworkStasisRangeScalePlayerCountStart", ConditionedOn = "OverrideNpcNetworkStasisRangeScale")]
        public int OnlinePlayerCountStart { get; set; } = 70;


        [DefaultValue(120)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "NPCNetworkStasisRangeScalePlayerCountEnd", ConditionedOn = "OverrideNpcNetworkStasisRangeScale")]
        public int OnlinePlayerCountEnd { get; set; } = 120;


        [DefaultValue(0.5f)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "NPCNetworkStasisRangeScalePercentEnd", ConditionedOn = "OverrideNpcNetworkStasisRangeScale")]
        public float ScaleMaximum { get; set; } = 0.5f;


        [DefaultValue(1f)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "OxygenSwimSpeedStatMultiplier", WriteIfNotValue = 1f)]
        public float OxygenSwimSpeedStatMultiplier { get; set; } = 1;


        [DefaultValue(1f)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "UseCorpseLifeSpanMultiplier", WriteIfNotValue = 1f)]
        public float UseCorpseLifeSpanMultiplier { get; set; } = 1;


        [DefaultValue(3600)] public int FjordhawkInventoryCooldown { get; set; } = 3600;


        [DefaultValue(4f)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "GlobalPoweredBatteryDurabilityDecreasePerSecond", WriteIfNotValue = 4f)]
        public float GlobalPoweredBatteryDurability { get; set; } = 4f;


        [DefaultValue(1f)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "FuelConsumptionIntervalMultiplier", WriteIfNotValue = 1f)]
        public float FuelConsumptionIntervalMultiplier { get; set; } = 1;


        [DefaultValue(0)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "LimitNonPlayerDroppedItemsRange", WriteIfNotValue = 0)]
        public int LimitNonPlayerDroppedItemsRange { get; set; } = 0;


        [DefaultValue(0)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "LimitNonPlayerDroppedItemsCount", WriteIfNotValue = 0)]
        public int LimitNonPlayerDroppedItemsCount { get; set; } = 0;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "EnableCryopodNerf", ConditionedOn = "EnableCryopodNerf")]
        public bool EnableCryopodNerf { get; set; } = false;


        [DefaultValue(10)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "CryopodNerfDuration", ConditionedOn = "EnableCryopodNerf")]
        public int EnableCryopodNerfDuration { get; set; } = 10;


        [DefaultValue(1f)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "CryopodNerfDamageMult", ConditionedOn = "EnableCryopodNerf")]
        public float OutgoingDamageMultiplier { get; set; } = 1;


        [DefaultValue(0f)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "CryopodNerfIncomingDamageMultPercent", ConditionedOn = "EnableCryopodNerf")]
        public float IncomingDamageMultiplierPercent { get; set; } = 0;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "bDisableGenesisMissions")]
        public bool Gen1DisableMissions { get; set; } = false;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "AllowTekSuitPowersInGenesis")]
        public bool Gen1AllowTekSuitPowers { get; set; } = false;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "bDisableDefaultMapItemSets")]
        public bool Gen2DisableTekSuitonSpawn { get; set; } = false;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "bDisableWorldBuffs")]
        public bool Gen2DisableWorldBuffs { get; set; } = false;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "bEnableWorldBuffScaling")]
        public bool EnableWorldBuffScaling { get; set; } = false;


        [DefaultValue(1f)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "WorldBuffScalingEfficacy", ConditionedOn = "EnableWorldBuffScaling")]
        public float WorldBuffScanlingEfficacy { get; set; } = 1;


        [DefaultValue(1f)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "AdjustableMutagenSpawnDelayMultiplier")]
        public float MutagemSpawnDelayMultiplier { get; set; } = 1;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "bDisableHexagonStore", ConditionedOn = "DisableHexagonStore")]
        public bool DisableHexagonStore { get; set; } = false;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "bHexStoreAllowOnlyEngramTradeOption", ConditionedOn = "AllowOnlyEngramPointsTrade")]
        public bool AllowOnlyEngramPointsTrade { get; set; } = false;


        [DefaultValue(2000000000)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "MaxHexagonsPerCharacter")]
        public int MaxHexagonsPerCharacter { get; set; } = 2000000000;


        [DefaultValue(1f)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "BaseHexagonRewardMultiplier")]
        public float HexagonRewardMultiplier { get; set; } = 1;


        [DefaultValue(1f)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "HexagonCostMultiplier")]
        public float HexagonCostMultiplier { get; set; } = 1;


        [DefaultValue(false)] public bool EnableRagnarokSettings { get; set; } = false;


        [DefaultValue(false)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_Ragnarok, ServerProfileCategory.Rules, "AllowMultipleTamedUnicorns", ConditionedOn = "EnableRagnarokSettings", ClearSectionIfEmpty = true)]
        public bool AllowMultipleTamedUnicorns { get; set; } = false;


        [DefaultValue(24)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_Ragnarok, ServerProfileCategory.Rules, "UnicornSpawnInterval", ConditionedOn = "EnableRagnarokSettings", ClearSectionIfEmpty = true)]
        public int UnicornSpawnInterval { get; set; } = 24;


        [DefaultValue(true)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_Ragnarok, ServerProfileCategory.Rules, "EnableVolcano", ConditionedOn = "EnableRagnarokSettings", ClearSectionIfEmpty = true)]
        public bool EnableVolcano { get; set; } = true;


        [DefaultValue(1f)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_Ragnarok, ServerProfileCategory.Rules, "VolcanoInterval", ConditionedOn = "EnableRagnarokSettings", ClearSectionIfEmpty = true)]
        public float VolcanoInterval { get; set; } = 1;


        [DefaultValue(1f)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_Ragnarok, ServerProfileCategory.Rules, "VolcanoIntensity", ConditionedOn = "EnableRagnarokSettings", ClearSectionIfEmpty = true)]
        public float VolcanoIntensity { get; set; } = 1;


        [DefaultValue(false)] public bool EnableFjordurSettings { get; set; } = false;


        [DefaultValue(true)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Rules, "UseFjordurTraversalBuff", ConditionedOn = "EnableFjordurSettings")]
        public bool EnableFjordurBiomeTeleport { get; set; } = true;


        [DefaultValue(false)] public bool EnableGenericQualityClamp { get; set; } = false;


        [DefaultValue(0)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "ItemStatClamps[0]", ConditionedOn = "EnableGenericQualityClamp", WriteIfNotValue = 0)]
        public int GenericQualityClamp { get; set; } = 0;


        [DefaultValue(false)] public bool EnableArmorClamp { get; set; } = false;


        [DefaultValue(0)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "ItemStatClamps[1]", ConditionedOn = "EnableArmorClamp", WriteIfNotValue = 0)]
        public int ArmorClamp { get; set; } = 0;


        [DefaultValue(false)] public bool EnableWeaponDamagePercentClamp { get; set; } = false;


        [DefaultValue(0)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "ItemStatClamps[3]", ConditionedOn = "EnableWeaponDamagePercentClamp", WriteIfNotValue = 0)]
        public int WeaponDamagePercentClamp { get; set; } = 0;


        [DefaultValue(false)] public bool EnableHypoInsulationClamp { get; set; } = false;


        [DefaultValue(0)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "ItemStatClamps[5]", ConditionedOn = "EnableHypoInsulationClamp", WriteIfNotValue = 0)]
        public int HypoInsulationClamp { get; set; } = 0;

        [DefaultValue(false)] public bool EnableWeightClamp { get; set; } = false;

        [DefaultValue(0)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "ItemStatClamps[6]", ConditionedOn = "EnableWeightClamp", WriteIfNotValue = 0)]
        public int WeightClamp { get; set; } = 0;

        [DefaultValue(false)] public bool EnableMaxDurabilityClamp { get; set; } = false;

        [DefaultValue(0)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "ItemStatClamps[2]", ConditionedOn = "EnableMaxDurabilityClamp", WriteIfNotValue = 0)]
        public int MaxDurabilityClamp { get; set; } = 0;

        [DefaultValue(false)] public bool EnableWeaponClipAmmoClamp { get; set; } = false;

        [DefaultValue(0)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "ItemStatClamps[4]", ConditionedOn = "EnableWeaponClipAmmoClamp", WriteIfNotValue = 0)]
        public int WeaponClipAmmoClamp { get; set; } = 0;

        [DefaultValue(false)] public bool EnableHyperInsulationClamp { get; set; } = false;

        [DefaultValue(0)]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Rules, "ItemStatClamps[7]", ConditionedOn = "EnableHyperInsulationClamp", WriteIfNotValue = 0)]
        public int HyperInsulationClamp { get; set; } = 0;

        [DefaultValue(false)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.ChatAndNotifications, "globalVoiceChat")]
        public bool EnableGlobalVoiceChat { get; set; } = false;

        [DefaultValue(false)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.ChatAndNotifications, "proximityChat")]
        public bool EnableProximityChat { get; set; } = false;

        [DefaultValue(false)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.ChatAndNotifications, "alwaysNotifyPlayerLeft")]
        public bool EnablePlayerLeaveNotifications { get; set; } = false;

        [DefaultValue(true)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.ChatAndNotifications, "DontAlwaysNotifyPlayerJoined")]
        public bool EnablePlayerJoinedNotifications { get; set; } = true;

        [DefaultValue(true)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.HudAndVisuals, "ServerCrosshair")]
        public bool AllowCrosshair { get; set; } = true;

        [DefaultValue(true)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.HudAndVisuals, "ServerForceNoHud", InvertBoolean = true)]
        public bool AllowHUD { get; set; } = true;

        [DefaultValue(false)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.HudAndVisuals, "AllowThirdPersonPlayer")]
        public bool AllowThirdPersonView { get; set; } = false;

        [DefaultValue(false)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.HudAndVisuals, "ShowMapPlayerLocation")]
        public bool AllowMapPlayerLocation { get; set; } = false;

        [DefaultValue(false)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.HudAndVisuals, "EnablePVPGamma")]
        public bool AllowPVPGamma { get; set; } = false;

        [DefaultValue(false)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.HudAndVisuals, "DisablePvEGamma", InvertBoolean = true)]
        public bool AllowPvEGamma { get; set; } = false;

        [DefaultValue(false)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.HudAndVisuals, "ShowFloatingDamageText")]
        public bool ShowFloatingDamageText { get; set; } = false;

        [DefaultValue(true)]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.HudAndVisuals, "AllowHitMarkers")]
        public bool AllowHitMarkers { get; set; } = true;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Players, "AllowFlyerCarryPVE")]
        public bool EnableFlyerCarry { get; set; } = false;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Players, "XPMultiplier", WriteIfNotValue = 1f)]
        public float XPMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Players, "PlayerDamageMultiplier", WriteIfNotValue = 1f)]
        public float PlayerDamageMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Players, "PlayerResistanceMultiplier", WriteIfNotValue = 1f)]
        public float PlayerResistanceMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Players, "PlayerCharacterWaterDrainMultiplier", WriteIfNotValue = 1f)]
        public float PlayerCharacterWaterDrainMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Players, "PlayerCharacterFoodDrainMultiplier", WriteIfNotValue = 1f)]
        public float PlayerCharacterFoodDrainMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Players, "PlayerCharacterStaminaDrainMultiplier", WriteIfNotValue = 1f)]
        public float PlayerCharacterStaminaDrainMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Players, "PlayerCharacterHealthRecoveryMultiplier", WriteIfNotValue = 1f)]
        public float PlayerCharacterHealthRecoveryMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Players, "PlayerHarvestingDamageMultiplier", WriteIfNotValue = 1f)]
        public float PlayerHarvestingDamageMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Players, "CraftingSkillBonusMultiplier", WriteIfNotValue = 1f)]
        public float CraftingSkillBonusMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Players, "MaxFallSpeedMultiplier", WriteIfNotValue = 1f)]
        public float MaxFallSpeedMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Players, "PlayerBaseStatMultipliers")]
        public StatsMultiplierFloatArray PlayerBaseStatMultipliers { get; set; }

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Players, "PerLevelStatsMultiplier_Player")]
        public StatsMultiplierFloatArray PerLevelStatsMultiplier_Player { get; set; }

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Dinos, "DinoDamageMultiplier", WriteIfNotValue = 1f)]
        public float DinoDamageMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Dinos, "TamedDinoDamageMultiplier", WriteIfNotValue = 1f)]
        public float TamedDinoDamageMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Dinos, "DinoResistanceMultiplier", WriteIfNotValue = 1f)]
        public float DinoResistanceMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Dinos, "TamedDinoResistanceMultiplier", WriteIfNotValue = 1f)]
        public float TamedDinoResistanceMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Dinos, "DinoCharacterFoodDrainMultiplier", WriteIfNotValue = 1f)]
        public float DinoCharacterFoodDrainMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Dinos, "DinoCharacterStaminaDrainMultiplier", WriteIfNotValue = 1f)]
        public float DinoCharacterStaminaDrainMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Dinos, "DinoCharacterHealthRecoveryMultiplier", WriteIfNotValue = 1f)]
        public float DinoCharacterHealthRecoveryMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "DinoHarvestingDamageMultiplier", WriteIfNotValue = 3f)]
        public float DinoHarvestingDamageMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "DinoTurretDamageMultiplier", WriteIfNotValue = 1f)]
        public float DinoTurretDamageMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Dinos, "AllowRaidDinoFeeding")]
        public bool AllowRaidDinoFeeding { get; set; } = false;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Dinos, "RaidDinoCharacterFoodDrainMultiplier", WriteIfNotValue = 1f)]
        public float RaidDinoCharacterFoodDrainMultiplier { get; set; } = 1f;

        public bool EnableAllowCaveFlyers { get; set; } = false;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Dinos, "AllowFlyingStaminaRecovery", ConditionedOn = "AllowFlyingStaminaRecovery")]
        public bool AllowFlyingStaminaRecovery { get; set; } = false;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "bAllowFlyerSpeedLeveling", ConditionedOn = "AllowFlyerSpeedLeveling")]
        public bool AllowFlyerSpeedLeveling { get; set; } = false;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Dinos, ConditionedOn = "PreventMateBoost")]
        public bool PreventMateBoost { get; set; } = false;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Dinos, "DisableDinoDecayPvE")]
        public bool DisableDinoDecayPvE { get; set; } = false;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Dinos, "PvPDinoDecay", InvertBoolean = true)]
        public bool DisableDinoDecayPvP { get; set; } = true;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Dinos, "AutoDestroyDecayedDinos")]
        public bool AutoDestroyDecayedDinos { get; set; } = false;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "bUseDinoLevelUpAnimations")]
        public bool UseDinoLevelUpAnimations { get; set; } = true;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Dinos, "PvEDinoDecayPeriodMultiplier", WriteIfNotValue = 1f)]
        public float PvEDinoDecayPeriodMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Dinos, "AllowMultipleAttachedC4", ConditionedOn = "AllowMultipleAttachedC4")]
        public bool AllowMultipleAttachedC4 { get; set; } = false;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "bAllowUnclaimDinos")]
        public bool AllowUnclaimDinos { get; set; } = true;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "bDisableDinoRiding", ConditionedOn = "DisableDinoRiding")]
        public bool DisableDinoRiding { get; set; } = false;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "bDisableDinoTaming", ConditionedOn = "DisableDinoTaming")]
        public bool DisableDinoTaming { get; set; } = false;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "bDisableDinoBreeding", ConditionedOn = "DisableDinoBreeding")]
        public bool DisableDinoBreeding { get; set; } = false;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Dinos, "MaxTamedDinos")]
        public int MaxTamedDinos { get; set; } = 5000;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Dinos, "MaxPersonalTamedDinos")]
        public float MaxPersonalTamedDinos { get; set; } = 40;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Dinos, "PersonalTamedDinosSaddleStructureCost")]
        public int PersonalTamedDinosSaddleStructureCost { get; set; } = 19;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "bUseTameLimitForStructuresOnly", ConditionedOn = "UseTameLimitForStructuresOnly")]
        public bool UseTameLimitForStructuresOnly { get; set; } = false;

        public bool EnableForceCanRideFliers { get; set; } = false;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Dinos, "bForceCanRideFliers", ConditionedOn = "EnableForceCanRideFliers")]
        public bool ForceCanRideFliers { get; set; } = true;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "MatingIntervalMultiplier", WriteIfNotValue = 1f)]
        public float MatingIntervalMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "MatingSpeedMultiplier", WriteIfNotValue = 1f)]
        public float MatingSpeedMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "EggHatchSpeedMultiplier", WriteIfNotValue = 1f)]
        public float EggHatchSpeedMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "BabyMatureSpeedMultiplier", WriteIfNotValue = 1f)]
        public float BabyMatureSpeedMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "BabyFoodConsumptionSpeedMultiplier", WriteIfNotValue = 1f)]
        public float BabyFoodConsumptionSpeedMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Dinos, "DisableImprintDinoBuff")]
        public bool DisableImprintDinoBuff { get; set; } = false;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Dinos, "AllowAnyoneBabyImprintCuddle")]
        public bool AllowAnyoneBabyImprintCuddle { get; set; } = false;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "BabyImprintingStatScaleMultiplier", WriteIfNotValue = 1f)]
        public float BabyImprintingStatScaleMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "BabyImprintAmountMultiplier", WriteIfNotValue = 1f)]
        public float BabyImprintAmountMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "BabyCuddleIntervalMultiplier", WriteIfNotValue = 1f)]
        public float BabyCuddleIntervalMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "BabyCuddleGracePeriodMultiplier", WriteIfNotValue = 1f)]
        public float BabyCuddleGracePeriodMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "BabyCuddleLoseImprintQualitySpeedMultiplier", WriteIfNotValue = 1f)]
        public float BabyCuddleLoseImprintQualitySpeedMultiplier { get; set; } = 1f;

        public int Imprintlimit { get; set; } = 101;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "WildDinoCharacterFoodDrainMultiplier", WriteIfNotValue = 1f)]
        public float WildDinoCharacterFoodDrainMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "TamedDinoCharacterFoodDrainMultiplier", WriteIfNotValue = 1f)]
        public float TamedDinoCharacterFoodDrainMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "WildDinoTorporDrainMultiplier", WriteIfNotValue = 1f)]
        public float WildDinoTorporDrainMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "TamedDinoTorporDrainMultiplier", WriteIfNotValue = 1f)]
        public float TamedDinoTorporDrainMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "PassiveTameIntervalMultiplier", WriteIfNotValue = 1f)]
        public float PassiveTameIntervalMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "PerLevelStatsMultiplier_DinoWild")]
        public StatsMultiplierFloatArray PerLevelStatsMultiplier_DinoWild { get; set; }

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "PerLevelStatsMultiplier_DinoTamed")]
        public StatsMultiplierFloatArray PerLevelStatsMultiplier_DinoTamed { get; set; }

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "PerLevelStatsMultiplier_DinoTamed_Add")]
        public StatsMultiplierFloatArray PerLevelStatsMultiplier_DinoTamed_Add { get; set; }

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "PerLevelStatsMultiplier_DinoTamed_Affinity")]
        public StatsMultiplierFloatArray PerLevelStatsMultiplier_DinoTamed_Affinity { get; set; }

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "MutagenLevelBoost")]
        public StatsMultiplierIntegerArray MutagenLevelBoost { get; set; }

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "MutagenLevelBoost_Bred")]
        public StatsMultiplierIntegerArray MutagenLevelBoost_Bred { get; set; }

        [JsonIgnore]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "DinoSpawnWeightMultipliers", WriteIfNotValue = 1f)]
        public AggregateIniValueList<DinoSpawn> DinoSpawnWeightMultipliers { get; set; }

        [JsonIgnore]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "TamedDinoClassDamageMultipliers", WriteIfNotValue = 1f)]
        public AggregateIniValueList<ClassMultiplier> TamedDinoClassDamageMultipliers { get; set; }

        [JsonIgnore]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "TamedDinoClassResistanceMultipliers", WriteIfNotValue = 1f)]
        public AggregateIniValueList<ClassMultiplier> TamedDinoClassResistanceMultipliers { get; set; }

        [JsonIgnore]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "DinoClassDamageMultipliers", WriteIfNotValue = 1f)]
        public AggregateIniValueList<ClassMultiplier> DinoClassDamageMultipliers { get; set; }

        [JsonIgnore]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "DinoClassResistanceMultipliers", WriteIfNotValue = 1f)]
        public AggregateIniValueList<ClassMultiplier> DinoClassResistanceMultipliers { get; set; }

        [JsonIgnore]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "NPCReplacements")]
        public AggregateIniValueList<NPCReplacement> NPCReplacements { get; set; }

        [JsonIgnore]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "PreventDinoTameClassNames")]
        public StringIniValueList PreventDinoTameClassNames { get; set; }

        [JsonIgnore]
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Dinos, "PreventBreedingForClassNames")]
        public StringIniValueList PreventBreedingForClassNames { get; set; }


        [JsonIgnore] public DinoSettingsList DinoSettings { get; set; }

        public List<DinoSettingsJSON> ChangedDinoSettings { get; set; }

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float DinoCountMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float TamingSpeedMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float HarvestAmountMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float ResourcesRespawnPeriodMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float ResourceNoReplenishRadiusPlayers { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float ResourceNoReplenishRadiusStructures { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float HarvestHealthMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Environment)]
        public bool UseOptimizedHarvestingHealth { get; set; } = false;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Environment)]
        public bool ClampResourceHarvestDamage { get; set; } = false;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Environment)]
        public bool ClampItemSpoilingTimes { get; set; } = false;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float BaseTemperatureMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float DayCycleSpeedScale { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float DayTimeSpeedScale { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float NightTimeSpeedScale { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Environment, ConditionedOn = "DisableWeatherFog")]
        public bool DisableWeatherFog { get; set; } = false;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float GlobalSpoilingTimeMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float GlobalItemDecompositionTimeMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float GlobalCorpseDecompositionTimeMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float CropDecaySpeedMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float CropGrowthSpeedMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float LayEggIntervalMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float PoopIntervalMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float HairGrowthSpeedMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float CraftXPMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float GenericXPMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float HarvestXPMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float KillXPMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Environment, WriteIfNotValue = 1f)]
        public float SpecialXPMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Structures, "", WriteIfNotValue = 1f)]
        public float StructureResistanceMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Structures, "", WriteIfNotValue = 1f)]
        public float StructureDamageMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Structures, "", WriteIfNotValue = 180)]
        public int StructureDamageRepairCooldown { get; set; } = 180;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Structures, "", WriteIfNotValue = false)]
        public bool PvPStructureDecay { get; set; } = false;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Structures, "", WriteIfNotValue = 6f)]
        public float PvPZoneStructureDamageMultiplier { get; set; } = 6f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Structures, "TheMaxStructuresInRange", WriteIfNotValue = 10500)]
        public int MaxStructuresInRange { get; set; } = 10500;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Structures, "", WriteIfNotValue = 1f)]
        public float PerPlatformMaxStructuresMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Structures, "", WriteIfNotValue = 0)]
        public int MaxPlatformSaddleStructureLimit { get; set; } = 0;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Structures, "", ConditionedOn = "OverrideStructurePlatformPrevention", WriteIfNotValue = false)]
        public bool OverrideStructurePlatformPrevention { get; set; } = false;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Structures, "bFlyerPlatformAllowUnalignedDinoBasing", ConditionedOn = "FlyerPlatformAllowUnalignedDinoBasing", WriteIfNotValue = false)]
        public bool FlyerPlatformAllowUnalignedDinoBasing { get; set; } = false;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Structures, "", ConditionedOn = "PvEAllowStructuresAtSupplyDrops", WriteIfNotValue = false)]
        public bool PvEAllowStructuresAtSupplyDrops { get; set; } = false;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Structures, "DisableStructureDecayPVE", InvertBoolean = true, WriteIfNotValue = false)]
        public bool EnableStructureDecayPvE { get; set; } = false;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Structures, "", ConditionedOn = "EnableStructureDecayPvE")]
        public float PvEStructureDecayPeriodMultiplier { get; set; } = 1f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Structures, "", WriteIfNotValue = 0.0f)]
        public float AutoDestroyOldStructuresMultiplier { get; set; } = 0f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Structures, "", WriteIfNotValue = false)]
        public bool ForceAllStructureLocking { get; set; } = false;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Structures, "bPassiveDefensesDamageRiderlessDinos", WriteIfNotValue = false)]
        public bool PassiveDefensesDamageRiderlessDinos { get; set; } = false;

        public bool EnableAutoDestroyStructures { get; set; } = false;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Structures, "", WriteIfNotValue = false)]
        public bool OnlyAutoDestroyCoreStructures { get; set; } = false;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Structures, "", WriteIfNotValue = false)]
        public bool OnlyDecayUnsnappedCoreStructures { get; set; } = false;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Structures, "", WriteIfNotValue = false)]
        public bool FastDecayUnsnappedCoreStructures { get; set; } = false;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Structures, "", WriteIfNotValue = false)]
        public bool DestroyUnconnectedWaterPipes { get; set; } = false;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Structures, "bDisableStructurePlacementCollision", WriteIfNotValue = false)]
        public bool DisableStructurePlacementCollision { get; set; } = false;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Structures, "IgnoreLimitMaxStructuresInRangeTypeFlag", WriteIfNotValue = false)]
        public bool RemoveDecorativeStructuresLimit { get; set; } = false;

        public bool EnableFastDecayInterval { get; set; } = false;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Structures, "", ConditionedOn = "EnableFastDecayInterval")]
        public int FastDecayInterval { get; set; } = 43200;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Structures, "bLimitTurretsInRange", WriteIfNotValue = false)]
        public bool LimitTurretsInRange { get; set; } = false;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Structures, "", ConditionedOn = "LimitTurretsInRange")]
        public int LimitTurretsRange { get; set; } = 10000;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Structures, "", ConditionedOn = "LimitTurretsInRange")]
        public int LimitTurretsNum { get; set; } = 100;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Structures, "bHardLimitTurretsInRange", WriteIfNotValue = false)]
        public bool HardLimitTurretsInRange { get; set; } = false;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Structures, "", WriteIfNotValue = false)]
        public bool AlwaysAllowStructurePickup { get; set; } = false;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Structures, "", WriteIfNotValue = 30f)]
        public float StructurePickupTimeAfterPlacement { get; set; } = 30f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Structures, "", WriteIfNotValue = 0.5f)]
        public float StructurePickupHoldDuration { get; set; } = 0.5f;

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Structures, "", WriteIfNotValue = true)]
        public bool AllowIntegratedSPlusStructures { get; set; } = true;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Structures, "bIgnoreStructuresPreventionVolumes", WriteIfNotValue = false)]
        public bool IgnoreStructuresPreventionVolumes { get; set; } = false;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Structures, "bGenesisUseStructuresPreventionVolumes", WriteIfNotValue = false)]
        public bool GenesisUseStructuresPreventionVolumes { get; set; } = false;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Engrams, "bAutoUnlockAllEngrams", ConditionedOn = "AutoUnlockAllEngrams", WriteIfNotValue = false)]
        public bool AutoUnlockAllEngrams { get; set; } = false;

        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Engrams, "bOnlyAllowSpecifiedEngrams", ConditionedOn = "OnlyAllowSpecifiedEngrams", WriteIfNotValue = false)]
        public bool OnlyAllowSpecifiedEngrams { get; set; } = false;
        [JsonIgnore] 
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Engrams, "")]
        public EngramEntryList OverrideNamedEngramEntries { get; set; }
        [JsonIgnore] 
        [IniFileEntry(IniFiles.Game, IniSections.Game_ShooterGameMode, ServerProfileCategory.Engrams, "")]
        public EngramAutoUnlockList EngramEntryAutoUnlocks { get; set; }
        [JsonIgnore] 
        public EngramSettingsList EngramSettings { get; set; }
    }
}