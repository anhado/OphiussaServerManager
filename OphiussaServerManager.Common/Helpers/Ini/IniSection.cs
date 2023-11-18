using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common.Ini
{
    public class IniSection
    {
        public string SectionName;
        public List<IniKey> Keys;

        public IniSection()
        {
            this.SectionName = string.Empty;
            this.Keys = new List<IniKey>();
        }

        public IniKey AddKey(string keyName, string keyValue)
        {
            IniKey iniKey = new IniKey()
            {
                KeyName = keyName,
                KeyValue = keyValue
            };
            this.Keys.Add(iniKey);
            return iniKey;
        }

        public IniKey AddKey(string keyValuePair)
        {
            string[] strArray1;
            if (keyValuePair == null)
                strArray1 = (string[])null;
            else
                strArray1 = keyValuePair.Split(new char[1] { '=' }, 2);
            if (strArray1 == null)
                strArray1 = new string[1];
            string[] strArray2 = strArray1;
            if (string.IsNullOrWhiteSpace(strArray2[0]))
                return (IniKey)null;
            IniKey iniKey = new IniKey()
            {
                KeyName = strArray2[0]
            };
            if (strArray2.Length > 1)
                iniKey.KeyValue = strArray2[1];
            this.Keys.Add(iniKey);
            return iniKey;
        }

        public IniKey GetKey(string keyName)
        {
            List<IniKey> keys = this.Keys;
            return keys == null ? (IniKey)null : keys.FirstOrDefault<IniKey>((Func<IniKey, bool>)(s => s.KeyName.Equals(keyName, StringComparison.OrdinalIgnoreCase)));
        }
        public IEnumerable<string> KeysToStringEnumerable() => this.Keys.Select<IniKey, string>((Func<IniKey, string>)(k => k.ToString()));
        public void RemoveKey(string keyName) => this.RemoveKey(this.GetKey(keyName));

        public void RemoveKey(IniKey key)
        {
            if (!this.Keys.Contains(key))
                return;
            this.Keys.Remove(key);
        }

        public override string ToString() => "[" + this.SectionName + "]";
    }
}
