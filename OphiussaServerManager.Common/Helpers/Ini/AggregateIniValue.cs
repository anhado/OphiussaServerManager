using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using OphiussaServerManager.Common.Ini;

namespace OphiussaServerManager.Common {
    public abstract class AggregateIniValue : DependencyObject {
        protected const    char               Delimiter  = ',';
        protected readonly List<PropertyInfo> Properties = new List<PropertyInfo>();

        public T Duplicate<T>() where T : AggregateIniValue, new() {
            GetPropertyInfos(true);
            var obj = new T();
            foreach (var propertyInfo in Properties.Where(prop => prop.CanWrite))
                propertyInfo.SetValue(obj, propertyInfo.GetValue(this));
            return obj;
        }

        public static T FromIniValue<T>(string value) where T : AggregateIniValue, new() {
            var obj = new T();
            obj.InitializeFromIniValue(value);
            return obj;
        }

        protected void GetPropertyInfos(bool allProperties = false) {
            if (Properties.Count != 0)
                return;
            if (allProperties)
                Properties.AddRange(GetType().GetProperties());
            else
                Properties.AddRange(GetType().GetProperties().Where(p => p.GetCustomAttribute(typeof(AggregateIniValueEntryAttribute)) != null));
        }

        public abstract string GetSortKey();

        public virtual void InitializeFromIniValue(string value) {
            if (string.IsNullOrWhiteSpace(value))
                return;
            GetPropertyInfos();
            if (Properties.Count == 0)
                return;
            value = value.Split(new char[1] { '=' }, 2)[1].Trim('(', ')', ' ');
            string str1     = value;
            char[] chArray1 = new char[1] { ',' };
            foreach (string str2 in str1.Split(chArray1)) {
                char[]   chArray2 = new char[1] { '=' };
                string[] strArray = str2.Split(chArray2);
                if (strArray.Length == 2) {
                    string key       = strArray[0].Trim();
                    string str3      = strArray[1].Trim();
                    var    property1 = Properties.FirstOrDefault(p => string.Equals(p.Name, key, StringComparison.OrdinalIgnoreCase));
                    if (property1 != null) {
                        Utils.SetPropertyValue(str3, this, property1);
                    }
                    else {
                        var property2 = Properties.FirstOrDefault(f => f.GetCustomAttributes(typeof(AggregateIniValueEntryAttribute), false).OfType<AggregateIniValueEntryAttribute>().Any(a => string.Equals(a.Key, key, StringComparison.OrdinalIgnoreCase)));
                        if (property2 != null)
                            Utils.SetPropertyValue(str3, this, property2);
                    }
                }
            }
        }

        public abstract bool IsEquivalent(AggregateIniValue other);

        public virtual bool ShouldSave() {
            return true;
        }

        public static object SortKeySelector(AggregateIniValue arg) {
            return arg.GetSortKey();
        }

        public virtual string ToIniValue() {
            GetPropertyInfos();
            if (Properties.Count == 0)
                return string.Empty;
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("(");
            string str1 = "";
            foreach (var property in Properties) {
                stringBuilder.Append(str1);
                var    valueEntryAttribute = property.GetCustomAttributes(typeof(AggregateIniValueEntryAttribute), false).OfType<AggregateIniValueEntryAttribute>().FirstOrDefault();
                string str2                = string.IsNullOrWhiteSpace(valueEntryAttribute?.Key) ? property.Name : valueEntryAttribute.Key;
                string propertyValue       = Utils.GetPropertyValue(property.GetValue(this), property, valueEntryAttribute == null || valueEntryAttribute.QuotedString);
                if (valueEntryAttribute == null || !valueEntryAttribute.ExcludeIfEmpty || !string.IsNullOrWhiteSpace(propertyValue)) {
                    if (valueEntryAttribute == null || !valueEntryAttribute.ExcludePropertyName)
                        stringBuilder.Append(str2 + "=");
                    stringBuilder.Append(propertyValue ?? "");
                    str1 = ','.ToString();
                }
            }

            stringBuilder.Append(")");
            return stringBuilder.ToString();
        }

        public override string ToString() {
            return ToIniValue();
        }

        protected virtual void FromComplexIniValue(string value) {
            if (string.IsNullOrWhiteSpace(value))
                return;
            GetPropertyInfos();
            if (Properties.Count == 0)
                return;
            var source = SplitCollectionValues(value.Trim(' '), ',');
            foreach (var property in Properties) {
                var    valueEntryAttribute = property.GetCustomAttributes(typeof(AggregateIniValueEntryAttribute), false).OfType<AggregateIniValueEntryAttribute>().FirstOrDefault();
                string propertyName        = string.IsNullOrWhiteSpace(valueEntryAttribute?.Key) ? property.Name : valueEntryAttribute.Key;
                string str                 = source.FirstOrDefault(p => p.StartsWith(propertyName + "="));
                if (str != null) {
                    string valueString = str.Split(new char[1] { '=' }, 2)[1].Trim(',', ' ');
                    if (valueEntryAttribute != null && valueEntryAttribute.ValueWithinBrackets) {
                        if (valueString.StartsWith("("))
                            valueString = valueString.Substring(1);
                        if (valueString.EndsWith(")"))
                            valueString = valueString.Substring(0, valueString.Length - 1);
                    }

                    if (property.GetValue(this) is IIniValuesCollection valuesCollection) {
                        var strings = SplitCollectionValues(valueString, ',').Where(v => !string.IsNullOrWhiteSpace(v));
                        if (valueEntryAttribute != null && valueEntryAttribute.ListValueWithinBrackets)
                            strings = strings.Select(v => v.Substring(1)).Select(v => v.Substring(0, v.Length - 1));
                        valuesCollection.FromIniValues(strings);
                    }
                    else {
                        Utils.SetPropertyValue(valueString, this, property);
                    }
                }
            }
        }

        protected virtual string ToComplexIniValue(bool resultWithinBrackets) {
            GetPropertyInfos();
            if (Properties.Count == 0)
                return string.Empty;
            var stringBuilder = new StringBuilder();
            if (resultWithinBrackets)
                stringBuilder.Append("(");
            string str1 = "";
            foreach (var property in Properties) {
                var    valueEntryAttribute = property.GetCustomAttributes(typeof(AggregateIniValueEntryAttribute), false).OfType<AggregateIniValueEntryAttribute>().FirstOrDefault();
                string str2                = string.IsNullOrWhiteSpace(valueEntryAttribute?.Key) ? property.Name : valueEntryAttribute.Key;
                object obj                 = property.GetValue(this);
                char   ch;
                if (obj is IIniValuesCollection valuesCollection) {
                    stringBuilder.Append(str1);
                    stringBuilder.Append(str2 + "=");
                    if (valueEntryAttribute != null && valueEntryAttribute.ValueWithinBrackets)
                        stringBuilder.Append("(");
                    var    iniValues = valuesCollection.ToIniValues();
                    string str3      = "";
                    foreach (string str4 in iniValues) {
                        stringBuilder.Append(str3);
                        if (valueEntryAttribute != null && valueEntryAttribute.ListValueWithinBrackets)
                            stringBuilder.Append("(" + str4 + ")");
                        else
                            stringBuilder.Append(str4);
                        ch   = ',';
                        str3 = ch.ToString();
                    }

                    if (valueEntryAttribute != null && valueEntryAttribute.ValueWithinBrackets)
                        stringBuilder.Append(")");
                    ch   = ',';
                    str1 = ch.ToString();
                }
                else if ((valueEntryAttribute == null || !valueEntryAttribute.ExcludeIfEmpty || !(obj is string) || !string.IsNullOrWhiteSpace(obj.ToString())) && (valueEntryAttribute == null || !valueEntryAttribute.ExcludeIfFalse || !(obj is bool flag) || flag)) {
                    string propertyValue = Utils.GetPropertyValue(obj, property, valueEntryAttribute == null || valueEntryAttribute.QuotedString);
                    stringBuilder.Append(str1);
                    if (valueEntryAttribute == null || !valueEntryAttribute.ExcludePropertyName)
                        stringBuilder.Append(str2 + "=");
                    if (valueEntryAttribute != null && valueEntryAttribute.ValueWithinBrackets)
                        stringBuilder.Append("(");
                    stringBuilder.Append(propertyValue);
                    if (valueEntryAttribute != null && valueEntryAttribute.ValueWithinBrackets)
                        stringBuilder.Append(")");
                    ch   = ',';
                    str1 = ch.ToString();
                }
            }

            if (resultWithinBrackets)
                stringBuilder.Append(")");
            return stringBuilder.ToString();
        }

        protected IEnumerable<string> SplitCollectionValues(string valueString, char delimiter) {
            if (string.IsNullOrWhiteSpace(valueString))
                return new string[0];
            string source = valueString.Trim();
            if (source.Count(c => c.Equals(delimiter)) == 0)
                return new string[1] { source };
            var stringList = new List<string>();
            int num        = 0;
            int startIndex = 0;
            for (int index = 0; index < source.Length; ++index) {
                char ch = source[index];
                switch (ch) {
                    case '(':
                        ++num;
                        break;
                    case ')':
                        --num;
                        break;
                    default:
                        if (ch == delimiter && num == 0) {
                            stringList.Add(source.Substring(startIndex, index - startIndex));
                            startIndex = index + 1;
                        }

                        break;
                }
            }

            stringList.Add(source.Substring(startIndex));
            return stringList;
        }

        public void Update(AggregateIniValue other) {
            if (other == null)
                return;
            GetPropertyInfos();
            other.GetPropertyInfos();
            foreach (var property in Properties) {
                var propInfo     = property;
                var propertyInfo = other.Properties.FirstOrDefault(p => p.Name.Equals(propInfo.Name, StringComparison.OrdinalIgnoreCase));
                if (!(propertyInfo == null))
                    Utils.SetPropertyValue(Utils.GetPropertyValue(propertyInfo.GetValue(other), propInfo), this, propInfo);
            }
        }
    }
}