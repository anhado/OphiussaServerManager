using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Security;
using CoreRCON;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Ini;
using OphiussaServerManager.Common.Models.SupportedServers;

namespace OphiussaServerManager.Common.Models.Profiles.ArkProfile {
    public class ArkProfile /*: BaseProfile*/ {
        public ArkProfile() {
            Administration = new Administration();
            Rules          = new Rules();
        }

        public Administration Administration { get; set; }
        public Rules          Rules          { get; set; }

        public string DefaultGameUserSettingsIniLocation => "ShooterGame\\Saved\\Config\\WindowsServer\\GameUserSettings.ini";

        public string DefaultGameIniLocation => "ShooterGame\\Saved\\Config\\WindowsServer\\Game.ini";

        public ArkProfile LoadGameIni(Profile prf) {
            var systemIniFile = new SystemIniFile(prf.InstallLocation);

            Administration.LogAdminCommandsToPublic         = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("AdminLogging",                     Administration.LogAdminCommandsToPublic);
            Administration.AllowHideDamageSourceFromLogs    = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("AllowHideDamageSourceFromLogs",    Administration.AllowHideDamageSourceFromLogs);
            Administration.TribeLogDestroyedEnemyStructures = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("TribeLogDestroyedEnemyStructures", Administration.TribeLogDestroyedEnemyStructures);
            Administration.IdleTimout                       = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadIntValue("KickIdlePlayersPeriod", Administration.IdleTimout);

            if (prf.Type.ServerType == EnumServerType.ArkSurviveEvolved)
                Administration.IdleTimout = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadIntValue("MaxPlayers", Administration.IdleTimout);

            if (prf.Type.ServerType == EnumServerType.ArkSurviveAscended)
                Administration.IdleTimout = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusGameSession).ReadIntValue("MaxPlayers", Administration.IdleTimout);

            Administration.UseRcon                 = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("RCONEnabled", Administration.UseRcon);
            Administration.RconPort                = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadStringValue("RCONPort", Administration.RconPort);
            Administration.RconServerLogBuffer     = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadIntValue("RCONServerGameLogBuffer", Administration.RconServerLogBuffer);
            Administration.ServerAdminPassword     = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadStringValue("ServerAdminPassword", Administration.ServerAdminPassword);
            Administration.EnableServerAdminLogs   = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("servergamelog", Administration.EnableServerAdminLogs);
            Administration.ServerPassword          = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadStringValue("ServerPassword",    Administration.ServerPassword);
            Administration.ServerSpectatorPassword = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadStringValue("SpectatorPassword", Administration.ServerSpectatorPassword);
            Administration.ServerName              = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusSessionSettings).ReadStringValue("SessionName", Administration.ServerName);
            Administration.ServerPort              = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusSessionSettings).ReadStringValue("Port",        Administration.ServerPort);
            Administration.PeerPort                = (systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusSessionSettings).ReadIntValue("Port", Administration.ServerPort.ToInt()) + 1).ToString();
            Administration.QueryPort               = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusSessionSettings).ReadStringValue("QueryPort", Administration.QueryPort);
            Administration.Mod                     = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusMessageOfTheDay).ReadStringValue("Message", Administration.Mod);
            Administration.ModDuration             = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusMessageOfTheDay).ReadIntValue("Duration", Administration.ModDuration);

            Rules.EnableHardcoreMode                 = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("ServerHardcore",                        Rules.EnableHardcoreMode);
            Rules.PreventBuildingInResourceRichAreas = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("EnableExtraStructurePreventionVolumes", Rules.PreventBuildingInResourceRichAreas);
            Rules.EnablePvp                          = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("serverPVE",                             Rules.EnablePvp);
            Rules.EnablePveCaveBuilding              = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("AllowCaveBuildingPvE",                  Rules.EnablePveCaveBuilding);
            Rules.EnablePvpCaveBuilding              = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("AllowCaveBuildingPvP",                  Rules.EnablePvpCaveBuilding);
            Rules.AllowCrateSpawnsOnTopOfStructures  = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("AllowCrateSpawnsOnTopOfStructures",     Rules.AllowCrateSpawnsOnTopOfStructures);

            Rules.EnableSinglePlayerSettings = systemIniFile.ReadSection(IniFiles.Game, IniSections.GameShooterGameMode).ReadBoolValue("bUseSingleplayerSettings", Rules.EnableSinglePlayerSettings);
            Rules.DisableSupplyCrates        = systemIniFile.ReadSection(IniFiles.Game, IniSections.GameShooterGameMode).ReadBoolValue("bDisableLootCrates",       Rules.DisableSupplyCrates);
            Rules.DisablePveFriendlyFire     = systemIniFile.ReadSection(IniFiles.Game, IniSections.GameShooterGameMode).ReadBoolValue("bPvEDisableFriendlyFire",  Rules.DisablePveFriendlyFire);
            Rules.DisablePvpFriendlyFire     = systemIniFile.ReadSection(IniFiles.Game, IniSections.GameShooterGameMode).ReadBoolValue("bDisableFriendlyFire",     Rules.DisablePvpFriendlyFire);
            return this;
        }

        internal void SaveGameIni(Profile profile) {
            var systemIniFile = new SystemIniFile(profile.InstallLocation);

            var iniSections      = systemIniFile.GetAllSections(IniFiles.GameUserSettings);
            var listSectionNames = new List<string>();
            var keyValuePairs    = new Dictionary<string, List<ConfigFile>>();

            foreach (var section in iniSections) {
                keyValuePairs.Add(section.SectionName, systemIniFile.ReadSection(IniFiles.GameUserSettings, section.SectionName).ToListConfigFile());
                listSectionNames.Add(section.SectionName);
            }

            if (keyValuePairs.Count == 0) return;

            if (!keyValuePairs.ContainsKey("MessageOfTheDay")) keyValuePairs.Add("MessageOfTheDay",                                                         systemIniFile.ReadSection(IniFiles.GameUserSettings, "MessageOfTheDay").ToListConfigFile());
            if (!keyValuePairs.ContainsKey("ServerSettings")) keyValuePairs.Add("ServerSettings",                                                           systemIniFile.ReadSection(IniFiles.GameUserSettings, "ServerSettings").ToListConfigFile());
            if (!keyValuePairs.ContainsKey("ScalabilityGroups")) keyValuePairs.Add("ScalabilityGroups",                                                     systemIniFile.ReadSection(IniFiles.GameUserSettings, "ScalabilityGroups").ToListConfigFile());
            if (!keyValuePairs.ContainsKey("/Script/ShooterGame.ShooterGameUserSettings")) keyValuePairs.Add("/Script/ShooterGame.ShooterGameUserSettings", systemIniFile.ReadSection(IniFiles.GameUserSettings, "/Script/ShooterGame.ShooterGameUserSettings").ToListConfigFile());
            if (!keyValuePairs.ContainsKey("/Script/Engine.GameUserSettings")) keyValuePairs.Add("/Script/Engine.GameUserSettings",                         systemIniFile.ReadSection(IniFiles.GameUserSettings, "/Script/Engine.GameUserSettings").ToListConfigFile());
            if (!keyValuePairs.ContainsKey("SessionSettings")) keyValuePairs.Add("SessionSettings",                                                         systemIniFile.ReadSection(IniFiles.GameUserSettings, "SessionSettings").ToListConfigFile());
            if (!keyValuePairs.ContainsKey("/Script/Engine.GameSession")) keyValuePairs.Add("/Script/Engine.GameSession",                                   systemIniFile.ReadSection(IniFiles.GameUserSettings, "/Script/Engine.GameSession").ToListConfigFile());

            keyValuePairs["ServerSettings"].WriteBoolValue("AdminLogging",                     Administration.LogAdminCommandsToPublic);
            keyValuePairs["ServerSettings"].WriteBoolValue("AllowHideDamageSourceFromLogs",    Administration.AllowHideDamageSourceFromLogs);
            keyValuePairs["ServerSettings"].WriteBoolValue("TribeLogDestroyedEnemyStructures", Administration.TribeLogDestroyedEnemyStructures);
            keyValuePairs["ServerSettings"].WriteIntValue("KickIdlePlayersPeriod", Administration.IdleTimout);

            if (profile.Type.ServerType == EnumServerType.ArkSurviveEvolved)
                keyValuePairs["ServerSettings"].WriteIntValue("MaxPlayers", Administration.MaxPlayers);

            if (profile.Type.ServerType == EnumServerType.ArkSurviveAscended)
                keyValuePairs["/Script/Engine.GameSession"].WriteIntValue("MaxPlayers", Administration.MaxPlayers);

            keyValuePairs["ServerSettings"].WriteBoolValue("RCONEnabled", Administration.UseRcon);
            keyValuePairs["ServerSettings"].WriteStringValue("RCONPort", Administration.RconPort);
            keyValuePairs["ServerSettings"].WriteIntValue("RCONServerGameLogBuffer", Administration.RconServerLogBuffer);
            keyValuePairs["ServerSettings"].WriteStringValue("ServerAdminPassword", Administration.ServerAdminPassword);
            keyValuePairs["ServerSettings"].WriteBoolValue("servergamelog", Administration.EnableServerAdminLogs);
            keyValuePairs["ServerSettings"].WriteStringValue("ServerPassword",    Administration.ServerPassword);
            keyValuePairs["ServerSettings"].WriteStringValue("SpectatorPassword", Administration.ServerSpectatorPassword);
            keyValuePairs["SessionSettings"].WriteStringValue("SessionName", Administration.ServerName);
            keyValuePairs["SessionSettings"].WriteStringValue("Port",        Administration.ServerPort);
            keyValuePairs["SessionSettings"].WriteStringValue("QueryPort",   Administration.QueryPort);
            keyValuePairs["MessageOfTheDay"].WriteStringValue("Message", Administration.Mod);
            keyValuePairs["MessageOfTheDay"].WriteIntValue("Duration", Administration.ModDuration);

            if (profile.Type.ServerType == EnumServerType.ArkSurviveEvolved)
                keyValuePairs["ServerSettings"].WriteStringValue("ActiveMods", string.Join(",", profile.ArkConfiguration.Administration.ModIDs.ToArray()));

            foreach (var section in keyValuePairs) systemIniFile.WriteSection(IniFiles.GameUserSettings, section.Key, section.Value.ToEnumerableConfigFile());
        }

        public string GetCommandLinesArguments(Settings settings, Profile prf, string publicIp) {
            string cmd = string.Empty;

            var hifenArgs         = new List<string>();
            var interrogationArgs = new List<string>();
            interrogationArgs.Add("?listen");
            //if (this.Administration.AllowFlyerSpeedLeveling) hifenArgs.Add(" -AllowFlyerSpeedLeveling");
            if (Administration.AlternateSaveDirectoryName.Trim() != "") interrogationArgs.Add($"?AltSaveDirectoryName=\"{Administration.AlternateSaveDirectoryName.Trim()}\"");
            //if (this.Administration.AutoDestroyStructures) hifenArgs.Add(" -AutoDestroyStructures");
            //if (this.Administration.AutoManagedMods) hifenArgs.Add(" -automanagedmods"); //Only ASE
            if (prf.Type.ServerType == EnumServerType.ArkSurviveEvolved)
                if (Administration.EnableCrossPlay)
                    hifenArgs.Add(" -crossplay"); //Only ASE
            if (prf.Type.ServerType == EnumServerType.ArkSurviveEvolved)
                if (Administration.EnablePublicIpForEpic)
                    hifenArgs.Add($" -PublicIPForEpic={publicIp}"); //Only For ASE
            //if (this.Administration.LogsLanguage) hifenArgs.Add($" -culture=<lang_code>");
            //if (this.Administration.DisableCustomFoldersInTributeInventories) hifenArgs.Add(" -DisableCustomFoldersInTributeInventories");
            //if (this.Administration.DisableRailgunPVP) hifenArgs.Add(" -DisableRailgunPVP");
            if (Administration.EnablIdleTimeOut) hifenArgs.Add(" -EnableIdlePlayerKick");
            if (prf.Type.ServerType == EnumServerType.ArkSurviveEvolved)
                if (Administration.EpicStorePlayersOnly)
                    hifenArgs.Add(" -epiconly"); //Only ASE
            //if (this.Administration.EventColorsChanceOverride != "") InterrogationArgs.Add($"?EventColorsChanceOverride=\"{this.Administration.EventColorsChanceOverride.ToString()}\"");
            //if (this.Administration.ExclusiveJoin) hifenArgs.Add(" -exclusivejoin");
            //if (this.Administration.ForceAllowCaveFlyers) hifenArgs.Add(" -ForceAllowCaveFlyers");
            if (Administration.EnableAutoForceRespawnDinos) hifenArgs.Add(" -ForceRespawnDinos");
            if (prf.Type.ServerType == EnumServerType.ArkSurviveAscended)
                if (Administration.ModIDs.Count > 0 && string.Join(",", Administration.ModIDs.ToArray()) != "")
                    hifenArgs.Add($" -mods={string.Join(",", Administration.ModIDs.ToArray())}");
            //if (this.Administration.imprintlimit) hifenArgs.Add(" -imprintlimit=101");
            if (Administration.DisableVac) hifenArgs.Add(" -insecure");
            //if (this.Administration.MapModID) hifenArgs.Add(" -MapModID=<ModID>");//Dont Use This
            //if (this.Administration.MaxNumOfSaveBackups) hifenArgs.Add($" -MaxNumOfSaveBackups={this.Administration.MaxNumOfSaveBackups}");
            //if (this.Administration.MapPlayerLocation != "") InterrogationArgs.Add($"?AltSaveDirectoryName=\"{(this.Administration.MapPlayerLocation ? "True" : "False")}\"");
            //if (this.Administration.MinimumTimeBetweenInventoryRetrieval) hifenArgs.Add($" -MinimumTimeBetweenInventoryRetrieval={this.Administration.MinimumTimeBetweenInventoryRetrieval}");
            if (Administration.LocalIp.Trim() != "") interrogationArgs.Add($"?MultiHome={Administration.LocalIp.Trim()}");
            if (Administration.DisableAntiSpeedHackDetection) hifenArgs.Add(" -noantispeedhack");
            if (!Administration.EnableBattleEye) hifenArgs.Add(" -NoBattlEye");
            if (Administration.DisablePlayerMovePhysics) hifenArgs.Add(" -nocombineclientmoves");
            if (Administration.NoDinos) hifenArgs.Add(" -nodinos");
            if (Administration.NoHandDetection) hifenArgs.Add(" -NoHangDetection");
            if (Administration.LogAdminCommandsToAdmins) hifenArgs.Add(" -NotifyAdminCommandsInChat");
            if (Administration.NoUnderMeshChecking) hifenArgs.Add(" -noundermeshchecking");
            if (Administration.NoUnderMeshKilling) hifenArgs.Add(" -noundermeshkilling");
            //if (this.Administration.PVEDisallowTribeWar) hifenArgs.Add(" -pvedisallowtribewar");
            //if (this.Administration.SecureSendArKPayload) hifenArgs.Add(" -SecureSendArKPayload");
            if (prf.Type.ServerType == EnumServerType.ArkSurviveAscended)
                if (Administration.ServerAllowAnsel)
                    hifenArgs.Add(" -ServerAllowAnsel");
            if (Administration.EnableServerAdminLogs) hifenArgs.Add(" -servergamelog");
            if (Administration.ServerAdminLogsIncludeTribeLogs) hifenArgs.Add(" -servergamelogincludetribelogs");
            if (Administration.ServerRconOutputTribeLogs) hifenArgs.Add(" -ServerRCONOutputTribeLogs");
            if (Administration.AntiSpeedHackBias != 0) hifenArgs.Add($" -speedhackbias={Administration.AntiSpeedHackBias}");
            if (Administration.StasisKeepController) hifenArgs.Add(" -StasisKeepControllers");
            if (Administration.StructureMemoryOptimizations) hifenArgs.Add(" -structurememopts");
            if (Administration.TotalConversionId != "") hifenArgs.Add($" -TotalConversionMod={Administration.TotalConversionId}");
            //if (this.Administration.UseDynamicConfig) hifenArgs.Add($" -UseDynamicConfig");
            //if (this.Administration.UseItemDupeCheck) hifenArgs.Add($" -UseItemDupeCheck");
            //if (this.Administration.UseSecureSpawnRules) hifenArgs.Add($" -UseSecureSpawnRules");
            //if (this.Administration.usestore) hifenArgs.Add($" -usestore");/Dont do this
            //if (this.Administration.UseStructureStasisGrid) hifenArgs.Add($" -UseStructureStasisGrid");
            if (Administration.EnableVivox) hifenArgs.Add(" -UseVivox");
            //if (this.Administration.webalarm) hifenArgs.Add($" -webalarm");
            if (prf.Type.ServerType == EnumServerType.ArkSurviveAscended)
                if (Administration.MaxPlayers != 0)
                    hifenArgs.Add($" -WinLiveMaxPlayers={Administration.MaxPlayers}");
            if (prf.Type.ServerType == EnumServerType.ArkSurviveEvolved)
                if (Administration.MaxPlayers != 0)
                    hifenArgs.Add($" -MaxPlayers={Administration.MaxPlayers}");
            if (prf.Type.ServerType == EnumServerType.ArkSurviveEvolved)
                if (Administration.ClusterDirectoryOverride)
                    hifenArgs.Add($" -ClusterDirOverride=\"{settings.DataFolder}\"");
            if (prf.Type.ServerType == EnumServerType.ArkSurviveEvolved)
                if (Administration.ClusterId != "")
                    hifenArgs.Add($" -clusterid=\"{Administration.ClusterId}\"");
            if (Administration.ForceDirectX10) hifenArgs.Add(" -dx10");
            if (Administration.ForceLowMemory) hifenArgs.Add(" -lowmemory");
            if (Administration.ForceNoManSky) hifenArgs.Add(" -nomansky");
            if (!Administration.UseNoMemoryBias) hifenArgs.Add(" -nomemorybias");
            if (Administration.UseCache) hifenArgs.Add(" -usecache");
            //if (this.Administration.PreventHibernation) hifenArgs.Add($" -PreventHibernation");

            interrogationArgs.Add($"?Port={Administration.ServerPort}");
            interrogationArgs.Add($"?QueryPort={Administration.QueryPort}");

            hifenArgs.Add(" -nosteamclient");
            hifenArgs.Add(" -game");
            hifenArgs.Add(" -server");
            hifenArgs.Add(" -log");

            hifenArgs.Add(" -ForceAllowCaveFlyers"); //Remove later
            //?AllowCrateSpawnsOnTopOfStructures=True 
            //-ForceAllowCaveFlyers
            //-NoTransferFromFiltering
            //-exclusivejoin

            // Olympus?listen?MultiHome=192.168.1.250?Port=8799?QueryPort=27036?MaxPlayers=50?AllowCrateSpawnsOnTopOfStructures=True 
            //-ActiveEvent=FearEvolved -ForceAllowCaveFlyers -EnableIdlePlayerKick -clusterid=OphiussaPVECluster01 -ClusterDirOverride="G:\asmdata" 
            //-NoTransferFromFiltering -NoBattlEye -forcerespawndinos -servergamelog -servergamelogincludetribelogs -ServerRCONOutputTribeLogs 
            //-usecache -NoHangDetection -exclusivejoin -nosteamclient -game -server -log
            cmd += Administration.MapName + string.Join("", interrogationArgs.ToArray()) + string.Join("", hifenArgs.ToArray());

            return cmd;
        }

        internal async Task<bool> SaveWorldRcon(Settings settings) {
            try {
                var rcon = new RCON(IPAddress.Parse(Administration.LocalIp), ushort.Parse(Administration.RconPort), Administration.ServerAdminPassword);
                await rcon.ConnectAsync();
                await rcon.SendCommandAsync($"Broadcast {settings.WorldSaveMessage}");
                await rcon.SendCommandAsync("saveworld");
                return true;
            }
            catch (Exception) {
                return false;
            }
        }
    }

    public class Administration : BaseAdministration {
        public bool   UseServerApi                     { get; set; } = false;
        public string ServerAdminPassword              { get; set; } = Membership.GeneratePassword(10, 6);
        public string ServerSpectatorPassword          { get; set; } = "";
        public string QueryPort                        { get; set; } = "27015";
        public bool   UseRcon                          { get; set; }
        public string RconPort                         { get; set; } = "32330";
        public int    RconServerLogBuffer              { get; set; } = 600;
        public string MapName                          { get; set; } = "";
        public string TotalConversionId                { get; set; } = "";
        public int    AutoSavePeriod                   { get; set; } = 15;
        public string Mod                              { get; set; } = "";
        public int    ModDuration                      { get; set; } = 20;
        public bool   EnableInterval                   { get; set; } = true;
        public int    ModInterval                      { get; set; } = 60;
        public string Branch                           { get; set; } = "Live";
        public bool   EnablIdleTimeOut                 { get; set; } = true;
        public int    IdleTimout                       { get; set; } = 3600;
        public bool   UseBanListUrl                    { get; set; } = false;
        public string BanListUrl                       { get; set; } = "http://arkdedicated.com/banlist.txt";
        public bool   DisableVac                       { get; set; } = false;
        public bool   EnableBattleEye                  { get; set; } = false;
        public bool   DisablePlayerMovePhysics         { get; set; } = false;
        public bool   OutputLogToConsole               { get; set; } = true;
        public bool   UseAllCores                      { get; set; } = false;
        public bool   UseCache                         { get; set; } = false;
        public bool   NoHandDetection                  { get; set; } = false;
        public bool   NoDinos                          { get; set; } = false;
        public bool   NoUnderMeshChecking              { get; set; } = false;
        public bool   NoUnderMeshKilling               { get; set; } = false;
        public bool   EnableVivox                      { get; set; } = false;
        public bool   AllowSharedConnections           { get; set; } = true;
        public bool   RespawnDinosOnStartUp            { get; set; } = false;
        public bool   EnableAutoForceRespawnDinos      { get; set; } = false;
        public int    AutoForceRespawnDinosInterval    { get; set; } = 24;
        public bool   DisableAntiSpeedHackDetection    { get; set; } = false;
        public int    AntiSpeedHackBias                { get; set; } = 1;
        public bool   ForceDirectX10                   { get; set; } = false;
        public bool   ForceLowMemory                   { get; set; } = false;
        public bool   ForceNoManSky                    { get; set; } = false;
        public bool   UseNoMemoryBias                  { get; set; } = false;
        public bool   StasisKeepController             { get; set; } = false;
        public bool   ServerAllowAnsel                 { get; set; } = false;
        public bool   StructureMemoryOptimizations     { get; set; } = false;
        public bool   EnableCrossPlay                  { get; set; } = false;
        public bool   EnablePublicIpForEpic            { get; set; } = false;
        public bool   EpicStorePlayersOnly             { get; set; } = false;
        public string AlternateSaveDirectoryName       { get; set; } = "";
        public string ClusterId                        { get; set; } = "";
        public bool   ClusterDirectoryOverride         { get; set; } = false;
        public bool   EnableServerAdminLogs            { get; set; } = true;
        public bool   ServerAdminLogsIncludeTribeLogs  { get; set; } = true;
        public bool   ServerRconOutputTribeLogs        { get; set; } = true;
        public bool   AllowHideDamageSourceFromLogs    { get; set; } = true;
        public int    MaximumTribeLogs                 { get; set; } = 100;
        public bool   LogAdminCommandsToPublic         { get; set; }
        public bool   LogAdminCommandsToAdmins         { get; set; } = false;
        public bool   TribeLogDestroyedEnemyStructures { get; set; } = true;
    }

    public class Rules {
        public bool   EnableHardcoreMode                       { get; set; }
        public bool   DisablePveFriendlyFire                   { get; set; }
        public bool   DisablePvpFriendlyFire                   { get; set; }
        public bool   PreventBuildingInResourceRichAreas       { get; set; }
        public bool   DisableSupplyCrates                      { get; set; }
        public bool   EnablePvp                                { get; set; }
        public bool   EnablePveCaveBuilding                    { get; set; }
        public bool   EnablePvpCaveBuilding                    { get; set; } = true;
        public bool   EnableSinglePlayerSettings               { get; set; }
        public bool   AllowCrateSpawnsOnTopOfStructures        { get; set; } = true;
        public bool   EnableCreativeMode                       { get; set; } = false;
        public bool   EnablePveCryoSickness                    { get; set; } = false;
        public bool   DisablePvpRailGun                        { get; set; } = false;
        public bool   DisableCostumTributeFolders              { get; set; } = false;
        public bool   RandomSupplyCratePoints                  { get; set; } = false;
        public float  SupplyCrateLootQualityMultiplier         { get; set; } = 1;
        public float  FishingLootQualityMultiplier             { get; set; } = 1;
        public bool   UseCorpseLocation                        { get; set; } = false;
        public bool   PreventSpawnAnimations                   { get; set; } = false;
        public bool   AllowUnlimitedRespecs                    { get; set; } = false;
        public bool   AllowPlatformSaddleMultiFloors           { get; set; } = false;
        public float  PlatformSaddleBuildAreaBoundsMultiplier  { get; set; } = 1;
        public int    MaxGatewaysOnSaddles                     { get; set; } = 2;
        public bool   EnableDifficultOverride                  { get; set; } = false;
        public int    MaxDinoLevel                             { get; set; } = 120;
        public float  DifficultyOffset                         { get; set; } = 1;
        public int    DestroyTamesOverLevel                    { get; set; } = 0;
        public bool   EnableTributeDownloads                   { get; set; } = false;
        public bool   NoSurvivorDownloads                      { get; set; } = true;
        public bool   NoItemDownloads                          { get; set; } = true;
        public bool   NoDinoDownloads                          { get; set; } = true;
        public bool   AllowForeignDinoDownloads                { get; set; } = false;
        public bool   NoSurvivorUploads                        { get; set; } = true;
        public bool   NoItemUploads                            { get; set; } = true;
        public bool   NoDinoUploads                            { get; set; } = true;
        public bool   LimitMaxTributeDinos                     { get; set; } = false;
        public int    MaxTributeDinos                          { get; set; } = 0;
        public bool   LimitTributeItems                        { get; set; } = false;
        public int    MaxTributeItems                          { get; set; } = 0;
        public bool   NoTransferFromFiltering                  { get; set; } = false;
        public bool   OverrideSurvivorUploadExpiration         { get; set; } = false;
        public int    OverrideSurvivorUploadExpirationValue    { get; set; } = 1440;
        public bool   OverrideItemUploadExpiration             { get; set; } = false;
        public int    OverrideItemUploadExpirationValue        { get; set; } = 1440;
        public bool   OverrideDinoUploadExpiration             { get; set; } = false;
        public int    OverrideDinoUploadExpirationValue        { get; set; } = 1440;
        public bool   OverrideMinimumDinoReUploadInterval      { get; set; } = false;
        public int    OverrideMinimumDinoReUploadIntervalValue { get; set; } = 720;
        public bool   PveSchedule                              { get; set; } = false;
        public bool   UseServerTime                            { get; set; } = false;
        public string PvpStartTime                             { get; set; } = "0000";
        public string PvpEndTime                               { get; set; } = "0000";
        public bool   PreventOfflinePvp                        { get; set; } = false;
        public int    LogoutInterval                           { get; set; } = 900;
        public int    ConnectionInvicibleInterval              { get; set; } = 5;
        public bool   IncreasePvpRespawnInterval               { get; set; } = false;
        public int    IntervalCheckPeriod                      { get; set; } = 300;
        public float  IntervalMultiplier                       { get; set; } = 1;
        public int    IntervalBaseAmount                       { get; set; } = 60;
        public int    MaxPlayersInTribe                        { get; set; } = 70;
        public int    TribeNameChangeCooldDown                 { get; set; } = 15;
        public int    TribeSlotReuseCooldown                   { get; set; } = 0;
        public bool   AllowTribeAlliances                      { get; set; } = true;
        public int    MaxAlliancesPerTribe                     { get; set; } = 10;
        public int    MaxTribesPerAlliance                     { get; set; } = 10;
        public bool   AllowTribeWarfare                        { get; set; } = true;
        public bool   AllowCancelingTribeWarfare               { get; set; } = false;
        public bool   AllowCostumRecipes                       { get; set; } = true;
        public float  CostumRecipesEffectivenessMultiplier     { get; set; } = 1;
        public float  CostumRecipesSkillMultiplier             { get; set; } = 1;
        public bool   EnableDiseases                           { get; set; } = true;
        public bool   NonPermanentDiseases                     { get; set; } = false;
        public bool   OverrideNpcNetworkStasisRangeScale       { get; set; } = false;
        public int    OnlinePlayerCountStart                   { get; set; } = 70;
        public int    OnlinePlayerCountEnd                     { get; set; } = 120;
        public float  ScaleMaximum                             { get; set; } = 0.5f;
        public float  OxygenSwimSpeedStatMultiplier            { get; set; } = 1;
        public float  UseCorpseLifeSpanMultiplier              { get; set; } = 1;
        public int    FjordhawkInventoryCooldown               { get; set; } = 3600;
        public float  GlobalPoweredBatteryDurability           { get; set; } = 1;
        public float  FuelConsumptionIntervalMultiplier        { get; set; } = 1;
        public int    LimitNonPlayerDroppedItemsRange          { get; set; } = 0;
        public int    LimitNonPlayerDroppedItemsCount          { get; set; } = 0;
        public bool   EnableCryopodNerf                        { get; set; } = false;
        public int    EnableCryopodNerfDuration                { get; set; } = 10;
        public float  OutgoingDamageMultiplier                 { get; set; } = 1;
        public float  IncomingDamageMultiplierPercent          { get; set; } = 0;
        public bool   Gen1DisableMissions                      { get; set; } = false;
        public bool   Gen1AllowTekSuitPowers                   { get; set; } = false;
        public bool   Gen2DisableTekSuitonSpawn                { get; set; } = false;
        public bool   Gen2DisableWorldBuffs                    { get; set; } = false;
        public bool   EnableWorldBuffScaling                   { get; set; } = false;
        public float  WorldBuffScanlingEfficacy                { get; set; } = 1;
        public float  MutagemSpawnDelayMultiplier              { get; set; } = 1;
        public bool   DisableHexagonStore                      { get; set; } = false;
        public bool   AllowOnlyEngramPointsTrade               { get; set; } = false;
        public int    MaxHexagonsPerCharacter                  { get; set; } = 2000000000;
        public float  HexagonRewardMultiplier                  { get; set; } = 1;
        public float  HexagonCostMultiplier                    { get; set; } = 1;
        public bool   EnableRagnarokSettings                   { get; set; } = false;
        public bool   AllowMultipleTamedUnicorns               { get; set; } = false;
        public int    UnicornSpawnInterval                     { get; set; } = 24;
        public bool   EnableVolcano                            { get; set; } = true;
        public float  VolcanoInterval                          { get; set; } = 1;
        public float  VolcanoIntensity                         { get; set; } = 1;
        public bool   EnableFjordurSettings                    { get; set; } = false;
        public bool   EnableFjordurBiomeTeleport               { get; set; } = true;
        public bool   EnableGenericQualityClamp                { get; set; } = false;
        public int    GenericQualityClamp                      { get; set; } = 0;
        public bool   EnableArmorClamp                         { get; set; } = false;
        public int    ArmorClamp                               { get; set; } = 0;
        public bool   EnableWeaponDamagePercentClamp           { get; set; } = false;
        public int    WeaponDamagePercentClamp                 { get; set; } = 0;
        public bool   EnableHypoInsulationClamp                { get; set; } = false;
        public int    HypoInsulationClamp                      { get; set; } = 0;
        public bool   EnableWeightClamp                        { get; set; } = false;
        public int    WeightClamp                              { get; set; } = 0;
        public bool   EnableMaxDurabilityClamp                 { get; set; } = false;
        public int    MaxDurabilityClamp                       { get; set; } = 0;
        public bool   EnableWeaponClipAmmoClamp                { get; set; } = false;
        public int    WeaponClipAmmoClamp                      { get; set; } = 0;
        public bool   EnableHyperInsulationClamp               { get; set; } = false;
        public int    HyperInsulationClamp                     { get; set; } = 0;
    }
}