using Newtonsoft.Json;
using OphiussaServerManager.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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
            else if (Array.IndexOf(args, "-au") >= 0)
            {
                ServerTools.UpdateServer();
            }
            else if (Array.IndexOf(args, "-ab") >= 0)
            { 
                ServerTools.BackupServer();
            }
            else
            {
                Application.Run(new MainForm());
            }

        }
    }
}
