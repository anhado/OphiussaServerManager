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
using TextBox = System.Windows.Controls.TextBox;

namespace OphiussaServerManager.Forms {
    public partial class FrmArk : Form {
        private bool _isRunning;
        private int _processId = -1;
        private Profile _profile;
        private TabPage _tab;
        public bool IsInstalled;

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

                cboPriority.DataSource = Enum.GetValues(typeof(ProcessPriorityClass));
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                MessageBox.Show("LoadDefaultFieldValues:" + e.Message);
            }
        }

        public void LoadSettings(Profile profile, TabPage tab) {
            _profile = profile;
            _tab = tab;
            LoadDefaultFieldValues();

            ForceTrackBarValues(Controls);

            txtProfileID.Text = profile.Key;
            txtProfileName.Text = profile.Name;
            tab.Text = txtProfileName.Text + "          ";
            txtServerType.Text = profile.Type.ServerTypeDescription;
            txtLocation.Text = profile.InstallLocation;
            chkUseApi.Checked = profile.ArkConfiguration.Administration.UseServerApi;
            txtServerName.Text = profile.ArkConfiguration.Administration.ServerName;
            txtServerPWD.Text = profile.ArkConfiguration.Administration.ServerPassword;
            txtAdminPass.Text = profile.ArkConfiguration.Administration.ServerAdminPassword;
            txtSpePwd.Text = profile.ArkConfiguration.Administration.ServerSpectatorPassword;
            txtLocalIP.SelectedValue = profile.ArkConfiguration.Administration.LocalIp;
            txtServerPort.Text = profile.ArkConfiguration.Administration.ServerPort;
            txtPeerPort.Text = profile.ArkConfiguration.Administration.PeerPort;
            txtQueryPort.Text = profile.ArkConfiguration.Administration.QueryPort;
            chkEnableRCON.Checked = profile.ArkConfiguration.Administration.UseRcon;
            txtRCONPort.Text = profile.ArkConfiguration.Administration.RconPort;
            txtRCONBuffer.Text = profile.ArkConfiguration.Administration.RconServerLogBuffer.ToString(CultureInfo.InvariantCulture);
            cboMap.SelectedValue = profile.ArkConfiguration.Administration.MapName;
            cbBranch.Text = profile.ArkConfiguration.Administration.Branch;
            txtMods.Text = string.Join(",", profile.ArkConfiguration.Administration.ModIDs.ToArray());
            txtTotalConversion.Text = profile.ArkConfiguration.Administration.TotalConversionId;
            txtAutoSavePeriod.Text = profile.ArkConfiguration.Administration.AutoSavePeriod.ToString(CultureInfo.InvariantCulture);
            txtMOTD.Text = profile.ArkConfiguration.Administration.Mod;
            txtMOTDDuration.Text = profile.ArkConfiguration.Administration.ModDuration.ToString(CultureInfo.InvariantCulture);
            txtMOTDInterval.Text = profile.ArkConfiguration.Administration.ModInterval.ToString(CultureInfo.InvariantCulture);
            chkEnableInterval.Checked = profile.ArkConfiguration.Administration.EnableInterval;
            txtMaxPlayers.Text = profile.ArkConfiguration.Administration.MaxPlayers.ToString(CultureInfo.InvariantCulture);
            chkEnableIdleTimeout.Checked = profile.ArkConfiguration.Administration.EnablIdleTimeOut;
            txtIdleTimeout.Text = profile.ArkConfiguration.Administration.IdleTimout.ToString(CultureInfo.InvariantCulture);
            chkUseBanUrl.Checked = profile.ArkConfiguration.Administration.UseBanListUrl;
            txtBanUrl.Text = profile.ArkConfiguration.Administration.BanListUrl;
            chkDisableVAC.Checked = profile.ArkConfiguration.Administration.DisableVac;
            chkEnableBattleEye.Checked = profile.ArkConfiguration.Administration.EnableBattleEye;
            chkDisablePlayerMove.Checked = profile.ArkConfiguration.Administration.DisablePlayerMovePhysics;
            chkOutputLogToConsole.Checked = profile.ArkConfiguration.Administration.OutputLogToConsole;
            chkUseAllCores.Checked = profile.ArkConfiguration.Administration.UseAllCores;
            chkUseCache.Checked = profile.ArkConfiguration.Administration.UseCache;
            chkNoHang.Checked = profile.ArkConfiguration.Administration.NoHandDetection;
            chkNoDinos.Checked = profile.ArkConfiguration.Administration.NoDinos;
            chkNoUnderMeshChecking.Checked = profile.ArkConfiguration.Administration.NoUnderMeshChecking;
            chkNoUnderMeshKilling.Checked = profile.ArkConfiguration.Administration.NoUnderMeshKilling;
            chkEnableVivox.Checked = profile.ArkConfiguration.Administration.EnableVivox;
            chkAllowSharedConnections.Checked = profile.ArkConfiguration.Administration.AllowSharedConnections;
            chkRespawnDinosOnStartup.Checked = profile.ArkConfiguration.Administration.RespawnDinosOnStartUp;
            chkEnableForceRespawn.Checked = profile.ArkConfiguration.Administration.EnableAutoForceRespawnDinos;
            txtRespawnInterval.Text = profile.ArkConfiguration.Administration.AutoForceRespawnDinosInterval.ToString(CultureInfo.InvariantCulture);
            chkDisableSpeedHack.Checked = profile.ArkConfiguration.Administration.DisableAntiSpeedHackDetection;
            txtSpeedBias.Text = profile.ArkConfiguration.Administration.AntiSpeedHackBias.ToString(CultureInfo.InvariantCulture);
            chkForceDX10.Checked = profile.ArkConfiguration.Administration.ForceDirectX10;
            chkForceLowMemory.Checked = profile.ArkConfiguration.Administration.ForceLowMemory;
            chkForceNoManSky.Checked = profile.ArkConfiguration.Administration.ForceNoManSky;
            chkUseNoMemoryBias.Checked = profile.ArkConfiguration.Administration.UseNoMemoryBias;
            chkStasisKeepControllers.Checked = profile.ArkConfiguration.Administration.StasisKeepController;
            chkAllowAnsel.Checked = profile.ArkConfiguration.Administration.ServerAllowAnsel;
            chkStructuresOptimization.Checked = profile.ArkConfiguration.Administration.StructureMemoryOptimizations;
            chkEnableCrossPlay.Checked = profile.ArkConfiguration.Administration.EnableCrossPlay;
            chkEnableCrossPlay.Checked = profile.ArkConfiguration.Administration.EnablePublicIpForEpic;
            ChkEpicOnly.Checked = profile.ArkConfiguration.Administration.EpicStorePlayersOnly;
            txtAltSaveDirectory.Text = profile.ArkConfiguration.Administration.AlternateSaveDirectoryName;
            txtClusterID.Text = profile.ArkConfiguration.Administration.ClusterId;
            chkClusterOverride.Checked = profile.ArkConfiguration.Administration.ClusterDirectoryOverride;
            cboPriority.SelectedItem = profile.ArkConfiguration.Administration.CpuPriority;
            txtAffinity.Text = profile.ArkConfiguration.Administration.CpuAffinity;
            chkEnableServerAdminLogs.Checked = profile.ArkConfiguration.Administration.EnableServerAdminLogs;
            chkServerAdminLogsIncludeTribeLogs.Checked = profile.ArkConfiguration.Administration.ServerAdminLogsIncludeTribeLogs;
            chkServerRCONOutputTribeLogs.Checked = profile.ArkConfiguration.Administration.ServerRconOutputTribeLogs;
            chkAllowHideDamageSourceFromLogs.Checked = profile.ArkConfiguration.Administration.AllowHideDamageSourceFromLogs;
            chkLogAdminCommandsToPublic.Checked = profile.ArkConfiguration.Administration.LogAdminCommandsToPublic;
            chkLogAdminCommandstoAdmins.Checked = profile.ArkConfiguration.Administration.LogAdminCommandsToAdmins;
            chkTribeLogDestroyedEnemyStructures.Checked = profile.ArkConfiguration.Administration.TribeLogDestroyedEnemyStructures;
            txtMaximumTribeLogs.Text = profile.ArkConfiguration.Administration.MaximumTribeLogs.ToString(CultureInfo.InvariantCulture);

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

            chkEnableHardcoreMode.Checked = profile.ArkConfiguration.Rules.EnableHardcoreMode;
            chkDisablePVEFriendlyFire.Checked = profile.ArkConfiguration.Rules.DisablePveFriendlyFire;
            chkDisablePVPFriendlyFire.Checked = profile.ArkConfiguration.Rules.DisablePvpFriendlyFire;
            chkPreventBuildingInResourceRichAreas.Checked = profile.ArkConfiguration.Rules.PreventBuildingInResourceRichAreas;
            chkDisableSupplyCrates.Checked = profile.ArkConfiguration.Rules.DisableSupplyCrates;
            chkEnablePVP.Checked = profile.ArkConfiguration.Rules.EnablePvp;
            chkEnablePVECaveBuilding.Checked = profile.ArkConfiguration.Rules.EnablePveCaveBuilding;
            chkEnablePVPCaveBuilding.Checked = profile.ArkConfiguration.Rules.EnablePvpCaveBuilding;
            chkEnableSinglePlayerSettings.Checked = profile.ArkConfiguration.Rules.EnableSinglePlayerSettings;
            chkAllowCrateSpawnsOnTopOfStructures.Checked = profile.ArkConfiguration.Rules.AllowCrateSpawnsOnTopOfStructures;
            chkEnableCreativeMode.Checked = profile.ArkConfiguration.Rules.EnableCreativeMode;
            chkEnablePVECryoSickness.Checked = profile.ArkConfiguration.Rules.EnablePveCryoSickness;
            chkDisablePVPRailGun.Checked = profile.ArkConfiguration.Rules.DisablePvpRailGun;
            chkDisableCostumTributeFolders.Checked = profile.ArkConfiguration.Rules.DisableCostumTributeFolders;
            chkRandomSupplyCratePoints.Checked = profile.ArkConfiguration.Rules.RandomSupplyCratePoints;
            txtSupplyCrateLootQualityMultiplier.Text = (profile.ArkConfiguration.Rules.SupplyCrateLootQualityMultiplier * 10.0f).ToString(CultureInfo.InvariantCulture);
            txtFishingLootQualityMultiplier.Text = (profile.ArkConfiguration.Rules.FishingLootQualityMultiplier * 10.0f).ToString(CultureInfo.InvariantCulture);
            chkUseCorpseLocation.Checked = profile.ArkConfiguration.Rules.UseCorpseLocation;
            chkPreventSpawnAnimations.Checked = profile.ArkConfiguration.Rules.PreventSpawnAnimations;
            chkAllowUnlimitedRespecs.Checked = profile.ArkConfiguration.Rules.AllowUnlimitedRespecs;
            chkAllowPlatformSaddleMultiFloors.Checked = profile.ArkConfiguration.Rules.AllowPlatformSaddleMultiFloors;
            txtPlatformSaddleBuildAreaBoundsMultiplier.Text = (profile.ArkConfiguration.Rules.PlatformSaddleBuildAreaBoundsMultiplier * 10.0f).ToString(CultureInfo.InvariantCulture);
            txtMaxGatewaysOnSaddles.Text = profile.ArkConfiguration.Rules.MaxGatewaysOnSaddles.ToString(CultureInfo.InvariantCulture);
            chkEnableDifficultOverride.Checked = profile.ArkConfiguration.Rules.EnableDifficultOverride;
            txtMaxDinoLevel.Text = profile.ArkConfiguration.Rules.MaxDinoLevel.ToString(CultureInfo.InvariantCulture);
            txtDifficultyOffset.Text = (profile.ArkConfiguration.Rules.DifficultyOffset * 100.0f).ToString(CultureInfo.InvariantCulture);
            txtDestroyTamesOverLevel.Text = profile.ArkConfiguration.Rules.DestroyTamesOverLevel.ToString(CultureInfo.InvariantCulture);
            chkEnableTributeDownloads.Checked = profile.ArkConfiguration.Rules.EnableTributeDownloads;
            chkNoSurvivorDownloads.Checked = profile.ArkConfiguration.Rules.NoSurvivorDownloads;
            chkNoItemDownloads.Checked = profile.ArkConfiguration.Rules.NoItemDownloads;
            chkNoDinoDownloads.Checked = profile.ArkConfiguration.Rules.NoDinoDownloads;
            chkAllowForeignDinoDownloads.Checked = profile.ArkConfiguration.Rules.AllowForeignDinoDownloads;
            chkNoSurvivorUploads.Checked = profile.ArkConfiguration.Rules.NoSurvivorUploads;
            chkNoItemUploads.Checked = profile.ArkConfiguration.Rules.NoItemUploads;
            chkNoDinoUploads.Checked = profile.ArkConfiguration.Rules.NoDinoUploads;
            chkLimitMaxTributeDinos.Checked = profile.ArkConfiguration.Rules.LimitMaxTributeDinos;
            txtMaxTributeDinos.Text = profile.ArkConfiguration.Rules.MaxTributeDinos.ToString(CultureInfo.InvariantCulture);
            chkLimitTributeItems.Checked = profile.ArkConfiguration.Rules.LimitTributeItems;
            txtMaxTributeItems.Text = profile.ArkConfiguration.Rules.MaxTributeItems.ToString(CultureInfo.InvariantCulture);
            chkNoTransferFromFiltering.Checked = profile.ArkConfiguration.Rules.NoTransferFromFiltering;
            chkOverrideSurvivorUploadExpiration.Checked = profile.ArkConfiguration.Rules.OverrideSurvivorUploadExpiration;
            txtOverrideSurvivorUploadExpirationValue.Text = profile.ArkConfiguration.Rules.OverrideSurvivorUploadExpirationValue.ToString(CultureInfo.InvariantCulture);
            chkOverrideItemUploadExpiration.Checked = profile.ArkConfiguration.Rules.OverrideItemUploadExpiration;
            txtOverrideItemUploadExpirationValue.Text = profile.ArkConfiguration.Rules.OverrideItemUploadExpirationValue.ToString(CultureInfo.InvariantCulture);
            chkOverrideDinoUploadExpiration.Checked = profile.ArkConfiguration.Rules.OverrideDinoUploadExpiration;
            txtOverrideDinoUploadExpirationValue.Text = profile.ArkConfiguration.Rules.OverrideDinoUploadExpirationValue.ToString(CultureInfo.InvariantCulture);
            chkOverrideMinimumDinoReUploadInterval.Checked = profile.ArkConfiguration.Rules.OverrideMinimumDinoReUploadInterval;
            txtOverrideMinimumDinoReUploadIntervalValue.Text = profile.ArkConfiguration.Rules.OverrideMinimumDinoReUploadIntervalValue.ToString(CultureInfo.InvariantCulture);
            chkPVESchedule.Checked = profile.ArkConfiguration.Rules.PveSchedule;
            chkUseServerTime.Checked = profile.ArkConfiguration.Rules.UseServerTime;
            txtPVPStartTime.Text = profile.ArkConfiguration.Rules.PvpStartTime;
            txtPVPEndTime.Text = profile.ArkConfiguration.Rules.PvpEndTime;
            chkPreventOfflinePVP.Checked = profile.ArkConfiguration.Rules.PreventOfflinePvp;
            txtLogoutInterval.Text = profile.ArkConfiguration.Rules.LogoutInterval.ToString(CultureInfo.InvariantCulture);
            txtConnectionInvicibleInterval.Text = profile.ArkConfiguration.Rules.ConnectionInvicibleInterval.ToString(CultureInfo.InvariantCulture);
            chkIncreasePVPRespawnInterval.Checked = profile.ArkConfiguration.Rules.IncreasePvpRespawnInterval;
            txtIntervalCheckPeriod.Text = profile.ArkConfiguration.Rules.IntervalCheckPeriod.ToString(CultureInfo.InvariantCulture);
            txtIntervalMultiplier.Text = (profile.ArkConfiguration.Rules.IntervalMultiplier * 100.0f).ToString(CultureInfo.InvariantCulture);
            txtIntervalBaseAmount.Text = profile.ArkConfiguration.Rules.IntervalBaseAmount.ToString(CultureInfo.InvariantCulture);
            txtMaxPlayersInTribe.Text = profile.ArkConfiguration.Rules.MaxPlayersInTribe.ToString(CultureInfo.InvariantCulture);
            txtTribeNameChangeCooldDown.Text = profile.ArkConfiguration.Rules.TribeNameChangeCooldDown.ToString(CultureInfo.InvariantCulture);
            txtTribeSlotReuseCooldown.Text = profile.ArkConfiguration.Rules.TribeSlotReuseCooldown.ToString(CultureInfo.InvariantCulture);
            chkAllowTribeAlliances.Checked = profile.ArkConfiguration.Rules.AllowTribeAlliances;
            txtMaxAlliancesPerTribe.Text = profile.ArkConfiguration.Rules.MaxAlliancesPerTribe.ToString(CultureInfo.InvariantCulture);
            txtMaxTribesPerAlliance.Text = profile.ArkConfiguration.Rules.MaxTribesPerAlliance.ToString(CultureInfo.InvariantCulture);
            chkAllowTribeWarfare.Checked = profile.ArkConfiguration.Rules.AllowTribeWarfare;
            chkAllowCancelingTribeWarfare.Checked = profile.ArkConfiguration.Rules.AllowCancelingTribeWarfare;
            chkAllowCostumRecipes.Checked = profile.ArkConfiguration.Rules.AllowCostumRecipes;
            txtCostumRecipesEffectivenessMultiplier.Text = (profile.ArkConfiguration.Rules.CostumRecipesEffectivenessMultiplier * 100.0f).ToString(CultureInfo.InvariantCulture);
            txtCostumRecipesSkillMultiplier.Text = (profile.ArkConfiguration.Rules.CostumRecipesSkillMultiplier * 100.0f).ToString(CultureInfo.InvariantCulture);
            chkEnableDiseases.Checked = profile.ArkConfiguration.Rules.EnableDiseases;
            chkNonPermanentDiseases.Checked = profile.ArkConfiguration.Rules.NonPermanentDiseases;
            chkOverrideNPCNetworkStasisRangeScale.Checked = profile.ArkConfiguration.Rules.OverrideNpcNetworkStasisRangeScale;
            txtOnlinePlayerCountStart.Text = profile.ArkConfiguration.Rules.OnlinePlayerCountStart.ToString(CultureInfo.InvariantCulture);
            txtOnlinePlayerCountEnd.Text = profile.ArkConfiguration.Rules.OnlinePlayerCountEnd.ToString(CultureInfo.InvariantCulture);
            txtScaleMaximum.Text = (profile.ArkConfiguration.Rules.ScaleMaximum * 10.0f).ToString(CultureInfo.InvariantCulture);
            txtOxygenSwimSpeedStatMultiplier.Text = (profile.ArkConfiguration.Rules.OxygenSwimSpeedStatMultiplier * 100.0f).ToString(CultureInfo.InvariantCulture);
            txtUseCorpseLifeSpanMultiplier.Text = (profile.ArkConfiguration.Rules.UseCorpseLifeSpanMultiplier * 100.0f).ToString(CultureInfo.InvariantCulture);
            txtFjordhawkInventoryCooldown.Text = profile.ArkConfiguration.Rules.FjordhawkInventoryCooldown.ToString(CultureInfo.InvariantCulture);
            txtGlobalPoweredBatteryDurability.Text = (profile.ArkConfiguration.Rules.GlobalPoweredBatteryDurability * 100.0f).ToString(CultureInfo.InvariantCulture);
            txtFuelConsumptionIntervalMultiplier.Text = (profile.ArkConfiguration.Rules.FuelConsumptionIntervalMultiplier * 100.0f).ToString(CultureInfo.InvariantCulture);
            txtLimitNonPlayerDroppedItemsRange.Text = profile.ArkConfiguration.Rules.LimitNonPlayerDroppedItemsRange.ToString(CultureInfo.InvariantCulture);
            txtLimitNonPlayerDroppedItemsCount.Text = profile.ArkConfiguration.Rules.LimitNonPlayerDroppedItemsCount.ToString(CultureInfo.InvariantCulture);
            chkEnableCryopodNerf.Checked = profile.ArkConfiguration.Rules.EnableCryopodNerf;
            txtEnableCryopodNerfDuration.Text = profile.ArkConfiguration.Rules.EnableCryopodNerfDuration.ToString(CultureInfo.InvariantCulture);
            txtOutgoingDamageMultiplier.Text = (profile.ArkConfiguration.Rules.OutgoingDamageMultiplier * 10.0f).ToString(CultureInfo.InvariantCulture);
            txtIncomingDamageMultiplierPercent.Text = (profile.ArkConfiguration.Rules.IncomingDamageMultiplierPercent * 10.0f).ToString(CultureInfo.InvariantCulture);
            chkGen1DisableMissions.Checked = profile.ArkConfiguration.Rules.Gen1DisableMissions;
            chkGen1AllowTekSuitPowers.Checked = profile.ArkConfiguration.Rules.Gen1AllowTekSuitPowers;
            chkGen1AllowTekSuitPowers.Checked = profile.ArkConfiguration.Rules.Gen1AllowTekSuitPowers;
            chkGen2DisableTEKSuitonSpawn.Checked = profile.ArkConfiguration.Rules.Gen2DisableTekSuitonSpawn;
            chkGen2DisableWorldBuffs.Checked = profile.ArkConfiguration.Rules.Gen2DisableWorldBuffs;
            chkEnableWorldBuffScaling.Checked = profile.ArkConfiguration.Rules.EnableWorldBuffScaling;
            txtWorldBuffScanlingEfficacy.Text = (profile.ArkConfiguration.Rules.WorldBuffScanlingEfficacy * 10.0f).ToString(CultureInfo.InvariantCulture);
            txtMutagemSpawnDelayMultiplier.Text = (profile.ArkConfiguration.Rules.MutagemSpawnDelayMultiplier * 10.0f).ToString(CultureInfo.InvariantCulture);
            chkDisableHexagonStore.Checked = profile.ArkConfiguration.Rules.DisableHexagonStore;
            chkAllowOnlyEngramPointsTrade.Checked = profile.ArkConfiguration.Rules.AllowOnlyEngramPointsTrade;
            txtMaxHexagonsPerCharacter.Text = profile.ArkConfiguration.Rules.MaxHexagonsPerCharacter.ToString(CultureInfo.InvariantCulture);
            txtHexagonRewardMultiplier.Text = (profile.ArkConfiguration.Rules.HexagonRewardMultiplier * 10.0f).ToString(CultureInfo.InvariantCulture);
            txtHexagonCostMultiplier.Text = (profile.ArkConfiguration.Rules.HexagonCostMultiplier * 10.0f).ToString(CultureInfo.InvariantCulture);
            chkAllowOnlyEngramPointsTrade.Checked = profile.ArkConfiguration.Rules.AllowOnlyEngramPointsTrade;
            chkAllowMultipleTamedUnicorns.Checked = profile.ArkConfiguration.Rules.AllowMultipleTamedUnicorns;
            txtUnicornSpawnInterval.Text = profile.ArkConfiguration.Rules.UnicornSpawnInterval.ToString(CultureInfo.InvariantCulture);
            chkEnableVolcano.Checked = profile.ArkConfiguration.Rules.EnableVolcano;
            txtVolcanoInterval.Text = (profile.ArkConfiguration.Rules.VolcanoInterval * 10.0f).ToString(CultureInfo.InvariantCulture);
            txtVolcanoIntensity.Text = (profile.ArkConfiguration.Rules.VolcanoIntensity * 10.0f).ToString(CultureInfo.InvariantCulture);
            chkEnableFjordurSettings.Checked = profile.ArkConfiguration.Rules.EnableFjordurSettings;
            chkEnableFjordurBiomeTeleport.Checked = profile.ArkConfiguration.Rules.EnableFjordurBiomeTeleport;
            chkEnableGenericQualityClamp.Checked = profile.ArkConfiguration.Rules.EnableGenericQualityClamp;
            txtGenericQualityClamp.Text = profile.ArkConfiguration.Rules.GenericQualityClamp.ToString(CultureInfo.InvariantCulture);
            chkEnableArmorClamp.Checked = profile.ArkConfiguration.Rules.EnableArmorClamp;
            txtArmorClamp.Text = profile.ArkConfiguration.Rules.ArmorClamp.ToString(CultureInfo.InvariantCulture);
            chkEnableWeaponDamagePercentClamp.Checked = profile.ArkConfiguration.Rules.EnableWeaponDamagePercentClamp;
            txtWeaponDamagePercentClamp.Text = profile.ArkConfiguration.Rules.WeaponDamagePercentClamp.ToString(CultureInfo.InvariantCulture);
            chkEnableHypoInsulationClamp.Checked = profile.ArkConfiguration.Rules.EnableHypoInsulationClamp;
            txtHypoInsulationClamp.Text = profile.ArkConfiguration.Rules.HypoInsulationClamp.ToString(CultureInfo.InvariantCulture);
            chkEnableWeightClamp.Checked = profile.ArkConfiguration.Rules.EnableWeightClamp;
            txtWeightClamp.Text = profile.ArkConfiguration.Rules.WeightClamp.ToString(CultureInfo.InvariantCulture);
            chkEnableMaxDurabilityClamp.Checked = profile.ArkConfiguration.Rules.EnableMaxDurabilityClamp;
            txtMaxDurabilityClamp.Text = profile.ArkConfiguration.Rules.MaxDurabilityClamp.ToString(CultureInfo.InvariantCulture);
            chkEnableWeaponClipAmmoClamp.Checked = profile.ArkConfiguration.Rules.EnableWeaponClipAmmoClamp;
            txtWeaponClipAmmoClamp.Text = profile.ArkConfiguration.Rules.WeaponClipAmmoClamp.ToString(CultureInfo.InvariantCulture);
            chkEnableHyperInsulationClamp.Checked = profile.ArkConfiguration.Rules.EnableHyperInsulationClamp;
            txtHyperInsulationClamp.Text = profile.ArkConfiguration.Rules.HyperInsulationClamp.ToString(CultureInfo.InvariantCulture);

            #region Validations

            ManageCheckGroupBox(chkEnableDifficultOverride, groupBox12);
            ManageCheckGroupBox(chkEnableTributeDownloads, groupBox13);
            ManageCheckGroupBox(chkPVESchedule, groupBox16);
            ManageCheckGroupBox(chkPreventOfflinePVP, groupBox28);
            ManageCheckGroupBox(chkAllowTribeAlliances, groupBox18);
            ManageCheckGroupBox(chkAllowCostumRecipes, groupBox26);
            ManageCheckGroupBox(chkEnableDiseases, groupBox21);
            ManageCheckGroupBox(chkOverrideNPCNetworkStasisRangeScale, groupBox22);
            ManageCheckGroupBox(chkEnableCryopodNerf, groupBox23);
            ManageCheckGroupBox(chkIncreasePVPRespawnInterval, groupBox17);
            ManageCheckGroupBox(chkEnableRagnarokSettings, groupBox30);
            ManageCheckGroupBox(chkEnableFjordurSettings, groupBox26);

            txtRCONPort.Enabled = chkEnableRCON.Checked;
            txtRCONBuffer.Enabled = chkEnableRCON.Checked;
            tbMOTDInterval.Enabled = chkEnableInterval.Checked;

            if (!Directory.Exists(txtLocation.Text)) {
                btUpdate.Text = "Install";
                IsInstalled = false;
            }
            else {
                if (Utils.IsAValidFolder(txtLocation.Text,
                                         new List<string> { "Engine", "ShooterGame", "steamapps" })) {
                    btUpdate.Text = "Update/Verify";
                    IsInstalled = true;
                }
                else {
                    btUpdate.Text = "Install";
                    IsInstalled = false;
                }
            }

            btStart.Enabled = IsInstalled;
            btRCON.Enabled = IsInstalled;

            cboMap.DataSource = SupportedServers.GetMapLists(profile.Type.ServerType);
            cboMap.ValueMember = "Key";
            cboMap.DisplayMember = "Description";

            #endregion

            txtVersion.Text = profile.GetVersion();
            txtBuild.Text = profile.GetBuild();

            txtCommand.Text =
                profile.ArkConfiguration.GetCommandLinesArguments(MainForm.Settings, profile, MainForm.LocaIp);

            //profile.ARKConfiguration.LoadGameINI(profile);
        }

        //        Severity Code    Description Project File Line    Suppression State
        //Error CS1503  Argument 1: cannot convert from 'System.Windows.Forms.Control.ControlCollection' to 'System.Windows.Forms.Form.ControlCollection'	OphiussaServerManager C:\Users\Utilizador\source\repos\OphiussaServerManager\OphiussaServerManager\Forms\FrmArk.cs	76	Active

        private void ForceTrackBarValues(Control.ControlCollection controls) {
            foreach (Control item in controls)
                if (item is TrackBar) {
                    (item as TrackBar).Value = (item as TrackBar).Maximum;
                    (item as TrackBar).Value = (item as TrackBar).Minimum;
                }
                else {
                    if (item.HasChildren) ForceTrackBarValues(item.Controls);
                }
        }

        private void txtProfileName_Validated(object sender, EventArgs e) {
            _tab.Text = txtProfileName.Text + "          ";
        }

        private void textBox1_DoubleClick(object sender, EventArgs e) {
            if (txtServerPWD.PasswordChar == '\0')
                txtServerPWD.PasswordChar = '*';
            else
                txtServerPWD.PasswordChar = '\0';
        }

        private void textBox2_DoubleClick(object sender, EventArgs e) {
            if (txtAdminPass.PasswordChar == '\0')
                txtAdminPass.PasswordChar = '*';
            else
                txtAdminPass.PasswordChar = '\0';
        }

        private void textBox3_DoubleClick(object sender, EventArgs e) {
            if (txtSpePwd.PasswordChar == '\0')
                txtSpePwd.PasswordChar = '*';
            else
                txtSpePwd.PasswordChar = '\0';
        }

        private void txtServerPort_TextChanged(object sender, EventArgs e) {
            int port;
            if (int.TryParse(txtServerPort.Text, out port)) txtPeerPort.Text = (port + 1).ToString();
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

                    DaysOfTheWeek daysofweek = 0;

                    if (_profile.AutoManageSettings.ShutdownServer1Monday)
                        daysofweek += 2;
                    if (_profile.AutoManageSettings.ShutdownServer1Tuesday)
                        daysofweek += 4;
                    if (_profile.AutoManageSettings.ShutdownServer1Wednesday)
                        daysofweek += 8;
                    if (_profile.AutoManageSettings.ShutdownServer1Thu)
                        daysofweek += 16;
                    if (_profile.AutoManageSettings.ShutdownServer1Friday)
                        daysofweek += 32;
                    if (_profile.AutoManageSettings.ShutdownServer1Saturday)
                        daysofweek += 64;
                    if (_profile.AutoManageSettings.ShutdownServer1Sunday)
                        daysofweek += 1;
                    var tt = new WeeklyTrigger();

                    int hour = short.Parse(_profile.AutoManageSettings.ShutdownServer1Hour.Split(':')[0]);
                    int minute = short.Parse(_profile.AutoManageSettings.ShutdownServer1Hour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek = daysofweek;
                    task.Definition.Triggers.Add(tt);
                    task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                    task.Definition.Settings.Priority = ProcessPriorityClass.Normal;
                    task.RegisterChanges();
                }
                else {
                    var td = TaskService.Instance.NewTask();
                    td.RegistrationInfo.Description = "Server Auto-ShutDown 1 - " + _profile.Name;
                    td.Principal.LogonType = TaskLogonType.InteractiveToken;
                    DaysOfTheWeek daysofweek = 0;

                    if (_profile.AutoManageSettings.ShutdownServer1Monday)
                        daysofweek += 2;
                    if (_profile.AutoManageSettings.ShutdownServer1Tuesday)
                        daysofweek += 4;
                    if (_profile.AutoManageSettings.ShutdownServer1Wednesday)
                        daysofweek += 8;
                    if (_profile.AutoManageSettings.ShutdownServer1Thu)
                        daysofweek += 16;
                    if (_profile.AutoManageSettings.ShutdownServer1Friday)
                        daysofweek += 32;
                    if (_profile.AutoManageSettings.ShutdownServer1Saturday)
                        daysofweek += 64;
                    if (_profile.AutoManageSettings.ShutdownServer1Sunday)
                        daysofweek += 1;
                    var tt = new WeeklyTrigger();

                    int hour = short.Parse(_profile.AutoManageSettings.ShutdownServer1Hour.Split(':')[0]);
                    int minute = short.Parse(_profile.AutoManageSettings.ShutdownServer1Hour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek = daysofweek;
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

                    DaysOfTheWeek daysofweek = 0;

                    if (_profile.AutoManageSettings.ShutdownServer2Monday)
                        daysofweek += 2;
                    if (_profile.AutoManageSettings.ShutdownServer2Tuesday)
                        daysofweek += 4;
                    if (_profile.AutoManageSettings.ShutdownServer2Wednesday)
                        daysofweek += 8;
                    if (_profile.AutoManageSettings.ShutdownServer2Thu)
                        daysofweek += 16;
                    if (_profile.AutoManageSettings.ShutdownServer2Friday)
                        daysofweek += 32;
                    if (_profile.AutoManageSettings.ShutdownServer2Saturday)
                        daysofweek += 64;
                    if (_profile.AutoManageSettings.ShutdownServer2Sunday)
                        daysofweek += 1;
                    var tt = new WeeklyTrigger();

                    int hour = short.Parse(_profile.AutoManageSettings.ShutdownServer2Hour.Split(':')[0]);
                    int minute = short.Parse(_profile.AutoManageSettings.ShutdownServer2Hour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek = daysofweek;
                    task.Definition.Triggers.Add(tt);
                    task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                    task.Definition.Settings.Priority = ProcessPriorityClass.Normal;
                    task.RegisterChanges();
                }
                else {
                    var td = TaskService.Instance.NewTask();
                    td.RegistrationInfo.Description = "Server Auto-ShutDown 2 - " + _profile.Name;
                    td.Principal.LogonType = TaskLogonType.InteractiveToken;

                    DaysOfTheWeek daysofweek = 0;

                    if (_profile.AutoManageSettings.ShutdownServer2Monday)
                        daysofweek += 2;
                    if (_profile.AutoManageSettings.ShutdownServer2Tuesday)
                        daysofweek += 4;
                    if (_profile.AutoManageSettings.ShutdownServer2Wednesday)
                        daysofweek += 8;
                    if (_profile.AutoManageSettings.ShutdownServer2Thu)
                        daysofweek += 16;
                    if (_profile.AutoManageSettings.ShutdownServer2Friday)
                        daysofweek += 32;
                    if (_profile.AutoManageSettings.ShutdownServer2Saturday)
                        daysofweek += 64;
                    if (_profile.AutoManageSettings.ShutdownServer2Sunday)
                        daysofweek += 1;
                    var tt = new WeeklyTrigger();

                    int hour = short.Parse(_profile.AutoManageSettings.ShutdownServer2Hour.Split(':')[0]);
                    int minute = short.Parse(_profile.AutoManageSettings.ShutdownServer2Hour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek = daysofweek;
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
            _profile.ArkConfiguration.Administration.UseServerApi = chkUseApi.Checked;
            _profile.ArkConfiguration.Administration.ServerName = txtServerName.Text;
            _profile.ArkConfiguration.Administration.ServerPassword = txtServerPWD.Text;
            _profile.ArkConfiguration.Administration.ServerAdminPassword = txtAdminPass.Text;
            _profile.ArkConfiguration.Administration.ServerSpectatorPassword = txtSpePwd.Text;
            _profile.ArkConfiguration.Administration.LocalIp = txtLocalIP.SelectedValue.ToString();
            _profile.ArkConfiguration.Administration.ServerPort = txtServerPort.Text;
            _profile.ArkConfiguration.Administration.PeerPort = txtPeerPort.Text;
            _profile.ArkConfiguration.Administration.QueryPort = txtQueryPort.Text;
            _profile.ArkConfiguration.Administration.UseRcon = chkEnableRCON.Checked;
            _profile.ArkConfiguration.Administration.RconPort = txtRCONPort.Text;
            _profile.ArkConfiguration.Administration.RconServerLogBuffer = int.Parse(txtRCONBuffer.Text);
            _profile.ArkConfiguration.Administration.MapName = cboMap.SelectedValue.ToString();
            _profile.ArkConfiguration.Administration.Branch = cbBranch.Text;
            _profile.ArkConfiguration.Administration.ModIDs = txtMods.Text.Split(',').ToList();
            _profile.ArkConfiguration.Administration.TotalConversionId = txtTotalConversion.Text;
            _profile.ArkConfiguration.Administration.AutoSavePeriod = tbAutoSavePeriod.Value;
            _profile.ArkConfiguration.Administration.Mod = txtMOTD.Text;
            _profile.ArkConfiguration.Administration.ModDuration = tbMOTDDuration.Value;
            _profile.ArkConfiguration.Administration.ModInterval = tbMOTDInterval.Value;
            _profile.ArkConfiguration.Administration.EnableInterval = chkEnableInterval.Checked;
            _profile.ArkConfiguration.Administration.MaxPlayers = tbMaxPlayers.Value;
            _profile.ArkConfiguration.Administration.EnablIdleTimeOut = chkEnableIdleTimeout.Checked;
            _profile.ArkConfiguration.Administration.IdleTimout = tbIdleTimeout.Value;
            _profile.ArkConfiguration.Administration.UseBanListUrl = chkUseBanUrl.Checked;
            _profile.ArkConfiguration.Administration.BanListUrl = txtBanUrl.Text;
            _profile.ArkConfiguration.Administration.DisableVac = chkDisableVAC.Checked;
            _profile.ArkConfiguration.Administration.EnableBattleEye = chkEnableBattleEye.Checked;
            _profile.ArkConfiguration.Administration.DisablePlayerMovePhysics = chkDisablePlayerMove.Checked;
            _profile.ArkConfiguration.Administration.OutputLogToConsole = chkOutputLogToConsole.Checked;
            _profile.ArkConfiguration.Administration.UseAllCores = chkUseAllCores.Checked;
            _profile.ArkConfiguration.Administration.UseCache = chkUseCache.Checked;
            _profile.ArkConfiguration.Administration.NoHandDetection = chkNoHang.Checked;
            _profile.ArkConfiguration.Administration.NoDinos = chkNoDinos.Checked;
            _profile.ArkConfiguration.Administration.NoUnderMeshChecking = chkNoUnderMeshChecking.Checked;
            _profile.ArkConfiguration.Administration.NoUnderMeshKilling = chkNoUnderMeshKilling.Checked;
            _profile.ArkConfiguration.Administration.EnableVivox = chkEnableVivox.Checked;
            _profile.ArkConfiguration.Administration.AllowSharedConnections = chkAllowSharedConnections.Checked;
            _profile.ArkConfiguration.Administration.RespawnDinosOnStartUp = chkRespawnDinosOnStartup.Checked;
            _profile.ArkConfiguration.Administration.EnableAutoForceRespawnDinos = chkEnableForceRespawn.Checked;
            _profile.ArkConfiguration.Administration.AutoForceRespawnDinosInterval = tbRespawnInterval.Value;
            _profile.ArkConfiguration.Administration.DisableAntiSpeedHackDetection = chkDisableSpeedHack.Checked;
            _profile.ArkConfiguration.Administration.AntiSpeedHackBias = tbSpeedBias.Value;
            _profile.ArkConfiguration.Administration.ForceDirectX10 = chkForceDX10.Checked;
            _profile.ArkConfiguration.Administration.ForceLowMemory = chkForceLowMemory.Checked;
            _profile.ArkConfiguration.Administration.ForceNoManSky = chkForceNoManSky.Checked;
            _profile.ArkConfiguration.Administration.UseNoMemoryBias = chkUseNoMemoryBias.Checked;
            _profile.ArkConfiguration.Administration.StasisKeepController = chkStasisKeepControllers.Checked;
            _profile.ArkConfiguration.Administration.ServerAllowAnsel = chkAllowAnsel.Checked;
            _profile.ArkConfiguration.Administration.StructureMemoryOptimizations = chkStructuresOptimization.Checked;
            _profile.ArkConfiguration.Administration.EnableCrossPlay = chkEnableCrossPlay.Checked;
            _profile.ArkConfiguration.Administration.EnablePublicIpForEpic = chkEnableCrossPlay.Checked;
            _profile.ArkConfiguration.Administration.EpicStorePlayersOnly = ChkEpicOnly.Checked;
            _profile.ArkConfiguration.Administration.AlternateSaveDirectoryName = txtAltSaveDirectory.Text;
            _profile.ArkConfiguration.Administration.ClusterId = txtClusterID.Text;
            _profile.ArkConfiguration.Administration.ClusterDirectoryOverride = chkClusterOverride.Checked;
            _profile.ArkConfiguration.Administration.CpuPriority = (ProcessPriorityClass)cboPriority.SelectedValue;
            _profile.ArkConfiguration.Administration.EnableServerAdminLogs = chkEnableServerAdminLogs.Checked;
            _profile.ArkConfiguration.Administration.ServerAdminLogsIncludeTribeLogs = chkServerAdminLogsIncludeTribeLogs.Checked;
            _profile.ArkConfiguration.Administration.ServerRconOutputTribeLogs = chkServerRCONOutputTribeLogs.Checked;
            _profile.ArkConfiguration.Administration.AllowHideDamageSourceFromLogs = chkAllowHideDamageSourceFromLogs.Checked;
            _profile.ArkConfiguration.Administration.LogAdminCommandsToPublic = chkLogAdminCommandsToPublic.Checked;
            _profile.ArkConfiguration.Administration.LogAdminCommandsToAdmins = chkLogAdminCommandstoAdmins.Checked;
            _profile.ArkConfiguration.Administration.TribeLogDestroyedEnemyStructures = chkTribeLogDestroyedEnemyStructures.Checked;
            _profile.ArkConfiguration.Administration.MaximumTribeLogs = int.Parse(txtMaximumTribeLogs.Text);

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

            _profile.ArkConfiguration.Rules.EnableHardcoreMode = chkEnableHardcoreMode.Checked;
            _profile.ArkConfiguration.Rules.DisablePveFriendlyFire = chkDisablePVEFriendlyFire.Checked;
            _profile.ArkConfiguration.Rules.DisablePvpFriendlyFire = chkDisablePVPFriendlyFire.Checked;
            _profile.ArkConfiguration.Rules.PreventBuildingInResourceRichAreas = chkPreventBuildingInResourceRichAreas.Checked;
            _profile.ArkConfiguration.Rules.DisableSupplyCrates = chkDisableSupplyCrates.Checked;
            _profile.ArkConfiguration.Rules.EnablePvp = chkEnablePVP.Checked;
            _profile.ArkConfiguration.Rules.EnablePveCaveBuilding = chkEnablePVECaveBuilding.Checked;
            _profile.ArkConfiguration.Rules.EnablePvpCaveBuilding = chkEnablePVPCaveBuilding.Checked;
            _profile.ArkConfiguration.Rules.EnableSinglePlayerSettings = chkEnableSinglePlayerSettings.Checked;
            _profile.ArkConfiguration.Rules.AllowCrateSpawnsOnTopOfStructures = chkAllowCrateSpawnsOnTopOfStructures.Checked;
            _profile.ArkConfiguration.Rules.EnableCreativeMode = chkEnableCreativeMode.Checked;
            _profile.ArkConfiguration.Rules.EnablePveCryoSickness = chkEnablePVECryoSickness.Checked;
            _profile.ArkConfiguration.Rules.DisablePvpRailGun = chkDisablePVPRailGun.Checked;
            _profile.ArkConfiguration.Rules.DisableCostumTributeFolders = chkDisableCostumTributeFolders.Checked;
            _profile.ArkConfiguration.Rules.RandomSupplyCratePoints = chkRandomSupplyCratePoints.Checked;
            _profile.ArkConfiguration.Rules.SupplyCrateLootQualityMultiplier = txtSupplyCrateLootQualityMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.Rules.FishingLootQualityMultiplier = txtFishingLootQualityMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.Rules.UseCorpseLocation = chkUseCorpseLocation.Checked;
            _profile.ArkConfiguration.Rules.PreventSpawnAnimations = chkPreventSpawnAnimations.Checked;
            _profile.ArkConfiguration.Rules.AllowUnlimitedRespecs = chkAllowUnlimitedRespecs.Checked;
            _profile.ArkConfiguration.Rules.AllowPlatformSaddleMultiFloors = chkAllowPlatformSaddleMultiFloors.Checked;
            _profile.ArkConfiguration.Rules.PlatformSaddleBuildAreaBoundsMultiplier = txtPlatformSaddleBuildAreaBoundsMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.Rules.MaxGatewaysOnSaddles = tbMaxGatewaysOnSaddles.Value;
            _profile.ArkConfiguration.Rules.EnableDifficultOverride = chkEnableDifficultOverride.Checked;
            _profile.ArkConfiguration.Rules.MaxDinoLevel = tbMaxDinoLevel.Value;
            _profile.ArkConfiguration.Rules.DifficultyOffset = txtDifficultyOffset.Text.ToFloat();
            _profile.ArkConfiguration.Rules.DestroyTamesOverLevel = tbDestroyTamesOverLevel.Value;
            _profile.ArkConfiguration.Rules.EnableTributeDownloads = chkEnableTributeDownloads.Checked;
            _profile.ArkConfiguration.Rules.NoSurvivorDownloads = chkNoSurvivorDownloads.Checked;
            _profile.ArkConfiguration.Rules.NoItemDownloads = chkNoItemDownloads.Checked;
            _profile.ArkConfiguration.Rules.NoDinoDownloads = chkNoDinoDownloads.Checked;
            _profile.ArkConfiguration.Rules.AllowForeignDinoDownloads = chkAllowForeignDinoDownloads.Checked;
            _profile.ArkConfiguration.Rules.NoSurvivorUploads = chkNoSurvivorUploads.Checked;
            _profile.ArkConfiguration.Rules.NoItemUploads = chkNoItemUploads.Checked;
            _profile.ArkConfiguration.Rules.NoDinoUploads = chkNoDinoUploads.Checked;
            _profile.ArkConfiguration.Rules.LimitMaxTributeDinos = chkLimitMaxTributeDinos.Checked;
            _profile.ArkConfiguration.Rules.MaxTributeDinos = tbMaxTributeDinos.Value;
            _profile.ArkConfiguration.Rules.LimitTributeItems = chkLimitTributeItems.Checked;
            _profile.ArkConfiguration.Rules.MaxTributeItems = tbMaxTributeItems.Value;
            _profile.ArkConfiguration.Rules.NoTransferFromFiltering = chkNoTransferFromFiltering.Checked;
            _profile.ArkConfiguration.Rules.OverrideSurvivorUploadExpiration = chkOverrideSurvivorUploadExpiration.Checked;
            _profile.ArkConfiguration.Rules.OverrideSurvivorUploadExpirationValue = tbOverrideSurvivorUploadExpirationValue.Value;
            _profile.ArkConfiguration.Rules.OverrideItemUploadExpiration = chkOverrideItemUploadExpiration.Checked;
            _profile.ArkConfiguration.Rules.OverrideItemUploadExpirationValue = tbOverrideItemUploadExpirationValue.Value;
            _profile.ArkConfiguration.Rules.OverrideDinoUploadExpiration = chkOverrideDinoUploadExpiration.Checked;
            _profile.ArkConfiguration.Rules.OverrideDinoUploadExpirationValue = tbOverrideDinoUploadExpirationValue.Value;
            _profile.ArkConfiguration.Rules.OverrideMinimumDinoReUploadInterval = chkOverrideMinimumDinoReUploadInterval.Checked;
            _profile.ArkConfiguration.Rules.OverrideMinimumDinoReUploadIntervalValue = tbOverrideMinimumDinoReUploadIntervalValue.Value;
            _profile.ArkConfiguration.Rules.PveSchedule = chkPVESchedule.Checked;
            _profile.ArkConfiguration.Rules.UseServerTime = chkUseServerTime.Checked;
            _profile.ArkConfiguration.Rules.PvpStartTime = txtPVPStartTime.Text;
            _profile.ArkConfiguration.Rules.PvpEndTime = txtPVPEndTime.Text;
            _profile.ArkConfiguration.Rules.PreventOfflinePvp = chkPreventOfflinePVP.Checked;
            _profile.ArkConfiguration.Rules.LogoutInterval = tbLogoutInterval.Value;
            _profile.ArkConfiguration.Rules.ConnectionInvicibleInterval = tbConnectionInvicibleInterval.Value;
            _profile.ArkConfiguration.Rules.IncreasePvpRespawnInterval = chkIncreasePVPRespawnInterval.Checked;
            _profile.ArkConfiguration.Rules.IntervalCheckPeriod = tbIntervalCheckPeriod.Value;
            _profile.ArkConfiguration.Rules.IntervalMultiplier = txtIntervalMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.Rules.IntervalBaseAmount = tbIntervalBaseAmount.Value;
            _profile.ArkConfiguration.Rules.MaxPlayersInTribe = tbMaxPlayersInTribe.Value;
            _profile.ArkConfiguration.Rules.TribeNameChangeCooldDown = tbTribeNameChangeCooldDown.Value;
            _profile.ArkConfiguration.Rules.TribeSlotReuseCooldown = tbTribeSlotReuseCooldown.Value;
            _profile.ArkConfiguration.Rules.AllowTribeAlliances = chkAllowTribeAlliances.Checked;
            _profile.ArkConfiguration.Rules.MaxAlliancesPerTribe = tbMaxAlliancesPerTribe.Value;
            _profile.ArkConfiguration.Rules.MaxTribesPerAlliance = tbMaxTribesPerAlliance.Value;
            _profile.ArkConfiguration.Rules.AllowTribeWarfare = chkAllowTribeWarfare.Checked;
            _profile.ArkConfiguration.Rules.AllowCancelingTribeWarfare = chkAllowCancelingTribeWarfare.Checked;
            _profile.ArkConfiguration.Rules.AllowCostumRecipes = chkAllowCostumRecipes.Checked;
            _profile.ArkConfiguration.Rules.CostumRecipesEffectivenessMultiplier = txtCostumRecipesEffectivenessMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.Rules.CostumRecipesSkillMultiplier = txtCostumRecipesSkillMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.Rules.EnableDiseases = chkEnableDiseases.Checked;
            _profile.ArkConfiguration.Rules.NonPermanentDiseases = chkNonPermanentDiseases.Checked;
            _profile.ArkConfiguration.Rules.OverrideNpcNetworkStasisRangeScale = chkOverrideNPCNetworkStasisRangeScale.Checked;
            _profile.ArkConfiguration.Rules.OnlinePlayerCountStart = tbOnlinePlayerCountStart.Value;
            _profile.ArkConfiguration.Rules.OnlinePlayerCountEnd = tbOnlinePlayerCountEnd.Value;
            _profile.ArkConfiguration.Rules.ScaleMaximum = txtScaleMaximum.Text.ToFloat();
            _profile.ArkConfiguration.Rules.OxygenSwimSpeedStatMultiplier = txtOxygenSwimSpeedStatMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.Rules.UseCorpseLifeSpanMultiplier = txtUseCorpseLifeSpanMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.Rules.FjordhawkInventoryCooldown = tbFjordhawkInventoryCooldown.Value;
            _profile.ArkConfiguration.Rules.GlobalPoweredBatteryDurability = txtGlobalPoweredBatteryDurability.Text.ToFloat();
            _profile.ArkConfiguration.Rules.FuelConsumptionIntervalMultiplier = txtFuelConsumptionIntervalMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.Rules.LimitNonPlayerDroppedItemsRange = tbLimitNonPlayerDroppedItemsRange.Value;
            _profile.ArkConfiguration.Rules.LimitNonPlayerDroppedItemsCount = tbLimitNonPlayerDroppedItemsCount.Value;
            _profile.ArkConfiguration.Rules.EnableCryopodNerf = chkEnableCryopodNerf.Checked;
            _profile.ArkConfiguration.Rules.EnableCryopodNerfDuration = tbEnableCryopodNerfDuration.Value;
            _profile.ArkConfiguration.Rules.OutgoingDamageMultiplier = txtOutgoingDamageMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.Rules.IncomingDamageMultiplierPercent = txtIncomingDamageMultiplierPercent.Text.ToFloat();
            _profile.ArkConfiguration.Rules.Gen1DisableMissions = chkGen1DisableMissions.Checked;
            _profile.ArkConfiguration.Rules.Gen1AllowTekSuitPowers = chkGen1AllowTekSuitPowers.Checked;
            _profile.ArkConfiguration.Rules.Gen2DisableTekSuitonSpawn = chkGen2DisableTEKSuitonSpawn.Checked;
            _profile.ArkConfiguration.Rules.Gen2DisableWorldBuffs = chkGen2DisableWorldBuffs.Checked;
            _profile.ArkConfiguration.Rules.EnableWorldBuffScaling = chkEnableWorldBuffScaling.Checked;
            _profile.ArkConfiguration.Rules.WorldBuffScanlingEfficacy = txtWorldBuffScanlingEfficacy.Text.ToFloat();
            _profile.ArkConfiguration.Rules.MutagemSpawnDelayMultiplier = txtMutagemSpawnDelayMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.Rules.DisableHexagonStore = chkDisableHexagonStore.Checked;
            _profile.ArkConfiguration.Rules.AllowOnlyEngramPointsTrade = chkAllowOnlyEngramPointsTrade.Checked;
            _profile.ArkConfiguration.Rules.MaxHexagonsPerCharacter = tbMaxHexagonsPerCharacter.Value;
            _profile.ArkConfiguration.Rules.HexagonRewardMultiplier = txtHexagonRewardMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.Rules.HexagonCostMultiplier = txtHexagonCostMultiplier.Text.ToFloat();
            _profile.ArkConfiguration.Rules.AllowOnlyEngramPointsTrade = chkAllowOnlyEngramPointsTrade.Checked;
            _profile.ArkConfiguration.Rules.AllowMultipleTamedUnicorns = chkAllowMultipleTamedUnicorns.Checked;
            _profile.ArkConfiguration.Rules.UnicornSpawnInterval = tbUnicornSpawnInterval.Value;
            _profile.ArkConfiguration.Rules.EnableVolcano = chkEnableVolcano.Checked;
            _profile.ArkConfiguration.Rules.VolcanoInterval = txtVolcanoInterval.Text.ToFloat();
            _profile.ArkConfiguration.Rules.VolcanoIntensity = txtVolcanoIntensity.Text.ToFloat();
            _profile.ArkConfiguration.Rules.EnableFjordurSettings = chkEnableFjordurSettings.Checked;
            _profile.ArkConfiguration.Rules.EnableFjordurBiomeTeleport = chkEnableFjordurBiomeTeleport.Checked;
            _profile.ArkConfiguration.Rules.EnableGenericQualityClamp = chkEnableGenericQualityClamp.Checked;
            _profile.ArkConfiguration.Rules.GenericQualityClamp = tbGenericQualityClamp.Value;
            _profile.ArkConfiguration.Rules.EnableArmorClamp = chkEnableArmorClamp.Checked;
            _profile.ArkConfiguration.Rules.ArmorClamp = tbArmorClamp.Value;
            _profile.ArkConfiguration.Rules.EnableWeaponDamagePercentClamp = chkEnableWeaponDamagePercentClamp.Checked;
            _profile.ArkConfiguration.Rules.WeaponDamagePercentClamp = tbWeaponDamagePercentClamp.Value;
            _profile.ArkConfiguration.Rules.EnableHypoInsulationClamp = chkEnableHypoInsulationClamp.Checked;
            _profile.ArkConfiguration.Rules.HypoInsulationClamp = tbHypoInsulationClamp.Value;
            _profile.ArkConfiguration.Rules.EnableWeightClamp = chkEnableWeightClamp.Checked;
            _profile.ArkConfiguration.Rules.WeightClamp = tbWeightClamp.Value;
            _profile.ArkConfiguration.Rules.EnableMaxDurabilityClamp = chkEnableMaxDurabilityClamp.Checked;
            _profile.ArkConfiguration.Rules.MaxDurabilityClamp = tbMaxDurabilityClamp.Value;
            _profile.ArkConfiguration.Rules.EnableWeaponClipAmmoClamp = chkEnableWeaponClipAmmoClamp.Checked;
            _profile.ArkConfiguration.Rules.WeaponClipAmmoClamp = tbWeaponClipAmmoClamp.Value;
            _profile.ArkConfiguration.Rules.EnableHyperInsulationClamp = chkEnableHyperInsulationClamp.Checked;
            _profile.ArkConfiguration.Rules.HyperInsulationClamp = tbHyperInsulationClamp.Value;

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

        public void IsRunningProcess() {
            while (true) {
                Process process = null;

                if (_processId == -1)
                    process = _profile.GetExeProcess();
                else
                    try {
                        process = Process.GetProcessById(_processId);
                    }
                    catch (Exception) {
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

                if (_isRunning)
                    btStart.Text = "Stop";
                else
                    btStart.Text = "Start";
                btUpdate.Enabled = !_isRunning;
                btRCON.Enabled = _isRunning && _profile.ArkConfiguration.Administration.UseRcon;

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

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e) {
        }

        private void btProcessorAffinity_Click(object sender, EventArgs e) {
            var frm = new FrmProcessors(_profile.ArkConfiguration.Administration.CpuAffinity == "All",
                                        _profile.ArkConfiguration.Administration.CpuAffinityList);
            frm.UpdateCpuAffinity = (all, lst) => {
                _profile.ArkConfiguration.Administration.CpuAffinity = all
                                                                           ? "All"
                                                                           : string.Join(",", lst.FindAll(x => x.Selected).Select(x => x.ProcessorNumber.ToString()));
                _profile.ArkConfiguration.Administration.CpuAffinityList = lst;
                txtAffinity.Text = _profile.ArkConfiguration.Administration.CpuAffinity;
            };
            frm.ShowDialog();
        }

        private void btMods_Click(object sender, EventArgs e) {
            var frm = new FrmModManager();
            frm.UpdateModList = lst => { txtMods.Text = string.Join(",", lst.Select(x => x.ModId.ToString()).ToArray()); };

            frm.LoadMods(ref _profile, txtMods.Text, this);
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
                chk.Location = new Point(
                                         chk.Left + grp.Left,
                                         chk.Top + grp.Top);

                // Move the CheckBox to the top of the stacking order.
                chk.BringToFront();
            }

            // Enable or disable the GroupBox.
            grp.Enabled = chk.Checked;
        }

        private void tbSupplyCrateLootQualityMultiplier_Scroll(object sender, EventArgs e) {
            txtSupplyCrateLootQualityMultiplier.Text = ((sender as TrackBar).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbFishingLootQualityMultiplier_Scroll(object sender, EventArgs e) {
            txtFishingLootQualityMultiplier.Text = ((sender as TrackBar).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbPlatformSaddleBuildAreaBoundsMultiplier_Scroll(object sender, EventArgs e) {
            txtPlatformSaddleBuildAreaBoundsMultiplier.Text = ((sender as TrackBar).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMaxGatewaysOnSaddles_Scroll(object sender, EventArgs e) {
            txtMaxGatewaysOnSaddles.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbMaxDinoLevel_Scroll(object sender, EventArgs e) {
            txtMaxDinoLevel.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbDifficultyOffset_Scroll(object sender, EventArgs e) {
            txtDifficultyOffset.Text = ((sender as TrackBar).Value / 100.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbDestroyTamesOverLevel_Scroll(object sender, EventArgs e) {
            txtDestroyTamesOverLevel.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbMaxTributeDinos_Scroll(object sender, EventArgs e) {
            txtMaxTributeDinos.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbMaxTributeItems_Scroll(object sender, EventArgs e) {
            txtMaxTributeItems.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbOverrideSurvivorUploadExpirationValue_Scroll(object sender, EventArgs e) {
            txtOverrideSurvivorUploadExpirationValue.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbOverrideItemUploadExpirationValue_Scroll(object sender, EventArgs e) {
            txtOverrideItemUploadExpirationValue.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbOverrideDinoUploadExpirationValue_Scroll(object sender, EventArgs e) {
            txtOverrideDinoUploadExpirationValue.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbOverrideMinimumDinoReUploadIntervalValue_Scroll(object sender, EventArgs e) {
            txtOverrideMinimumDinoReUploadIntervalValue.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbLogoutInterval_Scroll(object sender, EventArgs e) {
            txtLogoutInterval.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbConnectionInvicibleInterval_Scroll(object sender, EventArgs e) {
            txtConnectionInvicibleInterval.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbIntervalCheckPeriod_Scroll(object sender, EventArgs e) {
            txtIntervalCheckPeriod.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbIntervalMultiplier_Scroll(object sender, EventArgs e) {
            txtIntervalMultiplier.Text = ((sender as TrackBar).Value / 10.0f).ToString();
        }

        private void tbIntervalBaseAmount_Scroll(object sender, EventArgs e) {
            txtIntervalBaseAmount.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbMaxPlayersInTribe_Scroll(object sender, EventArgs e) {
            txtMaxPlayersInTribe.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbTribeNameChangeCooldDown_Scroll(object sender, EventArgs e) {
            txtTribeNameChangeCooldDown.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbTribeSlotReuseCooldown_Scroll(object sender, EventArgs e) {
            txtTribeSlotReuseCooldown.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbMaxAlliancesPerTribe_Scroll(object sender, EventArgs e) {
            txtMaxAlliancesPerTribe.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbMaxTribesPerAlliance_Scroll(object sender, EventArgs e) {
            txtMaxTribesPerAlliance.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbCostumRecipesEffectivenessMultiplier_Scroll(object sender, EventArgs e) {
            txtCostumRecipesEffectivenessMultiplier.Text = ((sender as TrackBar).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbCostumRecipesSkillMultiplier_Scroll(object sender, EventArgs e) {
            txtCostumRecipesSkillMultiplier.Text = ((sender as TrackBar).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbOnlinePlayerCountStart_Scroll(object sender, EventArgs e) {
            txtOnlinePlayerCountStart.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbOnlinePlayerCountEnd_Scroll(object sender, EventArgs e) {
            txtOnlinePlayerCountEnd.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbScaleMaximum_Scroll(object sender, EventArgs e) {
            txtScaleMaximum.Text = ((sender as TrackBar).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbOxygenSwimSpeedStatMultiplier_Scroll(object sender, EventArgs e) {
            txtOxygenSwimSpeedStatMultiplier.Text = ((sender as TrackBar).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbUseCorpseLifeSpanMultiplier_Scroll(object sender, EventArgs e) {
            txtUseCorpseLifeSpanMultiplier.Text = ((sender as TrackBar).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbFjordhawkInventoryCooldown_Scroll(object sender, EventArgs e) {
            txtFjordhawkInventoryCooldown.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbGlobalPoweredBatteryDurability_Scroll(object sender, EventArgs e) {
            txtGlobalPoweredBatteryDurability.Text = ((sender as TrackBar).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbFuelConsumptionIntervalMultiplier_Scroll(object sender, EventArgs e) {
            txtFuelConsumptionIntervalMultiplier.Text = ((sender as TrackBar).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbLimitNonPlayerDroppedItemsRange_Scroll(object sender, EventArgs e) {
            txtLimitNonPlayerDroppedItemsRange.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbLimitNonPlayerDroppedItemsCount_Scroll(object sender, EventArgs e) {
            txtLimitNonPlayerDroppedItemsCount.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbEnableCryopodNerfDuration_Scroll(object sender, EventArgs e) {
            txtEnableCryopodNerfDuration.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbOutgoingDamageMultiplier_Scroll(object sender, EventArgs e) {
            txtOutgoingDamageMultiplier.Text = ((sender as TrackBar).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbIncomingDamageMultiplierPercent_Scroll(object sender, EventArgs e) {
            txtIncomingDamageMultiplierPercent.Text = ((sender as TrackBar).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbWorldBuffScanlingEfficacy_Scroll(object sender, EventArgs e) {
            txtWorldBuffScanlingEfficacy.Text = ((sender as TrackBar).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMutagemSpawnDelayMultiplier_Scroll(object sender, EventArgs e) {
            txtMutagemSpawnDelayMultiplier.Text = ((sender as TrackBar).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbMaxHexagonsPerCharacter_Scroll(object sender, EventArgs e) {
            txtMaxHexagonsPerCharacter.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbHexagonRewardMultiplier_Scroll(object sender, EventArgs e) {
            txtHexagonRewardMultiplier.Text = ((sender as TrackBar).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbHexagonCostMultiplier_Scroll(object sender, EventArgs e) {
            txtHexagonCostMultiplier.Text = ((sender as TrackBar).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbUnicornSpawnInterval_Scroll(object sender, EventArgs e) {
            txtUnicornSpawnInterval.Text = (sender as TrackBar)?.Value.ToString();
        }

        private void tbVolcanoInterval_Scroll(object sender, EventArgs e) {
            txtVolcanoInterval.Text = ((sender as TrackBar).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbVolcanoIntensity_Scroll(object sender, EventArgs e) {
            txtVolcanoIntensity.Text = ((sender as TrackBar).Value / 10.0f).ToString(CultureInfo.InvariantCulture);
        }

        private void tbGenericQualityClamp_Scroll(object sender, EventArgs e) {
            txtGenericQualityClamp.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbArmorClamp_Scroll(object sender, EventArgs e) {
            txtArmorClamp.Text = ((TrackBar)sender).Value.ToString();
        }

        private void tbWeaponDamagePercentClamp_Scroll(object sender, EventArgs e) {
            txtWeaponDamagePercentClamp.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbHypoInsulationClamp_Scroll(object sender, EventArgs e) {
            txtHypoInsulationClamp.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbWeightClamp_Scroll(object sender, EventArgs e) {
            txtWeightClamp.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbMaxDurabilityClamp_Scroll(object sender, EventArgs e) {
            txtMaxDurabilityClamp.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbWeaponClipAmmoClamp_Scroll(object sender, EventArgs e) {
            txtWeaponClipAmmoClamp.Text = (sender as TrackBar).Value.ToString();
        }

        private void tbHyperInsulationClamp_Scroll(object sender, EventArgs e) {
            txtHyperInsulationClamp.Text = (sender as TrackBar).Value.ToString();
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
                float  fvalue = ((TextBox)sender).Text.ToFloat() * 10.0f;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbSupplyCrateLootQualityMultiplier.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtFishingLootQualityMultiplier_TextChanged(object sender, EventArgs e) {
            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() * 10.0f;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbFishingLootQualityMultiplier.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtAutoSavePeriod_TextChanged(object sender, EventArgs e) {
            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat();
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbAutoSavePeriod.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtMOTDDuration_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat();
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbMOTDDuration.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtMOTDInterval_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat();
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbMOTDInterval.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtIdleTimeout_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat();
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbIdleTimeout.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtMaxPlayers_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat();
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbMaxPlayers.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtSpeedBias_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat();
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbSpeedBias.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtRespawnInterval_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat();
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbRespawnInterval.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtPlatformSaddleBuildAreaBoundsMultiplier_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() * 10.0f;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbPlatformSaddleBuildAreaBoundsMultiplier.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtMaxGatewaysOnSaddles_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() ;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbMaxGatewaysOnSaddles.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtMaxDinoLevel_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() ;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbMaxDinoLevel.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtDifficultyOffset_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() * 100.0f;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbDifficultyOffset.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtDestroyTamesOverLevel_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() ;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbDestroyTamesOverLevel.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtTribeSlotReuseCooldown_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() ;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbTribeSlotReuseCooldown.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtTribeNameChangeCooldDown_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat();
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbTribeNameChangeCooldDown.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtMaxPlayersInTribe_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() ;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbMaxPlayersInTribe.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtIntervalBaseAmount_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() ;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbIntervalBaseAmount.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtIntervalMultiplier_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() * 100.0f;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbIntervalMultiplier.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtIntervalCheckPeriod_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() ;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbIntervalCheckPeriod.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtConnectionInvicibleInterval_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() ;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbConnectionInvicibleInterval.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtLogoutInterval_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() ;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbLogoutInterval.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtOverrideMinimumDinoReUploadIntervalValue_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() ;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbOverrideMinimumDinoReUploadIntervalValue.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtOverrideDinoUploadExpirationValue_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() ;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbOverrideSurvivorUploadExpirationValue.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtOverrideItemUploadExpirationValue_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() ;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbOverrideItemUploadExpirationValue.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtOverrideSurvivorUploadExpirationValue_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() ;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbOverrideSurvivorUploadExpirationValue.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtMaxTributeItems_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() ;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbMaxTributeItems.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtMaxTributeDinos_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() ;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbMaxTributeDinos.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtMaxAlliancesPerTribe_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() ;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbMaxAlliancesPerTribe.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtMaxTribesPerAlliance_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() ;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbMaxAlliancesPerTribe.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtCostumRecipesEffectivenessMultiplier_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() * 100.0f ;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbCostumRecipesEffectivenessMultiplier.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtCostumRecipesSkillMultiplier_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() * 100.0f ;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbCostumRecipesSkillMultiplier.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtOnlinePlayerCountStart_TextChanged(object sender, EventArgs e) {

            try {
                float  fvalue = ((TextBox)sender).Text.ToFloat() * 100.0f ;
                string value  = Math.Round(fvalue, 0).ToString(CultureInfo.InvariantCulture);
                tbOnlinePlayerCountStart.SetValueEx(value.ToInt());
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtOnlinePlayerCountEnd_TextChanged(object sender, EventArgs e) {

        }

        private void txtScaleMaximum_TextChanged(object sender, EventArgs e) {

        }

        private void txtOxygenSwimSpeedStatMultiplier_TextChanged(object sender, EventArgs e) {

        }

        private void txtUseCorpseLifeSpanMultiplier_TextChanged(object sender, EventArgs e) {

        }

        private void txtFjordhawkInventoryCooldown_TextChanged(object sender, EventArgs e) {

        }

        private void txtGlobalPoweredBatteryDurability_TextChanged(object sender, EventArgs e) {

        }

        private void txtFuelConsumptionIntervalMultiplier_TextChanged(object sender, EventArgs e) {

        }

        private void txtLimitNonPlayerDroppedItemsRange_TextChanged(object sender, EventArgs e) {

        }

        private void txtLimitNonPlayerDroppedItemsCount_TextChanged(object sender, EventArgs e) {

        }

        private void txtEnableCryopodNerfDuration_TextChanged(object sender, EventArgs e) {

        }

        private void txtOutgoingDamageMultiplier_TextChanged(object sender, EventArgs e) {

        }

        private void txtIncomingDamageMultiplierPercent_TextChanged(object sender, EventArgs e) {

        }

        private void txtWorldBuffScanlingEfficacy_TextChanged(object sender, EventArgs e) {

        }

        private void txtMutagemSpawnDelayMultiplier_TextChanged(object sender, EventArgs e) {

        }

        private void txtMaxHexagonsPerCharacter_TextChanged(object sender, EventArgs e) {

        }

        private void txtHexagonRewardMultiplier_TextChanged(object sender, EventArgs e) {

        }

        private void txtHexagonCostMultiplier_TextChanged(object sender, EventArgs e) {

        }

        private void txtUnicornSpawnInterval_TextChanged(object sender, EventArgs e) {

        }

        private void txtVolcanoInterval_TextChanged(object sender, EventArgs e) {

        }

        private void txtVolcanoIntensity_TextChanged(object sender, EventArgs e) {

        }

        private void txtGenericQualityClamp_TextChanged(object sender, EventArgs e) {

        }

        private void txtArmorClamp_TextChanged(object sender, EventArgs e) {

        }

        private void txtWeaponDamagePercentClamp_TextChanged(object sender, EventArgs e) {

        }

        private void txtHypoInsulationClamp_TextChanged(object sender, EventArgs e) {

        }

        private void txtWeightClamp_TextChanged(object sender, EventArgs e) {

        }

        private void txtMaxDurabilityClamp_TextChanged(object sender, EventArgs e) {

        }

        private void txtWeaponClipAmmoClamp_TextChanged(object sender, EventArgs e) {

        }

        private void txtHyperInsulationClamp_TextChanged(object sender, EventArgs e) {

        }
    }
}