using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Newtonsoft.Json;
using OphiussaServerManager.Common.Models;

namespace OphiussaServerManager.Forms {
    public partial class FrmStartScreen : Form {
        public FrmStartScreen() {
            InitializeComponent();
        }

        public string                 FolderName       { get; set; } = "osmdata";
        public List<DriveInfoDisplay> DriveInformation { get; set; } = new List<DriveInfoDisplay>();

        private void FrmStartScreen_Load(object sender, EventArgs e) {
            PopulateDriveInformation();
            txtFolderName.Text = FolderName;
        }

        private void PopulateDriveInformation() {
            DriveInformation = DriveInfo.GetDrives().Where(d => d.IsReady && d.DriveType == DriveType.Fixed).Select(d => new DriveInfoDisplay(d)).ToList();
            string pathRoot = Path.GetPathRoot(Assembly.GetEntryAssembly().Location);
            if (!pathRoot.EndsWith("\\"))
                pathRoot += "\\";

            foreach (var driveInfoDisplay in DriveInformation) {
                var itm = new ExListBoxItem(driveInfoDisplay.DriveInfo.Name, driveInfoDisplay.Line1, driveInfoDisplay.Line2, driveInfoDisplay);
                exListBox1.Items.Add(itm);
                if (driveInfoDisplay.DriveInfo.RootDirectory.FullName.Equals(pathRoot)) {
                    exListBox1.SelectedItem = itm;
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
            //if (!System.IO.File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")))
            //{

            var driveInfo = (exListBox1.SelectedItem as ExListBoxItem).AssociatedObject as DriveInfoDisplay;

            var s = new Settings(driveInfo.DriveInfo.RootDirectory.FullName, txtFolderName.Text);
            s.SteamKey      = textBox2.Text;
            s.CurseForgeKey = textBox3.Text;
            var guid = Guid.NewGuid();
            s.Guid = guid.ToString();
            string jsonString = JsonConvert.SerializeObject(s, Formatting.Indented);
            File.WriteAllText(fileName, jsonString);
            //}
            MessageBox.Show("After this setup go to Settings to configure Auto-Update and Auto-Backup");
            Close();
        }
    }
}