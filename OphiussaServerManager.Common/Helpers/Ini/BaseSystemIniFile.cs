﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OphiussaServerManager.Common.Helpers;

namespace OphiussaServerManager.Common.Ini {
    public abstract class BaseSystemIniFile {
        protected BaseSystemIniFile(string iniPath) {
            BasePath = iniPath;
        }

        public string BasePath { get; }

        public abstract Dictionary<Enum, string> FileNames { get; }

        public abstract Dictionary<Enum, string> SectionNames { get; }


        public void Deserialize(object obj, IEnumerable<Enum> exclusions) {
            var iniFiles = new Dictionary<string, IniFile>();
            var fields = obj.GetType()
                            .GetProperties()
                            .Where(f => f.IsDefined(typeof(BaseIniFileEntryAttribute), false));

            if (exclusions == null) exclusions = new Enum[0];

            foreach (var field in fields) {
                var attributes = field
                                .GetCustomAttributes(typeof(BaseIniFileEntryAttribute), false)
                                .OfType<BaseIniFileEntryAttribute>()
                                .Where(a => !exclusions.Contains(a.Category));
 
                foreach (var attr in attributes) {
                    if (exclusions.Contains(attr.Category)) continue;

                    try { 
                        if (attr.IsCustom) {
                            // this code is to handle custom sections
                            if (field.GetValue(obj) is IIniSectionCollection collection) {
                                ReadFile(iniFiles, attr.File);

                                var sectionNames = ReadCustomSectionNames(iniFiles, attr.File);
                                foreach (string sectionName in sectionNames) {
                                    var sectionValues = ReadSection(iniFiles, attr.File, sectionName);
                                    collection.Add(sectionName, sectionValues);
                                }
                            }
                        }
                        else {
                            string keyName = string.IsNullOrWhiteSpace(attr.Key) ? field.Name : attr.Key;

                            if (attr.WriteBoolValueIfNonEmpty) {
                                // Don't really need to do anything here, we don't care about this on reading it.
                                // extraBoolValue = Convert.ToBoolean(IniReadValue(SectionNames[attr.Section], attr.Key));
                            }
                            else {
                                if (field.GetValue(obj) is IIniValuesCollection collection) {
                                    var section = ReadSection(iniFiles, attr.File, attr.Section);
                                    var filteredSection = collection.IsArray
                                                              ? section.Where(s => s.StartsWith(collection.IniCollectionKey + "["))
                                                              : section.Where(s => s.StartsWith(collection.IniCollectionKey + "="));
                                    collection.FromIniValues(filteredSection);
                                }
                                else {
                                    string iniValue = ReadValue(iniFiles, attr.File, attr.Section, keyName);

                                    var fieldType = field.PropertyType;
                                    if (fieldType == typeof(string)) {
                                        string stringValue = iniValue;
                                        if (attr.QuotedString == QuotedStringType.True) {
                                            // remove the leading and trailing quotes, if any
                                            if (stringValue.StartsWith("\"")) stringValue = stringValue.Substring(1);

                                            if (stringValue.EndsWith("\"")) stringValue = stringValue.Substring(0, stringValue.Length - 1);
                                        }
                                        else if (attr.QuotedString == QuotedStringType.Remove) {
                                            // remove the leading and trailing quotes, if any
                                            if (stringValue.StartsWith("\"")) stringValue = stringValue.Substring(1);

                                            if (stringValue.EndsWith("\"")) stringValue = stringValue.Substring(0, stringValue.Length - 1);
                                        }

                                        if (attr.Multiline) stringValue = stringValue.Replace(attr.MultilineSeparator, Environment.NewLine);
                                        field.SetValue(obj, stringValue);
                                    }
                                    else {
                                        if (string.IsNullOrWhiteSpace(iniValue))
                                            // Skip non-string values which are not found
                                            continue;

                                        // Update the ConditionedOn flag, if this field has one.
                                        if (!string.IsNullOrWhiteSpace(attr.ConditionedOn)) {
                                            var conditionField = obj.GetType().GetProperty(attr.ConditionedOn);
                                            conditionField.SetValue(obj, true);
                                        }

                                        bool valueSet = Utils.SetPropertyValue(iniValue, obj, field, attr);
                                        if (!valueSet) throw new ArgumentException($"Unexpected field type {fieldType} for INI key {keyName} in section {attr.Section}.");
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex) {
                        OphiussaLogger.Logger.Error(ex);
                        throw;
                    }
                }
            }
        }

        public void Serialize(object obj, IEnumerable<Enum> exclusions) {
            var iniFiles = new Dictionary<string, IniFile>();
            var fields = obj.GetType()
                            .GetProperties()
                            .Where(f => f.IsDefined(typeof(BaseIniFileEntryAttribute), false));

            if (exclusions == null) exclusions = new Enum[0];

            foreach (var field in fields) {
                var attributes = field
                                .GetCustomAttributes(typeof(BaseIniFileEntryAttribute), false)
                                .OfType<BaseIniFileEntryAttribute>()
                                .Where(a => !exclusions.Contains(a.Category));

                foreach (var attr in attributes)
                    try { 
                        if (attr.IsCustom) {
                            // this code is to handle custom sections
                            if (field.GetValue(obj) is IIniSectionCollection collection) {
                                collection.Update();

                                foreach (var section in collection.Sections) {
                                    // clear the entire section
                                    WriteValue(iniFiles, attr.File, section.IniCollectionKey, null, null);

                                    if (section.IsEnabled) WriteSection(iniFiles, attr.File, section.IniCollectionKey, section.ToIniValues());
                                }
                            }
                        }
                        else {
                            object value   = field.GetValue(obj);
                            string keyName = string.IsNullOrWhiteSpace(attr.Key) ? field.Name : attr.Key;

                            if (attr.ClearSection) {
                                WriteValue(iniFiles, attr.File, attr.Section, null, null);
                                ClearSectionIfEmpty(iniFiles, attr);
                            }

                            //
                            // If this is a collection, we need to first remove all of its values from the INI.
                            //
                            var collection = value as IIniValuesCollection;
                            if (collection != null) {
                                var section = ReadSection(iniFiles, attr.File, attr.Section);
                                var filteredSection = section
                                   .Where(s => !s.StartsWith(collection.IniCollectionKey + (collection.IsArray ? "[" : "=")));
                                WriteSection(iniFiles, attr.File, attr.Section, filteredSection);
                            }

                            if (!string.IsNullOrEmpty(attr.ConditionedOn)) {
                                var    conditionField = obj.GetType().GetProperty(attr.ConditionedOn);
                                object conditionValue = conditionField.GetValue(obj);
                                if (conditionValue is bool && (bool)conditionValue == false) {
                                    // The condition value was not set to true, so clear this attribute instead of writing it
                                    WriteValue(iniFiles, attr.File, attr.Section, keyName, null);
                                    ClearSectionIfEmpty(iniFiles, attr);
                                    continue;
                                }
                            }

                            if (!string.IsNullOrEmpty(attr.ClearWhenOff)) {
                                var    updateOffField = obj.GetType().GetProperty(attr.ClearWhenOff);
                                object updateOffValue = updateOffField.GetValue(obj);
                                if (updateOffValue is bool && (bool)updateOffValue == false) {
                                    // The attributed value was set to false, so clear this attribute instead of writing it
                                    WriteValue(iniFiles, attr.File, attr.Section, keyName, null);
                                    ClearSectionIfEmpty(iniFiles, attr);
                                }

                                continue;
                            }

                            if (attr.WriteBoolValueIfNonEmpty) {
                                if (value == null) {
                                    string keyValue = string.Empty;
                                    if (attr.WriteBooleanAsInteger)
                                        keyValue = "0";
                                    else
                                        keyValue = "False";

                                    WriteValue(iniFiles, attr.File, attr.Section, keyName, keyValue);
                                }
                                else {
                                    if (value is string) {
                                        string strValue = value as string;

                                        string keyValue = string.Empty;
                                        if (attr.WriteBooleanAsInteger)
                                            keyValue = string.IsNullOrEmpty(strValue) ? "0" : "1";
                                        else
                                            keyValue = string.IsNullOrEmpty(strValue) ? "False" : "True";

                                        WriteValue(iniFiles, attr.File, attr.Section, keyName, keyValue);
                                    }
                                    else {
                                        // Not supported
                                        throw new NotSupportedException("Unexpected IniFileEntry value type.");
                                    }
                                }
                            }
                            else {
                                if (collection != null) {
                                    if (collection.IsEnabled) {
                                        // Remove all the values in the collection with this key name
                                        var section = ReadSection(iniFiles, attr.File, attr.Section);

                                        var filteredSection = collection.IsArray
                                                                  ? section.Where(s => !s.StartsWith(keyName + "["))
                                                                  : section.Where(s => !s.StartsWith(keyName + "="));
                                        var result = filteredSection;

                                        object objValue = attr.WriteIfNotValue;
                                        if (objValue != null && collection is IIniValuesList valueList)
                                            result = result.Concat(valueList.ToIniValues(objValue));
                                        else
                                            result = result.Concat(collection.ToIniValues());

                                        WriteSection(iniFiles, attr.File, attr.Section, result);
                                    }
                                }
                                else {
                                    //
                                    // If this is a NullableValue, we need to check if it has a value.
                                    //
                                    if (value is INullableValue nullableValue && !nullableValue.HasValue) {
                                        // The attributed value does not have a value, so clear this attribute instead of writing it.
                                        WriteValue(iniFiles, attr.File, attr.Section, keyName, null);
                                        ClearSectionIfEmpty(iniFiles, attr);
                                        continue;
                                    }

                                    string strValue = Utils.GetPropertyValue(value, field, attr);

                                    object objValue = attr.WriteIfNotValue;
                                    if (objValue != null) {
                                        string strValue2 = Utils.GetPropertyValue(objValue, field, attr);
                                        if (string.Equals(strValue, strValue2, StringComparison.OrdinalIgnoreCase)) {
                                            // The attributed value is the same as the specified value, so clear this attribute instead of writing it.
                                            WriteValue(iniFiles, attr.File, attr.Section, keyName, null);
                                            ClearSectionIfEmpty(iniFiles, attr);
                                            continue;
                                        }
                                    }

                                    if (attr.QuotedString == QuotedStringType.True) {
                                        // if the stValue is empty, return empty quoted string (parsing not needed)
                                        // bug fix for 'property="' on a empty string
                                        if (strValue.IsEmpty()) {
                                            strValue = "\"\"";
                                        }
                                        else {
                                            // add the leading and trailing quotes, if not already have them.
                                            if (!strValue.StartsWith("\""))
                                                strValue = "\"" + strValue;
                                            if (!strValue.EndsWith("\""))
                                                strValue = strValue + "\"";
                                        }
                                    }
                                    else if (attr.QuotedString == QuotedStringType.Remove) {
                                        // remove the leading and trailing quotes, if any
                                        if (strValue.StartsWith("\""))
                                            strValue = strValue.Substring(1);
                                        if (strValue.EndsWith("\""))
                                            strValue = strValue.Substring(0, strValue.Length - 1);
                                    }

                                    if (attr.Multiline)
                                        // substitutes the NewLine string with "\n"
                                        strValue = strValue.Replace(Environment.NewLine, attr.MultilineSeparator);

                                    WriteValue(iniFiles, attr.File, attr.Section, keyName, strValue);
                                }
                            }
                        }

                        ClearSectionIfEmpty(iniFiles, attr);
                    }
                    catch (Exception ex) {
                        OphiussaLogger.Logger.Error(ex);
                        throw;
                    }
            }

            SaveFiles(iniFiles);
        }

        private void ClearSectionIfEmpty(
            Dictionary<string, IniFile> iniFiles,
            BaseIniFileEntryAttribute   attr) {
            if (!attr.ClearSectionIfEmpty)
                return;
            var source = ReadSection(iniFiles, attr.File, attr.Section);
            if ((source != null ? source.Any() ? 1 : 0 : 0) != 0)
                return;
            WriteValue(iniFiles, attr.File, attr.Section, null, null);
        }

        public IEnumerable<string> ReadSection(Enum iniFile, Enum section) {
            return ReadSection(iniFile, SectionNames[section]);
        }

        public IEnumerable<string> ReadSection(Enum iniFile, string sectionName) {
            return IniFileUtils.ReadSection(Path.Combine(BasePath, FileNames[iniFile]), sectionName);
        }


        public List<IniSection> GetAllSections(Enum iniFile) {
            var ini = IniFileUtils.ReadFromFile(Path.Combine(BasePath, FileNames[iniFile]));
            return ini.Sections;
        }

        public void WriteSection(Enum iniFile, Enum section, IEnumerable<string> values) {
            WriteSection(iniFile, SectionNames[section], values);
        }

        public void WriteSection(Enum iniFile, string sectionName, IEnumerable<string> values) {
            IniFileUtils.WriteSection(Path.Combine(BasePath, FileNames[iniFile]), sectionName, values);
        }

        private IEnumerable<string> ReadCustomSectionNames(
            Dictionary<string, IniFile> iniFiles,
            Enum                        iniFile) {
            if (!iniFiles.ContainsKey(FileNames[iniFile])) {
                ReadFile(iniFiles, iniFile);
                if (!iniFiles.ContainsKey(FileNames[iniFile]))
                    return new string[0];
            }

            return iniFiles[FileNames[iniFile]].Sections.Select(s => s.SectionName).Where(s => !SectionNames.ContainsValue(s));
        }

        private IEnumerable<string> ReadSection(
            Dictionary<string, IniFile> iniFiles,
            Enum                        iniFile,
            Enum                        section) {
            return ReadSection(iniFiles, iniFile, SectionNames[section]);
        }

        private IEnumerable<string> ReadSection(
            Dictionary<string, IniFile> iniFiles,
            Enum                        iniFile,
            string                      sectionName) {
            if (!iniFiles.ContainsKey(FileNames[iniFile])) {
                ReadFile(iniFiles, iniFile);
                if (!iniFiles.ContainsKey(FileNames[iniFile]))
                    return new string[0];
            }

            return iniFiles[FileNames[iniFile]].GetSection(sectionName)?.KeysToStringEnumerable() ?? new string[0];
        }

        private string ReadValue(
            Dictionary<string, IniFile> iniFiles,
            Enum                        iniFile,
            Enum                        section,
            string                      keyName) {
            if (!iniFiles.ContainsKey(FileNames[iniFile])) {
                ReadFile(iniFiles, iniFile);
                if (!iniFiles.ContainsKey(FileNames[iniFile]))
                    return string.Empty;
            }

            return iniFiles[FileNames[iniFile]].GetKey(SectionNames[section], keyName)?.KeyValue ?? string.Empty;
        }

        private void WriteSection(
            Dictionary<string, IniFile> iniFiles,
            Enum                        iniFile,
            Enum                        section,
            IEnumerable<string>         values) {
            WriteSection(iniFiles, iniFile, SectionNames[section], values);
        }

        private void WriteSection(
            Dictionary<string, IniFile> iniFiles,
            Enum                        iniFile,
            string                      sectionName,
            IEnumerable<string>         values) {
            if (!iniFiles.ContainsKey(FileNames[iniFile])) {
                ReadFile(iniFiles, iniFile);
                if (!iniFiles.ContainsKey(FileNames[iniFile]))
                    return;
            }

            iniFiles[FileNames[iniFile]].WriteSection(sectionName, values);
        }

        private void WriteValue(
            Dictionary<string, IniFile> iniFiles,
            Enum                        iniFile,
            Enum                        section,
            string                      keyName,
            string                      keyValue) {
            WriteValue(iniFiles, iniFile, SectionNames[section], keyName, keyValue);
        }

        private void WriteValue(
            Dictionary<string, IniFile> iniFiles,
            Enum                        iniFile,
            string                      sectionName,
            string                      keyName,
            string                      keyValue) {
            if (!iniFiles.ContainsKey(FileNames[iniFile])) {
                ReadFile(iniFiles, iniFile);
                if (!iniFiles.ContainsKey(FileNames[iniFile]))
                    return;
            }

            iniFiles[FileNames[iniFile]].WriteKey(sectionName, keyName, keyValue);
        }

        private void ReadFile(Dictionary<string, IniFile> iniFiles, Enum iniFile) {
            if (iniFiles.ContainsKey(FileNames[iniFile]))
                return;
            string file = Path.Combine(BasePath, FileNames[iniFile]);
            iniFiles.Add(FileNames[iniFile], IniFileUtils.ReadFromFile(file));
        }

        private void SaveFiles(Dictionary<string, IniFile> iniFiles) {
            foreach (var iniFile in iniFiles)
                IniFileUtils.SaveToFile(Path.Combine(BasePath, iniFile.Key), iniFile.Value);
        }
    }
}