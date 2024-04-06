using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OphiussaFramework.Extensions;
using OphiussaFramework.Models;

namespace OphiussaFramework.Forms {
    public partial class FrmProgress : Form {
        private readonly ConcurrentQueue<ProcessEventArg> _myQueue = new ConcurrentQueue<ProcessEventArg>();
        private          bool                             _isUpdating;
        private          PluginController                 Controller;

        public FrmProgress(PluginController _controller) {
            InitializeComponent();
            this.Text  = $"Install/Update Server ({_controller.GetProfile().Name})";
            Controller = _controller;
        }

        private void btClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btUpdate_Click(object sender, EventArgs e) {

            if (_isUpdating) return;
            _isUpdating = true;

            Task.Factory.StartNew(async _ => {
                                      await Controller.InstallServer();
                                      _isUpdating = false;
                                  }, null);
        } 
        private void OnProgressChanged(object sender, ProcessEventArg e) {
            _myQueue.Enqueue(e); 
        }

        private void OnProcessCompleted(object sender, ProcessEventArg e) {
            _myQueue.Enqueue(e); 
        }

        private void OnProcessStarted(object sender, ProcessEventArg e) {
            _myQueue.Enqueue(e); 
        }

        private void OnProcessError(object sender, ProcessEventArg e) {
            _myQueue.Enqueue(e); 
        }
         
        private void FrmProgress_Load(object sender, EventArgs e) {

            ServerUtils.ServerUtils.ProcessStarted   += OnProcessStarted;
            ServerUtils.ServerUtils.ProcessError     += OnProcessError;
            ServerUtils.ServerUtils.ProgressChanged  += OnProgressChanged;
            ServerUtils.ServerUtils.ProcessCompleted += OnProcessCompleted;
        }

        private void FrmProgress_FormClosing(object sender, FormClosingEventArgs e) {
            if (_isUpdating) e.Cancel = true;

            ServerUtils.ServerUtils.ProcessStarted   -= OnProcessStarted;
            ServerUtils.ServerUtils.ProcessError     -= OnProcessError;
            ServerUtils.ServerUtils.ProgressChanged  -= OnProgressChanged;
            ServerUtils.ServerUtils.ProcessCompleted -= OnProcessCompleted;
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
    }
}
