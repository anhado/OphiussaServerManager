using OphiussaServerManager.Common.Helpers;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OphiussaServerManager.Common.Ini;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace OphiussaServerManager.Common.Models.Profiles
{

    public class ArkProfile
    {
        public Administration Administration { get; set; }
        public string DefaultGameUserSettingsINILocation { get { return "ShooterGame\\Saved\\Config\\WindowsServer\\GameUserSettings.ini"; } }
        public string DefaultGameIniLocation { get { return "ShooterGame\\Saved\\Config\\WindowsServer\\Game.ini"; } }

        public ArkProfile()
        {
            this.Administration = new Administration();

        }

        public void LoadNewArkProfile(string key)
        {

        }

        public ArkProfile LoadGameINI(Profile prf)
        {

            SystemIniFile systemIniFile = new SystemIniFile(prf.InstallLocation);

            this.Administration.LogAdminCommandsToPublic = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings).ReadBoolValue("AdminLogging", this.Administration.LogAdminCommandsToPublic);
            this.Administration.AllowHideDamageSourceFromLogs = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings).ReadBoolValue("AllowHideDamageSourceFromLogs", this.Administration.AllowHideDamageSourceFromLogs);
            this.Administration.TribeLogDestroyedEnemyStructures = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings).ReadBoolValue("TribeLogDestroyedEnemyStructures", this.Administration.TribeLogDestroyedEnemyStructures);
            this.Administration.IdleTimout = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings).ReadIntValue("KickIdlePlayersPeriod", this.Administration.IdleTimout);

            if (prf.Type.ServerType == SupportedServers.EnumServerType.ArkSurviveEvolved)
                this.Administration.IdleTimout = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings).ReadIntValue("MaxPlayers", this.Administration.IdleTimout);

            if (prf.Type.ServerType == SupportedServers.EnumServerType.ArkSurviveAscended)
                this.Administration.IdleTimout = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GUS_GameSession).ReadIntValue("MaxPlayers", this.Administration.IdleTimout);

            this.Administration.UseRCON = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings).ReadBoolValue("RCONEnabled", this.Administration.UseRCON);
            this.Administration.RCONPort = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings).ReadStringValue("RCONPort", this.Administration.RCONPort);
            this.Administration.RCONServerLogBuffer = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings).ReadIntValue("RCONServerGameLogBuffer", this.Administration.RCONServerLogBuffer);
            this.Administration.ServerAdminPassword = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings).ReadStringValue("ServerAdminPassword", this.Administration.ServerAdminPassword);
            this.Administration.EnableServerAdminLogs = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings).ReadBoolValue("servergamelog", this.Administration.EnableServerAdminLogs);
            this.Administration.ServerPassword = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings).ReadStringValue("ServerPassword", this.Administration.ServerPassword);
            this.Administration.ServerSpectatorPassword = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings).ReadStringValue("SpectatorPassword", this.Administration.ServerSpectatorPassword);
            this.Administration.ServerName = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GUS_SessionSettings).ReadStringValue("SessionName", this.Administration.ServerName);
            this.Administration.ServerPort = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GUS_SessionSettings).ReadStringValue("Port", this.Administration.ServerPort);
            this.Administration.PeerPort = (systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GUS_SessionSettings).ReadIntValue("Port", int.Parse(this.Administration.ServerPort)) + 1).ToString();
            this.Administration.QueryPort = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GUS_SessionSettings).ReadStringValue("QueryPort", this.Administration.QueryPort);
            this.Administration.MOD = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GUS_MessageOfTheDay).ReadStringValue("Message", this.Administration.MOD);
            this.Administration.MODDuration = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GUS_MessageOfTheDay).ReadIntValue("Duration", this.Administration.MODDuration);

            return this;
        }

        internal void SaveGameINI(Profile profile)
        {
            SystemIniFile systemIniFile = new SystemIniFile(profile.InstallLocation);
             
            List<IniSection> iniSections = systemIniFile.GetAllSections(IniFiles.GameUserSettings);
            List<string> listSectionNames = new List<string>();
            Dictionary<string, List<ConfigFile>> keyValuePairs = new Dictionary<string, List<ConfigFile>>();

            foreach (var section in iniSections)
            {
                keyValuePairs.Add(section.SectionName, systemIniFile.ReadSection(IniFiles.GameUserSettings, section.SectionName).ToListConfigFile());
                listSectionNames.Add(section.SectionName);
            }
             
            keyValuePairs["ServerSettings"].WriteBoolValue("AdminLogging", this.Administration.LogAdminCommandsToPublic); 
            keyValuePairs["ServerSettings"].WriteBoolValue("AllowHideDamageSourceFromLogs", this.Administration.AllowHideDamageSourceFromLogs); 
            keyValuePairs["ServerSettings"].WriteBoolValue("TribeLogDestroyedEnemyStructures", this.Administration.TribeLogDestroyedEnemyStructures); 
            keyValuePairs["ServerSettings"].WriteIntValue("KickIdlePlayersPeriod", this.Administration.IdleTimout);

            if (profile.Type.ServerType == SupportedServers.EnumServerType.ArkSurviveEvolved) 
                keyValuePairs["ServerSettings"].WriteIntValue("MaxPlayers", this.Administration.MaxPlayers);

            if (profile.Type.ServerType == SupportedServers.EnumServerType.ArkSurviveAscended) 
                keyValuePairs["/Script/Engine.GameSession"].WriteIntValue("MaxPlayers", this.Administration.MaxPlayers);
             
            keyValuePairs["ServerSettings"].WriteBoolValue("RCONEnabled", this.Administration.UseRCON); 
            keyValuePairs["ServerSettings"].WriteStringValue("RCONPort", this.Administration.RCONPort); 
            keyValuePairs["ServerSettings"].WriteIntValue("RCONServerGameLogBuffer", this.Administration.RCONServerLogBuffer); 
            keyValuePairs["ServerSettings"].WriteStringValue("ServerAdminPassword", this.Administration.ServerAdminPassword); 
            keyValuePairs["ServerSettings"].WriteBoolValue("servergamelog", this.Administration.EnableServerAdminLogs); 
            keyValuePairs["ServerSettings"].WriteStringValue("ServerPassword", this.Administration.ServerPassword); 
            keyValuePairs["ServerSettings"].WriteStringValue("SpectatorPassword", this.Administration.ServerSpectatorPassword); 
            keyValuePairs["SessionSettings"].WriteStringValue("SessionName", this.Administration.ServerName); 
            keyValuePairs["SessionSettings"].WriteStringValue("Port", this.Administration.ServerPort); 
            keyValuePairs["SessionSettings"].WriteStringValue("QueryPort", this.Administration.QueryPort); 
            keyValuePairs["MessageOfTheDay"].WriteStringValue("Message", this.Administration.MOD); 
            keyValuePairs["MessageOfTheDay"].WriteIntValue("Duration", this.Administration.MODDuration);

            foreach (var section in keyValuePairs)
            {
                systemIniFile.WriteSection(IniFiles.GameUserSettings, section.Key, section.Value.ToEnumerableConfigFile());
            } 
        }

        public string GetCommandLinesArguments(Settings settings, Profile prf, string PublicIP)
        {
            string cmd = string.Empty;

            List<string> hifenArgs = new List<string>();
            List<string> InterrogationArgs = new List<string>();
            InterrogationArgs.Add("?listen");
            //if (this.Administration.AllowFlyerSpeedLeveling) hifenArgs.Add(" -AllowFlyerSpeedLeveling");
            if (this.Administration.AlternateSaveDirectoryName.Trim() != "") InterrogationArgs.Add($"?AltSaveDirectoryName=\"{this.Administration.AlternateSaveDirectoryName.Trim()}\"");
            //if (this.Administration.AutoDestroyStructures) hifenArgs.Add(" -AutoDestroyStructures");
            //if (this.Administration.AutoManagedMods) hifenArgs.Add(" -automanagedmods"); //Only ASE
            if (prf.Type.ServerType == SupportedServers.EnumServerType.ArkSurviveEvolved)
                if (this.Administration.EnableCrossPlay) hifenArgs.Add(" -crossplay"); //Only ASE
            if (prf.Type.ServerType == SupportedServers.EnumServerType.ArkSurviveEvolved)
                if (this.Administration.EnablePublicIPForEpic) hifenArgs.Add($" -PublicIPForEpic={PublicIP}");//Only For ASE
            //if (this.Administration.LogsLanguage) hifenArgs.Add($" -culture=<lang_code>");
            //if (this.Administration.DisableCustomFoldersInTributeInventories) hifenArgs.Add(" -DisableCustomFoldersInTributeInventories");
            //if (this.Administration.DisableRailgunPVP) hifenArgs.Add(" -DisableRailgunPVP");
            if (this.Administration.EnablIdleTimeOut) hifenArgs.Add(" -EnableIdlePlayerKick");
            if (prf.Type.ServerType == SupportedServers.EnumServerType.ArkSurviveEvolved)
                if (this.Administration.EpicStorePlayersOnly) hifenArgs.Add(" -epiconly"); //Only ASE
            //if (this.Administration.EventColorsChanceOverride != "") InterrogationArgs.Add($"?EventColorsChanceOverride=\"{this.Administration.EventColorsChanceOverride.ToString()}\"");
            //if (this.Administration.ExclusiveJoin) hifenArgs.Add(" -exclusivejoin");
            //if (this.Administration.ForceAllowCaveFlyers) hifenArgs.Add(" -ForceAllowCaveFlyers");
            if (this.Administration.EnableAutoForceRespawnDinos) hifenArgs.Add(" -ForceRespawnDinos");
            if (prf.Type.ServerType == SupportedServers.EnumServerType.ArkSurviveAscended)
                if (this.Administration.ModIDs.Count > 0 && string.Join(",", this.Administration.ModIDs.ToArray()) != "") hifenArgs.Add($" -mods={string.Join(",", this.Administration.ModIDs.ToArray())}");
            //if (this.Administration.imprintlimit) hifenArgs.Add(" -imprintlimit=101");
            if (this.Administration.DisableVAC) hifenArgs.Add(" -insecure");
            //if (this.Administration.MapModID) hifenArgs.Add(" -MapModID=<ModID>");//Dont Use This
            //if (this.Administration.MaxNumOfSaveBackups) hifenArgs.Add($" -MaxNumOfSaveBackups={this.Administration.MaxNumOfSaveBackups}");
            //if (this.Administration.MapPlayerLocation != "") InterrogationArgs.Add($"?AltSaveDirectoryName=\"{(this.Administration.MapPlayerLocation ? "True" : "False")}\"");
            //if (this.Administration.MinimumTimeBetweenInventoryRetrieval) hifenArgs.Add($" -MinimumTimeBetweenInventoryRetrieval={this.Administration.MinimumTimeBetweenInventoryRetrieval}");
            if (this.Administration.LocalIP.Trim() != "") InterrogationArgs.Add($"?MultiHome={this.Administration.LocalIP.Trim()}");
            if (this.Administration.DisableAntiSpeedHackDetection) hifenArgs.Add(" -noantispeedhack");
            if (!this.Administration.EnableBattleEye) hifenArgs.Add(" -NoBattlEye");
            if (this.Administration.DisablePlayerMovePhysics) hifenArgs.Add(" -nocombineclientmoves");
            if (this.Administration.NoDinos) hifenArgs.Add(" -nodinos");
            if (this.Administration.NoHandDetection) hifenArgs.Add(" -NoHangDetection");
            if (this.Administration.LogAdminCommandsToAdmins) hifenArgs.Add(" -NotifyAdminCommandsInChat");
            if (this.Administration.NoUnderMeshChecking) hifenArgs.Add(" -noundermeshchecking");
            if (this.Administration.NoUnderMeshKilling) hifenArgs.Add(" -noundermeshkilling");
            //if (this.Administration.PVEDisallowTribeWar) hifenArgs.Add(" -pvedisallowtribewar");
            //if (this.Administration.SecureSendArKPayload) hifenArgs.Add(" -SecureSendArKPayload");
            if (prf.Type.ServerType == SupportedServers.EnumServerType.ArkSurviveAscended)
                if (this.Administration.ServerAllowAnsel) hifenArgs.Add($" -ServerAllowAnsel");
            if (this.Administration.EnableServerAdminLogs) hifenArgs.Add(" -servergamelog");
            if (this.Administration.ServerAdminLogsIncludeTribeLogs) hifenArgs.Add(" -servergamelogincludetribelogs");
            if (this.Administration.ServerRCONOutputTribeLogs) hifenArgs.Add(" -ServerRCONOutputTribeLogs");
            if (this.Administration.AntiSpeedHackBias != 0) hifenArgs.Add($" -speedhackbias={this.Administration.AntiSpeedHackBias}");
            if (this.Administration.StasisKeepController) hifenArgs.Add($" -StasisKeepControllers");
            if (this.Administration.StructureMemoryOptimizations) hifenArgs.Add($" -structurememopts");
            if (this.Administration.TotalConversionID != "") hifenArgs.Add($" -TotalConversionMod={this.Administration.TotalConversionID}");
            //if (this.Administration.UseDynamicConfig) hifenArgs.Add($" -UseDynamicConfig");
            //if (this.Administration.UseItemDupeCheck) hifenArgs.Add($" -UseItemDupeCheck");
            //if (this.Administration.UseSecureSpawnRules) hifenArgs.Add($" -UseSecureSpawnRules");
            //if (this.Administration.usestore) hifenArgs.Add($" -usestore");/Dont do this
            //if (this.Administration.UseStructureStasisGrid) hifenArgs.Add($" -UseStructureStasisGrid");
            if (this.Administration.EnableVivox) hifenArgs.Add($" -UseVivox");
            //if (this.Administration.webalarm) hifenArgs.Add($" -webalarm");
            if (prf.Type.ServerType == SupportedServers.EnumServerType.ArkSurviveAscended)
                if (this.Administration.MaxPlayers != 0) hifenArgs.Add($" -WinLiveMaxPlayers={this.Administration.MaxPlayers}");
            if (prf.Type.ServerType == SupportedServers.EnumServerType.ArkSurviveEvolved)
                if (this.Administration.MaxPlayers != 0) hifenArgs.Add($" -MaxPlayers={this.Administration.MaxPlayers}");
            if (prf.Type.ServerType == SupportedServers.EnumServerType.ArkSurviveEvolved)
                if (this.Administration.ClusterDirectoryOverride) hifenArgs.Add($" -ClusterDirOverride=\"{settings.DataFolder}\"");
            if (prf.Type.ServerType == SupportedServers.EnumServerType.ArkSurviveEvolved)
                if (this.Administration.ClusterID != "") hifenArgs.Add($" -clusterid=\"{this.Administration.ClusterID}\"");
            if (this.Administration.ForceDirectX10) hifenArgs.Add($" -dx10");
            if (this.Administration.ForceLowMemory) hifenArgs.Add($" -lowmemory");
            if (this.Administration.ForceNoManSky) hifenArgs.Add($" -nomansky");
            if (!this.Administration.UseNoMemoryBias) hifenArgs.Add($" -nomemorybias");
            if (this.Administration.UseCache) hifenArgs.Add($" -usecache");
            //if (this.Administration.PreventHibernation) hifenArgs.Add($" -PreventHibernation");

            InterrogationArgs.Add($"?Port={this.Administration.ServerPort}");
            InterrogationArgs.Add($"?QueryPort={this.Administration.QueryPort}");

            hifenArgs.Add($" -nosteamclient");
            hifenArgs.Add($" -game");
            hifenArgs.Add($" -server");
            hifenArgs.Add($" -log");

            //?AllowCrateSpawnsOnTopOfStructures=True 
            //-ForceAllowCaveFlyers
            //-NoTransferFromFiltering
            //-exclusivejoin

            // Olympus?listen?MultiHome=192.168.1.250?Port=8799?QueryPort=27036?MaxPlayers=50?AllowCrateSpawnsOnTopOfStructures=True 
            //-ActiveEvent=FearEvolved -ForceAllowCaveFlyers -EnableIdlePlayerKick -clusterid=OphiussaPVECluster01 -ClusterDirOverride="G:\asmdata" 
            //-NoTransferFromFiltering -NoBattlEye -forcerespawndinos -servergamelog -servergamelogincludetribelogs -ServerRCONOutputTribeLogs 
            //-usecache -NoHangDetection -exclusivejoin -nosteamclient -game -server -log
            cmd += this.Administration.MapName + string.Join("", InterrogationArgs.ToArray()) + string.Join("", hifenArgs.ToArray());

            return cmd;
        }
    }
    public class Administration
    {
        public bool UseServerAPI { get; set; } = false;
        public string ServerName { get; set; } = "New Server";
        public string ServerPassword { get; set; } = System.Web.Security.Membership.GeneratePassword(10, 6);
        public string ServerAdminPassword { get; set; } = System.Web.Security.Membership.GeneratePassword(10, 6);
        public string ServerSpectatorPassword { get; set; } = "";
        public string LocalIP { get; set; } = NetworkTools.GetHostIp();
        public string ServerPort { get; set; } = "7777";
        public string PeerPort { get; set; } = "7778";
        public string QueryPort { get; set; } = "27015";
        public bool UseRCON { get; set; } = false;
        public string RCONPort { get; set; } = "32330";
        public int RCONServerLogBuffer { get; set; } = 600;
        public string MapName { get; set; } = "";
        public string TotalConversionID { get; set; } = "";
        public List<string> ModIDs { get; set; } = new List<string>();
        public int AutoSavePeriod { get; set; } = 15;
        public string MOD { get; set; } = "";
        public int MODDuration { get; set; } = 20;
        public bool EnableInterval { get; set; } = true;
        public int MODInterval { get; set; } = 60;
        public string Branch { get; set; } = "Live";
        public int MaxPlayers { get; set; } = 70;
        public bool EnablIdleTimeOut { get; set; } = true;
        public int IdleTimout { get; set; } = 3600;
        public bool UseBanListUrl { get; set; } = false;
        public string BanListUrl { get; set; } = "http://arkdedicated.com/banlist.txt";
        public bool DisableVAC { get; set; } = false;
        public bool EnableBattleEye { get; set; } = false;
        public bool DisablePlayerMovePhysics { get; set; } = false;
        public bool OutputLogToConsole { get; set; } = true;
        public bool UseAllCores { get; set; } = false;
        public bool UseCache { get; set; } = false;
        public bool NoHandDetection { get; set; } = false;
        public bool NoDinos { get; set; } = false;
        public bool NoUnderMeshChecking { get; set; } = false;
        public bool NoUnderMeshKilling { get; set; } = false;
        public bool EnableVivox { get; set; } = false;
        public bool AllowSharedConnections { get; set; } = true;
        public bool RespawnDinosOnStartUp { get; set; } = false;
        public bool EnableAutoForceRespawnDinos { get; set; } = false;
        public int AutoForceRespawnDinosInterval { get; set; } = 24;
        public bool DisableAntiSpeedHackDetection { get; set; } = false;
        public int AntiSpeedHackBias { get; set; } = 1;
        public bool ForceDirectX10 { get; set; } = false;
        public bool ForceLowMemory { get; set; } = false;
        public bool ForceNoManSky { get; set; } = false;
        public bool UseNoMemoryBias { get; set; } = false;
        public bool StasisKeepController { get; set; } = false;
        public bool ServerAllowAnsel { get; set; } = false;
        public bool StructureMemoryOptimizations { get; set; } = false;
        public bool EnableCrossPlay { get; set; } = false;
        public bool EnablePublicIPForEpic { get; set; } = false;
        public bool EpicStorePlayersOnly { get; set; } = false;
        public string AlternateSaveDirectoryName { get; set; } = "";
        public string ClusterID { get; set; } = "";
        public bool ClusterDirectoryOverride { get; set; } = false;
        public ProcessPriorityClass CPUPriority { get; set; } = ProcessPriorityClass.Normal;
        public string CPUAffinity { get; set; } = "All";
        public bool EnableServerAdminLogs { get; set; } = true;
        public bool ServerAdminLogsIncludeTribeLogs { get; set; } = true;
        public bool ServerRCONOutputTribeLogs { get; set; } = true;
        public bool AllowHideDamageSourceFromLogs { get; set; } = true;
        public int MaximumTribeLogs { get; set; } = 100;
        public bool LogAdminCommandsToPublic { get; set; } = false;
        public bool LogAdminCommandsToAdmins { get; set; } = false;
        public bool TribeLogDestroyedEnemyStructures { get; set; } = true;
    }
}
