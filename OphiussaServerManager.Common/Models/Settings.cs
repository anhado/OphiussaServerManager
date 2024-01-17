using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using OphiussaServerManager.Common.Helpers;

namespace OphiussaServerManager.Common.Models {
    public class ProfileOrder {
        public int    Order { get; set; }
        public string Key   { get; set; }
    }

    public class Settings {
        public Settings(string pDrive = "", string pFoldernName = "osmdata") {
            var    f                = new FileInfo(Assembly.GetExecutingAssembly().FullName);
            string drive            = Path.GetPathRoot(f.FullName);
            if (pDrive != "") drive = pDrive;
            DataFolder                = drive + $"{pFoldernName}\\";
            DefaultInstallationFolder = drive + $"{pFoldernName}\\Servers\\";
            SteamCmdLocation          = drive + $"{pFoldernName}\\steamcmd\\";
            BackupDirectory           = drive + $"{pFoldernName}\\osmBackups\\";
            Guid                      = System.Guid.NewGuid().ToString();
            ModColors                 = Utils.GetModColors();
        }

        [JsonIgnore] public string CryptKey                  { get; set; } = "b14ca5898a4e4133bbce2ea2315a1916";
        public              string Guid                      { get; set; }
        public              string SteamKey                  { get; set; } = "";
        public              string CurseForgeKey             { get; set; } = "";
        public              bool   UpdateSteamCmdOnStartup   { get; set; }
        public              string DataFolder                { get; set; } = "";
        public              string DefaultInstallationFolder { get; set; }
        public              string SteamCmdLocation          { get; set; } = "";
        public              bool   UseAnonymousConnection    { get; set; } = true;
        public              string CryptedSteamUserName      { get; set; } = "";
        public              string CryptedSteamPassword      { get; set; } = "";

        [JsonIgnore] public string SteamUserName => EncryptionUtils.DecryptString(CryptKey, CryptedSteamUserName);

        [JsonIgnore] public string SteamPassword => EncryptionUtils.DecryptString(CryptKey, CryptedSteamPassword);

        public bool               ValidateProfileOnServerStart           { get; set; } = true;
        public bool               PerformServerAndModUpdateOnServerStart { get; set; } = false;
        public bool               UpdateModsWhenUpdatingServer           { get; set; } = true;
        public string             BackupDirectory                        { get; set; } = "";
        public bool               IncludeSaveGamesFolder                 { get; set; } = true;
        public bool               DeleteOldBackupFiles                   { get; set; } = true;
        public int                DeleteFilesDays                        { get; set; } = 30;
        public string             WorldSaveMessage                       { get; set; } = "A world save is about to be performed, you may experience some lag during this process. Please be patient.";
        public bool               AutoUpdate                             { get; set; } = true;
        public string             UpdateInterval                         { get; set; } = "0100";
        public bool               PerformOnlinePlayerCheck               { get; set; } = true;
        public bool               UseSmartCopy                           { get; set; } = true;
        public bool               SendShutdowMessages                    { get; set; } = true;
        public int                GracePeriod                            { get; set; } = 15;
        public string             Message1                               { get; set; } = "Server shutdown required. Server will shutdown in {minutes} minutes. Please logout before shutdown to prevent character corruption.";
        public string             Message2                               { get; set; } = "Server shutdown required. Server is about to shutdown, performing a world save.";
        public string             CancelMessage                          { get; set; } = "Server shutdown has been cancelled.";
        public List<string>       Branchs                                { get; set; } = new List<string> { "Live" };
        public bool               AutoBackup                             { get; set; } = true;
        public string             BackupInterval                         { get; set; } = "0100";
        public bool               EnableLogs                             { get; set; } = true;
        public int                MaxLogsDays                            { get; set; } = 30;
        public int                MaxLogFiles                            { get; set; } = 30;
        public List<ProfileOrder> ProfileOrders                          { get; set; } = new List<ProfileOrder>();
        
        public List<ModColors>    ModColors                              { get; set; } = new List<ModColors>();

        public void SaveSettings() {
            string fileName   = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
            string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(fileName, jsonString);
        }

        public static string GetDataFolder() {
            var Settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")));
            OphiussaLogger.ReconfigureLogging(Settings);
            return Settings.DataFolder;
        }

        public ModColors AddNewModColor(string Mod) {
            ModColors mc = new ModColors() { Mod   = Mod, Color = RandomPastelGenerator.GetNewPastelColor() };
            this.ModColors.Add(mc);
            return mc;
        }
    }
}