using System;
using System.Diagnostics;
using System.Reflection;
using System.Web.Security;
using OphiussaFramework.Interfaces;

namespace BasePlugin {
    public class Profile : IProfile {
        public string Key                { get; set; } = Guid.NewGuid().ToString();
        public string Name               { get; set; } = "New Server";
        public string Type               { get; set; }
        public string PluginVersion      { get; set; } =  FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
        public string InstallationFolder { get; set; } = "";
        public string AdditionalSettings { get; set; } = "";
        public string AdditionalCommands { get; set; } = "";
        public int    SteamServerId      { get; set; } = 0;
        public int    SteamApplicationID { get; set; } = 0;
        public int    CurseForgeId       { get; set; } = 0;
        public int    ServerPort         { get; set; } = 0;
        public int    PeerPort           { get; set; } = 0;
        public int    QueryPort          { get; set; } = 0;
        public int    RCONPort           { get; set; } = 0;
        public string ServerVersion      { get; set; } = "";
        public string ServerBuildVersion { get; set; } = "";
        public bool   StartOnBoot        { get; set; } = false;
        public bool   IncludeAutoBackup  { get; set; } = false;
        public bool   IncludeAutoUpdate  { get; set; } = false;
        public bool   RestartIfShutdown  { get; set; } = false;
        public string RCONPassword       { get; set; } = Membership.GeneratePassword(10, 6);
        public bool   UseRCON            { get; set; } = false;
        public string ExecutablePath     { get; set; } = "Dummy.exe";
        public string TestProperty1      { get; set; } = "";
        public bool   TestProperty2      { get; set; } = true;
        public int    TestProperty3      { get; set; } = 999;

    }
}