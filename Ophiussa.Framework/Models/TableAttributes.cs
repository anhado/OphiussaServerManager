using System;

namespace OphiussaFramework {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class TableAttributes : Attribute {
        public string TableName { get; set; }
    }
}