using OphiussaServerManager.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common.Ini
{
    public abstract class BaseSystemIniFile
    {
        protected BaseSystemIniFile(string iniPath) => this.BasePath = iniPath;

        public string BasePath { get; private set; }

        public abstract Dictionary<Enum, string> FileNames { get; }

        public abstract Dictionary<Enum, string> SectionNames { get; }

        public void Deserialize(object obj, IEnumerable<Enum> exclusions)
        {
            Dictionary<string, IniFile> iniFiles = new Dictionary<string, IniFile>();
            IEnumerable<PropertyInfo> propertyInfos = ((IEnumerable<PropertyInfo>)obj.GetType().GetProperties()).Where<PropertyInfo>((Func<PropertyInfo, bool>)(f => f.IsDefined(typeof(BaseIniFileEntryAttribute), false)));
            if (exclusions == null)
                exclusions = (IEnumerable<Enum>)new Enum[0];
            foreach (PropertyInfo property in propertyInfos)
            {
                foreach (BaseIniFileEntryAttribute attribute in property.GetCustomAttributes(typeof(BaseIniFileEntryAttribute), false).OfType<BaseIniFileEntryAttribute>().Where<BaseIniFileEntryAttribute>((Func<BaseIniFileEntryAttribute, bool>)(a => !exclusions.Contains<Enum>(a.Category))))
                {
                    if (!exclusions.Contains<Enum>(attribute.Category))
                    {
                        try
                        {
                            if (attribute.IsCustom)
                            {
                                if (property.GetValue(obj) is IIniSectionCollection sectionCollection)
                                {
                                    this.ReadFile(iniFiles, attribute.File);
                                    foreach (string customSectionName in this.ReadCustomSectionNames(iniFiles, attribute.File))
                                    {
                                        IEnumerable<string> values = this.ReadSection(iniFiles, attribute.File, customSectionName);
                                        sectionCollection.Add(customSectionName, values);
                                    }
                                }
                            }
                            else
                            {
                                string keyName = string.IsNullOrWhiteSpace(attribute.Key) ? property.Name : attribute.Key;
                                if (!attribute.WriteBoolValueIfNonEmpty)
                                {
                                    IIniValuesCollection collection = property.GetValue(obj) as IIniValuesCollection;
                                    if (collection != null)
                                    {
                                        IEnumerable<string> source = this.ReadSection(iniFiles, attribute.File, attribute.Section);
                                        IEnumerable<string> values = collection.IsArray ? source.Where<string>((Func<string, bool>)(s => s.StartsWith(collection.IniCollectionKey + "["))) : source.Where<string>((Func<string, bool>)(s => s.StartsWith(collection.IniCollectionKey + "=")));
                                        collection.FromIniValues(values);
                                    }
                                    else
                                    {
                                        string str1 = this.ReadValue(iniFiles, attribute.File, attribute.Section, keyName);
                                        Type propertyType = property.PropertyType;
                                        if (propertyType == typeof(string))
                                        {
                                            string str2 = str1;
                                            if (attribute.QuotedString == QuotedStringType.True)
                                            {
                                                if (str2.StartsWith("\""))
                                                    str2 = str2.Substring(1);
                                                if (str2.EndsWith("\""))
                                                    str2 = str2.Substring(0, str2.Length - 1);
                                            }
                                            else if (attribute.QuotedString == QuotedStringType.Remove)
                                            {
                                                if (str2.StartsWith("\""))
                                                    str2 = str2.Substring(1);
                                                if (str2.EndsWith("\""))
                                                    str2 = str2.Substring(0, str2.Length - 1);
                                            }
                                            if (attribute.Multiline)
                                                str2 = str2.Replace(attribute.MultilineSeparator, Environment.NewLine);
                                            property.SetValue(obj, (object)str2);
                                        }
                                        else if (!string.IsNullOrWhiteSpace(str1))
                                        {
                                            if (!string.IsNullOrWhiteSpace(attribute.ConditionedOn))
                                                obj.GetType().GetProperty(attribute.ConditionedOn).SetValue(obj, (object)true);
                                            if (!Utils.SetPropertyValue(str1, obj, property, attribute))
                                                throw new ArgumentException(string.Format("Unexpected field type {0} for INI key {1} in section {2}.", (object)propertyType, (object)keyName, (object)attribute.Section));
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                }
            }
        }

        public void Serialize(object obj, IEnumerable<Enum> exclusions)
        {
            Dictionary<string, IniFile> iniFiles = new Dictionary<string, IniFile>();
            IEnumerable<PropertyInfo> propertyInfos = ((IEnumerable<PropertyInfo>)obj.GetType().GetProperties()).Where<PropertyInfo>((Func<PropertyInfo, bool>)(f => f.IsDefined(typeof(BaseIniFileEntryAttribute), false)));
            if (exclusions == null)
                exclusions = (IEnumerable<Enum>)new Enum[0];
            foreach (PropertyInfo property in propertyInfos)
            {
                foreach (BaseIniFileEntryAttribute fileEntryAttribute in property.GetCustomAttributes(typeof(BaseIniFileEntryAttribute), false).OfType<BaseIniFileEntryAttribute>().Where<BaseIniFileEntryAttribute>((Func<BaseIniFileEntryAttribute, bool>)(a => !exclusions.Contains<Enum>(a.Category))))
                {
                    try
                    {
                        if (fileEntryAttribute.IsCustom)
                        {
                            if (property.GetValue(obj) is IIniSectionCollection sectionCollection)
                            {
                                sectionCollection.Update();
                                foreach (IIniValuesCollection section in sectionCollection.Sections)
                                {
                                    this.WriteValue(iniFiles, fileEntryAttribute.File, section.IniCollectionKey, (string)null, (string)null);
                                    if (section.IsEnabled)
                                        this.WriteSection(iniFiles, fileEntryAttribute.File, section.IniCollectionKey, section.ToIniValues());
                                }
                            }
                        }
                        else
                        {
                            object obj1 = property.GetValue(obj);
                            string keyName = string.IsNullOrWhiteSpace(fileEntryAttribute.Key) ? property.Name : fileEntryAttribute.Key;
                            if (fileEntryAttribute.ClearSection)
                            {
                                this.WriteValue(iniFiles, fileEntryAttribute.File, fileEntryAttribute.Section, (string)null, (string)null);
                                this.ClearSectionIfEmpty(iniFiles, fileEntryAttribute);
                            }
                            IIniValuesCollection collection = obj1 as IIniValuesCollection;
                            if (collection != null)
                            {
                                IEnumerable<string> values = this.ReadSection(iniFiles, fileEntryAttribute.File, fileEntryAttribute.Section).Where<string>((Func<string, bool>)(s => !s.StartsWith(collection.IniCollectionKey + (collection.IsArray ? "[" : "="))));
                                this.WriteSection(iniFiles, fileEntryAttribute.File, fileEntryAttribute.Section, values);
                            }
                            if (!string.IsNullOrEmpty(fileEntryAttribute.ConditionedOn) && obj.GetType().GetProperty(fileEntryAttribute.ConditionedOn).GetValue(obj) is bool flag1 && !flag1)
                            {
                                this.WriteValue(iniFiles, fileEntryAttribute.File, fileEntryAttribute.Section, keyName, (string)null);
                                this.ClearSectionIfEmpty(iniFiles, fileEntryAttribute);
                                continue;
                            }
                            if (!string.IsNullOrEmpty(fileEntryAttribute.ClearWhenOff))
                            {
                                if (obj.GetType().GetProperty(fileEntryAttribute.ClearWhenOff).GetValue(obj) is bool flag2)
                                {
                                    if (!flag2)
                                    {
                                        this.WriteValue(iniFiles, fileEntryAttribute.File, fileEntryAttribute.Section, keyName, (string)null);
                                        this.ClearSectionIfEmpty(iniFiles, fileEntryAttribute);
                                        continue;
                                    }
                                    continue;
                                }
                                continue;
                            }
                            if (fileEntryAttribute.WriteBoolValueIfNonEmpty)
                            {
                                if (obj1 == null)
                                {
                                    string empty = string.Empty;
                                    string keyValue = !fileEntryAttribute.WriteBooleanAsInteger ? "False" : "0";
                                    this.WriteValue(iniFiles, fileEntryAttribute.File, fileEntryAttribute.Section, keyName, keyValue);
                                }
                                else
                                {
                                    string str = obj1 is string ? obj1 as string : throw new NotSupportedException("Unexpected IniFileEntry value type.");
                                    string empty = string.Empty;
                                    string keyValue = !fileEntryAttribute.WriteBooleanAsInteger ? (string.IsNullOrEmpty(str) ? "False" : "True") : (string.IsNullOrEmpty(str) ? "0" : "1");
                                    this.WriteValue(iniFiles, fileEntryAttribute.File, fileEntryAttribute.Section, keyName, keyValue);
                                }
                            }
                            else if (collection != null)
                            {
                                if (collection.IsEnabled)
                                {
                                    IEnumerable<string> source = this.ReadSection(iniFiles, fileEntryAttribute.File, fileEntryAttribute.Section);
                                    IEnumerable<string> first = collection.IsArray ? source.Where<string>((Func<string, bool>)(s => !s.StartsWith(keyName + "["))) : source.Where<string>((Func<string, bool>)(s => !s.StartsWith(keyName + "=")));
                                    object writeIfNotValue = fileEntryAttribute.WriteIfNotValue;
                                    IEnumerable<string> values = writeIfNotValue == null || !(collection is IIniValuesList iniValuesList) ? first.Concat<string>(collection.ToIniValues()) : first.Concat<string>(iniValuesList.ToIniValues(writeIfNotValue));
                                    this.WriteSection(iniFiles, fileEntryAttribute.File, fileEntryAttribute.Section, values);
                                }
                            }
                            else
                            {
                                if (obj1 is INullableValue nullableValue && !nullableValue.HasValue)
                                {
                                    this.WriteValue(iniFiles, fileEntryAttribute.File, fileEntryAttribute.Section, keyName, (string)null);
                                    this.ClearSectionIfEmpty(iniFiles, fileEntryAttribute);
                                    continue;
                                }
                                string str = Utils.GetPropertyValue(obj1, property, fileEntryAttribute);
                                object writeIfNotValue = fileEntryAttribute.WriteIfNotValue;
                                if (writeIfNotValue != null)
                                {
                                    string propertyValue = Utils.GetPropertyValue(writeIfNotValue, property, fileEntryAttribute);
                                    if (string.Equals(str, propertyValue, StringComparison.OrdinalIgnoreCase))
                                    {
                                        this.WriteValue(iniFiles, fileEntryAttribute.File, fileEntryAttribute.Section, keyName, (string)null);
                                        this.ClearSectionIfEmpty(iniFiles, fileEntryAttribute);
                                        continue;
                                    }
                                }
                                if (fileEntryAttribute.QuotedString == QuotedStringType.True)
                                {
                                    if (str.IsEmpty<char>())
                                    //if (str.IsEmpty<char>())
                                        {
                                        str = "\"\"";
                                    }
                                    else
                                    {
                                        if (!str.StartsWith("\""))
                                            str = "\"" + str;
                                        if (!str.EndsWith("\""))
                                            str += "\"";
                                    }
                                }
                                else if (fileEntryAttribute.QuotedString == QuotedStringType.Remove)
                                {
                                    if (str.StartsWith("\""))
                                        str = str.Substring(1);
                                    if (str.EndsWith("\""))
                                        str = str.Substring(0, str.Length - 1);
                                }
                                if (fileEntryAttribute.Multiline)
                                    str = str.Replace(Environment.NewLine, fileEntryAttribute.MultilineSeparator);
                                this.WriteValue(iniFiles, fileEntryAttribute.File, fileEntryAttribute.Section, keyName, str);
                            }
                        }
                        this.ClearSectionIfEmpty(iniFiles, fileEntryAttribute);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            this.SaveFiles(iniFiles);
        }

        private void ClearSectionIfEmpty(
          Dictionary<string, IniFile> iniFiles,
          BaseIniFileEntryAttribute attr)
        {
            if (!attr.ClearSectionIfEmpty)
                return;
            IEnumerable<string> source = this.ReadSection(iniFiles, attr.File, attr.Section);
            if ((source != null ? (source.Any<string>() ? 1 : 0) : 0) != 0)
                return;
            this.WriteValue(iniFiles, attr.File, attr.Section, (string)null, (string)null);
        }

        public IEnumerable<string> ReadSection(Enum iniFile, Enum section) => this.ReadSection(iniFile, this.SectionNames[section]);

        public IEnumerable<string> ReadSection(Enum iniFile, string sectionName) => IniFileUtils.ReadSection(Path.Combine(this.BasePath, this.FileNames[iniFile]), sectionName);
         

        public void WriteSection(Enum iniFile, Enum section, IEnumerable<string> values) => this.WriteSection(iniFile, this.SectionNames[section], values);

        public void WriteSection(Enum iniFile, string sectionName, IEnumerable<string> values) => IniFileUtils.WriteSection(Path.Combine(this.BasePath, this.FileNames[iniFile]), sectionName, values);

        private IEnumerable<string> ReadCustomSectionNames(
          Dictionary<string, IniFile> iniFiles,
          Enum iniFile)
        {
            if (!iniFiles.ContainsKey(this.FileNames[iniFile]))
            {
                this.ReadFile(iniFiles, iniFile);
                if (!iniFiles.ContainsKey(this.FileNames[iniFile]))
                    return (IEnumerable<string>)new string[0];
            }
            return iniFiles[this.FileNames[iniFile]].Sections.Select<IniSection, string>((Func<IniSection, string>)(s => s.SectionName)).Where<string>((Func<string, bool>)(s => !this.SectionNames.ContainsValue(s)));
        }
        private IEnumerable<string> ReadSection(
        Dictionary<string, IniFile> iniFiles,
        Enum iniFile,
          Enum section)
        {
            return this.ReadSection(iniFiles, iniFile, this.SectionNames[section]);
        }

        private IEnumerable<string> ReadSection(
          Dictionary<string, IniFile> iniFiles,
          Enum iniFile,
          string sectionName)
        {
            if (!iniFiles.ContainsKey(this.FileNames[iniFile]))
            {
                this.ReadFile(iniFiles, iniFile);
                if (!iniFiles.ContainsKey(this.FileNames[iniFile]))
                    return (IEnumerable<string>)new string[0];
            }
            return iniFiles[this.FileNames[iniFile]].GetSection(sectionName)?.KeysToStringEnumerable() ?? (IEnumerable<string>)new string[0];
        }

        private string ReadValue(
          Dictionary<string, IniFile> iniFiles,
          Enum iniFile,
          Enum section,
          string keyName)
        {
            if (!iniFiles.ContainsKey(this.FileNames[iniFile]))
            {
                this.ReadFile(iniFiles, iniFile);
                if (!iniFiles.ContainsKey(this.FileNames[iniFile]))
                    return string.Empty;
            }
            return iniFiles[this.FileNames[iniFile]].GetKey(this.SectionNames[section], keyName)?.KeyValue ?? string.Empty;
        }

        private void WriteSection(
          Dictionary<string, IniFile> iniFiles,
          Enum iniFile,
          Enum section,
          IEnumerable<string> values)
        {
            this.WriteSection(iniFiles, iniFile, this.SectionNames[section], values);
        }

        private void WriteSection(
          Dictionary<string, IniFile> iniFiles,
          Enum iniFile,
          string sectionName,
          IEnumerable<string> values)
        {
            if (!iniFiles.ContainsKey(this.FileNames[iniFile]))
            {
                this.ReadFile(iniFiles, iniFile);
                if (!iniFiles.ContainsKey(this.FileNames[iniFile]))
                    return;
            }
            iniFiles[this.FileNames[iniFile]].WriteSection(sectionName, values);
        }

        private void WriteValue(
          Dictionary<string, IniFile> iniFiles,
          Enum iniFile,
          Enum section,
          string keyName,
          string keyValue)
        {
            this.WriteValue(iniFiles, iniFile, this.SectionNames[section], keyName, keyValue);
        }

        private void WriteValue(
          Dictionary<string, IniFile> iniFiles,
          Enum iniFile,
          string sectionName,
          string keyName,
          string keyValue)
        {
            if (!iniFiles.ContainsKey(this.FileNames[iniFile]))
            {
                this.ReadFile(iniFiles, iniFile);
                if (!iniFiles.ContainsKey(this.FileNames[iniFile]))
                    return;
            }
            iniFiles[this.FileNames[iniFile]].WriteKey(sectionName, keyName, keyValue);
        }

        private void ReadFile(Dictionary<string, IniFile> iniFiles, Enum iniFile)
        {
            if (iniFiles.ContainsKey(this.FileNames[iniFile]))
                return;
            string file = Path.Combine(this.BasePath, this.FileNames[iniFile]);
            iniFiles.Add(this.FileNames[iniFile], IniFileUtils.ReadFromFile(file));
        }

        private void SaveFiles(Dictionary<string, IniFile> iniFiles)
        {
            foreach (KeyValuePair<string, IniFile> iniFile in iniFiles)
                IniFileUtils.SaveToFile(Path.Combine(this.BasePath, iniFile.Key), iniFile.Value);
        }
    }
}
