using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaFramework.Models {
    public class ProcessEventArg {
        public string Message            { get; set; }
        public int    TotalFiles         { get; set; }
        public int    ProcessedFileCount { get; set; }
        public bool   IsError            { get; set; }
        public bool   Sucessful          { get; set; }
        public bool   IsStarting         { get; set; }
        public bool   SendToDiscord      { get; set; } = false;
    }
}
