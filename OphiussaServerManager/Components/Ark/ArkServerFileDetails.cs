using System;
using System.Diagnostics;
using System.Windows.Forms;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Tools;

namespace OphiussaServerManager.Components {
    public partial class ArkServerFileDetails : UserControl {
        internal AutoManageSettings _profile;

        public ArkServerFileDetails() {
            InitializeComponent();
        }

        public void LoadData(ref AutoManageSettings profile) {
            _profile = profile;
            
            UsefullTools.LoadValuesToFields(_profile, Controls);

        }

        public void GetData(ref AutoManageSettings _profile) {
           
            UsefullTools.LoadFieldsToObject(ref _profile, Controls);

        } 
    }
}