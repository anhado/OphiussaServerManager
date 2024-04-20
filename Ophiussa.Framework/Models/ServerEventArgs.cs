using OphiussaFramework.Enums;
using OphiussaFramework.Interfaces;

namespace OphiussaFramework.Models {
    public class ServerEventArgs {
        public IProfile     Profile { get; set; }
        public IPlugin      Plugin  { get; set; }
        public ServerStatus Status  { get; set; }
    }
} 