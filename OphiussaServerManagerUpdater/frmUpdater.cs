using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using OphiussaServerManagerUpdater.Properties;
using System.Globalization;
using System.Management;

namespace OphiussaServerManagerUpdater
{
    public partial class frmUpdater : Form
    {
        public frmUpdater()
        {
            InitializeComponent();
        }

        private void frmUpdater_Load(object sender, EventArgs e)
        {
            KillProcessRunning(AppContext.BaseDirectory + "OphiussaServerManager.exe");
            progressBar1.Maximum = 5;
            progressBar1.Value = 0;
            using (var client = new WebClient())
            {
                client.DownloadFile("https://www.ophiussa.eu/OSM/latest.txt", "latest.txt");
            }
            progressBar1.Value++;
            string lastversion = File.ReadAllText("latest.txt");

            var versionInfo = FileVersionInfo.GetVersionInfo("OphiussaServerManager.exe");
            string currentVersion = versionInfo.FileVersion;

            var version1 = new Version(lastversion);
            var version2 = new Version(currentVersion);
            var result = version1.CompareTo(version2);
            progressBar1.Value++;
            if (result > 0)
            {
                using (var client = new WebClient())
                {
                    client.DownloadFile("https://www.ophiussa.eu/OSM/latest.zip", "latest.zip");
                }
                progressBar1.Value++;


                using (ZipArchive archive = ZipFile.OpenRead("latest.zip"))
                {
                    progressBar1.Maximum = archive.Entries.Count + 4;
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {

                        entry.ExtractToFile(Path.Combine(".\\", entry.FullName), true);
                        progressBar1.Value++;
                    }
                }

                progressBar1.Value++;
            }

        }

        public static string NormalizePath(string path) => Path.GetFullPath(new Uri(path).LocalPath).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar).ToLowerInvariant();

        public static string FIELD_COMMANDLINE = "CommandLine";
        public static string FIELD_EXECUTABLEPATH = "ExecutablePath";
        public static string FIELD_PROCESSID = "ProcessId";

        public static string GetMainModuleFilepath(int processId)
        {
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(string.Format("SELECT {0} FROM Win32_Process WHERE {1} = {2}", (object)FIELD_EXECUTABLEPATH, (object)FIELD_PROCESSID, (object)processId)))
            {
                using (ManagementObjectCollection source = managementObjectSearcher.Get())
                {
                    ManagementObject managementObject = source.Cast<ManagementObject>().FirstOrDefault<ManagementObject>();
                    if (managementObject != null)
                        return (string)managementObject[FIELD_EXECUTABLEPATH];
                }
            }
            return (string)null;
        }

        public static void KillProcessRunning(string ExecutablePath)
        {
            string ProcesseName = Path.GetFileNameWithoutExtension(ExecutablePath);
            string ClientFile = ExecutablePath;
            if (string.IsNullOrWhiteSpace(ClientFile) || !System.IO.File.Exists(ClientFile)) return;
            string a = NormalizePath(ClientFile);
            Process[] processesByName = Process.GetProcessesByName(ProcesseName);
            foreach (Process process in processesByName)
            {
                string mainModuleFilepath = GetMainModuleFilepath(process.Id);
                if (string.Equals(a, mainModuleFilepath, StringComparison.OrdinalIgnoreCase))
                {
                    process.Kill();

                }
            }
        }

        public static void ExecuteAsAdmin(string exeName, string parameters, bool wait = true, bool noWindow = false, bool dontRunAsAdmin = false)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.FileName = exeName;
                if (!dontRunAsAdmin) startInfo.Verb = "runas";

                //MLHIDE
                startInfo.Arguments = parameters;
                if (noWindow)
                {
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = noWindow;
                }
                startInfo.ErrorDialog = true;

                Process process = System.Diagnostics.Process.Start(startInfo);
                process.PriorityClass = ProcessPriorityClass.Normal;
                if (wait) process.WaitForExit();
            }
            catch (Win32Exception ex)
            {
                throw new Exception("ExecuteAsAdmin:" + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExecuteAsAdmin("OphiussaServerManager.exe", "", false, false);
            this.Close();
        }
    }
}
