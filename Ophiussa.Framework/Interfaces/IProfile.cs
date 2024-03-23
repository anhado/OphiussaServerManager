namespace OphiussaFramework.Interfaces {
    public interface IProfile {
        string Key                { get; set; }
        string Name               { get; set; }
        string Type               { get; }
        string InstallationFolder { get; set; }
        object AdditionalSettings { get; set; }
        int    SteamServerId      { get; set; }
        int    SteamApplicationID { get; set; }
        int    CurseForgeId       { get; set; }
        bool   StartOnBoot        { get; set; }
        bool   IncludeAutoBackup  { get; set; }
        bool   IncludeAutoUpdate  { get; set; }
        bool   RestartIfShutdown  { get; set; } 
        string PluginVersion      { get; }
        int    ServerPort         { get; set; }
        int    PeerPort           { get; set; }
        int    QueryPort          { get; set; }
        bool   UseRCON            { get; set; }
        int    RCONPort           { get; set; }
        string RCONPassword       { get; }
        string ServerVersion      { get; set; }
        string ServerBuildVersion { get; set; }
        string ExecutablePath     { get; set; }
    }
}