using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common.Models
{
    public class PortForwardGrid
    {
        public string Profile { get; set; }
        public string ServerName { get; set; }
        public Bitmap RouterServerPort { get; set; }
        public Bitmap RouterPeerPort { get; set; }
        public Bitmap RouterQueryPort { get; set; }
        public Bitmap RouterRconPort { get; set; }
        public Bitmap FirewallServerPort { get; set; }
        public Bitmap FirewallPeerPort { get; set; }
        public Bitmap FirewallQueryPort { get; set; }
        public Bitmap FirewallRconPort { get; set; }
        public ushort ServerPort { get; set; }
        public ushort PeerPort { get; set; }
        public ushort QueryPort { get; set; }
        public ushort RconPort { get; set; }
        public bool isOK { get; set; }
    }
}
