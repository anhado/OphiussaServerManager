using System;
using System.IO;
using System.Windows.Forms;
using OphiussaServerManager.Common.Models.Profiles;

namespace OphiussaServerManager.Forms {
    public partial class FrmDeleteProfile : Form {
        public FrmDeleteProfile() {
            InitializeComponent();
        }

        private Profile Profile { get; set; }

        private void btCancel_Click(object sender, EventArgs e) {
            Close();
        }

        private void btDelete_Click(object sender, EventArgs e) {
            string dir = MainForm.Settings.DataFolder + "Profiles\\";
            if (!Directory.Exists(dir)) return;

            string[] files = Directory.GetFiles(dir);
            foreach (string f in files) {
                var fileInfo = new FileInfo(f);
                if (fileInfo.Name.Contains(Profile.Key)) {
                    if (chkDeleteServerFolder.Checked) {
                        var di = new DirectoryInfo(Profile.InstallLocation);

                        foreach (var file in di.GetFiles()) file.Delete();
                        foreach (var dir1 in di.GetDirectories()) dir1.Delete(true);
                        di.Delete(true);
                    }

                    if (chkDeleteBackup.Checked) {
                        //TODO: DELETE BACKUP FOLDER
                    }

                    fileInfo.Delete();
                    return;
                }
            }
        }

        public DialogResult OpenDeleteProfile(Profile profile) {
            txtKey.Text      = profile.Key;
            txtLocation.Text = profile.InstallLocation;
            txtName.Text     = profile.Name;
            Profile          = profile;
            return ShowDialog();
        }

        private void FrmDeleteProfile_Load(object sender, EventArgs e) {
        }
    }
}