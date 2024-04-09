using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaFramework.Models {
    public class FilesToBackup {
        public FileInfo File      { get; set; }
        public string   EntryName { get; set; }
    }
}
