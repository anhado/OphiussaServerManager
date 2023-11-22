using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using OphiussaServerManager.Common.Models.SupportedServers;

namespace OphiussaServerManager.Forms
{
    public partial class FrmServerMonitor : Form
    {
        Dictionary<string, Profile> Profiles = new Dictionary<string, Profile>();
        public static Common.Models.Settings Settings;
        public FrmServerMonitor()
        {
            InitializeComponent();
        }

        private void ServerMonitor_Load(object sender, EventArgs e)
        {
            if (!File.Exists("config.json"))
            {
                Forms.FrmSettings settings = new Forms.FrmSettings();
                settings.ShowDialog();
            }
            Settings = JsonConvert.DeserializeObject<Common.Models.Settings>(File.ReadAllText("config.json"));
            LoadProfiles();
        }

        private void LoadProfiles()
        {
            monitorGridBindingSource.Clear();
            try
            {
                string dir = Settings.DataFolder + "Profiles\\";
                if (!Directory.Exists(dir))
                {
                    return;
                }

                string[] files = System.IO.Directory.GetFiles(dir);
                foreach (string file in files)
                {
                    Profile p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(file));
                    if (p != null)
                    {
                        switch (p.Type.ServerType)
                        {
                            case EnumServerType.ArkSurviveEvolved:
                            case EnumServerType.ArkSurviveAscended:

                                monitorGridBindingSource.Add(
                                    new Common.Models.MonitorGrid()
                                    {
                                        Select = false,
                                        Profile = p.Name,
                                        ServerName = p.ARKConfiguration.Administration.ServerName,
                                        Map = p.ARKConfiguration.Administration.MapName,
                                        Mods = p.ARKConfiguration.Administration.ModIDs.Count,
                                        Status = !p.IsInstalled ? "Uninstalled" : (p.IsRunning ? "Running" : "Stopped"),
                                        Version = p.GetVersion() == "" ? p.GetBuild(): p.GetVersion(),
                                        Ports = p.ARKConfiguration.Administration.ServerPort + "," + p.ARKConfiguration.Administration.PeerPort + "," + p.ARKConfiguration.Administration.QueryPort,
                                        Players = p.ARKConfiguration.Administration.MaxPlayers.ToString()
                                    }
                                    );
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                OphiussaLogger.logger.Error(e);
                MessageBox.Show($"LoadProfiles: {e.Message}");
            }
        }
    }
}
