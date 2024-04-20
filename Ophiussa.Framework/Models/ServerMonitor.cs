using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using OphiussaFramework.Interfaces;

namespace OphiussaFramework.Models {
    public class ServerMonitor {  

        public PluginController Controller { get; set; }

        private IProfile Profile { get; set; }

        public event EventHandler<ServerEventArgs> StatusChanged;

        public ServerMonitor(PluginController controller, EventHandler<ServerEventArgs> statusChanged) {
            Controller    =  controller;
            Profile       =  controller.GetProfile();
            StatusChanged += statusChanged;
            Controller.StatusChanged += Controller_StatusChanged;
        }

        private void Controller_StatusChanged(object sender, ServerEventArgs e) {
            StatusChanged.Invoke(sender, e);
        }

        public bool   Selected    { get; set; }
        public string ProfileName => Profile.Name; 
        public string Name        => Controller.GetName();
        public string Ports       => Profile.ServerPort.ToString() + "-" + Profile.PeerPort.ToString() + "-" + Profile.RCONPort.ToString() + "-" + Profile.RCONPort.ToString();
        public string Build       => Controller.GetBuild();

        public string Status {
            get {
                string status = "Not Installed";

                if (Controller.IsRunning) status        = "Is Running";
                else if (Controller.IsInstalled) status = "Not Running"; 

                return status;
            }
        }

        public Image Restart => OphiussaFramework.Properties.Resources.RefreshIcon;
        public Image Stop    => OphiussaFramework.Properties.Resources.Delete;
        public Image Backup  => OphiussaFramework.Properties.Resources.SaveIcon;
        public Image Rcon  => OphiussaFramework.Properties.Resources.Monitor_icon_icon;
    } 
}
