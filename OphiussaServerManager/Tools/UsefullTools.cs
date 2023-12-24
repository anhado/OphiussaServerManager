using System.Windows.Forms;

namespace OphiussaServerManager.Tools {
    internal static class UsefullTools {
        public static MainForm MainForm;

        internal static bool IsFormRunning(string formName) {
            var fc = Application.OpenForms;

            foreach (Form frm in fc)
                //iterate through
                if (frm.Name == formName)
                    return true;
            return false;
        }
    }
}