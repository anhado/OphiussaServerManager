using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OphiussaServerManager.Common.Ini {
    public class IniFile {
        public List<IniSection> Sections;

        public IniFile() {
            Sections = new List<IniSection>();
        }

        public IniSection AddSection(string sectionName, bool allowEmptyName = false) {
            if (!allowEmptyName && string.IsNullOrWhiteSpace(sectionName))
                return null;
            var iniSection = Sections.FirstOrDefault(s => s.SectionName.Equals(sectionName, StringComparison.OrdinalIgnoreCase));
            if (iniSection == null) {
                iniSection = new IniSection {
                                                SectionName = sectionName
                                            };
                Sections.Add(iniSection);
            }

            return iniSection;
        }

        public IniKey AddKey(string keyName, string keyValue) {
            return (Sections.LastOrDefault() ?? AddSection(string.Empty, true)).AddKey(keyName, keyValue);
        }

        public IniKey AddKey(string keyValuePair) {
            return (Sections.LastOrDefault() ?? AddSection(string.Empty, true)).AddKey(keyValuePair);
        }

        public IniSection GetSection(string sectionName) {
            var sections = Sections;
            return sections == null ? null : sections.FirstOrDefault(s => s.SectionName.Equals(sectionName, StringComparison.OrdinalIgnoreCase));
        }

        public IniKey GetKey(string sectionName, string keyName) {
            return GetSection(sectionName)?.GetKey(keyName);
        }

        public void RemoveSection(string sectionName) {
            RemoveSection(GetSection(sectionName));
        }

        public void RemoveSection(IniSection section) {
            if (!Sections.Contains(section))
                return;
            Sections.Remove(section);
        }

        public bool WriteSection(string sectionName, IEnumerable<string> keysValuePairs) {
            if (sectionName == null)
                return false;
            bool flag    = true;
            var  section = GetSection(sectionName);
            if (section != null)
                RemoveSection(section);
            var iniSection = AddSection(sectionName);
            if (iniSection != null)
                foreach (string keysValuePair in keysValuePairs)
                    iniSection.AddKey(keysValuePair);
            return flag;
        }

        public bool WriteKey(string sectionName, string keyName, string keyValue) {
            if (sectionName == null)
                return false;
            int num     = 1;
            var section = GetSection(sectionName);
            if (section == null) {
                if (keyName != null)
                    section = AddSection(sectionName);
            }
            else if (keyName == null) {
                RemoveSection(section);
                section = null;
            }

            if (section == null)
                return num != 0;
            var    key = section.GetKey(keyName);
            IniKey iniKey;
            if (key == null) {
                if (keyValue == null)
                    return num != 0;
                iniKey = section.AddKey(keyName, keyValue);
                return num != 0;
            }

            if (keyValue == null) {
                section.RemoveKey(key);
                iniKey = null;
                return num != 0;
            }

            key.KeyValue = keyValue;
            return num != 0;
        }

        public string ToOutputString() {
            var stringBuilder = new StringBuilder();
            foreach (var section in Sections) {
                stringBuilder.AppendLine("[" + section.SectionName + "]");
                foreach (string str in section.KeysToStringEnumerable())
                    stringBuilder.AppendLine(str);
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }
}