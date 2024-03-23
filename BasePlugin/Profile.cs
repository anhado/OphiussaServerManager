using OphiussaFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BasePlugin
{
    public class Profile : IProfile
    {
        public string Key                { get; set; } = System.Guid.NewGuid().ToString();
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
