using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using Newtonsoft.Json;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Components;
using OphiussaServerManager.Tools.Update;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace OphiussaServerManager.Forms {

    public partial class FrmArkV2 : Form {

        private Profile _profile;
        private TabPage _tab;

        public FrmArkV2() {
            InitializeComponent();
        }

        public void LoadProfile(Profile profile, TabPage tab) {
            try {
                _profile = profile;
                _tab = tab;
                profileHeader1.LoadData(ref _tab, ref _profile);

                ArkProfile prf = _profile.ArkConfiguration;
                arkAdministration1.LoadData(ref prf, ref _profile);
                arkRules1.LoadData(ref prf);
                arkChatAndNotifications1.LoadData(ref prf);
                arkHUDAndVisuals1.LoadData(ref prf);
                arkPlayerSettings1.LoadData(ref prf);
                arkDinoSettings1.LoadData(ref prf);
                ucArkEnvironment1.LoadData(ref prf);

                AutoManageSettings auto = _profile.AutoManageSettings;
                automaticManagement1.LoadData(ref auto);

            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                MessageBox.Show(e.Message);
            }
        }

        private void SaveProfile() {
            //  if (!MainForm.Settings.Branchs.Contains(cbBranch.Text)) {
            //      MainForm.Settings.Branchs.Add(cbBranch.Text);
            //      MainForm.Settings.SaveSettings();
            //  }

            profileHeader1.GetData(ref _profile); //Get Data from Profile Header


            ArkProfile prf = _profile.ArkConfiguration; //Get Administration Data
            arkAdministration1.GetData(ref prf);
            arkRules1.GetData(ref prf);
            arkChatAndNotifications1.GetData(ref prf);
            arkHUDAndVisuals1.GetData(ref prf);
            arkPlayerSettings1.GetData(ref prf);
            arkDinoSettings1.GetData(ref prf);
            ucArkEnvironment1.GetData(ref prf);

            _profile.SaveProfile(MainForm.Settings);

            LoadProfile(_profile, _tab);
        }

        private async void profileHeader1_StopClickStart(object sender, EventArgs e) {

            try {
                if (_profile.IsRunning) {
                    //TODO: remove from here and place and use the profile closeserver
                    var x = new AutoUpdate();
                    await x.CloseServer(_profile, MainForm.Settings);
                }
                else {
                    _profile.StartServer(MainForm.Settings);
                }
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void profileHeader1_ClickRCON(object sender, EventArgs e) {
            var frm = new FrmRconServer(_profile);
            frm.Show();
        }

        private void profileHeader1_ClickReload(object sender, EventArgs e) {

            try {
                if (MessageBox.Show("Do you want reload from Server Config Files?", "Reload Option",
                                    MessageBoxButtons.OKCancel) == DialogResult.OK) {
                    _profile.ArkConfiguration = _profile.ArkConfiguration.LoadGameIni(_profile);
                    LoadProfile(_profile, _tab);
                }
                else {
                    string dir = MainForm.Settings.DataFolder + "Profiles\\";
                    if (!Directory.Exists(dir)) return;

                    string[] files = Directory.GetFiles(dir);

                    foreach (string file in files) {
                        var p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(file));
                        if (p.Key == _profile.Key) {
                            LoadProfile(p, _tab);
                            break;
                        }
                    }
                }

            }
            catch (Exception exception) {
                OphiussaLogger.Logger.Error(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void profileHeader1_ClickSave(object sender, EventArgs e) {

            try {
                SaveProfile();
                CreateWindowsTasks();

                MainForm.NotificationController.SendReloadCommand(_profile.Key);
                MessageBox.Show("Profile Saved");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void profileHeader1_ClickSync(object sender, EventArgs e) {

        }

        private void profileHeader1_ClickUpgrade(object sender, EventArgs e) {

            SaveProfile();

            var frm = new FrmProgress(MainForm.Settings, _profile);
            frm.ShowDialog();

            LoadProfile(_profile, _tab);
        }



        private void CreateWindowsTasks() {
            #region AutoStartServer

            if (_profile.AutoManageSettings.AutoStartServer) {
                string fileName = MainForm.Settings.DataFolder + $"StartServer\\Run_{_profile.Key.Replace("-", "")}.cmd";
                string taskName = "OphiussaServerManager\\AutoStart_" + _profile.Key;
                var task = TaskService.Instance.GetTask(taskName);
                if (task != null) {
                    task.Definition.Triggers.Clear();
                    if (_profile.AutoManageSettings.AutoStartOn == AutoStart.OnBoot) {
                        var bt1 = new BootTrigger { Delay = TimeSpan.FromMinutes(1) };
                        task.Definition.Triggers.Add(bt1);
                    }
                    else {
                        var lt1 = new LogonTrigger { Delay = TimeSpan.FromMinutes(1) };
                        task.Definition.Triggers.Add(lt1);
                    }

                    task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                    task.Definition.Settings.Priority = ProcessPriorityClass.Normal;
                    task.RegisterChanges();
                }
                else {
                    var td = TaskService.Instance.NewTask();
                    td.RegistrationInfo.Description = "Server Auto-Start - " + _profile.Name;
                    td.Principal.LogonType = TaskLogonType.InteractiveToken;
                    if (_profile.AutoManageSettings.AutoStartOn == AutoStart.OnBoot) {
                        var bt1 = new BootTrigger { Delay = TimeSpan.FromMinutes(1) };
                        td.Triggers.Add(bt1);
                    }
                    else {
                        var lt1 = new LogonTrigger { Delay = TimeSpan.FromMinutes(1) };
                        td.Triggers.Add(lt1);
                    }

                    td.Actions.Add(fileName);
                    td.Principal.RunLevel = TaskRunLevel.Highest;
                    td.Settings.Priority = ProcessPriorityClass.Normal;
                    TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td);
                }
            }
            else {
                string taskName = "OphiussaServerManager\\AutoStart_" + _profile.Key;
                var task = TaskService.Instance.GetTask(taskName);
                if (task != null) TaskService.Instance.RootFolder.DeleteTask(taskName);
            }

            #endregion

            #region Shutdown 1

            if (_profile.AutoManageSettings.ShutdownServer1) {
                string fileName = Assembly.GetExecutingAssembly().Location;
                string taskName = "OphiussaServerManager\\AutoShutDown1_" + _profile.Key;
                var task = TaskService.Instance.GetTask(taskName);

                if (task != null) {
                    task.Definition.Triggers.Clear();

                    DaysOfTheWeek weekday = 0;

                    if (_profile.AutoManageSettings.ShutdownServer1Monday)
                        weekday += 2;
                    if (_profile.AutoManageSettings.ShutdownServer1Tuesday)
                        weekday += 4;
                    if (_profile.AutoManageSettings.ShutdownServer1Wednesday)
                        weekday += 8;
                    if (_profile.AutoManageSettings.ShutdownServer1Thu)
                        weekday += 16;
                    if (_profile.AutoManageSettings.ShutdownServer1Friday)
                        weekday += 32;
                    if (_profile.AutoManageSettings.ShutdownServer1Saturday)
                        weekday += 64;
                    if (_profile.AutoManageSettings.ShutdownServer1Sunday)
                        weekday += 1;
                    var tt = new WeeklyTrigger();

                    int hour = short.Parse(_profile.AutoManageSettings.ShutdownServer1Hour.Split(':')[0]);
                    int minute = short.Parse(_profile.AutoManageSettings.ShutdownServer1Hour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek = weekday;
                    task.Definition.Triggers.Add(tt);
                    task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                    task.Definition.Settings.Priority = ProcessPriorityClass.Normal;
                    task.RegisterChanges();
                }
                else {
                    var td = TaskService.Instance.NewTask();
                    td.RegistrationInfo.Description = "Server Auto-ShutDown 1 - " + _profile.Name;
                    td.Principal.LogonType = TaskLogonType.InteractiveToken;
                    DaysOfTheWeek weekday = 0;

                    if (_profile.AutoManageSettings.ShutdownServer1Monday)
                        weekday += 2;
                    if (_profile.AutoManageSettings.ShutdownServer1Tuesday)
                        weekday += 4;
                    if (_profile.AutoManageSettings.ShutdownServer1Wednesday)
                        weekday += 8;
                    if (_profile.AutoManageSettings.ShutdownServer1Thu)
                        weekday += 16;
                    if (_profile.AutoManageSettings.ShutdownServer1Friday)
                        weekday += 32;
                    if (_profile.AutoManageSettings.ShutdownServer1Saturday)
                        weekday += 64;
                    if (_profile.AutoManageSettings.ShutdownServer1Sunday)
                        weekday += 1;
                    var tt = new WeeklyTrigger();

                    int hour = short.Parse(_profile.AutoManageSettings.ShutdownServer1Hour.Split(':')[0]);
                    int minute = short.Parse(_profile.AutoManageSettings.ShutdownServer1Hour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek = weekday;
                    td.Triggers.Add(tt);
                    td.Actions.Add(fileName, " -as1" + _profile.Key);
                    td.Principal.RunLevel = TaskRunLevel.Highest;
                    td.Settings.Priority = ProcessPriorityClass.Normal;

                    TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td);
                }
            }
            else {
                string taskName = "OphiussaServerManager\\AutoShutDown1_" + _profile.Key;
                var task = TaskService.Instance.GetTask(taskName);
                if (task != null) TaskService.Instance.RootFolder.DeleteTask(taskName);
            }

            #endregion

            #region Shutdown 2

            if (_profile.AutoManageSettings.ShutdownServer2) {
                string fileName = Assembly.GetExecutingAssembly().Location;
                string taskName = "OphiussaServerManager\\AutoShutDown2_" + _profile.Key;
                var task = TaskService.Instance.GetTask(taskName);
                if (task != null) {
                    task.Definition.Triggers.Clear();

                    DaysOfTheWeek weekday = 0;

                    if (_profile.AutoManageSettings.ShutdownServer2Monday)
                        weekday += 2;
                    if (_profile.AutoManageSettings.ShutdownServer2Tuesday)
                        weekday += 4;
                    if (_profile.AutoManageSettings.ShutdownServer2Wednesday)
                        weekday += 8;
                    if (_profile.AutoManageSettings.ShutdownServer2Thu)
                        weekday += 16;
                    if (_profile.AutoManageSettings.ShutdownServer2Friday)
                        weekday += 32;
                    if (_profile.AutoManageSettings.ShutdownServer2Saturday)
                        weekday += 64;
                    if (_profile.AutoManageSettings.ShutdownServer2Sunday)
                        weekday += 1;
                    var tt = new WeeklyTrigger();

                    int hour = short.Parse(_profile.AutoManageSettings.ShutdownServer2Hour.Split(':')[0]);
                    int minute = short.Parse(_profile.AutoManageSettings.ShutdownServer2Hour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek = weekday;
                    task.Definition.Triggers.Add(tt);
                    task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                    task.Definition.Settings.Priority = ProcessPriorityClass.Normal;
                    task.RegisterChanges();
                }
                else {
                    var td = TaskService.Instance.NewTask();
                    td.RegistrationInfo.Description = "Server Auto-ShutDown 2 - " + _profile.Name;
                    td.Principal.LogonType = TaskLogonType.InteractiveToken;

                    DaysOfTheWeek weekday = 0;

                    if (_profile.AutoManageSettings.ShutdownServer2Monday)
                        weekday += 2;
                    if (_profile.AutoManageSettings.ShutdownServer2Tuesday)
                        weekday += 4;
                    if (_profile.AutoManageSettings.ShutdownServer2Wednesday)
                        weekday += 8;
                    if (_profile.AutoManageSettings.ShutdownServer2Thu)
                        weekday += 16;
                    if (_profile.AutoManageSettings.ShutdownServer2Friday)
                        weekday += 32;
                    if (_profile.AutoManageSettings.ShutdownServer2Saturday)
                        weekday += 64;
                    if (_profile.AutoManageSettings.ShutdownServer2Sunday)
                        weekday += 1;
                    var tt = new WeeklyTrigger();

                    int hour = short.Parse(_profile.AutoManageSettings.ShutdownServer2Hour.Split(':')[0]);
                    int minute = short.Parse(_profile.AutoManageSettings.ShutdownServer2Hour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek = weekday;
                    td.Triggers.Add(tt);
                    td.Actions.Add(fileName, " -as2" + _profile.Key);
                    td.Principal.RunLevel = TaskRunLevel.Highest;
                    td.Settings.Priority = ProcessPriorityClass.Normal;
                    TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td);
                }
            }
            else {
                string taskName = "OphiussaServerManager\\AutoShutDown2_" + _profile.Key;
                var task = TaskService.Instance.GetTask(taskName);
                if (task != null) TaskService.Instance.RootFolder.DeleteTask(taskName);
            }

            #endregion
        }
    }
}
