using OphiussaServerManager.Tools;
using OphiussaServerManager.Tools.Update;

namespace OphiussaServerManager {
    internal class ServerTools {
        public static void UpdateAllServer() {
            var autoUpdate = new AutoUpdate();
            autoUpdate.UpdateAllServers();
        }

        public static void UpdateSingleServerJob1(string profileKey, bool restartOnlyToUpdate = false) {
            var autoUpdate = new AutoUpdate();
            autoUpdate.UpdateSingleServerJob1(profileKey, restartOnlyToUpdate);
        }

        public static void UpdateSingleServerJob2(string profileKey, bool restartOnlyToUpdate = false) {
            var autoUpdate = new AutoUpdate();
            autoUpdate.UpdateSingleServerJob2(profileKey, restartOnlyToUpdate);
        }

        public static void BackupAllServer() {
            var autoBackup = new AutoBackup();
            autoBackup.BackupAllServers();
        }

        public static void BackupSingleServer(string profileKey) {
            var autoBackup = new AutoBackup();
            autoBackup.BackupSingleServers(profileKey);
        }
    }
}