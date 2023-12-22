using Microsoft.Win32.TaskScheduler;
using Newtonsoft.Json;
using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models.SupportedServers;
using OphiussaServerManager.Tools;
using OphiussaServerManager.Tools.Update;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace OphiussaServerManager.Forms
{
    public partial class FrmArk : Form
    {
        Profile profile;
        TabPage tab;
        bool isInstalled = false;
        bool isRunning = false;
        int ProcessID = -1;

        public FrmArk()
        {
            InitializeComponent();
        }

        private void FrmArk_Load(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(isRunningProcess));
            thread.Start();
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

                chkEnableCrossPlay.Enabled = profile.Type.ServerType == EnumServerType.ArkSurviveEvolved;
                chkEnablPublicIPEpic.Enabled = profile.Type.ServerType == EnumServerType.ArkSurviveEvolved;
                ChkEpicOnly.Enabled = profile.Type.ServerType == EnumServerType.ArkSurviveEvolved;
                txtBanUrl.Enabled = chkUseBanUrl.Checked;

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
            chkUseApi.Checked = profile.ARKConfiguration.Administration.UseServerAPI;
            txtServerName.Text = profile.ARKConfiguration.Administration.ServerName;
            txtServerPWD.Text = profile.ARKConfiguration.Administration.ServerPassword;
            txtAdminPass.Text = profile.ARKConfiguration.Administration.ServerAdminPassword;
            txtSpePwd.Text = profile.ARKConfiguration.Administration.ServerSpectatorPassword;
            txtLocalIP.SelectedValue = profile.ARKConfiguration.Administration.LocalIP;
            txtServerPort.Text = profile.ARKConfiguration.Administration.ServerPort;
            txtPeerPort.Text = profile.ARKConfiguration.Administration.PeerPort;
            txtQueryPort.Text = profile.ARKConfiguration.Administration.QueryPort;
            chkEnableRCON.Checked = profile.ARKConfiguration.Administration.UseRCON;
            txtRCONPort.Text = profile.ARKConfiguration.Administration.RCONPort;
            txtRCONBuffer.Text = profile.ARKConfiguration.Administration.RCONServerLogBuffer.ToString();
            cboMap.SelectedValue = profile.ARKConfiguration.Administration.MapName;
            cbBranch.Text = profile.ARKConfiguration.Administration.Branch.ToString();
            txtMods.Text = string.Join(",", profile.ARKConfiguration.Administration.ModIDs.ToArray());
            txtTotalConversion.Text = profile.ARKConfiguration.Administration.TotalConversionID;
            tbAutoSavePeriod.Value = profile.ARKConfiguration.Administration.AutoSavePeriod;
            txtAutoSavePeriod.Text = tbAutoSavePeriod.Value.ToString();
            txtMOTD.Text = profile.ARKConfiguration.Administration.MOD;
            tbMOTDDuration.Value = profile.ARKConfiguration.Administration.MODDuration;
            tbMOTDInterval.Value = profile.ARKConfiguration.Administration.MODInterval;
            chkEnableInterval.Checked = profile.ARKConfiguration.Administration.EnableInterval;

            tbMaxPlayers.Value = profile.ARKConfiguration.Administration.MaxPlayers;
            txtMaxPlayers.Text = profile.ARKConfiguration.Administration.MaxPlayers.ToString();

            chkEnableIdleTimeout.Checked = profile.ARKConfiguration.Administration.EnablIdleTimeOut;
            tbIdleTimeout.Value = profile.ARKConfiguration.Administration.IdleTimout;
            txtIdleTimeout.Text = profile.ARKConfiguration.Administration.IdleTimout.ToString();

            chkUseBanUrl.Checked = profile.ARKConfiguration.Administration.UseBanListUrl;
            txtBanUrl.Text = profile.ARKConfiguration.Administration.BanListUrl;
            chkDisableVAC.Checked = profile.ARKConfiguration.Administration.DisableVAC;
            chkEnableBattleEye.Checked = profile.ARKConfiguration.Administration.EnableBattleEye;
            chkDisablePlayerMove.Checked = profile.ARKConfiguration.Administration.DisablePlayerMovePhysics;
            chkOutputLogToConsole.Checked = profile.ARKConfiguration.Administration.OutputLogToConsole;
            chkUseAllCores.Checked = profile.ARKConfiguration.Administration.UseAllCores;
            chkUseCache.Checked = profile.ARKConfiguration.Administration.UseCache;
            chkNoHang.Checked = profile.ARKConfiguration.Administration.NoHandDetection;
            chkNoDinos.Checked = profile.ARKConfiguration.Administration.NoDinos;
            chkNoUnderMeshChecking.Checked = profile.ARKConfiguration.Administration.NoUnderMeshChecking;
            chkNoUnderMeshKilling.Checked = profile.ARKConfiguration.Administration.NoUnderMeshKilling;
            chkEnableVivox.Checked = profile.ARKConfiguration.Administration.EnableVivox;
            chkAllowSharedConnections.Checked = profile.ARKConfiguration.Administration.AllowSharedConnections;
            chkRespawnDinosOnStartup.Checked = profile.ARKConfiguration.Administration.RespawnDinosOnStartUp;
            chkEnableForceRespawn.Checked = profile.ARKConfiguration.Administration.EnableAutoForceRespawnDinos;
            tbRespawnInterval.Value = profile.ARKConfiguration.Administration.AutoForceRespawnDinosInterval;
            chkDisableSpeedHack.Checked = profile.ARKConfiguration.Administration.DisableAntiSpeedHackDetection;
            tbSpeedBias.Value = profile.ARKConfiguration.Administration.AntiSpeedHackBias;
            chkForceDX10.Checked = profile.ARKConfiguration.Administration.ForceDirectX10;
            chkForceLowMemory.Checked = profile.ARKConfiguration.Administration.ForceLowMemory;
            chkForceNoManSky.Checked = profile.ARKConfiguration.Administration.ForceNoManSky;
            chkUseNoMemoryBias.Checked = profile.ARKConfiguration.Administration.UseNoMemoryBias;
            chkStasisKeepControllers.Checked = profile.ARKConfiguration.Administration.StasisKeepController;
            chkAllowAnsel.Checked = profile.ARKConfiguration.Administration.ServerAllowAnsel;
            chkStructuresOptimization.Checked = profile.ARKConfiguration.Administration.StructureMemoryOptimizations;
            chkEnableCrossPlay.Checked = profile.ARKConfiguration.Administration.EnableCrossPlay;
            chkEnableCrossPlay.Checked = profile.ARKConfiguration.Administration.EnablePublicIPForEpic;
            ChkEpicOnly.Checked = profile.ARKConfiguration.Administration.EpicStorePlayersOnly;
            txtAltSaveDirectory.Text = profile.ARKConfiguration.Administration.AlternateSaveDirectoryName;
            txtClusterID.Text = profile.ARKConfiguration.Administration.ClusterID;
            chkClusterOverride.Checked = profile.ARKConfiguration.Administration.ClusterDirectoryOverride;

            cboPriority.SelectedItem = profile.ARKConfiguration.Administration.CPUPriority;

            txtAffinity.Text = profile.ARKConfiguration.Administration.CPUAffinity;

            chkEnableServerAdminLogs.Checked = profile.ARKConfiguration.Administration.EnableServerAdminLogs;
            chkServerAdminLogsIncludeTribeLogs.Checked = profile.ARKConfiguration.Administration.ServerAdminLogsIncludeTribeLogs;
            chkServerRCONOutputTribeLogs.Checked = profile.ARKConfiguration.Administration.ServerRCONOutputTribeLogs;
            chkAllowHideDamageSourceFromLogs.Checked = profile.ARKConfiguration.Administration.AllowHideDamageSourceFromLogs;
            chkLogAdminCommandsToPublic.Checked = profile.ARKConfiguration.Administration.LogAdminCommandsToPublic;
            chkLogAdminCommandstoAdmins.Checked = profile.ARKConfiguration.Administration.LogAdminCommandsToAdmins;
            chkTribeLogDestroyedEnemyStructures.Checked = profile.ARKConfiguration.Administration.TribeLogDestroyedEnemyStructures;
            txtMaximumTribeLogs.Text = profile.ARKConfiguration.Administration.MaximumTribeLogs.ToString();

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


            #region Validations 

            txtRCONPort.Enabled = chkEnableRCON.Checked;
            txtRCONBuffer.Enabled = chkEnableRCON.Checked;
            tbMOTDInterval.Enabled = chkEnableInterval.Checked;
            txtMOTDDuration.Text = tbMOTDDuration.Value.ToString();
            txtMOTDInterval.Text = tbMOTDInterval.Value.ToString();

            if (!Directory.Exists(txtLocation.Text))
            {
                btUpdate.Text = "Install";
                isInstalled = false;
            }
            else
            {
                if (Common.Utils.IsAValidFolder(txtLocation.Text, new List<string> { "Engine", "ShooterGame", "steamapps" }))
                {
                    btUpdate.Text = "Update/Verify";
                    isInstalled = true;
                }
                else
                {
                    btUpdate.Text = "Install";
                    isInstalled = false;
                }

            }

            btStart.Enabled = isInstalled;
            btRCON.Enabled = isInstalled;

            cboMap.DataSource = SupportedServers.GetMapLists(profile.Type.ServerType);
            cboMap.ValueMember = "Key";
            cboMap.DisplayMember = "Description";

            #endregion

            txtVersion.Text = profile.GetVersion();
            txtBuild.Text = profile.GetBuild();

            txtCommand.Text = profile.ARKConfiguration.GetCommandLinesArguments(MainForm.Settings, profile, MainForm.LocaIP);

            //profile.ARKConfiguration.LoadGameINI(profile);
        }

        private void txtProfileName_Validated(object sender, EventArgs e)
        {
            tab.Text = txtProfileName.Text + "          ";
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            if (txtServerPWD.PasswordChar == '\0')
                txtServerPWD.PasswordChar = '*';
            else
                txtServerPWD.PasswordChar = '\0';
        }

        private void textBox2_DoubleClick(object sender, EventArgs e)
        {
            if (txtAdminPass.PasswordChar == '\0')
                txtAdminPass.PasswordChar = '*';
            else
                txtAdminPass.PasswordChar = '\0';
        }

        private void textBox3_DoubleClick(object sender, EventArgs e)
        {

            if (txtSpePwd.PasswordChar == '\0')
                txtSpePwd.PasswordChar = '*';
            else
                txtSpePwd.PasswordChar = '\0';
        }

        private void txtServerPort_TextChanged(object sender, EventArgs e)
        {
            int port;
            if (int.TryParse(txtServerPort.Text, out port))
            {
                txtPeerPort.Text = (port + 1).ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadDefaultFieldValues();

            if (MessageBox.Show("Do you want reload from Server Config Files?", "Reload Option", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                profile.ARKConfiguration = profile.ARKConfiguration.LoadGameINI(profile);
                LoadSettings(profile, this.tab);
            }
            else
            {
                string dir = MainForm.Settings.DataFolder + "Profiles\\";
                if (!Directory.Exists(dir))
                {
                    return;
                }

                string[] files = System.IO.Directory.GetFiles(dir);

                foreach (string file in files)
                {
                    Profile p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(file));
                    if (p.Key == this.profile.Key)
                    {
                        LoadSettings(p, this.tab);
                        break;
                    }
                }
            }
        }

        private void btChooseFolder_Click(object sender, EventArgs e)
        {
            if (!Common.Utils.IsAValidFolder(txtLocation.Text, new List<string> { "Engine", "ShooterGame", "steamapps" }))
            {
                MessageBox.Show("This is not a valid Ark Folder");
                return;
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            try
            {  
                SaveProfile();
                CreateWindowsTasks();

                MainForm.notificationController.SendReloadCommand(profile.Key);
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
                string fileName = MainForm.Settings.DataFolder + $"StartServer\\Run_{profile.Key.Replace("-", "")}.cmd";
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
            profile.ARKConfiguration.Administration.UseServerAPI = chkUseApi.Checked;
            profile.ARKConfiguration.Administration.ServerName = txtServerName.Text;
            profile.ARKConfiguration.Administration.ServerPassword = txtServerPWD.Text;
            profile.ARKConfiguration.Administration.ServerAdminPassword = txtAdminPass.Text;
            profile.ARKConfiguration.Administration.ServerSpectatorPassword = txtSpePwd.Text;
            profile.ARKConfiguration.Administration.LocalIP = txtLocalIP.SelectedValue.ToString();
            profile.ARKConfiguration.Administration.ServerPort = txtServerPort.Text;
            profile.ARKConfiguration.Administration.PeerPort = txtPeerPort.Text;
            profile.ARKConfiguration.Administration.QueryPort = txtQueryPort.Text;
            profile.ARKConfiguration.Administration.UseRCON = chkEnableRCON.Checked;
            profile.ARKConfiguration.Administration.RCONPort = txtRCONPort.Text;
            profile.ARKConfiguration.Administration.RCONServerLogBuffer = int.Parse(txtRCONBuffer.Text);
            profile.ARKConfiguration.Administration.MapName = cboMap.SelectedValue.ToString();
            profile.ARKConfiguration.Administration.Branch = cbBranch.Text;
            profile.ARKConfiguration.Administration.ModIDs = txtMods.Text.Split(',').ToList();
            profile.ARKConfiguration.Administration.TotalConversionID = txtTotalConversion.Text;
            profile.ARKConfiguration.Administration.AutoSavePeriod = tbAutoSavePeriod.Value;
            profile.ARKConfiguration.Administration.MOD = txtMOTD.Text;
            profile.ARKConfiguration.Administration.MODDuration = tbMOTDDuration.Value;
            profile.ARKConfiguration.Administration.MODInterval = tbMOTDInterval.Value;
            profile.ARKConfiguration.Administration.EnableInterval = chkEnableInterval.Checked;
            profile.ARKConfiguration.Administration.MaxPlayers = tbMaxPlayers.Value;
            profile.ARKConfiguration.Administration.EnablIdleTimeOut = chkEnableIdleTimeout.Checked;
            profile.ARKConfiguration.Administration.IdleTimout = tbIdleTimeout.Value;
            profile.ARKConfiguration.Administration.UseBanListUrl = chkUseBanUrl.Checked;
            profile.ARKConfiguration.Administration.BanListUrl = txtBanUrl.Text;
            profile.ARKConfiguration.Administration.DisableVAC = chkDisableVAC.Checked;
            profile.ARKConfiguration.Administration.EnableBattleEye = chkEnableBattleEye.Checked;
            profile.ARKConfiguration.Administration.DisablePlayerMovePhysics = chkDisablePlayerMove.Checked;
            profile.ARKConfiguration.Administration.OutputLogToConsole = chkOutputLogToConsole.Checked;
            profile.ARKConfiguration.Administration.UseAllCores = chkUseAllCores.Checked;
            profile.ARKConfiguration.Administration.UseCache = chkUseCache.Checked;
            profile.ARKConfiguration.Administration.NoHandDetection = chkNoHang.Checked;
            profile.ARKConfiguration.Administration.NoDinos = chkNoDinos.Checked;
            profile.ARKConfiguration.Administration.NoUnderMeshChecking = chkNoUnderMeshChecking.Checked;
            profile.ARKConfiguration.Administration.NoUnderMeshKilling = chkNoUnderMeshKilling.Checked;
            profile.ARKConfiguration.Administration.EnableVivox = chkEnableVivox.Checked;
            profile.ARKConfiguration.Administration.AllowSharedConnections = chkAllowSharedConnections.Checked;
            profile.ARKConfiguration.Administration.RespawnDinosOnStartUp = chkRespawnDinosOnStartup.Checked;
            profile.ARKConfiguration.Administration.EnableAutoForceRespawnDinos = chkEnableForceRespawn.Checked;
            profile.ARKConfiguration.Administration.AutoForceRespawnDinosInterval = tbRespawnInterval.Value;
            profile.ARKConfiguration.Administration.DisableAntiSpeedHackDetection = chkDisableSpeedHack.Checked;
            profile.ARKConfiguration.Administration.AntiSpeedHackBias = tbSpeedBias.Value;
            profile.ARKConfiguration.Administration.ForceDirectX10 = chkForceDX10.Checked;
            profile.ARKConfiguration.Administration.ForceLowMemory = chkForceLowMemory.Checked;
            profile.ARKConfiguration.Administration.ForceNoManSky = chkForceNoManSky.Checked;
            profile.ARKConfiguration.Administration.UseNoMemoryBias = chkUseNoMemoryBias.Checked;
            profile.ARKConfiguration.Administration.StasisKeepController = chkStasisKeepControllers.Checked;
            profile.ARKConfiguration.Administration.ServerAllowAnsel = chkAllowAnsel.Checked;
            profile.ARKConfiguration.Administration.StructureMemoryOptimizations = chkStructuresOptimization.Checked;
            profile.ARKConfiguration.Administration.EnableCrossPlay = chkEnableCrossPlay.Checked;
            profile.ARKConfiguration.Administration.EnablePublicIPForEpic = chkEnableCrossPlay.Checked;
            profile.ARKConfiguration.Administration.EpicStorePlayersOnly = ChkEpicOnly.Checked;
            profile.ARKConfiguration.Administration.AlternateSaveDirectoryName = txtAltSaveDirectory.Text;
            profile.ARKConfiguration.Administration.ClusterID = txtClusterID.Text;
            profile.ARKConfiguration.Administration.ClusterDirectoryOverride = chkClusterOverride.Checked;
            profile.ARKConfiguration.Administration.CPUPriority = (ProcessPriorityClass)cboPriority.SelectedValue;


            profile.ARKConfiguration.Administration.EnableServerAdminLogs = chkEnableServerAdminLogs.Checked;
            profile.ARKConfiguration.Administration.ServerAdminLogsIncludeTribeLogs = chkServerAdminLogsIncludeTribeLogs.Checked;
            profile.ARKConfiguration.Administration.ServerRCONOutputTribeLogs = chkServerRCONOutputTribeLogs.Checked;
            profile.ARKConfiguration.Administration.AllowHideDamageSourceFromLogs = chkAllowHideDamageSourceFromLogs.Checked;
            profile.ARKConfiguration.Administration.LogAdminCommandsToPublic = chkLogAdminCommandsToPublic.Checked;
            profile.ARKConfiguration.Administration.LogAdminCommandsToAdmins = chkLogAdminCommandstoAdmins.Checked;
            profile.ARKConfiguration.Administration.TribeLogDestroyedEnemyStructures = chkTribeLogDestroyedEnemyStructures.Checked;
            profile.ARKConfiguration.Administration.MaximumTribeLogs = int.Parse(txtMaximumTribeLogs.Text);


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

            profile.SaveProfile(MainForm.Settings);

            LoadSettings(profile, this.tab);
        }

        private void chkEnableRCON_CheckedChanged(object sender, EventArgs e)
        {

            txtRCONPort.Enabled = chkEnableRCON.Checked;
            txtRCONBuffer.Enabled = chkEnableRCON.Checked;
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

        private void tbMOTDDuration_Scroll(object sender, EventArgs e)
        {
            txtMOTDDuration.Text = tbMOTDDuration.Value.ToString();
        }

        private void tbMOTDInterval_Scroll(object sender, EventArgs e)
        {
            txtMOTDInterval.Text = tbMOTDInterval.Value.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            tbMOTDInterval.Enabled = chkEnableInterval.Checked;
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            tbIdleTimeout.Enabled = chkEnableIdleTimeout.Checked;
        }

        private void tbIdleTimeout_Scroll(object sender, EventArgs e)
        {
            txtIdleTimeout.Text = tbIdleTimeout.Value.ToString();
        }

        private void tbMaxPlayers_Scroll(object sender, EventArgs e)
        {
            txtMaxPlayers.Text = tbMaxPlayers.Value.ToString();
        }

        private void btRCON_Click(object sender, EventArgs e)
        {
            FrmRCONServer frm = new FrmRCONServer(profile);
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtCommand.Text = profile.ARKConfiguration.GetCommandLinesArguments(MainForm.Settings, profile, MainForm.LocaIP);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkUseBanUrl_CheckedChanged(object sender, EventArgs e)
        {
            txtBanUrl.Enabled = chkUseBanUrl.Checked;
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
                btRCON.Enabled = isRunning && profile.ARKConfiguration.Administration.UseRCON;

                UsefullTools.MainForm.SetTabHeader(tab, profile, isRunning);
                //TabColors[page] = color;
                //MainForm..Invalidate();

            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
            }
            finally { timerGetProcess.Enabled = true; }
        }

        private void checkBox1_CheckedChanged_2(object sender, EventArgs e)
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

        private void toolsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btProcessorAffinity_Click(object sender, EventArgs e)
        {
            FrmProcessors frm = new FrmProcessors(profile.ARKConfiguration.Administration.CPUAffinity == "All", profile.ARKConfiguration.Administration.CPUAffinityList);
            frm.updateCpuAffinity = (bool all, List<ProcessorAffinity> lst) =>
            {
                profile.ARKConfiguration.Administration.CPUAffinity = all ? "All" : string.Join(",", lst.FindAll(x => x.Selected).Select(x => x.ProcessorNumber.ToString()));
                profile.ARKConfiguration.Administration.CPUAffinityList = lst;
                txtAffinity.Text = profile.ARKConfiguration.Administration.CPUAffinity;
            };
            frm.ShowDialog();
        }

        private void btMods_Click(object sender, EventArgs e)
        {
            FrmModManager frm = new FrmModManager();
            frm.updateModList = (List<ModListDetails> lst) =>
            {
                txtMods.Text = string.Join(",", lst.Select(x => x.ModID.ToString()).ToArray());
            };

            frm.LoadMods(ref profile, txtMods.Text, this);
        }
    }
}
