using System;
using System.IO;
using System.Windows.Forms;
using OphiussaFramework;
using OphiussaFramework.Models;

namespace OphiussaServerManagerV2 {
    public partial class FrmServerTypeSelection : Form {
        public FrmServerTypeSelection() {
            InitializeComponent();
        }
         
        public Action<(PluginType serversType, string installDir)> AddNewTabPage  { get; set; }

        private void FrmServerTypeSelection_Load(object sender, EventArgs e) {
            cboServerType.DataSource    = ConnectionController.GetServerTypes();
            cboServerType.DisplayMember = "Name";
            cboServerType.ValueMember   = "GameType";

            txtDir.Text     = ConnectionController.Settings.DefaultInstallFolder;

            var folders =  System.IO.Directory.GetDirectories(ConnectionController.Settings.DefaultInstallFolder);

            txtDirName.Text = $"Server{folders.Length + 1}";
        }
         
        private void button1_Click(object sender, EventArgs e) {
            fdDiag.ShowDialog();
            txtDir.Text = fdDiag.SelectedPath;
        }

        private void btAdd_Click(object sender, EventArgs e) {
            if (cboServerType.SelectedItem == null) {
                MessageBox.Show("Invalid server type");
                return;
            }


            string dir = "";
            dir = txtDir.Text;
            if (!chkUsedInstall.Checked) dir += txtDirName.Text + "\\";

            if (chkUsedInstall.Checked) {
                if (ConnectionController.Plugins.ContainsKey(cboServerType.SelectedValue.ToString())) {
                    if (!ConnectionController.Plugins[cboServerType.SelectedValue.ToString()].IsValidFolder(dir)) MessageBox.Show("Invalid installation folder");
                }
                else {
                    MessageBox.Show("Invalid server type");
                    return;
                }
            }
            else {
                if (Directory.Exists(dir)) {
                    MessageBox.Show($"Selected folder already exists: {dir}");
                    return;
                }
            }

            AddNewTabPage.Invoke(((PluginType)cboServerType.SelectedItem, dir));
            Close();
        }

        private void chkUsedInstall_CheckedChanged(object sender) { 
            if (chkUsedInstall.Checked)
                txtDirName.Enabled = false;
            else
                txtDirName.Enabled = true;
        }

        private void osmButton_11_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}