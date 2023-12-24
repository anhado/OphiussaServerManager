using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Ini;

namespace OphiussaServerManager.Common {
    public static class Utils {
        public const string DefaultCultureCode = "en-US";

        private static readonly Guid _localLowId = new Guid("A520A1A4-1780-4FF6-BD18-167343C5AF16");

        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr hWnd);

        [DllImport("shell32.dll")]
        private static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr pszPath);

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

        public static bool IsAValidFolder(string initialFolder, List<string> folderList, bool isFiles = false) {
            var folders  = Directory.GetDirectories(initialFolder).ToList();
            var onlyLast = new List<string>();

            folders.ForEach(folder => { onlyLast.Add(new DirectoryInfo(folder).Name); });


            var notExists = folderList.FindAll(x => !onlyLast.Contains(x)).ToList();


            if (notExists.Count == 0) return true;
            return false;
        }

        public static string GetPropertyValue(object value, PropertyInfo property, bool quotedString = true) {
            return !(property.PropertyType == typeof(float))
                       ? !(property.PropertyType == typeof(string)) ? Convert.ToString(value, CultureInfo.GetCultureInfo("en-US")) : !quotedString ? string.Format("{0}", value) : string.Format("\"{0}\"", value)
                       : ((float)value).ToString("0.000000####", CultureInfo.GetCultureInfo("en-US"));
        }

        public static string GetPropertyValue(
            object                    value,
            PropertyInfo              property,
            BaseIniFileEntryAttribute attribute) {
            string propertyValue;
            if (property.PropertyType == typeof(int) || property.PropertyType == typeof(NullableValue<int>)) {
                propertyValue = Convert.ToString(value, CultureInfo.GetCultureInfo("en-US"));
            }
            else if (property.PropertyType == typeof(long) || property.PropertyType == typeof(NullableValue<long>)) {
                propertyValue = Convert.ToString(value, CultureInfo.GetCultureInfo("en-US"));
            }
            else if (property.PropertyType == typeof(float) || property.PropertyType == typeof(NullableValue<float>)) {
                propertyValue = ((float)value).ToString("0.000000####", CultureInfo.GetCultureInfo("en-US"));
            }
            else if (property.PropertyType == typeof(bool)) {
                bool flag = (bool)value;
                if (attribute.InvertBoolean)
                    flag = !flag;
                propertyValue = !attribute.WriteBooleanAsInteger ? flag.ToString(CultureInfo.GetCultureInfo("en-US")) : flag ? "1" : "0";
            }
            else {
                propertyValue = Convert.ToString(value, CultureInfo.GetCultureInfo("en-US"));
            }

            return propertyValue;
        }

        public static void SetPropertyValue(string value, object obj, PropertyInfo property) {
            if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?)) {
                bool result;
                bool.TryParse(value, out result);
                property.SetValue(obj, result);
            }
            else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?)) {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s                = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                int result;
                int.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out result);
                property.SetValue(obj, result);
            }
            else if (property.PropertyType == typeof(long) || property.PropertyType == typeof(long?)) {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s                = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                long result;
                long.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out result);
                property.SetValue(obj, result);
            }
            else if (property.PropertyType == typeof(float) || property.PropertyType == typeof(float?)) {
                float result;
                float.TryParse(value.Replace("f", ""), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out result);
                property.SetValue(obj, result);
            }
            else if (property.PropertyType == typeof(NullableValue<int>)) {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s                = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                int result;
                int.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out result);
                var nullableValue = property.GetValue(obj) as NullableValue<int>;
                property.SetValue(obj, nullableValue.SetValue(true, result));
            }
            else if (property.PropertyType == typeof(NullableValue<long>)) {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s                = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                long result;
                long.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out result);
                var nullableValue = property.GetValue(obj) as NullableValue<long>;
                property.SetValue(obj, nullableValue.SetValue(true, result));
            }
            else if (property.PropertyType == typeof(NullableValue<float>)) {
                float result;
                float.TryParse(value.Replace("f", ""), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out result);
                var nullableValue = property.GetValue(obj) as NullableValue<float>;
                property.SetValue(obj, nullableValue.SetValue(true, result));
            }
            else if (property.PropertyType.IsSubclassOf(typeof(AggregateIniValue))) {
                if (!(property.GetValue(obj) is AggregateIniValue aggregateIniValue))
                    return;
                aggregateIniValue.InitializeFromIniValue(value);
            }
            else {
                object obj1 = Convert.ChangeType(value, property.PropertyType, CultureInfo.GetCultureInfo("en-US"));
                if (obj1 is string)
                    obj1 = (obj1 as string).Trim('"');
                property.SetValue(obj, obj1);
            }
        }

        public static bool SetPropertyValue(
            string                    value,
            object                    obj,
            PropertyInfo              property,
            BaseIniFileEntryAttribute attribute) {
            if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?)) {
                bool result;
                if (attribute.WriteBooleanAsInteger)
                    result = value.Equals("1", StringComparison.OrdinalIgnoreCase);
                else
                    bool.TryParse(value, out result);
                if (attribute.InvertBoolean)
                    result = !result;
                property.SetValue(obj, result);
                return true;
            }

            if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?)) {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s                = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                int result;
                int.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out result);
                property.SetValue(obj, result);
                return true;
            }

            if (property.PropertyType == typeof(long) || property.PropertyType == typeof(long?)) {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s                = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                long result;
                long.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out result);
                property.SetValue(obj, result);
                return true;
            }

            if (property.PropertyType == typeof(float) || property.PropertyType == typeof(float?)) {
                float result;
                float.TryParse(value.Replace("f", ""), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out result);
                property.SetValue(obj, result);
                return true;
            }

            if (property.PropertyType == typeof(NullableValue<int>)) {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s                = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                int result;
                int.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out result);
                var nullableValue = property.GetValue(obj) as NullableValue<int>;
                property.SetValue(obj, nullableValue.SetValue(true, result));
                return true;
            }

            if (property.PropertyType == typeof(NullableValue<long>)) {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s                = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                long result;
                long.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out result);
                var nullableValue = property.GetValue(obj) as NullableValue<long>;
                property.SetValue(obj, nullableValue.SetValue(true, result));
                return true;
            }

            if (property.PropertyType == typeof(NullableValue<float>)) {
                float result;
                float.TryParse(value.Replace("f", ""), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out result);
                var nullableValue = property.GetValue(obj) as NullableValue<float>;
                property.SetValue(obj, nullableValue.SetValue(true, result));
                return true;
            }

            if (!property.PropertyType.IsSubclassOf(typeof(AggregateIniValue)))
                return false;
            if (property.GetValue(obj) is AggregateIniValue aggregateIniValue)
                aggregateIniValue.InitializeFromIniValue(value);
            return true;
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

        public static string CalculateMd5(string filename) {
            if (filename == null) return "";
            using (var md5 = MD5.Create()) {
                using (var stream = File.OpenRead(filename)) {
                    //var hash = md5.ComputeHash(stream);
                    //return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    return Encoding.Default.GetString(md5.ComputeHash(stream));
                }
            }
        }

        public static Process GetProcessRunning(string executablePath) {
            string processeName = Path.GetFileNameWithoutExtension(executablePath);
            string clientFile   = executablePath;
            if (string.IsNullOrWhiteSpace(clientFile) || !File.Exists(clientFile))
                return null;
            string  a               = IoUtils.NormalizePath(clientFile);
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

        public static int GetProcessorCount() {
            return Environment.ProcessorCount;
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

        public static void SendCloseCommand(Process process) {
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
    }
}