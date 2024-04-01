using OphiussaFramework.Interfaces;
using System;

namespace OphiussaFramework.Models {
    public class OphiussaEventArgs {
        public IProfile Profile         { get; set; }
        public IPlugin  Plugin          { get; set; }
        public bool     ForceStopServer { get; set; }
    }
}