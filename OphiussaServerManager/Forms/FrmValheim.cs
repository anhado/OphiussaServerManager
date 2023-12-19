using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models.SupportedServers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
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

            txtVersion.Text = profile.GetVersion();
            txtBuild.Text = profile.GetBuild();
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
                //CreateWindowsTasks();

                MessageBox.Show("Profile Saved");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SaveProfile()
        {
            if (!MainForm.Settings.Branchs.Contains(cbBranch.Text.ToString())) { MainForm.Settings.Branchs.Add(cbBranch.Text); MainForm.Settings.SaveSettings(); }

            profile.Name = txtProfileName.Text;
            profile.InstallLocation = txtLocation.Text;

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
    }
}
