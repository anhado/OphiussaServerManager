using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OphiussaFramework.Interfaces;

namespace OphiussaFramework.Models {
    internal class RawProfile : IProfile {
        public string Key                { get; set; }
        public string Name               { get; set; } 
        public string Type               { get; set; }
        public string InstallationFolder { get; set; }
        public string AdditionalSettings { get; set; }
        public string AdditionalCommands { get; set; }
        public int    SteamServerId      { get; set; }
        public int    SteamApplicationID { get; set; }
        public int    CurseForgeId       { get; set; }
        public bool   StartOnBoot        { get; set; }
        public bool   IncludeAutoBackup  { get; set; }
        public bool   IncludeAutoUpdate  { get; set; }
        public bool   RestartIfShutdown  { get; set; } 
        public string PluginVersion      { get; set; }
        public int    ServerPort         { get; set; }
        public int    PeerPort           { get; set; }
        public int    QueryPort          { get; set; }
        public bool   UseRCON            { get; set; }
        public int    RCONPort           { get; set; } 
        public string RCONPassword       { get; set; }
        public string ServerVersion      { get; set; }
        public string ServerBuildVersion { get; set; }
        public string ExecutablePath     { get; set; }
        public string Branch             { get; set; }
    }
}
