using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OphiussaFramework.Components {
    public partial class ProfileHeader : UserControl {
        public ProfileHeader() {
            InitializeComponent();
        }
         

        private void timerGetProcess_Tick(object sender, EventArgs e) {
            try {
                if (Profile == null) return;
                timerGetProcess.Enabled = false;
                btStart.Text            = IsRunning ? "Stop" : "Start";
                btUpdate.Enabled        = !IsRunning;
                btRCON.Enabled          = IsRunning && RconEnabled;

                if (Plugin.IsInstalled != _isInstalled) {
                    CheckInstallStatus();
                } 

                Plugin.TabHeaderChange(); 
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
            }
            finally {
                timerGetProcess.Enabled = true;
            }
        }

        #region Events
          
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks Reload Button")]
        public event EventHandler ClickReload;

        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks Sync Button")]
        public event EventHandler ClickSync;

        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks Save Button")]
        public event EventHandler ClickSave;

        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks Upgrade Button")]
        public event EventHandler ClickUpgrade;

        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks Start Stop Button")]
        public event EventHandler ClickStartStop;

        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks RCON Button")]
        public event EventHandler ClickRCON;

        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks Select Folder Button")]
        public event EventHandler ClickSelectFolder;

        #endregion

        #region Properties

        private int  _processId   = -1;
        private bool _RconEnabled = true;
        private bool _isInstalled = false;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(true)]
        public TabPage Tab { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(true)]
        public bool IsRunning { get; set; }
         
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(true)]
        public IProfile Profile { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(true)]
        public IPlugin Plugin { get; set; }

        [Category("Appearance")]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)] 
        public bool RconEnabled {
            get { return _RconEnabled; }
            set {
                _RconEnabled   = value;
                btRCON.Enabled = _RconEnabled;
            }
        }

        #endregion
         
        private void IsRunningProcess() {
            while (true) {
                if (Plugin == null) continue;
                Process process = null;

                if(Tab.ImageIndex == 0)  continue; 

                if (_processId == -1)
                    process = Plugin.GetExeProcess();
                else
                    try {
                        process = Process.GetProcessById(_processId);
                    }
                    catch (Exception) {
                        // ignored
                    }

                if (process != null) {
                    _processId = process.Id;
                    IsRunning  = true;
                }
                else {
                    _processId = -1;
                    IsRunning  = false;
                }

                if (!Utils.IsFormRunning("MainForm"))
                    break;
                Thread.Sleep(timerGetProcess.Interval);
            }
        }

        private void ProfileHeader_Load(object sender, EventArgs e) {


            var thread = new Thread(IsRunningProcess);
            thread.Start();

            this.timerGetProcess.Enabled = true;

            if (Profile == null) return;

            txtProfileID.DataBindings.Add("Text", Profile, "Key");
            txtLocation.DataBindings.Add("Text", Profile, "InstallationFolder");
            txtProfileName.DataBindings.Add("Text", Profile, "Name");
            txtServerType.DataBindings.Add("Text", Profile, "Type");
            txtBuild.DataBindings.Add("Text", Profile, "ServerBuildVersion");
            txtVersion.DataBindings.Add("Text", Profile, "ServerVersion");

            
        }

        private void CheckInstallStatus() { 
            if (!Plugin.IsInstalled) {
                btUpdate.Text = "Install";
                _isInstalled  = false;
            }
            else {
                if (Plugin.IsValidFolder(txtLocation.Text)) {
                    btUpdate.Text = "Update/Verify";
                    _isInstalled  = true;
                }
                else {
                    btUpdate.Text = "Install";
                    _isInstalled  = false;
                }
            }
            btStart.Enabled = _isInstalled;

            txtVersion.Text = Plugin.GetVersion();
            txtBuild.Text   = Plugin.GetBuild();
        }

        private void btSync_Click(object sender, EventArgs e) {
            ClickSync?.Invoke(this, e);
        }

        private void btSave_Click(object sender, EventArgs e) {
            ClickSave?.Invoke(this, e);
        }

        private void btUpdate_Click(object sender, EventArgs e) {
            ClickUpgrade?.Invoke(this, e);
        }

        private void btStart_Click(object sender, EventArgs e) {
            ClickStartStop?.Invoke(this, e);
        }

        private void btRCON_Click(object sender, EventArgs e) {
            ClickRCON?.Invoke(this, e);
        } 
        private void btReload_Click(object sender, EventArgs e) {
            ClickReload?.Invoke(this,e);
        }

        private void txtProfileName_Validated(object sender, EventArgs e) {
            Tab.Text = txtProfileName.Text;
        }

        private void btChooseFolder_Click(object sender, EventArgs e) {
            if (fBD.ShowDialog() == DialogResult.OK) {
                if (Plugin !=null && Plugin.IsValidFolder(fBD.SelectedPath)) {
                    txtLocation.Text = fBD.SelectedPath;
                    ClickSelectFolder?.Invoke(this, e);
                }
                else {
                    MessageBox.Show("Invalid Folder!!");
                }
            }
        }
    }
}