using System;
using System.IO;
using System.Runtime.InteropServices;

namespace OphiussaServerManager.Common.Helpers {
    public static class IoUtils {
        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteFile(string name);

        public static bool IsUnc(string path) {
            return new Uri(path).IsUnc;
        }

        public static string NormalizeFolder(string path) {
            string str = path.TrimEnd('\\') + "\\";
            if (IsUnc(str))
                return str.TrimEnd('\\');
            string pathRoot = Path.GetPathRoot(str);
            if (!pathRoot.EndsWith("\\"))
                pathRoot += "\\";
            if (!string.Equals(pathRoot, str, StringComparison.OrdinalIgnoreCase))
                str = str.TrimEnd('\\');
            return str;
        }

        public static string NormalizePath(string path) {
            return Path.GetFullPath(new Uri(path).LocalPath).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar).ToLowerInvariant();
        }

        public static bool Unblock(string fileName) {
            return DeleteFile(fileName + ":Zone.Identifier");
        }
    }
}