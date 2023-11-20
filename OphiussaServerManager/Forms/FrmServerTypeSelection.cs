using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Models.SupportedServers;
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
    public partial class FrmServerTypeSelection : Form
    {
        public Action<(SupportedServersType serversType, string installDir)> AddNewTabPage { get; set; }
        public int TotalPageCount = 0;
        public FrmServerTypeSelection()
        {
            InitializeComponent();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            string dir = "";
            dir = txtDir.Text;
            if (!chkUsedInstall.Checked) dir += txtDirName.Text + "\\";

            if (ValidateFolder(dir))
            {
                AddNewTabPage.Invoke(((SupportedServersType)cboServerType.SelectedItem, dir));
                this.Close();
            }
        }

        private bool ValidateFolder(string dir)
        {
            if (chkUsedInstall.Checked)
            {
                if (!System.IO.Directory.Exists(dir))
                {
                    MessageBox.Show("Selected folder dont exists");
                    return false;
                }
                //TODO:validate files in folder to check if is a correct folder 
                SupportedServersType type = (SupportedServersType)cboServerType.SelectedItem;
                switch (type.ServerType)
                {
                    case EnumServerType.ArkSurviveEvolved:
                    case EnumServerType.ArkSurviveAscended:

                        if (!Common.Utils.IsAValidFolder(dir, new List<string> { "Engine", "ShooterGame", "steamapps" }))
                        {
                            MessageBox.Show("This is not a valid Ark Folder");
                            return false;
                        }
                        break;
                }
            }
            else
            {
                if (System.IO.Directory.Exists(dir))
                {
                    MessageBox.Show($"Selected folder already exists: {dir}");
                    return false;
                }
            }

            return true;
        }

        private void FrmServerTypeSelection_Load(object sender, EventArgs e)
        {
            cboServerType.DataSource = SupportedServers.ServerTypeList;
            cboServerType.ValueMember = "KeyName";
            cboServerType.DisplayMember = "ServerTypeDescription";
            txtDir.Text = MainForm.Settings.DefaultInstallationFolder;
            txtDirName.Text = $"Server{TotalPageCount}";
        }

        private void chkUsedInstall_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUsedInstall.Checked)
                txtDirName.Enabled = false;
            else
                txtDirName.Enabled = true;
        }
    }
}
