using System;
using OphiussaFramework.Enums;

namespace OphiussaFramework.CommonUtils {
    public class Utils {
        public static Versions CompareVersion(Version oldVersion, Version NewVersion) {
            int result = oldVersion.CompareTo(NewVersion);
            if (result > 0) return Versions.Lower;
            if (result < 0) return Versions.Greater;
            return Versions.Equal;
        }
    }
}