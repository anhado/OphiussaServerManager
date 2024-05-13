using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OphiussaFramework;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.Enums;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;
using OphiussaFramework.Extensions;
using VRisingPlugin.Forms;
using Message = OphiussaFramework.Models.Message;

namespace VRisingPlugin {
    public class VRisingPlugin : IPlugin {
        public VRisingPlugin() {
            DefaultCommands = new List<CommandDefinition>();
        }

        public string ExecutablePath { get; set; } = "VRisingServer.exe"; //THIS WILL OVERWRITE THE PROFILE, I JUST NEED THAT IN PROFILE TO AVOID Deserialize THE ADDITIONAL SETTINGS

        // internal static readonly PluginType              Info = new PluginType { GameType = "Game1", Name = "Game 1 Name" };
        public IProfile Profile { get; set; } = new Profile();
        public string GameType { get; set; } = "VRising";
        public string GameName { get; set; } = "VRising";
        public TabPage TabPage { get; set; }
        public string PluginVersion { get; set; } = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
        public string PluginName { get; set; } = Path.GetFileName(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileName);
        public int ServerProcessID { get; set; }
        public bool IsRunning { get; set; }
        public bool IsInstalled { get; set; }
        public List<string> IgnoredFoldersInComparision { get; set; }
        public string CacheFolder { get; set; }
        public List<FilesToBackup> FilesToBackup => throw new NotImplementedException();
        public List<CommandDefinition> DefaultCommands { get; set; }
        public List<CommandDefinition> CustomCommands { get; set; }
        public ModProvider ModProvider { get; set; } = ModProvider.None;
        public bool Loaded { get; set; } = true;
        public ServerStatus ServerStatus { get; internal set; }
        public string ServerGameSettingsLocation { get; set; } = "VRisingServer_Data\\StreamingAssets\\Settings\\ServerGameSettings.json";
        public string ServerHostSettingsLocation { get; set; } = "VRisingServer_Data\\StreamingAssets\\Settings\\ServerHostSettings.json";
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
        public event EventHandler<OphiussaEventArgs> ServerStatusChangedEvent;

        //TODO:think in a way to do this in controller
        public void SetServerStatus(ServerStatus status, int serverProcessId) {
            switch (status) {
                case ServerStatus.NotInstalled:
                    IsInstalled = false;
                    IsRunning = false;
                    ServerProcessID = -1;
                    break;
                case ServerStatus.Running:
                    IsInstalled = true;
                    IsRunning = true;
                    ServerProcessID = serverProcessId;
                    break;
                case ServerStatus.Starting:
                    IsInstalled = true;
                    IsRunning = true;
                    ServerProcessID = serverProcessId;
                    break;
                case ServerStatus.Stopped:
                    IsInstalled = true;
                    IsRunning = false;
                    ServerProcessID = -1;
                    break;
                case ServerStatus.Stopping:
                    IsInstalled = true;
                    IsRunning = true;
                    ServerProcessID = serverProcessId;
                    break;
            }

            ServerStatus = status;
        }

        public Process GetExeProcess() {
            return Utils.GetProcessRunning(Path.Combine(Profile.InstallationFolder, Profile.ExecutablePath));
        }

        public PluginType GetInfo() {
            return new PluginType { GameType = GameType, Name = GameName };
        }

        public IProfile GetProfile() {
            return Profile;
        }

        public Form GetConfigurationForm(TabPage tab) {
            TabPage = tab;
            return new FrmVRising(this, tab);
        }

        public void TabHeaderChange() {
            TabHeaderChangeEvent?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        public async Task InstallServer(bool fromCache, bool showSteamCMD, bool startServerAtEnd) {
            if (!Directory.Exists(Profile.InstallationFolder)) Directory.CreateDirectory(Profile.InstallationFolder);

            if (Directory.GetDirectories(Profile.InstallationFolder).Any())
                if (!IsValidFolder(Profile.InstallationFolder))
                    throw new Exception("Invalid installation folder");

            InstallServerClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this, InstallFromCache = fromCache, ShowSteamCMD = showSteamCMD, StartServerAtEnd = startServerAtEnd });
        }

        public async Task StartServer() {
            StartServerClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        public async Task StopServer(bool force = false) {
            StopServerClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this, ForceStopServer = force });
        }

        public async Task BackupServer() {
            BackupServerClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        public void Save() {
            SaveClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });

            string priorityV = Profile.CpuPriority.ToString().ToLower();
            string affinityV = Utils.GetCpuAffinity(Profile.CpuAffinity, Profile.CpuAffinityList);

            if (!string.IsNullOrEmpty(affinityV)) affinityV = $"/affinity {affinityV}";


            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("@echo off");
            stringBuilder.AppendLine("set SteamAppId=1829350");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("echo \"Starting server PRESS CTRL-C to exit\"");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine($"start \"{Profile.Name}\" /{priorityV} {affinityV} \"{Path.Combine(Profile.InstallationFolder, Profile.ExecutablePath)}\" {GetCommandLinesArguments()}");

            File.WriteAllText(ConnectionController.Settings.DataFolder + $"StartServer\\Run_{Profile.Key.Replace("-", "")}.bat", stringBuilder.ToString());
        }

        public void Reload() {
            bool readedServerGameGameSettings = ReadServerGameSettings();
            bool readedServerHostSettings = ReadServerHostSettings();
            if (readedServerGameGameSettings && readedServerHostSettings)
                MessageBox.Show("Reloaded from files completed");
            else
                MessageBox.Show("Reloaded from files completed with errors");
            //ReloadClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        private bool ReadServerHostSettings() {

            try {
                var ServerHostSettingsTMP = JsonConvert.DeserializeObject(System.IO.File.ReadAllText(Path.Combine(Profile.InstallationFolder, ServerHostSettingsLocation)));

                JObject ServerHoistSettingsObj = JObject.FromObject(ServerHostSettingsTMP);

                var sgsProperties = ServerHoistSettingsObj.Properties();

                var myObjType = typeof(Profile);
                var myObjProperties = myObjType.GetProperties().ToList();
                //foreach (DataColumn column in dr.Table.Columns)
                //foreach (var pro in temp.GetProperties())

                foreach (JProperty prop in sgsProperties) {
                    Console.WriteLine(prop.Name);
                    var propName = prop.Name;
                    switch (propName) {
                        case "Port":
                            propName = "ServerPort";
                            break;
                        case "Name":
                            propName = "ServerName";
                            break;
                        case "Password":
                            propName = "ServerPassword";
                            break;
                    }

                    var p = myObjProperties.Find(x => x.Name == propName);
                    if (p != null) {

                        try {
                            object myValue = null;
                            var prop1 = prop.Value;

                            switch (prop.Type) {
                                case JTokenType.Property when prop1.Type == JTokenType.Integer:
                                    myValue = Convert.ChangeType(prop.Value, TypeCode.Int32);
                                    break;
                                case JTokenType.Property when prop1.Type == JTokenType.Boolean:
                                    myValue = Convert.ChangeType(prop.Value, TypeCode.Boolean);
                                    break;
                                case JTokenType.Property when prop1.Type == JTokenType.Float:
                                    myValue = Convert.ChangeType(prop.Value, TypeCode.Single);
                                    break;
                                case JTokenType.Property when prop1.Type == JTokenType.String:
                                    myValue = Convert.ChangeType(prop.Value, TypeCode.String);
                                    break;
                                case JTokenType.Property when prop1.Type == JTokenType.Array:
                                    var col = p.GetValue(Profile);
                                    SetPropertyRecursive(prop1, p, col);
                                    break;
                                case JTokenType.Property when prop1.Type == JTokenType.Object:
                                    var col2 = p.GetValue(Profile);
                                    SetPropertyRecursive(prop1, p, col2);
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }

                            if (myValue != null) p.SetValue(Profile, myValue);
                        }
                        catch (Exception e) {
                            OphiussaLogger.Logger.Error(e);
                        }
                    }
                    else {
                        OphiussaLogger.Logger.Error($"Could not find  property in plugin profile {prop.Name}");
                    }
                }

                return true;
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                return false;
            }
        }

        private bool ReadServerGameSettings() {

            try {
                var ServerGameSettingsTMP = JsonConvert.DeserializeObject(System.IO.File.ReadAllText(Path.Combine(Profile.InstallationFolder, ServerGameSettingsLocation)));

                JObject ServerGameSettingsObj = JObject.FromObject(ServerGameSettingsTMP);

                var sgsProperties = ServerGameSettingsObj.Properties();

                var myObjType = typeof(Profile);
                var myObjProperties = myObjType.GetProperties().ToList();

                foreach (JProperty prop in sgsProperties) {
                    var p = myObjProperties.Find(x => x.Name == prop.Name);
                    if (p != null) {

                        try {
                            object myValue = null;
                            var prop1 = prop.Value;

                            switch (prop.Type) {
                                case JTokenType.Property when prop1.Type == JTokenType.Integer:
                                    myValue = Convert.ChangeType(prop.Value, TypeCode.Int32);
                                    break;
                                case JTokenType.Property when prop1.Type == JTokenType.Boolean:
                                    myValue = Convert.ChangeType(prop.Value, TypeCode.Boolean);
                                    break;
                                case JTokenType.Property when prop1.Type == JTokenType.Float:
                                    myValue = Convert.ChangeType(prop.Value, TypeCode.Single);
                                    break;
                                case JTokenType.Property when prop1.Type == JTokenType.String:
                                    myValue = Convert.ChangeType(prop.Value, TypeCode.String);
                                    if (p.PropertyType.IsEnum) {
                                        string str = myValue.ToString();

                                        switch (p.Name) {
                                            case "GameModeType":
                                                myValue = str.ParseEnum<GameModeType>();
                                                break;
                                            case "CastleDamageMode":
                                                myValue = str.ParseEnum<CastleDamageMode>();
                                                break;
                                            case "SiegeWeaponHealth":
                                                myValue = str.ParseEnum<SiegeWeaponHealth>();
                                                break;
                                            case "PlayerDamageMode":
                                                myValue = str.ParseEnum<PlayerDamageMode>();
                                                break;
                                            case "CastleHeartDamageMode":
                                                myValue = str.ParseEnum<CastleHeartDamageMode>();
                                                break;
                                            case "PvPProtectionMode":
                                                myValue = str.ParseEnum<PvPProtectionMode>();
                                                break;
                                            case "DeathContainerPermission":
                                                myValue = str.ParseEnum<DeathContainerPermission>();
                                                break;
                                            case "RelicSpawnType":
                                                myValue = str.ParseEnum<RelicSpawnType>();
                                                break;
                                        }
                                    }
                                    break;

                                case JTokenType.Property when prop1.Type == JTokenType.Array:
                                    var col = p.GetValue(Profile);
                                    SetPropertyRecursive(prop1, p, col);
                                    break;
                                case JTokenType.Property when prop1.Type == JTokenType.Object:
                                    var col2 = p.GetValue(Profile);
                                    SetPropertyRecursive(prop1, p, col2);
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }

                            if (myValue != null) p.SetValue(Profile, myValue);
                        }
                        catch (Exception e) {
                            OphiussaLogger.Logger.Error(e);
                        }
                    }
                    else {
                        OphiussaLogger.Logger.Error($"Could not find  property in plugin profile {prop.Name}");
                    }
                }

                return true;
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                return false;
            }
        }

        private void SetPropertyRecursive(JToken value, PropertyInfo p, object collection) {

            var myObjProperties = p.PropertyType.GetProperties().ToList();

            foreach (JProperty prop in value) {
                object myValue = null;
                var prop1 = prop.Value;

                var p2 = myObjProperties.Find(x => x.Name == prop.Name);
                if (p2 != null) {
                    try {
                        switch (prop.Type) {
                            case JTokenType.Property when prop1.Type == JTokenType.Integer:
                                myValue = Convert.ChangeType(prop.Value, TypeCode.Int32);
                                break;
                            case JTokenType.Property when prop1.Type == JTokenType.Boolean:
                                myValue = Convert.ChangeType(prop.Value, TypeCode.Boolean);
                                break;
                            case JTokenType.Property when prop1.Type == JTokenType.Float:
                                myValue = Convert.ChangeType(prop.Value, TypeCode.Single);
                                break;
                            case JTokenType.Property when prop1.Type == JTokenType.String:
                                myValue = Convert.ChangeType(prop.Value, TypeCode.String);
                                break;
                            case JTokenType.Property when prop1.Type == JTokenType.Object:
                                var col2 = p2.GetValue(collection);
                                SetPropertyRecursive(prop1, p2, col2);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        if (myValue != null) p2.SetValue(collection, myValue);
                    }
                    catch (Exception e) {
                        OphiussaLogger.Logger.Error(e);
                    }
                }
            }
        }

        public void Sync() {
            SyncClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        public void OpenRCON() {
            OpenRCONClick?.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }

        public void ChooseFolder() {
            ChooseFolderClick.Invoke(this, new OphiussaEventArgs { Profile = Profile, Plugin = this });
        }


        public Message SetProfile(IProfile profile) {
            try {
                var p = profile.AdditionalSettings == null ? profile : JsonConvert.DeserializeObject<Profile>(profile.AdditionalSettings) == null ? profile : JsonConvert.DeserializeObject<Profile>(profile.AdditionalSettings);
                p.ServerBuildVersion = Utils.GetBuild(p);
                Profile = p;

                if (profile.AdditionalCommands != null) DefaultCommands = JsonConvert.DeserializeObject<List<CommandDefinition>>(profile.AdditionalCommands) ?? DefaultCommands;
                CacheFolder = Path.Combine(ConnectionController.Settings.DataFolder, $"cache\\{Profile.Branch}\\{Profile.Type}");

                return new Message {
                    MessageText = "Load Successful",
                    Success = true
                };
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                return new Message {
                    Exception = e,
                    MessageText = e.Message,
                    Success = false
                };
            }
        }

        public Message SetProfile(string json) {
            try {
                var p = JsonConvert.DeserializeObject<Profile>(json);
                p.ServerBuildVersion = Utils.GetBuild(p);
                Profile = p;
                if (Profile.AdditionalCommands != null) DefaultCommands = JsonConvert.DeserializeObject<List<CommandDefinition>>(Profile.AdditionalCommands) ?? DefaultCommands;
                CacheFolder = Path.Combine(ConnectionController.Settings.DataFolder, $"cache\\{Profile.Branch}\\{Profile.Type}");

                return new Message {
                    MessageText = "Load Successful",
                    Success = true
                };
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                return new Message {
                    Exception = e,
                    MessageText = e.Message,
                    Success = false
                };
            }
        }

        public bool IsValidFolder(string path) {
            //TODO:(New Games)Valid folder installation 
            return Utils.IsAValidFolder(Profile.InstallationFolder, new List<string> { "VRisingServer_Data" });
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
            return new Message { Exception = new NotImplementedException(), MessageText = "NOT IMPLEMENTED", Success = false };
        }

        public string GetVersion() {
            return "";
        }

        public string GetBuild() {
            return Utils.GetBuild(Profile);
        }

        public string GetCommandLinesArguments() {
            return "";
        }

        public string GetServerName() => Profile.Name;
    }
}