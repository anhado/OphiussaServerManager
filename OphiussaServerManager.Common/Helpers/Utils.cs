using OphiussaServerManager.Common.Ini;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common
{
    public static class Utils
    {
        public const string DEFAULT_CULTURE_CODE = "en-US";
        public static void ExecuteAsAdmin(string exeName, string parameters, bool wait = true)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.FileName = exeName;
                startInfo.Verb = "runas";
                //MLHIDE
                startInfo.Arguments = parameters;
                startInfo.ErrorDialog = true;

                Process process = System.Diagnostics.Process.Start(startInfo);
                if (wait) process.WaitForExit();
            }
            catch (Win32Exception ex)
            {
                throw new Exception("ExecuteAsAdmin:" + ex.Message);
            }
        }

        public static bool IsAValidFolder(string InitialFolder, List<string> FolderList, bool isFiles = false)
        {
            List<string> folders = System.IO.Directory.GetDirectories(InitialFolder).ToList<string>();
            List<string> OnlyLast = new List<string>();

            folders.ForEach(folder => { OnlyLast.Add(new DirectoryInfo(folder).Name); });


            List<string> notExists = FolderList.FindAll(x => !OnlyLast.Contains(x)).ToList();


            if (notExists.Count == 0)
            {
                return true;
            }
            return false;
        }

        public static string GetPropertyValue(object value, PropertyInfo property, bool quotedString = true) => !(property.PropertyType == typeof(float)) ? (!(property.PropertyType == typeof(string)) ? Convert.ToString(value, (IFormatProvider)CultureInfo.GetCultureInfo("en-US")) : (!quotedString ? string.Format("{0}", value) : string.Format("\"{0}\"", value))) : ((float)value).ToString("0.000000####", (IFormatProvider)CultureInfo.GetCultureInfo("en-US"));

        public static string GetPropertyValue(
          object value,
          PropertyInfo property,
          BaseIniFileEntryAttribute attribute)
        {
            string propertyValue;
            if (property.PropertyType == typeof(int) || property.PropertyType == typeof(NullableValue<int>))
                propertyValue = Convert.ToString(value, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"));
            else if (property.PropertyType == typeof(long) || property.PropertyType == typeof(NullableValue<long>))
                propertyValue = Convert.ToString(value, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"));
            else if (property.PropertyType == typeof(float) || property.PropertyType == typeof(NullableValue<float>))
                propertyValue = ((float)value).ToString("0.000000####", (IFormatProvider)CultureInfo.GetCultureInfo("en-US"));
            else if (property.PropertyType == typeof(bool))
            {
                bool flag = (bool)value;
                if (attribute.InvertBoolean)
                    flag = !flag;
                propertyValue = !attribute.WriteBooleanAsInteger ? flag.ToString((IFormatProvider)CultureInfo.GetCultureInfo("en-US")) : (flag ? "1" : "0");
            }
            else
                propertyValue = Convert.ToString(value, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"));
            return propertyValue;
        }

        public static void SetPropertyValue(string value, object obj, PropertyInfo property)
        {
            if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
            {
                bool result;
                bool.TryParse(value, out result);
                property.SetValue(obj, (object)result);
            }
            else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
            {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                int result;
                int.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                property.SetValue(obj, (object)result);
            }
            else if (property.PropertyType == typeof(long) || property.PropertyType == typeof(long?))
            {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                long result;
                long.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                property.SetValue(obj, (object)result);
            }
            else if (property.PropertyType == typeof(float) || property.PropertyType == typeof(float?))
            {
                float result;
                float.TryParse(value.Replace("f", ""), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                property.SetValue(obj, (object)result);
            }
            else if (property.PropertyType == typeof(NullableValue<int>))
            {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                int result;
                int.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                NullableValue<int> nullableValue = property.GetValue(obj) as NullableValue<int>;
                property.SetValue(obj, (object)nullableValue.SetValue(true, result));
            }
            else if (property.PropertyType == typeof(NullableValue<long>))
            {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                long result;
                long.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                NullableValue<long> nullableValue = property.GetValue(obj) as NullableValue<long>;
                property.SetValue(obj, (object)nullableValue.SetValue(true, result));
            }
            else if (property.PropertyType == typeof(NullableValue<float>))
            {
                float result;
                float.TryParse(value.Replace("f", ""), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                NullableValue<float> nullableValue = property.GetValue(obj) as NullableValue<float>;
                property.SetValue(obj, (object)nullableValue.SetValue(true, result));
            }
            else if (property.PropertyType.IsSubclassOf(typeof(AggregateIniValue)))
            {
                if (!(property.GetValue(obj) is AggregateIniValue aggregateIniValue))
                    return;
                aggregateIniValue.InitializeFromINIValue(value);
            }
            else
            {
                object obj1 = Convert.ChangeType((object)value, property.PropertyType, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"));
                if (obj1 is string)
                    obj1 = (object)(obj1 as string).Trim('"');
                property.SetValue(obj, obj1);
            }
        }

        public static bool SetPropertyValue(
          string value,
          object obj,
          PropertyInfo property,
          BaseIniFileEntryAttribute attribute)
        {
            if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
            {
                bool result;
                if (attribute.WriteBooleanAsInteger)
                    result = value.Equals("1", StringComparison.OrdinalIgnoreCase);
                else
                    bool.TryParse(value, out result);
                if (attribute.InvertBoolean)
                    result = !result;
                property.SetValue(obj, (object)result);
                return true;
            }
            if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
            {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                int result;
                int.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                property.SetValue(obj, (object)result);
                return true;
            }
            if (property.PropertyType == typeof(long) || property.PropertyType == typeof(long?))
            {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                long result;
                long.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                property.SetValue(obj, (object)result);
                return true;
            }
            if (property.PropertyType == typeof(float) || property.PropertyType == typeof(float?))
            {
                float result;
                float.TryParse(value.Replace("f", ""), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                property.SetValue(obj, (object)result);
                return true;
            }
            if (property.PropertyType == typeof(NullableValue<int>))
            {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                int result;
                int.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                NullableValue<int> nullableValue = property.GetValue(obj) as NullableValue<int>;
                property.SetValue(obj, (object)nullableValue.SetValue(true, result));
                return true;
            }
            if (property.PropertyType == typeof(NullableValue<long>))
            {
                string decimalSeparator = CultureInfo.GetCultureInfo("en-US").NumberFormat.NumberDecimalSeparator;
                string s = value;
                if (s.Contains(decimalSeparator))
                    s = s.Substring(0, s.IndexOf(decimalSeparator));
                long result;
                long.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                NullableValue<long> nullableValue = property.GetValue(obj) as NullableValue<long>;
                property.SetValue(obj, (object)nullableValue.SetValue(true, result));
                return true;
            }
            if (property.PropertyType == typeof(NullableValue<float>))
            {
                float result;
                float.TryParse(value.Replace("f", ""), NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider)CultureInfo.GetCultureInfo("en-US"), out result);
                NullableValue<float> nullableValue = property.GetValue(obj) as NullableValue<float>;
                property.SetValue(obj, (object)nullableValue.SetValue(true, result));
                return true;
            }
            if (!property.PropertyType.IsSubclassOf(typeof(AggregateIniValue)))
                return false;
            if (property.GetValue(obj) is AggregateIniValue aggregateIniValue)
                aggregateIniValue.InitializeFromINIValue(value);
            return true;
        }
    }

}