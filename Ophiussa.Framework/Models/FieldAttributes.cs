using System;
using System.Collections.Generic;

namespace OphiussaFramework {
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Struct)]
    public class FieldAttributes : Attribute {
        public bool       PrimaryKey    { get; set; } = false;
        public bool       AutoIncrement { get; set; } = false;
        public bool       Ignore        { get; set; } = false;
        public string     DataType      { get; set; } 
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Struct)]
    public class FieldDependesOn :Attribute {
        public FieldDependesOn(Type type, string columnName) {
            Type       = type;
            ColumnName = columnName;
        }
        public Type Type { get; set; }
        public string ColumnName { get; set; }
    }
}