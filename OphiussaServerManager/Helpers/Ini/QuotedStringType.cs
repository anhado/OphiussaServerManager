using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Ini
{
    [DefaultValue(QuotedStringType.False)]
    public enum QuotedStringType
    {
        False,
        True,
        Remove,
    }
}
