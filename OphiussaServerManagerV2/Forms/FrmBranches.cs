using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OphiussaFramework;
using OphiussaFramework.CommonUtils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace OphiussaServerManagerV2 {
    public partial class FrmBranches : Form {
        private BindingList<Branches> branches;
        public FrmBranches() {
            InitializeComponent();
        }

        private void FrmBranches_Load(object sender, EventArgs e) {
            LoadBranchesGrid();
        }

        private void LoadBranchesGrid() {
            branches                 = ConnectionController.SqlLite.GetRecordsB<Branches>();
            foreach (var branch in branches) {
                branch.OldCode = branch.Code;
            }
            dataGridView1.DataSource = branches;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
                switch (col.Name) {
                    case "Id":
                        col.HeaderText = "Id";
                        col.ReadOnly   = true;
                        break;
                    case "Code":
                        col.HeaderText = "Code";
                        col.ReadOnly   = false;
                        break;
                    case "Name":
                        col.HeaderText = "Name";
                        col.ReadOnly   = false;
                        break; 
                    default:
                        col.Visible = false;
                        break;
                }

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btDelete_Click(object sender, EventArgs e) {

            try {
                foreach (DataGridViewRow selectedRow in dataGridView1.SelectedRows)
                    try {
                        var obj = branches[selectedRow.Index];
                         
                        if (!ConnectionController.SqlLite.CanDelete<Branches>(obj)) throw new Exception("Branch in Use");

                        ConnectionController.SqlLite.Delete<Branches>(obj.Id.ToString());
                        branches.RemoveAt(selectedRow.Index);

                        MessageBox.Show("Branch removed!");
                    }
                    catch (Exception exception) {
                        Console.WriteLine(exception);
                        MessageBox.Show(exception.Message);
                    }

                LoadBranchesGrid();
            }
            catch (Exception exception) {
                OphiussaLogger.Logger.Error(exception);
                MessageBox.Show(exception.Message);
            }
        }

        private void btSave_Click(object sender, EventArgs e) {
        }
         
        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e) {


            var obj = e.Row.DataBoundItem;


            if (!ConnectionController.SqlLite.CanDelete<Branches>(obj)) {
                MessageBox.Show("Branch in Use"); 
                e.Cancel = true;
                return;
            }

            ConnectionController.SqlLite.Delete<Branches>(((Branches)obj).Id.ToString());
            ConnectionController.LoadBranches();
            MessageBox.Show("Branch removed!");
        }

        private void FrmBranches_FormClosing(object sender, FormClosingEventArgs e) {

            foreach (var branch in branches) {
                ConnectionController.SqlLite.Upsert<Branches>(branch);
            }
            ConnectionController.LoadBranches();
            MessageBox.Show("Saved!");
        }
    }
}
