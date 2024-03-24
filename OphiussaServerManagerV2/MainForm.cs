using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using OphiussaFramework;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.Extensions;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;
using OphiussaFramework.ServerUtils;
using OphiussaServerManagerV2.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement; 

namespace OphiussaServerManagerV2 {
    public partial class MainForm : Form {
        private const int TcmSetmintabwidth = 0x1300 + 49; 

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
                ConnectionController.SetMainForm(this);
                 
                txtLocalIP.Text = NetworkTools.GetHostIp();

                try {
                    if (txtPublicIP.Text == "") txtPublicIP.Text = await Task.Run(() => NetworkTools.DiscoverPublicIPAsync().Result);
                }
                catch (Exception ex) {
                    //  OphiussaLogger.Logger.Error(ex);
                }

                LoadProfiles();
            }
            catch (Exception exception) {
                OphiussaLogger.Logger.Error(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void LoadProfiles() {
            List<RawProfile> lst = ConnectionController.SqlLite.GetProfiles();
            lst.ForEach(prf => {
                            if (!Global.plugins.ContainsKey(prf.Type)) return;
                            PluginController nCtrl   = new PluginController(Global.plugins[prf.Type].PluginLocation(), null, null, null, null, null, null, null, null, null, TabHeadChangeEvent);
                            nCtrl.SetProfile(prf);
                            Global.serverControllers.Add(nCtrl.GetProfile().Key, nCtrl);

                            AddNewFormConfiguration(nCtrl);
                        });
        }

        private void txtPublicIP_DoubleClick(object sender, EventArgs e) {
            txtPublicIP.PasswordChar = txtPublicIP.PasswordChar == '\0' ? '*' : '\0';
        }
         
        private void AddNewFormConfiguration(PluginController controller) {
            tabControlExtra1.TabPages.Insert(tabControlExtra1.TabPages.Count, controller.GetProfile().Key, controller.GetProfile().Name);
            int index = tabControlExtra1.TabPages.IndexOfKey(controller.GetProfile().Key);
            var tab = tabControlExtra1.TabPages[index]; 

            Addform(tab, controller.GetConfigurationForm(tab));

            tabControlExtra1.SelectedTab = tab;
        }

        public void Addform(TabPage tp, Form f) {
            f.TopLevel = false;
            //no border if needed
            f.FormBorderStyle = FormBorderStyle.None;
            f.AutoScaleMode = AutoScaleMode.Dpi;

            if (!tp.Controls.Contains(f)) {
                tp.Controls.Add(f);
                f.Dock = DockStyle.Fill;
                f.Show();
                Refresh();
            }

            Refresh();
        }
         

        private void addNewServerToolStripMenuItem_Click(object sender, EventArgs e) {
            var guid = Guid.NewGuid();
            var frm = new FrmServerTypeSelection(); 
            frm.AddNewTabPage += newServer => {
                var nCtrl = Global.plugins[newServer.serversType.GameType].Clone(null /* ServerUtils.InstallServerClick*/,
                                                                                                null /*ServerUtils.BackupServerClick */,
                                                                                               null /*ServerUtils.StopServerClick   */, 
                                                                                               null /*ServerUtils.StartServerClick  */, 
                                                                                               null /*ServerUtils.SaveServerClick   */, 
                                                                                               null /*ServerUtils.ReladoServerClick */, 
                                                                                               null /*ServerUtils.SyncServerClick   */, 
                                                                                               null /*ServerUtils.OpenRCONClick     */,
                                                                                               null /*ServerUtils.ChooseFolderClick */,
                                                                                               TabHeadChangeEvent); 
                
                nCtrl.SetInstallationPath(newServer.installDir);

                Global.serverControllers.Add(nCtrl.GetProfile().Key, nCtrl);
                 
                AddNewFormConfiguration(nCtrl);
            };
            frm.ShowDialog();
        }

        public  void TabHeadChangeEvent(object sender, OphiussaEventArgs e) {
             
            e.Plugin.TabPage.ImageIndex = !e.Plugin.IsInstalled ? 0 : e.Plugin.IsRunning ? 2 : 1;
             
        }
    }
}