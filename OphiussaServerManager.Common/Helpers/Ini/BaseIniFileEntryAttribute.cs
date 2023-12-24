using System;

namespace OphiussaServerManager.Common.Ini {
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public abstract class BaseIniFileEntryAttribute : Attribute {
        public bool             ClearSection;
        public bool             ClearSectionIfEmpty;
        public string           ClearWhenOff;
        public string           ConditionedOn;
        public bool             InvertBoolean;
        public bool             IsCustom;
        public string           Key;
        public bool             Multiline;
        public string           MultilineSeparator;
        public QuotedStringType QuotedString;
        public bool             WriteBooleanAsInteger;
        public bool             WriteBoolValueIfNonEmpty;
        public object           WriteIfNotValue;

        protected BaseIniFileEntryAttribute(Enum file, Enum section, Enum category, string key = "") {
            File               = file;
            Section            = section;
            Category           = category;
            Key                = key;
            QuotedString       = QuotedStringType.False;
            Multiline          = false;
            MultilineSeparator = "\\n";
        }

        public Enum File { get; set; }

        public Enum Section { get; set; }

        public Enum Category { get; set; }
    }
}