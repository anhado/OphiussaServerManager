using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OphiussaFramework.Interfaces;

namespace OphiussaFramework.Models {
    public class LinkProfileForm {
        public IProfile Profile { get; set; }
        public Form    Form    { get; set; }
        public TabPage Tab     { get; set; }
    }
}
