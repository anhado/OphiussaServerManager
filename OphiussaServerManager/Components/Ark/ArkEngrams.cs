using System;
using System.Windows.Forms;
using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Tools;

namespace OphiussaServerManager.Components {
    public partial class ArkEngrams : UserControl {
        private ArkProfile _profile;

        public ArkEngrams() {
            InitializeComponent();
        }

        public void LoadData(ref ArkProfile profile) {
            _profile = profile;

            UsefullTools.LoadValuesToFields(_profile, Controls);
        }

        public void GetData(ref ArkProfile profile) {
            UsefullTools.LoadFieldsToObject(ref _profile, Controls);
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(CheckBox2, groupBox35);
        }

        private void ArkEngrams_Load(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(CheckBox2, groupBox35);
        }
    }
}