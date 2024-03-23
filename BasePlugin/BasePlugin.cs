using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using BasePlugin.Forms;
using Newtonsoft.Json;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;
using Message = OphiussaFramework.Models.Message;

namespace BasePlugin {
    public class BasePlugin : IPlugin {
        internal static readonly PluginType Info = new PluginType { GameType = "Game1", Name = "Game 1 Name" };
        public                   IProfile   Profile       { get; set; } = new Profile();
        public                   string     PluginVersion => FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
        public                   string     PluginName    => Path.GetFileName(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileName);

        public event EventHandler<InstallEventArgs> BackupServerClick;
        public event EventHandler<InstallEventArgs> StopServerClick;
        public event EventHandler<InstallEventArgs> StartServerClick;
        public event EventHandler<InstallEventArgs> InstallServerClick;

        public PluginType GetInfo() {
            return Info;
        }

        public IProfile GetProfile() {
            return Profile;
        }


        public Form GetConfigurationForm() {
            return new FrmConfigurationForm(this);
        }

        public void InstallServer() {
            InstallServerClick?.Invoke(this, new InstallEventArgs { Profile = Profile });
        }

        public void StartServer() {
            StartServerClick?.Invoke(this, new InstallEventArgs { Profile = Profile });
        }

        public void StopServer() {
            StopServerClick?.Invoke(this, new InstallEventArgs { Profile = Profile });
        }

        public void BackupServer() {
            BackupServerClick?.Invoke(this, new InstallEventArgs { Profile = Profile });
        }

        public Message SaveSettingsToDisk() {
            throw new NotImplementedException();
        }

        public Message SetProfile(string json) {
            try {
                var p = JsonConvert.DeserializeObject<Profile>(json);

                Profile = p;

                return new Message {
                                       MessageText = "Load Successful",
                                       Success     = false
                                   };
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                return new Message {
                                       Exception   = e,
                                       MessageText = e.Message,
                                       Success     = false
                                   };
            }
        }

        public bool IsValidFolder(string path) {
            //TODO:Valid folder installation
            return true;
        }

        public Message SetInstallFolder(string path) {
            try {
                if (IsValidFolder(path)) Profile.InstallationFolder = path;
                else throw new Exception("Invalid Installation folder");

                return new Message { MessageText = "Path Changed", Success = true };
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                return new Message { Exception = e, MessageText = e.Message, Success = false };
            }
        }
    }
}