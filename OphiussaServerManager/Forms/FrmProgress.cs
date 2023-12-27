using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Tools.Update;

namespace OphiussaServerManager.Forms {
    public partial class FrmProgress : Form {
        private readonly ConcurrentQueue<ProcessEventArg> _myQueue = new ConcurrentQueue<ProcessEventArg>();
        private          bool                             _isUpdating;

        public FrmProgress(Settings settings, Profile profile) {
            Settings = settings;
            Profile  = profile;
            InitializeComponent();
        }

        private Settings Settings { get; set; }
        private Profile  Profile  { get; }

        private void OnProgressChanged(object sender, ProcessEventArg e) {
            _myQueue.Enqueue(e);
            //WriteText(e, Color.Orange);
            //richTextBox1.AppendTextWithTimeStamp(e.Message, Color.Orange);
            //richTextBox1.SelectionStart = richTextBox1.Text.Length;
            //richTextBox1.ScrollToCaret();
            //richTextBox1.Refresh();
        }

        private void OnProcessCompleted(object sender, ProcessEventArg e) {
            _myQueue.Enqueue(e);
            //richTextBox1.AppendTextWithTimeStamp(e.Message, Color.Green);
            //richTextBox1.SelectionStart = richTextBox1.Text.Length;
            //richTextBox1.ScrollToCaret();
            //richTextBox1.Refresh();
        }

        private void OnProcessStarted(object sender, ProcessEventArg e) {
            _myQueue.Enqueue(e);
            //richTextBox1.AppendTextWithTimeStamp(e.Message, Color.Green);
            //richTextBox1.SelectionStart = richTextBox1.Text.Length;
            //richTextBox1.ScrollToCaret();
            //richTextBox1.Refresh();
        }

        private void OnProcessError(object sender, ProcessEventArg e) {
            _myQueue.Enqueue(e);
            //richTextBox1.AppendTextWithTimeStamp(e.Message, Color.Green);
            //richTextBox1.SelectionStart = richTextBox1.Text.Length;
            //richTextBox1.ScrollToCaret();
            //richTextBox1.Refresh();
        }

        private void btUpdate_Click(object sender, EventArgs e) {
            if (_isUpdating) return;
            _isUpdating = true;
            var autoUpdate = new AutoUpdate();
            autoUpdate.ProcessStarted   += OnProcessStarted;
            autoUpdate.ProcessError     += OnProcessError;
            autoUpdate.ProgressChanged  += OnProgressChanged;
            autoUpdate.ProcessCompleted += OnProcessCompleted;
            Task.Factory.StartNew(_ => {
                                      autoUpdate.UpdateSingleServerManually(Profile.Key, chkUpdateCache.Checked, chkStartServer.Checked, chkForceUpdateMods.Checked);
                                      _isUpdating = false;
                                  }, null);
        }

        private void btClose_Click(object sender, EventArgs e) {
            Close();
        }

        private void timer_updateBox_Tick(object sender, EventArgs e) {
            try {
                panel1.Enabled          = !_isUpdating;
                timer_updateBox.Enabled = false;
                foreach (var item in _myQueue)
                    if (_myQueue.TryDequeue(out var item2)) {
                        richTextBox1.AppendTextWithTimeStamp(item2.Message, !item2.IsError ? item2.Sucessful ? Color.Green : Color.Orange : Color.Red);
                        richTextBox1.SelectionStart = richTextBox1.Text.Length;
                        richTextBox1.ScrollToCaret();
                        richTextBox1.Refresh();
                    }
            }
            catch (Exception) {
            }

            timer_updateBox.Enabled = true;
        }

        private void FrmProgress_FormClosing(object sender, FormClosingEventArgs e) {
            if (_isUpdating) e.Cancel = true;
        }
    }
}