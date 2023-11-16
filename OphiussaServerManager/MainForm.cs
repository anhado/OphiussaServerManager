using Newtonsoft.Json;
using OphiussaServerManager.Forms;
using OphiussaServerManager.Helpers;
using OphiussaServerManager.Profiles;
using OphiussaServerManager.SupportedServers;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OphiussaServerManager
{
    public partial class MainForm : Form
    {
        List<Profile> Profiles = new List<Profile>();

        public static Classes.Settings Settings;
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            if (!File.Exists("config.json"))
            {
                Settings settings = new Settings();
                settings.ShowDialog();
            }
            Settings = JsonConvert.DeserializeObject<Classes.Settings>(File.ReadAllText("config.json"));
            txtLocalIP.Text = NetworkTools.GetHostIp();
            txtPublicIP.Text = NetworkTools.GetPublicIp();
            if (Settings.UpdateSteamCMDOnStartup) NetworkTools.DownloadSteamCMD();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            txtVersion.Text = fvi.FileVersion;

            LoadProfiles();
        }

        private void LoadProfiles()
        {
            try
            {
                string dir = Settings.DataFolder + "Profiles\\";
                if (!Directory.Exists(dir))
                {
                    return;
                }

                string[] files = System.IO.Directory.GetFiles(dir);
                foreach (string file in files)
                {
                    Profile p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(file));
                    switch (p.Type.ServerType)
                    {
                        case EnumServerType.ArkSurviveEvolved:
                        case EnumServerType.ArkSurviveAscended:
                            AddNewArkServer(p.Key, p.Type, "", p);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"LoadProfiles: {e.Message}");
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            Guid guid = Guid.NewGuid();
            if (tabControl1.SelectedTab == NewTab)
            {
                FrmServerTypeSelection frm = new FrmServerTypeSelection();
                frm.TotalPageCount = tabControl1.TabPages.Count;
                frm.AddNewTabPage += (newServer) =>
                {

                    switch (newServer.serversType.ServerType)
                    {
                        case EnumServerType.ArkSurviveEvolved:
                        case EnumServerType.ArkSurviveAscended:
                            AddNewArkServer(guid.ToString(), newServer.serversType, newServer.installDir, null);
                            break;
                        default:
                            break;
                    }

                };
                frm.ShowDialog();

            }
        }

        void AddNewArkServer(string guid, SupportedServersType serverType, string InstallLocation, Profile p)
        {
            try
            {
                string tabName = "Server " + tabControl1.TabPages.Count;
                tabControl1.TabPages.Insert(tabControl1.TabPages.Count - 1, guid.ToString(), tabName);
                int index = tabControl1.TabPages.IndexOfKey(guid.ToString());
                TabPage tab = tabControl1.TabPages[index];

                Profile prf;
                if (p == null)
                {
                    prf = new Profile(guid, tabName, serverType);
                    prf.InstallLocation = InstallLocation;
                    prf.SaveProfile();
                }
                else
                {
                    prf = p;
                    tab.Text = p.Name;
                }
                Profiles.Add(prf);

                FrmArk frm = new FrmArk();
                frm.LoadSettings(prf, tab);
                addform(tab, frm);

                tabControl1.SelectTab(index);

            }
            catch (Exception e)
            {
                MessageBox.Show($"Error Adding tab {e.Message}");
            }
        }

        public void addform(TabPage tp, Form f)
        {

            f.TopLevel = false;
            //no border if needed
            f.FormBorderStyle = FormBorderStyle.None;
            f.AutoScaleMode = AutoScaleMode.Dpi;

            if (!tp.Controls.Contains(f))
            {
                tp.Controls.Add(f);
                f.Dock = DockStyle.Fill;
                f.Show();
                Refresh();
            }
            Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings f = new Settings();
            f.Show();
        }
    }
}
