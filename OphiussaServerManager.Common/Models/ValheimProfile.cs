using OphiussaServerManager.Common.Models.Profiles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
        Default,
        VeryEasy,
        Easy,
        Hard,
        VeryHard
    }

    public enum DeathPenalty
    {
        Default,
        Casual,
        VeryEasy,
        Easy,
        Hard,
        HardCore
    }

    public enum Resources
    {
        Default,
        MuchLess,
        Less,
        More,
        MuchMore,
        Most
    }

    public enum Portals
    {
        Default,
        Casual,
        Hard,
        VeryHard
    }

    public enum Raids
    {
        Default,
        None,
        MuchLess,
        Less,
        More,
        MuchMore
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

        public string GetCommandLinesArguments(Settings settings, Profile profile, string locaIP)
        {
            string cmd = string.Empty;

            List<string> hifenArgs = new List<string>();

            hifenArgs.Add($" -nographics");
            hifenArgs.Add($" -batchmode");
            hifenArgs.Add($" -name \"{this.Administration.ServerName}\"");
            hifenArgs.Add($" -port {this.Administration.ServerPort}");
            hifenArgs.Add($" -world \"{this.Administration.WordName}\"");
            hifenArgs.Add($" -password \"{this.Administration.ServerPassword}\"");
            if (!string.IsNullOrEmpty(this.Administration.SaveLocation)) hifenArgs.Add($" -savedir \"{this.Administration.SaveLocation}\"");
            hifenArgs.Add($" -public {(this.Administration.Public ? 1 : 0)}");
            if (!string.IsNullOrEmpty(this.Administration.LogFileLocation)) hifenArgs.Add($" -logFile \"{this.Administration.LogFileLocation}\\VAL_{profile.Key}.log\"");
            hifenArgs.Add($" -saveinterval {this.Administration.AutoSavePeriod * 60}");
            hifenArgs.Add($" -backups {this.Administration.TotalBackups}");
            hifenArgs.Add($" -backupshort {this.Administration.BackupShort * 60}");
            hifenArgs.Add($" -backuplong {this.Administration.BackupLong * 60}");
            if (this.Administration.Crossplay) hifenArgs.Add(" -crossplay");
            if (!string.IsNullOrEmpty(this.Administration.InstanceID)) hifenArgs.Add($" -instanceid \"{this.Administration.InstanceID}\"");
            hifenArgs.Add($" -preset {this.Administration.Preset.ToString().ToLower()}");
            if (this.Administration.Combat != Combat.Default) hifenArgs.Add($" -modifier combat {this.Administration.Combat.ToString().ToLower()}");
            if (this.Administration.DeathPenalty != DeathPenalty.Default) hifenArgs.Add($" -modifier deathpenalty {this.Administration.DeathPenalty.ToString().ToLower()}");
            if (this.Administration.Resources != Resources.Default) hifenArgs.Add($" -modifier resources {this.Administration.Resources.ToString().ToLower()}");
            if (this.Administration.Raids != Raids.Default) hifenArgs.Add($" -modifier raids {this.Administration.Raids.ToString().ToLower()}");
            if (this.Administration.Portals != Portals.Default) hifenArgs.Add($" -modifier portals {this.Administration.Portals.ToString().ToLower()}");
            if (this.Administration.NoBuildcost) hifenArgs.Add(" -setkey nobuildcost");
            if (this.Administration.PlayerEvents) hifenArgs.Add(" -setkey playerevents");
            if (this.Administration.PassiveMobs) hifenArgs.Add(" -setkey passivemobs");
            if (this.Administration.NoMap) hifenArgs.Add(" -setkey nomap");


            cmd += string.Join("", hifenArgs.ToArray());

            return cmd;
        }

        internal string GetCPUAffinity()
        {
            //TODO: dublicated function
            List<ProcessorAffinity> lst = new List<ProcessorAffinity>();

            for (int i = Utils.GetProcessorCount() - 1; i >= 0; i--)
            {
                lst.Add(
                    new ProcessorAffinity()
                    {
                        ProcessorNumber = i,
                        Selected = this.Administration.CPUAffinity == "All" ? true : this.Administration.CPUAffinityList.DefaultIfEmpty(new ProcessorAffinity() { Selected = true, ProcessorNumber = i }).FirstOrDefault(x => x.ProcessorNumber == i).Selected
                    }
                    );
            }
            string bin = string.Join("", lst.Select(x => x.Selected ? "1" : "0"));
            string hex = !bin.Contains("0") ? "" : "0" + Utils.BinaryStringToHexString(bin);
            return hex;
        }
    }

    public class Administration
    {
        public string ServerName { get; set; } = "New Server";
        public string ServerPassword { get; set; } = System.Web.Security.Membership.GeneratePassword(10, 6);
        public string LocalIP { get; set; } = NetworkTools.GetHostIp();
        public string ServerPort { get; set; } = "2456";
        public string PeerPort { get; set; } = "2457";
        public string Branch { get; set; } = "Live";
        public bool Crossplay { get; set; } = false;
        public bool Public { get; set; } = false;
        public string InstanceID { get; set; } = "";
        public string WordName { get; set; } = "NewWorld";
        public Preset Preset { get; set; } = Preset.Normal;
        public int AutoSavePeriod { get; set; } = 30;
        public int TotalBackups { get; set; } = 3;
        public int BackupShort { get; set; } = 120;
        public int BackupLong { get; set; } = 720;
        public bool NoBuildcost { get; set; } = false;
        public bool PlayerEvents { get; set; } = false;
        public bool PassiveMobs { get; set; } = false;
        public bool NoMap { get; set; } = false;
        public Combat Combat { get; set; } = Combat.Default;
        public DeathPenalty DeathPenalty { get; set; } = DeathPenalty.Default;
        public Resources Resources { get; set; } = Resources.Default;
        public Portals Portals { get; set; } = Portals.Default;
        public Raids Raids { get; set; } = Raids.Default;
        public string SaveLocation { get; set; } = "";
        public string LogFileLocation { get; set; } = "";
        public ProcessPriorityClass CPUPriority { get; set; } = ProcessPriorityClass.Normal;
        public string CPUAffinity { get; set; } = "All";
        public List<ProcessorAffinity> CPUAffinityList { get; set; } = new List<ProcessorAffinity>();
    }
}
