using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OphiussaFramework;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.Enums;
using OphiussaFramework.Extensions;
using OphiussaFramework.Models;

namespace OphiussaServerManagerV2 {
    public partial class FrmServerMonitor : Form {

        List<ServerMonitor> servers = new List<ServerMonitor>();

        public FrmServerMonitor() {
            InitializeComponent();
        }

        private void FrmServerMonitor_Load(object sender, EventArgs e) {
            LoadServer();
        }

        private void LoadServer() {
            foreach (string key in ConnectionController.ServerControllers.Keys) {
                servers.Add(new ServerMonitor(ConnectionController.ServerControllers[key], ServerStatusChanged));
            }

            dataGridView1.DataSource = servers;

            FormatGrid();
        }

        private void ServerStatusChanged(object sender, ServerEventArgs e) {

            int index = servers.IndexOf(servers.Find(s => s.ProfileName == e.Profile.Name));
            if (index >= 0) {
                foreach (DataGridViewCell cell in dataGridView1.Rows[index].Cells) {
                    Color color = Color.LightSkyBlue;
                    switch (e.Status) {
                        case ServerStatus.Running:
                            color = Color.LightGreen; break;
                        case ServerStatus.Stopped:
                            color = Color.LightCoral; break;
                    }

                    cell.Style.BackColor = color;
                }
            }


            dataGridView1.Refresh();
        }

        private void FormatGrid() {

            foreach (DataGridViewColumn col in dataGridView1.Columns) {
                switch (col.Name) {
                    case "Controller":
                        col.Visible = false; break;
                    case "ProfileName":
                        col.HeaderText = "Profile";
                        col.ReadOnly = true;
                        break;
                    case "Selected":
                        col.ReadOnly = false; break;
                    default:
                        col.ReadOnly = true;
                        break;
                }
            }
            dataGridView1.AutoResizeColumns();
            foreach (DataGridViewRow r in dataGridView1.Rows) {
                ServerMonitor server = servers[r.Index];

                foreach (DataGridViewCell cell in r.Cells) {
                    Color color = Color.LightSkyBlue;
                    switch (server.Controller.ServerStatus) {
                        case ServerStatus.Running:
                            color = Color.LightGreen; break;
                        case ServerStatus.Stopped:
                            color = Color.LightCoral; break;
                    }

                    cell.Style.BackColor = color;
                }

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {

        }
        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) {
            try {
                if (e.RowIndex < 0) return;
                if (e.ColumnIndex < 0) return;

                if (dataGridView1.Columns[e.ColumnIndex].Name == "Restart") {
                    int index = e.RowIndex;
                    ServerMonitor obj = (ServerMonitor)servers[index];

                    if (obj.Controller.IsRunning) {

                        await obj.Controller.StopServer();

                        var startDate = DateTime.Now;
                        while (obj.Controller.IsRunning) {
                            var ts = DateTime.Now - startDate;
                            if (ts.TotalMinutes > 5) await obj.Controller.StopServer(true);
                            Thread.Sleep(5000);
                        }
                    }

                    obj.Controller.StartServer();
                    await Task.Delay(1000);
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Stop") {
                    int index = e.RowIndex;
                    ServerMonitor obj = (ServerMonitor)servers[index];
                    if (!obj.Controller.IsRunning) return;
                    await obj.Controller.StopServer(false);

                    var startDate = DateTime.Now;
                    while (obj.Controller.IsRunning) {
                        var ts = DateTime.Now - startDate;
                        if (ts.TotalMinutes > 5) await obj.Controller.StopServer(true);
                        Thread.Sleep(5000);
                    }
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Backup") {
                    int index = e.RowIndex;
                    ServerMonitor obj = (ServerMonitor)servers[index];
                    obj.Controller.BackupServer();
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Rcon") {
                    int index = e.RowIndex;
                    ServerMonitor obj = (ServerMonitor)servers[index];
                    obj.Controller.OpenRCON();
                }

            }
            catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            tbTimerSeconds.Visible = chkSequencial.Checked;
        }

        private async void btStartStop_Click(object sender, EventArgs e) {


            var tasks = new List<Task>();
            foreach (var server in servers) {
                var t = Task.Run(() => { RestartServer(server.Controller,true); });

                tasks.Add(t);
                if (chkSequencial.Checked) await Task.Delay(tbTimerSeconds.Value.ToInt() * 1000);
            }

            Task.WaitAll(tasks.ToArray());

        }

        private async void RestartServer(PluginController controller, bool DontStart = false) {
            if (controller.IsRunning) {
                await controller.StopServer();

                var startDate = DateTime.Now;
                while (controller.IsRunning) {
                    var ts = DateTime.Now - startDate;
                    if (ts.TotalMinutes > 5) await controller.StopServer(true);
                    Thread.Sleep(5000);
                }
            }

            if(!DontStart) controller.StartServer(); 
        }

        private async void btRestart_Click(object sender, EventArgs e) {

            var tasks = new List<Task>();
            foreach (var server in servers) {
                var t = Task.Run(() => { RestartServer(server.Controller); });

                tasks.Add(t);
                if (chkSequencial.Checked) await Task.Delay(tbTimerSeconds.Value.ToInt() * 1000);
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}
