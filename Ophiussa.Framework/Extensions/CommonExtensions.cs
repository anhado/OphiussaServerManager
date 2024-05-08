using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using OphiussaFramework.CommonUtils;

namespace OphiussaFramework.Extensions {
    public static class CommonExtensions {

        public static T ParseEnum<T>(this string value) {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static void RefreshBindings(this Form frm) {
            foreach (Control obs in frm.Controls) {
                var binding  = obs.DataBindings;
                if (binding.Count > 0) {
                    binding[0].ReadValue();
                }
                else {
                    obs.RefreshBindings();
                }
            }
        }

        public static void RefreshBindings(this Control ctrControl) {
            foreach (Control obs in ctrControl.Controls) {
                var binding = obs.DataBindings;
                if (binding.Count > 0) {
                    binding[0].ReadValue();
                }
                else {
                    obs.RefreshBindings();
                }
            }
        }

        public static void RefreshBindings(this BindingContext context, object dataSource) {
            foreach (var binding in context[dataSource].Bindings.Cast<Binding>())
                binding.ReadValue();
        }
         
        public static float ToFloat(this string prop) {
            if (float.TryParse(prop, NumberStyles.Any, CultureInfo.InvariantCulture, out float val)) return val;
            return 0;
        }

        public static int ToInt(this string prop) {
            return int.TryParse(prop, NumberStyles.Any, CultureInfo.InvariantCulture, out int val) ? val : 0;
        }

        public static int ToInt(this float prop) {
            return int.TryParse(prop.ToString(CultureInfo.InvariantCulture), NumberStyles.Any, CultureInfo.InvariantCulture, out int val) ? val : 0;
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