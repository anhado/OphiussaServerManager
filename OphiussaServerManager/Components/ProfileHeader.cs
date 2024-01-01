using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Tools;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OphiussaServerManager.Components {
    public partial class ProfileHeader : UserControl {
        public ProfileHeader() {
            InitializeComponent();
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
        public event EventHandler StopClickStart;

        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        [Category("Action")]
        [Description("Invoked when user clicks RCON Button")]
        public event EventHandler ClickRCON;


        #endregion

        #region Properties
        private Profile _profile = null;
        private TabPage _Tab { get; set; }
        private int _processId = -1;
        private bool _isInstalled = false;

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(true)]
        public bool IsRunning { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(true)]
        public bool RconEnabled { get; set; }
        #endregion

        private void timerGetProcess_Tick(object sender, EventArgs e) {
            try {
                if (_profile == null) return;
                timerGetProcess.Enabled = false;
                btStart.Text = IsRunning ? "Stop" : "Start";
                btUpdate.Enabled = !IsRunning;
                btRCON.Enabled = IsRunning && _profile.ArkConfiguration.UseRcon;

                RconEnabled = btRCON.Enabled;

                UsefullTools.MainForm.SetTabHeader(_Tab, _profile, IsRunning);
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
            }
            finally {
                timerGetProcess.Enabled = true;
            }
        }

        public void LoadData(ref TabPage tab, ref Profile profile) {
            _profile = profile;
            _Tab = tab;

            Stopwatch sw = new Stopwatch();

            sw.Start();
            UsefullTools.LoadValuesToFields(_profile, this.Controls);

            sw.Stop();

            Console.WriteLine("Elapsed={0}", sw.Elapsed.TotalSeconds);

            txtServerType.Text = profile.Type.ServerTypeDescription;

            if (!Directory.Exists(txtLocation.Text)) {
                btUpdate.Text = "Install";
                _isInstalled = false;
            }
            else {//TODO:FIX THIS DEPENDS ON SERVER TYPE
                if (Utils.IsAValidFolder(txtLocation.Text,
                                         new List<string> { "Engine", "ShooterGame", "steamapps" })) {
                    btUpdate.Text = "Update/Verify";
                    _isInstalled = true;
                }
                else {
                    btUpdate.Text = "Install";
                    _isInstalled = false;
                }
            }

            btStart.Enabled = _isInstalled;
            btRCON.Enabled = _isInstalled;

            txtVersion.Text = profile.GetVersion();
            txtBuild.Text = profile.GetBuild();

        }

        public void GetData(ref Profile profile) {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            UsefullTools.LoadFieldsToObject(ref _profile, this.Controls);

            sw.Stop();

            Console.WriteLine("Elapsed={0}", sw.Elapsed.TotalSeconds);
        }

        private void IsRunningProcess() {
            while (true) {
                if (_profile == null) continue;
                Process process = null;

                if (_processId == -1)
                    process = _profile.GetExeProcess();
                else
                    try {
                        process = Process.GetProcessById(_processId);
                    }
                    catch (Exception) {
                        // ignored
                    }

                if (process != null) {
                    _processId = process.Id;
                    IsRunning = true;
                }
                else {
                    _processId = -1;
                    IsRunning = false;
                }

                if (!UsefullTools.IsFormRunning("MainForm"))
                    break;
                Thread.Sleep(timerGetProcess.Interval);
            }
        }

        private void ProfileHeader_Load(object sender, EventArgs e) {
            var thread = new Thread(IsRunningProcess);
            thread.Start();
        }

        private void btChooseFolder_Click(object sender, EventArgs e) {
            if (!Utils.IsAValidFolder(txtLocation.Text,
                                      new List<string> { "Engine", "ShooterGame", "steamapps" }))
                MessageBox.Show("This is not a valid Ark Folder");
        }
         
        private void button5_Click(object sender, EventArgs e) {

            ClickReload?.Invoke(this, e);
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
            StopClickStart?.Invoke(this, e);
        }

        private void btRCON_Click(object sender, EventArgs e) {
            ClickRCON?.Invoke(this, e);
        }

        private void txtProfileName_Validated(object sender, EventArgs e) {

            _Tab.Text = txtProfileName.Text + "          ";
        }
    }
}
