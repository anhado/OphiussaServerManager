using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;

namespace OphiussaFramework.CommonUtils {
    public class NetworkTools {
        public static string GetHostIp() {
            string ipAddress = "";
            var    host      = default(IPHostEntry);
            string hostname  = null;
            hostname = Environment.MachineName;
            host     = Dns.GetHostEntry(hostname);
            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    ipAddress = Convert.ToString(ip);
            return ipAddress;
        }

        public static List<IpList> GetAllHostIp() {
            var ret = new List<IpList>();
            foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces())
                if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet) {
                    Console.WriteLine("Name: "        + netInterface.Name);
                    Console.WriteLine("Description: " + netInterface.Description);
                    Console.WriteLine("Addresses: ");

                    var ipProps = netInterface.GetIPProperties();

                    foreach (var addr in ipProps.UnicastAddresses)
                        if (addr.PrefixOrigin == PrefixOrigin.Dhcp || addr.PrefixOrigin == PrefixOrigin.Manual) {
                            Console.WriteLine(" " + addr.Address);
                            ret.Add(new IpList { Ip = addr.Address.ToString(), Description = $"({addr.Address})" + netInterface.Description });
                        }
                }

            return ret;
        }

        public static async Task<string> DiscoverPublicIPAsync() {
            using (var webClient = new WebClient()) {
                try {
                    string publicIP = await webClient.DownloadStringTaskAsync("https://api.ipify.org");
                    if (IPAddress.TryParse(publicIP, out var address1)) return publicIP;

                    publicIP = await webClient.DownloadStringTaskAsync("http://whatismyip.akamai.com/");
                    if (IPAddress.TryParse(publicIP, out var address2)) return publicIP;
                }
                catch (Exception ex) {
                    //OphiussaLogger.Logger.Error($"{nameof(DiscoverPublicIPAsync)} - Exception checking for public ip. {ex.Message}");
                }

                return string.Empty;
            }
        }

        public static void DownloadSteamCmd() {
            using (var wc = new WebClient()) {
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadFileCompleted   += wc_DownloadCompleted;
                wc.DownloadFileAsync(
                                     // Param1 = Link of file
                                     new Uri("https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip"),
                                     // Param2 = Path to save
                                     ConnectionController.Settings.DataFolder + "steamcmd.zip"
                                    );
            }
        }

        public static void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) {
            //progressBar.Value = e.ProgressPercentage;
        }

        public static void wc_DownloadCompleted(object sender, AsyncCompletedEventArgs e) {
            using (var archive = ZipFile.OpenRead(ConnectionController.Settings.DataFolder + "steamcmd.zip")) {
                foreach (var entry in archive.Entries) entry.ExtractToFile(Path.Combine(ConnectionController.Settings.SteamCMDFolder, entry.FullName), true);
            }

            Utils.ExecuteAsAdmin(Path.Combine(ConnectionController.Settings.SteamCMDFolder, "steamcmd.exe"), "+quit");
        }

        internal static void UpdateCacheFolder(ServerCache cache, bool showSteamCMD) { 
            if (!Directory.Exists(cache.CacheFolder)) Directory.CreateDirectory(cache.CacheFolder);
            string login                                           = "+login anonymous";
            if (!ConnectionController.Settings.UseAnonymous) login = $"+login {ConnectionController.Settings.SteamUser} {ConnectionController.Settings.SteamPwd}";
            Utils.ExecuteAsAdmin(Path.Combine(ConnectionController.Settings.SteamCMDFolder, "steamcmd.exe"), $" +force_install_dir {cache.CacheFolder} {login} +app_update {cache.SteamServerId} validate +quit", true, !showSteamCMD);
        }

        internal static void UpdateCacheFolder(PluginController controller, bool showSteamCMD) { 
            if (!Directory.Exists(controller.CacheFolder)) Directory.CreateDirectory(controller.CacheFolder);
            string login                                           = "+login anonymous";
            if (!ConnectionController.Settings.UseAnonymous) login = $"+login {ConnectionController.Settings.SteamUser} {ConnectionController.Settings.SteamPwd}";
            Utils.ExecuteAsAdmin(Path.Combine(ConnectionController.Settings.SteamCMDFolder, "steamcmd.exe"), $" +force_install_dir {controller.CacheFolder} {login} +app_update {controller.GetProfile().SteamServerId} validate +quit", true, !showSteamCMD);
        }

        public static void UpdateGameFolder(IProfile profile) {
            if (!Directory.Exists(profile.InstallationFolder)) Directory.CreateDirectory(profile.InstallationFolder);
            string login                                           = "+login anonymous";
            if (!ConnectionController.Settings.UseAnonymous) login = $"+login {ConnectionController.Settings.SteamUser} {ConnectionController.Settings.SteamPwd}";
            Utils.ExecuteAsAdmin(Path.Combine(ConnectionController.Settings.SteamCMDFolder, "steamcmd.exe"), $" +force_install_dir {profile.InstallationFolder} {login} +app_update {profile.SteamServerId} validate +quit", true, true);
        }

        public class IpList {
            public string Ip          { get; set; }
            public string Description { get; set; }
        }
    }
}