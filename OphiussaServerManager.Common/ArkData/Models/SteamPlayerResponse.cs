using System.Collections.Generic;

namespace OphiussaServerManager.Common.Models
{
    internal class SteamPlayerResponse<T>
    {
        public List<T> players { get; set; }
    }
}
