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

        public static List<ConfigFile> ToListConfigFile(this IEnumerable<string> strings)
        {

            List<ConfigFile> lst = strings.Select(val =>
            {
                ConfigFile c = new ConfigFile();
                int firstIndex = val.IndexOf("=");
                c.Value = val.Substring(0, firstIndex);
                c.Property = val.Substring(firstIndex + 1);
                return c;
            }).ToList();

            return lst;
        }

        public static bool GetBool(this List<ConfigFile> list,string PropertyName, bool defaultValue = false)
        {
            ConfigFile val = list.Find(x => x.Value == PropertyName);

            if (val == null) return defaultValue;
            else
                return list.Find(x => x.Value == PropertyName).Property.ToBool();
        }

        public static bool ToBool(this string prop)
        {
            if (prop.ToLower() == "true") return true;
            else return false;
        }

        public static int GetInt(this List<ConfigFile> list, string PropertyName, int defaultValue = 0)
        {
            ConfigFile val = list.Find(x => x.Value == PropertyName);

            if (val == null) return defaultValue;
            else
                return list.Find(x => x.Value == PropertyName).Property.ToInt();
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
            ConfigFile val = list.Find(x => x.Value == PropertyName);

            if (val == null) return defaultValue;
            else
                return list.Find(x => x.Value == PropertyName).Property;
        }
         
        public static void AppendTextWithTimeStamp(this RichTextBox box, string text, Color color)
        {
            text = "[" + DateTime.Now.ToString("u") + "] " + text + "\n";
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
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
