using OphiussaFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasePlugin.Forms
{
    public partial class FrmConfigurationForm : Form
    {
        private IPlugin _plugin;

        public FrmConfigurationForm(IPlugin plugin)
        {
            _plugin = plugin;
            InitializeComponent();
        }
    }
}
