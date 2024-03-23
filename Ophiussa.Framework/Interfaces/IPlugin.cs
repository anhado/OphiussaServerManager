using System;
using System.Diagnostics;
using System.Windows.Forms;
using OphiussaFramework.Models;
using Message = OphiussaFramework.Models.Message;

namespace OphiussaFramework.Interfaces {
    public interface IPlugin {
        IProfile                              Profile         { get; set; }
        string                                PluginVersion   { get; }
        string                                PluginName      { get; }
        int                                   ServerProcessID { get; }
        bool                                  IsRunning       { get; }
        bool                                  IsInstalled     { get; }
        TabPage                               TabPage         { get; }
        PluginType                            GetInfo();
        Form                                  GetConfigurationForm(TabPage tab);
        void                                  BackupServer();
        void                                  StopServer();
        void                                  StartServer();
        void                                  InstallServer();
        void                                  Save();
        void                                  Reload();
        void                                  Sync();
        void                                  OpenRCON();
        void                                  ChooseFolder();
        bool                                  IsValidFolder(string path);
        Message                               SaveSettingsToDisk();
        Message                               SetProfile(string       json);
        Message                               SetInstallFolder(string path);
        IProfile                              GetProfile();
        Process                               GetExeProcess();
        void                                  TabHeaderChange();
        string                                GetVersion();
        string                                GetBuild();
        event EventHandler<OphiussaEventArgs> SaveClick;
        event EventHandler<OphiussaEventArgs> ReloadClick;
        event EventHandler<OphiussaEventArgs> SyncClick;
        event EventHandler<OphiussaEventArgs> OpenRCONClick;
        event EventHandler<OphiussaEventArgs> BackupServerClick;
        event EventHandler<OphiussaEventArgs> StopServerClick;
        event EventHandler<OphiussaEventArgs> StartServerClick;
        event EventHandler<OphiussaEventArgs> InstallServerClick;
        event EventHandler<OphiussaEventArgs> ChooseFolderClick;
        event EventHandler<OphiussaEventArgs> TabHeaderChangeEvent;
    }
}