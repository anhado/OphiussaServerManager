using OphiussaServerManager.Helpers;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using OphiussaServerManager.Ini;

namespace OphiussaServerManager.Models.Profiles
{
    public class ArkProfile
    {
        public Administration Administration { get; set; }
        public string DefaultGameUserSettingsINILocation { get { return "ShooterGame\\Saved\\Config\\WindowsServer\\GameUserSettings.ini"; } }
        public string DefaultGameIniLocation { get { return "ShooterGame\\Saved\\Config\\WindowsServer\\Game.ini"; } }

        public ArkProfile()
        {
            this.Administration = new Administration();
            this.Administration.ServerName = "New Server";
            this.Administration.ServerPassword = System.Web.Security.Membership.GeneratePassword(10, 6);
            this.Administration.ServerAdminPassword = System.Web.Security.Membership.GeneratePassword(10, 6);
            this.Administration.ServerSpectatorPassword = "";
            this.Administration.LocalIP = NetworkTools.GetHostIp();
            this.Administration.ServerPort = "7777";
            this.Administration.PeerPort = "7778";
            this.Administration.QueryPort = "27015";
            this.Administration.UseRCON = false;
            this.Administration.RCONPort = "32330";
            this.Administration.RCONServerLogBuffer = 600;
            this.Administration.MapName = "";
            this.Administration.TotalConversionID = "";
            this.Administration.ModIDs = new List<string>();
            this.Administration.AutoSavePeriod = 15;
            this.Administration.MOD = "";
            this.Administration.MODDuration = 20;
            this.Administration.EnableInterval = true;
            this.Administration.MODInterval = 60;
            this.Administration.Branch = "Live";

            this.Administration.MaxPlayers = 70;
            this.Administration.EnablIdleTimeOut = true;
            this.Administration.IdleTimout = 3600;
            this.Administration.UseBanListUrl = false;
            this.Administration.BanListUrl = "http://arkdedicated.com/banlist.txt";
            this.Administration.DisableVAC = false;
            this.Administration.EnableBattleEye = false;
            this.Administration.DisablePlayerMovePhysics = false;
            this.Administration.OutputLogToConsole = true;
            this.Administration.UseAllCores = false;
            this.Administration.UseCache = false;
            this.Administration.NoHandDetection = false;
            this.Administration.NoDinos = false;
            this.Administration.NoUnderMeshChecking = false;
            this.Administration.NoUnderMeshKilling = false;
            this.Administration.EnableVivox = false;
            this.Administration.AllowSharedConnections = true;
            this.Administration.RespawnDinosOnStartUp = false;
            this.Administration.EnableAutoForceRespawnDinos = false;
            this.Administration.AutoForceRespawnDinosInterval = 24;
            this.Administration.DisableAntiSpeedHackDetection = false;
            this.Administration.AntiSpeedHackBias = 1;
            this.Administration.ForceDirectX10 = false;
            this.Administration.ForceLowMemory = false;
            this.Administration.ForceNoManSky = false;
            this.Administration.UseNoMemoryBias = false;
            this.Administration.StasisKeepController = false;
            this.Administration.ServerAllowAnsel = false;
            this.Administration.StructureMemoryOptimizations = false;
            this.Administration.EnableCrossPlay = false;
            this.Administration.EnablePublicIPForEpic = false;
            this.Administration.EpicStorePlayersOnly = false;
            this.Administration.AlternateSaveDirectoryName = "";
            this.Administration.ClusterID = "";
            this.Administration.ClusterDirectoryOverride = false;
            this.Administration.CPUPriority = ProcessPriorityClass.Normal;
            this.Administration.CPUAffinity = "All";

            this.Administration.EnableServerAdminLogs = true;
            this.Administration.ServerAdminLogsIncludeTribeLogs = true;
            this.Administration.ServerRCONOutputTribeLogs = true;
            this.Administration.AllowHideDamageSourceFromLogs = true;
            this.Administration.MaximumTribeLogs = "100";
            this.Administration.LogAdminCommandsToPublic = false;
            this.Administration.LogAdminCommandsToAdmins = false;
            this.Administration.TribeLogDestroyedEnemyStructures = true;

        }

        public void LoadNewArkProfile(string key)
        {

        }

        public void LoadGameINI(Profile prf)
        {

            SystemIniFile systemIniFile = new SystemIniFile(prf.InstallLocation);

            var asd = systemIniFile.ReadSection(IniFiles.GameUserSettings, "/Game/PrimalEarth/CoreBlueprints/TestGameMode.TestGameMode_C");
            //systemIniFile.w

            string x = "";
        }

        public string GetCommandLinesArguments(Profile prf)
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
                if (this.Administration.EnablePublicIPForEpic) hifenArgs.Add($" -PublicIPForEpic={MainForm.PublicIP}");//Only For ASE
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
            if (this.Administration.LogAdminCommandsToPublic) hifenArgs.Add(" -NotifyAdminCommandsInChat");
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
            //if (prf.Type.ServerType == SupportedServers.EnumServerType.ArkSurviveAscended)
            //    if (this.Administration.WinLiveMaxPlayers) hifenArgs.Add($" -WinLiveMaxPlayers");
            if (prf.Type.ServerType == SupportedServers.EnumServerType.ArkSurviveEvolved)
                if (this.Administration.ClusterDirectoryOverride) hifenArgs.Add($" -ClusterDirOverride=\"{MainForm.Settings.DataFolder}\"");
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

            if (prf.Type.ServerType == SupportedServers.EnumServerType.ArkSurviveAscended)
                if (this.Administration.MaxPlayers != 0) hifenArgs.Add($" -MaxPlayers={this.Administration.MaxPlayers}");

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
        public string ServerName { get; set; }
        public string ServerPassword { get; set; }
        public string ServerAdminPassword { get; set; }
        public string ServerSpectatorPassword { get; set; }
        public string LocalIP { get; set; }
        public string ServerPort { get; set; }
        public string PeerPort { get; set; }
        public string QueryPort { get; set; }
        public bool UseRCON { get; set; }
        public string RCONPort { get; set; }
        public int RCONServerLogBuffer { get; set; }
        public string MapName { get; set; }
        public string TotalConversionID { get; set; }
        public List<string> ModIDs { get; set; }
        public int AutoSavePeriod { get; set; }
        public string MOD { get; set; }
        public int MODDuration { get; set; }
        public bool EnableInterval { get; set; }
        public int MODInterval { get; set; }
        public string Branch { get; set; }
        public int MaxPlayers { get; set; }
        public bool EnablIdleTimeOut { get; set; }
        public int IdleTimout { get; set; }
        public bool UseBanListUrl { get; set; }
        public string BanListUrl { get; set; }
        public bool DisableVAC { get; set; }
        public bool EnableBattleEye { get; set; }
        public bool DisablePlayerMovePhysics { get; set; }
        public bool OutputLogToConsole { get; set; }
        public bool UseAllCores { get; set; }
        public bool UseCache { get; set; }
        public bool NoHandDetection { get; set; }
        public bool NoDinos { get; set; }
        public bool NoUnderMeshChecking { get; set; }
        public bool NoUnderMeshKilling { get; set; }
        public bool EnableVivox { get; set; }
        public bool AllowSharedConnections { get; set; }
        public bool RespawnDinosOnStartUp { get; set; }
        public bool EnableAutoForceRespawnDinos { get; set; }
        public int AutoForceRespawnDinosInterval { get; set; }
        public bool DisableAntiSpeedHackDetection { get; set; }
        public int AntiSpeedHackBias { get; set; }
        public bool ForceDirectX10 { get; set; }
        public bool ForceLowMemory { get; set; }
        public bool ForceNoManSky { get; set; }
        public bool UseNoMemoryBias { get; set; }
        public bool StasisKeepController { get; set; }
        public bool ServerAllowAnsel { get; set; }
        public bool StructureMemoryOptimizations { get; set; }
        public bool EnableCrossPlay { get; set; }
        public bool EnablePublicIPForEpic { get; set; }
        public bool EpicStorePlayersOnly { get; set; }
        public string AlternateSaveDirectoryName { get; set; }
        public string ClusterID { get; set; }
        public bool ClusterDirectoryOverride { get; set; }
        public ProcessPriorityClass CPUPriority { get; set; }
        public string CPUAffinity { get; set; }
        public bool EnableServerAdminLogs { get; set; }
        public bool ServerAdminLogsIncludeTribeLogs { get; set; }
        public bool ServerRCONOutputTribeLogs { get; set; }
        public bool AllowHideDamageSourceFromLogs { get; set; }
        public string MaximumTribeLogs { get; set; }
        public bool LogAdminCommandsToPublic { get; set; }
        public bool LogAdminCommandsToAdmins { get; set; }
        public bool TribeLogDestroyedEnemyStructures { get; set; }
    }
}
