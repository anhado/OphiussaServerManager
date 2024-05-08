using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OphiussaFormBuilder.Models;

namespace OphiussaFormBuilder {
    public partial class FrmAnalyzedData : Form {

        public  Action<List<ObjectDefinition>> GenerateCode { get; set; }
        private List<ObjectDefinition>         AnalyzedList;

        public FrmAnalyzedData(List<ObjectDefinition> analyzedList) {
            InitializeComponent();

            List<CboTypes> lst = new List<CboTypes>();
            Enum.GetNames(typeof(ObjectType)).ToList().ForEach(e => {
                                                                   lst.Add(new CboTypes {
                                                                                            Code = e,
                                                                                            Name = e
                                                                                        });
                                                               });
            DataGridViewComboBoxColumn categoryColumn = (DataGridViewComboBoxColumn)dataGridView1.Columns["objectTypeDataGridViewTextBoxColumn"];
            categoryColumn.DataSource    = lst;
            categoryColumn.ValueMember   = "Code";
            categoryColumn.DisplayMember = "Name";
            AnalyzedList                 = analyzedList;
            dataGridView1.DataSource     = AnalyzedList;
            dataGridView1.AutoResizeColumns(); 
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e) {
            Console.WriteLine(e.Exception.Message);
        }

        private void FrmAnalyzedData_Load(object sender, EventArgs e) {

        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e) {
            dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            dataGridView1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e) {
            GenerateCode.Invoke(AnalyzedList);
        }
    }
}
