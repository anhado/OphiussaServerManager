using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaFramework.Models {
    public class CommandDefinition {
        public int    Order            { get; set; }
        public bool   AddSpaceInPrefix { get; set; } = false;
        public string NamePrefix       { get; set; }
        public string Name             { get; set; }
        public string ValuePrefix      { get; set; }
        public string Value            { get; set; }
        public bool   Enabled          { get; set; } = true;
    }
}
