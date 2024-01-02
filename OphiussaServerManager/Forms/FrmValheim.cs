using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models.ValheimProfile;
using OphiussaServerManager.Tools;
using OphiussaServerManager.Tools.Update;

namespace OphiussaServerManager.Forms {
    public partial class FrmValheim : Form {
        private bool    _isInstalled = false;
        private bool    _isRunning;
        private int     _processId = -1;
        private Profile _profile;
        private TabPage _tab;

        public FrmValheim() {
            InitializeComponent();
        }

        private void LoadDefaultFieldValues() {
            try {
                //var ret = NetworkTools.GetAllHostIp();

                txtLocalIP.DataSource    = MainForm.IpLists;
                txtLocalIP.ValueMember   = "IP";
                txtLocalIP.DisplayMember = "Description";

                MainForm.Settings.Branchs.Distinct().ToList().ForEach(branch => { cbBranch.Items.Add(branch); });

                cboPriority.DataSource = Enum.GetValues(typeof(ProcessPriorityClass));
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                MessageBox.Show("LoadDefaultFieldValues:" + e.Message);
            }
        }

        public void LoadSettings(Profile profile, TabPage tab) {
            _profile = profile;
            _tab     = tab;
            LoadDefaultFieldValues();

            txtProfileID.Text   = profile.Key;
            txtProfileName.Text = profile.Name;
            tab.Text            = txtProfileName.Text + "          ";
            txtServerType.Text  = profile.Type.ServerTypeDescription;
            txtLocation.Text    = profile.InstallLocation;


            cbBranch.Text            = profile.ValheimConfiguration.Administration.Branch;
            txtServerName.Text       = profile.ValheimConfiguration.Administration.ServerName;
            txtServerPWD.Text        = profile.ValheimConfiguration.Administration.ServerPassword;
            txtLocalIP.SelectedValue = profile.ValheimConfiguration.Administration.LocalIp;
            txtServerPort.Text       = profile.ValheimConfiguration.Administration.ServerPort;
            txtPeerPort.Text         = profile.ValheimConfiguration.Administration.PeerPort;
            chkPublic.Checked        = profile.ValheimConfiguration.Administration.Public;
            chkCrossplay.Checked     = profile.ValheimConfiguration.Administration.Crossplay;
            txtInstanceID.Text       = profile.ValheimConfiguration.Administration.InstanceId;
            txtWorldName.Text        = profile.ValheimConfiguration.Administration.WordName;

            tbPresetNormal.Checked    = profile.ValheimConfiguration.Administration.Preset == Preset.Normal;
            tbPresetCasual.Checked    = profile.ValheimConfiguration.Administration.Preset == Preset.Casual;
            tbPresetEasy.Checked      = profile.ValheimConfiguration.Administration.Preset == Preset.Easy;
            tbPresetHard.Checked      = profile.ValheimConfiguration.Administration.Preset == Preset.Hard;
            tbPresetHardcore.Checked  = profile.ValheimConfiguration.Administration.Preset == Preset.Hardcore;
            tbPresetImmersive.Checked = profile.ValheimConfiguration.Administration.Preset == Preset.Immersive;
            tbPresetHammer.Checked    = profile.ValheimConfiguration.Administration.Preset == Preset.Hammer;

            cLBKeys.SetItemChecked(0, profile.ValheimConfiguration.Administration.NoBuildcost);
            cLBKeys.SetItemChecked(1, profile.ValheimConfiguration.Administration.PlayerEvents);
            cLBKeys.SetItemChecked(2, profile.ValheimConfiguration.Administration.PassiveMobs);
            cLBKeys.SetItemChecked(3, profile.ValheimConfiguration.Administration.NoMap);

            cboPriority.SelectedItem = profile.ValheimConfiguration.Administration.CpuPriority;
            txtAffinity.Text         = profile.ValheimConfiguration.Administration.CpuAffinity;

            rbCombatNone.Checked     = profile.ValheimConfiguration.Administration.Combat == Combat.Default;
            rbCombatVeryEasy.Checked = profile.ValheimConfiguration.Administration.Combat == Combat.VeryEasy;
            rbCombatEasy.Checked     = profile.ValheimConfiguration.Administration.Combat == Combat.Easy;
            rbCombatHard.Checked     = profile.ValheimConfiguration.Administration.Combat == Combat.Hard;
            rbCombatVeryHard.Checked = profile.ValheimConfiguration.Administration.Combat == Combat.VeryHard;

            rbDeathPenaltyNone.Checked     = profile.ValheimConfiguration.Administration.DeathPenalty == DeathPenalty.Default;
            rbDeathPenaltyCasual.Checked   = profile.ValheimConfiguration.Administration.DeathPenalty == DeathPenalty.Casual;
            rbDeathPenaltyVeryEasy.Checked = profile.ValheimConfiguration.Administration.DeathPenalty == DeathPenalty.VeryEasy;
            rbDeathPenaltyEasy.Checked     = profile.ValheimConfiguration.Administration.DeathPenalty == DeathPenalty.Easy;
            rbDeathPenaltyHard.Checked     = profile.ValheimConfiguration.Administration.DeathPenalty == DeathPenalty.Hard;
            rbDeathPenaltyHardCore.Checked = profile.ValheimConfiguration.Administration.DeathPenalty == DeathPenalty.HardCore;

            rbResourcesNone.Checked     = profile.ValheimConfiguration.Administration.Resources == Resources.Default;
            rbResourcesMuchLess.Checked = profile.ValheimConfiguration.Administration.Resources == Resources.MuchLess;
            rbResourcesLess.Checked     = profile.ValheimConfiguration.Administration.Resources == Resources.Less;
            rbResourcesMore.Checked     = profile.ValheimConfiguration.Administration.Resources == Resources.More;
            rbResourcesMuchMore.Checked = profile.ValheimConfiguration.Administration.Resources == Resources.MuchMore;
            rbResourcesMost.Checked     = profile.ValheimConfiguration.Administration.Resources == Resources.Most;

            rbRaidsDefault.Checked  = profile.ValheimConfiguration.Administration.Raids == Raids.Default;
            rbRaidsNone.Checked     = profile.ValheimConfiguration.Administration.Raids == Raids.None;
            rbRaidsMuchLess.Checked = profile.ValheimConfiguration.Administration.Raids == Raids.MuchLess;
            rbRaidsLess.Checked     = profile.ValheimConfiguration.Administration.Raids == Raids.Less;
            rbRaidsMore.Checked     = profile.ValheimConfiguration.Administration.Raids == Raids.More;
            rbRaidsMuchMore.Checked = profile.ValheimConfiguration.Administration.Raids == Raids.MuchMore;

            rbPortalsNone.Checked     = profile.ValheimConfiguration.Administration.Portals == Portals.Default;
            rbPortalsCasual.Checked   = profile.ValheimConfiguration.Administration.Portals == Portals.Casual;
            rbPortalsHard.Checked     = profile.ValheimConfiguration.Administration.Portals == Portals.Hard;
            rbPortalsVeryHard.Checked = profile.ValheimConfiguration.Administration.Portals == Portals.VeryHard;

            tbAutoSavePeriod.Value = profile.ValheimConfiguration.Administration.AutoSavePeriod;
            tbFirstBackup.Value    = profile.ValheimConfiguration.Administration.BackupShort;
            tbSubBackups.Value     = profile.ValheimConfiguration.Administration.BackupLong;

            txtAutoSavePeriod.Text = tbAutoSavePeriod.Value.ToString();
            txtFirstBackup.Text    = tbFirstBackup.Value.ToString();
            txtSubBackups.Text     = tbSubBackups.Value.ToString();

            txtSaveLocation.Text = profile.ValheimConfiguration.Administration.SaveLocation;
            txtLogLocation.Text  = profile.ValheimConfiguration.Administration.LogFileLocation;
            txtBackupToKeep.Text = profile.ValheimConfiguration.Administration.TotalBackups.ToString();

            chkAutoStart.Checked = profile.AutoManageSettings.AutoStartServer;
            rbOnBoot.Checked     = profile.AutoManageSettings.AutoStartOn == AutoStart.OnBoot;
            rbOnLogin.Checked    = profile.AutoManageSettings.AutoStartOn == AutoStart.OnLogin;

            chkShutdown1.Checked = profile.AutoManageSettings.ShutdownServer1;
            txtShutdow1.Text     = profile.AutoManageSettings.ShutdownServer1Hour;
            chkSun1.Checked      = profile.AutoManageSettings.ShutdownServer1Sunday;
            chkMon1.Checked      = profile.AutoManageSettings.ShutdownServer1Monday;
            chkTue1.Checked      = profile.AutoManageSettings.ShutdownServer1Tuesday;
            chkWed1.Checked      = profile.AutoManageSettings.ShutdownServer1Wednesday;
            chkThu1.Checked      = profile.AutoManageSettings.ShutdownServer1Thu;
            chkFri1.Checked      = profile.AutoManageSettings.ShutdownServer1Friday;
            chkSat1.Checked      = profile.AutoManageSettings.ShutdownServer1Saturday;
            chkUpdate1.Checked   = profile.AutoManageSettings.ShutdownServer1PerformUpdate;
            chkRestart1.Checked  = profile.AutoManageSettings.ShutdownServer1Restart;

            chkShutdown2.Checked = profile.AutoManageSettings.ShutdownServer2;
            txtShutdow2.Text     = profile.AutoManageSettings.ShutdownServer2Hour;
            chkSun2.Checked      = profile.AutoManageSettings.ShutdownServer2Sunday;
            chkMon2.Checked      = profile.AutoManageSettings.ShutdownServer2Monday;
            chkTue2.Checked      = profile.AutoManageSettings.ShutdownServer2Tuesday;
            chkWed2.Checked      = profile.AutoManageSettings.ShutdownServer2Wednesday;
            chkThu2.Checked      = profile.AutoManageSettings.ShutdownServer2Thu;
            chkFri2.Checked      = profile.AutoManageSettings.ShutdownServer2Friday;
            chkSat2.Checked      = profile.AutoManageSettings.ShutdownServer2Saturday;
            chkUpdate2.Checked   = profile.AutoManageSettings.ShutdownServer2PerformUpdate;
            chkRestart2.Checked  = profile.AutoManageSettings.ShutdownServer2Restart;


            chkIncludeAutoBackup.Checked = profile.AutoManageSettings.IncludeInAutoBackup;
            chkAutoUpdate.Checked        = profile.AutoManageSettings.IncludeInAutoUpdate;
            chkRestartIfShutdown.Checked = profile.AutoManageSettings.AutoStartServer;


            txtVersion.Text = profile.GetVersion();
            txtBuild.Text   = profile.GetBuild();

            txtCommand.Text = profile.ValheimConfiguration.GetCommandLinesArguments(MainForm.Settings, profile, MainForm.LocaIp);
        }

        private void txtProfileName_Validated(object sender, EventArgs e) {
            _tab.Text = txtProfileName.Text + "          ";
        }

        private void btSave_Click(object sender, EventArgs e) {
            try {
                SaveProfile();
                CreateWindowsTasks();

                MessageBox.Show("Profile Saved");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreateWindowsTasks() {
            #region AutoStartServer

            if (_profile.AutoManageSettings.AutoStartServer) {
                string fileName = MainForm.Settings.DataFolder        + $"StartServer\\Run_{_profile.Key.Replace("-", "")}.bat";
                string taskName = "OphiussaServerManager\\AutoStart_" + _profile.Key;
                var    task     = TaskService.Instance.GetTask(taskName);
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
                    task.Definition.Settings.Priority  = ProcessPriorityClass.Normal;
                    task.RegisterChanges();
                }
                else {
                    var td = TaskService.Instance.NewTask();
                    td.RegistrationInfo.Description = "Server Auto-Start - " + _profile.Name;
                    td.Principal.LogonType          = TaskLogonType.InteractiveToken;
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
                    td.Settings.Priority  = ProcessPriorityClass.Normal;
                    TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td);
                }
            }
            else {
                string taskName = "OphiussaServerManager\\AutoStart_" + _profile.Key;
                var    task     = TaskService.Instance.GetTask(taskName);
                if (task != null) TaskService.Instance.RootFolder.DeleteTask(taskName);
            }

            #endregion

            #region Shutdown 1

            if (_profile.AutoManageSettings.ShutdownServer1) {
                string fileName = Assembly.GetExecutingAssembly().Location;
                string taskName = "OphiussaServerManager\\AutoShutDown1_" + _profile.Key;
                var    task     = TaskService.Instance.GetTask(taskName);

                if (task != null) {
                    task.Definition.Triggers.Clear();

                    DaysOfTheWeek daysofweek = 0;

                    if (_profile.AutoManageSettings.ShutdownServer1Monday) daysofweek    += 2;
                    if (_profile.AutoManageSettings.ShutdownServer1Tuesday) daysofweek   += 4;
                    if (_profile.AutoManageSettings.ShutdownServer1Wednesday) daysofweek += 8;
                    if (_profile.AutoManageSettings.ShutdownServer1Thu) daysofweek       += 16;
                    if (_profile.AutoManageSettings.ShutdownServer1Friday) daysofweek    += 32;
                    if (_profile.AutoManageSettings.ShutdownServer1Saturday) daysofweek  += 64;
                    if (_profile.AutoManageSettings.ShutdownServer1Sunday) daysofweek    += 1;
                    var tt                                                               = new WeeklyTrigger();

                    int hour   = short.Parse(_profile.AutoManageSettings.ShutdownServer1Hour.Split(':')[0]);
                    int minute = short.Parse(_profile.AutoManageSettings.ShutdownServer1Hour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek    = daysofweek;
                    task.Definition.Triggers.Add(tt);
                    task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                    task.Definition.Settings.Priority  = ProcessPriorityClass.Normal;
                    task.RegisterChanges();
                }
                else {
                    var td = TaskService.Instance.NewTask();
                    td.RegistrationInfo.Description = "Server Auto-ShutDown 1 - " + _profile.Name;
                    td.Principal.LogonType          = TaskLogonType.InteractiveToken;
                    DaysOfTheWeek daysofweek = 0;

                    if (_profile.AutoManageSettings.ShutdownServer1Monday) daysofweek    += 2;
                    if (_profile.AutoManageSettings.ShutdownServer1Tuesday) daysofweek   += 4;
                    if (_profile.AutoManageSettings.ShutdownServer1Wednesday) daysofweek += 8;
                    if (_profile.AutoManageSettings.ShutdownServer1Thu) daysofweek       += 16;
                    if (_profile.AutoManageSettings.ShutdownServer1Friday) daysofweek    += 32;
                    if (_profile.AutoManageSettings.ShutdownServer1Saturday) daysofweek  += 64;
                    if (_profile.AutoManageSettings.ShutdownServer1Sunday) daysofweek    += 1;
                    var tt                                                               = new WeeklyTrigger();

                    int hour   = short.Parse(_profile.AutoManageSettings.ShutdownServer1Hour.Split(':')[0]);
                    int minute = short.Parse(_profile.AutoManageSettings.ShutdownServer1Hour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek    = daysofweek;
                    td.Triggers.Add(tt);
                    td.Actions.Add(fileName, " -as1" + _profile.Key);
                    td.Principal.RunLevel = TaskRunLevel.Highest;
                    td.Settings.Priority  = ProcessPriorityClass.Normal;

                    TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td);
                }
            }
            else {
                string taskName = "OphiussaServerManager\\AutoShutDown1_" + _profile.Key;
                var    task     = TaskService.Instance.GetTask(taskName);
                if (task != null) TaskService.Instance.RootFolder.DeleteTask(taskName);
            }

            #endregion

            #region Shutdown 2

            if (_profile.AutoManageSettings.ShutdownServer2) {
                string fileName = Assembly.GetExecutingAssembly().Location;
                string taskName = "OphiussaServerManager\\AutoShutDown2_" + _profile.Key;
                var    task     = TaskService.Instance.GetTask(taskName);
                if (task != null) {
                    task.Definition.Triggers.Clear();

                    DaysOfTheWeek daysofweek = 0;

                    if (_profile.AutoManageSettings.ShutdownServer2Monday) daysofweek    += 2;
                    if (_profile.AutoManageSettings.ShutdownServer2Tuesday) daysofweek   += 4;
                    if (_profile.AutoManageSettings.ShutdownServer2Wednesday) daysofweek += 8;
                    if (_profile.AutoManageSettings.ShutdownServer2Thu) daysofweek       += 16;
                    if (_profile.AutoManageSettings.ShutdownServer2Friday) daysofweek    += 32;
                    if (_profile.AutoManageSettings.ShutdownServer2Saturday) daysofweek  += 64;
                    if (_profile.AutoManageSettings.ShutdownServer2Sunday) daysofweek    += 1;
                    var tt                                                               = new WeeklyTrigger();

                    int hour   = short.Parse(_profile.AutoManageSettings.ShutdownServer2Hour.Split(':')[0]);
                    int minute = short.Parse(_profile.AutoManageSettings.ShutdownServer2Hour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek    = daysofweek;
                    task.Definition.Triggers.Add(tt);
                    task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                    task.Definition.Settings.Priority  = ProcessPriorityClass.Normal;
                    task.RegisterChanges();
                }
                else {
                    var td = TaskService.Instance.NewTask();
                    td.RegistrationInfo.Description = "Server Auto-ShutDown 2 - " + _profile.Name;
                    td.Principal.LogonType          = TaskLogonType.InteractiveToken;

                    DaysOfTheWeek daysofweek = 0;

                    if (_profile.AutoManageSettings.ShutdownServer2Monday) daysofweek    += 2;
                    if (_profile.AutoManageSettings.ShutdownServer2Tuesday) daysofweek   += 4;
                    if (_profile.AutoManageSettings.ShutdownServer2Wednesday) daysofweek += 8;
                    if (_profile.AutoManageSettings.ShutdownServer2Thu) daysofweek       += 16;
                    if (_profile.AutoManageSettings.ShutdownServer2Friday) daysofweek    += 32;
                    if (_profile.AutoManageSettings.ShutdownServer2Saturday) daysofweek  += 64;
                    if (_profile.AutoManageSettings.ShutdownServer2Sunday) daysofweek    += 1;
                    var tt                                                               = new WeeklyTrigger();

                    int hour   = short.Parse(_profile.AutoManageSettings.ShutdownServer2Hour.Split(':')[0]);
                    int minute = short.Parse(_profile.AutoManageSettings.ShutdownServer2Hour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek    = daysofweek;
                    td.Triggers.Add(tt);
                    td.Actions.Add(fileName, " -as2" + _profile.Key);
                    td.Principal.RunLevel = TaskRunLevel.Highest;
                    td.Settings.Priority  = ProcessPriorityClass.Normal;
                    TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td);
                }
            }
            else {
                string taskName = "OphiussaServerManager\\AutoShutDown2_" + _profile.Key;
                var    task     = TaskService.Instance.GetTask(taskName);
                if (task != null) TaskService.Instance.RootFolder.DeleteTask(taskName);
            }

            #endregion
        }


        private void SaveProfile() {
            if (!MainForm.Settings.Branchs.Contains(cbBranch.Text)) {
                MainForm.Settings.Branchs.Add(cbBranch.Text);
                MainForm.Settings.SaveSettings();
            }

            _profile.Name            = txtProfileName.Text;
            _profile.InstallLocation = txtLocation.Text;

            _profile.ValheimConfiguration.Administration.Branch         = cbBranch.Text;
            _profile.ValheimConfiguration.Administration.ServerName     = txtServerName.Text;
            _profile.ValheimConfiguration.Administration.ServerPassword = txtServerPWD.Text;
            _profile.ValheimConfiguration.Administration.LocalIp        = txtLocalIP.SelectedValue.ToString();
            _profile.ValheimConfiguration.Administration.ServerPort     = txtServerPort.Text;
            _profile.ValheimConfiguration.Administration.PeerPort       = txtPeerPort.Text;
            _profile.ValheimConfiguration.Administration.Public         = chkPublic.Checked;
            _profile.ValheimConfiguration.Administration.Crossplay      = chkCrossplay.Checked;
            _profile.ValheimConfiguration.Administration.InstanceId     = txtInstanceID.Text;
            _profile.ValheimConfiguration.Administration.WordName       = txtWorldName.Text;

            if (tbPresetNormal.Checked) _profile.ValheimConfiguration.Administration.Preset    = Preset.Normal;
            if (tbPresetCasual.Checked) _profile.ValheimConfiguration.Administration.Preset    = Preset.Casual;
            if (tbPresetEasy.Checked) _profile.ValheimConfiguration.Administration.Preset      = Preset.Easy;
            if (tbPresetHard.Checked) _profile.ValheimConfiguration.Administration.Preset      = Preset.Hard;
            if (tbPresetHardcore.Checked) _profile.ValheimConfiguration.Administration.Preset  = Preset.Hardcore;
            if (tbPresetImmersive.Checked) _profile.ValheimConfiguration.Administration.Preset = Preset.Immersive;
            if (tbPresetHammer.Checked) _profile.ValheimConfiguration.Administration.Preset    = Preset.Hammer;


            _profile.ValheimConfiguration.Administration.NoBuildcost  = cLBKeys.GetItemChecked(0);
            _profile.ValheimConfiguration.Administration.PlayerEvents = cLBKeys.GetItemChecked(1);
            _profile.ValheimConfiguration.Administration.PassiveMobs  = cLBKeys.GetItemChecked(2);
            _profile.ValheimConfiguration.Administration.NoMap        = cLBKeys.GetItemChecked(3);


            _profile.ValheimConfiguration.Administration.CpuPriority = (ProcessPriority)cboPriority.SelectedValue;

            _profile.ValheimConfiguration.Administration.CpuAffinity = txtAffinity.Text;


            if (rbCombatNone.Checked) _profile.ValheimConfiguration.Administration.Combat     = Combat.Default;
            if (rbCombatVeryEasy.Checked) _profile.ValheimConfiguration.Administration.Combat = Combat.VeryEasy;
            if (rbCombatEasy.Checked) _profile.ValheimConfiguration.Administration.Combat     = Combat.Easy;
            if (rbCombatHard.Checked) _profile.ValheimConfiguration.Administration.Combat     = Combat.Hard;
            if (rbCombatVeryHard.Checked) _profile.ValheimConfiguration.Administration.Combat = Combat.VeryHard;


            if (rbDeathPenaltyNone.Checked) _profile.ValheimConfiguration.Administration.DeathPenalty     = DeathPenalty.Default;
            if (rbDeathPenaltyCasual.Checked) _profile.ValheimConfiguration.Administration.DeathPenalty   = DeathPenalty.Casual;
            if (rbDeathPenaltyVeryEasy.Checked) _profile.ValheimConfiguration.Administration.DeathPenalty = DeathPenalty.VeryEasy;
            if (rbDeathPenaltyEasy.Checked) _profile.ValheimConfiguration.Administration.DeathPenalty     = DeathPenalty.Easy;
            if (rbDeathPenaltyHard.Checked) _profile.ValheimConfiguration.Administration.DeathPenalty     = DeathPenalty.Hard;
            if (rbDeathPenaltyHardCore.Checked) _profile.ValheimConfiguration.Administration.DeathPenalty = DeathPenalty.HardCore;


            if (rbResourcesNone.Checked) _profile.ValheimConfiguration.Administration.Resources     = Resources.Default;
            if (rbResourcesMuchLess.Checked) _profile.ValheimConfiguration.Administration.Resources = Resources.MuchLess;
            if (rbResourcesLess.Checked) _profile.ValheimConfiguration.Administration.Resources     = Resources.Less;
            if (rbResourcesMore.Checked) _profile.ValheimConfiguration.Administration.Resources     = Resources.More;
            if (rbResourcesMuchMore.Checked) _profile.ValheimConfiguration.Administration.Resources = Resources.MuchMore;
            if (rbResourcesMost.Checked) _profile.ValheimConfiguration.Administration.Resources     = Resources.Most;

            if (rbRaidsDefault.Checked) _profile.ValheimConfiguration.Administration.Raids  = Raids.Default;
            if (rbRaidsNone.Checked) _profile.ValheimConfiguration.Administration.Raids     = Raids.None;
            if (rbRaidsMuchLess.Checked) _profile.ValheimConfiguration.Administration.Raids = Raids.MuchLess;
            if (rbRaidsLess.Checked) _profile.ValheimConfiguration.Administration.Raids     = Raids.Less;
            if (rbRaidsMore.Checked) _profile.ValheimConfiguration.Administration.Raids     = Raids.More;
            if (rbRaidsMuchMore.Checked) _profile.ValheimConfiguration.Administration.Raids = Raids.MuchMore;

            if (rbPortalsNone.Checked) _profile.ValheimConfiguration.Administration.Portals     = Portals.Default;
            if (rbPortalsCasual.Checked) _profile.ValheimConfiguration.Administration.Portals   = Portals.Casual;
            if (rbPortalsHard.Checked) _profile.ValheimConfiguration.Administration.Portals     = Portals.Hard;
            if (rbPortalsVeryHard.Checked) _profile.ValheimConfiguration.Administration.Portals = Portals.VeryHard;

            _profile.ValheimConfiguration.Administration.AutoSavePeriod = tbAutoSavePeriod.Value;
            _profile.ValheimConfiguration.Administration.BackupShort    = tbFirstBackup.Value;
            _profile.ValheimConfiguration.Administration.BackupLong     = tbSubBackups.Value;


            _profile.ValheimConfiguration.Administration.SaveLocation    = txtSaveLocation.Text;
            _profile.ValheimConfiguration.Administration.LogFileLocation = txtLogLocation.Text;
            _profile.ValheimConfiguration.Administration.TotalBackups    = txtBackupToKeep.Text.ToInt();

            _profile.AutoManageSettings.AutoStartServer = chkAutoStart.Checked;
            _profile.AutoManageSettings.AutoStartOn     = rbOnBoot.Checked ? AutoStart.OnBoot : AutoStart.OnLogin;

            _profile.AutoManageSettings.ShutdownServer1              = chkShutdown1.Checked;
            _profile.AutoManageSettings.ShutdownServer1Hour          = txtShutdow1.Text;
            _profile.AutoManageSettings.ShutdownServer1Sunday        = chkSun1.Checked;
            _profile.AutoManageSettings.ShutdownServer1Monday        = chkMon1.Checked;
            _profile.AutoManageSettings.ShutdownServer1Tuesday       = chkTue1.Checked;
            _profile.AutoManageSettings.ShutdownServer1Wednesday     = chkWed1.Checked;
            _profile.AutoManageSettings.ShutdownServer1Thu           = chkThu1.Checked;
            _profile.AutoManageSettings.ShutdownServer1Friday        = chkFri1.Checked;
            _profile.AutoManageSettings.ShutdownServer1Saturday      = chkSat1.Checked;
            _profile.AutoManageSettings.ShutdownServer1PerformUpdate = chkUpdate1.Checked;
            _profile.AutoManageSettings.ShutdownServer1Restart       = chkRestart1.Checked;

            _profile.AutoManageSettings.ShutdownServer2              = chkShutdown2.Checked;
            _profile.AutoManageSettings.ShutdownServer2Hour          = txtShutdow2.Text;
            _profile.AutoManageSettings.ShutdownServer2Sunday        = chkSun2.Checked;
            _profile.AutoManageSettings.ShutdownServer2Monday        = chkMon2.Checked;
            _profile.AutoManageSettings.ShutdownServer2Tuesday       = chkTue2.Checked;
            _profile.AutoManageSettings.ShutdownServer2Wednesday     = chkWed2.Checked;
            _profile.AutoManageSettings.ShutdownServer2Thu           = chkThu2.Checked;
            _profile.AutoManageSettings.ShutdownServer2Friday        = chkFri2.Checked;
            _profile.AutoManageSettings.ShutdownServer2Saturday      = chkSat2.Checked;
            _profile.AutoManageSettings.ShutdownServer2PerformUpdate = chkUpdate2.Checked;
            _profile.AutoManageSettings.ShutdownServer2Restart       = chkRestart2.Checked;

            _profile.AutoManageSettings.IncludeInAutoBackup = chkIncludeAutoBackup.Checked;
            _profile.AutoManageSettings.IncludeInAutoUpdate = chkAutoUpdate.Checked;
            _profile.AutoManageSettings.AutoStartServer     = chkAutoStart.Checked;

            _profile.ArkConfiguration = null;
            _profile.SaveProfile(MainForm.Settings);

            LoadSettings(_profile, _tab);
        }

        private void btUpdate_Click(object sender, EventArgs e) {
            SaveProfile();

            var frm = new FrmUpdateProgress(MainForm.Settings, _profile);
            frm.ShowDialog();

            LoadSettings(_profile, _tab);
        }

        private void tbAutoSavePeriod_Scroll(object sender, EventArgs e) {
            txtAutoSavePeriod.Text = tbAutoSavePeriod.Value.ToString();
        }

        private void tbFirstBackup_Scroll(object sender, EventArgs e) {
            txtFirstBackup.Text = tbFirstBackup.Value.ToString();
        }

        private void tbSubBackups_Scroll(object sender, EventArgs e) {
            txtSubBackups.Text = tbSubBackups.Value.ToString();
        }

        private void chkAutoStart_CheckedChanged(object sender, EventArgs e) {
            rbOnBoot.Enabled  = chkAutoStart.Checked;
            rbOnLogin.Enabled = chkAutoStart.Checked;
        }

        private void chkShutdown1_CheckedChanged(object sender, EventArgs e) {
            txtShutdow1.Enabled = chkShutdown1.Checked;
            chkSun1.Enabled     = chkShutdown1.Checked;
            chkMon1.Enabled     = chkShutdown1.Checked;
            chkTue1.Enabled     = chkShutdown1.Checked;
            chkWed1.Enabled     = chkShutdown1.Checked;
            chkThu1.Enabled     = chkShutdown1.Checked;
            chkFri1.Enabled     = chkShutdown1.Checked;
            chkSat1.Enabled     = chkShutdown1.Checked;
            chkUpdate1.Enabled  = chkShutdown1.Checked;
            chkRestart1.Enabled = chkShutdown1.Checked;
        }

        private void chkShutdown2_CheckedChanged(object sender, EventArgs e) {
            txtShutdow2.Enabled = chkShutdown2.Checked;
            chkSun2.Enabled     = chkShutdown2.Checked;
            chkMon2.Enabled     = chkShutdown2.Checked;
            chkTue2.Enabled     = chkShutdown2.Checked;
            chkWed2.Enabled     = chkShutdown2.Checked;
            chkThu2.Enabled     = chkShutdown2.Checked;
            chkFri2.Enabled     = chkShutdown2.Checked;
            chkSat2.Enabled     = chkShutdown2.Checked;
            chkUpdate2.Enabled  = chkShutdown2.Checked;
            chkRestart2.Enabled = chkShutdown2.Checked;
        }

        private void btProcessorAffinity_Click(object sender, EventArgs e) {
            var frm = new FrmProcessors(_profile.ValheimConfiguration.Administration.CpuAffinity == "All", _profile.ValheimConfiguration.Administration.CpuAffinityList);
            frm.UpdateCpuAffinity = (all, lst) => {
                                        _profile.ValheimConfiguration.Administration.CpuAffinity     = all ? "All" : string.Join(",", lst.FindAll(x => x.Selected).Select(x => x.ProcessorNumber.ToString()));
                                        _profile.ValheimConfiguration.Administration.CpuAffinityList = lst;
                                        txtAffinity.Text                                             = _profile.ValheimConfiguration.Administration.CpuAffinity;
                                    };
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e) {
            txtCommand.Text = _profile.ValheimConfiguration.GetCommandLinesArguments(MainForm.Settings, _profile, MainForm.LocaIp);
        }

        private async void btStart_Click(object sender, EventArgs e) {
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

        private void txtServerPort_TextChanged(object sender, EventArgs e) {
            int port;
            if (int.TryParse(txtServerPort.Text, out port)) txtPeerPort.Text = (port + 1).ToString();
        }

        private void button1_Click(object sender, EventArgs e) {
            fbd.ShowDialog();

            txtSaveLocation.Text = fbd.SelectedPath;
        }

        private void button3_Click(object sender, EventArgs e) {
            fbd.ShowDialog();

            txtLogLocation.Text = fbd.SelectedPath;
        }

        public void IsRunningProcess() {
            while (true) {
                Process process = null;

                if (_processId == -1)
                    process = _profile.GetExeProcess();
                else
                    try {
                        process = Process.GetProcessById(_processId);
                    }
                    catch (Exception) {
                    }

                if (process != null) {
                    _processId = process.Id;
                    _isRunning = true;
                }
                else {
                    _processId = -1;
                    _isRunning = false;
                }

                if (!UsefullTools.IsFormRunning("MainForm")) break;
                Thread.Sleep(timerGetProcess.Interval);
            }
        }

        private void timerGetProcess_Tick(object sender, EventArgs e) {
            try {
                timerGetProcess.Enabled = false;

                if (_isRunning) btStart.Text = "Stop";
                else btStart.Text            = "Start";
                btUpdate.Enabled = !_isRunning;

                UsefullTools.MainForm.SetTabHeader(_tab, _profile, _isRunning);
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
            }
            finally {
                timerGetProcess.Enabled = true;
            }
        }

        private void FrmValheim_Load(object sender, EventArgs e) {
            var thread = new Thread(IsRunningProcess);
            thread.Start();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e) {
        }
    }
}