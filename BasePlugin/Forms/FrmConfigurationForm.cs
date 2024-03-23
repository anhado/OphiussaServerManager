using System.Windows.Forms;
using OphiussaFramework.Interfaces;

namespace BasePlugin.Forms {
    public partial class FrmConfigurationForm : Form {
        private IPlugin _plugin;

        public FrmConfigurationForm(IPlugin plugin) {
            _plugin = plugin;
            InitializeComponent();
        }
    }
}