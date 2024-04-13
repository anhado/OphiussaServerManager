using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;

namespace OphiussaFramework.Components {
    public partial class ProfileHeader : UserControl {
        public ProfileHeader() {
            InitializeComponent();
        }


        private void timerGetProcess_Tick(object sender, EventArgs e) {
            try {
                if (Profile == null) return;
                timerGetProcess.Enabled = false;
                btStart.Text            = Plugin.IsRunning ? "Stop" : "Start";
                btUpdate.Enabled        = !Plugin.IsRunning;
                btRCON.Enabled          = Plugin.IsRunning && RconEnabled;

                if (Plugin.IsInstalled != _isInstalled || Plugin.IsRunning != _isRunning) {
                    CheckInstallStatus();
                    Plugin.TabHeaderChange();
                }
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
            }
            finally {
                timerGetProcess.Enabled = true;
            }
        }
         

        private void ProfileHeader_Load(object sender, EventArgs e) { 

            timerGetProcess.Enabled = true;

            if (Profile == null) return;

            cboBranch.DataSource    = ConnectionController.Branches;
            cboBranch.ValueMember   = "Code";
            cboBranch.DisplayMember = "Name";

            txtProfileID.DataBindings.Add("Text", Profile, "Key", true, DataSourceUpdateMode.OnPropertyChanged);
            txtLocation.DataBindings.Add("Text", Profile, "InstallationFolder", true, DataSourceUpdateMode.OnPropertyChanged);
            txtProfileName.DataBindings.Add("Text", Profile, "Name", true, DataSourceUpdateMode.OnPropertyChanged);
            txtServerType.DataBindings.Add("Text", Profile, "Type", true, DataSourceUpdateMode.OnPropertyChanged);
            txtBuild.DataBindings.Add("Text", Profile, "ServerBuildVersion", true, DataSourceUpdateMode.OnPropertyChanged);
            txtVersion.DataBindings.Add("Text", Profile, "ServerVersion", true, DataSourceUpdateMode.OnPropertyChanged);
            cboBranch.DataBindings.Add("SelectedValue", Profile, "Branch", true, DataSourceUpdateMode.OnPropertyChanged);

            CheckInstallStatus();
        }

        private void CheckInstallStatus() {
            if (!Plugin.IsInstalled) btUpdate.Text = "Install";
            else btUpdate.Text                     = "Update/Verify";

            btStart.Enabled = Plugin.IsInstalled;
            _isInstalled    = Plugin.IsInstalled;
            _isRunning    = Plugin.IsRunning;
            txtVersion.Text = Plugin.GetVersion();
            txtBuild.Text   = Plugin.GetBuild();
            Plugin.TabHeaderChange();
        }

        private void btSync_Click(object sender, EventArgs e) {
            ClickSync?.Invoke(this, e);
        }

        private void btSave_Click(object sender, EventArgs e) {
            ClickSave?.Invoke(this, e);
        }

        private void btUpdate_Click(object sender, EventArgs e) {
            try { 
                ClickUpgrade?.Invoke(this, e);
            }
            catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
        }

        private void btStart_Click(object sender, EventArgs e) {
            ClickStartStop?.Invoke(this, e);
        }

        private void btRCON_Click(object sender, EventArgs e) {
            ClickRCON?.Invoke(this, e);
        }

        private void btReload_Click(object sender, EventArgs e) {
            ClickReload?.Invoke(this, e);
        }

        private void txtProfileName_Validated(object sender, EventArgs e) {
            Tab.Text = txtProfileName.Text;
        }

        private void btChooseFolder_Click(object sender, EventArgs e) {
            if (fBD.ShowDialog() == DialogResult.OK) {
                if (Plugin != null && Plugin.IsValidFolder(fBD.SelectedPath)) {
                    txtLocation.Text = fBD.SelectedPath;
                    ClickSelectFolder?.Invoke(this, e);
                }
                else {
                    MessageBox.Show("Invalid Folder!!");
                }
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
        private bool _isInstalled;
        private bool _isRunning;

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
            get => _RconEnabled;
            set {
                _RconEnabled   = value;
                btRCON.Enabled = _RconEnabled;
            }
        }

        #endregion

        private void cboBranch_Click(object sender, EventArgs e) {

            cboBranch.DataSource    = ConnectionController.Branches;
            cboBranch.ValueMember   = "Code";
            cboBranch.DisplayMember = "Name";
        }
    }
}