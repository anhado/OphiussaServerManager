using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.Models;
using Message = OphiussaFramework.Models.Message;

namespace OphiussaServerManagerV2
{
    public partial class FrmPluginManager : Form
    {
        BindingList<PluginInfo> plugins;

        public FrmPluginManager()
        {
            InitializeComponent();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            var res = fDiag.ShowDialog();
            if (res == DialogResult.OK)
            {
                try
                {
                    //TODO: validar se já existe o plugin ou algum plugin com o mesmo server type;

                    System.IO.File.Copy(fDiag.FileName, Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "plugins\\temp\\") + Path.GetFileName(fDiag.FileName), true);
                    PluginController ctrl = new PluginController(Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "plugins\\temp\\") + Path.GetFileName(fDiag.FileName));

                    System.IO.File.Copy(Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "plugins\\temp\\") + Path.GetFileName(fDiag.FileName), Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "plugins\\") + Path.GetFileName(fDiag.FileName), true);
                    Global.SqlLite.UpsertPlugin(ctrl);
                    LoadPluginsGrid();
                }
                catch (Exception exception)
                { 
                    OphiussaLogger.Logger.Error(exception);
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void FrmPluginManager_Load(object sender, EventArgs e)
        { 

            LoadPluginsGrid();
        }

        private void LoadPluginsGrid()
        {
            plugins                  = Global.SqlLite.GetPluginInfoListB();
            dataGridView1.DataSource = plugins;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                switch (col.Name)
                {
                    case "PluginName":
                        col.HeaderText = "Plugin Name";
                        col.ReadOnly = true;
                        break;
                    case "GameType":
                        col.HeaderText = "Game Type";
                        col.ReadOnly = true;
                        break;
                    case "GameName":
                        col.HeaderText = "Game Name";
                        col.ReadOnly = true;
                        break;
                    case "Version":
                        col.HeaderText = "Version";
                        col.ReadOnly = true;
                        break;
                    case "Loaded":
                        col.HeaderText = "Load";
                        break;
                }

            }
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }

        private void btSave_Click(object sender, EventArgs e)
        {
            foreach (PluginInfo plugin in plugins)
            {
                Global.SqlLite.UpsertPlugin(plugin);
            }

            MessageBox.Show("Saved");
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
                {
                    try
                    {
                        var obj = (PluginInfo)plugins[selectedRow.Index];
                        Global.SqlLite.DeletePlugin(obj.PluginName);
                        plugins.RemoveAt(selectedRow.Index);

                        MessageBox.Show("PLEASE RESTART THE APPLICATION!!!!!");
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        MessageBox.Show(exception.Message);
                    }
                }

                LoadPluginsGrid();

            }
            catch (Exception exception)
            {
                OphiussaLogger.Logger.Error(exception);
                MessageBox.Show(exception.Message); 
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Loaded" && e.RowIndex >= 0)
            {
                int index = e.RowIndex;
                var obj = (PluginInfo)plugins[index];
                Global.SqlLite.UpsertPlugin(obj);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.EndEdit();
        }

        private void FrmPluginManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.LoadPlugins();
        }
    }
}
