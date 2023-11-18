using CoreRCON;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog.Fluent;
using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models.SupportedServers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OphiussaServerManager.Forms
{
    public partial class FrmArk : Form
    {
        Profile profile;
        TabPage tab;
        bool isInstalled = false;
        bool isRunning = false;
        public FrmArk()
        {
            InitializeComponent();
        }

        private void FrmArk_Load(object sender, EventArgs e)
        {
            LoadDefaultFieldValues();
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
                MessageBox.Show("LoadDefaultFieldValues:" + e.Message);
            }
        }

        public void LoadSettings(Profile profile, TabPage tab)
        {
            this.profile = profile;
            this.tab = tab;
            txtProfileID.Text = profile.Key;
            txtProfileName.Text = profile.Name;
            tab.Text = txtProfileName.Text;
            txtServerType.Text = profile.Type.ServerTypeDescription;
            txtLocation.Text = profile.InstallLocation;
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
            tbMOTDDuration.Value = profile.ARKConfiguration.Administration.MODDuration;
            tbMOTDInterval.Value = profile.ARKConfiguration.Administration.MODInterval;
            chkEnableInterval.Checked = profile.ARKConfiguration.Administration.EnableInterval;

            tbMaxPlayers.Value = profile.ARKConfiguration.Administration.MaxPlayers;
            chkEnableIdleTimeout.Checked = profile.ARKConfiguration.Administration.EnablIdleTimeOut;
            tbIdleTimeout.Value = profile.ARKConfiguration.Administration.IdleTimout;
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

            cboPriority.SelectedValue = profile.ARKConfiguration.Administration.CPUPriority;//TODO

            txtAffinity.Text = profile.ARKConfiguration.Administration.CPUAffinity;//TODO

            chkEnableServerAdminLogs.Checked = profile.ARKConfiguration.Administration.EnableServerAdminLogs;
            chkServerAdminLogsIncludeTribeLogs.Checked = profile.ARKConfiguration.Administration.ServerAdminLogsIncludeTribeLogs;
            chkServerRCONOutputTribeLogs.Checked = profile.ARKConfiguration.Administration.ServerRCONOutputTribeLogs;
            chkAllowHideDamageSourceFromLogs.Checked = profile.ARKConfiguration.Administration.AllowHideDamageSourceFromLogs;
            chkLogAdminCommandsToPublic.Checked = profile.ARKConfiguration.Administration.LogAdminCommandsToPublic;
            chkLogAdminCommandstoAdmins.Checked = profile.ARKConfiguration.Administration.LogAdminCommandsToAdmins;
            chkTribeLogDestroyedEnemyStructures.Checked = profile.ARKConfiguration.Administration.TribeLogDestroyedEnemyStructures;
            txtMaximumTribeLogs.Text = profile.ARKConfiguration.Administration.MaximumTribeLogs;

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
                if (Utils.IsAValidFolder(txtLocation.Text, new List<string> { "Engine", "ShooterGame", "steamapps" }))
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

            txtVersion.Text = GetVersion();
            txtBuild.Text = GetBuild();

            txtCommand.Text = profile.ARKConfiguration.GetCommandLinesArguments(MainForm.Settings, profile, MainForm.LocaIP);

            profile.ARKConfiguration.LoadGameINI(profile);
        }

        private string GetBuild()
        {
            string fileName = "appmanifest_2430930.acf";
            if (profile.Type.ServerType == EnumServerType.ArkSurviveEvolved) fileName = "appmanifest_376030.acf";
            if (!File.Exists(Path.Combine(txtLocation.Text, "steamapps", fileName))) return "";

            string[] content = File.ReadAllText(Path.Combine(txtLocation.Text, "steamapps", fileName)).Split('\n');

            foreach (var item in content)
            {
                string[] t = item.Split('\t');

                if (item.Contains("buildid"))
                {
                    return t[3].Replace("\"", "");
                }

            }
            return System.IO.File.ReadAllText(Path.Combine(txtLocation.Text, "steamapps", "appmanifest_2430930.acf"));
        }

        private string GetVersion()
        {
            if (!File.Exists(Path.Combine(txtLocation.Text, "version.txt"))) return "";



            return System.IO.File.ReadAllText(Path.Combine(txtLocation.Text, "version.txt"));
        }

        private void txtProfileName_Validated(object sender, EventArgs e)
        {
            tab.Text = txtProfileName.Text;
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

        private void btChooseFolder_Click(object sender, EventArgs e)
        {
            if (!Utils.IsAValidFolder(txtLocation.Text, new List<string> { "Engine", "ShooterGame", "steamapps" }))
            {
                MessageBox.Show("This is not a valid Ark Folder");
                return;
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            SaveProfile();
        }

        private void SaveProfile()
        {
            if (!MainForm.Settings.Branchs.Contains(cbBranch.Text.ToString())) { MainForm.Settings.Branchs.Add(cbBranch.Text); MainForm.Settings.SaveSettings(); }

            profile.Name = txtProfileName.Text;
            profile.InstallLocation = txtLocation.Text;
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
            profile.ARKConfiguration.Administration.CPUPriority = (ProcessPriorityClass)cboPriority.SelectedValue;//TODO
            profile.ARKConfiguration.Administration.CPUAffinity = "All";//TODO


            profile.ARKConfiguration.Administration.EnableServerAdminLogs = chkEnableServerAdminLogs.Checked;
            profile.ARKConfiguration.Administration.ServerAdminLogsIncludeTribeLogs = chkServerAdminLogsIncludeTribeLogs.Checked;
            profile.ARKConfiguration.Administration.ServerRCONOutputTribeLogs = chkServerRCONOutputTribeLogs.Checked;
            profile.ARKConfiguration.Administration.AllowHideDamageSourceFromLogs = chkAllowHideDamageSourceFromLogs.Checked;
            profile.ARKConfiguration.Administration.LogAdminCommandsToPublic = chkLogAdminCommandsToPublic.Checked;
            profile.ARKConfiguration.Administration.LogAdminCommandsToAdmins = chkLogAdminCommandstoAdmins.Checked;
            profile.ARKConfiguration.Administration.TribeLogDestroyedEnemyStructures = chkTribeLogDestroyedEnemyStructures.Checked;
            profile.ARKConfiguration.Administration.MaximumTribeLogs = txtMaximumTribeLogs.Text;


            profile.SaveProfile();
        }

        private void chkEnableRCON_CheckedChanged(object sender, EventArgs e)
        {

            txtRCONPort.Enabled = chkEnableRCON.Checked;
            txtRCONBuffer.Enabled = chkEnableRCON.Checked;
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            SaveProfile();

            NetworkTools.InstallGame(profile);

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
            RCONServer frm = new RCONServer(profile);
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

        private void btStart_Click(object sender, EventArgs e)
        {
            Utils.ExecuteAsAdmin(Path.Combine(profile.InstallLocation, profile.Type.ExecutablePath), profile.ARKConfiguration.GetCommandLinesArguments(MainForm.Settings, profile, MainForm.LocaIP), false);
        }

        private void timerGetProcess_Tick(object sender, EventArgs e)
        {
            Process process = profile.GetExeProcess();
            if (process != null)
            {
                isRunning = true;
                btStart.Text = "Stop";
            }
            else
            {
                isRunning = false;
                btStart.Text = "Start";
            }
        }
    }
}
