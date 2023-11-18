using Newtonsoft.Json;
using OphiussaServerManager.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OphiussaServerManager.Forms
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            txtBackupDays.Text = tbDeleteDays.Value.ToString();
        }

        private void tbGracePeriod_Scroll(object sender, EventArgs e)
        {
            txtGrace.Text = tbGracePeriod.Value.ToString();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            string fileName = "config.json";
            if (!System.IO.File.Exists("config.json"))
            {
                Models.Settings s = new Models.Settings();
                string jsonString = JsonConvert.SerializeObject(s, Formatting.Indented);
                File.WriteAllText(fileName, jsonString);
            }

            Models.Settings settings = JsonConvert.DeserializeObject<Models.Settings>(File.ReadAllText(fileName));
            LoadSettings(settings);
        }

        public void LoadSettings(Models.Settings sett)
        {
            chkUpdateOnStart.Checked = sett.UpdateSteamCMDOnStartup;
            txtDataFolder.Text = sett.DataFolder;
            txtInstallFolder.Text = sett.DefaultInstallationFolder;
            txtSteamCmd.Text = sett.SteamCMDLocation;
            chkAnonymous.Checked = sett.UseAnonymousConnection;
            txtUserName.Text = sett.SteamUserName;
            txtPassword.Text = sett.SteamPassword;
            chkValidate.Checked = sett.ValidateProfileOnServerStart;
            chkPerform.Checked = sett.PerformServerAndModUpdateOnServerStart;
            chkUpdateMods.Checked = sett.UpdateModsWhenUpdatingServer;
            txtBackup.Text = sett.BackupDirectory;
            chkIncludeSaveGames.Checked = sett.IncludeSaveGamesFolder;
            chkDeleteOld.Checked = sett.DeleteOldBackupFiles;
            tbDeleteDays.Value = sett.DeleteFilesDays;
            txtBackupDays.Text = sett.DeleteFilesDays.ToString();
            txtWordSave.Text = sett.WorldSaveMessage;
            chkAutoUpdate.Checked = sett.AutoUpdate;
            txtInterval.Text = sett.BackupInterval;
            chlPerformPlayer.Checked = sett.PerformOnlinePlayerCheck;
            chkSendMessage.Checked = sett.SendShutdowMessages;
            tbGracePeriod.Value = sett.GracePeriod;
            txtGrace.Text = sett.GracePeriod.ToString();
            txtMessage1.Text = sett.Message1;
            txtMessage2.Text = sett.Message2;
            txtCancelMessage.Text = sett.CancelMessage;

            txtUserName.Enabled = !chkAnonymous.Checked;
            txtPassword.Enabled = !chkAnonymous.Checked;
        }

        private void btSave_Click(object sender, EventArgs e)
        {

            string fileName = "config.json";
            Models.Settings sett = new Models.Settings();
            sett.UpdateSteamCMDOnStartup = chkUpdateOnStart.Checked;
            sett.DataFolder = txtDataFolder.Text;
            sett.DefaultInstallationFolder = txtInstallFolder.Text;
            sett.SteamCMDLocation = txtSteamCmd.Text;
            sett.UseAnonymousConnection = chkAnonymous.Checked;
            sett.SteamUserName = txtUserName.Text;
            sett.SteamPassword = txtPassword.Text;
            sett.ValidateProfileOnServerStart = chkValidate.Checked;
            sett.PerformServerAndModUpdateOnServerStart = chkPerform.Checked;
            sett.UpdateModsWhenUpdatingServer = chkUpdateMods.Checked;
            sett.BackupDirectory = txtBackup.Text;
            sett.IncludeSaveGamesFolder = chkIncludeSaveGames.Checked;
            sett.DeleteOldBackupFiles = chkDeleteOld.Checked;
            sett.DeleteFilesDays = tbDeleteDays.Value;
            sett.WorldSaveMessage = txtWordSave.Text;
            sett.AutoUpdate = chkAutoUpdate.Checked;
            sett.BackupInterval = txtInterval.Text;
            sett.PerformOnlinePlayerCheck = chlPerformPlayer.Checked;
            sett.SendShutdowMessages = chkSendMessage.Checked;
            sett.GracePeriod = tbGracePeriod.Value;
            sett.Message1 = txtMessage1.Text;
            sett.Message2 = txtMessage2.Text;
            sett.CancelMessage = txtCancelMessage.Text;

            string jsonString = JsonConvert.SerializeObject(sett, Formatting.Indented);
            File.WriteAllText(fileName, jsonString);

            if (!Directory.Exists(txtDataFolder.Text))
            {
                Directory.CreateDirectory(txtDataFolder.Text);
            }
            if (!Directory.Exists(txtDataFolder.Text))
            {
                Directory.CreateDirectory(Path.Combine(txtDataFolder.Text, "cache"));
            }

            if (!Directory.Exists(txtInstallFolder.Text))
            {
                Directory.CreateDirectory(txtInstallFolder.Text);
            }
            if (!Directory.Exists(txtSteamCmd.Text))
            {
                Directory.CreateDirectory(txtSteamCmd.Text);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = txtDataFolder.Text;
            folderBrowserDialog1.ShowDialog();
            txtDataFolder.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = txtInstallFolder.Text;
            folderBrowserDialog1.ShowDialog();
            txtInstallFolder.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = txtSteamCmd.Text;
            folderBrowserDialog1.ShowDialog();
            txtSteamCmd.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = txtBackup.Text;
            folderBrowserDialog1.ShowDialog();
            txtBackup.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            NetworkTools.DownloadSteamCMD();

        }

        private void chkAnonymous_CheckedChanged(object sender, EventArgs e)
        {
            txtUserName.Enabled = !chkAnonymous.Checked;
            txtPassword.Enabled = !chkAnonymous.Checked;
        }
    }
}
