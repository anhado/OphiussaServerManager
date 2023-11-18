using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Ini
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public abstract class BaseIniFileEntryAttribute : Attribute
    {
        public string Key;
        public object WriteIfNotValue;
        public bool InvertBoolean;
        public bool WriteBoolValueIfNonEmpty;
        public bool WriteBooleanAsInteger;
        public bool ClearSection;
        public bool ClearSectionIfEmpty;
        public QuotedStringType QuotedString;
        public string ConditionedOn;
        public bool Multiline;
        public string MultilineSeparator;
        public string ClearWhenOff;
        public bool IsCustom;

        protected BaseIniFileEntryAttribute(Enum file, Enum section, Enum category, string key = "")
        {
            this.File = file;
            this.Section = section;
            this.Category = category;
            this.Key = key;
            this.QuotedString = QuotedStringType.False;
            this.Multiline = false;
            this.MultilineSeparator = "\\n";
        }

        public Enum File { get; set; }

        public Enum Section { get; set; }

        public Enum Category { get; set; }
    }
}
