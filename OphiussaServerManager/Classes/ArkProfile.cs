using OphiussaServerManager.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Profiles
{
    public class ArkProfile
    {
        public Administration Administration { get; set; }

        public ArkProfile()
        {
            this.Administration = new Administration();
            this.Administration.ServerName = "New Server";
            this.Administration.ServerPassword = System.Web.Security.Membership.GeneratePassword(10, 6);
            this.Administration.ServerAdminPassword = System.Web.Security.Membership.GeneratePassword(10, 6);
            this.Administration.ServerSpectatorPassword = "";
            this.Administration.LocalIP = NetworkTools.GetHostIp();
            this.Administration.ServerPort = "7777";
            this.Administration.PeerPort = "7778";
            this.Administration.QueryPort = "27015";
            this.Administration.UseRCON = false;
            this.Administration.RCONPort = "32330";
            this.Administration.RCONServerLogBuffer = 600;
            this.Administration.MapName = "";
            this.Administration.TotalConversionID = "";
            this.Administration.ModIDs = new List<string>();
            this.Administration.AutoSavePeriod = 15;
            this.Administration.MOD = "";
            this.Administration.MODDuration = 20;
            this.Administration.EnableInterval = true;
            this.Administration.MODInterval = 60;
            this.Administration.Branch = "Live";

        }

        public void LoadNewArkProfile(string key)
        {
            
        }
    }
    public class Administration
    {
        public string ServerName { get; set; }
        public string ServerPassword { get; set; }
        public string ServerAdminPassword { get; set; }
        public string ServerSpectatorPassword { get; set; }
        public string LocalIP { get; set; }
        public string ServerPort { get; set; }
        public string PeerPort { get; set; }
        public string QueryPort { get; set; }
        public bool UseRCON { get; set; }
        public string RCONPort { get; set; }
        public int RCONServerLogBuffer { get; set; }
        public string MapName { get; set; }
        public string TotalConversionID { get; set; }
        public List<string> ModIDs { get; set; }
        public int AutoSavePeriod { get; set; }
        public string MOD { get; set; }
        public int MODDuration { get; set; }
        public bool EnableInterval { get; set; }
        public int MODInterval { get; set; }
        public string Branch { get; set; }

    }
}
