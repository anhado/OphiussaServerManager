using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using OphiussaServerManager.Common.Models;

namespace OphiussaServerManager.Common.Helpers {
    public static class EnumerableExtensions {
        public static bool ReadBoolValue(this IEnumerable<string> strings, string paramName, bool defaultValue = false) {
            bool result = defaultValue;

            string prop = strings.FirstOrDefault(x => x.StartsWith(paramName));
            if (prop != null) {
                int firstIndex = prop.IndexOf("=");
                result = prop.Substring(firstIndex + 1).ToUpper() == "True" ? true : false;
            }

            return result;
        }

        public static string ReadStringValue(this IEnumerable<string> strings, string paramName, string defaultValue = "") {
            string result = defaultValue;

            string prop = strings.FirstOrDefault(x => x.StartsWith(paramName));
            if (prop != null) {
                int firstIndex = prop.IndexOf("=");
                result = prop.Substring(firstIndex + 1);
            }

            return result;
        }

        public static int ReadIntValue(this IEnumerable<string> strings, string paramName, int defaultValue = 0) {
            int result = defaultValue;

            string prop = strings.FirstOrDefault(x => x.StartsWith(paramName));
            if (prop != null) {
                int    firstIndex                            = prop.IndexOf("=");
                string value                                 = prop.Substring(firstIndex + 1);
                if (int.TryParse(value, out int res)) result = res;
            }

            return result;
        }

        public static void WriteBoolValue(this List<ConfigFile> settings, string paramName, bool value) {
            var config = settings.FirstOrDefault(x => x.PropertyName == paramName);

            if (config != null)
                settings.First(x => x.PropertyName == paramName).PropertyValue = value ? "True" : "False";
            else
                settings.Add(new ConfigFile { PropertyName = paramName, PropertyValue = value ? "True" : "False" });
        }

        public static void WriteStringValue(this List<ConfigFile> settings, string paramName, string value) {
            var config = settings.FirstOrDefault(x => x.PropertyName == paramName);

            if (config != null)
                settings.First(x => x.PropertyName == paramName).PropertyValue = value;
            else
                settings.Add(new ConfigFile { PropertyName = paramName, PropertyValue = value });
        }

        public static void WriteIntValue(this List<ConfigFile> settings, string paramName, int value) {
            var config = settings.FirstOrDefault(x => x.PropertyName == paramName);

            if (config != null)
                settings.First(x => x.PropertyName == paramName).PropertyValue = value.ToString();
            else
                settings.Add(new ConfigFile { PropertyName = paramName, PropertyValue = value.ToString() });
        }

        public static List<ConfigFile> ToListConfigFile(this IEnumerable<string> strings) {
            var lst = strings.Select(val => {
                                         var c          = new ConfigFile();
                                         int firstIndex = val.IndexOf("=");
                                         c.PropertyName  = val.Substring(0, firstIndex);
                                         c.PropertyValue = val.Substring(firstIndex + 1);
                                         return c;
                                     }).ToList();

            return lst;
        }

        public static IEnumerable<string> ToEnumerableConfigFile(this List<ConfigFile> lst) {
            IEnumerable<string> ret = null;

            var strings = new List<string>();

            foreach (var c in lst) strings.Add(c.PropertyName + "=" + c.PropertyValue);
            ret = strings;
            //List<ConfigFile> lst = strings.Select(val =>
            //{
            //    ConfigFile c = new ConfigFile();
            //    int firstIndex = val.IndexOf("=");
            //    c.Value = val.Substring(0, firstIndex);
            //    c.Property = val.Substring(firstIndex + 1);
            //    return c;
            //}).ToList();

            //return lst; 
            return ret;
        }

        public static bool GetBool(this List<ConfigFile> list, string propertyName, bool defaultValue = false) {
            var val = list.Find(x => x.PropertyName == propertyName);

            if (val == null) return defaultValue;
            return list.Find(x => x.PropertyName == propertyName).PropertyValue.ToBool();
        }

        public static bool ToBool(this string prop) {
            if (prop.ToLower() == "true") return true;
            return false;
        }

        public static int GetInt(this List<ConfigFile> list, string propertyName, int defaultValue = 0) {
            var val = list.Find(x => x.PropertyName == propertyName);

            if (val == null) return defaultValue;
            return list.Find(x => x.PropertyName == propertyName).PropertyValue.ToInt();
        }

        public static float ToFloat(this string prop) {
            float val = 0;
            if (float.TryParse(prop, NumberStyles.Any, CultureInfo.InvariantCulture, out val)) return val;
            return 0;
        }

        public static int ToInt(this string prop) {
            int val = 0;
            if (int.TryParse(prop, NumberStyles.Any, CultureInfo.InvariantCulture, out val)) return val;
            return 0;
        }

        public static ushort ToUShort(this string prop) {
            ushort val = 0;
            if (ushort.TryParse(prop, NumberStyles.Any, CultureInfo.InvariantCulture, out val)) return val;
            return 0;
        }

        public static string GetString(this List<ConfigFile> list, string propertyName, string defaultValue = "") {
            var val = list.Find(x => x.PropertyName == propertyName);

            if (val == null) return defaultValue;
            return list.Find(x => x.PropertyName == propertyName).PropertyValue;
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
            catch (Exception) {
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

        public static bool IsEmpty<TSource>(this IEnumerable<TSource> source) {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            using (var enumerator = source.GetEnumerator()) {
                if (enumerator.MoveNext())
                    return false;
            }

            return true;
        }

        public static bool HasOne<TSource>(this IEnumerable<TSource> source) {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            int num = 0;
            using (var enumerator = source.GetEnumerator()) {
                while (enumerator.MoveNext())
                    if (++num > 1)
                        return false;
            }

            return num == 1;
        }

        public static void AddRange<T>(this BindingSource list, IEnumerable<T> data) {
            if (list == null || data == null) return;

            foreach (var t in data) list.Add(t);
        }

        public static DateTime UnixTimeStampToDateTime(this int unixTimeStamp) {
            // Unix timestamp is seconds past epoch
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        public static void SetValueEx(this TrackBar tb, int value) {
            if ( value>=tb.Minimum && value<=tb.Maximum ) {
                tb.Value = value;
            }else if (value < tb.Minimum) {
                tb.Value = tb.Minimum;
            }else if (value > tb.Maximum) {
                tb.Value = tb.Maximum;
            }
        }
    }
}