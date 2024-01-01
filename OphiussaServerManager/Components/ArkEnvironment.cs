using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
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
            Stopwatch sw = new Stopwatch();

            sw.Start();
            UsefullTools.LoadValuesToFields(_profile, this.Controls);

            sw.Stop();

            Console.WriteLine("Elapsed={0}", sw.Elapsed.TotalSeconds);
        }

        public void GetData(ref ArkProfile profile) {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            UsefullTools.LoadFieldsToObject(ref _profile, this.Controls);

            sw.Stop();

            Console.WriteLine("Elapsed={0}", sw.Elapsed.TotalSeconds);

        }

        private void chkAllowRaidDinoFeeding_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkAllowRaidDinoFeeding, groupBox33);
        } 
    }
}
