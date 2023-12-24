using System.Collections.Generic;

namespace OphiussaServerManager.Common.Ini {
    public interface IIniSectionCollection {
        IIniValuesCollection[] Sections { get; }

        void Add(string sectionName, IEnumerable<string> values);

        void Update();
    }
}