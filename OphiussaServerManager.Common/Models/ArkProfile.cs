using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security;
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
            Administration.Motd                    = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusMessageOfTheDay).ReadStringValue("Message", Administration.Motd);
            Administration.ModDuration             = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusMessageOfTheDay).ReadIntValue("Duration", Administration.ModDuration);

            Rules.EnableHardcoreMode                       = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("ServerHardcore", Rules.EnableHardcoreMode);
            Rules.DisablePveFriendlyFire                   = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadBoolValue("bPvEDisableFriendlyFire", Rules.DisablePveFriendlyFire);
            Rules.DisablePvpFriendlyFire                   = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadBoolValue("bDisableFriendlyFire",    Rules.DisablePvpFriendlyFire);
            Rules.PreventBuildingInResourceRichAreas       = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("EnableExtraStructurePreventionVolumes", Rules.PreventBuildingInResourceRichAreas);
            Rules.DisableSupplyCrates                      = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadBoolValue("bDisableLootCrates", Rules.DisableSupplyCrates);
            Rules.EnablePvp                                = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("serverPVE",            Rules.EnablePvp);
            Rules.EnablePveCaveBuilding                    = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("AllowCaveBuildingPvE", Rules.EnablePveCaveBuilding);
            Rules.EnablePvpCaveBuilding                    = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("AllowCaveBuildingPvP", Rules.EnablePvpCaveBuilding);
            Rules.EnableSinglePlayerSettings               = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadBoolValue("bUseSingleplayerSettings", Rules.EnableSinglePlayerSettings);
            Rules.AllowCrateSpawnsOnTopOfStructures        = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("AllowCrateSpawnsOnTopOfStructures", Rules.AllowCrateSpawnsOnTopOfStructures);
            Rules.EnableCreativeMode                       = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadBoolValue("bShowCreativeMode", Rules.EnableCreativeMode);
            Rules.EnablePveCryoSickness                    = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("EnableCryoSicknessPVE",   Rules.EnablePveCryoSickness);
            Rules.RandomSupplyCratePoints                  = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("RandomSupplyCratePoints", Rules.RandomSupplyCratePoints);
            Rules.SupplyCrateLootQualityMultiplier         = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadFloatValue("SupplyCrateLootQualityMultiplier", Rules.SupplyCrateLootQualityMultiplier);
            Rules.FishingLootQualityMultiplier             = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadFloatValue("FishingLootQualityMultiplier",     Rules.FishingLootQualityMultiplier);
            Rules.UseCorpseLocation                        = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadBoolValue("bUseCorpseLocator", Rules.UseCorpseLocation);
            Rules.PreventSpawnAnimations                   = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("PreventSpawnAnimations", Rules.PreventSpawnAnimations);
            Rules.AllowUnlimitedRespecs                    = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadBoolValue("bAllowUnlimitedRespecs",          Rules.AllowUnlimitedRespecs);
            Rules.AllowPlatformSaddleMultiFloors           = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadBoolValue("bAllowPlatformSaddleMultiFloors", Rules.AllowPlatformSaddleMultiFloors);
            Rules.PlatformSaddleBuildAreaBoundsMultiplier  = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadFloatValue("PlatformSaddleBuildAreaBoundsMultiplier", Rules.PlatformSaddleBuildAreaBoundsMultiplier);
            Rules.MaxGatewaysOnSaddles                     = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadIntValue("MaxGateFrameOnSaddles", Rules.MaxGatewaysOnSaddles);
            Rules.DifficultyOffset                         = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadFloatValue("DifficultyOffset", Rules.DifficultyOffset);
            Rules.MaxDinoLevel                             = OfficialDifficultyValueConverter.Convert(systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadIntValue("OverrideOfficialDifficulty", Rules.MaxDinoLevel));
            Rules.EnableDifficultOverride                  = (Rules.MaxDinoLevel != 120 || Rules.DifficultyOffset != 1f);
            Rules.DestroyTamesOverLevel                    = systemIniFile.ReadSection(IniFiles.Game, IniSections.GameShooterGameMode).ReadIntValue("DestroyTamesOverLevelClamp", Rules.DestroyTamesOverLevel);
            Rules.EnableTributeDownloads                   = !systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("noTributeDownloads", Rules.EnableTributeDownloads);
            Rules.NoSurvivorDownloads                      = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("PreventDownloadSurvivors",          Rules.NoSurvivorDownloads);
            Rules.NoItemDownloads                          = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("PreventDownloadItems",              Rules.NoItemDownloads);
            Rules.NoDinoDownloads                          = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("PreventDownloadDinos",              Rules.NoDinoDownloads);
            Rules.AllowForeignDinoDownloads                = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("CrossARKAllowForeignDinoDownloads", Rules.AllowForeignDinoDownloads);
            Rules.NoSurvivorUploads                        = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("PreventUploadSurvivors",            Rules.NoSurvivorUploads);
            Rules.NoItemUploads                            = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("PreventUploadItems",                Rules.NoItemUploads);
            Rules.NoDinoUploads                            = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("PreventUploadDinos",                Rules.NoDinoUploads);
            Rules.MaxTributeDinos                          = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadIntValue("MaxTributeDinos", Rules.MaxTributeDinos);
            Rules.LimitMaxTributeDinos                     = Rules.MaxTributeDinos != 20;
            Rules.MaxTributeItems                          = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadIntValue("MaxTributeItems", Rules.MaxTributeItems);
            Rules.LimitTributeItems                        = Rules.MaxTributeDinos != 50;
            Rules.OverrideSurvivorUploadExpirationValue    = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadIntValue("TributeCharacterExpirationSeconds", Rules.OverrideSurvivorUploadExpirationValue);
            Rules.OverrideDinoUploadExpirationValue        = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadIntValue("TributeDinoExpirationSeconds",      Rules.OverrideDinoUploadExpirationValue);
            Rules.OverrideItemUploadExpirationValue        = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadIntValue("TributeItemExpirationSeconds",      Rules.OverrideItemUploadExpirationValue);
            Rules.OverrideMinimumDinoReUploadIntervalValue = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadIntValue("MinimumDinoReuploadInterval",       Rules.OverrideMinimumDinoReUploadIntervalValue);
            Rules.OverrideSurvivorUploadExpiration         = Rules.OverrideSurvivorUploadExpirationValue    != 86400 || Rules.OverrideSurvivorUploadExpirationValue    == 0;
            Rules.OverrideDinoUploadExpiration             = Rules.OverrideDinoUploadExpirationValue        != 86400 || Rules.OverrideDinoUploadExpirationValue        == 0;
            Rules.OverrideItemUploadExpiration             = Rules.OverrideItemUploadExpirationValue        != 86400 || Rules.OverrideItemUploadExpirationValue        == 0;
            Rules.OverrideMinimumDinoReUploadInterval      = Rules.OverrideMinimumDinoReUploadIntervalValue != 43200 || Rules.OverrideMinimumDinoReUploadIntervalValue == 0;
            Rules.PveSchedule                              = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadBoolValue("bAutoPvETimer",         Rules.PveSchedule);
            Rules.UseServerTime                            = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadBoolValue("bAutoPvEUseSystemTime", Rules.UseServerTime);
            Rules.PvpStartTime                             = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadIntValue("AutoPvEStartTimeSeconds", Rules.PvpStartTime.ConvertHourToSeconds()).ConvertSecondsToHour();
            Rules.PvpEndTime                               = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadIntValue("AutoPvEStopTimeSeconds",  Rules.PvpEndTime.ConvertHourToSeconds()).ConvertSecondsToHour();
            Rules.PreventOfflinePvp                        = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("PreventOfflinePvP", Rules.PreventOfflinePvp);
            Rules.LogoutInterval                           = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadIntValue("PreventOfflinePvPInterval",                     Rules.LogoutInterval);
            Rules.ConnectionInvicibleInterval              = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadIntValue("PreventOfflinePvPConnectionInvincibleInterval", Rules.ConnectionInvicibleInterval);
            Rules.IncreasePvpRespawnInterval               = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadBoolValue("bIncreasePvPRespawnInterval", Rules.IncreasePvpRespawnInterval);
            Rules.IntervalBaseAmount                       = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadIntValue("IncreasePvPRespawnIntervalBaseAmount",  Rules.IntervalBaseAmount);
            Rules.IntervalCheckPeriod                      = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadIntValue("IncreasePvPRespawnIntervalCheckPeriod", Rules.IntervalCheckPeriod);
            Rules.IntervalMultiplier                       = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadFloatValue("IncreasePvPRespawnIntervalMultiplier", Rules.IntervalMultiplier);
            Rules.MaxPlayersInTribe                        = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadIntValue("MaxNumberOfPlayersInTribe", Rules.MaxPlayersInTribe);
            Rules.TribeNameChangeCooldDown                 = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadIntValue("TribeNameChangeCooldown", Rules.TribeNameChangeCooldDown);
            Rules.TribeSlotReuseCooldown                   = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadIntValue("TribeSlotReuseCooldown", Rules.TribeSlotReuseCooldown);
            Rules.AllowTribeAlliances                      = !systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("PreventTribeAlliances", Rules.AllowTribeAlliances);
            Rules.MaxAlliancesPerTribe                     = systemIniFile.ReadSection(IniFiles.Game, IniSections.GameShooterGameMode).ReadIntValue("MaxAlliancesPerTribe", Rules.MaxAlliancesPerTribe);
            Rules.MaxTribesPerAlliance                     = systemIniFile.ReadSection(IniFiles.Game, IniSections.GameShooterGameMode).ReadIntValue("MaxTribesPerAlliance", Rules.MaxTribesPerAlliance);
            Rules.AllowTribeWarfare                        = systemIniFile.ReadSection(IniFiles.Game, IniSections.GameShooterGameMode).ReadBoolValue("bPvEAllowTribeWar",       Rules.AllowTribeWarfare);
            Rules.AllowCancelingTribeWarfare               = systemIniFile.ReadSection(IniFiles.Game, IniSections.GameShooterGameMode).ReadBoolValue("bPvEAllowTribeWarCancel", Rules.AllowCancelingTribeWarfare);
            Rules.AllowCostumRecipes                       = systemIniFile.ReadSection(IniFiles.Game, IniSections.GameShooterGameMode).ReadBoolValue("bAllowCustomRecipes",     Rules.AllowCostumRecipes);
            Rules.CostumRecipesEffectivenessMultiplier     = systemIniFile.ReadSection(IniFiles.Game, IniSections.GameShooterGameMode).ReadFloatValue("CustomRecipeEffectivenessMultiplier", Rules.CostumRecipesEffectivenessMultiplier);
            Rules.CostumRecipesSkillMultiplier             = systemIniFile.ReadSection(IniFiles.Game, IniSections.GameShooterGameMode).ReadFloatValue("CustomRecipeSkillMultiplier",         Rules.CostumRecipesSkillMultiplier);
            Rules.EnableDiseases                           = !systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("PreventDiseases", Rules.EnableDiseases);
            Rules.NonPermanentDiseases                     = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("NonPermanentDiseases", Rules.NonPermanentDiseases);
            Rules.OnlinePlayerCountStart                   = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadIntValue("NPCNetworkStasisRangeScalePlayerCountStart", Rules.OnlinePlayerCountStart);
            Rules.OnlinePlayerCountEnd                     = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadIntValue("NPCNetworkStasisRangeScalePlayerCountEnd",   Rules.OnlinePlayerCountEnd);
            Rules.ScaleMaximum                             = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadFloatValue("NPCNetworkStasisRangeScalePercentEnd", Rules.ScaleMaximum);
            Rules.OverrideNpcNetworkStasisRangeScale       = (Rules.OnlinePlayerCountStart != 0 && Rules.OnlinePlayerCountStart != 70) && (Rules.OnlinePlayerCountEnd != 0 && Rules.OnlinePlayerCountEnd != 120) && (Math.Round(Rules.ScaleMaximum, 2) != 0.5f && Rules.ScaleMaximum != 0.5f);
            Rules.OxygenSwimSpeedStatMultiplier            = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadFloatValue("OxygenSwimSpeedStatMultiplier", Rules.OxygenSwimSpeedStatMultiplier);
            Rules.UseCorpseLifeSpanMultiplier              = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadFloatValue("UseCorpseLifeSpanMultiplier",                     Rules.UseCorpseLifeSpanMultiplier);
            Rules.GlobalPoweredBatteryDurability           = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadFloatValue("GlobalPoweredBatteryDurabilityDecreasePerSecond", Rules.GlobalPoweredBatteryDurability);
            Rules.FuelConsumptionIntervalMultiplier        = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadFloatValue("FuelConsumptionIntervalMultiplier",               Rules.FuelConsumptionIntervalMultiplier);
            Rules.LimitNonPlayerDroppedItemsRange          = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadIntValue("LimitNonPlayerDroppedItemsRange", Rules.LimitNonPlayerDroppedItemsRange);
            Rules.LimitNonPlayerDroppedItemsCount          = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadIntValue("LimitNonPlayerDroppedItemsCount", Rules.LimitNonPlayerDroppedItemsCount);
            Rules.EnableCryopodNerf                        = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("EnableCryopodNerf", Rules.EnableCryopodNerf);
            Rules.EnableCryopodNerfDuration                = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadIntValue("CryopodNerfDuration", Rules.EnableCryopodNerfDuration);
            Rules.IncomingDamageMultiplierPercent          = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadFloatValue("CryopodNerfIncomingDamageMultPercent", Rules.IncomingDamageMultiplierPercent);
            Rules.OutgoingDamageMultiplier                 = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadFloatValue("CryopodNerfDamageMult",                Rules.OutgoingDamageMultiplier);
            Rules.Gen1DisableMissions                      = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadBoolValue("bDisableGenesisMissions", Rules.Gen1DisableMissions);
            Rules.Gen1AllowTekSuitPowers                   = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("AllowTekSuitPowersInGenesis", Rules.Gen1AllowTekSuitPowers);
            Rules.Gen2DisableTekSuitonSpawn                = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadBoolValue("bDisableDefaultMapItemSets", Rules.Gen2DisableTekSuitonSpawn);
            Rules.Gen2DisableWorldBuffs                    = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadBoolValue("bDisableWorldBuffs",         Rules.Gen2DisableWorldBuffs);
            Rules.EnableWorldBuffScaling                   = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadBoolValue("bEnableWorldBuffScaling",    Rules.EnableWorldBuffScaling);
            Rules.WorldBuffScanlingEfficacy                = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadFloatValue("WorldBuffScalingEfficacy",              Rules.WorldBuffScanlingEfficacy);
            Rules.MutagemSpawnDelayMultiplier              = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadFloatValue("AdjustableMutagenSpawnDelayMultiplier", Rules.MutagemSpawnDelayMultiplier);
            Rules.DisableHexagonStore                      = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadBoolValue("bDisableHexagonStore",                Rules.DisableHexagonStore);
            Rules.AllowOnlyEngramPointsTrade               = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadBoolValue("bHexStoreAllowOnlyEngramTradeOption", Rules.AllowOnlyEngramPointsTrade);
            Rules.MaxHexagonsPerCharacter                  = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadIntValue("MaxHexagonsPerCharacter", Rules.MaxHexagonsPerCharacter);
            Rules.HexagonRewardMultiplier                  = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadFloatValue("BaseHexagonRewardMultiplier", Rules.HexagonRewardMultiplier);
            Rules.HexagonCostMultiplier                    = systemIniFile.ReadSection(IniFiles.Game,             IniSections.GameShooterGameMode).ReadFloatValue("HexagonCostMultiplier",       Rules.HexagonCostMultiplier);
            Rules.AllowMultipleTamedUnicorns               = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusRagnarok).ReadBoolValue("AllowMultipleTamedUnicorns", Rules.AllowMultipleTamedUnicorns);
            Rules.UnicornSpawnInterval                     = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusRagnarok).ReadIntValue("HexagonCostMultiplier", Rules.UnicornSpawnInterval);
            Rules.EnableVolcano                            = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusRagnarok).ReadBoolValue("EnableVolcano", Rules.EnableVolcano);
            Rules.VolcanoInterval                          = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusRagnarok).ReadFloatValue("VolcanoInterval",  Rules.VolcanoInterval);
            Rules.VolcanoIntensity                         = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusRagnarok).ReadFloatValue("VolcanoIntensity", Rules.VolcanoIntensity);
            Rules.EnableRagnarokSettings                   = (Rules.AllowMultipleTamedUnicorns || Rules.UnicornSpawnInterval != 24 || !Rules.EnableVolcano || Rules.VolcanoInterval != 1f || Rules.VolcanoIntensity != 1f);
            Rules.EnableFjordurBiomeTeleport               = systemIniFile.ReadSection(IniFiles.GameUserSettings, IniSections.GusServerSettings).ReadBoolValue("UseFjordurTraversalBuff", Rules.EnableFjordurBiomeTeleport);
            Rules.EnableFjordurSettings                    = !Rules.EnableFjordurBiomeTeleport;
            Rules.GenericQualityClamp                      = systemIniFile.ReadSection(IniFiles.Game, IniSections.GameShooterGameMode).ReadIntValue("ItemStatClamps[0]", Rules.GenericQualityClamp);
            Rules.EnableGenericQualityClamp                = Rules.GenericQualityClamp != 0;
            Rules.ArmorClamp                               = systemIniFile.ReadSection(IniFiles.Game, IniSections.GameShooterGameMode).ReadIntValue("ItemStatClamps[1]", Rules.ArmorClamp);
            Rules.EnableArmorClamp                         = Rules.ArmorClamp != 0;
            Rules.MaxDurabilityClamp                       = systemIniFile.ReadSection(IniFiles.Game, IniSections.GameShooterGameMode).ReadIntValue("ItemStatClamps[2]", Rules.MaxDurabilityClamp);
            Rules.EnableMaxDurabilityClamp                 = Rules.MaxDurabilityClamp != 0;
            Rules.WeaponDamagePercentClamp                 = systemIniFile.ReadSection(IniFiles.Game, IniSections.GameShooterGameMode).ReadIntValue("ItemStatClamps[3]", Rules.WeaponDamagePercentClamp);
            Rules.EnableWeaponDamagePercentClamp           = Rules.WeaponDamagePercentClamp != 0;
            Rules.WeaponClipAmmoClamp                      = systemIniFile.ReadSection(IniFiles.Game, IniSections.GameShooterGameMode).ReadIntValue("ItemStatClamps[4]", Rules.WeaponClipAmmoClamp);
            Rules.EnableWeaponClipAmmoClamp                = Rules.WeaponClipAmmoClamp != 0;
            Rules.HypoInsulationClamp                      = systemIniFile.ReadSection(IniFiles.Game, IniSections.GameShooterGameMode).ReadIntValue("ItemStatClamps[5]", Rules.HypoInsulationClamp);
            Rules.EnableHypoInsulationClamp                = Rules.HypoInsulationClamp != 0;
            Rules.WeightClamp                              = systemIniFile.ReadSection(IniFiles.Game, IniSections.GameShooterGameMode).ReadIntValue("ItemStatClamps[6]", Rules.WeightClamp);
            Rules.EnableWeightClamp                        = Rules.WeightClamp != 0;
            Rules.HyperInsulationClamp                     = systemIniFile.ReadSection(IniFiles.Game, IniSections.GameShooterGameMode).ReadIntValue("ItemStatClamps[7]", Rules.HyperInsulationClamp);
            Rules.EnableHyperInsulationClamp               = Rules.HyperInsulationClamp != 0;

            return this;
        }

        internal void SaveGameIni(Profile profile) {
            Administration defaultAdministration = new Administration();
            Rules          defaultRules          = new Rules();

            var systemIniFile = new SystemIniFile(profile.InstallLocation);

            var GUSiniSections      = systemIniFile.GetAllSections(IniFiles.GameUserSettings);
            var GUSlistSectionNames = new List<string>();
            var GUSkeyValuePairs    = new Dictionary<string, List<ConfigFile>>();
            foreach (var section in GUSiniSections) {
                GUSkeyValuePairs.Add(section.SectionName, systemIniFile.ReadSection(IniFiles.GameUserSettings, section.SectionName).ToListConfigFile());
                GUSlistSectionNames.Add(section.SectionName);
            }

            var GameiniSections      = systemIniFile.GetAllSections(IniFiles.Game);
            var GamelistSectionNames = new List<string>();
            var GamekeyValuePairs    = new Dictionary<string, List<ConfigFile>>();
            foreach (var section in GameiniSections) {
                GamekeyValuePairs.Add(section.SectionName, systemIniFile.ReadSection(IniFiles.Game, section.SectionName).ToListConfigFile());
                GamelistSectionNames.Add(section.SectionName);
            }


            if (GUSkeyValuePairs.Count == 0) return; //TODO:REmove this when all settings is implementend

            if (!GUSkeyValuePairs.ContainsKey("MessageOfTheDay")) GUSkeyValuePairs.Add("MessageOfTheDay",                                                         systemIniFile.ReadSection(IniFiles.GameUserSettings, "MessageOfTheDay").ToListConfigFile());
            if (!GUSkeyValuePairs.ContainsKey("ServerSettings")) GUSkeyValuePairs.Add("ServerSettings",                                                           systemIniFile.ReadSection(IniFiles.GameUserSettings, "ServerSettings").ToListConfigFile());
            if (!GUSkeyValuePairs.ContainsKey("ScalabilityGroups")) GUSkeyValuePairs.Add("ScalabilityGroups",                                                     systemIniFile.ReadSection(IniFiles.GameUserSettings, "ScalabilityGroups").ToListConfigFile());
            if (!GUSkeyValuePairs.ContainsKey("/Script/ShooterGame.ShooterGameUserSettings")) GUSkeyValuePairs.Add("/Script/ShooterGame.ShooterGameUserSettings", systemIniFile.ReadSection(IniFiles.GameUserSettings, "/Script/ShooterGame.ShooterGameUserSettings").ToListConfigFile());
            if (!GUSkeyValuePairs.ContainsKey("/Script/Engine.GameUserSettings")) GUSkeyValuePairs.Add("/Script/Engine.GameUserSettings",                         systemIniFile.ReadSection(IniFiles.GameUserSettings, "/Script/Engine.GameUserSettings").ToListConfigFile());
            if (!GUSkeyValuePairs.ContainsKey("SessionSettings")) GUSkeyValuePairs.Add("SessionSettings",                                                         systemIniFile.ReadSection(IniFiles.GameUserSettings, "SessionSettings").ToListConfigFile());
            if (!GUSkeyValuePairs.ContainsKey("/Script/Engine.GameSession")) GUSkeyValuePairs.Add("/Script/Engine.GameSession",                                   systemIniFile.ReadSection(IniFiles.GameUserSettings, "/Script/Engine.GameSession").ToListConfigFile());

            if (!GamekeyValuePairs.ContainsKey("/script/shootergame.shootergamemode")) GamekeyValuePairs.Add("/script/shootergame.shootergamemode", systemIniFile.ReadSection(IniFiles.Game, "/script/shootergame.shootergamemode").ToListConfigFile());

            //TODO:[MultiHome]
            //MultiHome = True //this value depens if in networking we select the "Left Server choose" or if we select an IP
//also check the Multihome propertie in the Session Settings
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("AdminLogging",                     Administration.LogAdminCommandsToPublic,         Administration.GetType().GetAllAttributes("LogAdminCommandsToPublic"));
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("AllowHideDamageSourceFromLogs",    Administration.AllowHideDamageSourceFromLogs,    Administration.GetType().GetAllAttributes("AllowHideDamageSourceFromLogs"));
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("TribeLogDestroyedEnemyStructures", Administration.TribeLogDestroyedEnemyStructures, Administration.GetType().GetAllAttributes("TribeLogDestroyedEnemyStructures"));
            GUSkeyValuePairs["ServerSettings"].WriteIntValue("KickIdlePlayersPeriod", Administration.IdleTimout, Administration.GetType().GetAllAttributes("IdleTimout"), Administration.EnablIdleTimeOut);

            //if (profile.Type.ServerType == EnumServerType.ArkSurviveEvolved)
            //    GUSkeyValuePairs["ServerSettings"].WriteIntValue("MaxPlayers", Administration.MaxPlayers, Administration.GetType().GetAllAttributes("MaxPlayers"));

            //if (profile.Type.ServerType == EnumServerType.ArkSurviveAscended)
            GUSkeyValuePairs["/Script/Engine.GameSession"].WriteIntValue("MaxPlayers", Administration.MaxPlayers, Administration.GetType().GetAllAttributes("MaxPlayers"));

            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("RCONEnabled", Administration.UseRcon, Administration.GetType().GetAllAttributes("UseRcon"));
            GUSkeyValuePairs["ServerSettings"].WriteStringValue("RCONPort", Administration.RconPort, Administration.GetType().GetAllAttributes("RconPort"));
            GUSkeyValuePairs["ServerSettings"].WriteIntValue("RCONServerGameLogBuffer", Administration.RconServerLogBuffer, Administration.GetType().GetAllAttributes("RconServerLogBuffer"));
            GUSkeyValuePairs["ServerSettings"].WriteStringValue("ServerAdminPassword", Administration.ServerAdminPassword, Administration.GetType().GetAllAttributes("ServerAdminPassword"));
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("servergamelog", Administration.EnableServerAdminLogs, Administration.GetType().GetAllAttributes("EnableServerAdminLogs"));
            GUSkeyValuePairs["ServerSettings"].WriteStringValue("ServerPassword",    Administration.ServerPassword,          Administration.GetType().GetAllAttributes("ServerPassword"));
            GUSkeyValuePairs["ServerSettings"].WriteStringValue("SpectatorPassword", Administration.ServerSpectatorPassword, Administration.GetType().GetAllAttributes("ServerSpectatorPassword"));
            GUSkeyValuePairs["SessionSettings"].WriteStringValue("SessionName", Administration.ServerName, Administration.GetType().GetAllAttributes("ServerName"));
            GUSkeyValuePairs["SessionSettings"].WriteStringValue("Port",        Administration.ServerPort, Administration.GetType().GetAllAttributes("ServerPort"));
            GUSkeyValuePairs["SessionSettings"].WriteStringValue("QueryPort",   Administration.QueryPort,  Administration.GetType().GetAllAttributes("QueryPort"));
            GUSkeyValuePairs["MessageOfTheDay"].WriteStringValue("Message", Administration.Motd, Administration.GetType().GetAllAttributes("Motd"));
            GUSkeyValuePairs["MessageOfTheDay"].WriteIntValue("Duration", Administration.ModDuration, Administration.GetType().GetAllAttributes("ModDuration"));

            if (profile.Type.ServerType == EnumServerType.ArkSurviveEvolved)
                GUSkeyValuePairs["ServerSettings"].WriteStringValue("ActiveMods", string.Join(",", profile.ArkConfiguration.Administration.ModIDs.ToArray()), Administration.GetType().GetAllAttributes("ModIDs"));

            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("ServerHardcore", Rules.EnableHardcoreMode, Rules.GetType().GetAllAttributes("EnableHardcoreMode"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteBoolValue("bPvEDisableFriendlyFire", Rules.DisablePveFriendlyFire, Rules.GetType().GetAllAttributes("DisablePveFriendlyFire"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteBoolValue("bDisableFriendlyFire",    Rules.DisablePvpFriendlyFire, Rules.GetType().GetAllAttributes("DisablePvpFriendlyFire"));
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("EnableExtraStructurePreventionVolumes", Rules.PreventBuildingInResourceRichAreas, Rules.GetType().GetAllAttributes("PreventBuildingInResourceRichAreas"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteBoolValue("bDisableLootCrates", Rules.DisableSupplyCrates, Rules.GetType().GetAllAttributes("DisableSupplyCrates"));
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("serverPVE",            !Rules.EnablePvp,            Rules.GetType().GetAllAttributes("EnablePvp"));
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("AllowCaveBuildingPvE", Rules.EnablePveCaveBuilding, Rules.GetType().GetAllAttributes("EnablePveCaveBuilding"));
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("AllowCaveBuildingPvP", Rules.EnablePvpCaveBuilding, Rules.GetType().GetAllAttributes("EnablePvpCaveBuilding"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteBoolValue("bUseSingleplayerSettings", Rules.EnableSinglePlayerSettings, Rules.GetType().GetAllAttributes("EnableSinglePlayerSettings"));
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("AllowCrateSpawnsOnTopOfStructures", Rules.AllowCrateSpawnsOnTopOfStructures, Rules.GetType().GetAllAttributes("AllowCrateSpawnsOnTopOfStructures"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteBoolValue("bShowCreativeMode", Rules.EnableCreativeMode, Rules.GetType().GetAllAttributes("EnableCreativeMode"));
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("EnableCryoSicknessPVE",   Rules.EnablePveCryoSickness,   Rules.GetType().GetAllAttributes("EnablePveCryoSickness"));
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("RandomSupplyCratePoints", Rules.RandomSupplyCratePoints, Rules.GetType().GetAllAttributes("RandomSupplyCratePoints"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteFloatValue("SupplyCrateLootQualityMultiplier", Rules.SupplyCrateLootQualityMultiplier, Rules.GetType().GetAllAttributes("SupplyCrateLootQualityMultiplier"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteFloatValue("FishingLootQualityMultiplier",     Rules.FishingLootQualityMultiplier,     Rules.GetType().GetAllAttributes("FishingLootQualityMultiplier"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteBoolValue("bUseCorpseLocator", Rules.UseCorpseLocation, Rules.GetType().GetAllAttributes("UseCorpseLocation"));
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("PreventSpawnAnimations", Rules.PreventSpawnAnimations, Rules.GetType().GetAllAttributes("PreventSpawnAnimations"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteBoolValue("bAllowUnlimitedRespecs",          Rules.AllowUnlimitedRespecs,          Rules.GetType().GetAllAttributes("AllowUnlimitedRespecs"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteBoolValue("bAllowPlatformSaddleMultiFloors", Rules.AllowPlatformSaddleMultiFloors, Rules.GetType().GetAllAttributes("AllowPlatformSaddleMultiFloors"));
            GUSkeyValuePairs["ServerSettings"].WriteFloatValue("PlatformSaddleBuildAreaBoundsMultiplier", Rules.PlatformSaddleBuildAreaBoundsMultiplier, Rules.GetType().GetAllAttributes("PlatformSaddleBuildAreaBoundsMultiplier"));
            GUSkeyValuePairs["ServerSettings"].WriteIntValue("MaxGateFrameOnSaddles", Rules.MaxGatewaysOnSaddles, Rules.GetType().GetAllAttributes("MaxGatewaysOnSaddles"));
            GUSkeyValuePairs["ServerSettings"].WriteFloatValue("DifficultyOffset",           Rules.DifficultyOffset,                                           Rules.GetType().GetAllAttributes("DifficultyOffset"), Rules.EnableDifficultOverride);
            GUSkeyValuePairs["ServerSettings"].WriteFloatValue("OverrideOfficialDifficulty", OfficialDifficultyValueConverter.ConvertBack(Rules.MaxDinoLevel), Rules.GetType().GetAllAttributes("MaxDinoLevel"),     Rules.EnableDifficultOverride);
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteIntValue("DestroyTamesOverLevelClamp", Rules.DestroyTamesOverLevel, Rules.GetType().GetAllAttributes("DestroyTamesOverLevel"));
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("noTributeDownloads",                !Rules.EnableTributeDownloads,   Rules.GetType().GetAllAttributes("EnableTributeDownloads"),    Rules.EnableTributeDownloads);
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("PreventDownloadSurvivors",          Rules.NoSurvivorDownloads,       Rules.GetType().GetAllAttributes("NoSurvivorDownloads"),       Rules.EnableTributeDownloads);
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("PreventDownloadItems",              Rules.NoItemDownloads,           Rules.GetType().GetAllAttributes("NoItemDownloads"),           Rules.EnableTributeDownloads);
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("PreventDownloadDinos",              Rules.NoDinoDownloads,           Rules.GetType().GetAllAttributes("NoDinoDownloads"),           Rules.EnableTributeDownloads);
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("CrossARKAllowForeignDinoDownloads", Rules.AllowForeignDinoDownloads, Rules.GetType().GetAllAttributes("AllowForeignDinoDownloads"), Rules.EnableTributeDownloads);
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("PreventUploadSurvivors",            Rules.NoSurvivorUploads,         Rules.GetType().GetAllAttributes("NoSurvivorUploads"));
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("PreventUploadItems",                Rules.NoItemUploads,             Rules.GetType().GetAllAttributes("NoItemUploads"));
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("PreventUploadDinos",                Rules.NoDinoUploads,             Rules.GetType().GetAllAttributes("PreventUploadDinos"));
            GUSkeyValuePairs["ServerSettings"].WriteIntValue("MaxTributeDinos",                   Rules.MaxTributeDinos,                          Rules.GetType().GetAllAttributes("MaxTributeDinos"),                          Rules.LimitMaxTributeDinos);
            GUSkeyValuePairs["ServerSettings"].WriteIntValue("MaxTributeItems",                   Rules.MaxTributeItems,                          Rules.GetType().GetAllAttributes("MaxTributeItems"),                          Rules.LimitTributeItems);
            GUSkeyValuePairs["ServerSettings"].WriteIntValue("TributeCharacterExpirationSeconds", Rules.OverrideSurvivorUploadExpirationValue,    Rules.GetType().GetAllAttributes("OverrideSurvivorUploadExpirationValue"),    Rules.OverrideSurvivorUploadExpiration);
            GUSkeyValuePairs["ServerSettings"].WriteIntValue("TributeDinoExpirationSeconds",      Rules.OverrideDinoUploadExpirationValue,        Rules.GetType().GetAllAttributes("OverrideDinoUploadExpirationValue"),        Rules.OverrideDinoUploadExpiration);
            GUSkeyValuePairs["ServerSettings"].WriteIntValue("TributeItemExpirationSeconds",      Rules.OverrideItemUploadExpirationValue,        Rules.GetType().GetAllAttributes("OverrideItemUploadExpirationValue"),        Rules.OverrideItemUploadExpiration);
            GUSkeyValuePairs["ServerSettings"].WriteIntValue("MinimumDinoReuploadInterval",       Rules.OverrideMinimumDinoReUploadIntervalValue, Rules.GetType().GetAllAttributes("OverrideMinimumDinoReUploadIntervalValue"), Rules.OverrideMinimumDinoReUploadInterval);
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteBoolValue("bAutoPvETimer",         Rules.PveSchedule,   Rules.GetType().GetAllAttributes("PveSchedule"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteBoolValue("bAutoPvEUseSystemTime", Rules.UseServerTime, Rules.GetType().GetAllAttributes("UseServerTime"), Rules.PveSchedule);
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteIntValue("AutoPvEStartTimeSeconds", Rules.PvpStartTime.ConvertHourToSeconds(), Rules.GetType().GetAllAttributes("PvpStartTime"), Rules.PveSchedule);
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteIntValue("AutoPvEStopTimeSeconds",  Rules.PvpEndTime.ConvertHourToSeconds(),   Rules.GetType().GetAllAttributes("PvpEndTime"),   Rules.PveSchedule);
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("PreventOfflinePvP", Rules.PreventOfflinePvp, Rules.GetType().GetAllAttributes("PreventOfflinePvp"), Rules.PreventOfflinePvp);
            GUSkeyValuePairs["ServerSettings"].WriteIntValue("PreventOfflinePvPInterval",                     Rules.LogoutInterval,              Rules.GetType().GetAllAttributes("LogoutInterval"),              Rules.PreventOfflinePvp);
            GUSkeyValuePairs["ServerSettings"].WriteIntValue("PreventOfflinePvPConnectionInvincibleInterval", Rules.ConnectionInvicibleInterval, Rules.GetType().GetAllAttributes("ConnectionInvicibleInterval"), Rules.PreventOfflinePvp);
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteBoolValue("bIncreasePvPRespawnInterval", Rules.IncreasePvpRespawnInterval, Rules.GetType().GetAllAttributes("IncreasePvpRespawnInterval"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteIntValue("IncreasePvPRespawnIntervalBaseAmount",  Rules.IntervalBaseAmount,  Rules.GetType().GetAllAttributes("IntervalBaseAmount"),  Rules.IncreasePvpRespawnInterval);
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteIntValue("IncreasePvPRespawnIntervalCheckPeriod", Rules.IntervalCheckPeriod, Rules.GetType().GetAllAttributes("IntervalCheckPeriod"), Rules.IncreasePvpRespawnInterval);
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteFloatValue("IncreasePvPRespawnIntervalMultiplier", Rules.IntervalMultiplier, Rules.GetType().GetAllAttributes("IntervalMultiplier"), Rules.IncreasePvpRespawnInterval);
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteIntValue("MaxNumberOfPlayersInTribe", Rules.MaxPlayersInTribe, Rules.GetType().GetAllAttributes("MaxPlayersInTribe"));
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("PreventTribeAlliances", !Rules.AllowTribeAlliances, Administration.GetType().GetAllAttributes("AllowTribeAlliances"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteIntValue("MaxAlliancesPerTribe", Rules.MaxAlliancesPerTribe, Rules.GetType().GetAllAttributes("MaxAlliancesPerTribe"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteIntValue("MaxTribesPerAlliance", Rules.MaxTribesPerAlliance, Rules.GetType().GetAllAttributes("MaxTribesPerAlliance"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteBoolValue("bPvEAllowTribeWar",       Rules.AllowTribeWarfare,          Rules.GetType().GetAllAttributes("AllowTribeWarfare"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteBoolValue("bPvEAllowTribeWarCancel", Rules.AllowCancelingTribeWarfare, Rules.GetType().GetAllAttributes("AllowCancelingTribeWarfare"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteBoolValue("bAllowCustomRecipes",     Rules.AllowCostumRecipes,         Rules.GetType().GetAllAttributes("AllowCostumRecipes"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteFloatValue("CustomRecipeEffectivenessMultiplier", Rules.CostumRecipesEffectivenessMultiplier, Rules.GetType().GetAllAttributes("CostumRecipesEffectivenessMultiplier"), Rules.AllowCostumRecipes);
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteFloatValue("CustomRecipeSkillMultiplier",         Rules.CostumRecipesSkillMultiplier,         Rules.GetType().GetAllAttributes("CostumRecipesSkillMultiplier"),         Rules.AllowCostumRecipes);
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("PreventDiseases",      !Rules.EnableDiseases,      Rules.GetType().GetAllAttributes("EnableDiseases"));
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("NonPermanentDiseases", Rules.NonPermanentDiseases, Rules.GetType().GetAllAttributes("NonPermanentDiseases"), Rules.EnableDiseases);
            GUSkeyValuePairs["ServerSettings"].WriteIntValue("NPCNetworkStasisRangeScalePlayerCountStart", Rules.OnlinePlayerCountStart, Rules.GetType().GetAllAttributes("OnlinePlayerCountStart"), Rules.OverrideNpcNetworkStasisRangeScale);
            GUSkeyValuePairs["ServerSettings"].WriteIntValue("NPCNetworkStasisRangeScalePlayerCountEnd",   Rules.OnlinePlayerCountEnd,   Rules.GetType().GetAllAttributes("OnlinePlayerCountEnd"),   Rules.OverrideNpcNetworkStasisRangeScale);
            GUSkeyValuePairs["ServerSettings"].WriteFloatValue("NPCNetworkStasisRangeScalePercentEnd", Rules.ScaleMaximum,                  Rules.GetType().GetAllAttributes("ScaleMaximum"), Rules.OverrideNpcNetworkStasisRangeScale);
            GUSkeyValuePairs["ServerSettings"].WriteFloatValue("OxygenSwimSpeedStatMultiplier",        Rules.OxygenSwimSpeedStatMultiplier, Rules.GetType().GetAllAttributes("OxygenSwimSpeedStatMultiplier"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteFloatValue("UseCorpseLifeSpanMultiplier",                     Rules.UseCorpseLifeSpanMultiplier,       Rules.GetType().GetAllAttributes("UseCorpseLifeSpanMultiplier"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteFloatValue("GlobalPoweredBatteryDurabilityDecreasePerSecond", Rules.GlobalPoweredBatteryDurability,    Rules.GetType().GetAllAttributes("GlobalPoweredBatteryDurability"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteFloatValue("FuelConsumptionIntervalMultiplier",               Rules.FuelConsumptionIntervalMultiplier, Rules.GetType().GetAllAttributes("FuelConsumptionIntervalMultiplier"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteIntValue("LimitNonPlayerDroppedItemsRange", Rules.LimitNonPlayerDroppedItemsRange, Rules.GetType().GetAllAttributes("LimitNonPlayerDroppedItemsRange"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteIntValue("LimitNonPlayerDroppedItemsCount", Rules.LimitNonPlayerDroppedItemsCount, Rules.GetType().GetAllAttributes("LimitNonPlayerDroppedItemsCount"));
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("EnableCryopodNerf", Rules.EnableCryopodNerf, Rules.GetType().GetAllAttributes("EnableCryopodNerf"));
            GUSkeyValuePairs["ServerSettings"].WriteIntValue("CryopodNerfDuration", Rules.EnableCryopodNerfDuration, Rules.GetType().GetAllAttributes("EnableCryopodNerfDuration"));
            GUSkeyValuePairs["ServerSettings"].WriteFloatValue("CryopodNerfIncomingDamageMultPercent", Rules.IncomingDamageMultiplierPercent, Rules.GetType().GetAllAttributes("IncomingDamageMultiplierPercent"));
            GUSkeyValuePairs["ServerSettings"].WriteFloatValue("CryopodNerfDamageMult",                Rules.OutgoingDamageMultiplier,        Rules.GetType().GetAllAttributes("OutgoingDamageMultiplier"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteBoolValue("bDisableGenesisMissions", Rules.Gen1DisableMissions, Rules.GetType().GetAllAttributes("Gen1DisableMissions"));
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("AllowTekSuitPowersInGenesis", Rules.Gen1AllowTekSuitPowers, Rules.GetType().GetAllAttributes("Gen1AllowTekSuitPowers"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteBoolValue("bDisableDefaultMapItemSets", Rules.Gen2DisableTekSuitonSpawn, Rules.GetType().GetAllAttributes("Gen2DisableTekSuitonSpawn"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteBoolValue("bDisableWorldBuffs",         Rules.Gen2DisableWorldBuffs,     Rules.GetType().GetAllAttributes("Gen2DisableWorldBuffs"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteBoolValue("bEnableWorldBuffScaling",    Rules.EnableWorldBuffScaling,    Rules.GetType().GetAllAttributes("EnableWorldBuffScaling"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteFloatValue("WorldBuffScalingEfficacy",              Rules.WorldBuffScanlingEfficacy,   Rules.GetType().GetAllAttributes("WorldBuffScanlingEfficacy"), Rules.EnableWorldBuffScaling);
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteFloatValue("AdjustableMutagenSpawnDelayMultiplier", Rules.MutagemSpawnDelayMultiplier, Rules.GetType().GetAllAttributes("MutagemSpawnDelayMultiplier"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteBoolValue("bDisableHexagonStore",                Rules.DisableHexagonStore,        Rules.GetType().GetAllAttributes("DisableHexagonStore"),        Rules.DisableHexagonStore);
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteBoolValue("bHexStoreAllowOnlyEngramTradeOption", Rules.AllowOnlyEngramPointsTrade, Rules.GetType().GetAllAttributes("AllowOnlyEngramPointsTrade"), Rules.AllowOnlyEngramPointsTrade);
            GUSkeyValuePairs["ServerSettings"].WriteIntValue("MaxHexagonsPerCharacter", Rules.MaxHexagonsPerCharacter, Rules.GetType().GetAllAttributes("MaxHexagonsPerCharacter"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteFloatValue("BaseHexagonRewardMultiplier", Rules.HexagonRewardMultiplier, Rules.GetType().GetAllAttributes("HexagonRewardMultiplier"));
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteFloatValue("HexagonCostMultiplier",       Rules.HexagonCostMultiplier,   Rules.GetType().GetAllAttributes("HexagonCostMultiplier"));
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("AllowMultipleTamedUnicorns", Rules.AllowMultipleTamedUnicorns, Rules.GetType().GetAllAttributes("AllowMultipleTamedUnicorns"), Rules.EnableRagnarokSettings);
            GUSkeyValuePairs["ServerSettings"].WriteIntValue("HexagonCostMultiplier", Rules.UnicornSpawnInterval, Rules.GetType().GetAllAttributes("UnicornSpawnInterval"), Rules.EnableRagnarokSettings);
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("EnableVolcano", Rules.EnableVolcano, Rules.GetType().GetAllAttributes("EnableVolcano"), Rules.EnableRagnarokSettings);
            GUSkeyValuePairs["ServerSettings"].WriteFloatValue("VolcanoInterval",  Rules.VolcanoInterval,  Rules.GetType().GetAllAttributes("VolcanoInterval"),  Rules.EnableRagnarokSettings);
            GUSkeyValuePairs["ServerSettings"].WriteFloatValue("VolcanoIntensity", Rules.VolcanoIntensity, Rules.GetType().GetAllAttributes("VolcanoIntensity"), Rules.EnableRagnarokSettings);
            GUSkeyValuePairs["ServerSettings"].WriteBoolValue("UseFjordurTraversalBuff", Rules.EnableFjordurBiomeTeleport, Rules.GetType().GetAllAttributes("EnableFjordurBiomeTeleport"), Rules.EnableFjordurSettings);
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteIntValue("ItemStatClamps[0]", Rules.GenericQualityClamp,      Rules.GetType().GetAllAttributes("GenericQualityClamp"),      Rules.EnableGenericQualityClamp);
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteIntValue("ItemStatClamps[1]", Rules.ArmorClamp,               Rules.GetType().GetAllAttributes("ArmorClamp"),               Rules.EnableArmorClamp);
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteIntValue("ItemStatClamps[2]", Rules.MaxDurabilityClamp,       Rules.GetType().GetAllAttributes("MaxDurabilityClamp"),       Rules.EnableMaxDurabilityClamp);
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteIntValue("ItemStatClamps[3]", Rules.WeaponDamagePercentClamp, Rules.GetType().GetAllAttributes("WeaponDamagePercentClamp"), Rules.EnableWeaponDamagePercentClamp);
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteIntValue("ItemStatClamps[4]", Rules.WeaponClipAmmoClamp,      Rules.GetType().GetAllAttributes("WeaponClipAmmoClamp"),      Rules.EnableWeaponClipAmmoClamp);
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteIntValue("ItemStatClamps[5]", Rules.HypoInsulationClamp,      Rules.GetType().GetAllAttributes("HypoInsulationClamp"),      Rules.EnableHypoInsulationClamp);
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteIntValue("ItemStatClamps[6]", Rules.WeightClamp,              Rules.GetType().GetAllAttributes("WeightClamp"),              Rules.EnableWeightClamp);
            GamekeyValuePairs["/script/shootergame.shootergamemode"].WriteIntValue("ItemStatClamps[7]", Rules.HyperInsulationClamp,     Rules.GetType().GetAllAttributes("HyperInsulationClamp"),     Rules.EnableHyperInsulationClamp);

            foreach (var section in GUSkeyValuePairs) systemIniFile.WriteSection(IniFiles.GameUserSettings, section.Key, section.Value.ToEnumerableConfigFile());
            foreach (var section in GamekeyValuePairs) systemIniFile.WriteSection(IniFiles.Game,            section.Key, section.Value.ToEnumerableConfigFile());
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
            if (Rules.DisableCostumTributeFolders) hifenArgs.Add(" -DisableCustomFoldersInTributeInventories");
            if (Rules.DisablePvpRailGun) hifenArgs.Add(" -DisableRailgunPVP");
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
            if (Rules.FjordhawkInventoryCooldown != 3600) hifenArgs.Add($" -MinimumTimeBetweenInventoryRetrieval={Rules.FjordhawkInventoryCooldown}");
            if (Administration.LocalIp.Trim()    != "") interrogationArgs.Add($"?MultiHome={Administration.LocalIp.Trim()}");
            if (Administration.DisableAntiSpeedHackDetection) hifenArgs.Add(" -noantispeedhack");
            if (!Administration.EnableBattleEye) hifenArgs.Add(" -NoBattlEye");
            if (Administration.DisablePlayerMovePhysics) hifenArgs.Add(" -nocombineclientmoves");
            if (Administration.NoDinos) hifenArgs.Add(" -nodinos");
            if (Administration.NoHandDetection) hifenArgs.Add(" -NoHangDetection");
            if (Administration.LogAdminCommandsToAdmins) hifenArgs.Add(" -NotifyAdminCommandsInChat");
            if (Administration.NoUnderMeshChecking) hifenArgs.Add(" -noundermeshchecking");
            if (Administration.NoUnderMeshKilling) hifenArgs.Add(" -noundermeshkilling");
            if (!Rules.AllowTribeWarfare) hifenArgs.Add(" -pvedisallowtribewar");
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
            if (Rules.NoTransferFromFiltering) hifenArgs.Add(" -NoTransferFromFiltering");
            //if (this.Administration.PreventHibernation) hifenArgs.Add($" -PreventHibernation");

            interrogationArgs.Add($"?Port={Administration.ServerPort}");
            interrogationArgs.Add($"?QueryPort={Administration.QueryPort}");

            hifenArgs.Add(" -nosteamclient");
            hifenArgs.Add(" -game");
            hifenArgs.Add(" -server");
            hifenArgs.Add(" -log");

            if (Rules.AllowCrateSpawnsOnTopOfStructures) interrogationArgs.Add($"?AllowCrateSpawnsOnTopOfStructures=True");
            hifenArgs.Add(" -ForceAllowCaveFlyers"); //Remove later 
            //-ForceAllowCaveFlyers
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
        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool UseServerApi { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        public string ServerAdminPassword { get; set; } = Membership.GeneratePassword(10, 6);

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue("")]
        public string ServerSpectatorPassword { get; set; } = "";

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue("27015")]
        public string QueryPort { get; set; } = "27015";

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool UseRcon { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue("32330")]
        public string RconPort { get; set; } = "32330";

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(600)]
        public int RconServerLogBuffer { get; set; } = 600;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue("")]
        public string MapName { get; set; } = "";

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue("")]
        public string TotalConversionId { get; set; } = "";

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue("15")]
        public int AutoSavePeriod { get; set; } = 15;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue("")]
        public string Motd { get; set; } = "";

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(20)]
        public int ModDuration { get; set; } = 20;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(true)]
        public bool EnableInterval { get; set; } = true;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(60)]
        public int ModInterval { get; set; } = 60;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue("Live")]
        public string Branch { get; set; } = "Live";

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(true)]
        public bool EnablIdleTimeOut { get; set; } = true;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(3600)]
        public int IdleTimout { get; set; } = 3600;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool UseBanListUrl { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue("http://arkdedicated.com/banlist.txt")]
        public string BanListUrl { get; set; } = "http://arkdedicated.com/banlist.txt";

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool DisableVac { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool EnableBattleEye { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool DisablePlayerMovePhysics { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(true)]
        public bool OutputLogToConsole { get; set; } = true;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool UseAllCores { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool UseCache { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool NoHandDetection { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool NoDinos { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool NoUnderMeshChecking { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool NoUnderMeshKilling { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool EnableVivox { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(true)]
        public bool AllowSharedConnections { get; set; } = true;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool RespawnDinosOnStartUp { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool EnableAutoForceRespawnDinos { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(24)]
        public int AutoForceRespawnDinosInterval { get; set; } = 24;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool DisableAntiSpeedHackDetection { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(1)]
        public int AntiSpeedHackBias { get; set; } = 1;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool ForceDirectX10 { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool ForceLowMemory { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool ForceNoManSky { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool UseNoMemoryBias { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool StasisKeepController { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool ServerAllowAnsel { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool StructureMemoryOptimizations { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool EnableCrossPlay { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool EnablePublicIpForEpic { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool EpicStorePlayersOnly { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue("")]
        public string AlternateSaveDirectoryName { get; set; } = "";

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue("")]
        public string ClusterId { get; set; } = "";

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool ClusterDirectoryOverride { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfDefault)]
        [DefaultValue(true)]
        public bool EnableServerAdminLogs { get; set; } = true;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(true)]
        public bool ServerAdminLogsIncludeTribeLogs { get; set; } = true;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(true)]
        public bool ServerRconOutputTribeLogs { get; set; } = true;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(true)]
        public bool AllowHideDamageSourceFromLogs { get; set; } = true;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(100)]
        public int MaximumTribeLogs { get; set; } = 100;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool LogAdminCommandsToPublic { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool LogAdminCommandsToAdmins { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(true)]
        public bool TribeLogDestroyedEnemyStructures { get; set; } = true;
    }

    public class Rules {
        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool EnableHardcoreMode { get; set; }

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool DisablePveFriendlyFire { get; set; }

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool DisablePvpFriendlyFire { get; set; }

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool PreventBuildingInResourceRichAreas { get; set; }

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool DisableSupplyCrates { get; set; }

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool EnablePvp { get; set; }

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool EnablePveCaveBuilding { get; set; }

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(true)]
        public bool EnablePvpCaveBuilding { get; set; } = true;

        [ValueBehavior(DeleteEnumOption.DeleteIfDefault)]
        [DefaultValue(false)]
        public bool EnableSinglePlayerSettings { get; set; }

        [ValueBehavior(DeleteEnumOption.DeleteIfDefault)]
        [DefaultValue(true)]
        public bool AllowCrateSpawnsOnTopOfStructures { get; set; } = true;

        [ValueBehavior(DeleteEnumOption.DeleteIfDefault)]
        [DefaultValue(false)]
        public bool EnableCreativeMode { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool EnablePveCryoSickness { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool DisablePvpRailGun { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool DisableCostumTributeFolders { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool RandomSupplyCratePoints { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfDefault)]
        [DefaultValue(1f)]
        public float SupplyCrateLootQualityMultiplier { get; set; } = 1;

        [ValueBehavior(DeleteEnumOption.DeleteIfDefault)]
        [DefaultValue(1f)]
        public float FishingLootQualityMultiplier { get; set; } = 1;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool UseCorpseLocation { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool PreventSpawnAnimations { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool AllowUnlimitedRespecs { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool AllowPlatformSaddleMultiFrools { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool AllowPlatformSaddleMultiFloors { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(1f)]
        public float PlatformSaddleBuildAreaBoundsMultiplier { get; set; } = 1;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(2)]
        public int MaxGatewaysOnSaddles { get; set; } = 2;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool EnableDifficultOverride { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(4)] //This is 4 because is the default value in settings instead of level
        public int MaxDinoLevel { get; set; } = 120;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(1f)]
        public float DifficultyOffset { get; set; } = 1;

        [ValueBehavior(DeleteEnumOption.DeleteIfDefault)]
        [DefaultValue(0)]
        public int DestroyTamesOverLevel { get; set; } = 0;

        [ValueBehavior(DeleteEnumOption.DeleteIfDefault)]
        [DefaultValue(true)]
        public bool EnableTributeDownloads { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.ForceDefaultIfNotUsed)]
        [DefaultValue(true)]
        public bool NoSurvivorDownloads { get; set; } = true;

        [ValueBehavior(DeleteEnumOption.ForceDefaultIfNotUsed)]
        [DefaultValue(true)]
        public bool NoItemDownloads { get; set; } = true;

        [ValueBehavior(DeleteEnumOption.ForceDefaultIfNotUsed)]
        [DefaultValue(true)]
        public bool NoDinoDownloads { get; set; } = true;

        [ValueBehavior(DeleteEnumOption.ForceDefaultIfNotUsed)]
        [DefaultValue(false)]
        public bool AllowForeignDinoDownloads { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(true)]
        public bool NoSurvivorUploads { get; set; } = true;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(true)]
        public bool NoItemUploads { get; set; } = true;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(true)]
        public bool NoDinoUploads { get; set; } = true;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool LimitMaxTributeDinos { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(0)]
        public int MaxTributeDinos { get; set; } = 0;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool LimitTributeItems { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(0)]
        public int MaxTributeItems { get; set; } = 0;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool NoTransferFromFiltering { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool OverrideSurvivorUploadExpiration { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(86400)]
        public int OverrideSurvivorUploadExpirationValue { get; set; } = 86400;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool OverrideItemUploadExpiration { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(86400)]
        public int OverrideItemUploadExpirationValue { get; set; } = 86400;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool OverrideDinoUploadExpiration { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(86400)]
        public int OverrideDinoUploadExpirationValue { get; set; } = 86400;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool OverrideMinimumDinoReUploadInterval { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(43200)]
        public int OverrideMinimumDinoReUploadIntervalValue { get; set; } = 43200;

        [ValueBehavior(DeleteEnumOption.ForceDefaultIfNotUsed)]
        [DefaultValue(false)]
        public bool PveSchedule { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(false)]
        public bool UseServerTime { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue("0000")]
        public string PvpStartTime { get; set; } = "0000";

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue("0000")]
        public string PvpEndTime { get; set; } = "0000";

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool PreventOfflinePvp { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(900)]
        public int LogoutInterval { get; set; } = 900;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(5)]
        public int ConnectionInvicibleInterval { get; set; } = 5;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool IncreasePvpRespawnInterval { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(300)]
        public int IntervalCheckPeriod { get; set; } = 300;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(1f)]
        public float IntervalMultiplier { get; set; } = 1;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(60)]
        public int IntervalBaseAmount { get; set; } = 60;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(70)]
        public int MaxPlayersInTribe { get; set; } = 70;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(15)]
        public int TribeNameChangeCooldDown { get; set; } = 15;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(0)]
        public int TribeSlotReuseCooldown { get; set; } = 0;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(true)]
        public bool AllowTribeAlliances { get; set; } = true;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(10)]
        public int MaxAlliancesPerTribe { get; set; } = 10;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(10)]
        public int MaxTribesPerAlliance { get; set; } = 10;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(true)]
        public bool AllowTribeWarfare { get; set; } = true;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool AllowCancelingTribeWarfare { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(true)]
        public bool AllowCostumRecipes { get; set; } = true;

        [ValueBehavior(DeleteEnumOption.DeleteIfDefault)]
        [DefaultValue(1f)]
        public float CostumRecipesEffectivenessMultiplier { get; set; } = 1;

        [ValueBehavior(DeleteEnumOption.DeleteIfDefault)]
        [DefaultValue(1f)]
        public float CostumRecipesSkillMultiplier { get; set; } = 1;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(true)]
        public bool EnableDiseases { get; set; } = true;

        [ValueBehavior(DeleteEnumOption.ForceDefaultIfNotUsed)]
        [DefaultValue(false)]
        public bool NonPermanentDiseases { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool OverrideNpcNetworkStasisRangeScale { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(70)]
        public int OnlinePlayerCountStart { get; set; } = 70;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(120)]
        public int OnlinePlayerCountEnd { get; set; } = 120;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(0.5f)]
        public float ScaleMaximum { get; set; } = 0.5f;

        [ValueBehavior(DeleteEnumOption.DeleteIfDefault)]
        [DefaultValue(1f)]
        public float OxygenSwimSpeedStatMultiplier { get; set; } = 1;

        [ValueBehavior(DeleteEnumOption.DeleteIfDefault)]
        [DefaultValue(1f)]
        public float UseCorpseLifeSpanMultiplier { get; set; } = 1;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(3600)]
        public int FjordhawkInventoryCooldown { get; set; } = 3600;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(3f)]
        public float GlobalPoweredBatteryDurability { get; set; } = 3;

        [ValueBehavior(DeleteEnumOption.DeleteIfDefault)]
        [DefaultValue(1f)]
        public float FuelConsumptionIntervalMultiplier { get; set; } = 1;

        [ValueBehavior(DeleteEnumOption.DeleteIfDefault)]
        [DefaultValue(0)]
        public int LimitNonPlayerDroppedItemsRange { get; set; } = 0;

        [ValueBehavior(DeleteEnumOption.DeleteIfDefault)]
        [DefaultValue(0)]
        public int LimitNonPlayerDroppedItemsCount { get; set; } = 0;

        [ValueBehavior(DeleteEnumOption.DeleteIfDefault)]
        [DefaultValue(false)]
        public bool EnableCryopodNerf { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfDefault)]
        [DefaultValue(10)]
        public int EnableCryopodNerfDuration { get; set; } = 10;

        [ValueBehavior(DeleteEnumOption.DeleteIfDefault)]
        [DefaultValue(1f)]
        public float OutgoingDamageMultiplier { get; set; } = 1;

        [ValueBehavior(DeleteEnumOption.DeleteIfDefault)]
        [DefaultValue(0f)]
        public float IncomingDamageMultiplierPercent { get; set; } = 0;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool Gen1DisableMissions { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool Gen1AllowTekSuitPowers { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool Gen2DisableTekSuitonSpawn { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool Gen2DisableWorldBuffs { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool EnableWorldBuffScaling { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(1f)]
        public float WorldBuffScanlingEfficacy { get; set; } = 1;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(1f)]
        public float MutagemSpawnDelayMultiplier { get; set; } = 1;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(false)]
        public bool DisableHexagonStore { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(false)]
        public bool AllowOnlyEngramPointsTrade { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(2000000000)]
        public int MaxHexagonsPerCharacter { get; set; } = 2000000000;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(1f)]
        public float HexagonRewardMultiplier { get; set; } = 1;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(1f)]
        public float HexagonCostMultiplier { get; set; } = 1;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(false)]
        public bool EnableRagnarokSettings { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(false)]
        public bool AllowMultipleTamedUnicorns { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(24)]
        public int UnicornSpawnInterval { get; set; } = 24;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(true)]
        public bool EnableVolcano { get; set; } = true;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(1f)]
        public float VolcanoInterval { get; set; } = 1;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(1f)]
        public float VolcanoIntensity { get; set; } = 1;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool EnableFjordurSettings { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(true)]
        public bool EnableFjordurBiomeTeleport { get; set; } = true;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool EnableGenericQualityClamp { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(0)]
        public int GenericQualityClamp { get; set; } = 0;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool EnableArmorClamp { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(0)]
        public int ArmorClamp { get; set; } = 0;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool EnableWeaponDamagePercentClamp { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(0)]
        public int WeaponDamagePercentClamp { get; set; } = 0;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool EnableHypoInsulationClamp { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(0)]
        public int HypoInsulationClamp { get; set; } = 0;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool EnableWeightClamp { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(0)]
        public int WeightClamp { get; set; } = 0;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool EnableMaxDurabilityClamp { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(0)]
        public int MaxDurabilityClamp { get; set; } = 0;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool EnableWeaponClipAmmoClamp { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(0)]
        public int WeaponClipAmmoClamp { get; set; } = 0;

        [ValueBehavior(DeleteEnumOption.KeepValue)]
        [DefaultValue(false)]
        public bool EnableHyperInsulationClamp { get; set; } = false;

        [ValueBehavior(DeleteEnumOption.DeleteIfNotUsed)]
        [DefaultValue(0)]
        public int HyperInsulationClamp { get; set; } = 0;
    }
}