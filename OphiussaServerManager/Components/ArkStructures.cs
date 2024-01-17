using System;
using System.Windows.Forms;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Tools;

namespace OphiussaServerManager.Components {
    public partial class ArkStructures : UserControl {
        private ArkProfile _profile;

        public ArkStructures() {
            InitializeComponent();
        }

        private void exTrackBar1_Load(object sender, EventArgs e) { 
        }

        public void LoadData(ref ArkProfile profile) {
            _profile = profile;
          
            UsefullTools.LoadValuesToFields(_profile, Controls);
        }

        public void GetData(ref ArkProfile profile) {
            UsefullTools.LoadFieldsToObject(ref _profile, Controls);

        }

        private void chkChangeFlyerRiding_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkEnableStructureDecay, groupBox34);
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkEnableFastDecay, groupBox1);
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkLimitTurretsInRange, groupBox2);
        }

        private void ArkStructures_Load(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkEnableStructureDecay, groupBox34);
            UsefullTools.ManageCheckGroupBox(chkEnableFastDecay,      groupBox1);
            UsefullTools.ManageCheckGroupBox(chkLimitTurretsInRange,  groupBox2);
        }
    }
}