using System;
using System.IO;
using System.Windows.Forms;
using OphiussaFramework;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;
using OphiussaFramework.ServerUtils;

namespace OphiussaServerManagerV2 {
    internal static class Program {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main() {

            try {

                string[] args = Environment.GetCommandLineArgs();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (!Directory.Exists("plugins")) Directory.CreateDirectory("plugins");
                if (!Directory.Exists("plugins\\temp")) Directory.CreateDirectory("plugins\\temp"); 

                ConnectionController.Initialize(); 
                OphiussaLogger.ReconfigureLogging(); 

                OphiussaLogger.Logger.Info("\nApplication Started");

                foreach (string arg in args) {
                    if (arg.StartsWith("-as")) {
                        //OphiussaLogger.ReconfigureLogging();
                        ServerUtils.RestartServerSingleServer(arg);
                        return;
                    }
                } 


                if (ConnectionController.Settings == null)
                    Application.Run(new FrmDriveSelection());
                else
                    Application.Run(new MainForm());
            }
            catch (Exception ex) {
                string tmpFile = Path.GetTempFileName();
                if (!File.Exists(tmpFile)) File.Create(tmpFile);

                File.AppendAllText(tmpFile, "\nerror:" + ex.Message);
                File.AppendAllText(tmpFile, "\nStackTrace:" + ex.StackTrace);
            }
        }
    }
}