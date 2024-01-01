using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OphiussaServerManager.Components {
    public partial class ArkChatAndNotifications : UserControl {
        internal ArkProfile _profile;
        public ArkChatAndNotifications() {
            InitializeComponent();
        }
        public void LoadData(ref ArkProfile profile) {
            _profile = profile;
            Stopwatch sw = new Stopwatch();
            sw.Start();
             
            UsefullTools.LoadValuesToFields(_profile, this.Controls);
            Console.WriteLine("Elapsed={0}", sw.Elapsed.TotalSeconds);
        }
        public void GetData(ref ArkProfile profile) {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            UsefullTools.LoadFieldsToObject(ref _profile, this.Controls);
             
            sw.Stop();

            Console.WriteLine("Elapsed={0}", sw.Elapsed.TotalSeconds);

        }
    }
}
