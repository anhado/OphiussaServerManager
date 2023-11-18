using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common.Ini
{
    public interface IIniValuesList
    {
        IEnumerable<string> ToIniValues(object excludeIfValue);
    }
}
