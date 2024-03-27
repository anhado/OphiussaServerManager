using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaFramework {
    [System.AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Struct)]
    public class FieldAttributes : System.Attribute {
        public bool PrimaryKey { get; set; } = false;
        public bool AutoIncrement { get; set; } = false;
        public bool Ignore { get; set; } = false;
        public string DataType { get; set; }
    }
}
