using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common
{
    public interface INullableValue
    {
        bool HasValue { get; }

        INullableValue Clone();

        void SetValue(object value);
    }
}
