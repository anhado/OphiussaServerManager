using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using OphiussaServerManager.Common.Models;

namespace OphiussaServerManager.Common.Helpers {

    public static class EnumerableExtensions {
        public static bool ReadBoolValue(this IEnumerable<string> strings, string paramName, bool defaultValue = false) {
            bool result = defaultValue;

            string prop = strings.FirstOrDefault(x => x.StartsWith(paramName));
            if (prop == null) return result;
            int firstIndex = prop.IndexOf("=", StringComparison.Ordinal);
            result = prop.Substring(firstIndex + 1).ToUpper() == "True";

            return result;
        }

        public static string ReadStringValue(this IEnumerable<string> strings, string paramName, string defaultValue = "") {
            string result = defaultValue;

            string prop = strings.FirstOrDefault(x => x.StartsWith(paramName));
            if (prop == null) return result;
            int firstIndex = prop.IndexOf("=", StringComparison.Ordinal);
            result = prop.Substring(firstIndex + 1);

            return result;
        }

        public static int ReadIntValue(this IEnumerable<string> strings, string paramName, int defaultValue = 0) {
            int result = defaultValue;

            string prop = strings.FirstOrDefault(x => x.StartsWith(paramName));
            if (prop != null) {
                int    firstIndex = prop.IndexOf("=", StringComparison.Ordinal);
                string value      = prop.Substring(firstIndex + 1);
                if (int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int res)) {
                    result = res;
                }
            }

            return result;
        }

        public static float ReadFloatValue(this IEnumerable<string> strings, string paramName, float defaultValue = 0) {
            float result = defaultValue;

            string prop = strings.FirstOrDefault(x => x.StartsWith(paramName));
            if (prop != null) {
                int    firstIndex                                                                                = prop.IndexOf("=", StringComparison.Ordinal);
                string value                                                                                     = prop.Substring(firstIndex + 1);
                if (float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out float res)) result = res;
            }

            return result;
        }

        public static void WriteBoolValue(this List<ConfigFile> settings, string paramName, bool value) {
            var config = settings.FirstOrDefault(x => x.PropertyName == paramName);

            if (config != null)
                settings.First(x => x.PropertyName == paramName).PropertyValue = value ? "True" : "False";
            else settings.Add(new ConfigFile { PropertyName = paramName, PropertyValue = value ? "True" : "False" });
        }

        public static void WriteStringValue(this List<ConfigFile> settings, string paramName, string value) {
            var config = settings.FirstOrDefault(x => x.PropertyName == paramName);
 
            if (config != null)
                settings.First(x => x.PropertyName == paramName).PropertyValue = value;
            else   settings.Add(new ConfigFile { PropertyName = paramName, PropertyValue = value });
        }

        public static void WriteIntValue(this List<ConfigFile> settings, string paramName, int value) {
            var config = settings.FirstOrDefault(x => x.PropertyName == paramName);

            if (config != null)
                settings.First(x => x.PropertyName == paramName).PropertyValue = value.ToString(CultureInfo.InvariantCulture);
            else  settings.Add(new ConfigFile { PropertyName = paramName, PropertyValue = value.ToString(CultureInfo.InvariantCulture) });
        }

        public static void WriteFloatValue(this List<ConfigFile> settings, string paramName, float value) {
            var config = settings.FirstOrDefault(x => x.PropertyName == paramName);

             
            if (config != null)
                settings.First(x => x.PropertyName == paramName).PropertyValue = Math.Round(value, 2).ToString(CultureInfo.InvariantCulture);
            else settings.Add(new ConfigFile { PropertyName = paramName, PropertyValue = Math.Round(value, 2).ToString(CultureInfo.InvariantCulture) });
        }

        public static List<ConfigFile> ToListConfigFile(this IEnumerable<string> strings) {
            var lst = strings.Select(val => {
                                         var c          = new ConfigFile();
                                         int firstIndex = val.IndexOf("=", StringComparison.Ordinal);
                                         c.PropertyName  = val.Substring(0, firstIndex);
                                         c.PropertyValue = val.Substring(firstIndex + 1);
                                         return c;
                                     }).ToList();

            return lst;
        }

        public static IEnumerable<string> ToEnumerableConfigFile(this List<ConfigFile> lst) {
            var strings = new List<string>();

            foreach (var c in lst) {
                if (!c.Ignore) strings.Add(c.PropertyName + "=" + c.PropertyValue);
            }

            IEnumerable<string> ret = strings;

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
            if (value >= tb.Minimum && value <= tb.Maximum) {
                tb.Value = value;
            }
            else if (value < tb.Minimum) {
                tb.Value = tb.Minimum;
            }
            else if (value > tb.Maximum) {
                tb.Value = tb.Maximum;
            }
        }

        public static int ConvertHourToSeconds(this string value, bool haveSeparator = false) {
            TimeSpan t      = TimeSpan.FromHours(value.Substring(0, 2).ToInt()) + TimeSpan.FromMinutes(value.Substring(2, 2).ToInt());
            return t.Seconds;
        }

        public static string ConvertSecondsToHour(this int value, bool haveSeparator = false) {
            TimeSpan t = TimeSpan.FromSeconds(value);

            string result = string.Format("{0:D2}{1:D2}",
                                          t.Hours,
                                          t.Minutes);
            return result;
        }

        public static IEnumerable<Attribute> GetAllAttributes(this Type type, string propertyName) {
            var propertyInfos = type.GetProperties();

            var prp =  propertyInfos.FirstOrDefault(x => x.Name == propertyName)?.GetCustomAttributes() ?? new List<Attribute>();
            return prp;
        }
    }
}