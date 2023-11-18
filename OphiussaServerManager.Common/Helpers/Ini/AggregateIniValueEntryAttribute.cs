using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common.Ini
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AggregateIniValueEntryAttribute : Attribute
    {
        public string Key;
        public bool ValueWithinBrackets;
        public bool ListValueWithinBrackets;
        public int BracketsAroundValueDelimiter = 1;
        public bool ExcludeIfEmpty;
        public bool ExcludeIfFalse;
        public bool QuotedString = true;
        public bool ExcludePropertyName;

        public AggregateIniValueEntryAttribute(string key = "") => this.Key = key;
    }
}
