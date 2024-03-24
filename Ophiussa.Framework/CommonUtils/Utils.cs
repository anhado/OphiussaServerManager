using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OphiussaFramework.Enums;

namespace OphiussaFramework.CommonUtils {
    public class Utils {
        public static Versions CompareVersion(Version oldVersion, Version NewVersion) {
            int result = oldVersion.CompareTo(NewVersion);
            if (result > 0) return Versions.Lower;
            if (result < 0) return Versions.Greater;
            return Versions.Equal;
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
            
            if(!Directory.Exists(initialFolder)) return false;

            var folders  = Directory.GetDirectories(initialFolder).ToList();
            var onlyLast = new List<string>();

            folders.ForEach(folder => { onlyLast.Add(new DirectoryInfo(folder).Name); });


            var notExists = folderList.FindAll(x => !onlyLast.Contains(x)).ToList();


            if (notExists.Count == 0) return true;
            return false;
        }
    }
}