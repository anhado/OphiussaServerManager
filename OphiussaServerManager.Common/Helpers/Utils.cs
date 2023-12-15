using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Ini;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace OphiussaServerManager.Common
{
    public static class Utils
    {
        public const string DEFAULT_CULTURE_CODE = "en-US";
        public static void ExecuteAsAdmin(string exeName, string parameters, bool wait = true, bool noWindow = false)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.FileName = exeName;
                startInfo.Verb = "runas";

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

        public static string GetPropertyValue(object value, PropertyInfo property, bool quotedString = true) => !(property.PropertyType == typeof(float)) ? (!(property.PropertyType == typeof(string)) ? Convert.ToString(value, (IFormatProvider)CultureInfo.GetCultureInfo("en-US")) : (!quotedString ? string.Format("{0}", value) : string.Format("\"{0}\"", value))) : ((float)value).ToString("0.000000####", (IFormatProvider)CultureInfo.GetCultureInfo("en-US"));

        public static string GetPropertyValue(
          object value,
          PropertyInfo property,
          BaseIniFileEntryAttribute attribute)
        {
            string propertyValue;
            if (property.PropertyType == typeof(int) || property.PropertyType == typeof(NullableValue<int>))
                propertyValue = Convert.ToString(value, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"));
            else if (property.PropertyType == typeof(long) || property.PropertyType == typeof(NullableValue<long>))
                propertyValue = Convert.ToString(value, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"));
            else if (property.PropertyType == typeof(float) || property.PropertyType == typeof(NullableValue<float>))
                propertyValue = ((float)value).ToString("0.000000####", (IFormatProvider)CultureInfo.GetCultureInfo("en-US"));
            else if (property.PropertyType == typeof(bool))
            {
                bool flag = (bool)value;
                if (attribute.InvertBoolean)
                    flag = !flag;
                propertyValue = !attribute.WriteBooleanAsInteger ? flag.ToString((IFormatProvider)CultureInfo.GetCultureInfo("en-US")) : (flag ? "1" : "0");
            }
            else
                propertyValue = Convert.ToString(value, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"));
            return propertyValue;
        }

        public static void SetPropertyValue(string value, object obj, PropertyInfo property)
        {
            if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
            {
                bool result;
                bool.TryParse(value, out result);
                property.SetValue(obj, (object)result);
            }
            else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
            {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                int result;
                int.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                property.SetValue(obj, (object)result);
            }
            else if (property.PropertyType == typeof(long) || property.PropertyType == typeof(long?))
            {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                long result;
                long.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                property.SetValue(obj, (object)result);
            }
            else if (property.PropertyType == typeof(float) || property.PropertyType == typeof(float?))
            {
                float result;
                float.TryParse(value.Replace("f", ""), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                property.SetValue(obj, (object)result);
            }
            else if (property.PropertyType == typeof(NullableValue<int>))
            {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                int result;
                int.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                NullableValue<int> nullableValue = property.GetValue(obj) as NullableValue<int>;
                property.SetValue(obj, (object)nullableValue.SetValue(true, result));
            }
            else if (property.PropertyType == typeof(NullableValue<long>))
            {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                long result;
                long.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                NullableValue<long> nullableValue = property.GetValue(obj) as NullableValue<long>;
                property.SetValue(obj, (object)nullableValue.SetValue(true, result));
            }
            else if (property.PropertyType == typeof(NullableValue<float>))
            {
                float result;
                float.TryParse(value.Replace("f", ""), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                NullableValue<float> nullableValue = property.GetValue(obj) as NullableValue<float>;
                property.SetValue(obj, (object)nullableValue.SetValue(true, result));
            }
            else if (property.PropertyType.IsSubclassOf(typeof(AggregateIniValue)))
            {
                if (!(property.GetValue(obj) is AggregateIniValue aggregateIniValue))
                    return;
                aggregateIniValue.InitializeFromINIValue(value);
            }
            else
            {
                object obj1 = Convert.ChangeType((object)value, property.PropertyType, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"));
                if (obj1 is string)
                    obj1 = (object)(obj1 as string).Trim('"');
                property.SetValue(obj, obj1);
            }
        }

        public static bool SetPropertyValue(
          string value,
          object obj,
          PropertyInfo property,
          BaseIniFileEntryAttribute attribute)
        {
            if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
            {
                bool result;
                if (attribute.WriteBooleanAsInteger)
                    result = value.Equals("1", StringComparison.OrdinalIgnoreCase);
                else
                    bool.TryParse(value, out result);
                if (attribute.InvertBoolean)
                    result = !result;
                property.SetValue(obj, (object)result);
                return true;
            }
            if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
            {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                int result;
                int.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                property.SetValue(obj, (object)result);
                return true;
            }
            if (property.PropertyType == typeof(long) || property.PropertyType == typeof(long?))
            {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                long result;
                long.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                property.SetValue(obj, (object)result);
                return true;
            }
            if (property.PropertyType == typeof(float) || property.PropertyType == typeof(float?))
            {
                float result;
                float.TryParse(value.Replace("f", ""), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                property.SetValue(obj, (object)result);
                return true;
            }
            if (property.PropertyType == typeof(NullableValue<int>))
            {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                int result;
                int.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                NullableValue<int> nullableValue = property.GetValue(obj) as NullableValue<int>;
                property.SetValue(obj, (object)nullableValue.SetValue(true, result));
                return true;
            }
            if (property.PropertyType == typeof(NullableValue<long>))
            {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                long result;
                long.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                NullableValue<long> nullableValue = property.GetValue(obj) as NullableValue<long>;
                property.SetValue(obj, (object)nullableValue.SetValue(true, result));
                return true;
            }
            if (property.PropertyType == typeof(NullableValue<float>))
            {
                float result;
                float.TryParse(value.Replace("f", ""), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                NullableValue<float> nullableValue = property.GetValue(obj) as NullableValue<float>;
                property.SetValue(obj, (object)nullableValue.SetValue(true, result));
                return true;
            }
            if (!property.PropertyType.IsSubclassOf(typeof(AggregateIniValue)))
                return false;
            if (property.GetValue(obj) is AggregateIniValue aggregateIniValue)
                aggregateIniValue.InitializeFromINIValue(value);
            return true;
        }

        public static List<FileInfo> CompareFolderContent(string pathA, string pathB, List<string> ignorePaths)
        {

            // Create two identical or different temporary folders
            // on a local drive and change these file paths.  
            Stopwatch sw = new Stopwatch();

            sw.Start();
            System.IO.DirectoryInfo dir1 = new System.IO.DirectoryInfo(pathA);
            System.IO.DirectoryInfo dir2 = new System.IO.DirectoryInfo(pathB);

            // Take a snapshot of the file system.  
            IEnumerable<System.IO.FileInfo> list1 = dir1.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
            IEnumerable<System.IO.FileInfo> list2 = dir2.GetFiles("*.*", System.IO.SearchOption.AllDirectories);

            var cacheFilesList = list2.ToList();
            var InstallFilesList = list1.ToList();

            Console.WriteLine("Elapsed={0}", sw.Elapsed);
            sw.Restart();

            Console.WriteLine("Elapsed={0}", sw.Elapsed);
            sw.Restart();

            var changedFiles = cacheFilesList.FindAll(f => CompareFiles(f, InstallFilesList.Find(f2 => f2.Name == f.Name)) != null);

            Console.WriteLine("Elapsed={0}", sw.Elapsed);
            sw.Restart();

            FileInfo CompareFiles(FileInfo f1, FileInfo f2)
            {
                if (f2 == null) return f1; 
                if (ignorePaths.Count > 0)
                {
                    string pathName = Path.GetDirectoryName(f1.FullName);
                    foreach (var item in ignorePaths)
                    {
                        if (pathName.Contains(item)) return null;
                    }

                } 
                ReadFileInChunksAndCompareVector x = new ReadFileInChunksAndCompareVector(f1.FullName, f2.FullName, Vector<byte>.Count);
                if (!x.Compare()) return f1;
                else return null; 
            }
            sw.Stop();
            Console.WriteLine("Elapsed={0}", sw.Elapsed);
            return changedFiles;
        }

        public static string CalculateMD5(string filename)
        {
            if (filename == null) return "";
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    //var hash = md5.ComputeHash(stream);
                    //return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    return Encoding.Default.GetString(md5.ComputeHash(stream));
                }
            }
        }

        public static Process GetProcessRunning(string ExecutablePath)
        {
            string ProcesseName = Path.GetFileNameWithoutExtension(ExecutablePath);
            string ClientFile = ExecutablePath;
            if (string.IsNullOrWhiteSpace(ClientFile) || !System.IO.File.Exists(ClientFile))
                return (Process)null;
            string a = IOUtils.NormalizePath(ClientFile);
            Process[] processesByName = Process.GetProcessesByName(ProcesseName);
            Process ProcessInfo = (Process)null;
            foreach (Process process in processesByName)
            {
                string mainModuleFilepath = ProcessUtils.GetMainModuleFilepath(process.Id);
                if (string.Equals(a, mainModuleFilepath, StringComparison.OrdinalIgnoreCase))
                {
                    ProcessInfo = process;
                    break;
                }
            }
            return ProcessInfo;
        }

        public static int GetProcessorCount()
        {
            return Environment.ProcessorCount;
        }
        public static string BinaryStringToHexString(string binary)
        {
            if (string.IsNullOrEmpty(binary))
                return binary;

            StringBuilder result = new StringBuilder(binary.Length / 8 + 1);

            if (!isbin(binary)) throw new Exception("the string is not binary");

            int mod4Len = binary.Length % 8;
            if (mod4Len != 0)
            {
                // pad to length multiple of 8
                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }

            for (int i = 0; i < binary.Length; i += 8)
            {
                string eightBits = binary.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            return result.ToString();

            bool isbin(string s)
            {
                foreach (var c in s)
                    if (c != '0' && c != '1')
                        return false;
                return true;
            }
        }
    }
}