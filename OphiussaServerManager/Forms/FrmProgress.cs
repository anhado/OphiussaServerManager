using System.Windows.Forms;

namespace OphiussaServerManager.Forms {
    public partial class FrmProgress : Form {
        private int _min = 0;
        private int _max = 100;

        public FrmProgress(string title, int min = 0, int max = 100) {
            _min      = min;
            _max      = max;
            this.Text = title;
            InitializeComponent();
            progressBar1.Maximum = _max;
            progressBar1.Minimum = _min;
        }

        public void SetProgress(string label) {
            progressBar1.Value++;
            label1.Text        = label;
            this.Refresh();
            label1.Refresh();
            progressBar1.Refresh();
        }

        public void AddToMaxValue(int value) {
            progressBar1.Maximum = _max + value;
        }

        public void CloseProgress() {
            this.Close();
        }
    }
}