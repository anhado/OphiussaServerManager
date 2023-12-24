using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Management;
using System.Net;
using System.Windows.Forms;

namespace OphiussaServerManagerUpdater {
    public partial class FrmUpdater : Form {
        public static string FieldCommandline    = "CommandLine";
        public static string FieldExecutablepath = "ExecutablePath";
        public static string FieldProcessid      = "ProcessId";

        public FrmUpdater() {
            InitializeComponent();
        }

        private void frmUpdater_Load(object sender, EventArgs e) {
            KillProcessRunning(AppContext.BaseDirectory + "OphiussaServerManager.exe");
            progressBar1.Maximum = 5;
            progressBar1.Value   = 0;
            using (var client = new WebClient()) {
                client.DownloadFile("https://www.ophiussa.eu/OSM/latest.txt", "latest.txt");
            }

            progressBar1.Value++;
            string lastversion = File.ReadAllText("latest.txt");

            var    versionInfo    = FileVersionInfo.GetVersionInfo("OphiussaServerManager.exe");
            string currentVersion = versionInfo.FileVersion;

            var version1 = new Version(lastversion);
            var version2 = new Version(currentVersion);
            int result   = version1.CompareTo(version2);
            progressBar1.Value++;
            if (result > 0) {
                using (var client = new WebClient()) {
                    client.DownloadFile("https://www.ophiussa.eu/OSM/latest.zip", "latest.zip");
                }

                progressBar1.Value++;


                using (var archive = ZipFile.OpenRead("latest.zip")) {
                    progressBar1.Maximum = archive.Entries.Count + 4;
                    foreach (var entry in archive.Entries) {
                        entry.ExtractToFile(Path.Combine(".\\", entry.FullName), true);
                        progressBar1.Value++;
                    }
                }

                progressBar1.Value++;
            }
        }

        public static string NormalizePath(string path) {
            return Path.GetFullPath(new Uri(path).LocalPath).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar).ToLowerInvariant();
        }

        public static string GetMainModuleFilepath(int processId) {
            using (var managementObjectSearcher = new ManagementObjectSearcher(string.Format("SELECT {0} FROM Win32_Process WHERE {1} = {2}", FieldExecutablepath, FieldProcessid, processId))) {
                using (var source = managementObjectSearcher.Get()) {
                    var managementObject = source.Cast<ManagementObject>().FirstOrDefault();
                    if (managementObject != null)
                        return (string)managementObject[FieldExecutablepath];
                }
            }

            return null;
        }

        public static void KillProcessRunning(string executablePath) {
            string processeName = Path.GetFileNameWithoutExtension(executablePath);
            string clientFile   = executablePath;
            if (string.IsNullOrWhiteSpace(clientFile) || !File.Exists(clientFile)) return;
            string a               = NormalizePath(clientFile);
            var    processesByName = Process.GetProcessesByName(processeName);
            foreach (var process in processesByName) {
                string mainModuleFilepath = GetMainModuleFilepath(process.Id);
                if (string.Equals(a, mainModuleFilepath, StringComparison.OrdinalIgnoreCase)) process.Kill();
            }
        }

        public static void ExecuteAsAdmin(string exeName, string parameters, bool wait = true, bool noWindow = false, bool dontRunAsAdmin = false) {
            try {
                var startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.FileName        = exeName;
                if (!dontRunAsAdmin) startInfo.Verb = "runas";

                //MLHIDE
                startInfo.Arguments = parameters;
                if (noWindow) {
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow  = noWindow;
                }

                startInfo.ErrorDialog = true;

                var process = Process.Start(startInfo);
                process.PriorityClass = ProcessPriorityClass.Normal;
                if (wait) process.WaitForExit();
            }
            catch (Win32Exception ex) {
                throw new Exception("ExecuteAsAdmin:" + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            ExecuteAsAdmin("OphiussaServerManager.exe", "", false);
            Close();
        }
    }
}