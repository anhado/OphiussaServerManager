using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OphiussaServerManager.Helpers;
using OphiussaServerManager.Profiles;
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
    public partial class FrmArk : Form
    {
        Profile profile;
        TabPage tab;
        bool isInstalled = false;
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
            txtServerName.Text = profile.Configuration.Administration.ServerName;
            txtServerPWD.Text = profile.Configuration.Administration.ServerPassword;
            txtAdminPass.Text = profile.Configuration.Administration.ServerAdminPassword;
            txtSpePwd.Text = profile.Configuration.Administration.ServerSpectatorPassword;
            txtLocalIP.SelectedValue = profile.Configuration.Administration.LocalIP;
            txtServerPort.Text = profile.Configuration.Administration.ServerPort;
            txtPeerPort.Text = profile.Configuration.Administration.PeerPort;
            txtQueryPort.Text = profile.Configuration.Administration.QueryPort;
            chkEnableRCON.Checked = profile.Configuration.Administration.UseRCON;
            txtRCONPort.Text = profile.Configuration.Administration.RCONPort;
            txtRCONBuffer.Text = profile.Configuration.Administration.RCONServerLogBuffer.ToString();
            cboMap.Text = profile.Configuration.Administration.MapName;
            cbBranch.Text = profile.Configuration.Administration.Branch.ToString();
            txtMods.Text = string.Join(",", profile.Configuration.Administration.ModIDs.ToArray());
            txtTotalConversion.Text = profile.Configuration.Administration.TotalConversionID;
            tbAutoSavePeriod.Value = profile.Configuration.Administration.AutoSavePeriod;
            txtAutoSavePeriod.Text = tbAutoSavePeriod.Value.ToString();
            tbMOTDDuration.Value = profile.Configuration.Administration.MODDuration;
            tbMOTDInterval.Value = profile.Configuration.Administration.MODInterval;
            chkEnableInterval.Checked = profile.Configuration.Administration.EnableInterval;
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

            cboMap.DataSource = SupportedServers.SupportedServers.GetMapLists(profile.Type.ServerType);
            cboMap.ValueMember = "Key";
            cboMap.DisplayMember = "Description";

            #endregion

            txtVersion.Text = GetVersion();
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
            profile.Configuration.Administration.ServerName = txtServerName.Text;
            profile.Configuration.Administration.ServerPassword = txtServerPWD.Text;
            profile.Configuration.Administration.ServerAdminPassword = txtAdminPass.Text;
            profile.Configuration.Administration.ServerSpectatorPassword = txtSpePwd.Text;
            profile.Configuration.Administration.LocalIP = txtLocalIP.SelectedValue.ToString();
            profile.Configuration.Administration.ServerPort = txtServerPort.Text;
            profile.Configuration.Administration.PeerPort = txtPeerPort.Text;
            profile.Configuration.Administration.QueryPort = txtQueryPort.Text;
            profile.Configuration.Administration.UseRCON = chkEnableRCON.Checked;
            profile.Configuration.Administration.RCONPort = txtRCONPort.Text;
            profile.Configuration.Administration.RCONServerLogBuffer = int.Parse(txtRCONBuffer.Text);
            profile.Configuration.Administration.MapName = cboMap.Text;
            profile.Configuration.Administration.Branch = cbBranch.Text;
            profile.Configuration.Administration.ModIDs = txtMods.Text.Split(',').ToList();
            profile.Configuration.Administration.TotalConversionID = txtTotalConversion.Text;
            profile.Configuration.Administration.AutoSavePeriod = tbAutoSavePeriod.Value;
            profile.Configuration.Administration.MOD = txtMOTD.Text;
            profile.Configuration.Administration.MODDuration = tbMOTDDuration.Value;
            profile.Configuration.Administration.MODInterval = tbMOTDInterval.Value;
            profile.Configuration.Administration.EnableInterval = chkEnableInterval.Checked;

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
    }
}
