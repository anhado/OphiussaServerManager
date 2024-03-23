using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using OphiussaFramework;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.Models;

namespace OphiussaServerManagerV2 {
    public partial class FrmPluginManager : Form {
        private BindingList<PluginInfo> plugins;

        public FrmPluginManager() {
            InitializeComponent();
        }

        private void btAdd_Click(object sender, EventArgs e) {
            var res = fDiag.ShowDialog();
            if (res == DialogResult.OK)
                try {
                    //TODO: validar se já existe o plugin ou algum plugin com o mesmo server type;
                    File.Copy(fDiag.FileName, Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "plugins\\temp\\") + Path.GetFileName(fDiag.FileName), true);
                    var ctrl = new PluginController(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "plugins\\temp\\") + Path.GetFileName(fDiag.FileName));

                    File.Copy(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "plugins\\temp\\") + Path.GetFileName(fDiag.FileName), Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "plugins\\") + Path.GetFileName(fDiag.FileName), true);
                    ConnectionController.SqlLite.UpsertPlugin(ctrl);
                    LoadPluginsGrid();
                }
                catch (Exception exception) {
                    OphiussaLogger.Logger.Error(exception);
                    MessageBox.Show(exception.Message);
                }
        }

        private void FrmPluginManager_Load(object sender, EventArgs e) {
            LoadPluginsGrid();
        }

        private void LoadPluginsGrid() {
            plugins                  = ConnectionController.SqlLite.GetPluginInfoListB();
            dataGridView1.DataSource = plugins;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                switch (col.Name) {
                    case "PluginName":
                        col.HeaderText = "Plugin Name";
                        col.ReadOnly   = true;
                        break;
                    case "GameType":
                        col.HeaderText = "Game Type";
                        col.ReadOnly   = true;
                        break;
                    case "GameName":
                        col.HeaderText = "Game Name";
                        col.ReadOnly   = true;
                        break;
                    case "Version":
                        col.HeaderText = "Version";
                        col.ReadOnly   = true;
                        break;
                    case "Loaded":
                        col.HeaderText = "Load";
                        break;
                }

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btSave_Click(object sender, EventArgs e) {
            foreach (var plugin in plugins) ConnectionController.SqlLite.UpsertPlugin(plugin);

            MessageBox.Show("Saved");
        }

        private void btDelete_Click(object sender, EventArgs e) {
            try {
                foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
                    try {
                        var obj = plugins[selectedRow.Index];
                        ConnectionController.SqlLite.DeletePlugin(obj.PluginName);
                        plugins.RemoveAt(selectedRow.Index);

                        MessageBox.Show("PLEASE RESTART THE APPLICATION!!!!!");
                    }
                    catch (Exception exception) {
                        Console.WriteLine(exception);
                        MessageBox.Show(exception.Message);
                    }

                LoadPluginsGrid();
            }
            catch (Exception exception) {
                OphiussaLogger.Logger.Error(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) {
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Loaded" && e.RowIndex >= 0) {
                int index = e.RowIndex;
                var obj   = plugins[index];
                ConnectionController.SqlLite.UpsertPlugin(obj);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            dataGridView1.EndEdit();
        }

        private void FrmPluginManager_FormClosing(object sender, FormClosingEventArgs e) {
            Global.LoadPlugins();
        }
    }
}