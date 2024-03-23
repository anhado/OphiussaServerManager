using OphiussaFramework.CommonUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OphiussaServerManagerV2
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


            OphiussaLogger.ReconfigureLogging();

            OphiussaLogger.Logger.Info($"Application Started. Params -> {string.Join(",", args)}");
            if (!System.IO.Directory.Exists("plugins")) System.IO.Directory.CreateDirectory("plugins");
            if (!System.IO.Directory.Exists("plugins\\temp")) System.IO.Directory.CreateDirectory("plugins\\temp");

            Global.Initialize();

            if (Global.Settings == null)
            {
                Application.Run(new FrmDriveSelection());
            }
            else
            {
                Application.Run(new MainForm());
            }
        }
    }
}
