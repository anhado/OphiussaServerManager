using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models.SupportedServers;

namespace OphiussaServerManager.Forms {
    public partial class FrmServerMonitor : Form {
        public static Settings                    Settings;
        private       Dictionary<string, Profile> _profiles = new Dictionary<string, Profile>();

        public FrmServerMonitor() {
            InitializeComponent();
        }

        private void ServerMonitor_Load(object sender, EventArgs e) {
            if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json"))) {
                var settings = new FrmSettings();
                settings.ShowDialog();
            }

            Settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")));
            LoadProfiles();
        }

        private void LoadProfiles() {
            monitorGridBindingSource.Clear();
            try {
                string dir = Settings.DataFolder + "Profiles\\";
                if (!Directory.Exists(dir)) return;

                string[] files = Directory.GetFiles(dir);
                foreach (string file in files) {
                    var p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(file));
                    if (p != null)
                        switch (p.Type.ServerType) {
                            case EnumServerType.ArkSurviveEvolved:
                            case EnumServerType.ArkSurviveAscended:

                                monitorGridBindingSource.Add(
                                                             new MonitorGrid {
                                                                                 Select     = false,
                                                                                 Profile    = p.Name,
                                                                                 ServerName = p.ArkConfiguration.ServerName,
                                                                                 Map        = p.ArkConfiguration.MapName,
                                                                                 Mods       = p.ArkConfiguration.ModIDs.Count,
                                                                                 Status     = !p.IsInstalled ? "Uninstalled" : p.IsRunning ? "Running" : "Stopped",
                                                                                 Version    = p.GetVersion() == "" ? p.GetBuild() : p.GetVersion(),
                                                                                 Ports      = p.ArkConfiguration.ServerPort + "," + p.ArkConfiguration.PeerPort + "," + p.ArkConfiguration.QueryPort,
                                                                                 Players    = p.ArkConfiguration.MaxPlayers.ToString()
                                                                             }
                                                            );
                                break;
                            case EnumServerType.Valheim:
                                monitorGridBindingSource.Add(
                                                             new MonitorGrid {
                                                                                 Select     = false,
                                                                                 Profile    = p.Name,
                                                                                 ServerName = p.ValheimConfiguration.Administration.ServerName,
                                                                                 Map        = p.ValheimConfiguration.Administration.WordName,
                                                                                 Mods       = 0,
                                                                                 Status     = !p.IsInstalled ? "Uninstalled" : p.IsRunning ? "Running" : "Stopped",
                                                                                 Version    = p.GetVersion() == "" ? p.GetBuild() : p.GetVersion(),
                                                                                 Ports      = p.ValheimConfiguration.Administration.ServerPort + "," + p.ValheimConfiguration.Administration.PeerPort,
                                                                                 Players    = "0"
                                                                             }
                                                            );
                                break;
                        }
                }
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                MessageBox.Show($"LoadProfiles: {e.Message}");
            }
        }
    }
}