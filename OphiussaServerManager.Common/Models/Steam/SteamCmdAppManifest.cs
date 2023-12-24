using System.Collections.Generic;

namespace OphiussaServerManager.Common.Models {
    public class SteamCmdAppManifest {
        public string Appid { get; set; }

        public List<SteamCmdManifestUserConfig> UserConfig { get; set; }
    }
}