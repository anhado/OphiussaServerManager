using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OphiussaServerManager.Tools
{
    internal static class UsefullTools
    {
        public static MainForm MainForm;

        internal static bool isFormRunning(string formName)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                //iterate through
                if (frm.Name == formName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
