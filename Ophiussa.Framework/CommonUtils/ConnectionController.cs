using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OphiussaFramework.DataBaseUtils;
using OphiussaFramework.Models;

namespace OphiussaFramework {
    public static class ConnectionController {
        public static SqlLite           SqlLite  { get; set; }
        public static Settings          Settings { get; set; }
        public static Form              MainForm { get; internal set; }
         
        public static void Initialize() {
            SqlLite  = new SqlLite();
            Settings = SqlLite.GetSettings(); 
        }

        public static void SetMainForm(Form frm) {
            MainForm = frm;
        }
    }
}
