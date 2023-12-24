namespace ArkData.Models {
    internal class SteamBan {
        public string SteamId          { get; set; }
        public bool   CommunityBanned  { get; set; }
        public bool   VacBanned        { get; set; }
        public int    NumberOfVacBans  { get; set; }
        public int    DaysSinceLastBan { get; set; }
        public int    NumberOfGameBans { get; set; }
    }
}