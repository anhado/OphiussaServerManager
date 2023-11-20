using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.NetworkInformation;
using System.ComponentModel;
using System.IO.Compression;
using System.Diagnostics;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models;
using Newtonsoft.Json;

namespace OphiussaServerManager.Common
{

    public class IpList
    {
        public string IP { get; set; }
        public string Description { get; set; }
    }

    public class NetworkTools
    {
        public static Settings Settings
        {
            get
            {
                var NSettings = JsonConvert.DeserializeObject<Common.Models.Settings>(File.ReadAllText("config.json"));

                return NSettings;
            }
        }
        public static string GetHostIp()
        {
            string IPAddress = "";
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
            return IPAddress;
        }

        public static List<IpList> GetAllHostIp()
        {
            List<IpList> ret = new List<IpList>();
            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    Console.WriteLine("Name: " + netInterface.Name);
                    Console.WriteLine("Description: " + netInterface.Description);
                    Console.WriteLine("Addresses: ");

                    IPInterfaceProperties ipProps = netInterface.GetIPProperties();

                    foreach (UnicastIPAddressInformation addr in ipProps.UnicastAddresses)
                    {
                        if (addr.PrefixOrigin == PrefixOrigin.Dhcp)
                        {
                            Console.WriteLine(" " + addr.Address.ToString());
                            ret.Add(new IpList() { IP = addr.Address.ToString(), Description = $"({addr.Address.ToString()})" + netInterface.Description });
                        }
                    }

                }
            }
            return ret;
        }

        public static string GetPublicIp()
        {
            int retrys = 0;
            String address = "";

            while (retrys <= 5)
            {
                try
                {
                    WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
                    using (WebResponse response = request.GetResponse())
                    using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                    {
                        address = stream.ReadToEnd();
                    }

                    int first = address.IndexOf("Address: ") + 9;
                    int last = address.LastIndexOf("</body>");
                    address = address.Substring(first, last - first);

                    retrys = 99;
                }
                catch (Exception ex)
                {
                    retrys++;
                    address = ex.Message;
                }
            }

            return address;

            //var discoverer = new NatDiscoverer();
            //var device = await discoverer.DiscoverDeviceAsync();
            //var ip = await device.GetExternalIPAsync();

            //return ip.ToString();
        }

        public static void DownloadSteamCMD()
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                    wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadCompleted);
                    wc.DownloadFileAsync(
                        // Param1 = Link of file
                        new System.Uri("https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip"),
                        // Param2 = Path to save
                        Settings.DataFolder + "steamcmd.zip"
                    );
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }
        public static void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //progressBar.Value = e.ProgressPercentage;
        }
        public static void wc_DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            using (ZipArchive archive = ZipFile.OpenRead(Settings.DataFolder + "steamcmd.zip"))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    entry.ExtractToFile(Path.Combine(Settings.SteamCMDLocation, entry.FullName), true);
                }
            }

            Utils.ExecuteAsAdmin(Path.Combine(Settings.SteamCMDLocation, "steamcmd.exe"), "+quit");
        }


        public static void InstallGame(Profile profile)
        {
            //profile.Type.SteamAppID G:\asmdata\SteamCMD\steamcmd.exe +force_install_dir G:\ASA\server\ +login anonymous +app_update 2430930 validate +quit
            Utils.ExecuteAsAdmin(Path.Combine(Settings.SteamCMDLocation, "steamcmd.exe"), $" +force_install_dir {profile.InstallLocation} +login anonymous +app_update {profile.Type.SteamAppID} validate +quit");
        }
        public static void UpdateCacheFolder(CacheServerTypes serverType)
        {
            //profile.Type.SteamAppID G:\asmdata\SteamCMD\steamcmd.exe +force_install_dir G:\ASA\server\ +login anonymous +app_update 2430930 validate +quit
            Utils.ExecuteAsAdmin(Path.Combine(Settings.SteamCMDLocation, "steamcmd.exe"), $" +force_install_dir {serverType.InstallCacheFolder} +login anonymous +app_update {serverType.Type.SteamAppID} validate +quit",true,true);
        }
    }
}
