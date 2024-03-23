using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OphiussaFramework.Interfaces;

namespace OphiussaFramework.Models
{
    public class PluginController
    {
        private          Assembly _assembly;
        private readonly IPlugin  _plugin;
        private readonly string _location;
        public PluginController(string                         filePath,
                                EventHandler<InstallEventArgs> installServerClick = null,
                                EventHandler<InstallEventArgs> backupServerClick  = null,
                                EventHandler<InstallEventArgs> StopServerClick    = null,
                                EventHandler<InstallEventArgs> startServerClick   = null)
        {
            _location = filePath;
            var assembly = Assembly.LoadFile(filePath);

            var types = assembly.GetTypes();

            var pluginInterface = types.First(x => x.GetInterface("IPlugin") != null);

            _plugin                    =  (IPlugin)Activator.CreateInstance(pluginInterface, null);
            _plugin.InstallServerClick += installServerClick;
            _plugin.BackupServerClick  += backupServerClick;
            _plugin.StopServerClick    += StopServerClick;
            _plugin.StartServerClick   += startServerClick;

        }

        public string   GameType                   => _plugin.GetInfo().GameType;
        public string   GameName                   => _plugin.GetInfo().Name;
        public Form     GetConfigurationForm()     => _plugin.GetConfigurationForm();
        public void     BackupServer()             => _plugin.BackupServer();
        public void     StartServer()              => _plugin.StartServer();
        public void     StopServer()               => _plugin.StopServer();
        public void     InstallServer()            => _plugin.InstallServer();
        public IProfile GetProfile()               => _plugin.GetProfile();
        public Message  SetProfile(string json)    => _plugin.SetProfile(json);
        public Message  SaveSettingsToDisk()       => _plugin.SaveSettingsToDisk();
        public string   PluginLocation()           => _location;
        public bool     IsValidFolder(string path) => _plugin.IsValidFolder(path);
         
        internal object Version    => _plugin.PluginVersion; 
        internal object PluginName => _plugin.PluginName;
        internal object Loaded     { get; set; } = true;
    }
}
