using System.Collections.Generic;
using OphiussaFramework.DataBaseUtils;
using OphiussaFramework.Models;

namespace OphiussaFramework.Interfaces {
    [TableAttributes(TableName = "Profiles")]
    public interface IProfile {
        string                                                   Key                { get; set; }
        string                                                   Name               { get; set; }
        string                                                   Type               { get; set; }
        string                                                   InstallationFolder { get; set; }
        string                                                   Branch             { get; set; }
        string                                                   ServerPassword     { get; set; }
        [FieldAttributes(DataType = "TEXT")] string              AdditionalSettings { get; set; }
        [FieldAttributes(DataType = "TEXT")] string              AdditionalCommands { get; set; }
        int                                                      SteamServerId      { get; set; }
        int                                                      SteamApplicationID { get; set; }
        int                                                      CurseForgeId       { get; set; }
        bool                                                     StartOnBoot        { get; set; }
        bool                                                     IncludeAutoBackup  { get; set; }
        bool                                                     IncludeAutoUpdate  { get; set; }
        bool                                                     RestartIfShutdown  { get; set; } 
        string                                                   PluginVersion      { get; set; }
        int                                                      ServerPort         { get; set; }
        int                                                      PeerPort           { get; set; }
        int                                                      QueryPort          { get; set; }
        bool                                                     UseRCON            { get; set; }
        int                                                      RCONPort           { get; set; }
        string                                                   RCONPassword       { get; set; }
        string                                                   ServerVersion      { get; set; }
        string                                                   ServerBuildVersion { get; set; }
        string                                                   ExecutablePath     { get; set; }
        [FieldAttributes(Ignore = true)] ProcessPriority         CpuPriority        { get; set; }
        [FieldAttributes(Ignore = true)] string                  CpuAffinity        { get; set; }
        [FieldAttributes(Ignore = true)] List<ProcessorAffinity> CpuAffinityList    { get; set; }
        [FieldAttributes(Ignore = true)] List<AutoManagement>    AutoManagement     { get; set; }
    }
}