using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using OphiussaFramework.Enums;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;

namespace OphiussaFramework.CommonUtils {
    public class Utils {
        private static readonly Guid _localLowId = new Guid("A520A1A4-1780-4FF6-BD18-167343C5AF16");

        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr hWnd);

        [DllImport("shell32.dll")]
        private static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr pszPath);


        public static Versions CompareVersion(Version oldVersion, Version NewVersion) {
            int result = oldVersion.CompareTo(NewVersion);
            if (result > 0) return Versions.Lower;
            if (result < 0) return Versions.Greater;
            return Versions.Equal;
        }

        public static int GetProcessorCount() {
            return Environment.ProcessorCount;
        }

        public static Process GetProcessRunning(string executablePath) {
            string processeName = Path.GetFileNameWithoutExtension(executablePath);
            string clientFile   = executablePath;
            if (string.IsNullOrWhiteSpace(clientFile) || !File.Exists(clientFile))
                return null;
            string  a               = IOUtils.NormalizePath(clientFile);
            var     processesByName = Process.GetProcessesByName(processeName);
            Process processInfo     = null;
            foreach (var process in processesByName) {
                string mainModuleFilepath = ProcessUtils.GetMainModuleFilepath(process.Id);
                if (string.Equals(a, mainModuleFilepath, StringComparison.OrdinalIgnoreCase)) {
                    processInfo = process;
                    break;
                }
            }

            return processInfo;
        }

        internal static bool IsFormRunning(string formName) {
            var fc = Application.OpenForms;

            foreach (Form frm in fc)
                //iterate through
                if (frm.Name == formName)
                    return true;
            return false;
        }

        public static bool IsAValidFolder(string initialFolder, List<string> folderList, bool isFiles = false) {
            //TODO: validate if is files instead of folders
            if (!Directory.Exists(initialFolder)) return false;

            var folders  = Directory.GetDirectories(initialFolder).ToList();
            var onlyLast = new List<string>();

            folders.ForEach(folder => { onlyLast.Add(new DirectoryInfo(folder).Name); });


            var notExists = folderList.FindAll(x => !onlyLast.Contains(x)).ToList();


            if (notExists.Count == 0) return true;
            return false;
        }

        public static void ExecuteAsAdmin(string exeName, string parameters, bool wait = true, bool noWindow = false, bool dontRunAsAdmin = false) {
            try {
                Thread standardOutputThread = null;
                Thread standardErrorThread  = null;

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


        public static string BinaryStringToHexString(string binary) {
            if (string.IsNullOrEmpty(binary))
                return binary;

            var result = new StringBuilder(binary.Length / 8 + 1);

            if (!Isbin(binary)) throw new Exception("the string is not binary");

            int mod4Len = binary.Length % 8;
            if (mod4Len != 0)
                // pad to length multiple of 8
                binary = binary.PadLeft((binary.Length / 8 + 1) * 8, '0');

            for (int i = 0; i < binary.Length; i += 8) {
                string eightBits = binary.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            return result.ToString();

            bool Isbin(string s) {
                foreach (char c in s)
                    if (c != '0' && c != '1')
                        return false;
                return true;
            }
        }

        public static void SendCloseCommandCtrlC(Process process) {
            SetForegroundWindow(process.MainWindowHandle);
            SendKeys.SendWait("^(c)");
        }

        public static string GetLocalLowFolderPath() {
            var knownFolderId = _localLowId;
            var pszPath       = IntPtr.Zero;
            try {
                int hr = SHGetKnownFolderPath(knownFolderId, 0, IntPtr.Zero, out pszPath);
                if (hr >= 0)
                    return Marshal.PtrToStringAuto(pszPath);
                throw Marshal.GetExceptionForHR(hr);
            }
            finally {
                if (pszPath != IntPtr.Zero)
                    Marshal.FreeCoTaskMem(pszPath);
            }
        }

        public static string GetCpuAffinity(string cpuAffinity, List<ProcessorAffinity> cpuAffinityList) {
            var lst = new List<ProcessorAffinity>();

            for (int i = GetProcessorCount() - 1; i >= 0; i--)
                lst.Add(
                        new ProcessorAffinity {
                                                  ProcessorNumber = i,
                                                  Selected        = cpuAffinity == "All" ? true : cpuAffinityList.DefaultIfEmpty(new ProcessorAffinity { Selected = true, ProcessorNumber = i }).FirstOrDefault(x => x.ProcessorNumber == i).Selected
                                              }
                       );

            string bin = string.Join("", lst.Select(x => x.Selected ? "1" : "0"));
            string hex = !bin.Contains("0") ? "" : "0" + BinaryStringToHexString(bin);
            return hex;
        }

        public static string GetBuild(IProfile profile) {
            string fileName = $"appmanifest_{profile.SteamServerId}.acf";
            if (!File.Exists(Path.Combine(profile.InstallationFolder, "steamapps", fileName))) return "";

            string[] content = File.ReadAllText(Path.Combine(profile.InstallationFolder, "steamapps", fileName)).Split('\n');

            foreach (string item in content) {
                string[] t = item.Split('\t');

                if (item.Contains("buildid")) return t[3].Replace("\"", "");
            }

            return File.ReadAllText(Path.Combine(profile.InstallationFolder, "steamapps", fileName));
        }

        public static string GetVersion(IProfile profile) {
            if (!File.Exists(Path.Combine(profile.InstallationFolder, "version.txt"))) return "";

            return File.ReadAllText(Path.Combine(profile.InstallationFolder, "version.txt"));
        }

        public static string GetCacheBuild(IProfile profile, string cacheFolder) {
            string fileName    = $"appmanifest_{profile.SteamServerId}.acf"; 
            if (!File.Exists(Path.Combine(cacheFolder, "steamapps", fileName))) return "";

            string[] content = File.ReadAllText(Path.Combine(cacheFolder, "steamapps", fileName)).Split('\n');

            foreach (string item in content) {
                string[] t = item.Split('\t');

                if (item.Contains("buildid")) return t[3].Replace("\"", "");
            }

            return File.ReadAllText(Path.Combine(cacheFolder, "steamapps", fileName));
        }

        public static List<FileInfo> CompareFolderContent(string pathA, string pathB, List<string> ignorePaths) {
            // Create two identical or different temporary folders
            // on a local drive and change these file paths.  
            var sw = new Stopwatch();

            sw.Start();
            var dir1 = new DirectoryInfo(pathA);
            var dir2 = new DirectoryInfo(pathB);

            // Take a snapshot of the file system.  
            IEnumerable<FileInfo> list1 = dir1.GetFiles("*.*", SearchOption.AllDirectories);
            IEnumerable<FileInfo> list2 = dir2.GetFiles("*.*", SearchOption.AllDirectories);

            var cacheFilesList   = list2.ToList();
            var installFilesList = list1.ToList();

            Console.WriteLine("Elapsed={0}", sw.Elapsed);
            sw.Restart();

            Console.WriteLine("Elapsed={0}", sw.Elapsed);
            sw.Restart();

            var changedFiles = cacheFilesList.FindAll(f => CompareFiles(f, installFilesList.Find(f2 => f2.Name == f.Name)) != null);

            Console.WriteLine("Elapsed={0}", sw.Elapsed);
            sw.Restart();

            FileInfo CompareFiles(FileInfo f1, FileInfo f2) {
                if (f2 == null) return f1;
                if (ignorePaths.Count > 0) {
                    string pathName = Path.GetDirectoryName(f1.FullName);
                    foreach (string item in ignorePaths)
                        if (pathName.Contains(item))
                            return null;
                }

                //if (f1.LastWriteTime >= f2.LastWriteTime) return f1;
                //else return null;


                var x = new ReadFileInChunksAndCompareVector(f1.FullName, f2.FullName, Vector<byte>.Count);
                if (!x.Compare()) return f1;
                return null;
            }

            sw.Stop();
            Console.WriteLine("Elapsed={0}", sw.Elapsed);
            return changedFiles;
        }
    }
}