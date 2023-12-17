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
using Microsoft.Win32.TaskScheduler;
using NLog.Common;
using System.Diagnostics;

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
#if DEBUG
            testsToolStripMenuItem.Visible = true;
#else
            testsToolStripMenuItem.Visible = false;
#endif
            if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")))
            {
                Forms.FrmSettings settings = new Forms.FrmSettings();
                settings.ShowDialog();
            }
            Settings = JsonConvert.DeserializeObject<Common.Models.Settings>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")));
            OphiussaLogger.ReconfigureLogging(Settings);

            if (Settings.UpdateSteamCMDOnStartup) Common.NetworkTools.DownloadSteamCMD();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            txtVersion.Text = fvi.FileVersion;

            LoadProfiles();

            txtLocalIP.Text = await System.Threading.Tasks.Task.Run(() => Common.NetworkTools.GetHostIp());

            try
            {
                var discoverer = new NatDiscoverer();
                var device = await discoverer.DiscoverDeviceAsync();
                var ip = await device.GetExternalIPAsync();
                txtPublicIP.Text = ip.ToString();
            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
            }
            try
            {
                if (txtPublicIP.Text == "")
                {
                    txtPublicIP.Text = await System.Threading.Tasks.Task.Run(() => Common.NetworkTools.GetPublicIp());
                }
            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
            }
            timerCheckTask.Enabled = true;
            tabControl1.SelectedIndex = 0;

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
                OphiussaLogger.logger.Error(e);
                MessageBox.Show($"LoadProfiles: {e.Message}");
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {

            if (e.TabPage == NewTab)
            {
                e.Cancel = true;
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
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabCount == 1)
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
                    prf.SaveProfile(Settings);
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
                OphiussaLogger.logger.Error(e);
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
            Forms.FrmSettings f = new Forms.FrmSettings();
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
        }

        private void btMonitor_Click(object sender, EventArgs e)
        {
            FrmServerMonitor monitor = new FrmServerMonitor();
            monitor.Show();
        }

        private void serverMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmServerMonitor monitor = new FrmServerMonitor();
            monitor.Show();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Forms.FrmSettings f = new Forms.FrmSettings();
            f.Show();
        }

        private async void refreshPublicIPToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                var discoverer = new NatDiscoverer();
                var device = await discoverer.DiscoverDeviceAsync();
                var ip = await device.GetExternalIPAsync();
                txtPublicIP.Text = ip.ToString();
                Console.WriteLine("The external IP Address is: {0} ", ip);

            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private async void refreshLocalIPToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            txtLocalIP.Text = await System.Threading.Tasks.Task.Run(() => Common.NetworkTools.GetHostIp());

        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timerCheckTask_Tick(object sender, EventArgs e)
        {
            try
            {
                timerCheckTask.Enabled = false;
                //System.Threading.Tasks.Task.Run(() => 
                //{ 
                var lTasks = TaskService.Instance.GetRunningTasks().ToList();

                string taskName = "OphiussaServerManager\\AutoBackup_" + MainForm.Settings.GUID;
                Microsoft.Win32.TaskScheduler.Task task = TaskService.Instance.GetTask(taskName);
                if (task != null)
                {
                    task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                    task.Definition.Settings.Priority = ProcessPriorityClass.Normal;
                    var x = lTasks.Find(xc => xc?.Name == "AutoBackup_" + MainForm.Settings.GUID);

                    if (x != null)
                    {
                        lblAutoBackup.Text = "Is Running";
                        lblAutoBackup.ForeColor = Color.GreenYellow;
                        btRun1.Visible = false;
                        btDisable1.Visible = false;
                    }
                    else
                    {
                        lblAutoBackup.Text = "Ready";
                        lblAutoBackup.ForeColor = Color.White;
                        btRun1.Visible = true;
                        btDisable1.Visible = true;
                    }
                }
                else
                {
                    lblAutoBackup.Text = "Not Running";
                    lblAutoBackup.ForeColor = Color.White;
                    btRun1.Visible = false;
                    btDisable1.Visible = false;
                }
                string taskName2 = "OphiussaServerManager\\AutoUpdate_" + MainForm.Settings.GUID;

                Microsoft.Win32.TaskScheduler.Task task2 = TaskService.Instance.GetTask(taskName2);
                if (task2 != null)
                {
                    var x = lTasks.Find(xc => xc?.Name == "AutoUpdate_" + MainForm.Settings.GUID);

                    if (x != null)
                    {
                        lblAutoUpdate.Text = "Is Running";
                        lblAutoUpdate.ForeColor = Color.GreenYellow;
                        btRun2.Visible = false;
                        btDisable2.Visible = false;
                    }
                    else
                    {
                        lblAutoUpdate.Text = "Ready";
                        lblAutoUpdate.ForeColor = Color.White;
                        btRun2.Visible = true;
                        btDisable2.Visible = true;
                    }
                }
                else
                {
                    lblAutoUpdate.Text = "Not Running";
                    lblAutoUpdate.ForeColor = Color.White;
                    btRun2.Visible = false;
                    btDisable2.Visible = false;
                }
                //}).Wait();


            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                OphiussaLogger.logger.Error(ex);
            }
            finally
            {
                timerCheckTask.Enabled = true;
            }

        }

        private void updateSteamCMDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Common.NetworkTools.DownloadSteamCMD();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string taskName2 = "OphiussaServerManager\\AutoBackup_" + MainForm.Settings.GUID;

            Microsoft.Win32.TaskScheduler.Task task2 = TaskService.Instance.GetTask(taskName2);
            if (task2 != null)
            {
                task2.Run();
                btRun1.Visible = false;
                btDisable1.Visible = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            string taskName = "OphiussaServerManager\\AutoUpdate_" + MainForm.Settings.GUID;

            Microsoft.Win32.TaskScheduler.Task task = TaskService.Instance.GetTask(taskName);
            if (task != null)
            {
                btRun2.Visible = false;
                btDisable2.Visible = false;
                task.Run();
            }
        }

        private void testsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTests frmTests = new FrmTests();
            frmTests.Show();
        }

        private void routingFirewallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPortForward frmPortForward = new FrmPortForward();
            frmPortForward.LoadPortFoward(Profiles);
        }
    }
}
