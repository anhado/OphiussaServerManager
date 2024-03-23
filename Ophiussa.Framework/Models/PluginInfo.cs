using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaFramework.Models
{
    public class PluginInfo
    {
        public string PluginName { get; set; }
        public string GameType   { get; set; }
        public string GameName   { get; set; }
        public string Version    { get; set; }
        public bool   Loaded     { get; set; }
    }
}
