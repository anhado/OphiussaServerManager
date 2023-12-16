using OphiussaServerManager.Common.Models.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OphiussaServerManager.Common.Models
{
    public class LinkProfileForm
    {
        public Profile Profile { get; set; }
        public Form Form { get; set; }
        public TabPage Tab { get; set; }

    }

    public class PlayerList
    {
        public string PlayerNum { get; set; }
        public string Name { get; set; }
        public string SteamID { get; set; }
    }

    public class ConfigFile
    {
        public string PropertyValue { get; set; }
        public string PropertyName { get; set; }

    }

    public class ProcessEventArg
    { 
        public string Message { get; set; }
        public int TotalFiles { get; set; }
        public int ProcessedFileCount { get; set; }
        public bool isError { get; set; }
        public bool Sucessful { get; set; }
        public bool IsStarting { get; set; }
        public bool SendToDiscord { get; set; } = false;
    } 
}
