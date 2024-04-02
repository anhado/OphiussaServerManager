using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using OphiussaFramework;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;

namespace OphiussaServerManager.Components {
    public partial class AutomaticManagement : UserControl {
        private BindingList<AutoManagement> restartOptions;

        public AutomaticManagement() {
            InitializeComponent();
        }

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

        private void chkAutoStart_CheckedChanged(object sender, EventArgs e) {
            rbOnBoot.Enabled  = chkAutoStart.Checked;
            rbOnLogin.Enabled = chkAutoStart.Checked;
        }

        private void AutomaticManagement_Load(object sender, EventArgs e) {
            if (Profile == null) return;

            rbOnBoot.Checked  = Profile.StartOnBoot;
            rbOnLogin.Checked = !Profile.StartOnBoot;

            chkAutoStart.DataBindings.Add("Checked", Profile, "AutoStartServer");
            chkIncludeAutoBackup.DataBindings.Add("Checked", Profile, "IncludeAutoBackup");
            chkAutoUpdate.DataBindings.Add("Checked", Profile, "IncludeAutoUpdate");
            chkRestartIfShutdown.DataBindings.Add("Checked", Profile, "RestartIfShutdown");

            LoadGrid();
        }

        public void LoadGrid() {
            restartOptions = ConnectionController.SqlLite.GetRecordsB<AutoManagement>($"ServerKey ='{Profile.Key}'");

            foreach (var autoManagement in restartOptions)
                try {
                    int hour   = short.Parse(autoManagement.ShutdownHour.Split(':')[0]);
                    int minute = short.Parse(autoManagement.ShutdownHour.Split(':')[1]);
                    autoManagement.ShutdownHourDt = new DateTime(2000, 1, 1, hour, minute, 0);
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                    autoManagement.ShutdownHourDt = new DateTime(2000, 1, 1, 0, 0, 0);
                }

            dataGridView1.DataSource = restartOptions;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                switch (col.Name) {
                    case "Id":
                        col.HeaderText = "Id";
                        col.ReadOnly   = true;
                        break;
                    case "ShutdownHour":
                        col.HeaderText = "Shutdown Hour (24H)";
                        col.ReadOnly   = false;
                        col.Visible    = false;
                        break;
                    case "ShutdownHourDt":
                        col.HeaderText              = "Shutdown Hour (24H)";
                        col.ReadOnly                = false;
                        col.DefaultCellStyle.Format = "HH:mm";
                        break;
                    case "ShutdownSun":
                        col.HeaderText = "Sunday";
                        col.ReadOnly   = false;
                        break;
                    case "ShutdownMon":
                        col.HeaderText = "Monday";
                        col.ReadOnly   = false;
                        break;
                    case "ShutdownTue":
                        col.HeaderText = "Tuesday";
                        col.ReadOnly   = false;
                        break;
                    case "ShutdownWed":
                        col.HeaderText = "Wednesday";
                        col.ReadOnly   = false;
                        break;
                    case "ShutdownThu":
                        col.HeaderText = "Thursday";
                        col.ReadOnly   = false;
                        break;
                    case "ShutdownFri":
                        col.HeaderText = "Friday";
                        col.ReadOnly   = false;
                        break;
                    case "ShutdownSat":
                        col.HeaderText = "Saturday";
                        col.ReadOnly   = false;
                        break;
                    case "UpdateServer":
                        col.HeaderText = "Update Server";
                        col.ReadOnly   = false;
                        break;
                    case "RestartServer":
                        col.HeaderText = "Restart Server";
                        col.ReadOnly   = false;
                        break;
                    default:
                        col.Visible = false;
                        break;
                }

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btAdd_Click(object sender, EventArgs e) {
            restartOptions.AddNew();
        }

        private void btDelete_Click(object sender, EventArgs e) {
            try {
                if (MessageBox.Show("Want to delete this record?", "RECORD DELETE", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
                foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
                    try {
                        var obj = restartOptions[selectedRow.Index];
                        ConnectionController.SqlLite.Delete<AutoManagement>(obj.Id.ToString());
                        restartOptions.RemoveAt(selectedRow.Index);

                        string taskName = $"OphiussaServerManager\\AutoShutDown_{obj.Id:000}_" + Plugin.Profile.Key;
                        var    task     = TaskService.Instance.GetTask(taskName);
                        if (task != null) TaskService.Instance.RootFolder.DeleteTask(taskName);
                    }
                    catch (Exception exception) {
                        Console.WriteLine(exception);
                        MessageBox.Show(exception.Message);
                    }

                LoadGrid();
            }
            catch (Exception exception) {
                OphiussaLogger.Logger.Error(exception);
                MessageBox.Show(exception.Message);
            }
        }

        public List<AutoManagement> GetRestartSettings() {
            var ret = new List<AutoManagement>();
            foreach (var autoManagement in restartOptions) ret.Add(autoManagement);
            return ret;
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e) {
            MessageBox.Show(e.Exception.Message);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            if (e.RowIndex >= 0) restartOptions[e.RowIndex].ShutdownHour = restartOptions[e.RowIndex].ShutdownHourDt.ToString("HH:mm");
        }
    }
}