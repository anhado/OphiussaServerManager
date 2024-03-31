using System;
using System.Windows.Forms;
using Newtonsoft.Json;
using OphiussaFramework.DataBaseUtils;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;

namespace OphiussaFramework.ServerUtils {
    public static class ServerUtils {

        public static void InstallServerClick(object sender, OphiussaEventArgs e) {
            throw new NotImplementedException(); 
        }

        public static void BackupServerClick(object sender, OphiussaEventArgs e) {
            throw new NotImplementedException();
        }

        public static void StartServerClick(object sender, OphiussaEventArgs e) {
            throw new NotImplementedException();
        }

        public static void StopServerClick(object sender, OphiussaEventArgs e) {
            throw new NotImplementedException();
        }

        public static void ReloadServerClick(object sender, OphiussaEventArgs e) {
            throw new NotImplementedException();
        }

        public static void SyncServerClick(object sender, OphiussaEventArgs e) {
            throw new NotImplementedException();
        }

        public static void SaveServerClick(object sender, OphiussaEventArgs e) {
             
            IProfile profile           = e.Profile;
            profile.Type               = e.Plugin.GameType;
            profile.AdditionalSettings = null;
            profile.AdditionalCommands = null;
            profile.AdditionalSettings = JsonConvert.SerializeObject(e.Profile,                Formatting.Indented);
            profile.AdditionalCommands = JsonConvert.SerializeObject(e.Plugin.DefaultCommands, Formatting.Indented); 
            ConnectionController.SqlLite.Upsert<IProfile>(profile);
            foreach (AutoManagement opt in profile.AutoManagement) {
                opt.ServerKey = profile.Key;
                ConnectionController.SqlLite.Upsert<AutoManagement>(opt);
            }
        }

        public static void OpenRCONClick(object sender, OphiussaEventArgs e) {
            throw new NotImplementedException();
        }

        public static void ChooseFolderClick(object sender, OphiussaEventArgs e) {
            throw new NotImplementedException();
        }
    }
}