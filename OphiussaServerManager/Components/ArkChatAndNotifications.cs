using System;
using System.Diagnostics;
using System.Windows.Forms;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Tools;

namespace OphiussaServerManager.Components {
    public partial class ArkChatAndNotifications : UserControl {
        internal ArkProfile _profile;

        public ArkChatAndNotifications() {
            InitializeComponent();
        }

        public void LoadData(ref ArkProfile profile) {
            _profile = profile;
            var sw = new Stopwatch();
            sw.Start();

            UsefullTools.LoadValuesToFields(_profile, Controls);
            Console.WriteLine("ArkChatAndNotifications={0}", sw.Elapsed.TotalSeconds);
        }

        public void GetData(ref ArkProfile profile) {
            var sw = new Stopwatch();
            sw.Start();
            UsefullTools.LoadFieldsToObject(ref _profile, Controls);

            sw.Stop();

            Console.WriteLine("ArkChatAndNotifications={0}", sw.Elapsed.TotalSeconds);
        }
    }
}