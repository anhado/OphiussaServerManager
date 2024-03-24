﻿using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Newtonsoft.Json;
using OphiussaFramework.Interfaces;

namespace OphiussaFramework.Models {
    public class PluginController {
        private readonly string   _location;
        private readonly IPlugin  _plugin;
        private          Assembly _assembly; 
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

            _plugin                      =  (IPlugin)Activator.CreateInstance(pluginInterface, null);
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

        public Message SetProfile(IProfile profile) {
            return _plugin.SetProfile(profile);
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