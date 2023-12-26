using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Security;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Ini;

namespace OphiussaServerManager.Common.Models {
    public class BaseAdministration {
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_SessionSettings, ServerProfileCategory.Administration, "SessionName")]
        public string ServerName { get; set; } = "New Server";

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "ServerPassword")]
        public string ServerPassword { get; set; } = Membership.GeneratePassword(10, 6);

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_SessionSettings, ServerProfileCategory.Administration, "MultiHome")]
        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_MultiHome,       ServerProfileCategory.Administration, "MultiHome", WriteBoolValueIfNonEmpty = true)]
        public string LocalIp { get; set; } = NetworkTools.GetHostIp();

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_SessionSettings, ServerProfileCategory.Administration, "Port")]
        public string ServerPort { get; set; } = "7777";

        public string PeerPort { get; set; } = "7778";

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_ServerSettings, ServerProfileCategory.Administration, "ActiveMods")] //TODO:CHECK THIS BECAUSE IS SUPPOSED TO BE A STRING SPLITTED BY ,
        public string ActiveMods {
            get => string.Join(",", ModIDs.FindAll(m => !string.IsNullOrEmpty(m)).ToArray());
            set => ModIDs = value.Split(',').ToList().FindAll(m => !string.IsNullOrEmpty(m));
        }

        public List<string> ModIDs { get; set; } = new List<string>();

        [IniFileEntry(IniFiles.GameUserSettings, IniSections.GUS_GameSession, ServerProfileCategory.Administration, "MaxPlayers")]
        public int MaxPlayers { get; set; } = 70;

        public ProcessPriorityClass    CpuPriority     { get; set; } = ProcessPriorityClass.Normal;
        public string                  CpuAffinity     { get; set; } = "All";
        public List<ProcessorAffinity> CpuAffinityList { get; set; } = new List<ProcessorAffinity>();
    }

    public abstract class BaseProfile {
        public abstract string GetCommandLinesArguments(Settings settings, string locaIp);
        public abstract void   BackupServer(Settings             settings);

        public string GetCpuAffinity(string cpuAffinity, List<ProcessorAffinity> cpuAffinityList) {
            var lst = new List<ProcessorAffinity>();

            for (int i = Utils.GetProcessorCount() - 1; i >= 0; i--)
                lst.Add(
                        new ProcessorAffinity {
                                                  ProcessorNumber = i,
                                                  Selected        = cpuAffinity == "All" ? true : cpuAffinityList.DefaultIfEmpty(new ProcessorAffinity { Selected = true, ProcessorNumber = i }).FirstOrDefault(x => x.ProcessorNumber == i).Selected
                                              }
                       );

            string bin = string.Join("", lst.Select(x => x.Selected ? "1" : "0"));
            string hex = !bin.Contains("0") ? "" : "0" + Utils.BinaryStringToHexString(bin);
            return hex;
        }
    }
}