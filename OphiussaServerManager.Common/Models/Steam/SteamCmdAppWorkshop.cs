using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common.Models
{
    public class SteamCmdAppWorkshop
    {
        public string appid { get; set; }

        public string SizeOnDisk { get; set; }

        public string NeedsUpdate { get; set; }

        public string NeedsDownload { get; set; }

        public string TimeLastUpdated { get; set; }

        public string TimeLastAppRan { get; set; }

        public List<SteamCmdWorkshopItemsInstalled> WorkshopItemsInstalled { get; set; }

        public List<SteamCmdWorkshopItemDetails> WorkshopItemDetails { get; set; }
    }
}
