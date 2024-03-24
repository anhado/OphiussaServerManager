using System;
using System.Collections.Generic;
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
        public                   string     ExecutablePath { get; set; } = "Dummy123.exe";//THIS WILL OVERWRITE THE PROFILE, I JUST NEED THAT IN PROFILE TO AVOID Deserialize THE ADDITIONAL SETTINGS
        public                   IProfile   Profile        { get; set; } = new Profile();
        public                   string     PluginVersion  => FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
        public                   string     PluginName     => Path.GetFileName(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileName);

        public BasePlugin() {
            OphiussaFramework.ConnectionController.Initialize();
        }

        public int     ServerProcessID => Utils.GetProcessRunning(Path.Combine(Profile.InstallationFolder, Profile.ExecutablePath)).Id; 
        public bool    IsRunning       => Utils.GetProcessRunning(Path.Combine(Profile.InstallationFolder, Profile.ExecutablePath)) != null;
        public Process GetExeProcess() => Utils.GetProcessRunning(Path.Combine(Profile.InstallationFolder, Profile.ExecutablePath));
        public bool    IsInstalled     => IsValidFolder(Profile.InstallationFolder);
        public TabPage TabPage         { get; set; }

        public event EventHandler<OphiussaEventArgs> BackupServerClick;
        public event EventHandler<OphiussaEventArgs> StopServerClick;
        public event EventHandler<OphiussaEventArgs> StartServerClick;
        public event EventHandler<OphiussaEventArgs> InstallServerClick;
        public event EventHandler<OphiussaEventArgs> SaveClick;
        public event EventHandler<OphiussaEventArgs> ReloadClick;
        public event EventHandler<OphiussaEventArgs> SyncClick;
        public event EventHandler<OphiussaEventArgs> OpenRCONClick;
        public event EventHandler<OphiussaEventArgs> ChooseFolderClick;
        public event EventHandler<OphiussaEventArgs> TabHeaderChangeEvent;

        public PluginType GetInfo() {
            return Info;
        }

        public IProfile GetProfile() {
            return Profile;
        }
         
        public Form GetConfigurationForm(TabPage tab) {
            TabPage = tab;
            return new FrmConfigurationForm(this,tab);
        }

        public void TabHeaderChange() {
            TabHeaderChangeEvent?.Invoke(this, new OphiussaEventArgs { Profile = Profile,Plugin = this});
        }

        public void InstallServer() {
            InstallServerClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        public void StartServer() {
            StartServerClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        public void StopServer() {
            StopServerClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        public void BackupServer() {
            BackupServerClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        public void Save() {
            SaveClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        public void Reload() {
            ReloadClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        public void Sync() {
            SyncClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        public void OpenRCON() {
            OpenRCONClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        public void ChooseFolder() {
           ChooseFolderClick.Invoke(this, new OphiussaEventArgs() { Profile = Profile, Plugin = this });
        }


        public Message SetProfile(IProfile profile) {
            try {

                var p = JsonConvert.DeserializeObject<Profile>(profile.AdditionalSettings.ToString());
                Profile = p;

                return new Message {
                                       MessageText = "Load Successful",
                                       Success     = true
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

        public Message SetProfile(string json) {
            try {
                var p = JsonConvert.DeserializeObject<Profile>(json);

                Profile = p;

                return new Message {
                                       MessageText = "Load Successful",
                                       Success     = true
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
            //TODO:(New Games)Valid folder installation 
            return Utils.IsAValidFolder(Profile.InstallationFolder,new List<string>(){"FolderDummy","FolderDummy2"});
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

        public Message SaveSettingsToDisk() {
            //TODO:(New Games)Save settings to disc
            return new Message() { Exception = new NotImplementedException(), MessageText = "NOT IMPLEMENTED", Success = false };
        }

        public string GetVersion() {
            //TODO:(New Games)Save settings to disc
            return "NOT IMPLEMENTED"; //  throw new NotImplementedException();
        }

        public string GetBuild() {
            //TODO:(New Games)Save settings to disc
            return "NOT IMPLEMENTED"; //  throw new NotImplementedException();
        }
    }
}