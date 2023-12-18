using OphiussaServerManager.Common.Models.Profiles;
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
    public partial class FrmDeleteProfile : Form
    {
        Profile Profile { get; set; }

        public FrmDeleteProfile()
        {
            InitializeComponent();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            string dir = MainForm.Settings.DataFolder + "Profiles\\";
            if (!Directory.Exists(dir))
            {
                return;
            }

            string[] files = System.IO.Directory.GetFiles(dir);
            foreach (var f in files)
            {
                FileInfo fileInfo = new FileInfo(f);
                if (fileInfo.Name.Contains(Profile.Key))
                {
                    if (chkDeleteServerFolder.Checked)
                    {
                        System.IO.DirectoryInfo di = new DirectoryInfo(Profile.InstallLocation);

                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dir1 in di.GetDirectories())
                        {
                            dir1.Delete(true);
                        }
                        di.Delete(true);
                    }
                    if (chkDeleteBackup.Checked)
                    {
                        //TODO: DELETE BACKUP FOLDER
                    }
                    fileInfo.Delete();
                    return;
                }
            }
        }

        public DialogResult OpenDeleteProfile(Profile profile)
        {
            txtKey.Text = profile.Key;
            txtLocation.Text = profile.InstallLocation;
            txtName.Text = profile.Name;
            this.Profile = profile;
            return base.ShowDialog();
        }
        private void FrmDeleteProfile_Load(object sender, EventArgs e)
        {

        }
    }
}
