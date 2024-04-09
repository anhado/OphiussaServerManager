using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using OphiussaFramework.Enums;
using OphiussaFramework.Interfaces;

namespace OphiussaFramework.Models {
    internal class RawPlugin : IPlugin {
        public string                  PluginName                  { get; set; }
        public string                  PluginVersion               { get; set; }
        public string                  GameType                    { get; set; }
        public string                  GameName                    { get; set; }
        public bool                    Loaded                      { get; set; }
        public ModProvider             ModProvider                 { get; set; }
        public int                     ServerProcessID             { get; set; }
        public List<CommandDefinition> DefaultCommands             { get; set; }
        public List<FilesToBackup>     FilesToBackup               { get; set; }
        public IProfile                Profile                     { get; set; }
        public bool                    IsRunning                   { get; set; }
        public bool                    IsInstalled                 { get; set; }
        public TabPage                 TabPage                     { get; set; }
        public List<CommandDefinition> CustomCommands              { get; set; }
        public List<string>            IgnoredFoldersInComparision { get; set; }
        public string                  CacheFolder                 { get; set; }

        public PluginType GetInfo() {
            throw new NotImplementedException();
        }

        public Form GetConfigurationForm(TabPage tab) {
            throw new NotImplementedException();
        }

        public Task BackupServer() {
            throw new NotImplementedException();
        }

        public Task StopServer(bool force = false) {
            throw new NotImplementedException();
        }

        public Task StartServer() {
            throw new NotImplementedException();
        }

        public Task InstallServer(bool fromCache) {
            throw new NotImplementedException();
        }

        public void Save() {
            throw new NotImplementedException();
        }

        public void Reload() {
            throw new NotImplementedException();
        }

        public void Sync() {
            throw new NotImplementedException();
        }

        public void OpenRCON() {
            throw new NotImplementedException();
        }

        public void ChooseFolder() {
            throw new NotImplementedException();
        }

        public bool IsValidFolder(string path) {
            throw new NotImplementedException();
        }

        public Message SaveSettingsToDisk() {
            throw new NotImplementedException();
        }

        public Message SetProfile(string json) {
            throw new NotImplementedException();
        }

        public Message SetProfile(IProfile profile) {
            throw new NotImplementedException();
        }

        public Message SetInstallFolder(string path) {
            throw new NotImplementedException();
        }

        public IProfile GetProfile() {
            throw new NotImplementedException();
        }

        public Process GetExeProcess() {
            throw new NotImplementedException();
        }

        public void TabHeaderChange() {
            throw new NotImplementedException();
        }

        public string GetVersion() {
            throw new NotImplementedException();
        }

        public string GetBuild() {
            throw new NotImplementedException();
        }

        public string GetCommandLinesArguments() {
            throw new NotImplementedException();
        }


        public event EventHandler<OphiussaEventArgs> SaveClick;
        public event EventHandler<OphiussaEventArgs> ReloadClick;
        public event EventHandler<OphiussaEventArgs> SyncClick;
        public event EventHandler<OphiussaEventArgs> OpenRCONClick;
        public event EventHandler<OphiussaEventArgs> BackupServerClick;
        public event EventHandler<OphiussaEventArgs> StopServerClick;
        public event EventHandler<OphiussaEventArgs> StartServerClick;
        public event EventHandler<OphiussaEventArgs> InstallServerClick;
        public event EventHandler<OphiussaEventArgs> ChooseFolderClick;
        public event EventHandler<OphiussaEventArgs> TabHeaderChangeEvent;
    }
}