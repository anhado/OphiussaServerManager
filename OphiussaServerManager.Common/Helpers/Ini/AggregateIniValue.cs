using OphiussaServerManager.Common.Ini;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OphiussaServerManager.Common
{
    public abstract class AggregateIniValue : DependencyObject
    {
        protected const char DELIMITER = ',';
        protected readonly List<PropertyInfo> Properties = new List<PropertyInfo>();

        public T Duplicate<T>() where T : AggregateIniValue, new()
        {
            this.GetPropertyInfos(true);
            T obj = new T();
            foreach (PropertyInfo propertyInfo in this.Properties.Where<PropertyInfo>((Func<PropertyInfo, bool>)(prop => prop.CanWrite)))
                propertyInfo.SetValue((object)obj, propertyInfo.GetValue((object)this));
            return obj;
        }

        public static T FromINIValue<T>(string value) where T : AggregateIniValue, new()
        {
            T obj = new T();
            obj.InitializeFromINIValue(value);
            return obj;
        }

        protected void GetPropertyInfos(bool allProperties = false)
        {
            if (this.Properties.Count != 0)
                return;
            if (allProperties)
                this.Properties.AddRange((IEnumerable<PropertyInfo>)this.GetType().GetProperties());
            else
                this.Properties.AddRange(((IEnumerable<PropertyInfo>)this.GetType().GetProperties()).Where<PropertyInfo>((Func<PropertyInfo, bool>)(p => p.GetCustomAttribute(typeof(AggregateIniValueEntryAttribute)) != null)));
        }

        public abstract string GetSortKey();

        public virtual void InitializeFromINIValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return;
            this.GetPropertyInfos();
            if (this.Properties.Count == 0)
                return;
            value = value.Split(new char[1] { '=' }, 2)[1].Trim('(', ')', ' ');
            string str1 = value;
            char[] chArray1 = new char[1] { ',' };
            foreach (string str2 in str1.Split(chArray1))
            {
                char[] chArray2 = new char[1] { '=' };
                string[] strArray = str2.Split(chArray2);
                if (strArray.Length == 2)
                {
                    string key = strArray[0].Trim();
                    string str3 = strArray[1].Trim();
                    PropertyInfo property1 = this.Properties.FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(p => string.Equals(p.Name, key, StringComparison.OrdinalIgnoreCase)));
                    if (property1 != (PropertyInfo)null)
                    {
                        Utils.SetPropertyValue(str3, (object)this, property1);
                    }
                    else
                    {
                        PropertyInfo property2 = this.Properties.FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(f => f.GetCustomAttributes(typeof(AggregateIniValueEntryAttribute), false).OfType<AggregateIniValueEntryAttribute>().Any<AggregateIniValueEntryAttribute>((Func<AggregateIniValueEntryAttribute, bool>)(a => string.Equals(a.Key, key, StringComparison.OrdinalIgnoreCase)))));
                        if (property2 != (PropertyInfo)null)
                            Utils.SetPropertyValue(str3, (object)this, property2);
                    }
                }
            }
        }

        public abstract bool IsEquivalent(AggregateIniValue other);

        public virtual bool ShouldSave() => true;

        public static object SortKeySelector(AggregateIniValue arg) => (object)arg.GetSortKey();

        public virtual string ToINIValue()
        {
            this.GetPropertyInfos();
            if (this.Properties.Count == 0)
                return string.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("(");
            string str1 = "";
            foreach (PropertyInfo property in this.Properties)
            {
                stringBuilder.Append(str1);
                AggregateIniValueEntryAttribute valueEntryAttribute = property.GetCustomAttributes(typeof(AggregateIniValueEntryAttribute), false).OfType<AggregateIniValueEntryAttribute>().FirstOrDefault<AggregateIniValueEntryAttribute>();
                string str2 = string.IsNullOrWhiteSpace(valueEntryAttribute?.Key) ? property.Name : valueEntryAttribute.Key;
                string propertyValue = Utils.GetPropertyValue(property.GetValue((object)this), property, valueEntryAttribute == null || valueEntryAttribute.QuotedString);
                if (valueEntryAttribute == null || !valueEntryAttribute.ExcludeIfEmpty || !string.IsNullOrWhiteSpace(propertyValue))
                {
                    if (valueEntryAttribute == null || !valueEntryAttribute.ExcludePropertyName)
                        stringBuilder.Append(str2 + "=");
                    stringBuilder.Append(propertyValue ?? "");
                    str1 = ','.ToString();
                }
            }
            stringBuilder.Append(")");
            return stringBuilder.ToString();
        }

        public override string ToString() => this.ToINIValue();

        protected virtual void FromComplexINIValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return;
            this.GetPropertyInfos();
            if (this.Properties.Count == 0)
                return;
            IEnumerable<string> source = this.SplitCollectionValues(value.Trim(' '), ',');
            foreach (PropertyInfo property in this.Properties)
            {
                AggregateIniValueEntryAttribute valueEntryAttribute = property.GetCustomAttributes(typeof(AggregateIniValueEntryAttribute), false).OfType<AggregateIniValueEntryAttribute>().FirstOrDefault<AggregateIniValueEntryAttribute>();
                string propertyName = string.IsNullOrWhiteSpace(valueEntryAttribute?.Key) ? property.Name : valueEntryAttribute.Key;
                string str = source.FirstOrDefault<string>((Func<string, bool>)(p => p.StartsWith(propertyName + "=")));
                if (str != null)
                {
                    string valueString = str.Split(new char[1] { '=' }, 2)[1].Trim(',', ' ');
                    if (valueEntryAttribute != null && valueEntryAttribute.ValueWithinBrackets)
                    {
                        if (valueString.StartsWith("("))
                            valueString = valueString.Substring(1);
                        if (valueString.EndsWith(")"))
                            valueString = valueString.Substring(0, valueString.Length - 1);
                    }
                    if (property.GetValue((object)this) is IIniValuesCollection valuesCollection)
                    {
                        IEnumerable<string> strings = this.SplitCollectionValues(valueString, ',').Where<string>((Func<string, bool>)(v => !string.IsNullOrWhiteSpace(v)));
                        if (valueEntryAttribute != null && valueEntryAttribute.ListValueWithinBrackets)
                            strings = strings.Select<string, string>((Func<string, string>)(v => v.Substring(1))).Select<string, string>((Func<string, string>)(v => v.Substring(0, v.Length - 1)));
                        valuesCollection.FromIniValues(strings);
                    }
                    else
                        Utils.SetPropertyValue(valueString, (object)this, property);
                }
            }
        }

        protected virtual string ToComplexINIValue(bool resultWithinBrackets)
        {
            this.GetPropertyInfos();
            if (this.Properties.Count == 0)
                return string.Empty;
            StringBuilder stringBuilder = new StringBuilder();
            if (resultWithinBrackets)
                stringBuilder.Append("(");
            string str1 = "";
            foreach (PropertyInfo property in this.Properties)
            {
                AggregateIniValueEntryAttribute valueEntryAttribute = property.GetCustomAttributes(typeof(AggregateIniValueEntryAttribute), false).OfType<AggregateIniValueEntryAttribute>().FirstOrDefault<AggregateIniValueEntryAttribute>();
                string str2 = string.IsNullOrWhiteSpace(valueEntryAttribute?.Key) ? property.Name : valueEntryAttribute.Key;
                object obj = property.GetValue((object)this);
                char ch;
                if (obj is IIniValuesCollection valuesCollection)
                {
                    stringBuilder.Append(str1);
                    stringBuilder.Append(str2 + "=");
                    if (valueEntryAttribute != null && valueEntryAttribute.ValueWithinBrackets)
                        stringBuilder.Append("(");
                    IEnumerable<string> iniValues = valuesCollection.ToIniValues();
                    string str3 = "";
                    foreach (string str4 in iniValues)
                    {
                        stringBuilder.Append(str3);
                        if (valueEntryAttribute != null && valueEntryAttribute.ListValueWithinBrackets)
                            stringBuilder.Append("(" + str4 + ")");
                        else
                            stringBuilder.Append(str4);
                        ch = ',';
                        str3 = ch.ToString();
                    }
                    if (valueEntryAttribute != null && valueEntryAttribute.ValueWithinBrackets)
                        stringBuilder.Append(")");
                    ch = ',';
                    str1 = ch.ToString();
                }
                else if ((valueEntryAttribute == null || !valueEntryAttribute.ExcludeIfEmpty || !(obj is string) || !string.IsNullOrWhiteSpace(obj.ToString())) && (valueEntryAttribute == null || !valueEntryAttribute.ExcludeIfFalse || !(obj is bool flag) || flag))
                {
                    string propertyValue = Utils.GetPropertyValue(obj, property, valueEntryAttribute == null || valueEntryAttribute.QuotedString);
                    stringBuilder.Append(str1);
                    if (valueEntryAttribute == null || !valueEntryAttribute.ExcludePropertyName)
                        stringBuilder.Append(str2 + "=");
                    if (valueEntryAttribute != null && valueEntryAttribute.ValueWithinBrackets)
                        stringBuilder.Append("(");
                    stringBuilder.Append(propertyValue);
                    if (valueEntryAttribute != null && valueEntryAttribute.ValueWithinBrackets)
                        stringBuilder.Append(")");
                    ch = ',';
                    str1 = ch.ToString();
                }
            }
            if (resultWithinBrackets)
                stringBuilder.Append(")");
            return stringBuilder.ToString();
        }

        protected IEnumerable<string> SplitCollectionValues(string valueString, char delimiter)
        {
            if (string.IsNullOrWhiteSpace(valueString))
                return (IEnumerable<string>)new string[0];
            string source = valueString.Trim();
            if (source.Count<char>((Func<char, bool>)(c => c.Equals(delimiter))) == 0)
                return (IEnumerable<string>)new string[1] { source };
            List<string> stringList = new List<string>();
            int num = 0;
            int startIndex = 0;
            for (int index = 0; index < source.Length; ++index)
            {
                char ch = source[index];
                switch (ch)
                {
                    case '(':
                        ++num;
                        break;
                    case ')':
                        --num;
                        break;
                    default:
                        if ((int)ch == (int)delimiter && num == 0)
                        {
                            stringList.Add(source.Substring(startIndex, index - startIndex));
                            startIndex = index + 1;
                            break;
                        }
                        break;
                }
            }
            stringList.Add(source.Substring(startIndex));
            return (IEnumerable<string>)stringList;
        }

        public void Update(AggregateIniValue other)
        {
            if (other == null)
                return;
            this.GetPropertyInfos();
            other.GetPropertyInfos();
            foreach (PropertyInfo property in this.Properties)
            {
                PropertyInfo propInfo = property;
                PropertyInfo propertyInfo = other.Properties.FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>)(p => p.Name.Equals(propInfo.Name, StringComparison.OrdinalIgnoreCase)));
                if (!(propertyInfo == (PropertyInfo)null))
                    Utils.SetPropertyValue(Utils.GetPropertyValue(propertyInfo.GetValue((object)other), propInfo), (object)this, propInfo);
            }
        }
    }
}
