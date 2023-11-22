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
                OphiussaLogger.ReconfigureLogging();
                ServerTools.UpdateAllServer();
            }
            else if (Array.IndexOf(args, "-ab") >= 0)
            {
                OphiussaLogger.ReconfigureLogging();
                ServerTools.BackupServer();
            } 
            else
            { 
                foreach (string arg in args)
                {
                    if (arg.StartsWith("-as"))
                    {
                        OphiussaLogger.ReconfigureLogging();
                        ServerTools.RestartSingleServer(arg.Substring(3));
                        return;
                    }
                }

                Application.Run(new MainForm());
            }

        }
    }
}
