using OphiussaServerManager.Common.Ini;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common.Ini
{

    public static class IniFileUtils
    {
        public static IEnumerable<string> ReadSection(string file, string sectionName) => sectionName == null ? (IEnumerable<string>)new string[0] : IniFileUtils.ReadFromFile(file)?.GetSection(sectionName)?.KeysToStringEnumerable() ?? (IEnumerable<string>)new string[0];

        public static string ReadValue(
          string file,
          string sectionName,
          string keyName,
          string defaultValue)
        {
            return sectionName == null || keyName == null ? defaultValue ?? string.Empty : IniFileUtils.ReadFromFile(file)?.GetSection(sectionName)?.GetKey(keyName)?.KeyValue ?? defaultValue ?? string.Empty;
        }

        public static bool WriteSection(
          string file,
          string sectionName,
          IEnumerable<string> keysValuePairs)
        {
            if (sectionName == null)
                return false;
            IniFile iniFile = IniFileUtils.ReadFromFile(file) ?? new IniFile();
            return iniFile.WriteSection(sectionName, keysValuePairs) && IniFileUtils.SaveToFile(file, iniFile);
        }

        public static bool WriteValue(
          string file,
          string sectionName,
          string keyName,
          string keyValue)
        {
            if (sectionName == null)
                return false;
            IniFile iniFile = IniFileUtils.ReadFromFile(file) ?? new IniFile();
            return iniFile.WriteKey(sectionName, keyName, keyValue) && IniFileUtils.SaveToFile(file, iniFile);
        }

        public static IniFile ReadFromFile(string file)
        {
            if (string.IsNullOrWhiteSpace(file))
                return (IniFile)null;
            if (!File.Exists(file))
                return new IniFile();
            IniFile iniFile = new IniFile();
            using (StreamReader streamReader = new StreamReader(file))
            {
                while (!streamReader.EndOfStream)
                {
                    string str = streamReader.ReadLine().Trim();
                    if (!string.IsNullOrWhiteSpace(str) && !str.StartsWith(";") && !str.StartsWith("#"))
                    {
                        string sectionName = string.Empty;
                        if (str.StartsWith("[") && str.EndsWith("]"))
                            sectionName = Regex.Match(str, "(?<=^\\[).*(?=\\]$)").Value.Trim();
                        if (iniFile.AddSection(sectionName) == null)
                            iniFile.AddKey(str);
                    }
                }
                streamReader.Close();
            }
            return iniFile;
        }

        public static IniFile ReadString(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new IniFile();
            IniFile iniFile = new IniFile();
            string str1 = text;
            string[] separator = new string[2] { "\r\n", "\n" };
            foreach (string str2 in str1.Split(separator, StringSplitOptions.None))
            {
                string str3 = str2.Trim();
                if (!string.IsNullOrWhiteSpace(str3) && !str3.StartsWith(";") && !str3.StartsWith("#"))
                {
                    string sectionName = Regex.Match(str3, "(?<=^\\[).*(?=\\]$)").Value.Trim();
                    if (iniFile.AddSection(sectionName) == null)
                        iniFile.AddKey(str3);
                }
            }
            return iniFile;
        }

        public static bool SaveToFile(string file, IniFile iniFile)
        {
            if (string.IsNullOrWhiteSpace(file) || iniFile == null)
                return false;
            using (StreamWriter streamWriter = new StreamWriter(file, false))
            {
                foreach (IniSection section in iniFile.Sections)
                {
                    streamWriter.WriteLine("[" + section.SectionName + "]");
                    foreach (string str in section.KeysToStringEnumerable())
                        streamWriter.WriteLine(str);
                    streamWriter.WriteLine();
                }
                streamWriter.Close();
            }
            return true;
        }
    }
}
