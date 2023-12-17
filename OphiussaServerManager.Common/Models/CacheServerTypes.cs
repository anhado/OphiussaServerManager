using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common.Models
{
    public class CacheServerTypes
    {
        public string InstallCacheFolder { get; set; }
        public SupportedServers.SupportedServersType Type { get; set; }
        public string ModId { get; set; }
    }
}
