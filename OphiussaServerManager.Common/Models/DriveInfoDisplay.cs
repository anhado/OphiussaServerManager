using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common.Models
{

    public class DriveInfoDisplay
    {
        private const Decimal DIVISOR = 1024M;
        private static readonly string[] suffixes = new string[6]
        {
      "Bytes",
      "KB",
      "MB",
      "GB",
      "TB",
      "PB"
        };
        //private readonly GlobalizedApplication _globalizer = GlobalizedApplication.Instance;

        public DriveInfoDisplay(DriveInfo driveInfo) => this.DriveInfo = driveInfo;

        public DriveInfo DriveInfo { get; set; }

        public string Line1 => this.DriveInfo == null ? string.Empty : (string.IsNullOrWhiteSpace(this.DriveInfo.VolumeLabel) ? "Local Disk" : this.DriveInfo.VolumeLabel) + " (" + this.DriveInfo.Name.Replace("\\", string.Empty) + ")";

        public string Line2 => this.DriveInfo == null ? string.Empty : string.Format("{0} free of {1}", (object)DriveInfoDisplay.FormatSize(this.DriveInfo.TotalFreeSpace), (object)DriveInfoDisplay.FormatSize(this.DriveInfo.TotalSize));

        public static string FormatSize(long bytes)
        {
            int index = 0;
            Decimal num = (Decimal)bytes;
            while (num / 1024M >= 1M)
            {
                num /= 1024M;
                ++index;
            }
            return string.Format("{0:n2} {1}", (object)num, (object)DriveInfoDisplay.suffixes[index]);
        }
    }
}
