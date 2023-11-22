using Microsoft.Win32.TaskScheduler;
using Newtonsoft.Json;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models.Profiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OphiussaServerManager.Forms
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
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
                Common.Models.Settings s = new Common.Models.Settings();
                string jsonString = JsonConvert.SerializeObject(s, Formatting.Indented);
                File.WriteAllText(fileName, jsonString);
            }

            Common.Models.Settings settings = JsonConvert.DeserializeObject<Common.Models.Settings>(File.ReadAllText(fileName));
            LoadSettings(settings);
        }

        public void LoadSettings(Common.Models.Settings sett)
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
            txtBackupInterval.Text = sett.MaxLogFiles.ToString();

            txtUserName.Enabled = !chkAnonymous.Checked;
            txtPassword.Enabled = !chkAnonymous.Checked;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
        }

        private void CreateWindowsTaskS()
        {

            if (MainForm.Settings.AutoUpdate)
            {
                try
                {
                    string fileName = Assembly.GetExecutingAssembly().Location;
                    string taskName = "OphiussaServerManager\\AutoUpdate_" + MainForm.Settings.GUID;

                    Microsoft.Win32.TaskScheduler.Task task = TaskService.Instance.GetTask(taskName);
                    if (task != null)
                    {
                        task.Definition.Triggers.Clear();


                        TimeTrigger timeTrigger = new TimeTrigger();
                        timeTrigger.StartBoundary = DateTime.Now;

                        int hour = Int16.Parse(MainForm.Settings.UpdateInterval.Split(':')[0]);
                        int minute = Int16.Parse(MainForm.Settings.UpdateInterval.Split(':')[1]);
                        timeTrigger.StartBoundary = DateTime.Now.Date;
                        timeTrigger.Repetition.Interval = TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                        timeTrigger.Repetition.Duration = TimeSpan.Zero;
                        timeTrigger.Repetition.StopAtDurationEnd = false;
                        task.Definition.Triggers.Add(timeTrigger);
                        task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                        task.RegisterChanges();
                    }
                    else
                    {
                        TaskDefinition td = TaskService.Instance.NewTask();
                        td.RegistrationInfo.Description = "Auto Update Task";
                        td.Principal.LogonType = TaskLogonType.InteractiveToken;

                        TimeTrigger timeTrigger = new TimeTrigger();
                        timeTrigger.StartBoundary = DateTime.Now;

                        int hour = Int16.Parse(MainForm.Settings.UpdateInterval.Split(':')[0]);
                        int minute = Int16.Parse(MainForm.Settings.UpdateInterval.Split(':')[1]);
                        timeTrigger.StartBoundary = DateTime.Now.Date;
                        timeTrigger.Repetition.Interval = TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                        timeTrigger.Repetition.Duration = TimeSpan.Zero;
                        timeTrigger.Repetition.StopAtDurationEnd = false;
                        td.Triggers.Add(timeTrigger);
                        td.Actions.Add(fileName, " -au");
                        td.Principal.RunLevel = TaskRunLevel.Highest;
                        TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td);

                    }

                    int hour1 = Int16.Parse(MainForm.Settings.UpdateInterval.Split(':')[0]);
                    int minute1 = Int16.Parse(MainForm.Settings.UpdateInterval.Split(':')[1]);
                    TimeSpan ts = TimeSpan.FromHours(hour1) + TimeSpan.FromMinutes(minute1);

                    MessageBox.Show("Auto update will run every " + ts.TotalMinutes + " minutes");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                string taskName = "OphiussaServerManager\\AutoUpdate_" + MainForm.Settings.GUID;
                Microsoft.Win32.TaskScheduler.Task task = TaskService.Instance.GetTask(taskName);
                if (task != null)
                {
                    TaskService.Instance.RootFolder.DeleteTask(taskName);
                }
            }
            if (MainForm.Settings.AutoBackup)
            {
                try
                {
                    string fileName = Assembly.GetExecutingAssembly().Location;
                    string taskName = "OphiussaServerManager\\AutoBackup_" + MainForm.Settings.GUID;

                    Microsoft.Win32.TaskScheduler.Task task = TaskService.Instance.GetTask(taskName);
                    if (task != null)
                    {
                        task.Definition.Triggers.Clear();


                        TimeTrigger timeTrigger = new TimeTrigger();
                        timeTrigger.StartBoundary = DateTime.Now;

                        int hour = Int16.Parse(MainForm.Settings.BackupInterval.Split(':')[0]);
                        int minute = Int16.Parse(MainForm.Settings.BackupInterval.Split(':')[1]);
                        timeTrigger.StartBoundary = DateTime.Now.Date;
                        timeTrigger.Repetition.Interval = TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                        timeTrigger.Repetition.Duration = TimeSpan.Zero;
                        timeTrigger.Repetition.StopAtDurationEnd = false;
                        task.Definition.Triggers.Add(timeTrigger);
                        task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                        task.RegisterChanges();
                    }
                    else
                    {
                        TaskDefinition td = TaskService.Instance.NewTask();
                        td.RegistrationInfo.Description = "Auto Backup Task";
                        td.Principal.LogonType = TaskLogonType.InteractiveToken;

                        TimeTrigger timeTrigger = new TimeTrigger();
                        timeTrigger.StartBoundary = DateTime.Now;

                        int hour = Int16.Parse(MainForm.Settings.BackupInterval.Split(':')[0]);
                        int minute = Int16.Parse(MainForm.Settings.BackupInterval.Split(':')[1]);
                        timeTrigger.StartBoundary = DateTime.Now.Date;
                        timeTrigger.Repetition.Interval = TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                        timeTrigger.Repetition.Duration = TimeSpan.Zero;
                        timeTrigger.Repetition.StopAtDurationEnd = false;
                        td.Triggers.Add(timeTrigger);
                        td.Actions.Add(fileName, " -ab");
                        td.Principal.RunLevel = TaskRunLevel.Highest;
                        TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td);

                    }
                    int hour1 = Int16.Parse(MainForm.Settings.BackupInterval.Split(':')[0]);
                    int minute1 = Int16.Parse(MainForm.Settings.BackupInterval.Split(':')[1]);
                    TimeSpan ts = TimeSpan.FromHours(hour1) + TimeSpan.FromMinutes(minute1);

                    MessageBox.Show("Auto Backup will run every " + ts.TotalMinutes + " minutes");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                string taskName = "OphiussaServerManager\\AutoBackup_" + MainForm.Settings.GUID;
                Microsoft.Win32.TaskScheduler.Task task = TaskService.Instance.GetTask(taskName);
                if (task != null)
                {
                    TaskService.Instance.RootFolder.DeleteTask(taskName);
                }
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
            Common.NetworkTools.DownloadSteamCMD();

        }

        private void chkAnonymous_CheckedChanged(object sender, EventArgs e)
        {
            txtUserName.Enabled = !chkAnonymous.Checked;
            txtPassword.Enabled = !chkAnonymous.Checked;
        }

        private void FrmSettings_Leave(object sender, EventArgs e)
        {
        }

        private void FrmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            string fileName = "config.json";
            Common.Models.Settings sett = new Common.Models.Settings();
            sett.GUID = MainForm.Settings.GUID;
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

            if (!Directory.Exists(txtDataFolder.Text))
            {
                Directory.CreateDirectory(txtDataFolder.Text);
            }
            if (!Directory.Exists(Path.Combine(txtDataFolder.Text, "cache")))
            {
                Directory.CreateDirectory(Path.Combine(txtDataFolder.Text, "cache"));
            }
            if (!Directory.Exists(Path.Combine(txtDataFolder.Text, "StartServer")))
            {
                Directory.CreateDirectory(Path.Combine(txtDataFolder.Text, "StartServer"));
            }
            if (!Directory.Exists(txtInstallFolder.Text))
            {
                Directory.CreateDirectory(txtInstallFolder.Text);
            }
            if (!Directory.Exists(txtSteamCmd.Text))
            {
                Directory.CreateDirectory(txtSteamCmd.Text);
            }
            CreateWindowsTaskS();
        }
    }
}
