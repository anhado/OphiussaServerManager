using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OphiussaServerManager.Common.Helpers
{
    public static class IEnumerableExtensions
    {

        public static bool ReadBoolValue(this IEnumerable<string> strings, string ParamName, bool defaultValue = false)
        {
            bool result = defaultValue;

            string prop = strings.FirstOrDefault(x => x.StartsWith(ParamName));
            if (prop != null)
            {
                int firstIndex = prop.IndexOf("=");
                result = prop.Substring(firstIndex + 1).ToUpper() == "True" ? true : false;
            }
            return result;
        }
        public static string ReadStringValue(this IEnumerable<string> strings, string ParamName, string defaultValue = "")
        {
            string result = defaultValue;

            string prop = strings.FirstOrDefault(x => x.StartsWith(ParamName));
            if (prop != null)
            {
                int firstIndex = prop.IndexOf("=");
                result = prop.Substring(firstIndex + 1);
            }
            return result;
        }

        public static int ReadIntValue(this IEnumerable<string> strings, string ParamName, int defaultValue = 0)
        {
            int result = defaultValue;

            string prop = strings.FirstOrDefault(x => x.StartsWith(ParamName));
            if (prop != null)
            {
                int firstIndex = prop.IndexOf("=");
                string value = prop.Substring(firstIndex + 1);
                if (int.TryParse(value, out int res))
                {
                    result = res;
                }
            }
            return result;
        }

        public static void WriteBoolValue(this List<ConfigFile> settings, string ParamName, bool value)
        {
            ConfigFile config = settings.FirstOrDefault(x => x.PropertyName == ParamName);

            if (config != null)
            {
                settings.First(x => x.PropertyName == ParamName).PropertyValue = value ? "True" : "False";
            }
            else
            {
                settings.Add(new ConfigFile() { PropertyName = ParamName, PropertyValue = value ? "True" : "False" });
            }
        }

        public static void WriteStringValue(this List<ConfigFile> settings, string ParamName, string value)
        {
            ConfigFile config = settings.FirstOrDefault(x => x.PropertyName == ParamName);

            if (config != null)
            {
                settings.First(x => x.PropertyName == ParamName).PropertyValue = value;
            }
            else
            {
                settings.Add(new ConfigFile() { PropertyName = ParamName, PropertyValue = value });
            }
        }

        public static void WriteIntValue(this List<ConfigFile> settings, string ParamName, int value)
        {
            ConfigFile config = settings.FirstOrDefault(x => x.PropertyName == ParamName);

            if (config != null)
            {
                settings.First(x => x.PropertyName == ParamName).PropertyValue = value.ToString();
            }
            else
            {
                settings.Add(new ConfigFile() { PropertyName = ParamName, PropertyValue = value.ToString() });
            }
        }

        public static List<ConfigFile> ToListConfigFile(this IEnumerable<string> strings)
        {

            List<ConfigFile> lst = strings.Select(val =>
            {
                ConfigFile c = new ConfigFile();
                int firstIndex = val.IndexOf("=");
                c.PropertyName = val.Substring(0, firstIndex);
                c.PropertyValue = val.Substring(firstIndex + 1);
                return c;
            }).ToList();

            return lst;
        }

        public static IEnumerable<string> ToEnumerableConfigFile(this List<ConfigFile> lst)
        {
            IEnumerable<string> ret = (IEnumerable<string>)null;

            List<string> strings = new List<string>();

            foreach (ConfigFile c in lst)
            {
                strings.Add(c.PropertyName + "=" + c.PropertyValue);
            }
            ret = (IEnumerable<string>)strings;
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

        public static bool GetBool(this List<ConfigFile> list, string PropertyName, bool defaultValue = false)
        {
            ConfigFile val = list.Find(x => x.PropertyName == PropertyName);

            if (val == null) return defaultValue;
            else
                return list.Find(x => x.PropertyName == PropertyName).PropertyValue.ToBool();
        }

        public static bool ToBool(this string prop)
        {
            if (prop.ToLower() == "true") return true;
            else return false;
        }

        public static int GetInt(this List<ConfigFile> list, string PropertyName, int defaultValue = 0)
        {
            ConfigFile val = list.Find(x => x.PropertyName == PropertyName);

            if (val == null) return defaultValue;
            else
                return list.Find(x => x.PropertyName == PropertyName).PropertyValue.ToInt();
        }

        public static int ToInt(this string prop)
        {
            int val = 0;
            if (int.TryParse(prop, out val))
            {
                return val;
            }
            return 0;
        }

        public static string GetString(this List<ConfigFile> list, string PropertyName, string defaultValue = "")
        {
            ConfigFile val = list.Find(x => x.PropertyName == PropertyName);

            if (val == null) return defaultValue;
            else
                return list.Find(x => x.PropertyName == PropertyName).PropertyValue;
        }

        public static void AppendTextWithTimeStamp(this RichTextBox box, string text, Color color)
        {
            try
            {

                text = "[" + DateTime.Now.ToString("u") + "] " + text + "\n";
                box.SelectionStart = box.TextLength;
                box.SelectionLength = 0;

                box.SelectionColor = color;
                box.AppendText(text);
                box.SelectionColor = box.ForeColor;
            }
            catch (Exception)
            {

            }
        }
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            text = text + "\n";
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                if (enumerator.MoveNext())
                    return false;
            }
            return true;
        }

        public static bool HasOne<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            int num = 0;
            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    if (++num > 1)
                        return false;
                }
            }
            return num == 1;
        }
    }
}
