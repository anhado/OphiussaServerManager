using System.Collections.Generic;

namespace OphiussaServerManager.Common.Ini {
    public interface IIniValuesList {
        IEnumerable<string> ToIniValues(object excludeIfValue);
    }
}