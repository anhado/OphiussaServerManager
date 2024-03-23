using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaFramework.CommonUtils
{
    public class NetworkTools
    {
        public class IpList
        {
            public string Ip          { get; set; }
            public string Description { get; set; }
        }

        public static string GetHostIp()
        {
            string ipAddress = "";
            var host = default(IPHostEntry);
            string hostname = null;
            hostname = Environment.MachineName;
            host = Dns.GetHostEntry(hostname);
            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    ipAddress = Convert.ToString(ip);
            return ipAddress;
        }

        public static List<IpList> GetAllHostIp()
        {
            var ret = new List<IpList>();
            foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces())
                if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    Console.WriteLine("Name: " + netInterface.Name);
                    Console.WriteLine("Description: " + netInterface.Description);
                    Console.WriteLine("Addresses: ");

                    var ipProps = netInterface.GetIPProperties();

                    foreach (var addr in ipProps.UnicastAddresses)
                        if (addr.PrefixOrigin == PrefixOrigin.Dhcp || addr.PrefixOrigin == PrefixOrigin.Manual)
                        {
                            Console.WriteLine(" " + addr.Address);
                            ret.Add(new IpList { Ip = addr.Address.ToString(), Description = $"({addr.Address})" + netInterface.Description });
                        }
                }

            return ret;
        }

        public static async Task<string> DiscoverPublicIPAsync()
        {
            using (var webClient = new WebClient())
            {
                try
                {
                    var publicIP = await webClient.DownloadStringTaskAsync("https://api.ipify.org");
                    if (IPAddress.TryParse(publicIP, out IPAddress address1))
                    {
                        return publicIP;
                    }

                    publicIP = await webClient.DownloadStringTaskAsync("http://whatismyip.akamai.com/");
                    if (IPAddress.TryParse(publicIP, out IPAddress address2))
                    {
                        return publicIP;
                    }
                }
                catch (Exception ex)
                {
                    //OphiussaLogger.Logger.Error($"{nameof(DiscoverPublicIPAsync)} - Exception checking for public ip. {ex.Message}");
                }

                return String.Empty;
            }
        }
    }
}
