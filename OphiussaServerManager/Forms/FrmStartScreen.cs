using Newtonsoft.Json;
using OphiussaServerManager.Common.Models;
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

namespace OphiussaServerManager.Forms
{
    public partial class FrmStartScreen : Form
    {
        public FrmStartScreen()
        {
            InitializeComponent();
        }

        private void FrmStartScreen_Load(object sender, EventArgs e)
        {
            PopulateDriveInformation();
            txtFolderName.Text = FolderName;
        }

        public string FolderName { get; set; } = "osmdata";
        public List<DriveInfoDisplay> DriveInformation { get; set; } = new List<DriveInfoDisplay>();

        private void PopulateDriveInformation()
        {
            this.DriveInformation = ((IEnumerable<DriveInfo>)DriveInfo.GetDrives()).Where<DriveInfo>((Func<DriveInfo, bool>)(d => d.IsReady && d.DriveType == DriveType.Fixed)).Select<DriveInfo, DriveInfoDisplay>((Func<DriveInfo, DriveInfoDisplay>)(d => new DriveInfoDisplay(d))).ToList<DriveInfoDisplay>();
            string pathRoot = Path.GetPathRoot(Assembly.GetEntryAssembly().Location);
            if (!pathRoot.EndsWith("\\"))
                pathRoot += "\\";

            foreach (DriveInfoDisplay driveInfoDisplay in this.DriveInformation)
            {
                exListBoxItem itm = new exListBoxItem(driveInfoDisplay.DriveInfo.Name, driveInfoDisplay.Line1, driveInfoDisplay.Line2, driveInfoDisplay);
                exListBox1.Items.Add(itm);
                if (driveInfoDisplay.DriveInfo.RootDirectory.FullName.Equals(pathRoot))
                {
                    this.exListBox1.SelectedItem = (object)itm;
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
            //if (!System.IO.File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")))
            //{

            DriveInfoDisplay driveInfo = (exListBox1.SelectedItem as exListBoxItem).AssociatedObject as DriveInfoDisplay;

            Common.Models.Settings s = new Common.Models.Settings(driveInfo.DriveInfo.RootDirectory.FullName, txtFolderName.Text);
            s.SteamKey = textBox2.Text;
            s.CurseForgeKey = textBox3.Text;
            Guid guid = Guid.NewGuid();
            s.GUID = guid.ToString();
            string jsonString = JsonConvert.SerializeObject(s, Formatting.Indented);
            File.WriteAllText(fileName, jsonString);
            //}
            MessageBox.Show("After this setup go to Settings to configure Auto-Update and Auto-Backup");
            this.Close();
        }
    }
}
