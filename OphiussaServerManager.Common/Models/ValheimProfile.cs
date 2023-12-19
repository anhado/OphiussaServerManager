using OphiussaServerManager.Common.Models.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common.Models.ValheimProfile
{
    public enum Preset
    {
        Normal,
        Casual,
        Easy,
        Hard,
        Hardcore,
        Immersive,
        Hammer
    }

    public enum Combat
    {
        None,
        VeryEasy,
        Easy,
        Hard,
        VeryHard
    }

    public enum DeathPenalty
    {
        None,
        Casual,
        VeryEasy,
        Easy,
        Hard,
        HartdCore
    }

    public enum Resources
    {
        MuchLess,
        Less,
        More,
        MuchMore,
        Most
    }

    public enum Portals
    {
        Casual,
        Hard,
        VeryHard
    }


    public class ValheimProfile
    {
        public Administration Administration { get; set; }


        public ValheimProfile()
        {
            this.Administration = new Administration();

        }
        public void LoadNewValheimProfile(string key)
        {

        }

    }

    public class Administration
    {
        public string ServerName { get; set; } = "New Server";
        public string ServerPassword { get; set; } = System.Web.Security.Membership.GeneratePassword(10, 6);
        public string LocalIP { get; set; } = NetworkTools.GetHostIp();
        public string ServerPort { get; set; } = "7777";
        public string PeerPort { get; set; } = "7778";
        public string Branch { get; set; } = "Live";
        public bool Crossplay { get; set; } = false;
        public bool Public { get; set; } = false;
        public string InstanceID { get; set; }
        public string WordName { get; set; }
        public Preset Preset { get; set; }
        public int AutoSavePeriod { get; set; } = 30;
        public int TotalBackups { get; set; } = 3;
        public int BackupShort { get; set; } = 120;
        public int BackupLong { get; set; } = 720;
        public bool NoBuildcost { get; set; } = false;
        public bool PlayerEvents { get; set; } = false;
        public bool PassiveMobs { get; set; } = false;
        public bool NoMap { get; set; } = false;
        public Combat Combat { get; set; }
        public DeathPenalty DeathPenalty { get; set; }
        public Resources Resources { get; set; }
        public Portals Portals { get; set; }
        public string SaveLocation { get; set; }
        public string LogFileLocation { get; set; }
    }
}
