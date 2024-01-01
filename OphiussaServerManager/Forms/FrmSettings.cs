using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using Newtonsoft.Json;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models;

namespace OphiussaServerManager.Forms {
    public partial class FrmSettings : Form {
        public FrmSettings() {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e) {
            txtBackupDays.Text = tbDeleteDays.Value.ToString();
        }

        private void tbGracePeriod_Scroll(object sender, EventArgs e) {
            txtGrace.Text = tbGracePeriod.Value.ToString();
        }

        private void Settings_Load(object sender, EventArgs e) {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
            if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json"))) {
                var s = new Settings();
                string jsonString = JsonConvert.SerializeObject(s, Formatting.Indented);
                File.WriteAllText(fileName, jsonString);
            }

            var settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(fileName));
            LoadSettings(settings);
        }

        public void LoadSettings(Settings sett) {
            txtSteamKey.Text = sett.SteamKey;
            txtCurseForgeKey.Text = sett.CurseForgeKey;
            chkUpdateOnStart.Checked = sett.UpdateSteamCmdOnStartup;
            txtDataFolder.Text = sett.DataFolder;
            txtInstallFolder.Text = sett.DefaultInstallationFolder;
            txtSteamCmd.Text = sett.SteamCmdLocation;
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
            txtUpdateInterval.Text = sett.UpdateInterval;
            chlPerformPlayer.Checked = sett.PerformOnlinePlayerCheck;
            chkSendMessage.Checked = sett.SendShutdowMessages;
            tbGracePeriod.Value = sett.GracePeriod;
            txtGrace.Text = sett.GracePeriod.ToString();
            txtMessage1.Text = sett.Message1;
            txtMessage2.Text = sett.Message2;
            txtCancelMessage.Text = sett.CancelMessage;
            chkEnableAutoBackup.Checked = sett.AutoBackup;
            txtBackupInterval.Text = sett.BackupInterval;
            chkUseSmartCopy.Checked = sett.UseSmartCopy;
            chkEnableLogs.Checked = sett.EnableLogs;
            txtMaxDays.Text = sett.MaxLogsDays.ToString();
            txtMaxFiles.Text = sett.MaxLogFiles.ToString();

            txtUserName.Enabled = !chkAnonymous.Checked;
            txtPassword.Enabled = !chkAnonymous.Checked;
        }

        private void btSave_Click(object sender, EventArgs e) {
        }

        private void CreateWindowsTaskS(Settings settings) {
            if (settings.AutoUpdate) {
                try {
                    string fileName = Assembly.GetExecutingAssembly().Location;
                    string taskName = "OphiussaServerManager\\AutoUpdate_" + settings.Guid;

                    var task = TaskService.Instance.GetTask(taskName);
                    if (task != null) {
                        task.Definition.Triggers.Clear();


                        var timeTrigger = new TimeTrigger();
                        timeTrigger.StartBoundary = DateTime.Now;

                        int hour = short.Parse(settings.UpdateInterval.Split(':')[0]);
                        int minute = short.Parse(settings.UpdateInterval.Split(':')[1]);
                        timeTrigger.StartBoundary = DateTime.Now.Date;
                        timeTrigger.Repetition.Interval = TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                        timeTrigger.Repetition.Duration = TimeSpan.Zero;
                        timeTrigger.Repetition.StopAtDurationEnd = false;
                        task.Definition.Triggers.Add(timeTrigger);
                        task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                        task.Definition.Settings.Priority = ProcessPriorityClass.Normal;
                        task.RegisterChanges();
                    }
                    else {
                        var td = TaskService.Instance.NewTask();
                        td.RegistrationInfo.Description = "Auto Update Task";
                        td.Principal.LogonType = TaskLogonType.InteractiveToken;

                        var timeTrigger = new TimeTrigger();
                        timeTrigger.StartBoundary = DateTime.Now;

                        int hour = short.Parse(settings.UpdateInterval.Split(':')[0]);
                        int minute = short.Parse(settings.UpdateInterval.Split(':')[1]);
                        timeTrigger.StartBoundary = DateTime.Now.Date;
                        timeTrigger.Repetition.Interval = TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                        timeTrigger.Repetition.Duration = TimeSpan.Zero;
                        timeTrigger.Repetition.StopAtDurationEnd = false;
                        td.Triggers.Add(timeTrigger);
                        td.Actions.Add(fileName, " -au");
                        td.Principal.RunLevel = TaskRunLevel.Highest;
                        td.Settings.Priority  = ProcessPriorityClass.Normal;
                        TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td);
                    }

                    int hour1 = short.Parse(settings.UpdateInterval.Split(':')[0]);
                    int minute1 = short.Parse(settings.UpdateInterval.Split(':')[1]);
                    var ts = TimeSpan.FromHours(hour1) + TimeSpan.FromMinutes(minute1);

                    MessageBox.Show("Auto update will run every " + ts.TotalMinutes + " minutes");
                }
                catch (Exception ex) {
                    OphiussaLogger.Logger.Error(ex);
                    MessageBox.Show(ex.Message);
                }
            }
            else {
                string taskName = "OphiussaServerManager\\AutoUpdate_" + settings.Guid;
                var task = TaskService.Instance.GetTask(taskName);
                if (task != null) TaskService.Instance.RootFolder.DeleteTask(taskName);
            }

            if (settings.AutoBackup) {
                try {
                    string fileName = Assembly.GetExecutingAssembly().Location;
                    string taskName = "OphiussaServerManager\\AutoBackup_" + settings.Guid;

                    var task = TaskService.Instance.GetTask(taskName);
                    if (task != null) {
                        task.Definition.Triggers.Clear();


                        var timeTrigger = new TimeTrigger();
                        timeTrigger.StartBoundary = DateTime.Now;

                        int hour = short.Parse(settings.BackupInterval.Split(':')[0]);
                        int minute = short.Parse(settings.BackupInterval.Split(':')[1]);
                        timeTrigger.StartBoundary = DateTime.Now.Date;
                        timeTrigger.Repetition.Interval = TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                        timeTrigger.Repetition.Duration = TimeSpan.Zero;
                        timeTrigger.Repetition.StopAtDurationEnd = false;
                        task.Definition.Triggers.Add(timeTrigger);
                        task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                        task.Definition.Settings.Priority = ProcessPriorityClass.Normal;
                        task.RegisterChanges();
                    }
                    else {
                        var td = TaskService.Instance.NewTask();
                        td.RegistrationInfo.Description = "Auto Backup Task";
                        td.Principal.LogonType = TaskLogonType.InteractiveToken;

                        var timeTrigger = new TimeTrigger();
                        timeTrigger.StartBoundary = DateTime.Now;

                        int hour = short.Parse(settings.BackupInterval.Split(':')[0]);
                        int minute = short.Parse(settings.BackupInterval.Split(':')[1]);
                        timeTrigger.StartBoundary = DateTime.Now.Date;
                        timeTrigger.Repetition.Interval = TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                        timeTrigger.Repetition.Duration = TimeSpan.Zero;
                        timeTrigger.Repetition.StopAtDurationEnd = false;
                        td.Triggers.Add(timeTrigger);
                        td.Actions.Add(fileName, " -ab");
                        td.Principal.RunLevel = TaskRunLevel.Highest;
                        td.Settings.Priority = ProcessPriorityClass.Normal;
                        TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td);
                    }

                    int hour1 = short.Parse(settings.BackupInterval.Split(':')[0]);
                    int minute1 = short.Parse(settings.BackupInterval.Split(':')[1]);
                    var ts = TimeSpan.FromHours(hour1) + TimeSpan.FromMinutes(minute1);

                    MessageBox.Show("Auto Backup will run every " + ts.TotalMinutes + " minutes");
                }
                catch (Exception ex) {
                    OphiussaLogger.Logger.Error(ex);
                    MessageBox.Show(ex.Message);
                }
            }
            else {
                string taskName = "OphiussaServerManager\\AutoBackup_" + settings.Guid;
                var task = TaskService.Instance.GetTask(taskName);
                if (task != null) TaskService.Instance.RootFolder.DeleteTask(taskName);
            }

            //Notification Controller Task

            string fileName2 = Assembly.GetEntryAssembly().Location;
            string taskName2 = "OphiussaServerManager\\Notification_" + settings.Guid;
            var task3 = TaskService.Instance.GetTask(taskName2);
            if (task3 != null) {
                task3.Definition.Triggers.Clear();

                var bt1 = new BootTrigger { Delay = TimeSpan.FromMinutes(30) };
                task3.Definition.Triggers.Add(bt1);

                task3.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                task3.Definition.Settings.Priority = ProcessPriorityClass.Normal;
                task3.RegisterChanges();
            }
            else {
                var td = TaskService.Instance.NewTask();
                td.RegistrationInfo.Description = "Server Notification Controller - " + settings.Guid;
                td.Principal.LogonType = TaskLogonType.InteractiveToken;

                var bt1 = new BootTrigger { Delay = TimeSpan.FromMinutes(30) };
                td.Triggers.Add(bt1);
                td.Actions.Add(fileName2, "-notifications");
                td.Principal.RunLevel = TaskRunLevel.Highest;
                td.Settings.Priority = ProcessPriorityClass.Normal;
                TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName2, td);
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            folderBrowserDialog1.SelectedPath = txtDataFolder.Text;
            folderBrowserDialog1.ShowDialog();
            txtDataFolder.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button3_Click(object sender, EventArgs e) {
            folderBrowserDialog1.SelectedPath = txtInstallFolder.Text;
            folderBrowserDialog1.ShowDialog();
            txtInstallFolder.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button4_Click(object sender, EventArgs e) {
            folderBrowserDialog1.SelectedPath = txtSteamCmd.Text;
            folderBrowserDialog1.ShowDialog();
            txtSteamCmd.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button1_Click(object sender, EventArgs e) {
            folderBrowserDialog1.SelectedPath = txtBackup.Text;
            folderBrowserDialog1.ShowDialog();
            txtBackup.Text = folderBrowserDialog1.SelectedPath;
        }

        private void chkAnonymous_CheckedChanged(object sender, EventArgs e) {
            txtUserName.Enabled = !chkAnonymous.Checked;
            txtPassword.Enabled = !chkAnonymous.Checked;
            lblWarningSteam.Visible = !chkAnonymous.Checked;
        }

        private void FrmSettings_Leave(object sender, EventArgs e) {
        }

        private void FrmSettings_FormClosing(object sender, FormClosingEventArgs e) {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
            var sett = new Settings();
            if (MainForm.Settings == null) {
                var guid = Guid.NewGuid();
                sett.Guid = guid.ToString();
            }
            else {
                sett.Guid = MainForm.Settings.Guid;
            }

            sett.SteamKey = txtSteamKey.Text;
            sett.CurseForgeKey = txtCurseForgeKey.Text;
            sett.UpdateSteamCmdOnStartup = chkUpdateOnStart.Checked;
            sett.DataFolder = txtDataFolder.Text;
            sett.DefaultInstallationFolder = txtInstallFolder.Text;
            sett.SteamCmdLocation = txtSteamCmd.Text;
            sett.UseAnonymousConnection = chkAnonymous.Checked;
            sett.CryptedSteamUserName = EncryptionUtils.EncryptString(sett.CryptKey, txtUserName.Text);
            sett.CryptedSteamPassword = EncryptionUtils.EncryptString(sett.CryptKey, txtPassword.Text);
            sett.ValidateProfileOnServerStart = chkValidate.Checked;
            sett.PerformServerAndModUpdateOnServerStart = chkPerform.Checked;
            sett.UpdateModsWhenUpdatingServer = chkUpdateMods.Checked;
            sett.BackupDirectory = txtBackup.Text;
            sett.IncludeSaveGamesFolder = chkIncludeSaveGames.Checked;
            sett.DeleteOldBackupFiles = chkDeleteOld.Checked;
            sett.DeleteFilesDays = tbDeleteDays.Value;
            sett.WorldSaveMessage = txtWordSave.Text;
            sett.AutoUpdate = chkAutoUpdate.Checked;
            sett.UpdateInterval = txtUpdateInterval.Text;
            sett.PerformOnlinePlayerCheck = chlPerformPlayer.Checked;
            sett.SendShutdowMessages = chkSendMessage.Checked;
            sett.GracePeriod = tbGracePeriod.Value;
            sett.Message1 = txtMessage1.Text;
            sett.Message2 = txtMessage2.Text;
            sett.CancelMessage = txtCancelMessage.Text;
            sett.AutoBackup = chkEnableAutoBackup.Checked;
            sett.BackupInterval = txtBackupInterval.Text;
            sett.UseSmartCopy = chkUseSmartCopy.Checked;

            sett.EnableLogs = chkEnableLogs.Checked;
            sett.MaxLogsDays = int.Parse(txtMaxDays.Text);
            sett.MaxLogFiles = int.Parse(txtMaxFiles.Text);

            string jsonString = JsonConvert.SerializeObject(sett, Formatting.Indented);
            File.WriteAllText(fileName, jsonString);

            if (!Directory.Exists(txtDataFolder.Text)) Directory.CreateDirectory(txtDataFolder.Text);
            if (!Directory.Exists(Path.Combine(txtDataFolder.Text, "cache"))) Directory.CreateDirectory(Path.Combine(txtDataFolder.Text, "cache"));
            if (!Directory.Exists(Path.Combine(txtDataFolder.Text, "StartServer"))) Directory.CreateDirectory(Path.Combine(txtDataFolder.Text, "StartServer"));
            if (!Directory.Exists(txtInstallFolder.Text)) Directory.CreateDirectory(txtInstallFolder.Text);
            if (!Directory.Exists(txtSteamCmd.Text)) Directory.CreateDirectory(txtSteamCmd.Text);
            CreateWindowsTaskS(sett);
        }

        private void button5_Click(object sender, EventArgs e) {
            Process.Start("https://steamcommunity.com/dev/apikey");
        }

        private void button6_Click(object sender, EventArgs e) {
            Process.Start("https://console.curseforge.com/?#/api-keys");
        }
    }
}