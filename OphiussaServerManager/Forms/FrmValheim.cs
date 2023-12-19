using Microsoft.Win32.TaskScheduler;
using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models.SupportedServers;
using OphiussaServerManager.Tools;
using OphiussaServerManager.Tools.Update;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace OphiussaServerManager.Forms
{
    public partial class FrmValheim : Form
    {
        Profile profile;
        TabPage tab;
        bool isInstalled = false;
        bool isRunning = false;
        int ProcessID = -1;

        public FrmValheim()
        {
            InitializeComponent();
        }

        private void LoadDefaultFieldValues()
        {

            try
            {

                List<IpList> ret = NetworkTools.GetAllHostIp();

                txtLocalIP.DataSource = ret;
                txtLocalIP.ValueMember = "IP";
                txtLocalIP.DisplayMember = "Description";

                MainForm.Settings.Branchs.Distinct().ToList().ForEach(branch => { cbBranch.Items.Add(branch); });

                cboPriority.DataSource = Enum.GetValues(typeof(ProcessPriorityClass));
            }
            catch (Exception e)
            {
                OphiussaLogger.logger.Error(e);
                MessageBox.Show("LoadDefaultFieldValues:" + e.Message);
            }
        }
        public void LoadSettings(Profile profile, TabPage tab)
        {
            this.profile = profile;
            this.tab = tab;
            LoadDefaultFieldValues();

            txtProfileID.Text = profile.Key;
            txtProfileName.Text = profile.Name;
            tab.Text = txtProfileName.Text + "          ";
            txtServerType.Text = profile.Type.ServerTypeDescription;
            txtLocation.Text = profile.InstallLocation;


            cbBranch.Text = profile.ValheimConfiguration.Administration.Branch.ToString();
            txtServerName.Text = profile.ValheimConfiguration.Administration.ServerName;
            txtServerPWD.Text = profile.ValheimConfiguration.Administration.ServerPassword;
            txtLocalIP.SelectedValue = profile.ValheimConfiguration.Administration.LocalIP;
            txtServerPort.Text = profile.ValheimConfiguration.Administration.ServerPort;
            txtPeerPort.Text = profile.ValheimConfiguration.Administration.PeerPort;
            chkPublic.Checked = profile.ValheimConfiguration.Administration.Public;
            chkCrossplay.Checked = profile.ValheimConfiguration.Administration.Crossplay;
            txtInstanceID.Text = profile.ValheimConfiguration.Administration.InstanceID;
            txtWorldName.Text = profile.ValheimConfiguration.Administration.WordName;

            tbPresetNormal.Checked = profile.ValheimConfiguration.Administration.Preset == Common.Models.ValheimProfile.Preset.Normal;
            tbPresetCasual.Checked = profile.ValheimConfiguration.Administration.Preset == Common.Models.ValheimProfile.Preset.Casual;
            tbPresetEasy.Checked = profile.ValheimConfiguration.Administration.Preset == Common.Models.ValheimProfile.Preset.Easy;
            tbPresetHard.Checked = profile.ValheimConfiguration.Administration.Preset == Common.Models.ValheimProfile.Preset.Hard;
            tbPresetHardcore.Checked = profile.ValheimConfiguration.Administration.Preset == Common.Models.ValheimProfile.Preset.Hardcore;
            tbPresetImmersive.Checked = profile.ValheimConfiguration.Administration.Preset == Common.Models.ValheimProfile.Preset.Immersive;
            tbPresetHammer.Checked = profile.ValheimConfiguration.Administration.Preset == Common.Models.ValheimProfile.Preset.Hammer;

            cLBKeys.SetItemChecked(0, profile.ValheimConfiguration.Administration.NoBuildcost);
            cLBKeys.SetItemChecked(1, profile.ValheimConfiguration.Administration.PlayerEvents);
            cLBKeys.SetItemChecked(2, profile.ValheimConfiguration.Administration.PassiveMobs);
            cLBKeys.SetItemChecked(3, profile.ValheimConfiguration.Administration.NoMap);

            cboPriority.SelectedItem = profile.ValheimConfiguration.Administration.CPUPriority;
            txtAffinity.Text = profile.ValheimConfiguration.Administration.CPUAffinity;

            rbCombatNone.Checked = profile.ValheimConfiguration.Administration.Combat == Common.Models.ValheimProfile.Combat.Default;
            rbCombatVeryEasy.Checked = profile.ValheimConfiguration.Administration.Combat == Common.Models.ValheimProfile.Combat.VeryEasy;
            rbCombatEasy.Checked = profile.ValheimConfiguration.Administration.Combat == Common.Models.ValheimProfile.Combat.Easy;
            rbCombatHard.Checked = profile.ValheimConfiguration.Administration.Combat == Common.Models.ValheimProfile.Combat.Hard;
            rbCombatVeryHard.Checked = profile.ValheimConfiguration.Administration.Combat == Common.Models.ValheimProfile.Combat.VeryHard;

            rbDeathPenaltyNone.Checked = profile.ValheimConfiguration.Administration.DeathPenalty == Common.Models.ValheimProfile.DeathPenalty.Default;
            rbDeathPenaltyCasual.Checked = profile.ValheimConfiguration.Administration.DeathPenalty == Common.Models.ValheimProfile.DeathPenalty.Casual;
            rbDeathPenaltyVeryEasy.Checked = profile.ValheimConfiguration.Administration.DeathPenalty == Common.Models.ValheimProfile.DeathPenalty.VeryEasy;
            rbDeathPenaltyEasy.Checked = profile.ValheimConfiguration.Administration.DeathPenalty == Common.Models.ValheimProfile.DeathPenalty.Easy;
            rbDeathPenaltyHard.Checked = profile.ValheimConfiguration.Administration.DeathPenalty == Common.Models.ValheimProfile.DeathPenalty.Hard;
            rbDeathPenaltyHardCore.Checked = profile.ValheimConfiguration.Administration.DeathPenalty == Common.Models.ValheimProfile.DeathPenalty.HardCore;

            rbResourcesNone.Checked = profile.ValheimConfiguration.Administration.Resources == Common.Models.ValheimProfile.Resources.Default;
            rbResourcesMuchLess.Checked = profile.ValheimConfiguration.Administration.Resources == Common.Models.ValheimProfile.Resources.MuchLess;
            rbResourcesLess.Checked = profile.ValheimConfiguration.Administration.Resources == Common.Models.ValheimProfile.Resources.Less;
            rbResourcesMore.Checked = profile.ValheimConfiguration.Administration.Resources == Common.Models.ValheimProfile.Resources.More;
            rbResourcesMuchMore.Checked = profile.ValheimConfiguration.Administration.Resources == Common.Models.ValheimProfile.Resources.MuchMore;
            rbResourcesMost.Checked = profile.ValheimConfiguration.Administration.Resources == Common.Models.ValheimProfile.Resources.Most;
             
            rbRaidsDefault.Checked = profile.ValheimConfiguration.Administration.Raids == Common.Models.ValheimProfile.Raids.Default;
            rbRaidsNone.Checked = profile.ValheimConfiguration.Administration.Raids == Common.Models.ValheimProfile.Raids.None;
            rbRaidsMuchLess.Checked = profile.ValheimConfiguration.Administration.Raids == Common.Models.ValheimProfile.Raids.MuchLess;
            rbRaidsLess.Checked = profile.ValheimConfiguration.Administration.Raids == Common.Models.ValheimProfile.Raids.Less;
            rbRaidsMore.Checked = profile.ValheimConfiguration.Administration.Raids == Common.Models.ValheimProfile.Raids.More;
            rbRaidsMuchMore.Checked = profile.ValheimConfiguration.Administration.Raids == Common.Models.ValheimProfile.Raids.MuchMore;
             
            rbPortalsNone.Checked = profile.ValheimConfiguration.Administration.Portals == Common.Models.ValheimProfile.Portals.Default;
            rbPortalsCasual.Checked = profile.ValheimConfiguration.Administration.Portals == Common.Models.ValheimProfile.Portals.Casual;
            rbPortalsHard.Checked = profile.ValheimConfiguration.Administration.Portals == Common.Models.ValheimProfile.Portals.Hard;
            rbPortalsVeryHard.Checked = profile.ValheimConfiguration.Administration.Portals == Common.Models.ValheimProfile.Portals.VeryHard;

            tbAutoSavePeriod.Value = profile.ValheimConfiguration.Administration.AutoSavePeriod;
            tbFirstBackup.Value = profile.ValheimConfiguration.Administration.BackupShort;
            tbSubBackups.Value = profile.ValheimConfiguration.Administration.BackupLong;

            txtAutoSavePeriod.Text = tbAutoSavePeriod.Value.ToString();
            txtFirstBackup.Text = tbFirstBackup.Value.ToString();
            txtSubBackups.Text = tbSubBackups.Value.ToString();

            txtSaveLocation.Text = profile.ValheimConfiguration.Administration.SaveLocation;
            txtLogLocation.Text = profile.ValheimConfiguration.Administration.LogFileLocation;
            txtBackupToKeep.Text = profile.ValheimConfiguration.Administration.TotalBackups.ToString();

            chkAutoStart.Checked = profile.AutoManageSettings.AutoStartServer;
            rbOnBoot.Checked = profile.AutoManageSettings.AutoStartOn == Common.Models.AutoStart.onBoot;
            rbOnLogin.Checked = profile.AutoManageSettings.AutoStartOn == Common.Models.AutoStart.onLogin;

            chkShutdown1.Checked = profile.AutoManageSettings.ShutdownServer1;
            txtShutdow1.Text = profile.AutoManageSettings.ShutdownServer1Hour;
            chkSun1.Checked = profile.AutoManageSettings.ShutdownServer1Sunday;
            chkMon1.Checked = profile.AutoManageSettings.ShutdownServer1Monday;
            chkTue1.Checked = profile.AutoManageSettings.ShutdownServer1Tuesday;
            chkWed1.Checked = profile.AutoManageSettings.ShutdownServer1Wednesday;
            chkThu1.Checked = profile.AutoManageSettings.ShutdownServer1Thu;
            chkFri1.Checked = profile.AutoManageSettings.ShutdownServer1Friday;
            chkSat1.Checked = profile.AutoManageSettings.ShutdownServer1Saturday;
            chkUpdate1.Checked = profile.AutoManageSettings.ShutdownServer1PerformUpdate;
            chkRestart1.Checked = profile.AutoManageSettings.ShutdownServer1Restart;

            chkShutdown2.Checked = profile.AutoManageSettings.ShutdownServer2;
            txtShutdow2.Text = profile.AutoManageSettings.ShutdownServer2Hour;
            chkSun2.Checked = profile.AutoManageSettings.ShutdownServer2Sunday;
            chkMon2.Checked = profile.AutoManageSettings.ShutdownServer2Monday;
            chkTue2.Checked = profile.AutoManageSettings.ShutdownServer2Tuesday;
            chkWed2.Checked = profile.AutoManageSettings.ShutdownServer2Wednesday;
            chkThu2.Checked = profile.AutoManageSettings.ShutdownServer2Thu;
            chkFri2.Checked = profile.AutoManageSettings.ShutdownServer2Friday;
            chkSat2.Checked = profile.AutoManageSettings.ShutdownServer2Saturday;
            chkUpdate2.Checked = profile.AutoManageSettings.ShutdownServer2PerformUpdate;
            chkRestart2.Checked = profile.AutoManageSettings.ShutdownServer2Restart;


            chkIncludeAutoBackup.Checked = profile.AutoManageSettings.IncludeInAutoBackup;
            chkAutoUpdate.Checked = profile.AutoManageSettings.IncludeInAutoUpdate;
            chkRestartIfShutdown.Checked = profile.AutoManageSettings.AutoStartServer;


            txtVersion.Text = profile.GetVersion();
            txtBuild.Text = profile.GetBuild();

            txtCommand.Text = profile.ValheimConfiguration.GetCommandLinesArguments(MainForm.Settings, profile, MainForm.LocaIP);
        }

        private void txtProfileName_Validated(object sender, EventArgs e)
        {
            tab.Text = txtProfileName.Text + "          ";
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveProfile();
                CreateWindowsTasks();

                MessageBox.Show("Profile Saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void CreateWindowsTasks()
        {
            #region AutoStartServer
            if (profile.AutoManageSettings.AutoStartServer)
            {
                string fileName = MainForm.Settings.DataFolder + $"StartServer\\Run_{profile.Key.Replace("-", "")}.bat";
                string taskName = "OphiussaServerManager\\AutoStart_" + profile.Key;
                Microsoft.Win32.TaskScheduler.Task task = TaskService.Instance.GetTask(taskName);
                if (task != null)
                {
                    task.Definition.Triggers.Clear();
                    if (profile.AutoManageSettings.AutoStartOn == Common.Models.AutoStart.onBoot)
                    {
                        BootTrigger bt1 = new BootTrigger { Delay = TimeSpan.FromMinutes(1) };
                        task.Definition.Triggers.Add(bt1);
                    }
                    else
                    {
                        LogonTrigger lt1 = new LogonTrigger { Delay = TimeSpan.FromMinutes(1) };
                        task.Definition.Triggers.Add(lt1);
                    }

                    task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                    task.Definition.Settings.Priority = ProcessPriorityClass.Normal;
                    task.RegisterChanges();
                }
                else
                {
                    TaskDefinition td = TaskService.Instance.NewTask();
                    td.RegistrationInfo.Description = "Server Auto-Start - " + profile.Name;
                    td.Principal.LogonType = TaskLogonType.InteractiveToken;
                    if (profile.AutoManageSettings.AutoStartOn == Common.Models.AutoStart.onBoot)
                    {
                        BootTrigger bt1 = new BootTrigger { Delay = TimeSpan.FromMinutes(1) };
                        td.Triggers.Add(bt1);
                    }
                    else
                    {
                        LogonTrigger lt1 = new LogonTrigger { Delay = TimeSpan.FromMinutes(1) };
                        td.Triggers.Add(lt1);
                    }
                    td.Actions.Add(fileName);
                    td.Principal.RunLevel = TaskRunLevel.Highest;
                    td.Settings.Priority = ProcessPriorityClass.Normal;
                    TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td);
                }
            }
            else
            {
                string taskName = "OphiussaServerManager\\AutoStart_" + profile.Key;
                Microsoft.Win32.TaskScheduler.Task task = TaskService.Instance.GetTask(taskName);
                if (task != null)
                {
                    TaskService.Instance.RootFolder.DeleteTask(taskName);
                }
            }
            #endregion
            #region Shutdown 1
            if (profile.AutoManageSettings.ShutdownServer1)
            {
                string fileName = Assembly.GetExecutingAssembly().Location;
                string taskName = "OphiussaServerManager\\AutoShutDown1_" + profile.Key;
                Microsoft.Win32.TaskScheduler.Task task = TaskService.Instance.GetTask(taskName);

                if (task != null)
                {
                    task.Definition.Triggers.Clear();

                    DaysOfTheWeek daysofweek = 0;

                    if (profile.AutoManageSettings.ShutdownServer1Monday) daysofweek += 2;
                    if (profile.AutoManageSettings.ShutdownServer1Tuesday) daysofweek += 4;
                    if (profile.AutoManageSettings.ShutdownServer1Wednesday) daysofweek += 8;
                    if (profile.AutoManageSettings.ShutdownServer1Thu) daysofweek += 16;
                    if (profile.AutoManageSettings.ShutdownServer1Friday) daysofweek += 32;
                    if (profile.AutoManageSettings.ShutdownServer1Saturday) daysofweek += 64;
                    if (profile.AutoManageSettings.ShutdownServer1Sunday) daysofweek += 1;
                    WeeklyTrigger tt = new WeeklyTrigger();

                    int hour = Int16.Parse(profile.AutoManageSettings.ShutdownServer1Hour.Split(':')[0]);
                    int minute = Int16.Parse(profile.AutoManageSettings.ShutdownServer1Hour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek = daysofweek;
                    task.Definition.Triggers.Add(tt);
                    task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                    task.Definition.Settings.Priority = ProcessPriorityClass.Normal;
                    task.RegisterChanges();
                }
                else
                {
                    TaskDefinition td = TaskService.Instance.NewTask();
                    td.RegistrationInfo.Description = "Server Auto-ShutDown 1 - " + profile.Name;
                    td.Principal.LogonType = TaskLogonType.InteractiveToken;
                    DaysOfTheWeek daysofweek = 0;

                    if (profile.AutoManageSettings.ShutdownServer1Monday) daysofweek += 2;
                    if (profile.AutoManageSettings.ShutdownServer1Tuesday) daysofweek += 4;
                    if (profile.AutoManageSettings.ShutdownServer1Wednesday) daysofweek += 8;
                    if (profile.AutoManageSettings.ShutdownServer1Thu) daysofweek += 16;
                    if (profile.AutoManageSettings.ShutdownServer1Friday) daysofweek += 32;
                    if (profile.AutoManageSettings.ShutdownServer1Saturday) daysofweek += 64;
                    if (profile.AutoManageSettings.ShutdownServer1Sunday) daysofweek += 1;
                    WeeklyTrigger tt = new WeeklyTrigger();

                    int hour = Int16.Parse(profile.AutoManageSettings.ShutdownServer1Hour.Split(':')[0]);
                    int minute = Int16.Parse(profile.AutoManageSettings.ShutdownServer1Hour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek = daysofweek;
                    td.Triggers.Add(tt);
                    td.Actions.Add(fileName, " -as1" + profile.Key);
                    td.Principal.RunLevel = TaskRunLevel.Highest;
                    td.Settings.Priority = ProcessPriorityClass.Normal;

                    TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td);
                }
            }
            else
            {
                string taskName = "OphiussaServerManager\\AutoShutDown1_" + profile.Key;
                Microsoft.Win32.TaskScheduler.Task task = TaskService.Instance.GetTask(taskName);
                if (task != null)
                {
                    TaskService.Instance.RootFolder.DeleteTask(taskName);
                }
            }
            #endregion

            #region Shutdown 2
            if (profile.AutoManageSettings.ShutdownServer2)
            {
                string fileName = Assembly.GetExecutingAssembly().Location;
                string taskName = "OphiussaServerManager\\AutoShutDown2_" + profile.Key;
                Microsoft.Win32.TaskScheduler.Task task = TaskService.Instance.GetTask(taskName);
                if (task != null)
                {
                    task.Definition.Triggers.Clear();

                    DaysOfTheWeek daysofweek = 0;

                    if (profile.AutoManageSettings.ShutdownServer2Monday) daysofweek += 2;
                    if (profile.AutoManageSettings.ShutdownServer2Tuesday) daysofweek += 4;
                    if (profile.AutoManageSettings.ShutdownServer2Wednesday) daysofweek += 8;
                    if (profile.AutoManageSettings.ShutdownServer2Thu) daysofweek += 16;
                    if (profile.AutoManageSettings.ShutdownServer2Friday) daysofweek += 32;
                    if (profile.AutoManageSettings.ShutdownServer2Saturday) daysofweek += 64;
                    if (profile.AutoManageSettings.ShutdownServer2Sunday) daysofweek += 1;
                    WeeklyTrigger tt = new WeeklyTrigger();

                    int hour = Int16.Parse(profile.AutoManageSettings.ShutdownServer2Hour.Split(':')[0]);
                    int minute = Int16.Parse(profile.AutoManageSettings.ShutdownServer2Hour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek = daysofweek;
                    task.Definition.Triggers.Add(tt);
                    task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                    task.Definition.Settings.Priority = ProcessPriorityClass.Normal;
                    task.RegisterChanges();
                }
                else
                {
                    TaskDefinition td = TaskService.Instance.NewTask();
                    td.RegistrationInfo.Description = "Server Auto-ShutDown 2 - " + profile.Name;
                    td.Principal.LogonType = TaskLogonType.InteractiveToken;

                    DaysOfTheWeek daysofweek = 0;

                    if (profile.AutoManageSettings.ShutdownServer2Monday) daysofweek += 2;
                    if (profile.AutoManageSettings.ShutdownServer2Tuesday) daysofweek += 4;
                    if (profile.AutoManageSettings.ShutdownServer2Wednesday) daysofweek += 8;
                    if (profile.AutoManageSettings.ShutdownServer2Thu) daysofweek += 16;
                    if (profile.AutoManageSettings.ShutdownServer2Friday) daysofweek += 32;
                    if (profile.AutoManageSettings.ShutdownServer2Saturday) daysofweek += 64;
                    if (profile.AutoManageSettings.ShutdownServer2Sunday) daysofweek += 1;
                    WeeklyTrigger tt = new WeeklyTrigger();

                    int hour = Int16.Parse(profile.AutoManageSettings.ShutdownServer2Hour.Split(':')[0]);
                    int minute = Int16.Parse(profile.AutoManageSettings.ShutdownServer2Hour.Split(':')[1]);
                    tt.StartBoundary = DateTime.Today + TimeSpan.FromHours(hour) + TimeSpan.FromMinutes(minute);
                    tt.DaysOfWeek = daysofweek;
                    td.Triggers.Add(tt);
                    td.Actions.Add(fileName, " -as2" + profile.Key);
                    td.Principal.RunLevel = TaskRunLevel.Highest;
                    td.Settings.Priority = ProcessPriorityClass.Normal;
                    TaskService.Instance.RootFolder.RegisterTaskDefinition(taskName, td);
                }
            }
            else
            {
                string taskName = "OphiussaServerManager\\AutoShutDown2_" + profile.Key;
                Microsoft.Win32.TaskScheduler.Task task = TaskService.Instance.GetTask(taskName);
                if (task != null)
                {
                    TaskService.Instance.RootFolder.DeleteTask(taskName);
                }
            }
            #endregion
        }


        private void SaveProfile()
        {
            if (!MainForm.Settings.Branchs.Contains(cbBranch.Text.ToString())) { MainForm.Settings.Branchs.Add(cbBranch.Text); MainForm.Settings.SaveSettings(); }

            profile.Name = txtProfileName.Text;
            profile.InstallLocation = txtLocation.Text;

            profile.ValheimConfiguration.Administration.Branch = cbBranch.Text;
            profile.ValheimConfiguration.Administration.ServerName = txtServerName.Text;
            profile.ValheimConfiguration.Administration.ServerPassword = txtServerPWD.Text;
            profile.ValheimConfiguration.Administration.LocalIP = txtLocalIP.SelectedValue.ToString();
            profile.ValheimConfiguration.Administration.ServerPort = txtServerPort.Text;
            profile.ValheimConfiguration.Administration.PeerPort = txtPeerPort.Text;
            profile.ValheimConfiguration.Administration.Public = chkPublic.Checked;
            profile.ValheimConfiguration.Administration.Crossplay = chkCrossplay.Checked;
            profile.ValheimConfiguration.Administration.InstanceID = txtInstanceID.Text;
            profile.ValheimConfiguration.Administration.WordName = txtWorldName.Text;

            if (tbPresetNormal.Checked) profile.ValheimConfiguration.Administration.Preset = Common.Models.ValheimProfile.Preset.Normal;
            if (tbPresetCasual.Checked) profile.ValheimConfiguration.Administration.Preset = Common.Models.ValheimProfile.Preset.Casual;
            if (tbPresetEasy.Checked) profile.ValheimConfiguration.Administration.Preset = Common.Models.ValheimProfile.Preset.Easy;
            if (tbPresetHard.Checked) profile.ValheimConfiguration.Administration.Preset = Common.Models.ValheimProfile.Preset.Hard;
            if (tbPresetHardcore.Checked) profile.ValheimConfiguration.Administration.Preset = Common.Models.ValheimProfile.Preset.Hardcore;
            if (tbPresetImmersive.Checked) profile.ValheimConfiguration.Administration.Preset = Common.Models.ValheimProfile.Preset.Immersive;
            if (tbPresetHammer.Checked) profile.ValheimConfiguration.Administration.Preset = Common.Models.ValheimProfile.Preset.Hammer;



            profile.ValheimConfiguration.Administration.NoBuildcost = cLBKeys.GetItemChecked(0);
            profile.ValheimConfiguration.Administration.PlayerEvents = cLBKeys.GetItemChecked(1);
            profile.ValheimConfiguration.Administration.PassiveMobs = cLBKeys.GetItemChecked(2);
            profile.ValheimConfiguration.Administration.NoMap = cLBKeys.GetItemChecked(3);


            profile.ValheimConfiguration.Administration.CPUPriority = (ProcessPriorityClass)cboPriority.SelectedValue;

            profile.ValheimConfiguration.Administration.CPUAffinity = txtAffinity.Text;


            if (rbCombatNone.Checked) profile.ValheimConfiguration.Administration.Combat = Common.Models.ValheimProfile.Combat.Default;
            if (rbCombatVeryEasy.Checked) profile.ValheimConfiguration.Administration.Combat = Common.Models.ValheimProfile.Combat.VeryEasy;
            if (rbCombatEasy.Checked) profile.ValheimConfiguration.Administration.Combat = Common.Models.ValheimProfile.Combat.Easy;
            if (rbCombatHard.Checked) profile.ValheimConfiguration.Administration.Combat = Common.Models.ValheimProfile.Combat.Hard;
            if (rbCombatVeryHard.Checked) profile.ValheimConfiguration.Administration.Combat = Common.Models.ValheimProfile.Combat.VeryHard;


            if (rbDeathPenaltyNone.Checked) profile.ValheimConfiguration.Administration.DeathPenalty = Common.Models.ValheimProfile.DeathPenalty.Default;
            if (rbDeathPenaltyCasual.Checked) profile.ValheimConfiguration.Administration.DeathPenalty = Common.Models.ValheimProfile.DeathPenalty.Casual;
            if (rbDeathPenaltyVeryEasy.Checked) profile.ValheimConfiguration.Administration.DeathPenalty = Common.Models.ValheimProfile.DeathPenalty.VeryEasy;
            if (rbDeathPenaltyEasy.Checked) profile.ValheimConfiguration.Administration.DeathPenalty = Common.Models.ValheimProfile.DeathPenalty.Easy;
            if (rbDeathPenaltyHard.Checked) profile.ValheimConfiguration.Administration.DeathPenalty = Common.Models.ValheimProfile.DeathPenalty.Hard;
            if (rbDeathPenaltyHardCore.Checked) profile.ValheimConfiguration.Administration.DeathPenalty = Common.Models.ValheimProfile.DeathPenalty.HardCore;


            if (rbResourcesNone.Checked) profile.ValheimConfiguration.Administration.Resources = Common.Models.ValheimProfile.Resources.Default;
            if (rbResourcesMuchLess.Checked) profile.ValheimConfiguration.Administration.Resources = Common.Models.ValheimProfile.Resources.MuchLess;
            if (rbResourcesLess.Checked) profile.ValheimConfiguration.Administration.Resources = Common.Models.ValheimProfile.Resources.Less;
            if (rbResourcesMore.Checked) profile.ValheimConfiguration.Administration.Resources = Common.Models.ValheimProfile.Resources.More;
            if (rbResourcesMuchMore.Checked) profile.ValheimConfiguration.Administration.Resources = Common.Models.ValheimProfile.Resources.MuchMore;
            if (rbResourcesMost.Checked) profile.ValheimConfiguration.Administration.Resources = Common.Models.ValheimProfile.Resources.Most;

            if (rbRaidsDefault.Checked) profile.ValheimConfiguration.Administration.Raids = Common.Models.ValheimProfile.Raids.Default;
            if (rbRaidsNone.Checked) profile.ValheimConfiguration.Administration.Raids = Common.Models.ValheimProfile.Raids.None;
            if (rbRaidsMuchLess.Checked) profile.ValheimConfiguration.Administration.Raids = Common.Models.ValheimProfile.Raids.MuchLess;
            if (rbRaidsLess.Checked) profile.ValheimConfiguration.Administration.Raids = Common.Models.ValheimProfile.Raids.Less;
            if (rbRaidsMore.Checked) profile.ValheimConfiguration.Administration.Raids = Common.Models.ValheimProfile.Raids.More;
            if (rbRaidsMuchMore.Checked) profile.ValheimConfiguration.Administration.Raids = Common.Models.ValheimProfile.Raids.MuchMore;

            if (rbPortalsNone.Checked) profile.ValheimConfiguration.Administration.Portals = Common.Models.ValheimProfile.Portals.Default;
            if (rbPortalsCasual.Checked) profile.ValheimConfiguration.Administration.Portals = Common.Models.ValheimProfile.Portals.Casual;
            if (rbPortalsHard.Checked) profile.ValheimConfiguration.Administration.Portals = Common.Models.ValheimProfile.Portals.Hard;
            if (rbPortalsVeryHard.Checked) profile.ValheimConfiguration.Administration.Portals = Common.Models.ValheimProfile.Portals.VeryHard;

            profile.ValheimConfiguration.Administration.AutoSavePeriod = tbAutoSavePeriod.Value;
            profile.ValheimConfiguration.Administration.BackupShort = tbFirstBackup.Value;
            profile.ValheimConfiguration.Administration.BackupLong = tbSubBackups.Value;


            profile.ValheimConfiguration.Administration.SaveLocation = txtSaveLocation.Text;
            profile.ValheimConfiguration.Administration.LogFileLocation = txtLogLocation.Text;
            profile.ValheimConfiguration.Administration.TotalBackups = txtBackupToKeep.Text.ToInt();

            profile.AutoManageSettings.AutoStartServer = chkAutoStart.Checked;
            profile.AutoManageSettings.AutoStartOn = rbOnBoot.Checked ? Common.Models.AutoStart.onBoot : Common.Models.AutoStart.onLogin;

            profile.AutoManageSettings.ShutdownServer1 = chkShutdown1.Checked;
            profile.AutoManageSettings.ShutdownServer1Hour = txtShutdow1.Text;
            profile.AutoManageSettings.ShutdownServer1Sunday = chkSun1.Checked;
            profile.AutoManageSettings.ShutdownServer1Monday = chkMon1.Checked;
            profile.AutoManageSettings.ShutdownServer1Tuesday = chkTue1.Checked;
            profile.AutoManageSettings.ShutdownServer1Wednesday = chkWed1.Checked;
            profile.AutoManageSettings.ShutdownServer1Thu = chkThu1.Checked;
            profile.AutoManageSettings.ShutdownServer1Friday = chkFri1.Checked;
            profile.AutoManageSettings.ShutdownServer1Saturday = chkSat1.Checked;
            profile.AutoManageSettings.ShutdownServer1PerformUpdate = chkUpdate1.Checked;
            profile.AutoManageSettings.ShutdownServer1Restart = chkRestart1.Checked;

            profile.AutoManageSettings.ShutdownServer2 = chkShutdown2.Checked;
            profile.AutoManageSettings.ShutdownServer2Hour = txtShutdow2.Text;
            profile.AutoManageSettings.ShutdownServer2Sunday = chkSun2.Checked;
            profile.AutoManageSettings.ShutdownServer2Monday = chkMon2.Checked;
            profile.AutoManageSettings.ShutdownServer2Tuesday = chkTue2.Checked;
            profile.AutoManageSettings.ShutdownServer2Wednesday = chkWed2.Checked;
            profile.AutoManageSettings.ShutdownServer2Thu = chkThu2.Checked;
            profile.AutoManageSettings.ShutdownServer2Friday = chkFri2.Checked;
            profile.AutoManageSettings.ShutdownServer2Saturday = chkSat2.Checked;
            profile.AutoManageSettings.ShutdownServer2PerformUpdate = chkUpdate2.Checked;
            profile.AutoManageSettings.ShutdownServer2Restart = chkRestart2.Checked;

            profile.AutoManageSettings.IncludeInAutoBackup = chkIncludeAutoBackup.Checked;
            profile.AutoManageSettings.IncludeInAutoUpdate = chkAutoUpdate.Checked;
            profile.AutoManageSettings.AutoStartServer = chkAutoStart.Checked;

            profile.ARKConfiguration = null;
            profile.SaveProfile(MainForm.Settings);

            LoadSettings(profile, this.tab);
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {

            SaveProfile();

            FrmProgress frm = new FrmProgress(MainForm.Settings, profile);
            frm.ShowDialog();

            LoadSettings(this.profile, this.tab);
        }

        private void tbAutoSavePeriod_Scroll(object sender, EventArgs e)
        {
            txtAutoSavePeriod.Text = tbAutoSavePeriod.Value.ToString();
        }

        private void tbFirstBackup_Scroll(object sender, EventArgs e)
        {
            txtFirstBackup.Text = tbFirstBackup.Value.ToString();
        }

        private void tbSubBackups_Scroll(object sender, EventArgs e)
        {
            txtSubBackups.Text = tbSubBackups.Value.ToString();
        }

        private void chkAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            rbOnBoot.Enabled = chkAutoStart.Checked;
            rbOnLogin.Enabled = chkAutoStart.Checked;
        }

        private void chkShutdown1_CheckedChanged(object sender, EventArgs e)
        {

            txtShutdow1.Enabled = chkShutdown1.Checked;
            chkSun1.Enabled = chkShutdown1.Checked;
            chkMon1.Enabled = chkShutdown1.Checked;
            chkTue1.Enabled = chkShutdown1.Checked;
            chkWed1.Enabled = chkShutdown1.Checked;
            chkThu1.Enabled = chkShutdown1.Checked;
            chkFri1.Enabled = chkShutdown1.Checked;
            chkSat1.Enabled = chkShutdown1.Checked;
            chkUpdate1.Enabled = chkShutdown1.Checked;
            chkRestart1.Enabled = chkShutdown1.Checked;
        }

        private void chkShutdown2_CheckedChanged(object sender, EventArgs e)
        {

            txtShutdow2.Enabled = chkShutdown2.Checked;
            chkSun2.Enabled = chkShutdown2.Checked;
            chkMon2.Enabled = chkShutdown2.Checked;
            chkTue2.Enabled = chkShutdown2.Checked;
            chkWed2.Enabled = chkShutdown2.Checked;
            chkThu2.Enabled = chkShutdown2.Checked;
            chkFri2.Enabled = chkShutdown2.Checked;
            chkSat2.Enabled = chkShutdown2.Checked;
            chkUpdate2.Enabled = chkShutdown2.Checked;
            chkRestart2.Enabled = chkShutdown2.Checked;
        }

        private void btProcessorAffinity_Click(object sender, EventArgs e)
        {
            FrmProcessors frm = new FrmProcessors(profile.ValheimConfiguration.Administration.CPUAffinity == "All", profile.ValheimConfiguration.Administration.CPUAffinityList);
            frm.updateCpuAffinity = (bool all, List<ProcessorAffinity> lst) =>
            {
                profile.ValheimConfiguration.Administration.CPUAffinity = all ? "All" : string.Join(",", lst.FindAll(x => x.Selected).Select(x => x.ProcessorNumber.ToString()));
                profile.ValheimConfiguration.Administration.CPUAffinityList = lst;
                txtAffinity.Text = profile.ValheimConfiguration.Administration.CPUAffinity;
            };
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            txtCommand.Text = profile.ValheimConfiguration.GetCommandLinesArguments(MainForm.Settings, profile, MainForm.LocaIP);
        }

        private async void btStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (profile.IsRunning)
                {
                    //TODO: remove from here and place and use the profile closeserver
                    AutoUpdate x = new AutoUpdate();
                    await x.CloseServer(profile, MainForm.Settings);
                }
                else
                {
                    profile.StartServer(MainForm.Settings);
                }
            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void txtServerPort_TextChanged(object sender, EventArgs e)
        {
            int port;
            if (int.TryParse(txtServerPort.Text, out port))
            {
                txtPeerPort.Text = (port + 1).ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fbd.ShowDialog();

            txtSaveLocation.Text = fbd.SelectedPath;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            fbd.ShowDialog();

            txtLogLocation.Text = fbd.SelectedPath;
        }

        public void isRunningProcess()
        {
            while (true)
            {
                Process process = null;

                if (ProcessID == -1)
                {
                    process = profile.GetExeProcess();
                }
                else
                {
                    try
                    {
                        process = Process.GetProcessById(ProcessID);
                    }
                    catch (Exception)
                    {

                    }
                }

                if (process != null)
                {
                    ProcessID = process.Id;
                    isRunning = true;
                }
                else
                {
                    ProcessID = -1;
                    isRunning = false;
                }
                if (!UsefullTools.isFormRunning("MainForm")) break;
                Thread.Sleep(timerGetProcess.Interval);
            }
        }

        private void timerGetProcess_Tick(object sender, EventArgs e)
        {
            try
            {
                timerGetProcess.Enabled = false;

                if (isRunning) btStart.Text = "Stop";
                else btStart.Text = "Start";
                btUpdate.Enabled = !isRunning;

            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
            }
            finally { timerGetProcess.Enabled = true; }
        }

        private void FrmValheim_Load(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(isRunningProcess));
            thread.Start();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
