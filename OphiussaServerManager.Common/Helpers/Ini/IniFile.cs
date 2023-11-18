using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common.Ini
{
    public class IniFile
    {
        public List<IniSection> Sections;
        public IniFile() => this.Sections = new List<IniSection>();

        public IniSection AddSection(string sectionName, bool allowEmptyName = false)
        {
            if (!allowEmptyName && string.IsNullOrWhiteSpace(sectionName))
                return (IniSection)null;
            IniSection iniSection = this.Sections.FirstOrDefault<IniSection>((Func<IniSection, bool>)(s => s.SectionName.Equals(sectionName, StringComparison.OrdinalIgnoreCase)));
            if (iniSection == null)
            {
                iniSection = new IniSection()
                {
                    SectionName = sectionName
                };
                this.Sections.Add(iniSection);
            }
            return iniSection;
        }
        public IniKey AddKey(string keyName, string keyValue) => (this.Sections.LastOrDefault<IniSection>() ?? this.AddSection(string.Empty, true)).AddKey(keyName, keyValue);
        public IniKey AddKey(string keyValuePair) => (this.Sections.LastOrDefault<IniSection>() ?? this.AddSection(string.Empty, true)).AddKey(keyValuePair);

        public IniSection GetSection(string sectionName)
        {
            List<IniSection> sections = this.Sections;
            return sections == null ? (IniSection)null : sections.FirstOrDefault<IniSection>((Func<IniSection, bool>)(s => s.SectionName.Equals(sectionName, StringComparison.OrdinalIgnoreCase)));
        }
        public IniKey GetKey(string sectionName, string keyName) => this.GetSection(sectionName)?.GetKey(keyName);
        public void RemoveSection(string sectionName) => this.RemoveSection(this.GetSection(sectionName));

        public void RemoveSection(IniSection section)
        {
            if (!this.Sections.Contains(section))
                return;
            this.Sections.Remove(section);
        }

        public bool WriteSection(string sectionName, IEnumerable<string> keysValuePairs)
        {
            if (sectionName == null)
                return false;
            bool flag = true;
            IniSection section = this.GetSection(sectionName);
            if (section != null)
                this.RemoveSection(section);
            IniSection iniSection = this.AddSection(sectionName);
            if (iniSection != null)
            {
                foreach (string keysValuePair in keysValuePairs)
                    iniSection.AddKey(keysValuePair);
            }
            return flag;
        }

        public bool WriteKey(string sectionName, string keyName, string keyValue)
        {
            if (sectionName == null)
                return false;
            int num = 1;
            IniSection section = this.GetSection(sectionName);
            if (section == null)
            {
                if (keyName != null)
                    section = this.AddSection(sectionName);
            }
            else if (keyName == null)
            {
                this.RemoveSection(section);
                section = (IniSection)null;
            }
            if (section == null)
                return num != 0;
            IniKey key = section.GetKey(keyName);
            IniKey iniKey;
            if (key == null)
            {
                if (keyValue == null)
                    return num != 0;
                iniKey = section.AddKey(keyName, keyValue);
                return num != 0;
            }
            if (keyValue == null)
            {
                section.RemoveKey(key);
                iniKey = (IniKey)null;
                return num != 0;
            }
            key.KeyValue = keyValue;
            return num != 0;
        }

        public string ToOutputString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (IniSection section in this.Sections)
            {
                stringBuilder.AppendLine("[" + section.SectionName + "]");
                foreach (string str in section.KeysToStringEnumerable())
                    stringBuilder.AppendLine(str);
                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
        }
    }
}
