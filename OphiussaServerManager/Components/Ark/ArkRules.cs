using System;
using System.Diagnostics;
using System.Windows.Forms;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Tools;

namespace OphiussaServerManager.Components {
    public partial class ArkRules : UserControl {
        internal ArkProfile _profile;

        public ArkRules() {
            InitializeComponent();
        }

        public void LoadData(ref ArkProfile profile) {
            _profile = profile;

            _profile.MaxDinoLevel = OfficialDifficultyValueConverter.Convert(_profile.OverrideOfficialDifficulty);

            UsefullTools.LoadValuesToFields(_profile, this.Controls);


        }

        public void GetData(ref ArkProfile profile) {
            UsefullTools.LoadFieldsToObject(ref _profile, this.Controls);

            _profile.OverrideOfficialDifficulty = OfficialDifficultyValueConverter.ConvertBack(_profile.MaxDinoLevel);

        }

        private void exTrackBar50_Load(object sender, EventArgs e) {
        }

        private void chkEnableDifficultOverride_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkEnableDifficultOverride, groupBox12);
        }

        private void chkEnableTributeDownloads_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkEnableTributeDownloads, groupBox13);
        }

        private void chkLimitMaxTributeDinos_CheckedChanged(object sender, EventArgs e) {
            exTrackBar8.Enabled = chkLimitMaxTributeDinos.Checked;
        }

        private void chkLimitTributeItems_CheckedChanged(object sender, EventArgs e) {
            exTrackBar9.Enabled = chkLimitTributeItems.Checked;
        }

        private void chkOverrideSurvivorUploadExpiration_CheckedChanged(object sender, EventArgs e) {
            exTrackBar11.Enabled = chkOverrideSurvivorUploadExpiration.Checked;
        }

        private void chkOverrideItemUploadExpiration_CheckedChanged(object sender, EventArgs e) {
            exTrackBar12.Enabled = chkOverrideItemUploadExpiration.Checked;
        }

        private void chkOverrideDinoUploadExpiration_CheckedChanged(object sender, EventArgs e) {
            exTrackBar13.Enabled = chkOverrideDinoUploadExpiration.Checked;
        }

        private void chkOverrideMinimumDinoReUploadInterval_CheckedChanged(object sender, EventArgs e) {
            exTrackBar10.Enabled = chkOverrideMinimumDinoReUploadInterval.Checked;
        }

        private void chkIncreasePVPRespawnInterval_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkIncreasePVPRespawnInterval, groupBox17);
        }

        private void chkPreventOfflinePVP_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkPreventOfflinePVP, groupBox28);
        }

        private void chkPVESchedule_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkPVESchedule, groupBox16);
        }

        private void chkAllowTribeAlliances_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkAllowTribeAlliances, groupBox18);
        }

        private void chkAllowCostumRecipes_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkAllowCostumRecipes, groupBox20);
        }

        private void chkEnableDiseases_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkEnableDiseases, groupBox21);
        }

        private void chkOverrideNPCNetworkStasisRangeScale_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkOverrideNPCNetworkStasisRangeScale, groupBox22);
        }

        private void chkEnableCryopodNerf_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkEnableCryopodNerf, groupBox23);
        }

        private void chkEnableRagnarokSettings_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkEnableRagnarokSettings, groupBox30);
        }

        private void chkEnableGenericQualityClamp_CheckedChanged(object sender, EventArgs e) {
            exTrackBar47.Enabled = chkEnableGenericQualityClamp.Checked;
        }

        private void chkEnableArmorClamp_CheckedChanged(object sender, EventArgs e) {
            exTrackBar48.Enabled = chkEnableArmorClamp.Checked;
        }

        private void chkEnableWeaponDamagePercentClamp_CheckedChanged(object sender, EventArgs e) {
            exTrackBar49.Enabled = chkEnableWeaponDamagePercentClamp.Checked;
        }

        private void chkEnableHypoInsulationClamp_CheckedChanged(object sender, EventArgs e) {
            exTrackBar50.Enabled = chkEnableHypoInsulationClamp.Checked;
        }

        private void chkEnableWeightClamp_CheckedChanged(object sender, EventArgs e) {
            exTrackBar51.Enabled = chkEnableWeightClamp.Checked;
        }

        private void chkEnableMaxDurabilityClamp_CheckedChanged(object sender, EventArgs e) {
            exTrackBar52.Enabled = chkEnableMaxDurabilityClamp.Checked;
        }

        private void chkEnableWeaponClipAmmoClamp_CheckedChanged(object sender, EventArgs e) {
            exTrackBar53.Enabled = chkEnableWeaponClipAmmoClamp.Checked;
        }

        private void chkEnableHyperInsulationClamp_CheckedChanged(object sender, EventArgs e) {
            exTrackBar54.Enabled = chkEnableHyperInsulationClamp.Checked;
        }

        private void ArkRules_Load(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkEnableDifficultOverride,            groupBox12);
            UsefullTools.ManageCheckGroupBox(chkEnableTributeDownloads,             groupBox13);
            UsefullTools.ManageCheckGroupBox(chkIncreasePVPRespawnInterval,         groupBox17);
            UsefullTools.ManageCheckGroupBox(chkPreventOfflinePVP,                  groupBox28);
            UsefullTools.ManageCheckGroupBox(chkPVESchedule,                        groupBox16);
            UsefullTools.ManageCheckGroupBox(chkAllowTribeAlliances,                groupBox18);
            UsefullTools.ManageCheckGroupBox(chkAllowCostumRecipes,                 groupBox20);
            UsefullTools.ManageCheckGroupBox(chkEnableDiseases,                     groupBox21);
            UsefullTools.ManageCheckGroupBox(chkOverrideNPCNetworkStasisRangeScale, groupBox22);
            UsefullTools.ManageCheckGroupBox(chkEnableCryopodNerf,                  groupBox23);
            UsefullTools.ManageCheckGroupBox(chkEnableRagnarokSettings,             groupBox30);
            UsefullTools.ManageCheckGroupBox(chkEnableFjordurSettings,              groupBox26);
        }

        private void chkEnableFjordurSettings_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkEnableFjordurSettings, groupBox26);
        }
    }
}