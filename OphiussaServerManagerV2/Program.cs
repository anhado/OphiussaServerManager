using System;
using System.IO;
using System.Windows.Forms;
using OphiussaFramework.CommonUtils;

namespace OphiussaServerManagerV2 {
    internal static class Program {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main() {
            string[] args = Environment.GetCommandLineArgs();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            OphiussaLogger.ReconfigureLogging();

          //  OphiussaLogger.Logger.Info($"Application Started. Params -> {string.Join(",", args)}");
            if (!Directory.Exists("plugins")) Directory.CreateDirectory("plugins");
            if (!Directory.Exists("plugins\\temp")) Directory.CreateDirectory("plugins\\temp");

            Global.Initialize();

            if (Global.Settings == null)
                Application.Run(new FrmDriveSelection());
            else
                Application.Run(new MainForm());
        }
    }
}