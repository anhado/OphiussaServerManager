using OphiussaServerManager.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OphiussaServerManager
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Array.IndexOf(args, "-monitor") >= 0)
            {
                Application.Run(new FrmServerMonitor());
            }
            else
            {
                Application.Run(new MainForm());
            }

        }
    }
}
