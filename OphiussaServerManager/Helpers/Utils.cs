using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OphiussaServerManager
{
    internal static class Utils
    {
        public static void ExecuteAsAdmin(string exeName, string parameters)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.FileName = exeName;
                startInfo.Verb = "runas";
                //MLHIDE
                startInfo.Arguments = parameters;
                startInfo.ErrorDialog = true;

                Process process = System.Diagnostics.Process.Start(startInfo);
                process.WaitForExit();
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show("ExecuteAsAdmin:" + ex.Message);
            }
        }

        public static bool IsAValidFolder(string InitialFolder, List<string> FolderList, bool isFiles = false)
        {
            List<string> folders = System.IO.Directory.GetDirectories(InitialFolder).ToList<string>();
            List<string> OnlyLast = new List<string>();

            folders.ForEach(folder => { OnlyLast.Add(new DirectoryInfo(folder).Name); });


            List<string> notExists = FolderList.FindAll(x => !OnlyLast.Contains(x)).ToList();


            if (notExists.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}
