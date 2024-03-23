using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using OphiussaFramework.Interfaces;

namespace OphiussaFramework.Models {
    public class PluginController {
        private readonly string   _location;
        private readonly IPlugin  _plugin;
        private          Assembly _assembly; 
        public PluginController(string                          filePath,
                                EventHandler<OphiussaEventArgs> installServerClick   = null,
                                EventHandler<OphiussaEventArgs> backupServerClick    = null,
                                EventHandler<OphiussaEventArgs> StopServerClick      = null,
                                EventHandler<OphiussaEventArgs> startServerClick     = null, 
                                EventHandler<OphiussaEventArgs> SaveClick            = null,
                                EventHandler<OphiussaEventArgs> ReloadClick          = null,
                                EventHandler<OphiussaEventArgs> SyncClick            = null,
                                EventHandler<OphiussaEventArgs> OpenRCONClick        = null,
                                EventHandler<OphiussaEventArgs> ChooseFolderClick    = null,
                                EventHandler<OphiussaEventArgs> TabHeaderChangeEvent = null) {
            _location = filePath;
            var assembly = Assembly.LoadFile(filePath);

            var types = assembly.GetTypes();

            var pluginInterface = types.First(x => x.GetInterface("IPlugin") != null);

            _plugin                      =  (IPlugin)Activator.CreateInstance(pluginInterface, null);
            _plugin.InstallServerClick   += installServerClick;
            _plugin.BackupServerClick    += backupServerClick;
            _plugin.StopServerClick      += StopServerClick;
            _plugin.StartServerClick     += startServerClick;
            _plugin.SaveClick            += SaveClick;
            _plugin.ReloadClick          += ReloadClick;
            _plugin.SyncClick            += SyncClick;
            _plugin.OpenRCONClick        += OpenRCONClick;
            _plugin.ChooseFolderClick    += ChooseFolderClick;
            _plugin.TabHeaderChangeEvent += TabHeaderChangeEvent;
        }

        public   string GameType    => _plugin.GetInfo().GameType;
        public   string GameName    => _plugin.GetInfo().Name; 
        internal object Version     => _plugin.PluginVersion;
        internal object PluginName  => _plugin.PluginName;
        internal object Loaded      { get; set; } = true;
        public   bool   IsInstalled => _plugin.IsInstalled;
        public   bool   IsRunning   => _plugin.IsRunning;

        public Form    GetConfigurationForm(TabPage tabPage) => _plugin.GetConfigurationForm(tabPage);
        public TabPage GeTabPage()                           => _plugin.TabPage; 

        public void BackupServer() {
            _plugin.BackupServer();
        }

        public void StartServer() {
            _plugin.StartServer();
        }

        public void StopServer() {
            _plugin.StopServer();
        }

        public void InstallServer() {
            _plugin.InstallServer();
        }

        public IProfile GetProfile() {
            return _plugin.GetProfile();
        }

        public Message SetProfile(string json) {
            return _plugin.SetProfile(json);
        }

        public Message SaveSettingsToDisk() {
            return _plugin.SaveSettingsToDisk();
        }

        public string PluginLocation() {
            return _location;
        }

        public bool IsValidFolder(string path) {
            return _plugin.IsValidFolder(path);
        }

        public void  SetInstallationPath(string path) {
             _plugin.Profile.InstallationFolder = path;
        }
    }
}