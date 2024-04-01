using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
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
        private readonly Dictionary<string, LinkProfileForm> _linkProfileForms = new Dictionary<string, LinkProfileForm>();

        public MainForm() {
            InitializeComponent();
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
                if (ConnectionController.Settings.UpdateSteamCMDStart) NetworkTools.DownloadSteamCmd();

                try {
                    if (txtPublicIP.Text == "") txtPublicIP.Text = await System.Threading.Tasks.Task.Run(() => NetworkTools.DiscoverPublicIPAsync().Result);
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
            List<IProfile> lst = ConnectionController.SqlLite.GetRecords<IProfile>();

            if(lst == null) return;
            lst.ForEach(prf => {
                            if (!ConnectionController.Plugins.ContainsKey(prf.Type)) return;
                            PluginController nCtrl = new PluginController(ConnectionController.Plugins[prf.Type].PluginLocation(), null, null, null, null, null, null, null, null, null, TabHeadChangeEvent);
                            nCtrl.SetProfile(prf);
                            ConnectionController.ServerControllers.Add(nCtrl.GetProfile().Key, nCtrl);

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

            var form = controller.GetConfigurationForm(tab);
             
            Addform(tab, form);

            _linkProfileForms.Add(controller.GetProfile().Key, new LinkProfileForm { Form = form, Profile = controller.GetProfile(), Tab = tab });
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
                var nCtrl = ConnectionController.Plugins[newServer.serversType.GameType].Clone(null /* ServerUtils.InstallServerClick*/,
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

                ConnectionController.ServerControllers.Add(nCtrl.GetProfile().Key, nCtrl);
                 
                AddNewFormConfiguration(nCtrl);
            };
            frm.ShowDialog();
        }

        public  void TabHeadChangeEvent(object sender, OphiussaEventArgs e) {
             
            e.Plugin.TabPage.ImageIndex = !e.Plugin.IsInstalled ? 0 : e.Plugin.IsRunning ? 2 : 1;
             
        }

        private void tabControlExtra1_TabImageClick(object sender, TabControlEventArgs e) {
             
        }

        private void tabControlExtra1_TabClosing(object sender, TabControlCancelEventArgs e) {

            
            if (MessageBox.Show("Do you want to delete this profile?", "DELETE PROFILE", MessageBoxButtons.OKCancel) == DialogResult.OK) {

                foreach (string key in _linkProfileForms.Keys)
                    if (_linkProfileForms[key].Tab == e.TabPage) { 
                        ConnectionController.SqlLite.Delete<IProfile>(_linkProfileForms[key].Profile.Key);
                        foreach (AutoManagement am in _linkProfileForms[key].Profile.AutoManagement) { 
                            ConnectionController.SqlLite.Delete<AutoManagement>(am.Id.ToString());

                            string taskName = $"OphiussaServerManager\\AutoShutDown_{am.Id:000}_" + _linkProfileForms[key].Profile.Key;
                            var    task     = TaskService.Instance.GetTask(taskName);
                            if (task != null) TaskService.Instance.RootFolder.DeleteTask(taskName);
                        } 
                    }
            }
            else {
                e.Cancel = true;
            }
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void updateSteamCMDToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                NetworkTools.DownloadSteamCmd();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void createDesktopIconToolStripMenuItem_Click(object sender, EventArgs e) {

            var link = (IShellLink)new ShellLink();

            // setup shortcut information
            link.SetDescription("Ophiussa Server Manager");
            link.SetPath(Assembly.GetEntryAssembly().Location);

            // save it
            var    file        = (IPersistFile)link;
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            file.Save(Path.Combine(desktopPath, "Ophiussa Server Manager.lnk"), false);
        }

        private async void refreshPublicIPToolStripMenuItem_Click(object sender, EventArgs e) {

            try {
                if (txtPublicIP.Text == "") txtPublicIP.Text = await System.Threading.Tasks.Task.Run(() => NetworkTools.DiscoverPublicIPAsync().Result);
            }
            catch (Exception ex) {
                //  OphiussaLogger.Logger.Error(ex);
            }
        }

        private void refreshLocalIPToolStripMenuItem_Click(object sender, EventArgs e) {

            txtLocalIP.Text = NetworkTools.GetHostIp();
        }
    }
}