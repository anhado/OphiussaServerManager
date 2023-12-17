using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common.Models
{
    public class ModListDetails
    {
        public int Order { get; set; }
        public string ModID { get; set; }
        public string Name { get; set; }
        public string ModType { get; set; }
        public DateTime LastDownloaded { get; set; }
        public DateTime LastUpdatedAuthor { get; set; }
        public Int64 TimeStamp { get; set; }
        public string FolderSize { get; set; }
        public Int64 Subscriptions { get; set; }
        public string Link { get; set; }
    }
}
