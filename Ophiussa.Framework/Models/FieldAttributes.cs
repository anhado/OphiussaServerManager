using System;

namespace OphiussaFramework {
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Struct)]
    public class FieldAttributes : Attribute {
        public bool   PrimaryKey    { get; set; } = false;
        public bool   AutoIncrement { get; set; } = false;
        public bool   Ignore        { get; set; } = false;
        public string DataType      { get; set; }
    }
}