using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using OphiussaFramework.Models;
using OphiussaFramework.Components;

namespace OphiussaServerManagerV2
{
    public partial class FrmDriveSelection : Form
    {  
        public FrmDriveSelection()
        {
            InitializeComponent();
        }

        private string                 FolderName       { get; set; } = "osmdata";

        private List<DriveInfoDisplay> DriveInformation { get; set; } = new List<DriveInfoDisplay>();

        private void PopulateDriveDisplayInfo()
        {
            DriveInformation = DriveInfo.GetDrives().Where(d => d.IsReady && d.DriveType == DriveType.Fixed).Select(d => new DriveInfoDisplay(d)).ToList();
            string pathRoot = Path.GetPathRoot(Assembly.GetEntryAssembly().Location);
            if (!pathRoot.EndsWith("\\"))
                pathRoot += "\\";

            foreach (var driveInfoDisplay in DriveInformation)
            {
                var itm = new ExListBoxItem(driveInfoDisplay.DriveInfo.Name, driveInfoDisplay.Line1, driveInfoDisplay.Line2, driveInfoDisplay);
                exListBox1.Items.Add(itm);
                if (driveInfoDisplay.DriveInfo.RootDirectory.FullName.Equals(pathRoot))
                {
                    exListBox1.SelectedItem = itm;
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        { 
            var driveInfo = (exListBox1.SelectedItem as ExListBoxItem).AssociatedObject as DriveInfoDisplay;

            var s = new Settings(driveInfo.DriveInfo.RootDirectory.FullName, txtFolderName.Text);
            s.SteamWepApiKey      = textBox2.Text;
            s.CurseForgeApiKey = textBox3.Text;
            //var guid = Guid.NewGuid();
            //s.GUID = guid.ToString();

            Global.SqlLite.UpSertSettings(s);

            MessageBox.Show("After this setup go to Settings to configure Auto-Update and Auto-Backup");
            Close();
        }

        private void FrmDriveSelection_Load(object sender, EventArgs e)
        {
            PopulateDriveDisplayInfo();
            txtFolderName.Text = FolderName;
        }
    }
}
