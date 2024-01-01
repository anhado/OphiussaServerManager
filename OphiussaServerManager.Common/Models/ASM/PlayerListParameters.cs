using System.Dynamic;
using System.Windows;

namespace OphiussaServerManager.Common.Models {
    public class PlayerListParameters {
        public string ProfileName { get; set; } = "";

        public string ProfileId { get; set; }

        public string InstallDirectory { get; set; }

        public string AltSaveDirectoryName { get; set; }

        public string ServerMap { get; set; }

        public Rect WindowExtents { get; set; }

        public string WindowTitle { get; set; }
    }
}