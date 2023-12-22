using Newtonsoft.Json;
using OphiussaServerManager.Forms;
using OphiussaServerManager.Tools;
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
            try
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
                    ServerTools.BackupAllServer();
                }
                else if (Array.IndexOf(args, "-notifications") >= 0)
                {
                    OphiussaLogger.ReconfigureLogging();
                    NotificationController notificationController = new NotificationController();
                    notificationController.StartServer();
                }
                else
                {
                    foreach (string arg in args)
                    {
                        if (arg.StartsWith("-as1"))
                        {
                            OphiussaLogger.ReconfigureLogging();
                            ServerTools.UpdateSingleServerJob1(arg.Substring(4));
                            return;
                        }
                        if (arg.StartsWith("-as2"))
                        {
                            OphiussaLogger.ReconfigureLogging();
                            ServerTools.UpdateSingleServerJob2(arg.Substring(4));
                            return;
                        }
                    }

                    if (!System.IO.File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")))
                    {
                        Application.Run(new FrmStartScreen());
                        if (System.IO.File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json"))) Application.Run(new MainForm());
                    }
                    else
                    {
                        Application.Run(new MainForm());
                    }
                }

            }
            catch (Exception ex)
            {
                string tmpFile = Path.GetTempFileName();
                if (!File.Exists(tmpFile)) File.Create(tmpFile);
                File.AppendAllText(tmpFile, "error:" + ex.Message);
            }
        }
    }
}
