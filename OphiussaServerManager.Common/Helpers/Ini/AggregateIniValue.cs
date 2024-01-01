using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using OphiussaServerManager.Common.Ini;

namespace OphiussaServerManager.Common {
    public abstract class AggregateIniValue {
        protected const char DELIMITER = ',';

        protected readonly List<PropertyInfo> Properties = new List<PropertyInfo>();

        public T Duplicate<T>() where T : AggregateIniValue, new() {
            GetPropertyInfos(true);

            var result = new T();
            foreach (var prop in Properties.Where(prop => prop.CanWrite)) prop.SetValue(result, prop.GetValue(this));

            return result;
        }

        public static T FromINIValue<T>(string value) where T : AggregateIniValue, new() {
            var result = new T();
            result.InitializeFromINIValue(value);
            return result;
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

        public virtual void InitializeFromINIValue(string value) {
            if (string.IsNullOrWhiteSpace(value))
                return;

            GetPropertyInfos();
            if (Properties.Count == 0)
                return;

            string[] kvPair = value.Split(new[] { '=' }, 2);
            value = kvPair[1].Trim('(', ')', ' ');
            string[] pairs = value.Split(DELIMITER);

            foreach (string pair in pairs) {
                kvPair = pair.Split('=');
                if (kvPair.Length != 2)
                    continue;

                string key      = kvPair[0].Trim();
                string val      = kvPair[1].Trim();
                var    propInfo = Properties.FirstOrDefault(p => string.Equals(p.Name, key, StringComparison.OrdinalIgnoreCase));
                if (propInfo != null) {
                    Utils.SetPropertyValue(val, this, propInfo);
                }
                else {
                    propInfo = Properties.FirstOrDefault(f => f.GetCustomAttributes(typeof(AggregateIniValueEntryAttribute), false).OfType<AggregateIniValueEntryAttribute>().Any(a => string.Equals(a.Key, key, StringComparison.OrdinalIgnoreCase)));
                    if (propInfo != null)
                        Utils.SetPropertyValue(val, this, propInfo);
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

        public virtual string ToINIValue() {
            GetPropertyInfos();
            if (Properties.Count == 0)
                return string.Empty;

            var result = new StringBuilder();
            result.Append("(");

            string delimiter = "";
            foreach (var prop in Properties) {
                result.Append(delimiter);

                var    attr     = prop.GetCustomAttributes(typeof(AggregateIniValueEntryAttribute), false).OfType<AggregateIniValueEntryAttribute>().FirstOrDefault();
                string propName = string.IsNullOrWhiteSpace(attr?.Key) ? prop.Name : attr.Key;

                object val       = prop.GetValue(this);
                string propValue = Utils.GetPropertyValue(val, prop, attr?.QuotedString ?? true);

                if ((attr?.ExcludeIfEmpty ?? false) && string.IsNullOrWhiteSpace(propValue)) {
                    Console.WriteLine($"{propName} skipped, ExcludeIfEmpty = true and value is empty");
                }
                else {
                    if (!(attr?.ExcludePropertyName ?? false))
                        result.Append($"{propName}=");
                    result.Append($"{propValue}");

                    delimiter = DELIMITER.ToString();
                }
            }

            result.Append(")");
            return result.ToString();
        }

        public override string ToString() {
            return ToINIValue();
        }

        protected virtual void FromComplexINIValue(string value) {
            if (string.IsNullOrWhiteSpace(value))
                return;

            GetPropertyInfos();
            if (Properties.Count == 0)
                return;

            string kvValue = value.Trim(' ');

            var propertyValues = SplitCollectionValues(kvValue, DELIMITER);

            foreach (var property in Properties) {
                var    attr         = property.GetCustomAttributes(typeof(AggregateIniValueEntryAttribute), false).OfType<AggregateIniValueEntryAttribute>().FirstOrDefault();
                string propertyName = string.IsNullOrWhiteSpace(attr?.Key) ? property.Name : attr.Key;

                string propertyValue = propertyValues.FirstOrDefault(p => p.StartsWith($"{propertyName}="));
                if (propertyValue == null)
                    continue;

                string[] kvPropertyPair  = propertyValue.Split(new[] { '=' }, 2);
                string   kvPropertyValue = kvPropertyPair[1].Trim(DELIMITER, ' ');

                if (attr?.ValueWithinBrackets ?? false) {
                    if (kvPropertyValue.StartsWith("("))
                        kvPropertyValue = kvPropertyValue.Substring(1);
                    if (kvPropertyValue.EndsWith(")"))
                        kvPropertyValue = kvPropertyValue.Substring(0, kvPropertyValue.Length - 1);
                }

                if (property.GetValue(this) is IIniValuesCollection collection) {
                    var values = SplitCollectionValues(kvPropertyValue, DELIMITER)
                       .Where(v => !string.IsNullOrWhiteSpace(v));

                    if (attr?.ListValueWithinBrackets ?? false) {
                        values = values.Select(v => v.Substring(1));
                        values = values.Select(v => v.Substring(0, v.Length - 1));
                    }

                    collection.FromIniValues(values);
                }
                else {
                    Utils.SetPropertyValue(kvPropertyValue, this, property);
                }
            }
        }

        protected virtual string ToComplexINIValue(bool resultWithinBrackets) {
            GetPropertyInfos();
            if (Properties.Count == 0)
                return string.Empty;

            var result = new StringBuilder();
            if (resultWithinBrackets)
                result.Append("(");

            string delimiter = "";
            foreach (var prop in Properties) {
                var    attr     = prop.GetCustomAttributes(typeof(AggregateIniValueEntryAttribute), false).OfType<AggregateIniValueEntryAttribute>().FirstOrDefault();
                string propName = string.IsNullOrWhiteSpace(attr?.Key) ? prop.Name : attr.Key;
                object val      = prop.GetValue(this);

                var collection = val as IIniValuesCollection;
                if (collection != null) {
                    result.Append(delimiter);
                    result.Append($"{propName}=");
                    if (attr?.ValueWithinBrackets ?? false)
                        result.Append("(");

                    var    iniVals    = collection.ToIniValues();
                    string delimiter2 = "";
                    foreach (string iniVal in iniVals) {
                        result.Append(delimiter2);
                        if (attr?.ListValueWithinBrackets ?? false)
                            result.Append($"({iniVal})");
                        else
                            result.Append(iniVal);

                        delimiter2 = DELIMITER.ToString();
                    }

                    if (attr?.ValueWithinBrackets ?? false)
                        result.Append(")");

                    delimiter = DELIMITER.ToString();
                }
                else {
                    if ((attr?.ExcludeIfEmpty ?? false) && val is string && string.IsNullOrWhiteSpace(val.ToString())) {
                        Console.WriteLine($"{propName} skipped, ExcludeIfEmpty = true and value is null or empty");
                    }
                    else if ((attr?.ExcludeIfFalse ?? false) && val is bool && !(bool)val) {
                        Console.WriteLine($"{propName} skipped, ExcludeIfFalse = true and value is false");
                    }
                    else {
                        string propValue = Utils.GetPropertyValue(val, prop, attr?.QuotedString ?? true);

                        result.Append(delimiter);
                        if (!(attr?.ExcludePropertyName ?? false))
                            result.Append($"{propName}=");
                        if (attr?.ValueWithinBrackets ?? false)
                            result.Append("(");

                        result.Append(propValue);

                        if (attr?.ValueWithinBrackets ?? false)
                            result.Append(")");

                        delimiter = DELIMITER.ToString();
                    }
                }
            }

            if (resultWithinBrackets)
                result.Append(")");
            return result.ToString();
        }

        protected IEnumerable<string> SplitCollectionValues(string valueString, char delimiter) {
            if (string.IsNullOrWhiteSpace(valueString))
                return new string[0];

            // string any leading or trailing spaces
            string tempString = valueString.Trim();

            // check if any delimiters
            int total1 = tempString.Count(c => c.Equals(delimiter));
            if (total1 == 0)
                return new[] { tempString };

            var result = new List<string>();

            int bracketCount = 0;
            int startIndex   = 0;
            for (int index = 0; index < tempString.Length; index++) {
                char charValue = tempString[index];
                if (charValue == '(') {
                    bracketCount++;
                    continue;
                }

                if (charValue == ')') {
                    bracketCount--;
                    continue;
                }

                if (charValue != delimiter || bracketCount != 0)
                    continue;

                result.Add(tempString.Substring(startIndex, index - startIndex));

                startIndex = index + 1;
            }

            result.Add(tempString.Substring(startIndex));

            return result;
        }

        public void Update(AggregateIniValue other) {
            if (other == null)
                return;

            GetPropertyInfos();
            other.GetPropertyInfos();

            foreach (var propInfo in Properties) {
                var otherPropInfo = other.Properties.FirstOrDefault(p => p.Name.Equals(propInfo.Name, StringComparison.OrdinalIgnoreCase));
                if (otherPropInfo == null)
                    continue;

                object val       = otherPropInfo.GetValue(other);
                string propValue = Utils.GetPropertyValue(val, propInfo);

                Utils.SetPropertyValue(propValue, this, propInfo);
            }
        }
    }
}