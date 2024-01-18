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
          
            UsefullTools.LoadValuesToFields(_profile, Controls);
        }

        public void GetData(ref ArkProfile profile) {
            UsefullTools.LoadFieldsToObject(ref _profile, Controls);

        }
    }
}