using Newtonsoft.Json;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models.SupportedServers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Forms;

namespace OphiussaServerManager.Forms
{
    public partial class FrmOrderProfiles : Form
    {
        public FrmOrderProfiles()
        {
            InitializeComponent();
        }

        private void FrmOrderProfiles_Load(object sender, EventArgs e)
        {

        }

        public void LoadProfiles(Settings Settings)
        {
            profileOrderGridBindingSource.Clear();
            try
            {

                string dir = Settings.DataFolder + "Profiles\\";
                if (!Directory.Exists(dir))
                {
                    return;
                }

                string[] files = System.IO.Directory.GetFiles(dir);
                if (Settings.ProfileOrders.Count > 0)
                {
                    foreach (var profileOrder in Settings.ProfileOrders.OrderBy(x => x.Order))
                    {
                        string file = files.First(f => f.Contains(profileOrder.Key));
                        if (!string.IsNullOrEmpty(file))
                        {
                            Profile p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(file));
                            if (p != null)
                            {
                                profileOrderGridBindingSource.Add(new ProfileOrderGrid()
                                {
                                    Order = profileOrder.Order,
                                    Key = profileOrder.Key,
                                    ProfileName = p.Name,
                                });
                            }
                        }
                        files = files.Where(x => x != file).ToArray();
                    }
                }
                if (files.Length > 0)
                {
                    int i = 1;
                    foreach (string file in files)
                    {
                        Profile p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(file));
                        if (p != null)
                        {
                            profileOrderGridBindingSource.Add(new ProfileOrderGrid()
                            {
                                Order = i,
                                Key = p.Key,
                                ProfileName = p.Name,
                            });
                        }
                        i++;
                    }
                }
            }
            catch (Exception e)
            {
                OphiussaLogger.logger.Error(e);
                MessageBox.Show($"LoadProfiles: {e.Message}");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "clUp")
                {
                    int index = e.RowIndex;

                    this.dataGridView1.ClearSelection();
                    if (index == 0)
                    {
                        return;//can go down
                    }
                    var tempKey = this.profileOrderGridBindingSource[index];
                    this.profileOrderGridBindingSource.RemoveAt(index);
                    this.profileOrderGridBindingSource.Insert(index - 1, tempKey);
                    this.dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex - 1].Cells[e.ColumnIndex];
                    ResetOrder();
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "clDown")
                {

                    int index = e.RowIndex;

                    this.dataGridView1.ClearSelection();
                    if (index == this.dataGridView1.Rows.Count - 1)
                    {
                        return;//can go down
                    }
                    var tempKey = this.profileOrderGridBindingSource[index];
                    this.profileOrderGridBindingSource.RemoveAt(index);
                    this.profileOrderGridBindingSource.Insert(index + 1, tempKey);
                    this.dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex + 1].Cells[e.ColumnIndex];
                    ResetOrder();
                }
            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void ResetOrder()
        {
            int i = 1;
            foreach (ProfileOrderGrid item in profileOrderGridBindingSource)
            {
                item.Order = i;
                i++;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadProfiles(MainForm.Settings);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm.Settings.ProfileOrders.Clear();
            foreach (ProfileOrderGrid item in profileOrderGridBindingSource)
            {
                MainForm.Settings.ProfileOrders.Add(new ProfileOrder()
                {
                    Order = item.Order,
                    Key = item.Key
                });
            }
            MainForm.Settings.SaveSettings();
            MessageBox.Show("Restart application to apply the Tab order.");
            this.Close();
        }
    }
}
