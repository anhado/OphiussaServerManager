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
using OphiussaFramework.Extensions;
using OphiussaFramework.Models;

namespace OphiussaServerManagerV2
{
    public partial class FrmServerTypeSelection : Form
    {
        public FrmServerTypeSelection()
        {
            InitializeComponent();
        }

        public int TotalPageCount { get; internal set; }
        public Action<(PluginType serversType, string installDir)> AddNewTabPage { get; set; }

        private void FrmServerTypeSelection_Load(object sender, EventArgs e)
        {
            cboServerType.DataSource = Global.GetServerTypes();
            cboServerType.DisplayMember = "Name";
            cboServerType.ValueMember = "GameType";

            txtDir.Text = Global.Settings.DefaultInstallFolder;
            txtDirName.Text = $"Server{TotalPageCount}";
        }

        private void chkUsedInstall_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUsedInstall.Checked)
                txtDirName.Enabled = false;
            else
                txtDirName.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fdDiag.ShowDialog();
            txtDir.Text = fdDiag.SelectedPath;
        }

        private void btAdd_Click(object sender, EventArgs e)
        {

            if(cboServerType.SelectedItem == null)
            {
                MessageBox.Show("Invalid server type");
                return;
            }

            if (chkUsedInstall.Checked)
            {
                if (Global.plugins.ContainsKey(cboServerType.SelectedValue.ToString()))
                {
                    if (!Global.plugins[cboServerType.SelectedValue.ToString()].IsValidFolder(txtDirName.Text))
                    {
                        MessageBox.Show("Invalid installation folder");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid server type");
                    return;
                }
            }
            else
            {
                if (Directory.Exists(txtDirName.Text))
                {
                    MessageBox.Show($"Selected folder already exists: {txtDirName.Text}");
                    return;
                }
            }


            AddNewTabPage.Invoke(((PluginType)cboServerType.SelectedItem, txtDirName.Text));
            this.Close();
        }
    }
}
