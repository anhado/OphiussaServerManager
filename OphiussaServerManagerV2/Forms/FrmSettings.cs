using OphiussaFramework;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using OphiussaFramework.Models;

namespace OphiussaServerManagerV2 {
    public partial class FrmSettings : Form {
        public FrmSettings() {
            InitializeComponent();
        }

        private void FrmSettings_Load(object sender, EventArgs e) {
            LoadSettings();
        }

        private void LoadSettings() {
            txtGUID.DataBindings.Add("Text", ConnectionController.Settings, "GUID");
            txtDefaultInstallationFolder.DataBindings.Add("Text", ConnectionController.Settings, "DefaultInstallFolder");
            txtBackupFolder.DataBindings.Add("Text", ConnectionController.Settings, "BackupFolder");
            txtDataFolder.DataBindings.Add("Text", ConnectionController.Settings, "DataFolder");
            txtSteamWebApiKey.DataBindings.Add("Text", ConnectionController.Settings, "SteamWepApiKey");
            txtCurseForgeKey.DataBindings.Add("Text", ConnectionController.Settings, "CurseForgeApiKey");
            chkEnableLogs.DataBindings.Add("Checked", ConnectionController.Settings, "EnableLogs");
            txtMaxDays.DataBindings.Add("Text", ConnectionController.Settings, "MaxLogsDays");
            txtMaxFiles.DataBindings.Add("Text", ConnectionController.Settings, "MaxLogFiles");
        }

        private void FrmSettings_FormClosing(object sender, FormClosingEventArgs e) {
            ConnectionController.SqlLite.Upsert<Settings>(ConnectionController.Settings);
            //ConnectionController.SqlLite.Upsert(ConnectionController.Settings);
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