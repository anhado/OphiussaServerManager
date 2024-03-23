using System;
using System.Windows.Forms;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.Interfaces;

namespace BasePlugin.Forms {
    public partial class FrmConfigurationForm : Form {
        private IPlugin _plugin;
        private TabPage _tabPage;

        public FrmConfigurationForm(IPlugin plugin, TabPage tab) {
            _plugin = plugin;
            InitializeComponent();
            profileHeader1.Profile = _plugin.Profile;
            profileHeader1.Plugin  = _plugin;
            profileHeader1.Tab     = tab;
            _tabPage               = tab;
        }

        private void profileHeader1_ClickSave(object sender, EventArgs e) {
            try { 
                _plugin.Save();
                OphiussaFramework.Models.Message msg =  _plugin.SaveSettingsToDisk();
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
            _plugin.InstallServer();
        }

        private void profileHeader1_ClickStartStop(object sender, EventArgs e) {
            if (profileHeader1.IsRunning)  
                _plugin.StartServer();
            else 
                _plugin.StopServer();
        }

        private void FrmConfigurationForm_Load(object sender, EventArgs e) {

        }

        private void profileHeader1_TabHeaderChange(object sender, OphiussaFramework.Models.OphiussaEventArgs e) {
            _plugin.TabHeaderChange();
        }
    }
}