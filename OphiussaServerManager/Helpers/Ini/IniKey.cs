using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Ini
{
    public class IniKey
    {
        public string KeyName;
        public string KeyValue;

        public IniKey()
        {
            this.KeyName = string.Empty;
            this.KeyValue = string.Empty;
        }

        public override string ToString() => this.KeyName + "=" + this.KeyValue;
    }
}
