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
using Open.Nat;
using System.Net.Sockets;
using FirewallManager;
using System.Web.Configuration;
using NetFwTypeLib;

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
                var NSettings = JsonConvert.DeserializeObject<Common.Models.Settings>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")));

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
                        if (addr.PrefixOrigin == PrefixOrigin.Dhcp || addr.PrefixOrigin == PrefixOrigin.Manual)
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
            string login = "+login anonymous";
            if (!Settings.UseAnonymousConnection) login = $"+login {Settings.SteamUserName} {Settings.SteamPassword}";

            Utils.ExecuteAsAdmin(Path.Combine(Settings.SteamCMDLocation, "steamcmd.exe"), $" +force_install_dir {profile.InstallLocation} {login} +app_update {profile.Type.SteamServerID} validate +quit");
        }
        public static void UpdateCacheFolder(CacheServerTypes serverType)
        {
            string login = "+login anonymous";
            if (!Settings.UseAnonymousConnection) login = $"+login {Settings.SteamUserName} {Settings.SteamPassword}";
            Utils.ExecuteAsAdmin(Path.Combine(Settings.SteamCMDLocation, "steamcmd.exe"), $" +force_install_dir {serverType.InstallCacheFolder} {login} +app_update {serverType.Type.SteamServerID} validate +quit", true, true);
        }
        public static void UpdateModCacheFolder(CacheServerTypes serverType)
        {
            string login = "+login anonymous";
            if (!Settings.UseAnonymousConnection) login = $"+login {Settings.SteamUserName} {Settings.SteamPassword}";
            Utils.ExecuteAsAdmin(Path.Combine(Settings.SteamCMDLocation, "steamcmd.exe"), $" +force_install_dir {Path.Combine(serverType.InstallCacheFolder)} {login} +workshop_download_item {serverType.Type.SteamClientID} {serverType.ModId} validate +quit", true, true);
        }

        public static bool IsPortOpen(string name, int port)
        {
            try
            {
                FirewallCom fw = new FirewallCom();
                var rules = fw.GetRules().ToList().FindAll(r => r.Name == name + " TCP");

                bool find = false;
                if (rules.Count > 0)
                {

                    rules.ForEach(r =>
                    {
                        if (r.RemotePorts.Contains(port.ToString()) || r.LocalPorts.Contains(port.ToString()))
                        {
                            find = true;
                        }
                    });
                }
                if (!find) return false;
                rules = fw.GetRules().ToList().FindAll(r => r.Name == name + " UDP");

                if (rules.Count > 0)
                {

                    rules.ForEach(r =>
                    {
                        if (r.RemotePorts.Contains(port.ToString()) || r.LocalPorts.Contains(port.ToString()))
                        {
                            find = true;
                        }
                    });
                }

                return find;
            }
            catch
            {
                return false;
            }
        }

        public static void AddRule(Rule rule)
        {
            INetFwPolicy2 _firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2")); ;

            INetFwRule rule1 = ConvertRule((Func<INetFwRule>)(() =>
                {
                    // ISSUE: variable of a compiler-generated type
                    INetFwRule instance = (INetFwRule)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
                    return instance;
                }), rule);

            _firewallPolicy.Rules.Add(rule1);
        }

        public static INetFwRule ConvertRule(Func<INetFwRule> func, Rule item)
        {
            // ISSUE: variable of a compiler-generated type
            INetFwRule netFwRule1 = func();
            netFwRule1.Action = Tools.Convert(item.Action);
            netFwRule1.Protocol = (int)Tools.Convert(item.Protocol);
            netFwRule1.ApplicationName = item.ApplicationName;
            netFwRule1.Description = item.Description;
            netFwRule1.Direction = Tools.Convert(item.Direction);
            netFwRule1.EdgeTraversal = item.EdgeTraversal;
            netFwRule1.Enabled = item.Enabled;
            netFwRule1.Grouping = item.Grouping;
            if (item.IcmpTypesAndCodes != null)
                netFwRule1.IcmpTypesAndCodes = item.IcmpTypesAndCodes;
            netFwRule1.InterfaceTypes = item.InterfaceTypes;
            netFwRule1.Interfaces = item.Interfaces;
            netFwRule1.LocalAddresses = item.LocalAddresses;
            if (item.LocalPorts != null)
                netFwRule1.LocalPorts = item.LocalPorts;
            netFwRule1.Name = item.Name;
            netFwRule1.Profiles = (int)Tools.Convert(item.Profiles);
            netFwRule1.RemoteAddresses = item.RemoteAddresses;
            if (item.RemotePorts != null)
                netFwRule1.RemotePorts = item.RemotePorts;
            netFwRule1.serviceName = item.ServiceName;
            // ISSUE: variable of a compiler-generated type
            INetFwRule netFwRule2 = netFwRule1;
            return netFwRule2;
        }

    }
}
