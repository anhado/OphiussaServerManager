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
            var sw = new Stopwatch();

            sw.Start();
            UsefullTools.LoadValuesToFields(_profile, Controls);

            sw.Stop();

            Console.WriteLine("ArkEnvironment={0}", sw.Elapsed.TotalSeconds);
        }

        public void GetData(ref ArkProfile profile) {
            var sw = new Stopwatch();

            sw.Start();
            UsefullTools.LoadFieldsToObject(ref _profile, Controls);

            sw.Stop();

            Console.WriteLine("ArkEnvironment={0}", sw.Elapsed.TotalSeconds);
        }

        private void chkAllowRaidDinoFeeding_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkAllowRaidDinoFeeding, groupBox33);
        }
    }
}