using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using OphiussaFramework.CommonUtils;

namespace OphiussaFramework.Extensions {
    public static class CommonExtensions {

        public static void RefreshBindings(this BindingContext context, object dataSource) {
            foreach (var binding in context[dataSource].Bindings.Cast<Binding>())
                binding.ReadValue();
        }

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
        public static void AppendTextWithTimeStamp(this RichTextBox box, string text, Color color) {
            try {
                text                = "[" + DateTime.Now.ToString("u") + "] " + text + "\n";
                box.SelectionStart  = box.TextLength;
                box.SelectionLength = 0;

                box.SelectionColor = color;
                box.AppendText(text);
                box.SelectionColor = box.ForeColor;
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
            }
        }

        public static void AppendText(this RichTextBox box, string text, Color color) {
            text                = text + "\n";
            box.SelectionStart  = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}