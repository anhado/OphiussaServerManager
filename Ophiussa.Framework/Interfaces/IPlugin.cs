using System;
using System.Windows.Forms;
using OphiussaFramework.Models;
using Message = OphiussaFramework.Models.Message;

namespace OphiussaFramework.Interfaces {
    public interface IPlugin {
        IProfile                             Profile       { get; set; }
        string                               PluginVersion { get; }
        string                               PluginName    { get; }
        PluginType                           GetInfo();
        Form                                 GetConfigurationForm();
        void                                 BackupServer();
        void                                 StopServer();
        void                                 StartServer();
        void                                 InstallServer();
        bool                                 IsValidFolder(string path);
        Message                              SaveSettingsToDisk();
        Message                              SetProfile(string       json);
        Message                              SetInstallFolder(string path);
        IProfile                             GetProfile();
        event EventHandler<InstallEventArgs> BackupServerClick;
        event EventHandler<InstallEventArgs> StopServerClick;
        event EventHandler<InstallEventArgs> StartServerClick;
        event EventHandler<InstallEventArgs> InstallServerClick;
    }
}