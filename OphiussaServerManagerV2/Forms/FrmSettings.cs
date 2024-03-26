using OphiussaFramework;
using System;
using System.Diagnostics;
using System.IO;
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
            txtSteamCmd.DataBindings.Add("Text", ConnectionController.Settings, "SteamCMDFolder");
            txtSteamWebApiKey.DataBindings.Add("Text", ConnectionController.Settings, "SteamWepApiKey");
            txtCurseForgeKey.DataBindings.Add("Text", ConnectionController.Settings, "CurseForgeApiKey");
            chkEnableLogs.DataBindings.Add("Checked", ConnectionController.Settings, "EnableLogs");
            txtMaxDays.DataBindings.Add("Value", ConnectionController.Settings, "MaxLogsDays");
            txtMaxFiles.DataBindings.Add("Value", ConnectionController.Settings, "MaxLogFiles");

            
        }

        private void FrmSettings_FormClosing(object sender, FormClosingEventArgs e) {
            ConnectionController.SqlLite.Upsert<Settings>(ConnectionController.Settings);

            if (!Directory.Exists(txtDataFolder.Text)) Directory.CreateDirectory(txtDataFolder.Text);
            if (!Directory.Exists(Path.Combine(txtDataFolder.Text, "cache"))) Directory.CreateDirectory(Path.Combine(txtDataFolder.Text, "cache"));
            if (!Directory.Exists(Path.Combine(txtDataFolder.Text, "StartServer"))) Directory.CreateDirectory(Path.Combine(txtDataFolder.Text, "StartServer"));
            if (!Directory.Exists(txtDefaultInstallationFolder.Text)) Directory.CreateDirectory(txtDefaultInstallationFolder.Text);
            if (!Directory.Exists(txtSteamCmd.Text)) Directory.CreateDirectory(txtSteamCmd.Text);
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

        private void button1_Click_1(object sender, EventArgs e) {

            fd.SelectedPath = txtSteamCmd.Text;
            fd.ShowDialog();
            txtSteamCmd.Text = fd.SelectedPath;
        }

        private void btDefaultInstallFolder_Click(object sender, EventArgs e) {

            fd.SelectedPath = txtDefaultInstallationFolder.Text;
            fd.ShowDialog();
            txtDefaultInstallationFolder.Text = fd.SelectedPath;
        }

        private void btBackupFolder_Click(object sender, EventArgs e) {

            fd.SelectedPath = txtBackupFolder.Text;
            fd.ShowDialog();
            txtBackupFolder.Text = fd.SelectedPath;
        }
    }
}