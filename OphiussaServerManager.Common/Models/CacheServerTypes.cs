using OphiussaServerManager.Common.Models.SupportedServers;

namespace OphiussaServerManager.Common.Models {
    public class CacheServerTypes {
        public string               InstallCacheFolder { get; set; }
        public SupportedServersType Type               { get; set; }
        public string               ModId              { get; set; }
    }
}