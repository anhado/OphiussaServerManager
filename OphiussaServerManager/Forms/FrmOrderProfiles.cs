using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;

namespace OphiussaServerManager.Forms {
    public partial class FrmOrderProfiles : Form {
        public FrmOrderProfiles() {
            InitializeComponent();
        }

        private void FrmOrderProfiles_Load(object sender, EventArgs e) {
        }

        public void LoadProfiles(Settings settings) {
            profileOrderGridBindingSource.Clear();
            try {
                string dir = settings.DataFolder + "Profiles\\";
                if (!Directory.Exists(dir)) return;

                string[] files = Directory.GetFiles(dir);
                if (settings.ProfileOrders.Count > 0)
                    foreach (var profileOrder in settings.ProfileOrders.OrderBy(x => x.Order)) {
                        string file = files.First(f => f.Contains(profileOrder.Key));
                        if (!string.IsNullOrEmpty(file)) {
                            var p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(file));
                            if (p != null)
                                profileOrderGridBindingSource.Add(new ProfileOrderGrid {
                                                                                           Order       = profileOrder.Order,
                                                                                           Key         = profileOrder.Key,
                                                                                           ProfileName = p.Name
                                                                                       });
                        }

                        files = files.Where(x => x != file).ToArray();
                    }

                if (files.Length > 0) {
                    int i = 1;
                    foreach (string file in files) {
                        var p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(file));
                        if (p != null)
                            profileOrderGridBindingSource.Add(new ProfileOrderGrid {
                                                                                       Order       = i,
                                                                                       Key         = p.Key,
                                                                                       ProfileName = p.Name
                                                                                   });
                        i++;
                    }
                }
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                MessageBox.Show($"LoadProfiles: {e.Message}");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) {
            try {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "clUp") {
                    int index = e.RowIndex;

                    dataGridView1.ClearSelection();
                    if (index == 0) return; //can go down
                    object tempKey = profileOrderGridBindingSource[index];
                    profileOrderGridBindingSource.RemoveAt(index);
                    profileOrderGridBindingSource.Insert(index - 1, tempKey);
                    dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex - 1].Cells[e.ColumnIndex];
                    ResetOrder();
                }

                if (dataGridView1.Columns[e.ColumnIndex].Name == "clDown") {
                    int index = e.RowIndex;

                    dataGridView1.ClearSelection();
                    if (index == dataGridView1.Rows.Count - 1) return; //can go down
                    object tempKey = profileOrderGridBindingSource[index];
                    profileOrderGridBindingSource.RemoveAt(index);
                    profileOrderGridBindingSource.Insert(index + 1, tempKey);
                    dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex + 1].Cells[e.ColumnIndex];
                    ResetOrder();
                }
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void ResetOrder() {
            int i = 1;
            foreach (ProfileOrderGrid item in profileOrderGridBindingSource) {
                item.Order = i;
                i++;
            }
        }

        private void button4_Click(object sender, EventArgs e) {
            LoadProfiles(MainForm.Settings);
        }

        private void button1_Click(object sender, EventArgs e) {
            MainForm.Settings.ProfileOrders.Clear();
            foreach (ProfileOrderGrid item in profileOrderGridBindingSource)
                MainForm.Settings.ProfileOrders.Add(new ProfileOrder {
                                                                         Order = item.Order,
                                                                         Key   = item.Key
                                                                     });
            MainForm.Settings.SaveSettings();
            MessageBox.Show("Restart application to apply the Tab order.");
            Close();
        }
    }
}