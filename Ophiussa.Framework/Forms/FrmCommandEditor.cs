using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.Models;

namespace OphiussaFramework.Forms {
    internal partial class FrmCommandEditor : Form {
        private readonly List<CommandDefinition> _commands = new List<CommandDefinition>();

        internal FrmCommandEditor(List<CommandDefinition> commands) {
            InitializeComponent();
            _commands = commands;
            LoadCommands();
        }

        internal Action<List<CommandDefinition>> BuildCommand { get; set; }

        private void LoadCommands() {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _commands;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                switch (col.Name) {
                    case "Order":
                        col.HeaderText = "Order";
                        break;
                    case "AddSpaceInPrefix":
                        col.HeaderText = "Add Space In Prefix";
                        break;
                    case "NamePrefix":
                        col.HeaderText = "Name Prefix";
                        break;
                    case "Name":
                        col.HeaderText = "Name";
                        break;
                    case "ValuePrefix":
                        col.HeaderText = "Value Prefix";
                        break;
                    case "Value":
                        col.HeaderText = "Value";
                        break;
                    case "Enabled":
                        col.HeaderText = "Enabled";
                        break;
                }

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btSave_Click(object sender, EventArgs e) {
            BuildCommand.Invoke(_commands);
            Close();
        }

        private void btAdd_Click(object sender, EventArgs e) {
            _commands.Add(new CommandDefinition {
                                                    Order   = _commands.Count + 1,
                                                    Name    = "New",
                                                    Enabled = false
                                                });
            LoadCommands();
        }

        private void btDelete_Click(object sender, EventArgs e) {
            try {
                foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
                    try {
                        _commands.RemoveAt(selectedRow.Index);
                    }
                    catch (Exception exception) {
                        Console.WriteLine(exception);
                        MessageBox.Show(exception.Message);
                    }

                LoadCommands();
            }
            catch (Exception exception) {
                OphiussaLogger.Logger.Error(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void btPreview_Click(object sender, EventArgs e) {
            var cmdBuilder = new CommandBuilder(_commands);
            var Frm        = new FrmCommandPreview(cmdBuilder.ToString());
            Frm.ShowDialog();
        }
    }
}