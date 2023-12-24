using System;
using System.Collections.Generic;
using System.Linq;

namespace OphiussaServerManager.Common.Ini {
    public class IniSection {
        public List<IniKey> Keys;
        public string       SectionName;

        public IniSection() {
            SectionName = string.Empty;
            Keys        = new List<IniKey>();
        }

        public IniKey AddKey(string keyName, string keyValue) {
            var iniKey = new IniKey {
                                        KeyName  = keyName,
                                        KeyValue = keyValue
                                    };
            Keys.Add(iniKey);
            return iniKey;
        }

        public IniKey AddKey(string keyValuePair) {
            string[] strArray1;
            if (keyValuePair == null)
                strArray1 = null;
            else
                strArray1 = keyValuePair.Split(new char[1] { '=' }, 2);
            if (strArray1 == null)
                strArray1 = new string[1];
            string[] strArray2 = strArray1;
            if (string.IsNullOrWhiteSpace(strArray2[0]))
                return null;
            var iniKey = new IniKey {
                                        KeyName = strArray2[0]
                                    };
            if (strArray2.Length > 1)
                iniKey.KeyValue = strArray2[1];
            Keys.Add(iniKey);
            return iniKey;
        }

        public IniKey GetKey(string keyName) {
            var keys = Keys;
            return keys == null ? null : keys.FirstOrDefault(s => s.KeyName.Equals(keyName, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<string> KeysToStringEnumerable() {
            return Keys.Select(k => k.ToString());
        }

        public void RemoveKey(string keyName) {
            RemoveKey(GetKey(keyName));
        }

        public void RemoveKey(IniKey key) {
            if (!Keys.Contains(key))
                return;
            Keys.Remove(key);
        }

        public override string ToString() {
            return "[" + SectionName + "]";
        }
    }
}