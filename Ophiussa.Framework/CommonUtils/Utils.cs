using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OphiussaFramework.Enums;

namespace OphiussaFramework.CommonUtils
{
    public class Utils
    {
        public static Versions CompareVersion(Version oldVersion, Version NewVersion)
        {
            var result = oldVersion.CompareTo(NewVersion);
            if (result > 0) return Versions.Lower;
            if (result < 0) return Versions.Greater;
            return Versions.Equal;
        }
    }
}
