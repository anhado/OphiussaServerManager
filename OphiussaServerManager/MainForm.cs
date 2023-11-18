using Newtonsoft.Json;
using OphiussaServerManager.Forms;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.Sockets; 
using Open.Nat;

namespace OphiussaServerManager
{
    public partial class MainForm : Form
    {
        Dictionary<string, Profile> Profiles = new Dictionary<string, Profile>();
        Dictionary<string, LinkProfileForm> linkProfileForms = new Dictionary<string, LinkProfileForm>();
        public static Common.Models.Settings Settings;

        public static string PublicIP { get; set; }
        public static string LocaIP { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {

            if (!File.Exists("config.json"))
            {
                Forms.Settings settings = new Forms.Settings();
                settings.ShowDialog();
            }
            Settings = JsonConvert.DeserializeObject<Common.Models.Settings>(File.ReadAllText("config.json"));



            if (Settings.UpdateSteamCMDOnStartup) Common.NetworkTools.DownloadSteamCMD();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            txtVersion.Text = fvi.FileVersion;

            LoadProfiles();

            txtLocalIP.Text = await Task.Run(() => Common.NetworkTools.GetHostIp());

            var discoverer = new NatDiscoverer();
            var device = await discoverer.DiscoverDeviceAsync();
            var ip = await device.GetExternalIPAsync();
            
            txtPublicIP.Text = ip.ToString();
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
                Profiles.Add(prf.Key, prf);

                FrmArk frm = new FrmArk();
                frm.LoadSettings(prf, tab);
                addform(tab, frm);

                linkProfileForms.Add(prf.Key, new LinkProfileForm() { Form = frm, Profile = prf, Tab = tab });
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
            Forms.Settings f = new Forms.Settings();
            f.Show();
        }

        private void txtPublicIP_TextChanged(object sender, EventArgs e)
        {
            PublicIP = txtPublicIP.Text;
        }

        private void txtLocalIP_TextChanged(object sender, EventArgs e)
        {
            LocaIP = txtLocalIP.Text;
        }

        private async void btRefreshIP_Click(object sender, EventArgs e)
        {


            var discoverer = new NatDiscoverer();
            var device = await discoverer.DiscoverDeviceAsync();
            var ip = await device.GetExternalIPAsync();
            txtPublicIP.Text = ip.ToString();
            Console.WriteLine("The external IP Address is: {0} ", ip);

            // var xxx = await device.GetAllMappingsAsync();

            // await device.CreatePortMapAsync(new Mapping(Protocol.TcpUpd, 1600, 1700, "The mapping name"));
            // await device.CreatePortMapAsync(new Mapping(Protocol.Tcp, 1601, 1701, "The mapping name"));
            // await device.CreatePortMapAsync(new Mapping(Protocol.Udp, 1600, 1700, "The mapping name"));
            // await device.CreatePortMapAsync(new Mapping(Protocol.Udp, 1601, 1701, "The mapping name"));



        } 
    }
}
