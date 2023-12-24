using System;

namespace OphiussaServerManager.Common.Ini {
    [AttributeUsage(AttributeTargets.Property)]
    public class AggregateIniValueEntryAttribute : Attribute {
        public int    BracketsAroundValueDelimiter = 1;
        public bool   ExcludeIfEmpty;
        public bool   ExcludeIfFalse;
        public bool   ExcludePropertyName;
        public string Key;
        public bool   ListValueWithinBrackets;
        public bool   QuotedString = true;
        public bool   ValueWithinBrackets;

        public AggregateIniValueEntryAttribute(string key = "") {
            Key = key;
        }
    }
}