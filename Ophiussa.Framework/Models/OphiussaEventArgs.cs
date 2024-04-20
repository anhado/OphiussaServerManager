using OphiussaFramework.Interfaces;

namespace OphiussaFramework.Models {
    public class OphiussaEventArgs {
        public IProfile Profile          { get; set; }
        public IPlugin  Plugin           { get; set; } 
        public bool     ForceStopServer  { get; set; }
        public bool     InstallFromCache { get; set; }
        public bool     ShowSteamCMD     { get; set; }
        public bool     StartServerAtEnd  { get; set; }
    }
}