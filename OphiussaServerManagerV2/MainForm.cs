using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.Extensions;
using OphiussaFramework.Models;
using OphiussaFramework.ServerUtils;
using OphiussaServerManagerV2.Properties;

namespace OphiussaServerManagerV2 {
    public partial class MainForm : Form {
        private const    int                        TcmSetmintabwidth = 0x1300 + 49;
        private readonly Dictionary<TabPage, Color> _tabColors        = new Dictionary<TabPage, Color>();

        public MainForm() {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e) {
            new FrmSettings().ShowDialog();
        }

        private void pluginsToolStripMenuItem_Click(object sender, EventArgs e) {
            new FrmPluginManager().ShowDialog();
        }

        private async void MainForm_Load(object sender, EventArgs e) {
            try {
                txtLocalIP.Text = NetworkTools.GetHostIp();

                try {
                    if (txtPublicIP.Text == "") txtPublicIP.Text = await Task.Run(() => NetworkTools.DiscoverPublicIPAsync().Result);
                }
                catch (Exception ex) {
                    //  OphiussaLogger.Logger.Error(ex);
                }

                tabControl1.SelectedIndex =  0;
                tabControl1.HandleCreated += tabControl1_HandleCreated;
            }
            catch (Exception exception) {
                OphiussaLogger.Logger.Error(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void txtPublicIP_DoubleClick(object sender, EventArgs e) {
            txtPublicIP.PasswordChar = txtPublicIP.PasswordChar == '\0' ? '*' : '\0'; 
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e) {
            if (_tabColors.ContainsKey(tabControl1.TabPages[e.Index]))
                using (Brush br = new SolidBrush(_tabColors[tabControl1.TabPages[e.Index]])) {
                    e.Graphics.FillRectangle(br, e.Bounds);
                }

            var tabPage = tabControl1.TabPages[e.Index];
            var tabRect = tabControl1.GetTabRect(e.Index);
            // tabRect.Width = 50;
            if (e.Index == tabControl1.TabCount - 1) {
                tabRect.Inflate(-2, -2);
                var addImage = Resources.add_icon_icon__1_;
                e.Graphics.DrawImage(addImage,
                                     tabRect.Left + (tabRect.Width  - addImage.Width)  / 2,
                                     tabRect.Top  + (tabRect.Height - addImage.Height) / 2);
            }
            else {
                tabRect.Inflate(-2, -2);
                var closeImage = Resources.Close_icon_icon;
                e.Graphics.DrawImage(closeImage,
                                     tabRect.Right - closeImage.Width,
                                     tabRect.Top   + (tabRect.Height - closeImage.Height) / 2);
                TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font,
                                      tabRect, tabPage.ForeColor, TextFormatFlags.Left);
            }
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        private void tabControl1_HandleCreated(object sender, EventArgs e) {
            SendMessage(tabControl1.Handle, TcmSetmintabwidth, IntPtr.Zero, (IntPtr)16);
        }

        private void tabControl1_Click(object sender, EventArgs e) {
            if (tabControl1.TabCount == 1) {
                var guid = Guid.NewGuid();
                if (tabControl1.SelectedTab == NewTab) {
                    var frm = new FrmServerTypeSelection();
                    frm.TotalPageCount = tabControl1.TabPages.Count;
                    frm.AddNewTabPage += newServer => {
                                             var nCtrl = Global.plugins[newServer.serversType.GameType].Clone(ServerUtils.InstallServerClick, ServerUtils.BackupServerClick, ServerUtils.StopServerClick, ServerUtils.StartServerClick);

                                             Global.serverControllers.Add(nCtrl.GetProfile().Key, nCtrl);

                                             //TODO: adicionar ao ecrã o form n tabulador novo
                                             AddNewFormConfiguration(nCtrl);
                                         };
                    frm.ShowDialog();
                }
            }
        }

        private void AddNewFormConfiguration(PluginController controller) {
            tabControl1.TabPages.Insert(tabControl1.TabPages.Count - 1, controller.GetProfile().Key, controller.GetProfile().Name + "          ");
            int index = tabControl1.TabPages.IndexOfKey(controller.GetProfile().Key);
            var tab   = tabControl1.TabPages[index];
            _tabColors.Add(tab, SystemColors.Control);

            Addform(tab, controller.GetConfigurationForm());

            tabControl1.SelectedTab = tab;
        }

        public void Addform(TabPage tp, Form f) {
            f.TopLevel = false;
            //no border if needed
            f.FormBorderStyle = FormBorderStyle.None;
            f.AutoScaleMode   = AutoScaleMode.Dpi;

            if (!tp.Controls.Contains(f)) {
                tp.Controls.Add(f);
                f.Dock = DockStyle.Fill;
                f.Show();
                Refresh();
            }

            Refresh();
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e) {
            int lastIndex = tabControl1.TabCount - 1;
            if (tabControl1.GetTabRect(lastIndex).Contains(e.Location)) {
                //this.tabControl1.TabPages.Insert(lastIndex, "New Tab");
                //this.tabControl1.SelectedIndex = lastIndex;
            }
            else {
                for (int i = 0; i < tabControl1.TabPages.Count; i++) {
                    var tabRect = tabControl1.GetTabRect(i);
                    tabRect.Inflate(-2, -2);
                    var closeImage = Resources.Close_icon_icon;
                    var imageRect = new Rectangle(
                                                  tabRect.Right - closeImage.Width,
                                                  tabRect.Top   + (tabRect.Height - closeImage.Height) / 2,
                                                  closeImage.Width,
                                                  closeImage.Height);
                    if (imageRect.Contains(e.Location))
                        if (MessageBox.Show("Do you want to delete this profile?", "DELETE PROFILE", MessageBoxButtons.OKCancel) == DialogResult.OK) {
                            //foreach (string key in _linkProfileForms.Keys)
                            //    if (_linkProfileForms[key].Tab == tabControl1.TabPages[i])
                            //    {
                            //        var frm = new FrmDeleteProfile();
                            //        if (frm.OpenDeleteProfile(_linkProfileForms[key].Profile) == DialogResult.OK)
                            //        {
                            //            tabControl1.TabPages[i].Dispose();
                            //            return;
                            //        }
                        }
                    //}
                }
            }
        }
    }
}