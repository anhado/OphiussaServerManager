using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Tools.Update;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace OphiussaServerManager.Forms
{
    public partial class FrmProgress : Form
    {
        ConcurrentQueue<ProcessEventArg> myQueue = new ConcurrentQueue<ProcessEventArg>();
        private Settings settings { get; set; }
        private Profile profile { get; set; }

        private bool IsUpdating = false;

        public FrmProgress(Settings settings, Profile profile)
        {
            this.settings = settings;
            this.profile = profile;
            InitializeComponent();

        }

        private void OnProgressChanged(object sender, ProcessEventArg e)
        {
            myQueue.Enqueue(e);
            //WriteText(e, Color.Orange);
            //richTextBox1.AppendTextWithTimeStamp(e.Message, Color.Orange);
            //richTextBox1.SelectionStart = richTextBox1.Text.Length;
            //richTextBox1.ScrollToCaret();
            //richTextBox1.Refresh();

        }
        private void OnProcessCompleted(object sender, ProcessEventArg e)
        {
            myQueue.Enqueue(e);
            //richTextBox1.AppendTextWithTimeStamp(e.Message, Color.Green);
            //richTextBox1.SelectionStart = richTextBox1.Text.Length;
            //richTextBox1.ScrollToCaret();
            //richTextBox1.Refresh();
        }
        private void OnProcessStarted(object sender, ProcessEventArg e)
        {
            myQueue.Enqueue(e);
            //richTextBox1.AppendTextWithTimeStamp(e.Message, Color.Green);
            //richTextBox1.SelectionStart = richTextBox1.Text.Length;
            //richTextBox1.ScrollToCaret();
            //richTextBox1.Refresh();
        }
        private void OnProcessError(object sender, ProcessEventArg e)
        {
            myQueue.Enqueue(e);
            //richTextBox1.AppendTextWithTimeStamp(e.Message, Color.Green);
            //richTextBox1.SelectionStart = richTextBox1.Text.Length;
            //richTextBox1.ScrollToCaret();
            //richTextBox1.Refresh();
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            if (IsUpdating) { return; }
            IsUpdating = true;
            AutoUpdate autoUpdate = new AutoUpdate();
            autoUpdate.ProcessStarted += OnProcessStarted;
            autoUpdate.ProcessError += OnProcessError;
            autoUpdate.ProgressChanged += OnProgressChanged;
            autoUpdate.ProcessCompleted += OnProcessCompleted;
            Task.Factory.StartNew(_ =>
            {
                autoUpdate.UpdateSingleServerManually(profile.Key, chkUpdateCache.Checked, chkStartServer.Checked);
                IsUpdating = false;
            }, null);

        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer_updateBox_Tick(object sender, EventArgs e)
        {
            try
            {
                panel1.Enabled = !IsUpdating; 
                timer_updateBox.Enabled = false;
                foreach (var item in myQueue)
                {
                    if (myQueue.TryDequeue(out var item2))
                    {
                        richTextBox1.AppendTextWithTimeStamp(item2.Message, (!item2.isError ? (item2.Sucessful ? Color.Green : Color.Orange) : Color.Red));
                        richTextBox1.SelectionStart = richTextBox1.Text.Length;
                        richTextBox1.ScrollToCaret();
                        richTextBox1.Refresh();
                    }
                }

            }
            catch (Exception)
            {

            }
            timer_updateBox.Enabled = true;
        }

        private void FrmProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsUpdating) e.Cancel = true;
        }
    }
}
