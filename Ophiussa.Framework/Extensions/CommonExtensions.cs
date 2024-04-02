using System.Globalization;
using System.Windows.Forms;

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

        public static void SetValueEx(this TrackBar tb, int value) {
            if (value >= tb.Minimum && value <= tb.Maximum)
                tb.Value = value;
            else if (value < tb.Minimum)
                tb.Value                          = tb.Minimum;
            else if (value > tb.Maximum) tb.Value = tb.Maximum;
        }
    }
}