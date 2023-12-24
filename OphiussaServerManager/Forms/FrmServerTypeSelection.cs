using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Models.SupportedServers;

namespace OphiussaServerManager.Forms {
    public partial class FrmServerTypeSelection : Form {
        public int TotalPageCount = 0;

        public FrmServerTypeSelection() {
            InitializeComponent();
        }

        public Action<(SupportedServersType serversType, string installDir)> AddNewTabPage { get; set; }

        private void btCancel_Click(object sender, EventArgs e) {
            Close();
        }

        private void btAdd_Click(object sender, EventArgs e) {
            string dir = "";
            dir = txtDir.Text;
            if (!chkUsedInstall.Checked) dir += txtDirName.Text + "\\";

            if (ValidateFolder(dir)) {
                AddNewTabPage.Invoke(((SupportedServersType)cboServerType.SelectedItem, dir));
                Close();
            }
        }

        private bool ValidateFolder(string dir) {
            if (chkUsedInstall.Checked) {
                if (!Directory.Exists(dir)) {
                    MessageBox.Show("Selected folder dont exists");
                    return false;
                }

                var type = (SupportedServersType)cboServerType.SelectedItem;
                switch (type.ServerType) {
                    case EnumServerType.ArkSurviveEvolved:
                    case EnumServerType.ArkSurviveAscended:

                        if (!Utils.IsAValidFolder(dir, new List<string> { "Engine", "ShooterGame", "steamapps" })) {
                            MessageBox.Show("This is not a valid Ark Folder");
                            return false;
                        }

                        break;

                    case EnumServerType.Valheim:
                        if (!Utils.IsAValidFolder(dir, new List<string> { "MonoBleedingEdge", "MonoBleedingEdge", "steamapps" })) {
                            MessageBox.Show("This is not a valid Ark Folder");
                            return false;
                        }

                        break;
                }
            }
            else {
                if (Directory.Exists(dir)) {
                    MessageBox.Show($"Selected folder already exists: {dir}");
                    return false;
                }
            }

            return true;
        }

        private void FrmServerTypeSelection_Load(object sender, EventArgs e) {
            cboServerType.DataSource    = SupportedServers.ServerTypeList;
            cboServerType.ValueMember   = "KeyName";
            cboServerType.DisplayMember = "ServerTypeDescription";
            txtDir.Text                 = MainForm.Settings.DefaultInstallationFolder;
            txtDirName.Text             = $"Server{TotalPageCount}";
        }

        private void chkUsedInstall_CheckedChanged(object sender, EventArgs e) {
            if (chkUsedInstall.Checked)
                txtDirName.Enabled = false;
            else
                txtDirName.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e) {
            fdDiag.ShowDialog();
            txtDir.Text = fdDiag.SelectedPath;
        }
    }
}