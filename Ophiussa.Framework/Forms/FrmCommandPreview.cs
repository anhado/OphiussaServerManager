using System;
using System.Windows.Forms;

namespace OphiussaFramework.Forms {
    public partial class FrmCommandPreview : Form {
        public FrmCommandPreview(string commmand) {
            InitializeComponent();
            textBox1.Text = commmand;
        }

        private void FrmCommandPreview_Load(object sender, EventArgs e) {
        }
    }
}