using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaFramework {
    [System.AttributeUsage(System.AttributeTargets.Class |  System.AttributeTargets.Interface)]
    public class TableAttributes : System.Attribute {
        public string TableName { get; set; }
    }
}
