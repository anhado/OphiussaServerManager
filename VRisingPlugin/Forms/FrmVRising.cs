using System;
using System.Windows.Forms;
using OphiussaFramework;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;

namespace VRisingPlugin.Forms {
    public partial class FrmVRising : Form {
        private readonly IPlugin _plugin;
        private readonly Profile Profile;
        private          TabPage _tabPage;

        public FrmVRising(IPlugin plugin, TabPage tab) {
            _plugin = plugin;
            InitializeComponent();
            profileHeader1.Profile       = _plugin.Profile;
            Profile                      = (Profile)_plugin.Profile;
            profileHeader1.Plugin        = _plugin;
            automaticManagement1.Profile = _plugin.Profile;
            automaticManagement1.Plugin  = _plugin;
            profileHeader1.Tab           = tab;
            _tabPage                     = tab;

            if (_plugin.Profile.CpuAffinityList.Count == 0) _plugin.Profile.CpuAffinityList = ConnectionController.ProcessorList;
        }

        private void profileHeader1_ClickSave(object sender, EventArgs e) {
            try {
                _plugin.Profile.AutoManagement = automaticManagement1.GetRestartSettings();
                _plugin.Save();
                automaticManagement1.LoadGrid();
                var msg = _plugin.SaveSettingsToDisk();
                if (msg.Success) MessageBox.Show(msg.MessageText);
                else throw msg.Exception;
            }
            catch (Exception exception) {
                MessageBox.Show(exception.Message);
                OphiussaLogger.Logger.Error(exception);
            }
        }

        private void profileHeader1_ClickRCON(object sender, EventArgs e) {
            _plugin.OpenRCON();
        }

        private void profileHeader1_ClickReload(object sender, EventArgs e) {
            _plugin.Reload();
        }

        private void profileHeader1_ClickSync(object sender, EventArgs e) {
            _plugin.Sync();
        }

        private void profileHeader1_ClickUpgrade(object sender, EventArgs e) {
            try {

                ConnectionController.ServerControllers[Profile.Key].ShowServerInstallationOptions();
            }
            catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
        }

        private void profileHeader1_ClickStartStop(object sender, EventArgs e) {
            if (!profileHeader1.IsRunning)
                _plugin.StartServer();
            else
                _plugin.StopServer();
        }

        private void FrmConfigurationForm_Load(object sender, EventArgs e) {
        }

        private void profileHeader1_TabHeaderChange(object sender, OphiussaEventArgs e) {
            _plugin.TabHeaderChange();
        }

        private void button1_Click(object sender, EventArgs e) {
            var cmdBuilder = new CommandBuilder(_plugin.DefaultCommands);
            cmdBuilder.OpenCommandEditor(fullCommand => {
                                             _plugin.DefaultCommands = fullCommand.ComandList;
                                             MessageBox.Show(fullCommand.ToString());
                                         });
        }
    }
}