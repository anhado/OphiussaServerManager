using System.ComponentModel;

namespace OphiussaServerManager.Common.Ini {
    [DefaultValue(False)]
    public enum QuotedStringType {
        False,
        True,
        Remove
    }
}