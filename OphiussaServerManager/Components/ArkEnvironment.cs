using System;
using System.Diagnostics;
using System.Windows.Forms;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Tools;

namespace OphiussaServerManager.Components {
    public partial class ArkEnvironment : UserControl {
        internal ArkProfile _profile;

        public ArkEnvironment() {
            InitializeComponent();
            UsefullTools.ManageCheckGroupBox(chkAllowRaidDinoFeeding, groupBox33);
        }


        public void LoadData(ref ArkProfile profile) {
            _profile = profile;
           
            UsefullTools.LoadValuesToFields(_profile, Controls);

        }

        public void GetData(ref ArkProfile profile) {
           
            UsefullTools.LoadFieldsToObject(ref _profile, Controls);

        }

        private void chkAllowRaidDinoFeeding_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkAllowRaidDinoFeeding, groupBox33);
        }
    }
}