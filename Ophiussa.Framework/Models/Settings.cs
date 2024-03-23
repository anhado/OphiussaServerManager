using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaFramework.Models
{
    public class Settings
    {
        public Settings() {}

        public Settings(string pDrive = "", string pFoldernName = "osmdata")
        {
            var    f                = new FileInfo(Assembly.GetExecutingAssembly().FullName);
            string drive            = Path.GetPathRoot(f.FullName);
            if (pDrive != "") drive = pDrive;

            DataFolder           = drive + $"{pFoldernName}\\";
            DefaultInstallFolder = drive + $"{pFoldernName}\\Servers\\";
            SteamCMDFolder       = drive + $"{pFoldernName}\\steamcmd\\";
            BackupFolder         = drive + $"{pFoldernName}\\osmBackups\\";
            GUID                 = System.Guid.NewGuid().ToString();
        }
         
        public string GUID                 { get; set; }
        public string DataFolder           { get; set; }
        public string DefaultInstallFolder { get; set; }
        public string SteamCMDFolder       { get; set; }
        public string SteamWepApiKey       { get; set; }
        public string CurseForgeApiKey     { get; set; }
        public string BackupFolder         { get; set; }
        public bool   EnableLogs           { get; set; }
        public int    MaxLogFiles          { get; set; }
        public int    MaxLogsDays          { get; set; }
    }
}
