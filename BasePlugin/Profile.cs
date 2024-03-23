using System;
using System.Diagnostics;
using System.Reflection;
using OphiussaFramework.Interfaces;

namespace BasePlugin {
    public class Profile : IProfile {
        public string Key                { get; set; } = Guid.NewGuid().ToString();
        public string Name               { get; set; } = "New Server";
        public string Type               => BasePlugin.Info.GameType;
        public string InstallationFolder { get; set; }
        public object AdditionalSettings { get; set; }
        public int    SteamServerId      { get; set; }
        public int    SteamApplicationID { get; set; }
        public int    CurseForgeId       { get; set; }
        public int    ServerPort         { get; set; }
        public int    PeerPort           { get; set; }
        public int    QueryPort          { get; set; }
        public int    RCONPort           { get; set; }

        public string PluginVersion => FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
    }
}