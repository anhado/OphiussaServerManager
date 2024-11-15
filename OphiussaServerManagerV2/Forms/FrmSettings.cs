﻿using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using OphiussaFramework;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.Extensions;
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
            txtGUID.DataBindings.Add("Text", ConnectionController.Settings, "GUID",true,DataSourceUpdateMode.OnPropertyChanged);
            txtDefaultInstallationFolder.DataBindings.Add("Text", ConnectionController.Settings, "DefaultInstallFolder", true, DataSourceUpdateMode.OnPropertyChanged);
            txtBackupFolder.DataBindings.Add("Text", ConnectionController.Settings, "BackupFolder", true, DataSourceUpdateMode.OnPropertyChanged);
            txtDataFolder.DataBindings.Add("Text", ConnectionController.Settings, "DataFolder", true, DataSourceUpdateMode.OnPropertyChanged);
            txtSteamCmd.DataBindings.Add("Text", ConnectionController.Settings, "SteamCMDFolder", true, DataSourceUpdateMode.OnPropertyChanged);
            txtSteamKey.DataBindings.Add("Text", ConnectionController.Settings, "SteamWepApiKey", true, DataSourceUpdateMode.OnPropertyChanged);
            txtCurseForgeKey.DataBindings.Add("Text", ConnectionController.Settings, "CurseForgeApiKey", true, DataSourceUpdateMode.OnPropertyChanged);
            chkEnableLogs.DataBindings.Add("Checked", ConnectionController.Settings, "EnableLogs", true, DataSourceUpdateMode.OnPropertyChanged);
            txtMaxDays.DataBindings.Add("Text", ConnectionController.Settings, "MaxLogsDays", true, DataSourceUpdateMode.OnPropertyChanged);
            txtMaxFiles.DataBindings.Add("Text", ConnectionController.Settings, "MaxLogFiles", true, DataSourceUpdateMode.OnPropertyChanged);
            txtUserName.DataBindings.Add("Text", ConnectionController.Settings, "SteamUser", true, DataSourceUpdateMode.OnPropertyChanged);
            txtPassword.DataBindings.Add("Text", ConnectionController.Settings, "SteamPwd", true, DataSourceUpdateMode.OnPropertyChanged);
            chkAnonymous.DataBindings.Add("Checked", ConnectionController.Settings, "UseAnonymous", true, DataSourceUpdateMode.OnPropertyChanged);
            chkUpdateOnStart.DataBindings.Add("Checked", ConnectionController.Settings, "UpdateSteamCMDStart", true, DataSourceUpdateMode.OnPropertyChanged);
            chkEnableAutoUpdate.DataBindings.Add("Checked", ConnectionController.Settings, "EnableAutoUpdate", true, DataSourceUpdateMode.OnPropertyChanged);
            chkEnableAutoBackup.DataBindings.Add("Checked", ConnectionController.Settings, "EnableAutoBackup", true, DataSourceUpdateMode.OnPropertyChanged);
            txtUpdateInterval.DataBindings.Add("Text", ConnectionController.Settings, "UpdateInterval", true, DataSourceUpdateMode.OnPropertyChanged);
            txtBackupInterval.DataBindings.Add("Text", ConnectionController.Settings, "BackupInterval", true, DataSourceUpdateMode.OnPropertyChanged);
            chkDeleteOld.DataBindings.Add("Checked", ConnectionController.Settings, "DeleteOldBackups", true, DataSourceUpdateMode.OnPropertyChanged);
            chkUseSmartCopy.DataBindings.Add("Checked", ConnectionController.Settings, "UseSmartCopy", true, DataSourceUpdateMode.OnPropertyChanged);
            tbDeleteDays.DataBindings.Add("Value", ConnectionController.Settings, "DaysToKeep", true, DataSourceUpdateMode.OnPropertyChanged);
            chkUpdateSequencial.DataBindings.Add("Checked", ConnectionController.Settings, "UpdateSequencial", true, DataSourceUpdateMode.OnPropertyChanged);
            txtBackupDays.Text = ConnectionController.Settings.DaysToKeep.ToString();
              
        }

        private void FrmSettings_FormClosing(object sender, FormClosingEventArgs e) {
            if (MessageBox.Show("Do you want to save the changes?", "Save Settings", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;

            txtUpdateInterval.DataBindings["Text"]?.WriteValue();

            ConnectionController.SqlLite.Upsert<Settings>(ConnectionController.Settings);

            if (!Directory.Exists(txtDataFolder.Text)) Directory.CreateDirectory(txtDataFolder.Text);
            if (!Directory.Exists(Path.Combine(txtDataFolder.Text, "cache"))) Directory.CreateDirectory(Path.Combine(txtDataFolder.Text,       "cache"));
            if (!Directory.Exists(Path.Combine(txtDataFolder.Text, "StartServer"))) Directory.CreateDirectory(Path.Combine(txtDataFolder.Text, "StartServer"));
            if (!Directory.Exists(txtDefaultInstallationFolder.Text)) Directory.CreateDirectory(txtDefaultInstallationFolder.Text);
            if (!Directory.Exists(txtSteamCmd.Text)) Directory.CreateDirectory(txtSteamCmd.Text);

            CreateWindowsTasks();
        }

        private void CreateWindowsTasks() {
            if (ConnectionController.Settings.EnableAutoUpdate) {
                try {
                    string fileName = Assembly.GetExecutingAssembly().Location;
                    string taskName = "OphiussaServerManager\\AutoUpdate_" + ConnectionController.Settings.GUID;

                    var task = TaskService.Instance.GetTask(taskName);
                    if (task != null) {
                        task.Definition.Triggers.Clear();


                        var timeTrigger = new TimeTrigger();
                        timeTrigger.StartBoundary = DateTime.Now;

                        int hour   = short.Parse(ConnectionController.Settings.UpdateInterval.Split(':')[0]);
                        int minute = short.Parse(ConnectionController.Settings.UpdateInterval.Split(':')[1]);
                        timeTrigger.StartBoundary                = DateTime.Now.Date;
                        timeTrigger.Repetition.Interval          = TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                        timeTrigger.Repetition.Duration          = TimeSpan.Zero;
                        timeTrigger.Repetition.StopAtDurationEnd = false;
                        task.Definition.Triggers.Add(timeTrigger);
                        task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                        task.Definition.Settings.Priority  = ProcessPriorityClass.Normal;
                        task.RegisterChanges();
                    }
                    else {
                        var td = TaskService.Instance.NewTask();
                        td.RegistrationInfo.Description = "Auto Update Task";
                        td.Principal.LogonType          = TaskLogonType.InteractiveToken;

                        var timeTrigger = new TimeTrigger();
                        timeTrigger.StartBoundary = DateTime.Now;

                        int hour   = short.Parse(ConnectionController.Settings.UpdateInterval.Split(':')[0]);
                        int minute = short.Parse(ConnectionController.Settings.UpdateInterval.Split(':')[1]);
                        timeTrigger.StartBoundary                = DateTime.Now.Date;
                        timeTrigger.Repetition.Interval          = TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                        timeTrigger.Repetition.Duration          = TimeSpan.Zero;
                        timeTrigger.Repetition.StopAtDurationEnd = false;
                        td.Triggers.Add(timeTrigger);
                        td.Actions.Add(fileName, " -au");
                        td.Principal.RunLevel = TaskRunLevel.Highest;
                        td.Settings.Priority  = ProcessPriorityClass.Normal;
                        TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td);
                    }

                    int hour1   = short.Parse(ConnectionController.Settings.UpdateInterval.Split(':')[0]);
                    int minute1 = short.Parse(ConnectionController.Settings.UpdateInterval.Split(':')[1]);
                    var ts      = TimeSpan.FromHours(hour1) + TimeSpan.FromMinutes(minute1);

                    MessageBox.Show("Auto update will run every " + ts.TotalMinutes + " minutes");
                }
                catch (Exception ex) {
                    OphiussaLogger.Logger.Error(ex);
                    MessageBox.Show(ex.Message);
                }
            }
            else {
                string taskName = "OphiussaServerManager\\AutoUpdate_" + ConnectionController.Settings.GUID;
                var    task     = TaskService.Instance.GetTask(taskName);
                if (task != null) TaskService.Instance.RootFolder.DeleteTask(taskName);
            }

            if (ConnectionController.Settings.EnableAutoBackup) {
                try {
                    string fileName = Assembly.GetExecutingAssembly().Location;
                    string taskName = "OphiussaServerManager\\AutoBackup_" + ConnectionController.Settings.GUID;

                    var task = TaskService.Instance.GetTask(taskName);
                    if (task != null) {
                        task.Definition.Triggers.Clear();


                        var timeTrigger = new TimeTrigger();
                        timeTrigger.StartBoundary = DateTime.Now;

                        int hour   = short.Parse(ConnectionController.Settings.BackupInterval.Split(':')[0]);
                        int minute = short.Parse(ConnectionController.Settings.BackupInterval.Split(':')[1]);
                        timeTrigger.StartBoundary                = DateTime.Now.Date;
                        timeTrigger.Repetition.Interval          = TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                        timeTrigger.Repetition.Duration          = TimeSpan.Zero;
                        timeTrigger.Repetition.StopAtDurationEnd = false;
                        task.Definition.Triggers.Add(timeTrigger);
                        task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                        task.Definition.Settings.Priority  = ProcessPriorityClass.Normal;
                        task.RegisterChanges();
                    }
                    else {
                        var td = TaskService.Instance.NewTask();
                        td.RegistrationInfo.Description = "Auto Backup Task";
                        td.Principal.LogonType          = TaskLogonType.InteractiveToken;

                        var timeTrigger = new TimeTrigger();
                        timeTrigger.StartBoundary = DateTime.Now;

                        int hour   = short.Parse(ConnectionController.Settings.BackupInterval.Split(':')[0]);
                        int minute = short.Parse(ConnectionController.Settings.BackupInterval.Split(':')[1]);
                        timeTrigger.StartBoundary                = DateTime.Now.Date;
                        timeTrigger.Repetition.Interval          = TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                        timeTrigger.Repetition.Duration          = TimeSpan.Zero;
                        timeTrigger.Repetition.StopAtDurationEnd = false;
                        td.Triggers.Add(timeTrigger);
                        td.Actions.Add(fileName, " -ab");
                        td.Principal.RunLevel = TaskRunLevel.Highest;
                        td.Settings.Priority  = ProcessPriorityClass.Normal;
                        TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td);
                    }

                    int hour1   = short.Parse(ConnectionController.Settings.BackupInterval.Split(':')[0]);
                    int minute1 = short.Parse(ConnectionController.Settings.BackupInterval.Split(':')[1]);
                    var ts      = TimeSpan.FromHours(hour1) + TimeSpan.FromMinutes(minute1);

                    MessageBox.Show("Auto Backup will run every " + ts.TotalMinutes + " minutes");
                }
                catch (Exception ex) {
                    OphiussaLogger.Logger.Error(ex);
                    MessageBox.Show(ex.Message);
                }
            }
            else {
                string taskName = "OphiussaServerManager\\AutoBackup_" + ConnectionController.Settings.GUID;
                var    task     = TaskService.Instance.GetTask(taskName);
                if (task != null) TaskService.Instance.RootFolder.DeleteTask(taskName);
            }
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

        private void tbDeleteDays_Scroll(object sender, EventArgs e) {
            txtBackupDays.Text = tbDeleteDays.Value.ToString();
        }
    }
}