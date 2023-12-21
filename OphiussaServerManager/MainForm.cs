using Microsoft.Win32.TaskScheduler;
using Newtonsoft.Json;
using Open.Nat;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models.SupportedServers;
using OphiussaServerManager.Forms;
using OphiussaServerManager.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;

namespace OphiussaServerManager
{
    public partial class MainForm : Form
    {
        Dictionary<string, Profile> Profiles = new Dictionary<string, Profile>();
        Dictionary<string, LinkProfileForm> linkProfileForms = new Dictionary<string, LinkProfileForm>();
        private Dictionary<TabPage, Color> TabColors = new Dictionary<TabPage, Color>();

        public static Common.Models.Settings Settings;
        private int HoverIndex = -1;

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
            try
            {
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

                UsefullTools.MainForm = this;
                TabColors.Add(tabControl1.TabPages[0], SystemColors.Control);
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
                this.tabControl1.HandleCreated += tabControl1_HandleCreated;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                if (Settings.ProfileOrders.Count > 0)
                {
                    foreach (var profileOrder in Settings.ProfileOrders.OrderBy(x => x.Order))
                    {
                        string file = files.First(f => f.Contains(profileOrder.Key));
                        if (!string.IsNullOrEmpty(file))
                        {
                            Profile p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(file));
                            switch (p.Type.ServerType)
                            {
                                case EnumServerType.ArkSurviveEvolved:
                                case EnumServerType.ArkSurviveAscended:
                                    AddNewArkServer(p.Key, p.Type, "", p);
                                    break;
                                case EnumServerType.Valheim:
                                    AddNewValheimServer(p.Key, p.Type, "", p);
                                    break;
                                default:
                                    break;
                            }
                            files = files.Where(x => x != file).ToArray();
                        }
                    }
                }

                if (files.Length > 0)
                {
                    foreach (string file in files)
                    {
                        Profile p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(file));
                        switch (p.Type.ServerType)
                        {
                            case EnumServerType.ArkSurviveEvolved:
                            case EnumServerType.ArkSurviveAscended:
                                AddNewArkServer(p.Key, p.Type, "", p);
                                break;
                            case EnumServerType.Valheim:
                                AddNewValheimServer(p.Key, p.Type, "", p);
                                break;
                            default:
                                break;
                        }
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
                //e.Cancel = true;
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
                            case EnumServerType.Valheim:
                                AddNewValheimServer(guid.ToString(), newServer.serversType, newServer.installDir, null);
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
                            case Common.Models.SupportedServers.EnumServerType.Valheim:
                                AddNewValheimServer(guid.ToString(), newServer.serversType, newServer.installDir, null);
                                break;
                            default:
                                break;
                        }

                    };
                    frm.ShowDialog();

                }
            }

        }

        void AddNewValheimServer(string guid, SupportedServersType serverType, string InstallLocation, Profile p)
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
                    tab.Text = p.Name + "          ";
                }
                Profiles.Add(prf.Key, prf);

                FrmValheim frm = new FrmValheim();
                frm.LoadSettings(prf, tab);
                addform(tab, frm);

                TabColors.Add(tab, SystemColors.Control);
                linkProfileForms.Add(prf.Key, new LinkProfileForm() { Form = frm, Profile = prf, Tab = tab });
                tabControl1.SelectTab(index);
                tabControl1.SelectedTab = tab;
            }
            catch (Exception e)
            {
                OphiussaLogger.logger.Error(e);
                MessageBox.Show($"Error Adding tab {e.Message}");
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
                TabColors.Add(tab, SystemColors.Control);

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
                    tab.Text = p.Name + "          ";
                }
                Profiles.Add(prf.Key, prf);

                FrmArk frm = new FrmArk();
                frm.LoadSettings(prf, tab);
                addform(tab, frm);

                linkProfileForms.Add(prf.Key, new LinkProfileForm() { Form = frm, Profile = prf, Tab = tab });
                tabControl1.SelectTab(index);
                tabControl1.SelectedTab = tab;

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
            try
            {
                Common.NetworkTools.DownloadSteamCMD();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        private void perfomanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUsedResources frmUsedResources = new FrmUsedResources();
            frmUsedResources.Show();
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            //switch (e.Index)
            //{
            //    case 0:
            //        e.Graphics.FillRectangle(new SolidBrush(Color.Red), e.Bounds);
            //        break;
            //    case 1:
            //        e.Graphics.FillRectangle(new SolidBrush(Color.Blue), e.Bounds);
            //        break;
            //    default:
            //        break;
            //}

            if (TabColors.ContainsKey(tabControl1.TabPages[e.Index]))
            {
                using (Brush br = new SolidBrush(TabColors[tabControl1.TabPages[e.Index]]))
                {
                    e.Graphics.FillRectangle(br, e.Bounds);
                    //SizeF sz = e.Graphics.MeasureString(tabControl1.TabPages[e.Index].Text, e.Font);
                    //e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

                    //Rectangle rect = e.Bounds;
                    //rect.Offset(0, 1);
                    //rect.Inflate(0, -1);
                    //e.Graphics.DrawRectangle(Pens.DarkGray, rect);
                    //e.DrawFocusRectangle();
                }

            }

            var tabPage = this.tabControl1.TabPages[e.Index];
            var tabRect = this.tabControl1.GetTabRect(e.Index);
            tabRect.Inflate(-2, -2);
            if (e.Index == this.tabControl1.TabCount - 1)
            {
                var addImage = Properties.Resources.add_icon_icon_16x16;
                e.Graphics.DrawImage(addImage,
                    tabRect.Left + (tabRect.Width - addImage.Width) / 2,
                    tabRect.Top + (tabRect.Height - addImage.Height) / 2);
            }
            else
            {
                var closeImage = Properties.Resources.Close_icon_icon;
                e.Graphics.DrawImage(closeImage,
                    (tabRect.Right - closeImage.Width),
                    tabRect.Top + (tabRect.Height - closeImage.Height) / 2);
                TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font,
                    tabRect, tabPage.ForeColor, TextFormatFlags.Left);
                /*
                var g = e.Graphics;
                var tp = tabControl1.TabPages[e.Index];

                var rt = e.Bounds;
                var rx = new Rectangle(rt.Right - 20, (rt.Y + (rt.Height - 12)) / 2 + 1, 12, 12);

                if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
                {
                    rx.Offset(0, 2);
                }

                rt.Inflate(-rx.Width, 0);
                rt.Offset(-(rx.Width / 2), 0);
                using (Font f = new Font("Marlett", 8f))
                using (StringFormat sf = new StringFormat()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter,
                    FormatFlags = StringFormatFlags.NoWrap,
                })
                {
                    g.DrawString(tp.Text, tp.Font ?? Font, Brushes.Black, rt, sf);
                    if (tp.Text != "+") g.DrawString("r", f, HoverIndex == e.Index ? Brushes.Black : Brushes.Red, rx, sf);
                }
                tp.Tag = rx;
                */
            }
        }

        private void tabControl1_MouseMove(object sender, MouseEventArgs e)
        {
            //for (int i = 0; i < tabControl1.TabCount; i++)
            //{
            //    if (tabControl1.TabPages[i].Tag == null) continue;
            //    var rx = (Rectangle)tabControl1.TabPages[i].Tag;

            //    if (rx.Contains(e.Location))
            //    {
            //        //To avoid the redundant calls. 
            //        if (HoverIndex != i)
            //        {
            //            HoverIndex = i;
            //            tabControl1.Invalidate();
            //        }
            //        return;
            //    }
            //}

            ////To avoid the redundant calls.
            //if (HoverIndex != -1)
            //{
            //    HoverIndex = -1;
            //    tabControl1.Invalidate();
            //}
        }

        private void tabControl1_MouseLeave(object sender, EventArgs e)
        {
            //if (HoverIndex != -1)
            //{
            //    HoverIndex = -1;
            //    tabControl1.Invalidate();
            //}
        }

        private void tabControl1_MouseUp(object sender, MouseEventArgs e)
        {
            //for (int i = 0; i < tabControl1.TabCount; i++)
            //{
            //    var rx = (Rectangle)tabControl1.TabPages[i].Tag;

            //    if (rx.Contains(e.Location)) //changed e.Location to rx.Location
            //    {
            //        if (MessageBox.Show("Do you want to delete this profile?", "DELETE PROFILE", MessageBoxButtons.OKCancel) == DialogResult.OK)
            //        {

            //            foreach (var key in linkProfileForms.Keys)
            //            {
            //                if (linkProfileForms[key].Tab == tabControl1.TabPages[i])
            //                {
            //                    FrmDeleteProfile frm = new FrmDeleteProfile();
            //                    if (frm.OpenDeleteProfile(linkProfileForms[key].Profile) == DialogResult.OK)
            //                    {
            //                        tabControl1.TabPages[i].Dispose();
            //                        return;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            var lastIndex = this.tabControl1.TabCount - 1;
            if (this.tabControl1.GetTabRect(lastIndex).Contains(e.Location))
            {
                //this.tabControl1.TabPages.Insert(lastIndex, "New Tab");
                //this.tabControl1.SelectedIndex = lastIndex;
            }
            else
            {
                for (var i = 0; i < this.tabControl1.TabPages.Count; i++)
                {
                    var tabRect = this.tabControl1.GetTabRect(i);
                    tabRect.Inflate(-2, -2);
                    var closeImage = Properties.Resources.Close_icon_icon;
                    var imageRect = new Rectangle(
                        (tabRect.Right - closeImage.Width),
                        tabRect.Top + (tabRect.Height - closeImage.Height) / 2,
                        closeImage.Width,
                        closeImage.Height);
                    if (imageRect.Contains(e.Location))
                    {
                        //if (MessageBox.Show("Do you want to delete this profile?", "DELETE PROFILE", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        //{

                        foreach (var key in linkProfileForms.Keys)
                        {
                            if (linkProfileForms[key].Tab == tabControl1.TabPages[i])
                            {
                                FrmDeleteProfile frm = new FrmDeleteProfile();
                                if (frm.OpenDeleteProfile(linkProfileForms[key].Profile) == DialogResult.OK)
                                {
                                    tabControl1.TabPages[i].Dispose();
                                    return;
                                }
                            }
                        }
                        //}
                    }
                }
            }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        private const int TCM_SETMINTABWIDTH = 0x1300 + 49;
        private void tabControl1_HandleCreated(object sender, EventArgs e)
        {
            SendMessage(this.tabControl1.Handle, TCM_SETMINTABWIDTH, IntPtr.Zero, (IntPtr)16);
        }

        public void SetTabHeader(TabPage page, Profile prf, bool isRunning)
        {
            Color color = (!prf.IsInstalled ? Color.LightBlue : (isRunning ? Color.LightGreen : Color.LightSalmon));
            if (TabColors[page] == color) return;
            TabColors[page] = color;
            tabControl1.Invalidate();
        }

        private void tabControl1_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void orderProfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOrderProfiles frm = new FrmOrderProfiles();
            frm.LoadProfiles(Settings);
            frm.ShowDialog();

            //TODO: RELOAD TABS https://stackoverflow.com/questions/2559280/programmatically-change-the-tab-order
            //LoadProfiles();
        }

        private void createDesktopShortcutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IShellLink link = (IShellLink)new ShellLink();

            // setup shortcut information
            link.SetDescription("Ophiussa Server Manager");
            link.SetPath(System.Reflection.Assembly.GetEntryAssembly().Location);

            // save it
            IPersistFile file = (IPersistFile)link;
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            file.Save(Path.Combine(desktopPath, "Ophiussa Server Manager.lnk"), false);
        }

        private void createMonitorDesktopShortcutToolStripMenuItem_Click(object sender, EventArgs e)
        {

            IShellLink link = (IShellLink)new ShellLink();

            // setup shortcut information
            link.SetDescription("Ophiussa Server Manager Monitor");
            link.SetPath(System.Reflection.Assembly.GetEntryAssembly().Location);
            link.SetArguments(" -monitor");
            // save it
            IPersistFile file = (IPersistFile)link;
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            file.Save(Path.Combine(desktopPath, "Ophiussa Server Manager Monitor.lnk"), false);
        }
    }
}
