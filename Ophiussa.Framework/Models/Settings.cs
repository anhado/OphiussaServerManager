﻿using System;
using System.IO;
using System.Reflection;

namespace OphiussaFramework.Models {
    public class Settings {
        public Settings() {
        }

        public Settings(string pDrive = "", string pFoldernName = "osmdata") {
            var    f                = new FileInfo(Assembly.GetExecutingAssembly().FullName);
            string drive            = Path.GetPathRoot(f.FullName);
            if (pDrive != "") drive = pDrive;

            DataFolder           = drive + $"{pFoldernName}\\";
            DefaultInstallFolder = drive + $"{pFoldernName}\\Servers\\";
            SteamCMDFolder       = drive + $"{pFoldernName}\\steamcmd\\";
            BackupFolder         = drive + $"{pFoldernName}\\osmBackups\\";
            GUID                 = Guid.NewGuid().ToString();
        }


        [FieldAttributes(PrimaryKey = true, DataType = "Varchar(100)")]
        public string GUID { get; set; }

        public                                  string DataFolder           { get; set; }
        public                                  string DefaultInstallFolder { get; set; }
        public                                  string SteamCMDFolder       { get; set; }
        public                                  string SteamWepApiKey       { get; set; }
        public                                  string CurseForgeApiKey     { get; set; }
        public                                  string BackupFolder         { get; set; }
        public                                  bool   EnableLogs           { get; set; }
        public                                  int    MaxLogFiles          { get; set; }
        public                                  int    MaxLogsDays          { get; set; }
        [FieldAttributes(Ignore = true)] public string CryptKey             { get; set; } = "b14ca5898a4e4133bbce2ea2315a1916";
        public                                  bool   UseAnonymous         { get; set; }
        public                                  bool   UpdateSteamCMDStart  { get; set; }
        public                                  string SteamUser            { get; set; }
        public                                  string SteamPwd             { get; set; }
    }
}