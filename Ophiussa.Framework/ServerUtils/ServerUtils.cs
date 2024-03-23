using System.Windows.Forms;
using OphiussaFramework.Models;

namespace OphiussaFramework.ServerUtils {
    public static class ServerUtils {
        public static void InstallServerClick(object sender, InstallEventArgs e) {
            MessageBox.Show("Clicked InstallServerClick");
        }

        public static void BackupServerClick(object sender, InstallEventArgs e) {
            MessageBox.Show("Clicked BackupServerClick");
        }

        public static void StartServerClick(object sender, InstallEventArgs e) {
            MessageBox.Show("Clicked StartServerClick");
        }

        public static void StopServerClick(object sender, InstallEventArgs e) {
            MessageBox.Show("Clicked StopServerClick");
        }
    }
}