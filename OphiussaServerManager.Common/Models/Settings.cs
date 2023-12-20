using Newtonsoft.Json;
using OphiussaServerManager.Common.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common.Models
{
    public class Settings
    {
        [JsonIgnore]
        public string CryptKey { get; set; } = "b14ca5898a4e4133bbce2ea2315a1916";
        public string GUID { get; set; }
        public string SteamKey { get; set; } = "";
        public string CurseForgeKey { get; set; } = "";
        public bool UpdateSteamCMDOnStartup { get; set; }
        public string DataFolder { get; set; } = "";
        public string DefaultInstallationFolder { get; set; }
        public string SteamCMDLocation { get; set; } = "";
        public bool UseAnonymousConnection { get; set; } = true;
        public string CryptedSteamUserName { get; set; } = "";
        public string CryptedSteamPassword { get; set; } = "";
        [JsonIgnore]
        public string SteamUserName { get { return EncryptionUtils.DecryptString(this.CryptKey, this.CryptedSteamUserName); } }
        [JsonIgnore]
        public string SteamPassword { get { return EncryptionUtils.DecryptString(this.CryptKey, this.CryptedSteamPassword); } }
        public bool ValidateProfileOnServerStart { get; set; } = true;
        public bool PerformServerAndModUpdateOnServerStart { get; set; } = false;
        public bool UpdateModsWhenUpdatingServer { get; set; } = true;
        public string BackupDirectory { get; set; } = "";
        public bool IncludeSaveGamesFolder { get; set; } = true;
        public bool DeleteOldBackupFiles { get; set; } = true;
        public int DeleteFilesDays { get; set; } = 30;
        public string WorldSaveMessage { get; set; } = "A world save is about to be performed, you may experience some lag during this process. Please be patient.";
        public bool AutoUpdate { get; set; } = true;
        public string UpdateInterval { get; set; } = "0100";
        public bool PerformOnlinePlayerCheck { get; set; } = true;
        public bool UseSmartCopy { get; set; } = true;
        public bool SendShutdowMessages { get; set; } = true;
        public int GracePeriod { get; set; } = 15;
        public string Message1 { get; set; } = "Server shutdown required. Server will shutdown in {minutes} minutes. Please logout before shutdown to prevent character corruption.";
        public string Message2 { get; set; } = "Server shutdown required. Server is about to shutdown, performing a world save.";
        public string CancelMessage { get; set; } = "Server shutdown has been cancelled.";
        public List<string> Branchs { get; set; } = new List<string>() { "Live" };
        public bool AutoBackup { get; set; } = true;
        public string BackupInterval { get; set; } = "0100";
        public bool EnableLogs { get; set; } = true;
        public int MaxLogsDays { get; set; } = 30;
        public int MaxLogFiles { get; set; } = 30;
        public Settings()
        {
            FileInfo f = new FileInfo(Assembly.GetExecutingAssembly().FullName);
            string drive = Path.GetPathRoot(f.FullName);
            DataFolder = drive + "osmdata\\";
            DefaultInstallationFolder = drive + "osmdata\\Servers\\";
            SteamCMDLocation = drive + "osmdata\\steamcmd\\";
            BackupDirectory = drive + "osmBackups\\";
            GUID = Guid.NewGuid().ToString();
        }

        public void SaveSettings()
        {
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
            string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
