using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common.Models
{
    public class SteamCmdAppManifest
    {
        public string appid { get; set; }

        public List<SteamCmdManifestUserConfig> UserConfig { get; set; }
    }
}
