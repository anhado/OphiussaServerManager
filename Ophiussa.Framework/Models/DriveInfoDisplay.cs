using System.IO;

namespace OphiussaFramework.Models {
    public class DriveInfoDisplay {
        private const decimal Divisor = 1024M;

        private static readonly string[] Suffixes = new string[6] {
                                                                      "Bytes",
                                                                      "KB",
                                                                      "MB",
                                                                      "GB",
                                                                      "TB",
                                                                      "PB"
                                                                  };
        //private readonly GlobalizedApplication _globalizer = GlobalizedApplication.Instance;

        public DriveInfoDisplay(DriveInfo driveInfo) {
            DriveInfo = driveInfo;
        }

        public DriveInfo DriveInfo { get; set; }

        public string Line1 => DriveInfo == null ? string.Empty : (string.IsNullOrWhiteSpace(DriveInfo.VolumeLabel) ? "Local Disk" : DriveInfo.VolumeLabel) + " (" + DriveInfo.Name.Replace("\\", string.Empty) + ")";

        public string Line2 => DriveInfo == null ? string.Empty : string.Format("{0} free of {1}", FormatSize(DriveInfo.TotalFreeSpace), FormatSize(DriveInfo.TotalSize));

        public static string FormatSize(long bytes) {
            int     index = 0;
            decimal num   = bytes;
            while (num / 1024M >= 1M) {
                num /= 1024M;
                ++index;
            }

            return string.Format("{0:n2} {1}", num, Suffixes[index]);
        }
    }
}