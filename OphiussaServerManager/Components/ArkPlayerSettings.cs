using System;
using System.Diagnostics;
using System.Windows.Forms;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Tools;

namespace OphiussaServerManager.Components {
    public partial class ArkPlayerSettings : UserControl {
        private ArkProfile _profile;

        public ArkPlayerSettings() {
            InitializeComponent();
        }

        public void LoadData(ref ArkProfile profile) {
            _profile = profile;
            UsefullTools.LoadValuesToFields(_profile, Controls);
            if (profile.PlayerBaseStatMultipliers.Count != 12) profile.PlayerBaseStatMultipliers.Reset();
            chkBaseStatMultiplier.Checked = profile.PlayerBaseStatMultipliers.IsEnabled;
            txtBSHealth.Value             = profile.PlayerBaseStatMultipliers[0];
            txtBSStamina.Value            = profile.PlayerBaseStatMultipliers[1];
            txtBSTorpidity.Value          = profile.PlayerBaseStatMultipliers[2];
            txtBSOxygen.Value             = profile.PlayerBaseStatMultipliers[3];
            txtBSFood.Value               = profile.PlayerBaseStatMultipliers[4];
            txtBSWater.Value              = profile.PlayerBaseStatMultipliers[5];
            txtBSTemperature.Value        = profile.PlayerBaseStatMultipliers[6];
            txtBSWeigth.Value             = profile.PlayerBaseStatMultipliers[7];
            txtBSDamage.Value             = profile.PlayerBaseStatMultipliers[8];
            txtBSSpeed.Value              = profile.PlayerBaseStatMultipliers[9];
            txtBSFortitude.Value          = profile.PlayerBaseStatMultipliers[10];
            txtBSCrafting.Value           = profile.PlayerBaseStatMultipliers[11];
            if (profile.PerLevelStatsMultiplier_Player.Count != 12) profile.PerLevelStatsMultiplier_Player.Reset();
            chkPerLeveStatMultiplier.Checked = profile.PerLevelStatsMultiplier_Player.IsEnabled;
            txtPLHealth.Value                = profile.PerLevelStatsMultiplier_Player[0];
            txtPLStamina.Value               = profile.PerLevelStatsMultiplier_Player[1];
            txtPLTorpidity.Value             = profile.PerLevelStatsMultiplier_Player[2];
            txtPLOxygen.Value                = profile.PerLevelStatsMultiplier_Player[3];
            txtPLFood.Value                  = profile.PerLevelStatsMultiplier_Player[4];
            txtPLWater.Value                 = profile.PerLevelStatsMultiplier_Player[5];
            txtPLTemperature.Value           = profile.PerLevelStatsMultiplier_Player[6];
            txtPLWeigth.Value                = profile.PerLevelStatsMultiplier_Player[7];
            txtPLDamage.Value                = profile.PerLevelStatsMultiplier_Player[8];
            txtPLSpeed.Value                 = profile.PerLevelStatsMultiplier_Player[9];
            txtPLFortitude.Value             = profile.PerLevelStatsMultiplier_Player[10];
            txtPLCrafting.Value              = profile.PerLevelStatsMultiplier_Player[11];

        }

        public void GetData(ref ArkProfile profile) {
            UsefullTools.LoadFieldsToObject(ref _profile, Controls);

            _profile.PlayerBaseStatMultipliers.IsEnabled = chkBaseStatMultiplier.Checked;
            if (_profile.PerLevelStatsMultiplier_Player.IsEnabled) {
                _profile.PlayerBaseStatMultipliers[0]  = txtBSHealth.Value;
                _profile.PlayerBaseStatMultipliers[1]  = txtBSStamina.Value;
                _profile.PlayerBaseStatMultipliers[2]  = txtBSTorpidity.Value;
                _profile.PlayerBaseStatMultipliers[3]  = txtBSOxygen.Value;
                _profile.PlayerBaseStatMultipliers[4]  = txtBSFood.Value;
                _profile.PlayerBaseStatMultipliers[5]  = txtBSWater.Value;
                _profile.PlayerBaseStatMultipliers[6]  = txtBSTemperature.Value;
                _profile.PlayerBaseStatMultipliers[7]  = txtBSWeigth.Value;
                _profile.PlayerBaseStatMultipliers[8]  = txtBSDamage.Value;
                _profile.PlayerBaseStatMultipliers[9]  = txtBSSpeed.Value;
                _profile.PlayerBaseStatMultipliers[10] = txtBSFortitude.Value;
                _profile.PlayerBaseStatMultipliers[11] = txtBSCrafting.Value;
            }
            else {
                _profile.PlayerBaseStatMultipliers.Reset();
            }

            _profile.PerLevelStatsMultiplier_Player.IsEnabled = chkPerLeveStatMultiplier.Checked;
            if (_profile.PerLevelStatsMultiplier_Player.IsEnabled) {
                _profile.PerLevelStatsMultiplier_Player[0]  = txtPLHealth.Value;
                _profile.PerLevelStatsMultiplier_Player[1]  = txtPLStamina.Value;
                _profile.PerLevelStatsMultiplier_Player[2]  = txtPLTorpidity.Value;
                _profile.PerLevelStatsMultiplier_Player[3]  = txtPLOxygen.Value;
                _profile.PerLevelStatsMultiplier_Player[4]  = txtPLFood.Value;
                _profile.PerLevelStatsMultiplier_Player[5]  = txtPLWater.Value;
                _profile.PerLevelStatsMultiplier_Player[6]  = txtPLTemperature.Value;
                _profile.PerLevelStatsMultiplier_Player[7]  = txtPLWeigth.Value;
                _profile.PerLevelStatsMultiplier_Player[8]  = txtPLDamage.Value;
                _profile.PerLevelStatsMultiplier_Player[9]  = txtPLSpeed.Value;
                _profile.PerLevelStatsMultiplier_Player[10] = txtPLFortitude.Value;
                _profile.PerLevelStatsMultiplier_Player[11] = txtPLCrafting.Value;
            }
            else {
                _profile.PerLevelStatsMultiplier_Player.Reset();
            }

        }

        private void chkBaseStatMultiplier_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkBaseStatMultiplier, groupBox31);
        }

        private void chkPerLeveStatMultiplier_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkPerLeveStatMultiplier, groupBox1);
        }

        private void ArkPlayerSettings_Load(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkBaseStatMultiplier,    groupBox31);
            UsefullTools.ManageCheckGroupBox(chkPerLeveStatMultiplier, groupBox1);
        }
    }
}