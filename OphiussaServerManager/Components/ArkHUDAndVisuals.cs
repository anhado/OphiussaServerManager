using System;
using System.Diagnostics;
using System.Windows.Forms;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Tools;

namespace OphiussaServerManager.Components {
    public partial class ArkHUDAndVisuals : UserControl {
        private ArkProfile _profile;

        public ArkHUDAndVisuals() {
            InitializeComponent();
        }

        public void LoadData(ref ArkProfile profile) {
            _profile = profile;
            var sw = new Stopwatch();
            sw.Start();

            UsefullTools.LoadValuesToFields(_profile, Controls);
            Console.WriteLine("ArkHUDAndVisuals={0}", sw.Elapsed.TotalSeconds);
        }

        public void GetData(ref ArkProfile profile) {
            var sw = new Stopwatch();
            sw.Start();
            UsefullTools.LoadFieldsToObject(ref _profile, Controls);

            sw.Stop();

            Console.WriteLine("ArkHUDAndVisuals={0}", sw.Elapsed.TotalSeconds);
        }
    }
}