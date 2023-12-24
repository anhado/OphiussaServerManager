using System.Collections.Generic;

namespace ArkData.Models {
    internal class SteamPlayerResponse<T> {
        public List<T> Players { get; set; }
    }
}