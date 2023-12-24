using System.Collections.Generic;

namespace OphiussaServerManager.Common.Ini {
    public interface IIniValuesCollection {
        string IniCollectionKey { get; }

        bool IsArray { get; }

        bool IsEnabled { get; set; }

        void FromIniValues(IEnumerable<string> values);

        IEnumerable<string> ToIniValues();
    }
}