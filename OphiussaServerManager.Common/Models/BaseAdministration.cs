using OphiussaServerManager.Common.Models.Profiles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common.Models
{
    public class BaseAdministration
    {
        public string ServerName { get; set; } = "New Server";
        public string ServerPassword { get; set; } = System.Web.Security.Membership.GeneratePassword(10, 6);
        public string LocalIP { get; set; } = NetworkTools.GetHostIp();
        public string ServerPort { get; set; } = "7777";
        public string PeerPort { get; set; } = "7778";
        public List<string> ModIDs { get; set; } = new List<string>();
        public int MaxPlayers { get; set; } = 70;
        public ProcessPriorityClass CPUPriority { get; set; } = ProcessPriorityClass.Normal;
        public string CPUAffinity { get; set; } = "All";
        public List<ProcessorAffinity> CPUAffinityList { get; set; } = new List<ProcessorAffinity>();

    }

    public abstract class BaseProfile
    {
        public abstract string GetCommandLinesArguments(Settings settings, string locaIP);
        public abstract void BackupServer(Settings settings);
        public string GetCPUAffinity(string CPUAffinity, List<ProcessorAffinity> CPUAffinityList)
        {
            List<ProcessorAffinity> lst = new List<ProcessorAffinity>();

            for (int i = Utils.GetProcessorCount() - 1; i >= 0; i--)
            {
                lst.Add(
                    new ProcessorAffinity()
                    {
                        ProcessorNumber = i,
                        Selected = CPUAffinity == "All" ? true : CPUAffinityList.DefaultIfEmpty(new ProcessorAffinity() { Selected = true, ProcessorNumber = i }).FirstOrDefault(x => x.ProcessorNumber == i).Selected
                    }
                    );
            }
            string bin = string.Join("", lst.Select(x => x.Selected ? "1" : "0"));
            string hex = !bin.Contains("0") ? "" : "0" + Utils.BinaryStringToHexString(bin);
            return hex;
        }
    }
}
