using Newtonsoft.Json;
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
        public string DefaultSteamKey { get { return "4DF03576EDA7C3350237D3E547E9CE3C"; } }
        public string DefaultCurseForgeKey { get { return "$2a$10$eKu6i/S1ipKbGYxiDGGjnO4BjVuBJCoTaw7DNg.6Qqf5y./9tQDIq"; } }
        public string SteamKey { get; set; }
        public bool UpdateSteamCMDOnStartup { get; set; }
        public string DataFolder { get; set; }
        public string DefaultInstallationFolder { get; set; }
        public string SteamCMDLocation { get; set; }
        public bool UseAnonymousConnection { get; set; }
        public string SteamUserName { get; set; }
        public string SteamPassword { get; set; }
        public bool ValidateProfileOnServerStart { get; set; }
        public bool PerformServerAndModUpdateOnServerStart { get; set; }
        public bool UpdateModsWhenUpdatingServer { get; set; }
        public string BackupDirectory { get; set; }
        public bool IncludeSaveGamesFolder { get; set; }
        public bool DeleteOldBackupFiles { get; set; }
        public int DeleteFilesDays { get; set; }
        public string WorldSaveMessage { get; set; }
        public bool AutoUpdate { get; set; }
        public string BackupInterval { get; set; }
        public bool PerformOnlinePlayerCheck { get; set; }
        public bool SendShutdowMessages { get; set; }
        public int GracePeriod { get; set; }
        public string Message1 { get; set; }
        public string Message2 { get; set; }
        public string CancelMessage { get; set; }
        public List<string> Branchs { get; set; }
        public Settings()
        {
            FileInfo f = new FileInfo(Assembly.GetExecutingAssembly().FullName);
            string drive = Path.GetPathRoot(f.FullName);
            DataFolder = drive + "osmdata\\";
            DefaultInstallationFolder = drive + "osmdata\\Servers\\";
            SteamCMDLocation = drive + "osmdata\\steamcmd\\";
            UseAnonymousConnection = true;
            SteamUserName = "";
            SteamPassword = "";
            ValidateProfileOnServerStart = true;
            PerformServerAndModUpdateOnServerStart = false;
            UpdateModsWhenUpdatingServer = true;
            BackupDirectory = drive + "osmBackups\\";
            IncludeSaveGamesFolder = true;
            DeleteOldBackupFiles = true;
            DeleteFilesDays = 30;
            WorldSaveMessage = "A world save is about to be performed, you may experience some lag during this process. Please be patient.";
            AutoUpdate = true;
            BackupInterval = "0100";
            PerformOnlinePlayerCheck = true;
            SendShutdowMessages = true;
            GracePeriod = 15;
            Message1 = "Server shutdown required. Server will shutdown in {minutes} minutes. Please logout before shutdown to prevent character corruption.";
            Message2 = "Server shutdown required. Server is about to shutdown, performing a world save.";
            CancelMessage = "Server shutdown has been cancelled.";
            Branchs = new List<string>() { "Live" };
        }

        public void SaveSettings()
        {
            string fileName = "config.json";
            string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
