using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using OphiussaFramework.Enums;
using OphiussaFramework.Models;
using Message = OphiussaFramework.Models.Message;

namespace OphiussaFramework.Interfaces {
    [TableAttributes(TableName = "Plugins")]
    public interface IPlugin {
        [FieldAttributes(PrimaryKey = true, DataType = "Varchar(100)")]
        string PluginName { get; set; }

        string                                                   PluginVersion               { get; set; }
        string                                                   GameType                    { get; set; }
        string                                                   GameName                    { get; set; }
        ModProvider                                              ModProvider                 { get; set; }
        bool                                                     Loaded                      { get; set; }
        [FieldAttributes(Ignore = true)] int                     ServerProcessID             { get; }
        [FieldAttributes(Ignore = true)] List<CommandDefinition> DefaultCommands             { get; set; }
        [FieldAttributes(Ignore = true)] List<CommandDefinition> CustomCommands              { get; set; }
        [FieldAttributes(Ignore = true)] List<FileInfo>          FilesToBackup               { get; }
        [FieldAttributes(Ignore = true)] IProfile                Profile                     { get; }
        [FieldAttributes(Ignore = true)] bool                    IsRunning                   { get; }
        [FieldAttributes(Ignore = true)] bool                    IsInstalled                 { get; }
        [FieldAttributes(Ignore = true)] TabPage                 TabPage                     { get; }
        [FieldAttributes(Ignore = true)] List<string>            IgnoredFoldersInComparision { get; }
        [FieldAttributes(Ignore = true)] string                  CacheFolder                 { get; set; }
        PluginType                                               GetInfo();
        Form                                                     GetConfigurationForm(TabPage tab);
        Task                                                     BackupServer();
        Task                                                     StopServer(bool force = false);
        Task                                                     StartServer();
        Task                                                     InstallServer(bool fromCache = false);
        void                                                     Save();
        void                                                     Reload();
        void                                                     Sync();
        void                                                     OpenRCON();
        void                                                     ChooseFolder();
        bool                                                     IsValidFolder(string path);
        Message                                                  SaveSettingsToDisk();
        Message                                                  SetProfile(string       json);
        Message                                                  SetProfile(IProfile     profile);
        Message                                                  SetInstallFolder(string path);
        IProfile                                                 GetProfile();
        Process                                                  GetExeProcess();
        void                                                     TabHeaderChange();
        string                                                   GetVersion();
        string                                                   GetBuild();
        string                                                   GetCommandLinesArguments(); 
        event EventHandler<OphiussaEventArgs>                    SaveClick;
        event EventHandler<OphiussaEventArgs>                    ReloadClick;
        event EventHandler<OphiussaEventArgs>                    SyncClick;
        event EventHandler<OphiussaEventArgs>                    OpenRCONClick;
        event EventHandler<OphiussaEventArgs>                    BackupServerClick;
        event EventHandler<OphiussaEventArgs>                    StopServerClick;
        event EventHandler<OphiussaEventArgs>                    StartServerClick;
        event EventHandler<OphiussaEventArgs>                    InstallServerClick;
        event EventHandler<OphiussaEventArgs>                    ChooseFolderClick;
        event EventHandler<OphiussaEventArgs>                    TabHeaderChangeEvent;
    }
}