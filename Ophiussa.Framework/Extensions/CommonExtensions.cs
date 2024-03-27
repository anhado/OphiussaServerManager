using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaFramework.Extensions {
    public static class CommonExtensions {

        public static float ToFloat(this string prop) {
            if (float.TryParse(prop, NumberStyles.Any, CultureInfo.InvariantCulture, out float val)) return val;
            return 0;
        }

        public static int ToInt(this string prop) {
            if (int.TryParse(prop, NumberStyles.Any, CultureInfo.InvariantCulture, out int val)) return val;
            return 0;
        }

        public static ushort ToUShort(this string prop) {
            if (ushort.TryParse(prop, NumberStyles.Any, CultureInfo.InvariantCulture, out ushort val)) return val;
            return 0;
        }
    }
}
