using System;
using System.Collections.Generic;
using System.Linq;
using NeXt.Vdf;

namespace OphiussaServerManager.Common.Models {
    public class SteamCmdManifestDetailsResult {
        public static bool ClearUserConfigBetaKeys(VdfValue data) {
            bool flag = false;
            if (data is VdfTable source1 && source1.FirstOrDefault(v => v.Name.Equals("UserConfig", StringComparison.OrdinalIgnoreCase)) is VdfTable source2 && source2.Count > 0)
                foreach (var vdfValue in source2.Where(v => v.Name.Equals("betakey", StringComparison.OrdinalIgnoreCase)).ToArray()) {
                    source2.Remove(vdfValue);
                    flag = true;
                }

            return flag;
        }

        public static SteamCmdAppManifest Deserialize(VdfValue data) {
            var steamCmdAppManifest = new SteamCmdAppManifest();
            if (data is VdfTable source) {
                var data1 = source.FirstOrDefault(v => v.Name.Equals("appid", StringComparison.OrdinalIgnoreCase));
                if (data1 != null)
                    steamCmdAppManifest.Appid = GetValue(data1);
                if (source.FirstOrDefault(v => v.Name.Equals("UserConfig", StringComparison.OrdinalIgnoreCase)) is VdfTable vdfTable && vdfTable.Count > 0) {
                    steamCmdAppManifest.UserConfig = new List<SteamCmdManifestUserConfig>();
                    foreach (var vdfValue in vdfTable)
                        if (vdfValue is VdfTable)
                            steamCmdAppManifest.UserConfig.Add(new SteamCmdManifestUserConfig {
                                                                                                  Betakey = vdfValue.Name
                                                                                              });
                }
            }

            return steamCmdAppManifest;
        }

        public static string GetValue(VdfValue data) {
            if (data == null)
                return null;
            switch (data.Type) {
                case VdfValueType.String:
                    return ((VdfString)data).Content;
                case VdfValueType.Long:
                    return ((VdfLong)data).Content.ToString("G0");
                case VdfValueType.Decimal:
                    return ((VdfDecimal)data).Content.ToString("G0");
                default:
                    return null;
            }
        }
    }
}