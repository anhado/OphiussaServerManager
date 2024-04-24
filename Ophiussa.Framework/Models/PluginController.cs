using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.Enums;
using OphiussaFramework.Forms;
using OphiussaFramework.Interfaces;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OphiussaFramework.Models {
    public class PluginController {
        private readonly string   _location;
        private readonly IPlugin  _plugin;
        private          Assembly _assembly;
        private          Timer    _timer =  new Timer();


        public PluginController(string                          filePath,
                                EventHandler<OphiussaEventArgs> installServerClick   = null,
                                EventHandler<OphiussaEventArgs> backupServerClick    = null,
                                EventHandler<OphiussaEventArgs> stopServerClick      = null,
                                EventHandler<OphiussaEventArgs> startServerClick     = null,
                                EventHandler<OphiussaEventArgs> saveClick            = null,
                                EventHandler<OphiussaEventArgs> reloadClick          = null,
                                EventHandler<OphiussaEventArgs> syncClick            = null,
                                EventHandler<OphiussaEventArgs> openRCONClick        = null,
                                EventHandler<OphiussaEventArgs> chooseFolderClick    = null,
                                EventHandler<OphiussaEventArgs> tabHeaderChangeEvent = null) {
            _location = filePath;
            var assembly = Assembly.LoadFile(filePath);

            var types = assembly.GetTypes();

            var pluginInterface = types.First(x => x.GetInterface("IPlugin") != null);

            _plugin = (IPlugin)Activator.CreateInstance(pluginInterface, null);

            _plugin.Profile.Type         =  _plugin.GameType;
            _plugin.InstallServerClick   += installServerClick ?? ServerUtils.ServerUtils.InstallServerClick;
            _plugin.BackupServerClick    += backupServerClick  ?? ServerUtils.ServerUtils.BackupServerClick;
            _plugin.StopServerClick      += stopServerClick    ?? ServerUtils.ServerUtils.StopServerClick;
            _plugin.StartServerClick     += startServerClick   ?? ServerUtils.ServerUtils.StartServerClick;
            _plugin.SaveClick            += saveClick          ?? ServerUtils.ServerUtils.SaveServerClick;
            _plugin.ReloadClick          += reloadClick        ?? ServerUtils.ServerUtils.ReloadServerClick;
            _plugin.SyncClick            += syncClick          ?? ServerUtils.ServerUtils.SyncServerClick;
            _plugin.OpenRCONClick        += openRCONClick      ?? ServerUtils.ServerUtils.OpenRCONClick;
            _plugin.ChooseFolderClick    += chooseFolderClick  ?? ServerUtils.ServerUtils.ChooseFolderClick;
            _plugin.TabHeaderChangeEvent += tabHeaderChangeEvent;
            _timer.Interval              =  1000;
            _timer.Tick += (sender, args) => {
                               if (_plugin.ServerStatus != ServerStatus) {
                                   StatusChanged?.Invoke(this, new ServerEventArgs() { Plugin = _plugin, Profile = _plugin.GetProfile(), Status = _plugin.ServerStatus });
                                   ServerStatus = _plugin.ServerStatus;
                               }
                           };
            _timer.Enabled = true;
        }

        public   string                            GameType      => _plugin.GetInfo().GameType;
        public   string                            GameName      => _plugin.GetInfo().Name;
        internal object                            Version       => _plugin.PluginVersion;
        internal object                            PluginName    => _plugin.PluginName;
        internal object                            Loaded        { get; set; } = true;
        public   bool                              IsInstalled   => _plugin.IsInstalled;
        public   bool                              IsRunning     => _plugin.IsRunning;
        public   string                            CacheFolder   => _plugin.CacheFolder;
        public   List<FilesToBackup>               FilesToBackup => _plugin.FilesToBackup;
        public event EventHandler<ServerEventArgs> StatusChanged;
        public ServerStatus                        ServerStatus = ServerStatus.NotInstalled;

        public void ShowServerInstallationOptions() {
            (new FrmProgress(this)).Show();
        }

        public Form GetConfigurationForm(TabPage tabPage) {
            return _plugin.GetConfigurationForm(tabPage);
        }

        public TabPage GeTabPage() {
            return _plugin.TabPage;
        }

        public void Save() {
            _plugin.Save();
        }

        public void Reload() {
            _plugin.Reload();
        }

        public void Sync() {
            _plugin.Sync();
        }

        public void OpenRCON() {
            _plugin.OpenRCON();
        }

        public void ChooseFolder() {
            _plugin.ChooseFolder();
        }

        public void TabHeaderChange() {
            _plugin.TabHeaderChange();
        }

        public void BackupServer() {
            _plugin.BackupServer();
        }

        public void StartServer() {
            _plugin.StartServer();
        }

        public async Task StopServer(bool force = false) {
            await _plugin.StopServer(force);
        }

        public async Task InstallServer(bool fromCache = false, bool showSteamCMD = false, bool startAtTheEnd = false) {
            await _plugin.InstallServer(fromCache, showSteamCMD, startAtTheEnd);
        }

        public IProfile GetProfile() {
            return _plugin.GetProfile();
        }

        public Message SetProfile(string json) {
            return _plugin.SetProfile(json);
        }

        public Message SetProfile(IProfile profile) {
            return _plugin.SetProfile(profile);
        }

        public Message SaveSettingsToDisk() {
            return _plugin.SaveSettingsToDisk();
        }

        public string PluginLocation() => _location;

        public bool IsValidFolder(string path) {
            return _plugin.IsValidFolder(path);
        }

        public void SetInstallationPath(string path) {
            _plugin.Profile.InstallationFolder = path;
        }

        public bool SavePluginInfo() {
            return ConnectionController.SqlLite.Upsert<IPlugin>(_plugin);
        }

        public List<string> IgnoredFoldersInComparision => _plugin.IgnoredFoldersInComparision;

        public bool InternalIsServerRunning {
            get {

                Process proc = Utils.GetProcessRunning(Path.Combine(_plugin.GetProfile().InstallationFolder, _plugin.GetProfile().ExecutablePath));

                return proc != null;
            }
        }

        public void SetServerStatus(ServerStatus status, int serverProcessID) {
            _plugin.SetServerStatus(status, serverProcessID);
        }

        internal string GetName()    => _plugin.GetServerName();
        internal string GetBuild()   => _plugin.GetBuild();
        internal string GetVersion() => _plugin.GetVersion();
    }
} 