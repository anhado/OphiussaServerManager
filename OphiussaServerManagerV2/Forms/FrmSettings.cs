using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace OphiussaServerManagerV2 {
    public partial class FrmSettings : Form {
        public FrmSettings() {
            InitializeComponent();
        }

        private void FrmSettings_Load(object sender, EventArgs e) {
            LoadSettings();
        }

        private void LoadSettings() {
            txtGUID.DataBindings.Add("Text", Global.Settings, "GUID");
            txtDefaultInstallationFolder.DataBindings.Add("Text", Global.Settings, "DefaultInstallFolder");
            txtBackupFolder.DataBindings.Add("Text", Global.Settings, "BackupFolder");
            txtDataFolder.DataBindings.Add("Text", Global.Settings, "DataFolder");
            txtSteamWebApiKey.DataBindings.Add("Text", Global.Settings, "SteamWepApiKey");
            txtCurseForgeKey.DataBindings.Add("Text", Global.Settings, "CurseForgeApiKey");
            chkEnableLogs.DataBindings.Add("Checked", Global.Settings, "EnableLogs");
            txtMaxDays.DataBindings.Add("Text", Global.Settings, "MaxLogsDays");
            txtMaxFiles.DataBindings.Add("Text", Global.Settings, "MaxLogFiles");
        }

        private void FrmSettings_FormClosing(object sender, FormClosingEventArgs e) {
            Global.SqlLite.UpSertSettings(Global.Settings);
        }

        private void button1_Click(object sender, EventArgs e) {
            fd.SelectedPath = txtDataFolder.Text;
            fd.ShowDialog();
            txtDataFolder.Text = fd.SelectedPath;
        }

        private void button4_Click(object sender, EventArgs e) {
            Process.Start("https://steamcommunity.com/dev/apikey");
        }

        private void button5_Click(object sender, EventArgs e) {
            Process.Start("https://console.curseforge.com/?#/api-keys");
        }

        private void expandCollapsePanel5_Paint(object sender, PaintEventArgs e) {
        }
    }
}