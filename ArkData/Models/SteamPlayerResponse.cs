using System.Collections.Generic;

namespace ArkData.Models
{
    internal class SteamPlayerResponse<T>
    {
        public List<T> players { get; set; }
    }
}
