using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using Newtonsoft.Json;
using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models.SupportedServers;
using OphiussaServerManager.Tools;
using OphiussaServerManager.Tools.Update;

namespace OphiussaServerManager.Forms {
    public partial class FrmArk : Form {
        private bool _isRunning;
        private int _processId = -1;
        private Profile _profile;
        private TabPage _tab;
        private bool _isInstalled;

        public FrmArk() {
            InitializeComponent();
        }

        private void FrmArk_Load(object sender, EventArgs e) {
            var thread = new Thread(IsRunningProcess);
            thread.Start();
        }

        private void LoadDefaultFieldValues() {
            try {
                var ret = NetworkTools.GetAllHostIp();

                txtLocalIP.DataSource = ret;
                txtLocalIP.ValueMember = "IP";
                txtLocalIP.DisplayMember = "Description";

                MainForm.Settings.Branchs.Distinct().ToList().ForEach(branch => { cbBranch.Items.Add(branch); });

                chkEnableCrossPlay.Enabled = _profile.Type.ServerType == EnumServerType.ArkSurviveEvolved;
                chkEnablPublicIPEpic.Enabled = _profile.Type.ServerType == EnumServerType.ArkSurviveEvolved;
                ChkEpicOnly.Enabled = _profile.Type.ServerType == EnumServerType.ArkSurviveEvolved;
                txtBanUrl.Enabled = chkUseBanUrl.Checked;

                cboPriority.DataSource = Enum.GetValues(typeof(ProcessPriority));
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                MessageBox.Show($"{MethodBase.GetCurrentMethod()?.Name}: {e.Message}");
            }
        }

        public void LoadSettings(Profile profile, TabPage tab) {
            _profile = profile;
            _tab = tab;
            LoadDefaultFieldValues();

            txtProfileID.Text = profile.Key;
            txtProfileName.Text = profile.Name;
            tab.Text = txtProfileName.Text + "          ";
            txtServerType.Text = profile.Type.ServerTypeDescription;
            txtLocation.Text = profile.InstallLocation;
            chkUseApi.Checked = profile.ArkConfiguration.UseServerApi;
            txtServerName.Text = profile.ArkConfiguration.ServerName;
            txtServerPWD.Text = profile.ArkConfiguration.ServerPassword;
            txtAdminPass.Text = profile.ArkConfiguration.ServerAdminPassword;
            txtSpePwd.Text = profile.ArkConfiguration.ServerSpectatorPassword;
            txtLocalIP.SelectedValue = profile.ArkConfiguration.LocalIp;
            txtServerPort.Text = profile.ArkConfiguration.ServerPort;
            txtPeerPort.Text = profile.ArkConfiguration.PeerPort;
            txtQueryPort.Text = profile.ArkConfiguration.QueryPort;
            chkEnableRCON.Checked = profile.ArkConfiguration.UseRcon;
            txtRCONPort.Text = profile.ArkConfiguration.RconPort;
            txtRCONBuffer.Text = profile.ArkConfiguration.RconServerLogBuffer.ToString(CultureInfo.InvariantCulture);
            cboMap.SelectedValue = profile.ArkConfiguration.MapName;
            cbBranch.Text = profile.ArkConfiguration.Branch;
            txtMods.Text = profile.ArkConfiguration.ActiveMods;
            txtTotalConversion.Text = profile.ArkConfiguration.TotalConversionId;
            txtAutoSavePeriod.Text = profile.ArkConfiguration.AutoSavePeriod.ToString(CultureInfo.InvariantCulture);
            txtMOTD.Text = profile.ArkConfiguration.Motd;
            txtMOTDDuration.Text = profile.ArkConfiguration.ModDuration.ToString(CultureInfo.InvariantCulture);
            txtMOTDInterval.Text = profile.ArkConfiguration.ModInterval.ToString(CultureInfo.InvariantCulture);
            chkEnableInterval.Checked = profile.ArkConfiguration.EnableInterval;
            txtMaxPlayers.Text = profile.ArkConfiguration.MaxPlayers.ToString(CultureInfo.InvariantCulture);
            chkEnableIdleTimeout.Checked = profile.ArkConfiguration.EnablIdleTimeOut;
            txtIdleTimeout.Text = profile.ArkConfiguration.IdleTimout.ToString(CultureInfo.InvariantCulture);
            chkUseBanUrl.Checked = profile.ArkConfiguration.UseBanListUrl;
            txtBanUrl.Text = profile.ArkConfiguration.BanListUrl;
            chkDisableVAC.Checked = profile.ArkConfiguration.DisableVac;
            chkEnableBattleEye.Checked = profile.ArkConfiguration.EnableBattleEye;
            chkDisablePlayerMove.Checked = profile.ArkConfiguration.DisablePlayerMovePhysics;
            chkOutputLogToConsole.Checked = profile.ArkConfiguration.OutputLogToConsole;
            chkUseAllCores.Checked = profile.ArkConfiguration.UseAllCores;
            chkUseCache.Checked = profile.ArkConfiguration.UseCache;
            chkNoHang.Checked = profile.ArkConfiguration.NoHandDetection;
            chkNoDinos.Checked = profile.ArkConfiguration.NoDinos;
            chkNoUnderMeshChecking.Checked = profile.ArkConfiguration.NoUnderMeshChecking;
            chkNoUnderMeshKilling.Checked = profile.ArkConfiguration.NoUnderMeshKilling;
            chkEnableVivox.Checked = profile.ArkConfiguration.EnableVivox;
            chkAllowSharedConnections.Checked = profile.ArkConfiguration.AllowSharedConnections;
            chkRespawnDinosOnStartup.Checked = profile.ArkConfiguration.RespawnDinosOnStartUp;
            chkEnableForceRespawn.Checked = profile.ArkConfiguration.EnableAutoForceRespawnDinos;
            txtRespawnInterval.Text = profile.ArkConfiguration.AutoForceRespawnDinosInterval.ToString(CultureInfo.InvariantCulture);
            chkDisableSpeedHack.Checked = profile.ArkConfiguration.DisableAntiSpeedHackDetection;
            txtSpeedBias.Text = profile.ArkConfiguration.AntiSpeedHackBias.ToString(CultureInfo.InvariantCulture);
            chkForceDX10.Checked = profile.ArkConfiguration.ForceDirectX10;
            chkForceLowMemory.Checked = profile.ArkConfiguration.ForceLowMemory;
            chkForceNoManSky.Checked = profile.ArkConfiguration.ForceNoManSky;
            chkUseNoMemoryBias.Checked = profile.ArkConfiguration.UseNoMemoryBias;
            chkStasisKeepControllers.Checked = profile.ArkConfiguration.StasisKeepController;
            chkAllowAnsel.Checked = profile.ArkConfiguration.ServerAllowAnsel;
            chkStructuresOptimization.Checked = profile.ArkConfiguration.StructureMemoryOptimizations;
            chkEnableCrossPlay.Checked = profile.ArkConfiguration.EnableCrossPlay;
            chkEnableCrossPlay.Checked = profile.ArkConfiguration.EnablePublicIpForEpic;
            ChkEpicOnly.Checked = profile.ArkConfiguration.EpicStorePlayersOnly;
            txtAltSaveDirectory.Text = profile.ArkConfiguration.AlternateSaveDirectoryName;
            txtClusterID.Text = profile.ArkConfiguration.ClusterId;
            chkClusterOverride.Checked = profile.ArkConfiguration.ClusterDirectoryOverride;
            cboPriority.SelectedItem = profile.ArkConfiguration.CpuPriority;
            txtAffinity.Text = profile.ArkConfiguration.CpuAffinity;
            chkEnableServerAdminLogs.Checked = profile.ArkConfiguration.EnableServerAdminLogs;
            chkServerAdminLogsIncludeTribeLogs.Checked = profile.ArkConfiguration.ServerAdminLogsIncludeTribeLogs;
            chkServerRCONOutputTribeLogs.Checked = profile.ArkConfiguration.ServerRconOutputTribeLogs;
            chkAllowHideDamageSourceFromLogs.Checked = profile.ArkConfiguration.AllowHideDamageSourceFromLogs;
            chkLogAdminCommandsToPublic.Checked = profile.ArkConfiguration.LogAdminCommandsToPublic;
            chkLogAdminCommandstoAdmins.Checked = profile.ArkConfiguration.LogAdminCommandsToAdmins;
            chkTribeLogDestroyedEnemyStructures.Checked = profile.ArkConfiguration.TribeLogDestroyedEnemyStructures;
            txtMaximumTribeLogs.Text = profile.ArkConfiguration.MaximumTribeLogs.ToString(CultureInfo.InvariantCulture);

            chkAutoStart.Checked = profile.AutoManageSettings.AutoStartServer;
            rbOnBoot.Checked = profile.AutoManageSettings.AutoStartOn == AutoStart.OnBoot;
            rbOnLogin.Checked = profile.AutoManageSettings.AutoStartOn == AutoStart.OnLogin;
            chkShutdown1.Checked = profile.AutoManageSettings.ShutdownServer1;
            txtShutdow1.Text = profile.AutoManageSettings.ShutdownServer1Hour;
            chkSun1.Checked = profile.AutoManageSettings.ShutdownServer1Sunday;
            chkMon1.Checked = profile.AutoManageSettings.ShutdownServer1Monday;
            chkTue1.Checked = profile.AutoManageSettings.ShutdownServer1Tuesday;
            chkWed1.Checked = profile.AutoManageSettings.ShutdownServer1Wednesday;
            chkThu1.Checked = profile.AutoManageSettings.ShutdownServer1Thu;
            chkFri1.Checked = profile.AutoManageSettings.ShutdownServer1Friday;
            chkSat1.Checked = profile.AutoManageSettings.ShutdownServer1Saturday;
            chkUpdate1.Checked = profile.AutoManageSettings.ShutdownServer1PerformUpdate;
            chkRestart1.Checked = profile.AutoManageSettings.ShutdownServer1Restart;
            chkShutdown2.Checked = profile.AutoManageSettings.ShutdownServer2;
            txtShutdow2.Text = profile.AutoManageSettings.ShutdownServer2Hour;
            chkSun2.Checked = profile.AutoManageSettings.ShutdownServer2Sunday;
            chkMon2.Checked = profile.AutoManageSettings.ShutdownServer2Monday;
            chkTue2.Checked = profile.AutoManageSettings.ShutdownServer2Tuesday;
            chkWed2.Checked = profile.AutoManageSettings.ShutdownServer2Wednesday;
            chkThu2.Checked = profile.AutoManageSettings.ShutdownServer2Thu;
            chkFri2.Checked = profile.AutoManageSettings.ShutdownServer2Friday;
            chkSat2.Checked = profile.AutoManageSettings.ShutdownServer2Saturday;
            chkUpdate2.Checked = profile.AutoManageSettings.ShutdownServer2PerformUpdate;
            chkRestart2.Checked = profile.AutoManageSettings.ShutdownServer2Restart;
            chkIncludeAutoBackup.Checked = profile.AutoManageSettings.IncludeInAutoBackup;
            chkAutoUpdate.Checked = profile.AutoManageSettings.IncludeInAutoUpdate;
            chkRestartIfShutdown.Checked = profile.AutoManageSettings.AutoStartServer;

            chkEnableHardcoreMode.Checked = profile.ArkConfiguration.EnableHardcoreMode;
            chkDisablePVEFriendlyFire.Checked = profile.ArkConfiguration.DisablePveFriendlyFire;
            chkDisablePVPFriendlyFire.Checked = profile.ArkConfiguration.DisablePvpFriendlyFire;
            chkPreventBuildingInResourceRichAreas.Checked = profile.ArkConfiguration.PreventBuildingInResourceRichAreas;
            chkDisableSupplyCrates.Checked = profile.ArkConfiguration.DisableSupplyCrates;
            chkEnablePVP.Checked = profile.ArkConfiguration.EnablePvp;
            chkEnablePVECaveBuilding.Checked = profile.ArkConfiguration.EnablePveCaveBuilding;
            chkEnablePVPCaveBuilding.Checked = profile.ArkConfiguration.EnablePvpCaveBuilding;
            chkEnableSinglePlayerSettings.Checked = profile.ArkConfiguration.EnableSinglePlayerSettings;
            chkAllowCrateSpawnsOnTopOfStructures.Checked = profile.ArkConfiguration.AllowCrateSpawnsOnTopOfStructures;
            chkEnableCreativeMode.Checked = profile.ArkConfiguration.EnableCreativeMode;
            chkEnablePVECryoSickness.Checked = profile.ArkConfiguration.EnablePveCryoSickness;
            chkDisablePVPRailGun.Checked = profile.ArkConfiguration.DisablePvpRailGun;
            chkDisableCostumTributeFolders.Checked = profile.ArkConfiguration.DisableCostumTributeFolders;
            chkRandomSupplyCratePoints.Checked = profile.ArkConfiguration.RandomSupplyCratePoints;
            txtSupplyCrateLootQualityMultiplier.Text = (profile.ArkConfiguration.SupplyCrateLootQualityMultiplier).ToString(CultureInfo.InvariantCulture);
            txtFishingLootQualityMultiplier.Text = (profile.ArkConfiguration.FishingLootQualityMultiplier).ToString(CultureInfo.InvariantCulture);
            chkUseCorpseLocation.Checked = profile.ArkConfiguration.UseCorpseLocation;
            chkPreventSpawnAnimations.Checked = profile.ArkConfiguration.PreventSpawnAnimations;
            chkAllowUnlimitedRespecs.Checked = profile.ArkConfiguration.AllowUnlimitedRespecs;
            chkAllowPlatformSaddleMultiFloors.Checked = profile.ArkConfiguration.AllowPlatformSaddleMultiFloors;
            txtPlatformSaddleBuildAreaBoundsMultiplier.Text = (profile.ArkConfiguration.PlatformSaddleBuildAreaBoundsMultiplier).ToString(CultureInfo.InvariantCulture);
            txtMaxGatewaysOnSaddles.Text = profile.ArkConfiguration.MaxGatewaysOnSaddles.ToString(CultureInfo.InvariantCulture);
            chkEnableDifficultOverride.Checked = profile.ArkConfiguration.EnableDifficultOverride;
            txtMaxDinoLevel.Text = profile.ArkConfiguration.MaxDinoLevel.ToString(CultureInfo.InvariantCulture);
            txtDifficultyOffset.Text = profile.ArkConfiguration.DifficultyOffset.ToString(CultureInfo.InvariantCulture);
            txtDestroyTamesOverLevel.Text = profile.ArkConfiguration.DestroyTamesOverLevel.ToString(CultureInfo.InvariantCulture);
            chkEnableTributeDownloads.Checked = profile.ArkConfiguration.EnableTributeDownloads;
            chkNoSurvivorDownloads.Checked = profile.ArkConfiguration.NoSurvivorDownloads;
            chkNoItemDownloads.Checked = profile.ArkConfiguration.NoItemDownloads;
            chkNoDinoDownloads.Checked = profile.ArkConfiguration.NoDinoDownloads;
            chkAllowForeignDinoDownloads.Checked = profile.ArkConfiguration.AllowForeignDinoDownloads;
            chkNoSurvivorUploads.Checked = profile.ArkConfiguration.NoSurvivorUploads;
            chkNoItemUploads.Checked = profile.ArkConfiguration.NoItemUploads;
            chkNoDinoUploads.Checked = profile.ArkConfiguration.NoDinoUploads;
            chkLimitMaxTributeDinos.Checked = profile.ArkConfiguration.LimitMaxTributeDinos;
            txtMaxTributeDinos.Text = profile.ArkConfiguration.MaxTributeDinos.ToString(CultureInfo.InvariantCulture);
            chkLimitTributeItems.Checked = profile.ArkConfiguration.LimitTributeItems;
            txtMaxTributeItems.Text = profile.ArkConfiguration.MaxTributeItems.ToString(CultureInfo.InvariantCulture);
            chkNoTransferFromFiltering.Checked = profile.ArkConfiguration.NoTransferFromFiltering;
            chkOverrideSurvivorUploadExpiration.Checked = profile.ArkConfiguration.OverrideSurvivorUploadExpiration;
            txtOverrideSurvivorUploadExpirationValue.Text = profile.ArkConfiguration.OverrideSurvivorUploadExpirationValue.ToString(CultureInfo.InvariantCulture);
            chkOverrideItemUploadExpiration.Checked = profile.ArkConfiguration.OverrideItemUploadExpiration;
            txtOverrideItemUploadExpirationValue.Text = profile.ArkConfiguration.OverrideItemUploadExpirationValue.ToString(CultureInfo.InvariantCulture);
            chkOverrideDinoUploadExpiration.Checked = profile.ArkConfiguration.OverrideDinoUploadExpiration;
            txtOverrideDinoUploadExpirationValue.Text = profile.ArkConfiguration.OverrideDinoUploadExpirationValue.ToString(CultureInfo.InvariantCulture);
            chkOverrideMinimumDinoReUploadInterval.Checked = profile.ArkConfiguration.OverrideMinimumDinoReUploadInterval;
            txtOverrideMinimumDinoReUploadIntervalValue.Text = profile.ArkConfiguration.OverrideMinimumDinoReUploadIntervalValue.ToString(CultureInfo.InvariantCulture);
            chkPVESchedule.Checked = profile.ArkConfiguration.PveSchedule;
            chkUseServerTime.Checked = profile.ArkConfiguration.UseServerTime;
            txtPVPStartTime.Text = profile.ArkConfiguration.PvpStartTime.ConvertSecondsToHour();
            txtPVPEndTime.Text = profile.ArkConfiguration.PvpEndTime.ConvertSecondsToHour();
            chkPreventOfflinePVP.Checked = profile.ArkConfiguration.PreventOfflinePvp;
            txtLogoutInterval.Text = profile.ArkConfiguration.LogoutInterval.ToString(CultureInfo.InvariantCulture);
            txtConnectionInvicibleInterval.Text = profile.ArkConfiguration.ConnectionInvicibleInterval.ToString(CultureInfo.InvariantCulture);
            chkIncreasePVPRespawnInterval.Checked = profile.ArkConfiguration.IncreasePvpRespawnInterval;
            txtIntervalCheckPeriod.Text = profile.ArkConfiguration.IntervalCheckPeriod.ToString(CultureInfo.InvariantCulture);
            txtIntervalMultiplier.Text = (profile.ArkConfiguration.IntervalMultiplier).ToString(CultureInfo.InvariantCulture);
            txtIntervalBaseAmount.Text = profile.ArkConfiguration.IntervalBaseAmount.ToString(CultureInfo.InvariantCulture);
            txtMaxPlayersInTribe.Text = profile.ArkConfiguration.MaxPlayersInTribe.ToString(CultureInfo.InvariantCulture);
            txtTribeNameChangeCooldDown.Text = profile.ArkConfiguration.TribeNameChangeCooldDown.ToString(CultureInfo.InvariantCulture);
            txtTribeSlotReuseCooldown.Text = profile.ArkConfiguration.TribeSlotReuseCooldown.ToString(CultureInfo.InvariantCulture);
            chkAllowTribeAlliances.Checked = profile.ArkConfiguration.AllowTribeAlliances;
            txtMaxAlliancesPerTribe.Text = profile.ArkConfiguration.MaxAlliancesPerTribe.ToString(CultureInfo.InvariantCulture);
            txtMaxTribesPerAlliance.Text = profile.ArkConfiguration.MaxTribesPerAlliance.ToString(CultureInfo.InvariantCulture);
            chkAllowTribeWarfare.Checked = profile.ArkConfiguration.AllowTribeWarfare;
            chkAllowCancelingTribeWarfare.Checked = profile.ArkConfiguration.AllowCancelingTribeWarfare;
            chkAllowCostumRecipes.Checked = profile.ArkConfiguration.AllowCostumRecipes;
            txtCostumRecipesEffectivenessMultiplier.Text = (profile.ArkConfiguration.CostumRecipesEffectivenessMultiplier).ToString(CultureInfo.InvariantCulture);
            txtCostumRecipesSkillMultiplier.Text = (profile.ArkConfiguration.CostumRecipesSkillMultiplier).ToString(CultureInfo.InvariantCulture);
            chkEnableDiseases.Checked = profile.ArkConfiguration.EnableDiseases;
            chkNonPermanentDiseases.Checked = profile.ArkConfiguration.NonPermanentDiseases;
            chkOverrideNPCNetworkStasisRangeScale.Checked = profile.ArkConfiguration.OverrideNpcNetworkStasisRangeScale;
            txtOnlinePlayerCountStart.Text = profile.ArkConfiguration.OnlinePlayerCountStart.ToString(CultureInfo.InvariantCulture);
            txtOnlinePlayerCountEnd.Text = profile.ArkConfiguration.OnlinePlayerCountEnd.ToString(CultureInfo.InvariantCulture);
            txtScaleMaximum.Text = (profile.ArkConfiguration.ScaleMaximum).ToString(CultureInfo.InvariantCulture);
            txtOxygenSwimSpeedStatMultiplier.Text = (profile.ArkConfiguration.OxygenSwimSpeedStatMultiplier).ToString(CultureInfo.InvariantCulture);
            txtUseCorpseLifeSpanMultiplier.Text = (profile.ArkConfiguration.UseCorpseLifeSpanMultiplier).ToString(CultureInfo.InvariantCulture);
            txtFjordhawkInventoryCooldown.Text = profile.ArkConfiguration.FjordhawkInventoryCooldown.ToString(CultureInfo.InvariantCulture);
            txtGlobalPoweredBatteryDurability.Text = (profile.ArkConfiguration.GlobalPoweredBatteryDurability).ToString(CultureInfo.InvariantCulture);
            txtFuelConsumptionIntervalMultiplier.Text = (profile.ArkConfiguration.FuelConsumptionIntervalMultiplier).ToString(CultureInfo.InvariantCulture);
            txtLimitNonPlayerDroppedItemsRange.Text = profile.ArkConfiguration.LimitNonPlayerDroppedItemsRange.ToString(CultureInfo.InvariantCulture);
            txtLimitNonPlayerDroppedItemsCount.Text = profile.ArkConfiguration.LimitNonPlayerDroppedItemsCount.ToString(CultureInfo.InvariantCulture);
            chkEnableCryopodNerf.Checked = profile.ArkConfiguration.EnableCryopodNerf;
            txtEnableCryopodNerfDuration.Text = profile.ArkConfiguration.EnableCryopodNerfDuration.ToString(CultureInfo.InvariantCulture);
            txtOutgoingDamageMultiplier.Text = (profile.ArkConfiguration.OutgoingDamageMultiplier).ToString(CultureInfo.InvariantCulture);
            txtIncomingDamageMultiplierPercent.Text = (profile.ArkConfiguration.IncomingDamageMultiplierPercent).ToString(CultureInfo.InvariantCulture);
            chkGen1DisableMissions.Checked = profile.ArkConfiguration.Gen1DisableMissions;
            chkGen1AllowTekSuitPowers.Checked = profile.ArkConfiguration.Gen1AllowTekSuitPowers;
            chkGen2DisableTEKSuitonSpawn.Checked = profile.ArkConfiguration.Gen2DisableTekSuitonSpawn;
            chkGen2DisableWorldBuffs.Checked = profile.ArkConfiguration.Gen2DisableWorldBuffs;
            chkEnableWorldBuffScaling.Checked = profile.ArkConfiguration.EnableWorldBuffScaling;
            txtWorldBuffScanlingEfficacy.Text = (profile.ArkConfiguration.WorldBuffScanlingEfficacy).ToString(CultureInfo.InvariantCulture);
            txtMutagemSpawnDelayMultiplier.Text = (profile.ArkConfiguration.MutagemSpawnDelayMultiplier).ToString(CultureInfo.InvariantCulture);
            chkDisableHexagonStore.Checked = profile.ArkConfiguration.DisableHexagonStore;
            chkAllowOnlyEngramPointsTrade.Checked = profile.ArkConfiguration.AllowOnlyEngramPointsTrade;
            txtMaxHexagonsPerCharacter.Text = profile.ArkConfiguration.MaxHexagonsPerCharacter.ToString(CultureInfo.InvariantCulture);
            txtHexagonRewardMultiplier.Text = (profile.ArkConfiguration.HexagonRewardMultiplier).ToString(CultureInfo.InvariantCulture);
            txtHexagonCostMultiplier.Text = (profile.ArkConfiguration.HexagonCostMultiplier).ToString(CultureInfo.InvariantCulture);
            chkAllowMultipleTamedUnicorns.Checked = profile.ArkConfiguration.AllowMultipleTamedUnicorns;
            txtUnicornSpawnInterval.Text = profile.ArkConfiguration.UnicornSpawnInterval.ToString(CultureInfo.InvariantCulture);
            chkEnableVolcano.Checked = profile.ArkConfiguration.EnableVolcano;
            txtVolcanoInterval.Text = (profile.ArkConfiguration.VolcanoInterval).ToString(CultureInfo.InvariantCulture);
            txtVolcanoIntensity.Text = (profile.ArkConfiguration.VolcanoIntensity).ToString(CultureInfo.InvariantCulture);
            chkEnableFjordurSettings.Checked = profile.ArkConfiguration.EnableFjordurSettings;
            chkEnableFjordurBiomeTeleport.Checked = profile.ArkConfiguration.EnableFjordurBiomeTeleport;
            chkEnableGenericQualityClamp.Checked = profile.ArkConfiguration.EnableGenericQualityClamp;
            txtGenericQualityClamp.Text = profile.ArkConfiguration.GenericQualityClamp.ToString(CultureInfo.InvariantCulture);
            chkEnableArmorClamp.Checked = profile.ArkConfiguration.EnableArmorClamp;
            txtArmorClamp.Text = profile.ArkConfiguration.ArmorClamp.ToString(CultureInfo.InvariantCulture);
            chkEnableWeaponDamagePercentClamp.Checked = profile.ArkConfiguration.EnableWeaponDamagePercentClamp;
            txtWeaponDamagePercentClamp.Text = profile.ArkConfiguration.WeaponDamagePercentClamp.ToString(CultureInfo.InvariantCulture);
            chkEnableHypoInsulationClamp.Checked = profile.ArkConfiguration.EnableHypoInsulationClamp;
            txtHypoInsulationClamp.Text = profile.ArkConfiguration.HypoInsulationClamp.ToString(CultureInfo.InvariantCulture);
            chkEnableWeightClamp.Checked = profile.ArkConfiguration.EnableWeightClamp;
            txtWeightClamp.Text = profile.ArkConfiguration.WeightClamp.ToString(CultureInfo.InvariantCulture);
            chkEnableMaxDurabilityClamp.Checked = profile.ArkConfiguration.EnableMaxDurabilityClamp;
            txtMaxDurabilityClamp.Text = profile.ArkConfiguration.MaxDurabilityClamp.ToString(CultureInfo.InvariantCulture);
            chkEnableWeaponClipAmmoClamp.Checked = profile.ArkConfiguration.EnableWeaponClipAmmoClamp;
            txtWeaponClipAmmoClamp.Text = profile.ArkConfiguration.WeaponClipAmmoClamp.ToString(CultureInfo.InvariantCulture);
            chkEnableHyperInsulationClamp.Checked = profile.ArkConfiguration.EnableHyperInsulationClamp;
            txtHyperInsulationClamp.Text = profile.ArkConfiguration.HyperInsulationClamp.ToString(CultureInfo.InvariantCulture);
            chkEnableGlobalVoiceChat.Checked = profile.ArkConfiguration.EnableGlobalVoiceChat;
            chkEnableProximityTextChat.Checked = profile.ArkConfiguration.EnableProximityChat;
            chkEnableLeftNotifications.Checked = profile.ArkConfiguration.EnablePlayerLeaveNotifications;
            chkEnableJoinNotifications.Checked = profile.ArkConfiguration.EnablePlayerJoinedNotifications;
            chkAllowCrossHair.Checked = profile.ArkConfiguration.AllowCrosshair;
            chkAllowHUD.Checked = profile.ArkConfiguration.AllowHUD;
            chkAllowMapPlayerLocation.Checked = profile.ArkConfiguration.AllowMapPlayerLocation;
            chkAllowthirdPerson.Checked = profile.ArkConfiguration.AllowThirdPersonView;
            chkShowFloatingDamage.Checked = profile.ArkConfiguration.ShowFloatingDamageText;
            chkAllowHitMarkers.Checked = profile.ArkConfiguration.AllowHitMarkers;
            chkAllowGammaPvP.Checked = profile.ArkConfiguration.AllowPVPGamma;
            chkAllowGammaPvE.Checked = profile.ArkConfiguration.AllowPvEGamma;
            chkFlyerCarry.Checked = profile.ArkConfiguration.EnableFlyerCarry;
            txtXPMultiplier.Text = profile.ArkConfiguration.XPMultiplier.ToString(CultureInfo.InvariantCulture);
            txtDamage.Text = profile.ArkConfiguration.PlayerDamageMultiplier.ToString(CultureInfo.InvariantCulture);
            txtResistance.Text = profile.ArkConfiguration.PlayerResistanceMultiplier.ToString(CultureInfo.InvariantCulture);
            txtWaterDrain.Text = profile.ArkConfiguration.PlayerCharacterWaterDrainMultiplier.ToString(CultureInfo.InvariantCulture);
            txtFoodDrain.Text = profile.ArkConfiguration.PlayerCharacterFoodDrainMultiplier.ToString(CultureInfo.InvariantCulture);
            txtStaminaDrain.Text = profile.ArkConfiguration.PlayerCharacterStaminaDrainMultiplier.ToString(CultureInfo.InvariantCulture);
            txtHealthRecovery.Text = profile.ArkConfiguration.PlayerCharacterHealthRecoveryMultiplier.ToString(CultureInfo.InvariantCulture);
            txtHarvestDamage.Text = profile.ArkConfiguration.PlayerHarvestingDamageMultiplier.ToString(CultureInfo.InvariantCulture);
            txtCraftingSkillMultiplier.Text = profile.ArkConfiguration.CraftingSkillBonusMultiplier.ToString(CultureInfo.InvariantCulture);
            txtMaxFallSpeed.Text = profile.ArkConfiguration.MaxFallSpeedMultiplier.ToString(CultureInfo.InvariantCulture);
            if (profile.ArkConfiguration.PlayerBaseStatMultipliers.Count != 12) profile.ArkConfiguration.PlayerBaseStatMultipliers.Reset();
            chkBaseStatMultiplier.Checked = profile.ArkConfiguration.PlayerBaseStatMultipliers.IsEnabled;
            txtBSHealth.Text = profile.ArkConfiguration.PlayerBaseStatMultipliers[0].ToString(CultureInfo.InvariantCulture);
            txtBSStamina.Text = profile.ArkConfiguration.PlayerBaseStatMultipliers[1].ToString(CultureInfo.InvariantCulture);
            txtBSTorpidity.Text = profile.ArkConfiguration.PlayerBaseStatMultipliers[2].ToString(CultureInfo.InvariantCulture);
            txtBSOxygen.Text = profile.ArkConfiguration.PlayerBaseStatMultipliers[3].ToString(CultureInfo.InvariantCulture);
            txtBSFood.Text = profile.ArkConfiguration.PlayerBaseStatMultipliers[4].ToString(CultureInfo.InvariantCulture);
            txtBSWater.Text = profile.ArkConfiguration.PlayerBaseStatMultipliers[5].ToString(CultureInfo.InvariantCulture);
            txtBSTemperature.Text = profile.ArkConfiguration.PlayerBaseStatMultipliers[6].ToString(CultureInfo.InvariantCulture);
            txtBSWeigth.Text = profile.ArkConfiguration.PlayerBaseStatMultipliers[7].ToString(CultureInfo.InvariantCulture);
            txtBSDamage.Text = profile.ArkConfiguration.PlayerBaseStatMultipliers[8].ToString(CultureInfo.InvariantCulture);
            txtBSSpeed.Text = profile.ArkConfiguration.PlayerBaseStatMultipliers[9].ToString(CultureInfo.InvariantCulture);
            txtBSFortitude.Text = profile.ArkConfiguration.PlayerBaseStatMultipliers[10].ToString(CultureInfo.InvariantCulture);
            txtBSCrafting.Text = profile.ArkConfiguration.PlayerBaseStatMultipliers[11].ToString(CultureInfo.InvariantCulture);
            if (profile.ArkConfiguration.PerLevelStatsMultiplier_Player.Count != 12) profile.ArkConfiguration.PerLevelStatsMultiplier_Player.Reset();
            chkPerLeveStatMultiplier.Checked = profile.ArkConfiguration.PerLevelStatsMultiplier_Player.IsEnabled;
            txtPLHealth.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_Player[0].ToString(CultureInfo.InvariantCulture);
            txtPLStamina.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_Player[1].ToString(CultureInfo.InvariantCulture);
            txtPLTorpidity.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_Player[2].ToString(CultureInfo.InvariantCulture);
            txtPLOxygen.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_Player[3].ToString(CultureInfo.InvariantCulture);
            txtPLFood.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_Player[4].ToString(CultureInfo.InvariantCulture);
            txtPLWater.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_Player[5].ToString(CultureInfo.InvariantCulture);
            txtPLTemperature.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_Player[6].ToString(CultureInfo.InvariantCulture);
            txtPLWeigth.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_Player[7].ToString(CultureInfo.InvariantCulture);
            txtPLDamage.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_Player[8].ToString(CultureInfo.InvariantCulture);
            txtPLSpeed.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_Player[9].ToString(CultureInfo.InvariantCulture);
            txtPLFortitude.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_Player[10].ToString(CultureInfo.InvariantCulture);
            txtPLCrafting.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_Player[11].ToString(CultureInfo.InvariantCulture);
            txttbMaxTamedDinosServer.Text = profile.ArkConfiguration.MaxTamedDinos.ToString(CultureInfo.InvariantCulture);
            txttbMaxedTamedDinosTribe.Text = profile.ArkConfiguration.MaxPersonalTamedDinos.ToString(CultureInfo.InvariantCulture);
            txttbDinosDamage.Text = profile.ArkConfiguration.DinoDamageMultiplier.ToString(CultureInfo.InvariantCulture);
            txttbDinosTamedDamage.Text = profile.ArkConfiguration.TamedDinoDamageMultiplier.ToString(CultureInfo.InvariantCulture);
            txttbDinosResistance.Text = profile.ArkConfiguration.DinoResistanceMultiplier.ToString(CultureInfo.InvariantCulture);
            txttbDinosTamedResistance.Text = profile.ArkConfiguration.TamedDinoResistanceMultiplier.ToString(CultureInfo.InvariantCulture);
            txttbDinosFoodDrain.Text = profile.ArkConfiguration.WildDinoCharacterFoodDrainMultiplier.ToString(CultureInfo.InvariantCulture);
            txttbDinosTamedFoodDrain.Text = profile.ArkConfiguration.TamedDinoCharacterFoodDrainMultiplier.ToString(CultureInfo.InvariantCulture);
            txttbDinosTorporDrain.Text = profile.ArkConfiguration.WildDinoTorporDrainMultiplier.ToString(CultureInfo.InvariantCulture);
            txttbDinosTamedTorporDrain.Text = profile.ArkConfiguration.TamedDinoTorporDrainMultiplier.ToString(CultureInfo.InvariantCulture);
            txttbDinosPassiveTameInterval.Text = profile.ArkConfiguration.PassiveTameIntervalMultiplier.ToString(CultureInfo.InvariantCulture);
            txttbDinosTamedDinosSaddleStructuresCost.Text = profile.ArkConfiguration.PersonalTamedDinosSaddleStructureCost.ToString(CultureInfo.InvariantCulture);
            chkUseTameLimitForStructuresOnly.Checked = profile.ArkConfiguration.UseTameLimitForStructuresOnly;
            txttbDinosCharacterFoodDrain.Text = profile.ArkConfiguration.DinoCharacterFoodDrainMultiplier.ToString(CultureInfo.InvariantCulture);
            txttbDinosCharacterStaminaDrain.Text = profile.ArkConfiguration.DinoCharacterStaminaDrainMultiplier.ToString(CultureInfo.InvariantCulture);
            txttbDinosCharacterHealthRecovery.Text = profile.ArkConfiguration.DinoCharacterHealthRecoveryMultiplier.ToString(CultureInfo.InvariantCulture);
            txttbDinosHarvestingDamage.Text = profile.ArkConfiguration.DinoHarvestingDamageMultiplier.ToString(CultureInfo.InvariantCulture);
            txttbDinosTurretDamage.Text = profile.ArkConfiguration.DinoTurretDamageMultiplier.ToString(CultureInfo.InvariantCulture);
            chkAllowRaidDinoFeeding.Checked = profile.ArkConfiguration.AllowRaidDinoFeeding;
            txttbDinosRaidFoodDrainMultiplier.Text = profile.ArkConfiguration.RaidDinoCharacterFoodDrainMultiplier.ToString(CultureInfo.InvariantCulture);
            chkAllowFlyersinCaves.Checked = profile.ArkConfiguration.EnableAllowCaveFlyers;
            chkAllowFlyingStaminaRecovery.Checked = profile.ArkConfiguration.AllowFlyingStaminaRecovery;
            chkAllowFlyerSpeedLeveling.Checked = profile.ArkConfiguration.AllowFlyerSpeedLeveling;
            chkPreventDinoMateBoost.Checked = profile.ArkConfiguration.PreventMateBoost;
            chkAllowMultipleAttachedC4.Checked = profile.ArkConfiguration.AllowMultipleAttachedC4;
            chkAllowUnclaimDinos.Checked = profile.ArkConfiguration.AllowUnclaimDinos;
            chkDisableDinoDecayPvE.Checked = profile.ArkConfiguration.DisableDinoDecayPvE;
            chkDisableDinoDecayPvP.Checked = profile.ArkConfiguration.DisableDinoDecayPvP;
            chkAutoDestroyDecayedDinos.Checked = profile.ArkConfiguration.AutoDestroyDecayedDinos;
            chkEnableLevelUpAnimation.Checked = profile.ArkConfiguration.UseDinoLevelUpAnimations;
            txttbPvEDinoDecayPeriod.Text = profile.ArkConfiguration.PvEDinoDecayPeriodMultiplier.ToString(CultureInfo.InvariantCulture);
            chkDisableDinoRiding.Checked = profile.ArkConfiguration.DisableDinoRiding;
            chkDisableDinoTaming.Checked = profile.ArkConfiguration.DisableDinoTaming;
            chkDisableDinoBreeding.Checked = profile.ArkConfiguration.DisableDinoBreeding;
            chkChangeFlyerRiding.Checked = profile.ArkConfiguration.EnableForceCanRideFliers;
            chkEnableFlyerRiding.Checked = profile.ArkConfiguration.ForceCanRideFliers;

            //TODO:FILL DINO COSTUMIZATION
            profile.ArkConfiguration.DinoSettings.Reset();
            foreach (var dinoSetting in profile.ArkConfiguration.DinoSettings) {
                DinoSettingsJSON j = profile.ArkConfiguration.ChangedDinoSettings?.Find(ds => ds.ClassName == dinoSetting.ClassName);
                if (j != null) {
                    dinoSetting.ReplacementClass = j.ReplacementClass;
                    dinoSetting.CanTame = j.CanTame;
                    dinoSetting.CanBreeding = j.CanBreeding;
                    dinoSetting.CanSpawn = j.CanSpawn;
                    dinoSetting.OverrideSpawnLimitPercentage = j.OverrideSpawnLimitPercentage;
                    dinoSetting.SpawnWeightMultiplier = j.SpawnWeightMultiplier;
                    dinoSetting.SpawnLimitPercentage = j.SpawnLimitPercentage;
                    dinoSetting.TamedDamageMultiplier = j.TamedDamageMultiplier;
                    dinoSetting.TamedResistanceMultiplier = j.TamedResistanceMultiplier;
                    dinoSetting.WildDamageMultiplier = j.WildDamageMultiplier;
                    dinoSetting.WildResistanceMultiplier = j.WildResistanceMultiplier;
                }
            }

            bindingSource2.Clear();
            bindingSource2.AddRange(profile.ArkConfiguration.DinoSettings.ToArray());


            var cbo = dataGridViewTextBoxColumn9;
            //cbo.Items.Clear();
            cbo.DataSource = GameData.GetDinoSpawns().ToArray();
            cbo.DisplayMember = "DinoNameTag";
            cbo.ValueMember = "ClassName";

            //dinoSettingsBindingSource.DataSource = profile.ArkConfiguration.DinoSettings;


            if (profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild.Count != 12) profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild.Reset();
            chkPerLevelStatsMultiplierWild.Checked = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild.IsEnabled;
            txttbPerLevelStatsMultiplierWildHealth.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild[0].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierWildStamina.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild[1].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierWildOxygen.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild[2].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierWildFood.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild[3].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierWildTemperature.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild[4].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierWildWeight.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild[5].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierWildDamage.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild[6].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierWildSped.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild[7].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierWildCrafting.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild[8].ToString(CultureInfo.InvariantCulture);
            if (profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed.Count != 12) profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed.Reset();
            chkPerLevelStatsMultiplierTamed.Checked = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed.IsEnabled;
            txttbPerLevelStatsMultiplierTamedHealth.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed[0].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedStamina.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed[1].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedOxygen.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed[2].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedFood.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed[3].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedTemperature.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed[4].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedWeight.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed[5].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedDamage.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed[6].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedSpeed.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed[7].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedCrafting.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed[8].ToString(CultureInfo.InvariantCulture);
            if (profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add.Count != 12) profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add.Reset();
            chkPerLevelStatMultiplierTamedAdd.Checked = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add.IsEnabled;
            txttbPerLevelStatsMultiplierTamedAddHealth.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[0].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedAddStamina.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[1].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedAddTorpidity.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[2].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedAddOxygen.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[3].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedAddFood.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[4].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedAddWater.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[5].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedAddTemperature.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[6].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedAddWeight.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[7].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedAddDamage.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[8].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedAddSpeed.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[9].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedAddFortitude.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[10].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedAddCrafting.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[11].ToString(CultureInfo.InvariantCulture);
            if (profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity.Count != 12) profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity.Reset();
            chkPerLevelStatsMultiplierTamedAffinity.Checked = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity.IsEnabled;
            txttbPerLevelStatsMultiplierTamedAffinityHealth.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[0].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedAffinityStamina.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[1].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedAffinityTorpidity.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[2].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedAffinityOxygen.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[3].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedAffinityFood.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[4].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedAffinityWater.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[5].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedAffinityTemperature.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[6].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedAffinityWeight.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[7].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedAffinityDamage.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[8].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedAffinitySpeed.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[9].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedAffinityFortitude.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[10].ToString(CultureInfo.InvariantCulture);
            txttbPerLevelStatsMultiplierTamedAffinityCrafting.Text = profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[11].ToString(CultureInfo.InvariantCulture);
            if (profile.ArkConfiguration.MutagenLevelBoost.Count != 12) profile.ArkConfiguration.MutagenLevelBoost.Reset();
            chkMutagenLevelBoostWild.Checked = profile.ArkConfiguration.MutagenLevelBoost.IsEnabled;
            txttbMutagenLevelBoostWildHealth.Text = profile.ArkConfiguration.MutagenLevelBoost[0].ToString(CultureInfo.InvariantCulture);
            txttbMutagenLevelBoostWildStamina.Text = profile.ArkConfiguration.MutagenLevelBoost[1].ToString(CultureInfo.InvariantCulture);
            txttbMutagenLevelBoostWildTorpidity.Text = profile.ArkConfiguration.MutagenLevelBoost[2].ToString(CultureInfo.InvariantCulture);
            txttbMutagenLevelBoostWildOxygen.Text = profile.ArkConfiguration.MutagenLevelBoost[3].ToString(CultureInfo.InvariantCulture);
            txttbMutagenLevelBoostWildFood.Text = profile.ArkConfiguration.MutagenLevelBoost[4].ToString(CultureInfo.InvariantCulture);
            txttbMutagenLevelBoostWildWater.Text = profile.ArkConfiguration.MutagenLevelBoost[5].ToString(CultureInfo.InvariantCulture);
            txttbMutagenLevelBoostWildTemperature.Text = profile.ArkConfiguration.MutagenLevelBoost[6].ToString(CultureInfo.InvariantCulture);
            txttbMutagenLevelBoostWildWeight.Text = profile.ArkConfiguration.MutagenLevelBoost[7].ToString(CultureInfo.InvariantCulture);
            txttbMutagenLevelBoostWildDamage.Text = profile.ArkConfiguration.MutagenLevelBoost[8].ToString(CultureInfo.InvariantCulture);
            txttbMutagenLevelBoostWildSpeed.Text = profile.ArkConfiguration.MutagenLevelBoost[9].ToString(CultureInfo.InvariantCulture);
            txttbMutagenLevelBoostWildFortitude.Text = profile.ArkConfiguration.MutagenLevelBoost[10].ToString(CultureInfo.InvariantCulture);
            txttbMutagenLevelBoostWildCrafting.Text = profile.ArkConfiguration.MutagenLevelBoost[11].ToString(CultureInfo.InvariantCulture);
            if (profile.ArkConfiguration.MutagenLevelBoost_Bred.Count != 12) profile.ArkConfiguration.MutagenLevelBoost_Bred.Reset();
            chkMutagenLevelBoostWild.Checked = profile.ArkConfiguration.MutagenLevelBoost_Bred.IsEnabled;
            txttbMutagenLevelBoostBredHealth.Text = profile.ArkConfiguration.MutagenLevelBoost_Bred[0].ToString(CultureInfo.InvariantCulture);
            txttbMutagenLevelBoostBredStamina.Text = profile.ArkConfiguration.MutagenLevelBoost_Bred[1].ToString(CultureInfo.InvariantCulture);
            txttbMutagenLevelBoostBredTorpidity.Text = profile.ArkConfiguration.MutagenLevelBoost_Bred[2].ToString(CultureInfo.InvariantCulture);
            txttbMutagenLevelBoostBredOxygen.Text = profile.ArkConfiguration.MutagenLevelBoost_Bred[3].ToString(CultureInfo.InvariantCulture);
            txttbMutagenLevelBoostBredFood.Text = profile.ArkConfiguration.MutagenLevelBoost_Bred[4].ToString(CultureInfo.InvariantCulture);
            txttbMutagenLevelBoostBredWater.Text = profile.ArkConfiguration.MutagenLevelBoost_Bred[5].ToString(CultureInfo.InvariantCulture);
            txttbMutagenLevelBoostBredTemperature.Text = profile.ArkConfiguration.MutagenLevelBoost_Bred[6].ToString(CultureInfo.InvariantCulture);
            txttbMutagenLevelBoostBredWeigth.Text = profile.ArkConfiguration.MutagenLevelBoost_Bred[7].ToString(CultureInfo.InvariantCulture);
            txttbMutagenLevelBoostBredDamage.Text = profile.ArkConfiguration.MutagenLevelBoost_Bred[8].ToString(CultureInfo.InvariantCulture);
            txttbMutagenLevelBoostBredSpeed.Text = profile.ArkConfiguration.MutagenLevelBoost_Bred[9].ToString(CultureInfo.InvariantCulture);
            txttbMutagenLevelBoostBredFortitude.Text = profile.ArkConfiguration.MutagenLevelBoost_Bred[10].ToString(CultureInfo.InvariantCulture);
            txttbMutagenLevelBoostBredCrafting.Text = profile.ArkConfiguration.MutagenLevelBoost_Bred[11].ToString(CultureInfo.InvariantCulture);
            txttbMatingInterval.Text = profile.ArkConfiguration.MatingIntervalMultiplier.ToString(CultureInfo.InvariantCulture);
            txttbMatingSpeed.Text = profile.ArkConfiguration.MatingSpeedMultiplier.ToString(CultureInfo.InvariantCulture);
            txttbEggHatchSpeed.Text = profile.ArkConfiguration.EggHatchSpeedMultiplier.ToString(CultureInfo.InvariantCulture);
            txttbBabyMatureSpeed.Text = profile.ArkConfiguration.BabyMatureSpeedMultiplier.ToString(CultureInfo.InvariantCulture);
            txttbBabyFoodConsumptionSpeed.Text = profile.ArkConfiguration.BabyFoodConsumptionSpeedMultiplier.ToString(CultureInfo.InvariantCulture);
            chkDisableBabyDinoImprintBuff.Checked = profile.ArkConfiguration.DisableImprintDinoBuff;
            chkAllowBabyDinoImprintCuddleByAnyone.Checked = profile.ArkConfiguration.AllowAnyoneBabyImprintCuddle;
            txttbImprintStatScale.Text = profile.ArkConfiguration.BabyImprintingStatScaleMultiplier.ToString(CultureInfo.InvariantCulture);
            txttbImprintAmountScale.Text = profile.ArkConfiguration.BabyImprintAmountMultiplier.ToString(CultureInfo.InvariantCulture);
            txttbCuddleInterval.Text = profile.ArkConfiguration.BabyCuddleIntervalMultiplier.ToString(CultureInfo.InvariantCulture);
            txttbGracePeriod.Text = profile.ArkConfiguration.BabyCuddleGracePeriodMultiplier.ToString(CultureInfo.InvariantCulture);
            txttbCuddleLoseImprintQualitySpeed.Text = profile.ArkConfiguration.BabyCuddleLoseImprintQualitySpeedMultiplier.ToString(CultureInfo.InvariantCulture);
            txttbMaxImprintLimit.Text = profile.ArkConfiguration.Imprintlimit.ToString(CultureInfo.InvariantCulture);


            ArkProfile ark = profile.ArkConfiguration;

            ucArkEnvironment1.LoadData(ref ark);

            #region Validations

            ManageCheckGroupBox(chkEnableDifficultOverride, groupBox12);
            ManageCheckGroupBox(chkEnableTributeDownloads, groupBox13);
            ManageCheckGroupBox(chkPVESchedule, groupBox16);
            ManageCheckGroupBox(chkIncreasePVPRespawnInterval, groupBox17);
            ManageCheckGroupBox(chkAllowTribeAlliances, groupBox18);
            ManageCheckGroupBox(chkAllowCostumRecipes, groupBox20);
            ManageCheckGroupBox(chkEnableDiseases, groupBox21);
            ManageCheckGroupBox(chkOverrideNPCNetworkStasisRangeScale, groupBox22);
            ManageCheckGroupBox(chkEnableCryopodNerf, groupBox23);
            ManageCheckGroupBox(chkEnableFjordurSettings, groupBox26);
            ManageCheckGroupBox(chkPreventOfflinePVP, groupBox28);
            ManageCheckGroupBox(chkEnableRagnarokSettings, groupBox30);
            ManageCheckGroupBox(chkBaseStatMultiplier, groupBox31);
            ManageCheckGroupBox(chkPerLeveStatMultiplier, groupBox32);
            ManageCheckGroupBox(chkAllowRaidDinoFeeding, groupBox33);
            ManageCheckGroupBox(chkPerLevelStatsMultiplierWild, groupBox36);
            ManageCheckGroupBox(chkPerLevelStatsMultiplierTamed, groupBox37);
            ManageCheckGroupBox(chkPerLevelStatMultiplierTamedAdd, groupBox38);
            ManageCheckGroupBox(chkPerLevelStatsMultiplierTamedAffinity, groupBox39);
            ManageCheckGroupBox(chkMutagenLevelBoostWild, groupBox40);
            ManageCheckGroupBox(chkMutagenLevelBoostBred, groupBox41);

            txtRCONPort.Enabled = chkEnableRCON.Checked;
            txtRCONBuffer.Enabled = chkEnableRCON.Checked;
            tbMOTDInterval.Enabled = chkEnableInterval.Checked;

            if (!Directory.Exists(txtLocation.Text)) {
                btUpdate.Text = "Install";
                _isInstalled = false;
            }
            else {
                if (Utils.IsAValidFolder(txtLocation.Text,
                                         new List<string> { "Engine", "ShooterGame", "steamapps" })) {
                    btUpdate.Text = "Update/Verify";
                    _isInstalled = true;
                }
                else {
                    btUpdate.Text = "Install";
                    _isInstalled = false;
                }
            }

            btStart.Enabled = _isInstalled;
            btRCON.Enabled = _isInstalled;

            cboMap.DataSource = SupportedServers.GetMapLists(profile.Type.ServerType);
            cboMap.ValueMember = "Key";
            cboMap.DisplayMember = "Description";

            #endregion

            txtVersion.Text = profile.GetVersion();
            txtBuild.Text = profile.GetBuild();

            txtCommand.Text =
                profile.ArkConfiguration.GetCommandLinesArguments(MainForm.Settings, profile, MainForm.LocaIp);

            ForceTextBoxValues(Controls);
        }

        private void ForceTrackBarValues(Control.ControlCollection controls) {
            foreach (Control item in controls)
                if (item is TrackBar bar) {
                    bar.Value = bar.Maximum;
                    bar.Value = bar.Minimum;
                }
                else {
                    if (item.HasChildren) ForceTrackBarValues(item.Controls);
                }
        }

        private void ForceTextBoxValues(Control.ControlCollection controls) {
            foreach (Control item in controls)
                if (item is System.Windows.Forms.TextBox txt) {
                    string oldText = txt.Text;
                    txt.Text = "";
                    txt.Text = oldText;
                }
                else {
                    if (item.HasChildren) ForceTextBoxValues(item.Controls);
                }
        }


        private void txtProfileName_Validated(object sender, EventArgs e) {
            _tab.Text = txtProfileName.Text + "          ";
        }

        private void textBox1_DoubleClick(object sender, EventArgs e) {
            txtServerPWD.PasswordChar = txtServerPWD.PasswordChar == '\0' ? '*' : '\0';
        }

        private void textBox2_DoubleClick(object sender, EventArgs e) {
            txtAdminPass.PasswordChar = txtAdminPass.PasswordChar == '\0' ? '*' : '\0';
        }

        private void textBox3_DoubleClick(object sender, EventArgs e) {
            txtSpePwd.PasswordChar = txtSpePwd.PasswordChar == '\0' ? '*' : '\0';
        }

        private void txtServerPort_TextChanged(object sender, EventArgs e) {
            if (int.TryParse(txtServerPort.Text, out int port)) txtPeerPort.Text = (port + 1).ToString();
        }

        private void button5_Click(object sender, EventArgs e) {
            try {
                LoadDefaultFieldValues();

                if (MessageBox.Show("Do you want reload from Server Config Files?", "Reload Option",
                                    MessageBoxButtons.OKCancel) == DialogResult.OK) {
                    _profile.ArkConfiguration = _profile.ArkConfiguration.LoadGameIni(_profile);
                    LoadSettings(_profile, _tab);
                }
                else {
                    string dir = MainForm.Settings.DataFolder + "Profiles\\";
                    if (!Directory.Exists(dir)) return;

                    string[] files = Directory.GetFiles(dir);

                    foreach (string file in files) {
                        var p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(file));
                        if (p.Key == _profile.Key) {
                            LoadSettings(p, _tab);
                            break;
                        }
                    }
                }

                ForceTextBoxValues(Controls);
            }
            catch (Exception exception) {
                OphiussaLogger.Logger.Error(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void btChooseFolder_Click(object sender, EventArgs e) {
            if (!Utils.IsAValidFolder(txtLocation.Text,
                                      new List<string> { "Engine", "ShooterGame", "steamapps" }))
                MessageBox.Show("This is not a valid Ark Folder");
        }

        private void btSave_Click(object sender, EventArgs e) {
            try {
                SaveProfile();
                CreateWindowsTasks();

                MainForm.NotificationController.SendReloadCommand(_profile.Key);
                MessageBox.Show("Profile Saved");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateWindowsTasks() {
            #region AutoStartServer

            if (_profile.AutoManageSettings.AutoStartServer) {
                string fileName = MainForm.Settings.DataFolder + $"StartServer\\Run_{_profile.Key.Replace("-", "")}.cmd";
                string taskName = "OphiussaServerManager\\AutoStart_" + _profile.Key;
                var task = TaskService.Instance.GetTask(taskName);
                if (task != null) {
                    task.Definition.Triggers.Clear();
                    if (_profile.AutoManageSettings.AutoStartOn == AutoStart.OnBoot) {
                        var bt1 = new BootTrigger { Delay = TimeSpan.FromMinutes(1) };
                        task.Definition.Triggers.Add(bt1);
                    }
                    else {
                        var lt1 = new LogonTrigger { Delay = TimeSpan.FromMinutes(1) };
                        task.Definition.Triggers.Add(lt1);
                    }

                    task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                    task.Definition.Settings.Priority = ProcessPriorityClass.Normal;
                    task.RegisterChanges();
                }
                else {
                    var td = TaskService.Instance.NewTask();
                    td.RegistrationInfo.Description = "Server Auto-Start - " + _profile.Name;
                    td.Principal.LogonType = TaskLogonType.InteractiveToken;
                    if (_profile.AutoManageSettings.AutoStartOn == AutoStart.OnBoot) {
                        var bt1 = new BootTrigger { Delay = TimeSpan.FromMinutes(1) };
                        td.Triggers.Add(bt1);
                    }
                    else {
                        var lt1 = new LogonTrigger { Delay = TimeSpan.FromMinutes(1) };
                        td.Triggers.Add(lt1);
                    }

                    td.Actions.Add(fileName);
                    td.Principal.RunLevel = TaskRunLevel.Highest;
                    td.Settings.Priority = ProcessPriorityClass.Normal;
                    TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td);
                }
            }
            else {
                string taskName = "OphiussaServerManager\\AutoStart_" + _profile.Key;
                var task = TaskService.Instance.GetTask(taskName);
                if (task != null) TaskService.Instance.RootFolder.DeleteTask(taskName);
            }

            #endregion

            #region Shutdown 1

            if (_profile.AutoManageSettings.ShutdownServer1) {
                string fileName = Assembly.GetExecutingAssembly().Location;
                string taskName = "OphiussaServerManager\\AutoShutDown1_" + _profile.Key;
                var task = TaskService.Instance.GetTask(taskName);

                if (task != null) {
                    task.Definition.Triggers.Clear();

                    DaysOfTheWeek weekday = 0;

                    if (_profile.AutoManageSettings.ShutdownServer1Monday)
                        weekday += 2;
                    if (_profile.AutoManageSettings.ShutdownServer1Tuesday)
                        weekday += 4;
                    if (_profile.AutoManageSettings.ShutdownServer1Wednesday)
                        weekday += 8;
                    if (_profile.AutoManageSettings.ShutdownServer1Thu)
                        weekday += 16;
                    if (_profile.AutoManageSettings.ShutdownServer1Friday)
                        weekday += 32;
                    if (_profile.AutoManageSettings.ShutdownServer1Saturday)
                        weekday += 64;
                    if (_profile.AutoManageSettings.ShutdownServer1Sunday)
                        weekday += 1;
                    var tt = new WeeklyTrigger();

                    int hour = short.Parse(_profile.AutoManageSettings.ShutdownServer1Hour.Split(':')[0]);
                    int minute = short.Parse(_profile.AutoManageSettings.ShutdownServer1Hour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek = weekday;
                    task.Definition.Triggers.Add(tt);
                    task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                    task.Definition.Settings.Priority = ProcessPriorityClass.Normal;
                    task.RegisterChanges();
                }
                else {
                    var td = TaskService.Instance.NewTask();
                    td.RegistrationInfo.Description = "Server Auto-ShutDown 1 - " + _profile.Name;
                    td.Principal.LogonType = TaskLogonType.InteractiveToken;
                    DaysOfTheWeek weekday = 0;

                    if (_profile.AutoManageSettings.ShutdownServer1Monday)
                        weekday += 2;
                    if (_profile.AutoManageSettings.ShutdownServer1Tuesday)
                        weekday += 4;
                    if (_profile.AutoManageSettings.ShutdownServer1Wednesday)
                        weekday += 8;
                    if (_profile.AutoManageSettings.ShutdownServer1Thu)
                        weekday += 16;
                    if (_profile.AutoManageSettings.ShutdownServer1Friday)
                        weekday += 32;
                    if (_profile.AutoManageSettings.ShutdownServer1Saturday)
                        weekday += 64;
                    if (_profile.AutoManageSettings.ShutdownServer1Sunday)
                        weekday += 1;
                    var tt = new WeeklyTrigger();

                    int hour = short.Parse(_profile.AutoManageSettings.ShutdownServer1Hour.Split(':')[0]);
                    int minute = short.Parse(_profile.AutoManageSettings.ShutdownServer1Hour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek = weekday;
                    td.Triggers.Add(tt);
                    td.Actions.Add(fileName, " -as1" + _profile.Key);
                    td.Principal.RunLevel = TaskRunLevel.Highest;
                    td.Settings.Priority = ProcessPriorityClass.Normal;

                    TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td);
                }
            }
            else {
                string taskName = "OphiussaServerManager\\AutoShutDown1_" + _profile.Key;
                var task = TaskService.Instance.GetTask(taskName);
                if (task != null) TaskService.Instance.RootFolder.DeleteTask(taskName);
            }

            #endregion

            #region Shutdown 2

            if (_profile.AutoManageSettings.ShutdownServer2) {
                string fileName = Assembly.GetExecutingAssembly().Location;
                string taskName = "OphiussaServerManager\\AutoShutDown2_" + _profile.Key;
                var task = TaskService.Instance.GetTask(taskName);
                if (task != null) {
                    task.Definition.Triggers.Clear();

                    DaysOfTheWeek weekday = 0;

                    if (_profile.AutoManageSettings.ShutdownServer2Monday)
                        weekday += 2;
                    if (_profile.AutoManageSettings.ShutdownServer2Tuesday)
                        weekday += 4;
                    if (_profile.AutoManageSettings.ShutdownServer2Wednesday)
                        weekday += 8;
                    if (_profile.AutoManageSettings.ShutdownServer2Thu)
                        weekday += 16;
                    if (_profile.AutoManageSettings.ShutdownServer2Friday)
                        weekday += 32;
                    if (_profile.AutoManageSettings.ShutdownServer2Saturday)
                        weekday += 64;
                    if (_profile.AutoManageSettings.ShutdownServer2Sunday)
                        weekday += 1;
                    var tt = new WeeklyTrigger();

                    int hour = short.Parse(_profile.AutoManageSettings.ShutdownServer2Hour.Split(':')[0]);
                    int minute = short.Parse(_profile.AutoManageSettings.ShutdownServer2Hour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek = weekday;
                    task.Definition.Triggers.Add(tt);
                    task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                    task.Definition.Settings.Priority = ProcessPriorityClass.Normal;
                    task.RegisterChanges();
                }
                else {
                    var td = TaskService.Instance.NewTask();
                    td.RegistrationInfo.Description = "Server Auto-ShutDown 2 - " + _profile.Name;
                    td.Principal.LogonType = TaskLogonType.InteractiveToken;

                    DaysOfTheWeek weekday = 0;

                    if (_profile.AutoManageSettings.ShutdownServer2Monday)
                        weekday += 2;
                    if (_profile.AutoManageSettings.ShutdownServer2Tuesday)
                        weekday += 4;
                    if (_profile.AutoManageSettings.ShutdownServer2Wednesday)
                        weekday += 8;
                    if (_profile.AutoManageSettings.ShutdownServer2Thu)
                        weekday += 16;
                    if (_profile.AutoManageSettings.ShutdownServer2Friday)
                        weekday += 32;
                    if (_profile.AutoManageSettings.ShutdownServer2Saturday)
                        weekday += 64;
                    if (_profile.AutoManageSettings.ShutdownServer2Sunday)
                        weekday += 1;
                    var tt = new WeeklyTrigger();

                    int hour = short.Parse(_profile.AutoManageSettings.ShutdownServer2Hour.Split(':')[0]);
                    int minute = short.Parse(_profile.AutoManageSettings.ShutdownServer2Hour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek = weekday;
                    td.Triggers.Add(tt);
                    td.Actions.Add(fileName, " -as2" + _profile.Key);
                    td.Principal.RunLevel = TaskRunLevel.Highest;
                    td.Settings.Priority = ProcessPriorityClass.Normal;
                    TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td);
                }
            }
            else {
                string taskName = "OphiussaServerManager\\AutoShutDown2_" + _profile.Key;
                var task = TaskService.Instance.GetTask(taskName);
                if (task != null) TaskService.Instance.RootFolder.DeleteTask(taskName);
            }

            #endregion
        }

        private void SaveProfile() {
            if (!MainForm.Settings.Branchs.Contains(cbBranch.Text)) {
                MainForm.Settings.Branchs.Add(cbBranch.Text);
                MainForm.Settings.SaveSettings();
            }

            _profile.Name = txtProfileName.Text;
            _profile.InstallLocation = txtLocation.Text;
            _profile.ArkConfiguration.UseServerApi = chkUseApi.Checked;
            _profile.ArkConfiguration.ServerName = txtServerName.Text;
            _profile.ArkConfiguration.ServerPassword = txtServerPWD.Text;
            _profile.ArkConfiguration.ServerAdminPassword = txtAdminPass.Text;
            _profile.ArkConfiguration.ServerSpectatorPassword = txtSpePwd.Text;
            _profile.ArkConfiguration.LocalIp = txtLocalIP.SelectedValue.ToString();
            _profile.ArkConfiguration.ServerPort = txtServerPort.Text;
            _profile.ArkConfiguration.PeerPort = txtPeerPort.Text;
            _profile.ArkConfiguration.QueryPort = txtQueryPort.Text;
            _profile.ArkConfiguration.UseRcon = chkEnableRCON.Checked;
            _profile.ArkConfiguration.RconPort = txtRCONPort.Text;
            _profile.ArkConfiguration.RconServerLogBuffer = int.Parse(txtRCONBuffer.Text);
            _profile.ArkConfiguration.MapName = cboMap.SelectedValue.ToString();
            _profile.ArkConfiguration.Branch = cbBranch.Text;
            _profile.ArkConfiguration.ActiveMods = txtMods.Text;
            _profile.ArkConfiguration.TotalConversionId = txtTotalConversion.Text;
            _profile.ArkConfiguration.AutoSavePeriod = txtAutoSavePeriod.Text.ToInt();
            _profile.ArkConfiguration.Motd = txtMOTD.Text;
            _profile.ArkConfiguration.ModDuration = txtMOTDDuration.Text.ToInt();
            _profile.ArkConfiguration.ModInterval = txtMOTDInterval.Text.ToInt();
            _profile.ArkConfiguration.EnableInterval = chkEnableInterval.Checked;
            _profile.ArkConfiguration.MaxPlayers = txtMaxPlayers.Text.ToInt();
            _profile.ArkConfiguration.EnablIdleTimeOut = chkEnableIdleTimeout.Checked;
            _profile.ArkConfiguration.IdleTimout = tbIdleTimeout.Text.ToInt();
            _profile.ArkConfiguration.UseBanListUrl = chkUseBanUrl.Checked;
            _profile.ArkConfiguration.BanListUrl = txtBanUrl.Text;
            _profile.ArkConfiguration.DisableVac = chkDisableVAC.Checked;
            _profile.ArkConfiguration.EnableBattleEye = chkEnableBattleEye.Checked;
            _profile.ArkConfiguration.DisablePlayerMovePhysics = chkDisablePlayerMove.Checked;
            _profile.ArkConfiguration.OutputLogToConsole = chkOutputLogToConsole.Checked;
            _profile.ArkConfiguration.UseAllCores = chkUseAllCores.Checked;
            _profile.ArkConfiguration.UseCache = chkUseCache.Checked;
            _profile.ArkConfiguration.NoHandDetection = chkNoHang.Checked;
            _profile.ArkConfiguration.NoDinos = chkNoDinos.Checked;
            _profile.ArkConfiguration.NoUnderMeshChecking = chkNoUnderMeshChecking.Checked;
            _profile.ArkConfiguration.NoUnderMeshKilling = chkNoUnderMeshKilling.Checked;
            _profile.ArkConfiguration.EnableVivox = chkEnableVivox.Checked;
            _profile.ArkConfiguration.AllowSharedConnections = chkAllowSharedConnections.Checked;
            _profile.ArkConfiguration.RespawnDinosOnStartUp = chkRespawnDinosOnStartup.Checked;
            _profile.ArkConfiguration.EnableAutoForceRespawnDinos = chkEnableForceRespawn.Checked;
            _profile.ArkConfiguration.AutoForceRespawnDinosInterval = txtRespawnInterval.Text.ToInt();
            _profile.ArkConfiguration.DisableAntiSpeedHackDetection = chkDisableSpeedHack.Checked;
            _profile.ArkConfiguration.AntiSpeedHackBias = txtSpeedBias.Text.ToInt();
            _profile.ArkConfiguration.ForceDirectX10 = chkForceDX10.Checked;
            _profile.ArkConfiguration.ForceLowMemory = chkForceLowMemory.Checked;
            _profile.ArkConfiguration.ForceNoManSky = chkForceNoManSky.Checked;
            _profile.ArkConfiguration.UseNoMemoryBias = chkUseNoMemoryBias.Checked;
            _profile.ArkConfiguration.StasisKeepController = chkStasisKeepControllers.Checked;
            _profile.ArkConfiguration.ServerAllowAnsel = chkAllowAnsel.Checked;
            _profile.ArkConfiguration.StructureMemoryOptimizations = chkStructuresOptimization.Checked;
            _profile.ArkConfiguration.EnableCrossPlay = chkEnableCrossPlay.Checked;
            _profile.ArkConfiguration.EnablePublicIpForEpic = chkEnableCrossPlay.Checked;
            _profile.ArkConfiguration.EpicStorePlayersOnly = ChkEpicOnly.Checked;
            _profile.ArkConfiguration.AlternateSaveDirectoryName = txtAltSaveDirectory.Text;
            _profile.ArkConfiguration.ClusterId = txtClusterID.Text;
            _profile.ArkConfiguration.ClusterDirectoryOverride = chkClusterOverride.Checked;
            _profile.ArkConfiguration.CpuPriority = (ProcessPriority)cboPriority.SelectedValue;
            _profile.ArkConfiguration.EnableServerAdminLogs = chkEnableServerAdminLogs.Checked;
            _profile.ArkConfiguration.ServerAdminLogsIncludeTribeLogs = chkServerAdminLogsIncludeTribeLogs.Checked;
            _profile.ArkConfiguration.ServerRconOutputTribeLogs = chkServerRCONOutputTribeLogs.Checked;
            _profile.ArkConfiguration.AllowHideDamageSourceFromLogs = chkAllowHideDamageSourceFromLogs.Checked;
            _profile.ArkConfiguration.LogAdminCommandsToPublic = chkLogAdminCommandsToPublic.Checked;
            _profile.ArkConfiguration.LogAdminCommandsToAdmins = chkLogAdminCommandstoAdmins.Checked;
            _profile.ArkConfiguration.TribeLogDestroyedEnemyStructures = chkTribeLogDestroyedEnemyStructures.Checked;
            _profile.ArkConfiguration.MaximumTribeLogs = int.Parse(txtMaximumTribeLogs.Text);

            _profile.AutoManageSettings.AutoStartServer = chkAutoStart.Checked;
            _profile.AutoManageSettings.AutoStartOn = rbOnBoot.Checked ? AutoStart.OnBoot : AutoStart.OnLogin;
            _profile.AutoManageSettings.ShutdownServer1 = chkShutdown1.Checked;
            _profile.AutoManageSettings.ShutdownServer1Hour = txtShutdow1.Text;
            _profile.AutoManageSettings.ShutdownServer1Sunday = chkSun1.Checked;
            _profile.AutoManageSettings.ShutdownServer1Monday = chkMon1.Checked;
            _profile.AutoManageSettings.ShutdownServer1Tuesday = chkTue1.Checked;
            _profile.AutoManageSettings.ShutdownServer1Wednesday = chkWed1.Checked;
            _profile.AutoManageSettings.ShutdownServer1Thu = chkThu1.Checked;
            _profile.AutoManageSettings.ShutdownServer1Friday = chkFri1.Checked;
            _profile.AutoManageSettings.ShutdownServer1Saturday = chkSat1.Checked;
            _profile.AutoManageSettings.ShutdownServer1PerformUpdate = chkUpdate1.Checked;
            _profile.AutoManageSettings.ShutdownServer1Restart = chkRestart1.Checked;
            _profile.AutoManageSettings.ShutdownServer2 = chkShutdown2.Checked;
            _profile.AutoManageSettings.ShutdownServer2Hour = txtShutdow2.Text;
            _profile.AutoManageSettings.ShutdownServer2Sunday = chkSun2.Checked;
            _profile.AutoManageSettings.ShutdownServer2Monday = chkMon2.Checked;
            _profile.AutoManageSettings.ShutdownServer2Tuesday = chkTue2.Checked;
            _profile.AutoManageSettings.ShutdownServer2Wednesday = chkWed2.Checked;
            _profile.AutoManageSettings.ShutdownServer2Thu = chkThu2.Checked;
            _profile.AutoManageSettings.ShutdownServer2Friday = chkFri2.Checked;
            _profile.AutoManageSettings.ShutdownServer2Saturday = chkSat2.Checked;
            _profile.AutoManageSettings.ShutdownServer2PerformUpdate = chkUpdate2.Checked;
            _profile.AutoManageSettings.ShutdownServer2Restart = chkRestart2.Checked;
            _profile.AutoManageSettings.IncludeInAutoBackup = chkIncludeAutoBackup.Checked;
            _profile.AutoManageSettings.IncludeInAutoUpdate = chkAutoUpdate.Checked;
            _profile.AutoManageSettings.AutoStartServer = chkAutoStart.Checked;

            _profile.ArkConfiguration.EnableHardcoreMode = chkEnableHardcoreMode.Checked;
            _profile.ArkConfiguration.DisablePveFriendlyFire = chkDisablePVEFriendlyFire.Checked;
            _profile.ArkConfiguration.DisablePvpFriendlyFire = chkDisablePVPFriendlyFire.Checked;
            _profile.ArkConfiguration.PreventBuildingInResourceRichAreas = chkPreventBuildingInResourceRichAreas.Checked;
            _profile.ArkConfiguration.DisableSupplyCrates = chkDisableSupplyCrates.Checked;
            _profile.ArkConfiguration.EnablePvp = chkEnablePVP.Checked;
            _profile.ArkConfiguration.EnablePveCaveBuilding = chkEnablePVECaveBuilding.Checked;
            _profile.ArkConfiguration.EnablePvpCaveBuilding = chkEnablePVPCaveBuilding.Checked;
            _profile.ArkConfiguration.EnableSinglePlayerSettings = chkEnableSinglePlayerSettings.Checked;
            _profile.ArkConfiguration.AllowCrateSpawnsOnTopOfStructures = chkAllowCrateSpawnsOnTopOfStructures.Checked;
            _profile.ArkConfiguration.EnableCreativeMode = chkEnableCreativeMode.Checked;
            _profile.ArkConfiguration.EnablePveCryoSickness = chkEnablePVECryoSickness.Checked;
            _profile.ArkConfiguration.DisablePvpRailGun = chkDisablePVPRailGun.Checked;
            _profile.ArkConfiguration.DisableCostumTributeFolders = chkDisableCostumTributeFolders.Checked;
            _profile.ArkConfiguration.RandomSupplyCratePoints = chkRandomSupplyCratePoints.Checked;
            _profile.ArkConfiguration.SupplyCrateLootQualityMultiplier = txtSupplyCrateLootQualityMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.FishingLootQualityMultiplier = txtFishingLootQualityMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.UseCorpseLocation = chkUseCorpseLocation.Checked;
            _profile.ArkConfiguration.PreventSpawnAnimations = chkPreventSpawnAnimations.Checked;
            _profile.ArkConfiguration.AllowUnlimitedRespecs = chkAllowUnlimitedRespecs.Checked;
            _profile.ArkConfiguration.AllowPlatformSaddleMultiFloors = chkAllowPlatformSaddleMultiFloors.Checked;
            _profile.ArkConfiguration.PlatformSaddleBuildAreaBoundsMultiplier = txtPlatformSaddleBuildAreaBoundsMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.MaxGatewaysOnSaddles = txtMaxGatewaysOnSaddles.Text.ToInt();
            _profile.ArkConfiguration.EnableDifficultOverride = chkEnableDifficultOverride.Checked;
            _profile.ArkConfiguration.MaxDinoLevel = txtMaxDinoLevel.Text.ToInt();
            _profile.ArkConfiguration.DifficultyOffset = txtDifficultyOffset.Text.ToFloat();
            _profile.ArkConfiguration.DestroyTamesOverLevel = txtDestroyTamesOverLevel.Text.ToInt();
            _profile.ArkConfiguration.EnableTributeDownloads = chkEnableTributeDownloads.Checked;
            _profile.ArkConfiguration.NoSurvivorDownloads = chkNoSurvivorDownloads.Checked;
            _profile.ArkConfiguration.NoItemDownloads = chkNoItemDownloads.Checked;
            _profile.ArkConfiguration.NoDinoDownloads = chkNoDinoDownloads.Checked;
            _profile.ArkConfiguration.AllowForeignDinoDownloads = chkAllowForeignDinoDownloads.Checked;
            _profile.ArkConfiguration.NoSurvivorUploads = chkNoSurvivorUploads.Checked;
            _profile.ArkConfiguration.NoItemUploads = chkNoItemUploads.Checked;
            _profile.ArkConfiguration.NoDinoUploads = chkNoDinoUploads.Checked;
            _profile.ArkConfiguration.LimitMaxTributeDinos = chkLimitMaxTributeDinos.Checked;
            _profile.ArkConfiguration.MaxTributeDinos = txtMaxTributeDinos.Text.ToInt();
            _profile.ArkConfiguration.LimitTributeItems = chkLimitTributeItems.Checked;
            _profile.ArkConfiguration.MaxTributeItems = txtMaxTributeItems.Text.ToInt();
            _profile.ArkConfiguration.NoTransferFromFiltering = chkNoTransferFromFiltering.Checked;
            _profile.ArkConfiguration.OverrideSurvivorUploadExpiration = chkOverrideSurvivorUploadExpiration.Checked;
            _profile.ArkConfiguration.OverrideSurvivorUploadExpirationValue = txtOverrideSurvivorUploadExpirationValue.Text.ToInt();
            _profile.ArkConfiguration.OverrideItemUploadExpiration = chkOverrideItemUploadExpiration.Checked;
            _profile.ArkConfiguration.OverrideItemUploadExpirationValue = txtOverrideItemUploadExpirationValue.Text.ToInt();
            _profile.ArkConfiguration.OverrideDinoUploadExpiration = chkOverrideDinoUploadExpiration.Checked;
            _profile.ArkConfiguration.OverrideDinoUploadExpirationValue = txtOverrideDinoUploadExpirationValue.Text.ToInt();
            _profile.ArkConfiguration.OverrideMinimumDinoReUploadInterval = chkOverrideMinimumDinoReUploadInterval.Checked;
            _profile.ArkConfiguration.OverrideMinimumDinoReUploadIntervalValue = txtOverrideMinimumDinoReUploadIntervalValue.Text.ToInt();
            _profile.ArkConfiguration.PveSchedule = chkPVESchedule.Checked;
            _profile.ArkConfiguration.UseServerTime = chkUseServerTime.Checked;
            _profile.ArkConfiguration.PvpStartTime = txtPVPStartTime.Text.ConvertHourToSeconds();
            _profile.ArkConfiguration.PvpEndTime = txtPVPEndTime.Text.ConvertHourToSeconds();
            _profile.ArkConfiguration.PreventOfflinePvp = chkPreventOfflinePVP.Checked;
            _profile.ArkConfiguration.LogoutInterval = txtLogoutInterval.Text.ToInt();
            _profile.ArkConfiguration.ConnectionInvicibleInterval = txtConnectionInvicibleInterval.Text.ToInt();
            _profile.ArkConfiguration.IncreasePvpRespawnInterval = chkIncreasePVPRespawnInterval.Checked;
            _profile.ArkConfiguration.IntervalCheckPeriod = txtIntervalCheckPeriod.Text.ToInt();
            _profile.ArkConfiguration.IntervalMultiplier = txtIntervalMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.IntervalBaseAmount = txtIntervalBaseAmount.Text.ToInt();
            _profile.ArkConfiguration.MaxPlayersInTribe = txtMaxPlayersInTribe.Text.ToInt();
            _profile.ArkConfiguration.TribeNameChangeCooldDown = txtTribeNameChangeCooldDown.Text.ToInt();
            _profile.ArkConfiguration.TribeSlotReuseCooldown = txtTribeSlotReuseCooldown.Text.ToFloat();
            _profile.ArkConfiguration.AllowTribeAlliances = chkAllowTribeAlliances.Checked;
            _profile.ArkConfiguration.MaxAlliancesPerTribe = txtMaxAlliancesPerTribe.Text.ToInt();
            _profile.ArkConfiguration.MaxTribesPerAlliance = txtMaxTribesPerAlliance.Text.ToInt();
            _profile.ArkConfiguration.AllowTribeWarfare = chkAllowTribeWarfare.Checked;
            _profile.ArkConfiguration.AllowCancelingTribeWarfare = chkAllowCancelingTribeWarfare.Checked;
            _profile.ArkConfiguration.AllowCostumRecipes = chkAllowCostumRecipes.Checked;
            _profile.ArkConfiguration.CostumRecipesEffectivenessMultiplier = txtCostumRecipesEffectivenessMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.CostumRecipesSkillMultiplier = txtCostumRecipesSkillMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.EnableDiseases = chkEnableDiseases.Checked;
            _profile.ArkConfiguration.NonPermanentDiseases = chkNonPermanentDiseases.Checked;
            _profile.ArkConfiguration.OverrideNpcNetworkStasisRangeScale = chkOverrideNPCNetworkStasisRangeScale.Checked;
            _profile.ArkConfiguration.OnlinePlayerCountStart = txtOnlinePlayerCountStart.Text.ToInt();
            _profile.ArkConfiguration.OnlinePlayerCountEnd = txtOnlinePlayerCountEnd.Text.ToInt();
            _profile.ArkConfiguration.ScaleMaximum = txtScaleMaximum.Text.ToFloat();
            _profile.ArkConfiguration.OxygenSwimSpeedStatMultiplier = txtOxygenSwimSpeedStatMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.UseCorpseLifeSpanMultiplier = txtUseCorpseLifeSpanMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.FjordhawkInventoryCooldown = txtFjordhawkInventoryCooldown.Text.ToInt();
            _profile.ArkConfiguration.GlobalPoweredBatteryDurability = txtGlobalPoweredBatteryDurability.Text.ToFloat();
            _profile.ArkConfiguration.FuelConsumptionIntervalMultiplier = txtFuelConsumptionIntervalMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.LimitNonPlayerDroppedItemsRange = txtLimitNonPlayerDroppedItemsRange.Text.ToInt();
            _profile.ArkConfiguration.LimitNonPlayerDroppedItemsCount = txtLimitNonPlayerDroppedItemsCount.Text.ToInt();
            _profile.ArkConfiguration.EnableCryopodNerf = chkEnableCryopodNerf.Checked;
            _profile.ArkConfiguration.EnableCryopodNerfDuration = txtEnableCryopodNerfDuration.Text.ToInt();
            _profile.ArkConfiguration.OutgoingDamageMultiplier = txtOutgoingDamageMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.IncomingDamageMultiplierPercent = txtIncomingDamageMultiplierPercent.Text.ToFloat();
            _profile.ArkConfiguration.Gen1DisableMissions = chkGen1DisableMissions.Checked;
            _profile.ArkConfiguration.Gen1AllowTekSuitPowers = chkGen1AllowTekSuitPowers.Checked;
            _profile.ArkConfiguration.Gen2DisableTekSuitonSpawn = chkGen2DisableTEKSuitonSpawn.Checked;
            _profile.ArkConfiguration.Gen2DisableWorldBuffs = chkGen2DisableWorldBuffs.Checked;
            _profile.ArkConfiguration.EnableWorldBuffScaling = chkEnableWorldBuffScaling.Checked;
            _profile.ArkConfiguration.WorldBuffScanlingEfficacy = txtWorldBuffScanlingEfficacy.Text.ToFloat();
            _profile.ArkConfiguration.MutagemSpawnDelayMultiplier = txtMutagemSpawnDelayMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.DisableHexagonStore = chkDisableHexagonStore.Checked;
            _profile.ArkConfiguration.AllowOnlyEngramPointsTrade = chkAllowOnlyEngramPointsTrade.Checked;
            _profile.ArkConfiguration.MaxHexagonsPerCharacter = txtMaxHexagonsPerCharacter.Text.ToInt();
            _profile.ArkConfiguration.HexagonRewardMultiplier = txtHexagonRewardMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.HexagonCostMultiplier = txtHexagonCostMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.AllowOnlyEngramPointsTrade = chkAllowOnlyEngramPointsTrade.Checked;
            _profile.ArkConfiguration.AllowMultipleTamedUnicorns = chkAllowMultipleTamedUnicorns.Checked;
            _profile.ArkConfiguration.UnicornSpawnInterval = txtUnicornSpawnInterval.Text.ToInt();
            _profile.ArkConfiguration.EnableVolcano = chkEnableVolcano.Checked;
            _profile.ArkConfiguration.VolcanoInterval = txtVolcanoInterval.Text.ToFloat();
            _profile.ArkConfiguration.VolcanoIntensity = txtVolcanoIntensity.Text.ToFloat();
            _profile.ArkConfiguration.EnableFjordurSettings = chkEnableFjordurSettings.Checked;
            _profile.ArkConfiguration.EnableFjordurBiomeTeleport = chkEnableFjordurBiomeTeleport.Checked;
            _profile.ArkConfiguration.EnableGenericQualityClamp = chkEnableGenericQualityClamp.Checked;
            _profile.ArkConfiguration.GenericQualityClamp = txtGenericQualityClamp.Text.ToInt();
            _profile.ArkConfiguration.EnableArmorClamp = chkEnableArmorClamp.Checked;
            _profile.ArkConfiguration.ArmorClamp = txtArmorClamp.Text.ToInt();
            _profile.ArkConfiguration.EnableWeaponDamagePercentClamp = chkEnableWeaponDamagePercentClamp.Checked;
            _profile.ArkConfiguration.WeaponDamagePercentClamp = txtWeaponDamagePercentClamp.Text.ToInt();
            _profile.ArkConfiguration.EnableHypoInsulationClamp = chkEnableHypoInsulationClamp.Checked;
            _profile.ArkConfiguration.HypoInsulationClamp = txtHypoInsulationClamp.Text.ToInt();
            _profile.ArkConfiguration.EnableWeightClamp = chkEnableWeightClamp.Checked;
            _profile.ArkConfiguration.WeightClamp = txtWeightClamp.Text.ToInt();
            _profile.ArkConfiguration.EnableMaxDurabilityClamp = chkEnableMaxDurabilityClamp.Checked;
            _profile.ArkConfiguration.MaxDurabilityClamp = txtMaxDurabilityClamp.Text.ToInt();
            _profile.ArkConfiguration.EnableWeaponClipAmmoClamp = chkEnableWeaponClipAmmoClamp.Checked;
            _profile.ArkConfiguration.WeaponClipAmmoClamp = txtWeaponClipAmmoClamp.Text.ToInt();
            _profile.ArkConfiguration.EnableHyperInsulationClamp = chkEnableHyperInsulationClamp.Checked;
            _profile.ArkConfiguration.HyperInsulationClamp = txtHyperInsulationClamp.Text.ToInt();
            _profile.ArkConfiguration.EnableGlobalVoiceChat = chkEnableGlobalVoiceChat.Checked;
            _profile.ArkConfiguration.EnableProximityChat = chkEnableProximityTextChat.Checked;
            _profile.ArkConfiguration.EnablePlayerLeaveNotifications = chkEnableLeftNotifications.Checked;
            _profile.ArkConfiguration.EnablePlayerJoinedNotifications = chkEnableJoinNotifications.Checked;
            _profile.ArkConfiguration.AllowCrosshair = chkAllowCrossHair.Checked;
            _profile.ArkConfiguration.AllowHUD = chkAllowHUD.Checked;
            _profile.ArkConfiguration.AllowMapPlayerLocation = chkAllowMapPlayerLocation.Checked;
            _profile.ArkConfiguration.AllowThirdPersonView = chkAllowthirdPerson.Checked;
            _profile.ArkConfiguration.ShowFloatingDamageText = chkShowFloatingDamage.Checked;
            _profile.ArkConfiguration.AllowHitMarkers = chkAllowHitMarkers.Checked;
            _profile.ArkConfiguration.AllowPVPGamma = chkAllowGammaPvP.Checked;
            _profile.ArkConfiguration.AllowPvEGamma = chkAllowGammaPvE.Checked;
            _profile.ArkConfiguration.EnableFlyerCarry = chkFlyerCarry.Checked;
            _profile.ArkConfiguration.XPMultiplier = txtXPMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.PlayerDamageMultiplier = txtDamage.Text.ToFloat();
            _profile.ArkConfiguration.PlayerResistanceMultiplier = txtResistance.Text.ToFloat();
            _profile.ArkConfiguration.PlayerCharacterWaterDrainMultiplier = txtWaterDrain.Text.ToFloat();
            _profile.ArkConfiguration.PlayerCharacterFoodDrainMultiplier = txtFoodDrain.Text.ToFloat();
            _profile.ArkConfiguration.PlayerCharacterStaminaDrainMultiplier = txtStaminaDrain.Text.ToFloat();
            _profile.ArkConfiguration.PlayerCharacterHealthRecoveryMultiplier = txtHealthRecovery.Text.ToFloat();
            _profile.ArkConfiguration.PlayerHarvestingDamageMultiplier = txtHarvestDamage.Text.ToFloat();
            _profile.ArkConfiguration.CraftingSkillBonusMultiplier = txtCraftingSkillMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.MaxFallSpeedMultiplier = txtMaxFallSpeed.Text.ToFloat();

            _profile.ArkConfiguration.PlayerBaseStatMultipliers.IsEnabled = chkBaseStatMultiplier.Checked;
            if (_profile.ArkConfiguration.PerLevelStatsMultiplier_Player.IsEnabled) {
                _profile.ArkConfiguration.PlayerBaseStatMultipliers[0] = txtBSHealth.Text.ToFloat();
                _profile.ArkConfiguration.PlayerBaseStatMultipliers[1] = txtBSStamina.Text.ToFloat();
                _profile.ArkConfiguration.PlayerBaseStatMultipliers[2] = txtBSTorpidity.Text.ToFloat();
                _profile.ArkConfiguration.PlayerBaseStatMultipliers[3] = txtBSOxygen.Text.ToFloat();
                _profile.ArkConfiguration.PlayerBaseStatMultipliers[4] = txtBSFood.Text.ToFloat();
                _profile.ArkConfiguration.PlayerBaseStatMultipliers[5] = txtBSWater.Text.ToFloat();
                _profile.ArkConfiguration.PlayerBaseStatMultipliers[6] = txtBSTemperature.Text.ToFloat();
                _profile.ArkConfiguration.PlayerBaseStatMultipliers[7] = txtBSWeigth.Text.ToFloat();
                _profile.ArkConfiguration.PlayerBaseStatMultipliers[8] = txtBSDamage.Text.ToFloat();
                _profile.ArkConfiguration.PlayerBaseStatMultipliers[9] = txtBSSpeed.Text.ToFloat();
                _profile.ArkConfiguration.PlayerBaseStatMultipliers[10] = txtBSFortitude.Text.ToFloat();
                _profile.ArkConfiguration.PlayerBaseStatMultipliers[11] = txtBSCrafting.Text.ToFloat();
            }
            else {
                _profile.ArkConfiguration.PlayerBaseStatMultipliers.Reset();
            }

            _profile.ArkConfiguration.PerLevelStatsMultiplier_Player.IsEnabled = chkPerLeveStatMultiplier.Checked;
            if (_profile.ArkConfiguration.PerLevelStatsMultiplier_Player.IsEnabled) {
                _profile.ArkConfiguration.PerLevelStatsMultiplier_Player[0] = txtPLHealth.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_Player[1] = txtPLStamina.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_Player[2] = txtPLTorpidity.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_Player[3] = txtPLOxygen.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_Player[4] = txtPLFood.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_Player[5] = txtPLWater.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_Player[6] = txtPLTemperature.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_Player[7] = txtPLWeigth.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_Player[8] = txtPLDamage.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_Player[9] = txtPLSpeed.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_Player[10] = txtPLFortitude.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_Player[11] = txtPLCrafting.Text.ToFloat();
            }
            else {
                _profile.ArkConfiguration.PerLevelStatsMultiplier_Player.Reset();
            }

            _profile.ArkConfiguration.MaxTamedDinos = txttbMaxTamedDinosServer.Text.ToInt();
            _profile.ArkConfiguration.MaxPersonalTamedDinos = txttbMaxedTamedDinosTribe.Text.ToInt();
            _profile.ArkConfiguration.DinoDamageMultiplier = txttbDinosDamage.Text.ToInt();
            _profile.ArkConfiguration.DinoDamageMultiplier = txttbDinosTamedDamage.Text.ToFloat();
            _profile.ArkConfiguration.DinoResistanceMultiplier = txttbDinosResistance.Text.ToFloat();
            _profile.ArkConfiguration.TamedDinoResistanceMultiplier = txttbDinosTamedResistance.Text.ToFloat();
            _profile.ArkConfiguration.WildDinoCharacterFoodDrainMultiplier = txttbDinosFoodDrain.Text.ToFloat();
            _profile.ArkConfiguration.TamedDinoCharacterFoodDrainMultiplier = txttbDinosTamedFoodDrain.Text.ToFloat();
            _profile.ArkConfiguration.WildDinoTorporDrainMultiplier = txttbDinosTorporDrain.Text.ToFloat();
            _profile.ArkConfiguration.TamedDinoTorporDrainMultiplier = txttbDinosTamedTorporDrain.Text.ToFloat();
            _profile.ArkConfiguration.PassiveTameIntervalMultiplier = txttbDinosPassiveTameInterval.Text.ToFloat();
            _profile.ArkConfiguration.PersonalTamedDinosSaddleStructureCost = txttbDinosTamedDinosSaddleStructuresCost.Text.ToInt();
            _profile.ArkConfiguration.UseTameLimitForStructuresOnly = chkUseTameLimitForStructuresOnly.Checked;
            _profile.ArkConfiguration.DinoCharacterFoodDrainMultiplier = txttbDinosCharacterFoodDrain.Text.ToFloat();
            _profile.ArkConfiguration.DinoCharacterStaminaDrainMultiplier = txttbDinosCharacterStaminaDrain.Text.ToFloat();
            _profile.ArkConfiguration.DinoCharacterHealthRecoveryMultiplier = txttbDinosCharacterHealthRecovery.Text.ToFloat();
            _profile.ArkConfiguration.DinoHarvestingDamageMultiplier = txttbDinosHarvestingDamage.Text.ToFloat();
            _profile.ArkConfiguration.DinoTurretDamageMultiplier = txttbDinosTurretDamage.Text.ToFloat();
            _profile.ArkConfiguration.AllowRaidDinoFeeding = chkAllowRaidDinoFeeding.Checked;
            _profile.ArkConfiguration.RaidDinoCharacterFoodDrainMultiplier = txttbDinosRaidFoodDrainMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.EnableAllowCaveFlyers = chkAllowFlyersinCaves.Checked;
            _profile.ArkConfiguration.AllowFlyingStaminaRecovery = chkAllowFlyingStaminaRecovery.Checked;
            _profile.ArkConfiguration.AllowFlyerSpeedLeveling = chkAllowFlyerSpeedLeveling.Checked;
            _profile.ArkConfiguration.PreventMateBoost = chkPreventDinoMateBoost.Checked;
            _profile.ArkConfiguration.AllowMultipleAttachedC4 = chkAllowMultipleAttachedC4.Checked;
            _profile.ArkConfiguration.AllowUnclaimDinos = chkAllowUnclaimDinos.Checked;
            _profile.ArkConfiguration.DisableDinoDecayPvE = chkDisableDinoDecayPvE.Checked;
            _profile.ArkConfiguration.DisableDinoDecayPvP = chkDisableDinoDecayPvP.Checked;
            _profile.ArkConfiguration.AutoDestroyDecayedDinos = chkAutoDestroyDecayedDinos.Checked;
            _profile.ArkConfiguration.UseDinoLevelUpAnimations = chkEnableLevelUpAnimation.Checked;
            _profile.ArkConfiguration.PvEDinoDecayPeriodMultiplier = txttbPvEDinoDecayPeriod.Text.ToFloat();
            _profile.ArkConfiguration.DisableDinoRiding = chkDisableDinoRiding.Checked;
            _profile.ArkConfiguration.DisableDinoTaming = chkDisableDinoTaming.Checked;
            _profile.ArkConfiguration.DisableDinoBreeding = chkDisableDinoBreeding.Checked;
            _profile.ArkConfiguration.EnableForceCanRideFliers = chkChangeFlyerRiding.Checked;
            _profile.ArkConfiguration.ForceCanRideFliers = chkEnableFlyerRiding.Checked;


            //TODO:FILL DINO COSTUMIZATION
            _profile.ArkConfiguration.DinoSettings.Clear();
            foreach (object o in bindingSource2.List) {
                _profile.ArkConfiguration.DinoSettings.Add((DinoSettings)o);
            }

            _profile.ArkConfiguration.ChangedDinoSettings = _profile.ArkConfiguration.DinoSettings.GetChangedSettingsList();

            _profile.ArkConfiguration.PreventDinoTameClassNames.Clear();
            foreach (var setting in _profile.ArkConfiguration.ChangedDinoSettings) {
                DinoSettings ds = _profile.ArkConfiguration.DinoSettings.First(f => f.ClassName == setting.ClassName);

                if (ds != null) {
                    if (setting.OverrideSpawnLimitPercentage != ds.OriginalOverrideSpawnLimitPercentage ||
                        setting.SpawnLimitPercentage != ds.OriginalSpawnLimitPercentage ||
                        setting.SpawnWeightMultiplier != ds.OriginalSpawnWeightMultiplier)
                        _profile.ArkConfiguration.DinoSpawnWeightMultipliers.Add(new DinoSpawn() {
                            ClassName = ds.ClassName,
                            OverrideSpawnLimitPercentage = setting.OverrideSpawnLimitPercentage,
                            SpawnLimitPercentage = setting.SpawnLimitPercentage,
                            SpawnWeightMultiplier = setting.SpawnWeightMultiplier,
                            Mod = ds.Mod,
                            KnownDino = ds.KnownDino,
                            DinoNameTag = ds.NameTag
                        });

                    if (setting.TamedDamageMultiplier != DinoSpawn.DEFAULT_SPAWN_WEIGHT_MULTIPLIER)
                        _profile.ArkConfiguration.TamedDinoClassDamageMultipliers.Add(new ClassMultiplier() {
                            ClassName = ds.ClassName,
                            Multiplier = setting.TamedDamageMultiplier
                        });

                    if (setting.TamedResistanceMultiplier != DinoSpawn.DEFAULT_SPAWN_WEIGHT_MULTIPLIER)
                        _profile.ArkConfiguration.TamedDinoClassResistanceMultipliers.Add(new ClassMultiplier() {
                            ClassName = ds.ClassName,
                            Multiplier = setting.TamedResistanceMultiplier
                        });

                    if (setting.WildDamageMultiplier != DinoSpawn.DEFAULT_SPAWN_WEIGHT_MULTIPLIER)
                        _profile.ArkConfiguration.DinoClassDamageMultipliers.Add(new ClassMultiplier() {
                            ClassName = ds.ClassName,
                            Multiplier = setting.WildDamageMultiplier
                        });

                    if (setting.WildResistanceMultiplier != DinoSpawn.DEFAULT_SPAWN_WEIGHT_MULTIPLIER)
                        _profile.ArkConfiguration.DinoClassResistanceMultipliers.Add(new ClassMultiplier() {
                            ClassName = ds.ClassName,
                            Multiplier = setting.WildResistanceMultiplier
                        });


                    if (setting.ClassName != setting.ReplacementClass)
                        _profile.ArkConfiguration.NPCReplacements.Add(new NPCReplacement() {
                            FromClassName = ds.ClassName,
                            ToClassName = setting.ReplacementClass
                        });

                    if (!setting.CanSpawn && ds.IsSpawnable == true) {
                        _profile.ArkConfiguration.PreventDinoTameClassNames.Add(setting.ReplacementClass);
                    }

                    if (!setting.CanTame && ds.IsTameable == DinoTamable.True) {
                        _profile.ArkConfiguration.PreventDinoTameClassNames.Add(setting.ReplacementClass);
                    }

                    if (!setting.CanBreeding && ds.IsBreedingable == DinoBreedingable.True) {
                        _profile.ArkConfiguration.PreventDinoTameClassNames.Add(setting.ReplacementClass);
                    }
                }
            }

            _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild.IsEnabled = chkPerLevelStatsMultiplierWild.Checked;
            if (_profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild.IsEnabled) {
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild[0] = txttbPerLevelStatsMultiplierWildHealth.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild[1] = txttbPerLevelStatsMultiplierWildStamina.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild[2] = txttbPerLevelStatsMultiplierWildOxygen.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild[3] = txttbPerLevelStatsMultiplierWildFood.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild[4] = txttbPerLevelStatsMultiplierWildTemperature.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild[5] = txttbPerLevelStatsMultiplierWildWeight.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild[6] = txttbPerLevelStatsMultiplierWildDamage.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild[7] = txttbPerLevelStatsMultiplierWildSped.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild[8] = txttbPerLevelStatsMultiplierWildCrafting.Text.ToFloat();
            }
            else {
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild.Reset();
            }

            _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed.IsEnabled = chkPerLevelStatsMultiplierTamed.Checked;
            if (_profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed.IsEnabled) {
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed[0] = txttbPerLevelStatsMultiplierTamedHealth.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed[1] = txttbPerLevelStatsMultiplierTamedStamina.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed[2] = txttbPerLevelStatsMultiplierTamedOxygen.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed[3] = txttbPerLevelStatsMultiplierTamedFood.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed[4] = txttbPerLevelStatsMultiplierTamedTemperature.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed[5] = txttbPerLevelStatsMultiplierTamedWeight.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed[6] = txttbPerLevelStatsMultiplierTamedDamage.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed[7] = txttbPerLevelStatsMultiplierTamedSpeed.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed[8] = txttbPerLevelStatsMultiplierTamedCrafting.Text.ToFloat();
            }
            else {
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoWild.Reset();
            }

            _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add.IsEnabled = chkPerLevelStatMultiplierTamedAdd.Checked;
            if (_profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add.IsEnabled) {
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[0] = txttbPerLevelStatsMultiplierTamedAddHealth.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[1] = txttbPerLevelStatsMultiplierTamedAddStamina.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[2] = txttbPerLevelStatsMultiplierTamedAddTorpidity.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[3] = txttbPerLevelStatsMultiplierTamedAddOxygen.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[4] = txttbPerLevelStatsMultiplierTamedAddFood.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[5] = txttbPerLevelStatsMultiplierTamedAddWater.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[6] = txttbPerLevelStatsMultiplierTamedAddTemperature.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[7] = txttbPerLevelStatsMultiplierTamedAddWeight.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[8] = txttbPerLevelStatsMultiplierTamedAddDamage.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[9] = txttbPerLevelStatsMultiplierTamedAddSpeed.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[10] = txttbPerLevelStatsMultiplierTamedAddFortitude.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add[11] = txttbPerLevelStatsMultiplierTamedAddCrafting.Text.ToFloat();
            }
            else {
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Add.Reset();
            }

            _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity.IsEnabled = chkPerLevelStatsMultiplierTamedAffinity.Checked;
            if (_profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity.IsEnabled) {
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[0] = txttbPerLevelStatsMultiplierTamedAffinityHealth.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[1] = txttbPerLevelStatsMultiplierTamedAffinityStamina.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[2] = txttbPerLevelStatsMultiplierTamedAffinityTorpidity.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[3] = txttbPerLevelStatsMultiplierTamedAffinityOxygen.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[4] = txttbPerLevelStatsMultiplierTamedAffinityFood.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[5] = txttbPerLevelStatsMultiplierTamedAffinityWater.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[6] = txttbPerLevelStatsMultiplierTamedAffinityTemperature.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[7] = txttbPerLevelStatsMultiplierTamedAffinityWeight.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[8] = txttbPerLevelStatsMultiplierTamedAffinityDamage.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[9] = txttbPerLevelStatsMultiplierTamedAffinitySpeed.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[10] = txttbPerLevelStatsMultiplierTamedAffinityFortitude.Text.ToFloat();
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity[11] = txttbPerLevelStatsMultiplierTamedAffinityCrafting.Text.ToFloat();
            }
            else {
                _profile.ArkConfiguration.PerLevelStatsMultiplier_DinoTamed_Affinity.Reset();
            }

            _profile.ArkConfiguration.MutagenLevelBoost.IsEnabled = chkMutagenLevelBoostWild.Checked;
            if (_profile.ArkConfiguration.MutagenLevelBoost.IsEnabled) {
                _profile.ArkConfiguration.MutagenLevelBoost[0] = txttbMutagenLevelBoostWildHealth.Text.ToInt();
                _profile.ArkConfiguration.MutagenLevelBoost[1] = txttbMutagenLevelBoostWildStamina.Text.ToInt();
                _profile.ArkConfiguration.MutagenLevelBoost[2] = txttbMutagenLevelBoostWildTorpidity.Text.ToInt();
                _profile.ArkConfiguration.MutagenLevelBoost[3] = txttbMutagenLevelBoostWildOxygen.Text.ToInt();
                _profile.ArkConfiguration.MutagenLevelBoost[4] = txttbMutagenLevelBoostWildFood.Text.ToInt();
                _profile.ArkConfiguration.MutagenLevelBoost[5] = txttbMutagenLevelBoostWildWater.Text.ToInt();
                _profile.ArkConfiguration.MutagenLevelBoost[6] = txttbMutagenLevelBoostWildTemperature.Text.ToInt();
                _profile.ArkConfiguration.MutagenLevelBoost[7] = txttbMutagenLevelBoostWildWeight.Text.ToInt();
                _profile.ArkConfiguration.MutagenLevelBoost[8] = txttbMutagenLevelBoostWildDamage.Text.ToInt();
                _profile.ArkConfiguration.MutagenLevelBoost[9] = txttbMutagenLevelBoostWildSpeed.Text.ToInt();
                _profile.ArkConfiguration.MutagenLevelBoost[10] = txttbMutagenLevelBoostWildFortitude.Text.ToInt();
                _profile.ArkConfiguration.MutagenLevelBoost[11] = txttbMutagenLevelBoostWildCrafting.Text.ToInt();
            }
            else {
                _profile.ArkConfiguration.MutagenLevelBoost.Reset();
            }

            _profile.ArkConfiguration.MutagenLevelBoost_Bred.IsEnabled = chkMutagenLevelBoostBred.Checked;
            if (_profile.ArkConfiguration.MutagenLevelBoost_Bred.IsEnabled) {
                _profile.ArkConfiguration.MutagenLevelBoost_Bred[0] = txttbMutagenLevelBoostBredHealth.Text.ToInt();
                _profile.ArkConfiguration.MutagenLevelBoost_Bred[1] = txttbMutagenLevelBoostBredStamina.Text.ToInt();
                _profile.ArkConfiguration.MutagenLevelBoost_Bred[2] = txttbMutagenLevelBoostBredTorpidity.Text.ToInt();
                _profile.ArkConfiguration.MutagenLevelBoost_Bred[3] = txttbMutagenLevelBoostBredOxygen.Text.ToInt();
                _profile.ArkConfiguration.MutagenLevelBoost_Bred[4] = txttbMutagenLevelBoostBredFood.Text.ToInt();
                _profile.ArkConfiguration.MutagenLevelBoost_Bred[5] = txttbMutagenLevelBoostBredWater.Text.ToInt();
                _profile.ArkConfiguration.MutagenLevelBoost_Bred[6] = txttbMutagenLevelBoostBredTemperature.Text.ToInt();
                _profile.ArkConfiguration.MutagenLevelBoost_Bred[7] = txttbMutagenLevelBoostBredWeigth.Text.ToInt();
                _profile.ArkConfiguration.MutagenLevelBoost_Bred[8] = txttbMutagenLevelBoostBredDamage.Text.ToInt();
                _profile.ArkConfiguration.MutagenLevelBoost_Bred[9] = txttbMutagenLevelBoostBredSpeed.Text.ToInt();
                _profile.ArkConfiguration.MutagenLevelBoost_Bred[10] = txttbMutagenLevelBoostBredFortitude.Text.ToInt();
                _profile.ArkConfiguration.MutagenLevelBoost_Bred[11] = txttbMutagenLevelBoostBredCrafting.Text.ToInt();
            }
            else {
                _profile.ArkConfiguration.MutagenLevelBoost_Bred.Reset();
            }

            _profile.ArkConfiguration.MatingIntervalMultiplier = txttbMatingInterval.Text.ToFloat();
            _profile.ArkConfiguration.MatingSpeedMultiplier = txttbMatingSpeed.Text.ToFloat();
            _profile.ArkConfiguration.EggHatchSpeedMultiplier = txttbEggHatchSpeed.Text.ToFloat();
            _profile.ArkConfiguration.BabyMatureSpeedMultiplier = txttbBabyMatureSpeed.Text.ToFloat();
            _profile.ArkConfiguration.BabyFoodConsumptionSpeedMultiplier = txttbBabyFoodConsumptionSpeed.Text.ToFloat();
            _profile.ArkConfiguration.DisableImprintDinoBuff = chkDisableBabyDinoImprintBuff.Checked = _profile.ArkConfiguration.DisableImprintDinoBuff;
            _profile.ArkConfiguration.AllowAnyoneBabyImprintCuddle = chkAllowBabyDinoImprintCuddleByAnyone.Checked = _profile.ArkConfiguration.AllowAnyoneBabyImprintCuddle;
            _profile.ArkConfiguration.BabyImprintingStatScaleMultiplier = txttbImprintStatScale.Text.ToFloat();
            _profile.ArkConfiguration.BabyImprintAmountMultiplier = txttbImprintAmountScale.Text.ToFloat();
            _profile.ArkConfiguration.BabyCuddleIntervalMultiplier = txttbCuddleInterval.Text.ToFloat();
            _profile.ArkConfiguration.BabyCuddleGracePeriodMultiplier = txttbGracePeriod.Text.ToFloat();
            _profile.ArkConfiguration.BabyCuddleLoseImprintQualitySpeedMultiplier = txttbCuddleLoseImprintQualitySpeed.Text.ToFloat();
            _profile.ArkConfiguration.Imprintlimit = txttbMaxImprintLimit.Text.ToInt();


            ArkProfile ark = _profile.ArkConfiguration;
            ucArkEnvironment1.GetData(ref ark);

            _profile.SaveProfile(MainForm.Settings);

            LoadSettings(_profile, _tab);
        }

        private void chkEnableRCON_CheckedChanged(object sender, EventArgs e) {
            txtRCONPort.Enabled = chkEnableRCON.Checked;
            txtRCONBuffer.Enabled = chkEnableRCON.Checked;
        }

        private void btUpdate_Click(object sender, EventArgs e) {
            SaveProfile();

            var frm = new FrmProgress(MainForm.Settings, _profile);
            frm.ShowDialog();

            LoadSettings(_profile, _tab);
        }

        private void tbAutoSavePeriod_Scroll(object sender, EventArgs e) {
            txtAutoSavePeriod.Text = tbAutoSavePeriod.Value.ToString();
        }

        private void tbMOTDDuration_Scroll(object sender, EventArgs e) {
            txtMOTDDuration.Text = tbMOTDDuration.Value.ToString();
        }

        private void tbMOTDInterval_Scroll(object sender, EventArgs e) {
            txtMOTDInterval.Text = tbMOTDInterval.Value.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            tbMOTDInterval.Enabled = chkEnableInterval.Checked;
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e) {
            tbIdleTimeout.Enabled = chkEnableIdleTimeout.Checked;
        }

        private void tbIdleTimeout_Scroll(object sender, EventArgs e) {
            txtIdleTimeout.Text = tbIdleTimeout.Value.ToString();
        }

        private void tbMaxPlayers_Scroll(object sender, EventArgs e) {
            txtMaxPlayers.Text = tbMaxPlayers.Value.ToString();
        }

        private void btRCON_Click(object sender, EventArgs e) {
            var frm = new FrmRconServer(_profile);
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e) {
            txtCommand.Text =
                _profile.ArkConfiguration.GetCommandLinesArguments(MainForm.Settings, _profile, MainForm.LocaIp);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e) {
        }

        private void chkUseBanUrl_CheckedChanged(object sender, EventArgs e) {
            txtBanUrl.Enabled = chkUseBanUrl.Checked;
        }

        protected virtual bool OnProgressChanged(ProcessEventArg e) {
            OphiussaLogger.Logger.Info(e.Message);
            return true;
        }

        private async void btStart_Click(object sender, EventArgs e) {
            try {
                if (_profile.IsRunning) {
                    //TODO: remove from here and place and use the profile closeserver
                    var x = new AutoUpdate();
                    await x.CloseServer(_profile, MainForm.Settings);
                }
                else {
                    _profile.StartServer(MainForm.Settings);
                }
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void IsRunningProcess() {
            while (true) {
                Process process = null;

                if (_processId == -1)
                    process = _profile.GetExeProcess();
                else
                    try {
                        process = Process.GetProcessById(_processId);
                    }
                    catch (Exception) {
                        // ignored
                    }

                if (process != null) {
                    _processId = process.Id;
                    _isRunning = true;
                }
                else {
                    _processId = -1;
                    _isRunning = false;
                }

                if (!UsefullTools.IsFormRunning("MainForm"))
                    break;
                Thread.Sleep(timerGetProcess.Interval);
            }
        }

        private void timerGetProcess_Tick(object sender, EventArgs e) {
            try {
                timerGetProcess.Enabled = false;

                btStart.Text = _isRunning ? "Stop" : "Start";
                btUpdate.Enabled = !_isRunning;
                btRCON.Enabled = _isRunning && _profile.ArkConfiguration.UseRcon;

                UsefullTools.MainForm.SetTabHeader(_tab, _profile, _isRunning);
                //TabColors[page] = color;
                //MainForm..Invalidate();
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
            }
            finally {
                timerGetProcess.Enabled = true;
            }
        }

        private void checkBox1_CheckedChanged_2(object sender, EventArgs e) {
            rbOnBoot.Enabled = chkAutoStart.Checked;
            rbOnLogin.Enabled = chkAutoStart.Checked;
        }

        private void chkShutdown1_CheckedChanged(object sender, EventArgs e) {
            txtShutdow1.Enabled = chkShutdown1.Checked;
            chkSun1.Enabled = chkShutdown1.Checked;
            chkMon1.Enabled = chkShutdown1.Checked;
            chkTue1.Enabled = chkShutdown1.Checked;
            chkWed1.Enabled = chkShutdown1.Checked;
            chkThu1.Enabled = chkShutdown1.Checked;
            chkFri1.Enabled = chkShutdown1.Checked;
            chkSat1.Enabled = chkShutdown1.Checked;
            chkUpdate1.Enabled = chkShutdown1.Checked;
            chkRestart1.Enabled = chkShutdown1.Checked;
        }

        private void chkShutdown2_CheckedChanged(object sender, EventArgs e) {
            txtShutdow2.Enabled = chkShutdown2.Checked;
            chkSun2.Enabled = chkShutdown2.Checked;
            chkMon2.Enabled = chkShutdown2.Checked;
            chkTue2.Enabled = chkShutdown2.Checked;
            chkWed2.Enabled = chkShutdown2.Checked;
            chkThu2.Enabled = chkShutdown2.Checked;
            chkFri2.Enabled = chkShutdown2.Checked;
            chkSat2.Enabled = chkShutdown2.Checked;
            chkUpdate2.Enabled = chkShutdown2.Checked;
            chkRestart2.Enabled = chkShutdown2.Checked;
        }

        private void btProcessorAffinity_Click(object sender, EventArgs e) {
            var frm = new FrmProcessors(_profile.ArkConfiguration.CpuAffinity == "All",
                                        _profile.ArkConfiguration.CpuAffinityList);
            frm.UpdateCpuAffinity = (all, lst) => {
                _profile.ArkConfiguration.CpuAffinity = all
                                                            ? "All"
                                                            : string.Join(",", lst.FindAll(x => x.Selected).Select(x => x.ProcessorNumber.ToString()));
                _profile.ArkConfiguration.CpuAffinityList = lst;
                txtAffinity.Text = _profile.ArkConfiguration.CpuAffinity;
            };
            frm.ShowDialog();
        }

        private void btMods_Click(object sender, EventArgs e) {
            var frm = new FrmModManager();
            frm.UpdateModList = lst => { txtMods.Text = string.Join(",", lst.Select(x => x.ModId.ToString()).ToArray()); };

            frm.LoadMods(ref _profile, txtMods.Text);
        }

        private void expandCollapsePanel3_Paint(object sender, PaintEventArgs e) {
        }

        private void ManageCheckGroupBox(CheckBox chk, GroupBox grp) {
            // Make sure the CheckBox isn't in the GroupBox.
            // This will only happen the first time.
            if (chk.Parent == grp) {
                // Reparent the CheckBox so it's not in the GroupBox.
                grp.Parent.Controls.Add(chk);

                // Adjust the CheckBox's location.
                chk.Location = new System.Drawing.Point(
                                                       chk.Left + grp.Left,
                                                       chk.Top + grp.Top);

                // Move the CheckBox to the top of the stacking order.
                chk.BringToFront();
            }

            // Enable or disable the GroupBox.
            grp.Enabled = chk.Checked;
        }

        private void tbSupplyCrateLootQualityMultiplier_Scroll(object sender, EventArgs e) {
            txtSupplyCrateLootQualityMultiplier.Text = (((TrackBar)sender).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbFishingLootQualityMultiplier_Scroll(object sender, EventArgs e) {
            txtFishingLootQualityMultiplier.Text = (((TrackBar)sender).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPlatformSaddleBuildAreaBoundsMultiplier_Scroll(object sender, EventArgs e) {
            txtPlatformSaddleBuildAreaBoundsMultiplier.Text = (((TrackBar)sender).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMaxGatewaysOnSaddles_Scroll(object sender, EventArgs e) {
            txtMaxGatewaysOnSaddles.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbMaxDinoLevel_Scroll(object sender, EventArgs e) {
            txtMaxDinoLevel.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbDifficultyOffset_Scroll(object sender, EventArgs e) {
            txtDifficultyOffset.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbDestroyTamesOverLevel_Scroll(object sender, EventArgs e) {
            txtDestroyTamesOverLevel.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbMaxTributeDinos_Scroll(object sender, EventArgs e) {
            txtMaxTributeDinos.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbMaxTributeItems_Scroll(object sender, EventArgs e) {
            txtMaxTributeItems.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbOverrideSurvivorUploadExpirationValue_Scroll(object sender, EventArgs e) {
            txtOverrideSurvivorUploadExpirationValue.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbOverrideItemUploadExpirationValue_Scroll(object sender, EventArgs e) {
            txtOverrideItemUploadExpirationValue.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbOverrideDinoUploadExpirationValue_Scroll(object sender, EventArgs e) {
            txtOverrideDinoUploadExpirationValue.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbOverrideMinimumDinoReUploadIntervalValue_Scroll(object sender, EventArgs e) {
            txtOverrideMinimumDinoReUploadIntervalValue.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbLogoutInterval_Scroll(object sender, EventArgs e) {
            txtLogoutInterval.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbConnectionInvicibleInterval_Scroll(object sender, EventArgs e) {
            txtConnectionInvicibleInterval.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbIntervalCheckPeriod_Scroll(object sender, EventArgs e) {
            txtIntervalCheckPeriod.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbIntervalMultiplier_Scroll(object sender, EventArgs e) {
            txtIntervalMultiplier.Text = (((TrackBar)sender).Value / 10.0f).ToString();
        }

        private void tbIntervalBaseAmount_Scroll(object sender, EventArgs e) {
            txtIntervalBaseAmount.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbMaxPlayersInTribe_Scroll(object sender, EventArgs e) {
            txtMaxPlayersInTribe.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbTribeNameChangeCooldDown_Scroll(object sender, EventArgs e) {
            txtTribeNameChangeCooldDown.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbTribeSlotReuseCooldown_Scroll(object sender, EventArgs e) {
            txtTribeSlotReuseCooldown.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbMaxAlliancesPerTribe_Scroll(object sender, EventArgs e) {
            txtMaxAlliancesPerTribe.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbMaxTribesPerAlliance_Scroll(object sender, EventArgs e) {
            txtMaxTribesPerAlliance.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbCostumRecipesEffectivenessMultiplier_Scroll(object sender, EventArgs e) {
            txtCostumRecipesEffectivenessMultiplier.Text = (((TrackBar)sender).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbCostumRecipesSkillMultiplier_Scroll(object sender, EventArgs e) {
            txtCostumRecipesSkillMultiplier.Text = (((TrackBar)sender).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbOnlinePlayerCountStart_Scroll(object sender, EventArgs e) {
            txtOnlinePlayerCountStart.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbOnlinePlayerCountEnd_Scroll(object sender, EventArgs e) {
            txtOnlinePlayerCountEnd.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbScaleMaximum_Scroll(object sender, EventArgs e) {
            txtScaleMaximum.Text = (((TrackBar)sender).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbOxygenSwimSpeedStatMultiplier_Scroll(object sender, EventArgs e) {
            txtOxygenSwimSpeedStatMultiplier.Text = (((TrackBar)sender).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbUseCorpseLifeSpanMultiplier_Scroll(object sender, EventArgs e) {
            txtUseCorpseLifeSpanMultiplier.Text = (((TrackBar)sender).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbFjordhawkInventoryCooldown_Scroll(object sender, EventArgs e) {
            txtFjordhawkInventoryCooldown.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbGlobalPoweredBatteryDurability_Scroll(object sender, EventArgs e) {
            txtGlobalPoweredBatteryDurability.Text = (((TrackBar)sender).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbFuelConsumptionIntervalMultiplier_Scroll(object sender, EventArgs e) {
            txtFuelConsumptionIntervalMultiplier.Text = (((TrackBar)sender).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbLimitNonPlayerDroppedItemsRange_Scroll(object sender, EventArgs e) {
            txtLimitNonPlayerDroppedItemsRange.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbLimitNonPlayerDroppedItemsCount_Scroll(object sender, EventArgs e) {
            txtLimitNonPlayerDroppedItemsCount.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbEnableCryopodNerfDuration_Scroll(object sender, EventArgs e) {
            txtEnableCryopodNerfDuration.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbOutgoingDamageMultiplier_Scroll(object sender, EventArgs e) {
            txtOutgoingDamageMultiplier.Text = (((TrackBar)sender).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbIncomingDamageMultiplierPercent_Scroll(object sender, EventArgs e) {
            txtIncomingDamageMultiplierPercent.Text = (((TrackBar)sender).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbWorldBuffScanlingEfficacy_Scroll(object sender, EventArgs e) {
            txtWorldBuffScanlingEfficacy.Text = (((TrackBar)sender).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagemSpawnDelayMultiplier_Scroll(object sender, EventArgs e) {
            txtMutagemSpawnDelayMultiplier.Text = (((TrackBar)sender).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMaxHexagonsPerCharacter_Scroll(object sender, EventArgs e) {
            txtMaxHexagonsPerCharacter.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbHexagonRewardMultiplier_Scroll(object sender, EventArgs e) {
            txtHexagonRewardMultiplier.Text = (((TrackBar)sender).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbHexagonCostMultiplier_Scroll(object sender, EventArgs e) {
            txtHexagonCostMultiplier.Text = (((TrackBar)sender).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbUnicornSpawnInterval_Scroll(object sender, EventArgs e) {
            txtUnicornSpawnInterval.Text = ((TrackBar)sender)?.Value.ToString();
        }

        private void tbVolcanoInterval_Scroll(object sender, EventArgs e) {
            txtVolcanoInterval.Text = (((TrackBar)sender).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbVolcanoIntensity_Scroll(object sender, EventArgs e) {
            txtVolcanoIntensity.Text = (((TrackBar)sender).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbGenericQualityClamp_Scroll(object sender, EventArgs e) {
            txtGenericQualityClamp.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbArmorClamp_Scroll(object sender, EventArgs e) {
            txtArmorClamp.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbWeaponDamagePercentClamp_Scroll(object sender, EventArgs e) {
            txtWeaponDamagePercentClamp.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbHypoInsulationClamp_Scroll(object sender, EventArgs e) {
            txtHypoInsulationClamp.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbWeightClamp_Scroll(object sender, EventArgs e) {
            txtWeightClamp.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbMaxDurabilityClamp_Scroll(object sender, EventArgs e) {
            txtMaxDurabilityClamp.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbWeaponClipAmmoClamp_Scroll(object sender, EventArgs e) {
            txtWeaponClipAmmoClamp.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbHyperInsulationClamp_Scroll(object sender, EventArgs e) {
            txtHyperInsulationClamp.Text = ((TrackBar)sender).Value.ToString();
        }

        private void chkEnableDifficultOverride_CheckedChanged(object sender, EventArgs e) {
            ManageCheckGroupBox((CheckBox)sender, groupBox12);
        }

        private void chkEnableTributeDownloads_CheckedChanged(object sender, EventArgs e) {
            ManageCheckGroupBox((CheckBox)sender, groupBox13);
        }

        private void chkPVESchedule_CheckedChanged(object sender, EventArgs e) {
            ManageCheckGroupBox((CheckBox)sender, groupBox16);
        }

        private void chkPreventOfflinePVP_CheckedChanged(object sender, EventArgs e) {
            ManageCheckGroupBox((CheckBox)sender, groupBox28);
        }

        private void chkAllowTribeAlliances_CheckedChanged(object sender, EventArgs e) {
            ManageCheckGroupBox((CheckBox)sender, groupBox18);
        }

        private void chkEnableDiseases_CheckedChanged(object sender, EventArgs e) {
            ManageCheckGroupBox((CheckBox)sender, groupBox21);
        }

        private void chkOverrideNPCNetworkStasisRangeScale_CheckedChanged(object sender, EventArgs e) {
            ManageCheckGroupBox((CheckBox)sender, groupBox22);
        }

        private void chkEnableCryopodNerf_CheckedChanged(object sender, EventArgs e) {
            ManageCheckGroupBox((CheckBox)sender, groupBox23);
        }

        private void chkIncreasePVPRespawnInterval_CheckedChanged(object sender, EventArgs e) {
            ManageCheckGroupBox((CheckBox)sender, groupBox17);
        }

        private void chkEnableRagnarokSettings_CheckedChanged(object sender, EventArgs e) {
            ManageCheckGroupBox((CheckBox)sender, groupBox30);
        }

        private void chkEnableFjordurSettings_CheckedChanged(object sender, EventArgs e) {
            ManageCheckGroupBox((CheckBox)sender, groupBox26);
        }

        private void chkAllowCostumRecipes_CheckedChanged(object sender, EventArgs e) {
            ManageCheckGroupBox((CheckBox)sender, groupBox20);
        }

        private void txtSupplyCrateLootQualityMultiplier_TextChanged(object sender, EventArgs e) {
        }

        private void txtFishingLootQualityMultiplier_Validated(object sender, EventArgs e) {
        }

        private void txtSupplyCrateLootQualityMultiplier_TextChanged_1(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 10.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbSupplyCrateLootQualityMultiplier.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtFishingLootQualityMultiplier_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 10.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbFishingLootQualityMultiplier.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtAutoSavePeriod_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbAutoSavePeriod.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtMOTDDuration_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMOTDDuration.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtMOTDInterval_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMOTDInterval.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtIdleTimeout_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbIdleTimeout.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtMaxPlayers_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMaxPlayers.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtSpeedBias_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbSpeedBias.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtRespawnInterval_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbRespawnInterval.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtPlatformSaddleBuildAreaBoundsMultiplier_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 10.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPlatformSaddleBuildAreaBoundsMultiplier.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtMaxGatewaysOnSaddles_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMaxGatewaysOnSaddles.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtMaxDinoLevel_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMaxDinoLevel.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtDifficultyOffset_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbDifficultyOffset.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtDestroyTamesOverLevel_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbDestroyTamesOverLevel.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtTribeSlotReuseCooldown_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbTribeSlotReuseCooldown.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtTribeNameChangeCooldDown_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbTribeNameChangeCooldDown.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtMaxPlayersInTribe_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMaxPlayersInTribe.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtIntervalBaseAmount_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbIntervalBaseAmount.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtIntervalMultiplier_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbIntervalMultiplier.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtIntervalCheckPeriod_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbIntervalCheckPeriod.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtConnectionInvicibleInterval_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbConnectionInvicibleInterval.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtLogoutInterval_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbLogoutInterval.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtOverrideMinimumDinoReUploadIntervalValue_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbOverrideMinimumDinoReUploadIntervalValue.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtOverrideDinoUploadExpirationValue_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbOverrideDinoUploadExpirationValue.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtOverrideItemUploadExpirationValue_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbOverrideItemUploadExpirationValue.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtOverrideSurvivorUploadExpirationValue_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbOverrideSurvivorUploadExpirationValue.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtMaxTributeItems_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMaxTributeItems.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtMaxTributeDinos_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMaxTributeDinos.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtMaxAlliancesPerTribe_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMaxAlliancesPerTribe.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtMaxTribesPerAlliance_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMaxTribesPerAlliance.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtCostumRecipesEffectivenessMultiplier_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbCostumRecipesEffectivenessMultiplier.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtCostumRecipesSkillMultiplier_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbCostumRecipesSkillMultiplier.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtOnlinePlayerCountStart_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbOnlinePlayerCountStart.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtOnlinePlayerCountEnd_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbOnlinePlayerCountEnd.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtScaleMaximum_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 10.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbScaleMaximum.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtOxygenSwimSpeedStatMultiplier_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 10f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbOxygenSwimSpeedStatMultiplier.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtUseCorpseLifeSpanMultiplier_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 10f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbUseCorpseLifeSpanMultiplier.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtFjordhawkInventoryCooldown_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbFjordhawkInventoryCooldown.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtGlobalPoweredBatteryDurability_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbGlobalPoweredBatteryDurability.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtFuelConsumptionIntervalMultiplier_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbFuelConsumptionIntervalMultiplier.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtLimitNonPlayerDroppedItemsRange_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbLimitNonPlayerDroppedItemsRange.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtLimitNonPlayerDroppedItemsCount_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbLimitNonPlayerDroppedItemsCount.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtEnableCryopodNerfDuration_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbEnableCryopodNerfDuration.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtOutgoingDamageMultiplier_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 10f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbOutgoingDamageMultiplier.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtIncomingDamageMultiplierPercent_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 10f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbIncomingDamageMultiplierPercent.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtWorldBuffScanlingEfficacy_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 10f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbWorldBuffScanlingEfficacy.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtMutagemSpawnDelayMultiplier_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 10f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagemSpawnDelayMultiplier.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtMaxHexagonsPerCharacter_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMaxHexagonsPerCharacter.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtHexagonRewardMultiplier_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 10f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbHexagonRewardMultiplier.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtHexagonCostMultiplier_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 10f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbHexagonCostMultiplier.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtUnicornSpawnInterval_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbUnicornSpawnInterval.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtVolcanoInterval_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 10f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbVolcanoInterval.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtVolcanoIntensity_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 10f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbVolcanoIntensity.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtGenericQualityClamp_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbGenericQualityClamp.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtArmorClamp_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbArmorClamp.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtWeaponDamagePercentClamp_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbWeaponDamagePercentClamp.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtHypoInsulationClamp_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbHypoInsulationClamp.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtWeightClamp_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbWeightClamp.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtMaxDurabilityClamp_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMaxDurabilityClamp.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtWeaponClipAmmoClamp_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbWeaponClipAmmoClamp.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtHyperInsulationClamp_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat();
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbHyperInsulationClamp.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e) {
            ManageCheckGroupBox((System.Windows.Forms.CheckBox)sender, groupBox32);
        }

        private void chkBaseStatMultiplier_CheckedChanged(object sender, EventArgs e) {
            ManageCheckGroupBox((System.Windows.Forms.CheckBox)sender, groupBox31);
        }

        private void txtXPMultiplier_TextChanged_1(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbXPMultiplier.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtDamage_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbDamage.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtResistance_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbResistance.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtWaterDrain_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbWaterDrain.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtFoodDrain_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbFoodDrain.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtStaminaDrain_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbStaminaDrain.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtHealthRecovery_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbHealthRecovery.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtHarvestDamage_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbHarvestDamage.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtCraftingSkillMultiplier_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbCraftingSkillMultiplier.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void expandCollapsePanel6_Paint(object sender, PaintEventArgs e) {
        }

        private void txtMaxFallSpeed_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMaxFallSpeed.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtBSHealth_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbBSHealth.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtBSStamina_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbBSStamina.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtBSTorpidity_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbBSTorpidity.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtBSOxygen_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbBSOxygen.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtBSFood_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbBSFood.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtBSWater_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbBSWater.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtBSTemperature_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbBSTemperature.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtBSWeigth_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbBSWeigth.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtBSDamage_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbBSDamage.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtBSSpeed_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbBSSpeed.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtBSFortitude_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbBSFortitude.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtBSCrafting_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbBSCrafting.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtPLHealth_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPLHealth.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtPLStamina_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPLStamina.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtPLTorpidity_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPLTorpidity.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtPLOxygen_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPLOxygen.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtPLFood_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPLFood.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtPLWater_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPLWater.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtPLTemperature_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPLTemperature.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtPLWeigth_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPLWeight.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtPLDamage_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPLDamage.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtPLSpeed_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPLSpeed.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtPLFortitude_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPLFortitude.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtPLCrafting_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPLCrafting.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void tbXPMultiplier_Scroll(object sender, EventArgs e) {
            txtXPMultiplier.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbDamage_Scroll(object sender, EventArgs e) {
            txtDamage.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbResistance_Scroll(object sender, EventArgs e) {
            txtResistance.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbWaterDrain_Scroll(object sender, EventArgs e) {
            txtWaterDrain.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbFoodDrain_Scroll(object sender, EventArgs e) {
            txtFoodDrain.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbStaminaDrain_Scroll(object sender, EventArgs e) {
            txtStaminaDrain.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbHealthRecovery_Scroll(object sender, EventArgs e) {
            txtHealthRecovery.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbHarvestDamage_Scroll(object sender, EventArgs e) {
            txtHarvestDamage.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbCraftingSkillMultiplier_Scroll(object sender, EventArgs e) {
            txtCraftingSkillMultiplier.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMaxFallSpeed_Scroll(object sender, EventArgs e) {
            txtMaxFallSpeed.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbBSHealth_Scroll(object sender, EventArgs e) {
            txtBSHealth.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbBSStamina_Scroll(object sender, EventArgs e) {
            txtBSStamina.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbBSTorpidity_Scroll(object sender, EventArgs e) {
            txtBSTorpidity.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbBSOxygen_Scroll(object sender, EventArgs e) {
            txtBSOxygen.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbBSFood_Scroll(object sender, EventArgs e) {
            txtBSFood.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbBSWater_Scroll(object sender, EventArgs e) {
            txtBSWater.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbBSTemperature_Scroll(object sender, EventArgs e) {
            txtBSTemperature.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbBSWeigth_Scroll(object sender, EventArgs e) {
            txtBSWeigth.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbBSDamage_Scroll(object sender, EventArgs e) {
            txtBSDamage.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbBSSpeed_Scroll(object sender, EventArgs e) {
            txtBSSpeed.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbBSFortitude_Scroll(object sender, EventArgs e) {
            txtBSFortitude.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbBSCrafting_Scroll(object sender, EventArgs e) {
            txtBSCrafting.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPLHealth_Scroll(object sender, EventArgs e) {
            txtPLHealth.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPLStamina_Scroll(object sender, EventArgs e) {
            txtPLStamina.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPLTorpidity_Scroll(object sender, EventArgs e) {
            txtPLTorpidity.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPLOxygen_Scroll(object sender, EventArgs e) {
            txtPLOxygen.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPLFood_Scroll(object sender, EventArgs e) {
            txtPLFood.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPLWater_Scroll(object sender, EventArgs e) {
            txtPLWater.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPLTemperature_Scroll(object sender, EventArgs e) {
            txtPLTemperature.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPLWeight_Scroll(object sender, EventArgs e) {
            txtPLWeigth.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPLDamage_Scroll(object sender, EventArgs e) {
            txtPLDamage.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPLSpeed_Scroll(object sender, EventArgs e) {
            txtPLSpeed.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPLFortitude_Scroll(object sender, EventArgs e) {
            txtPLFortitude.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPLCrafting_Scroll(object sender, EventArgs e) {
            txtPLCrafting.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void txttbMaxTamedDinosServer_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMaxTamedDinosServer.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMaxedTamedDinosTribe_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMaxedTamedDinosTribe.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbDinosDamage_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbDinosDamage.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbDinosTamedDamage_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 10.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbDinosTamedDamage.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbDinosResistance_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbDinosResistance.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbDinosTamedResistance_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbDinosTamedResistance.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbDinosFoodDrain_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbDinosFoodDrain.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbDinosTamedFoodDrain_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbDinosTamedFoodDrain.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbDinosTorporDrain_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbDinosTorporDrain.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbDinosTamedTorporDrain_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbDinosTamedTorporDrain.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbDinosPassiveTameInterval_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbDinosPassiveTameInterval.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbDinosTamedDinosSaddleStructuresCost_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbDinosTamedDinosSaddleStructuresCost.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbDinosCharacterFoodDrain_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbDinosCharacterFoodDrain.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbDinosCharacterStaminaDrain_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbDinosCharacterStaminaDrain.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbDinosCharacterHealthRecovery_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbDinosCharacterHealthRecovery.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbDinosHarvestingDamage_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbDinosHarvestingDamage.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbDinosTurretDamage_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbDinosTurretDamage.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbDinosRaidFoodDrainMultiplier_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbDinosRaidFoodDrainMultiplier.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPvEDinoDecayPeriod_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPvEDinoDecayPeriod.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierWildHealth_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierWildHealth.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierWildStamina_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierWildStamina.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierWildOxygen_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierWildOxygen.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierWildFood_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierWildFood.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierWildTemperature_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierWildTemperature.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierWildWeight_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierWildWeight.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierWildDamage_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierWildDamage.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierWildSped_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierWildSped.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierWildCrafting_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierWildCrafting.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedHealth_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedHealth.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedStamina_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedStamina.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedOxygen_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedOxygen.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedFood_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedFood.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedTemperature_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedTemperature.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedWeight_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedWeight.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedDamage_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedDamage.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedSpeed_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedSpeed.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedCrafting_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedCrafting.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAddHealth_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAddHealth.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAddStamina_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAddStamina.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAddTorpidity_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAddTorpidity.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAddOxygen_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAddOxygen.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAddFood_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAddFood.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAddWater_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAddWater.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAddTemperature_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAddTemperature.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAddWeight_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAddWeight.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAddDamage_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAddDamage.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAddSpeed_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAddSpeed.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAddFortitude_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAddFortitude.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAddCrafting_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAddCrafting.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAffinityHealth_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAffinityHealth.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAffinityStamina_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAffinityHealth.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAffinityTorpidity_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAffinityTorpidity.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAffinityOxygen_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAffinityOxygen.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAffinityFood_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAffinityFood.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAffinityWater_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAffinityWater.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAffinityTemperature_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAffinityTemperature.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAffinityWeight_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAffinityWeight.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAffinityDamage_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAffinityDamage.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAffinitySpeed_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAffinitySpeed.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAffinityFortitude_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAffinityFortitude.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbPerLevelStatsMultiplierTamedAffinityCrafting_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbPerLevelStatsMultiplierTamedAffinityCrafting.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostWildHealth_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostWildHealth.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostWildStamina_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostWildStamina.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostWildTorpidity_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostWildTorpidity.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostWildOxygen_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostWildOxygen.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostWildFood_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostWildFood.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostWildWater_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostWildWater.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostWildTemperature_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostWildTemperature.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostWildWeight_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostWildWeight.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostWildDamage_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostWildDamage.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostWildSpeed_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostWildSpeed.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostWildFortitude_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostWildFortitude.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostWildCrafting_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostWildCrafting.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostBredHealth_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostBredHealth.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostBredStamina_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostBredStamina.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostBredTorpidity_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostBredTorpidity.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostBredOxygen_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostBredOxygen.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostBredFood_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostBredFood.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostBredWater_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostBredWater.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostBredTemperature_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostBredTemperature.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostBredWeigth_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostBredWeigth.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostBredDamage_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostBredDamage.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostBredSpeed_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostBredSpeed.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostBredFortitude_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostBredFortitude.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMutagenLevelBoostBredCrafting_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMutagenLevelBoostBredCrafting.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMatingInterval_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMatingInterval.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMatingSpeed_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMatingSpeed.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbEggHatchSpeed_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbEggHatchSpeed.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbBabyMatureSpeed_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbBabyMatureSpeed.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbBabyFoodConsumptionSpeed_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbBabyFoodConsumptionSpeed.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbImprintStatScale_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbImprintStatScale.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbImprintAmountScale_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbImprintAmountScale.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbCuddleInterval_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbCuddleInterval.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbGracePeriod_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbGracePeriod.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbCuddleLoseImprintQualitySpeed_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbCuddleLoseImprintQualitySpeed.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txttbMaxImprintLimit_TextChanged(object sender, EventArgs e) {
            try {
                float fValue = ((System.Windows.Forms.TextBox)sender).Text.ToFloat() * 100.0f;
                string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
                tbMaxImprintLimit.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void tbMaxImprintLimit_Scroll(object sender, EventArgs e) {
            txttbMaxImprintLimit.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbCuddleLoseImprintQualitySpeed_Scroll(object sender, EventArgs e) {
            txttbCuddleLoseImprintQualitySpeed.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbGracePeriod_Scroll(object sender, EventArgs e) {
            txttbGracePeriod.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbCuddleInterval_Scroll(object sender, EventArgs e) {
            txttbCuddleInterval.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbImprintAmountScale_Scroll(object sender, EventArgs e) {
            txttbImprintAmountScale.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbImprintStatScale_Scroll(object sender, EventArgs e) {
            txttbImprintStatScale.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbBabyFoodConsumptionSpeed_Scroll(object sender, EventArgs e) {
            txttbBabyFoodConsumptionSpeed.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbBabyMatureSpeed_Scroll(object sender, EventArgs e) {
            txttbBabyMatureSpeed.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbEggHatchSpeed_Scroll(object sender, EventArgs e) {
            txttbEggHatchSpeed.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMatingSpeed_Scroll(object sender, EventArgs e) {
            txttbMatingSpeed.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMatingInterval_Scroll(object sender, EventArgs e) {
            txttbMatingInterval.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostBredCrafting_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostBredCrafting.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostBredFortitude_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostBredFortitude.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostBredSpeed_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostBredSpeed.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostBredDamage_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostBredDamage.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostBredWeigth_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostBredWeigth.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostBredTemperature_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostBredTemperature.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostBredWater_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostBredWater.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostBredFood_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostBredFood.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostBredOxygen_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostBredOxygen.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostBredTorpidity_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostBredTorpidity.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostBredStamina_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostBredStamina.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostBredHealth_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostBredHealth.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostWildCrafting_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostWildCrafting.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostWildFortitude_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostWildFortitude.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostWildSpeed_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostWildSpeed.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostWildDamage_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostWildDamage.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostWildWeight_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostWildWeight.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostWildTemperature_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostWildTemperature.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostWildWater_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostWildWater.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostWildFood_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostWildFood.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostWildOxygen_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostWildOxygen.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostWildTorpidity_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostWildTorpidity.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostWildStamina_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostWildStamina.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagenLevelBoostWildHealth_Scroll(object sender, EventArgs e) {
            txttbMutagenLevelBoostWildHealth.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAffinityCrafting_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAffinityCrafting.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAffinityFortitude_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAffinityFortitude.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAffinitySpeed_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAffinitySpeed.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAffinityDamage_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAffinityDamage.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAffinityWeight_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAffinityWeight.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAffinityTemperature_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAffinityTemperature.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAffinityWater_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAffinityWater.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAffinityFood_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAffinityFood.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAffinityOxygen_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAffinityOxygen.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAffinityTorpidity_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAffinityTorpidity.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAffinityStamina_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAffinityStamina.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAffinityHealth_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAffinityHealth.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAddCrafting_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAddCrafting.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAddFortitude_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAddFortitude.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAddSpeed_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAddSpeed.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAddDamage_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAddDamage.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAddWeight_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAddWeight.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAddTemperature_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAddTemperature.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAddWater_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAddWater.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAddFood_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAddFood.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAddOxygen_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAddOxygen.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAddTorpidity_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAddTorpidity.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAddStamina_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAddStamina.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedAddHealth_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedAddHealth.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedCrafting_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedCrafting.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedSpeed_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedSpeed.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedDamage_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedDamage.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedWeight_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedWeight.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedTemperature_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedTemperature.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedFood_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedFood.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedOxygen_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedOxygen.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedStamina_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedStamina.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierTamedHealth_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierTamedHealth.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierWildCrafting_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierWildCrafting.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierWildSped_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierWildSped.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierWildDamage_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierWildDamage.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierWildWeight_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierWildWeight.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierWildTemperature_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierWildTemperature.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierWildFood_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierWildFood.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierWildOxygen_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierWildOxygen.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierWildStamina_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierWildStamina.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPerLevelStatsMultiplierWildHealth_Scroll(object sender, EventArgs e) {
            txttbPerLevelStatsMultiplierWildHealth.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPvEDinoDecayPeriod_Scroll(object sender, EventArgs e) {
            txttbPvEDinoDecayPeriod.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbDinosRaidFoodDrainMultiplier_Scroll(object sender, EventArgs e) {
            txttbDinosRaidFoodDrainMultiplier.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbDinosTurretDamage_Scroll(object sender, EventArgs e) {
            txttbDinosTurretDamage.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbDinosHarvestingDamage_Scroll(object sender, EventArgs e) {
            txttbDinosHarvestingDamage.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbDinosCharacterHealthRecovery_Scroll(object sender, EventArgs e) {
            txttbDinosCharacterHealthRecovery.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbDinosCharacterStaminaDrain_Scroll(object sender, EventArgs e) {
            txttbDinosCharacterStaminaDrain.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbDinosCharacterFoodDrain_Scroll(object sender, EventArgs e) {
            txttbDinosCharacterFoodDrain.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbDinosTamedDinosSaddleStructuresCost_Scroll(object sender, EventArgs e) {
            txttbDinosTamedDinosSaddleStructuresCost.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbDinosPassiveTameInterval_Scroll(object sender, EventArgs e) {
            txttbDinosPassiveTameInterval.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbDinosTamedTorporDrain_Scroll(object sender, EventArgs e) {
            txttbDinosTamedTorporDrain.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbDinosTorporDrain_Scroll(object sender, EventArgs e) {
            txttbDinosTorporDrain.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbDinosTamedFoodDrain_Scroll(object sender, EventArgs e) {
            txttbDinosTamedFoodDrain.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbDinosFoodDrain_Scroll(object sender, EventArgs e) {
            txttbDinosFoodDrain.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbDinosTamedResistance_Scroll(object sender, EventArgs e) {
            txttbDinosTamedResistance.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbDinosResistance_Scroll(object sender, EventArgs e) {
            txttbDinosResistance.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbDinosTamedDamage_Scroll(object sender, EventArgs e) {
            txttbDinosTamedDamage.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbDinosDamage_Scroll(object sender, EventArgs e) {
            txttbDinosDamage.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMaxedTamedDinosTribe_Scroll(object sender, EventArgs e) {
            txttbMaxedTamedDinosTribe.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMaxTamedDinosServer_Scroll(object sender, EventArgs e) {
            txttbMaxTamedDinosServer.Text = (((TrackBar)sender).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void chkPerLevelStatsMultiplierWild_CheckedChanged(object sender, EventArgs e) {
            ManageCheckGroupBox(chkPerLevelStatsMultiplierWild, groupBox36);
        }

        private void chkPerLevelStatsMultiplierTamed_CheckedChanged(object sender, EventArgs e) {
            ManageCheckGroupBox(chkPerLevelStatsMultiplierTamed, groupBox37);
        }

        private void chkPerLevelStatMultiplierTamedAdd_CheckedChanged(object sender, EventArgs e) {
            ManageCheckGroupBox(chkPerLevelStatMultiplierTamedAdd, groupBox38);
        }

        private void chkPerLevelStatsMultiplierTamedAffinity_CheckedChanged(object sender, EventArgs e) {
            ManageCheckGroupBox(chkPerLevelStatsMultiplierTamedAffinity, groupBox39);
        }

        private void chkMutagenLevelBoostWild_CheckedChanged(object sender, EventArgs e) {
            ManageCheckGroupBox(chkMutagenLevelBoostWild, groupBox40);
        }

        private void chkMutagenLevelBoostBred_CheckedChanged(object sender, EventArgs e) {
            ManageCheckGroupBox(chkMutagenLevelBoostBred, groupBox41);
        }

        private void chkAllowRaidDinoFeeding_CheckedChanged(object sender, EventArgs e) {
            ManageCheckGroupBox(chkAllowRaidDinoFeeding, groupBox33);
        }

        private void chkSpawnMultiplier_CheckedChanged(object sender, EventArgs e) {
            throw new System.NotImplementedException();
        }

        private void chkTamedDamage_CheckedChanged(object sender, EventArgs e) {
            throw new System.NotImplementedException();
        }

        private void chkTamedResistance_CheckedChanged(object sender, EventArgs e) {
            throw new System.NotImplementedException();
        }

        private void chkWildDamage_CheckedChanged(object sender, EventArgs e) {
            throw new System.NotImplementedException();
        }

        private void chkWildResistance_CheckedChanged(object sender, EventArgs e) {
            throw new System.NotImplementedException();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e) {
            try {
                object value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                if (!((DataGridViewComboBoxColumn)dataGridView1.Columns[e.ColumnIndex]).Items.Contains((value))) {
                    ((DataGridViewComboBoxColumn)dataGridView1.Columns[e.ColumnIndex]).Items.Add(value);
                }
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
            }
        }

        private void btSync_Click(object sender, EventArgs e) {

        }

        private void rbOnBoot_CheckedChanged(object sender, EventArgs e) {

        }
    }
}