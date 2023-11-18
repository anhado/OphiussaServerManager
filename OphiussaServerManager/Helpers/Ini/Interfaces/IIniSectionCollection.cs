using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Ini
{
    public interface IIniSectionCollection
    {
        IIniValuesCollection[] Sections { get; }

        void Add(string sectionName, IEnumerable<string> values);

        void Update();
    }
}
