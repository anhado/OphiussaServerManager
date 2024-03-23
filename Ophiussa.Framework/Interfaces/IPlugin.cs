using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OphiussaFramework.Models; 

namespace OphiussaFramework.Interfaces
{
    public interface IPlugin
    {
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
        Models.Message                       SaveSettingsToDisk();
        Models.Message                       SetProfile(string json);
        Models.Message                       SetInstallFolder(string path);
        IProfile                             GetProfile();
        event EventHandler<InstallEventArgs> BackupServerClick;
        event EventHandler<InstallEventArgs> StopServerClick;
        event EventHandler<InstallEventArgs> StartServerClick;
        event EventHandler<InstallEventArgs> InstallServerClick;
    }
}
