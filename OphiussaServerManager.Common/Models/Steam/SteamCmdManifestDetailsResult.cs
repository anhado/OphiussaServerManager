using NeXt.Vdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common.Models
{
    public class SteamCmdManifestDetailsResult
    {
        public static bool ClearUserConfigBetaKeys(VdfValue data)
        {
            bool flag = false;
            if (data is VdfTable source1 && source1.FirstOrDefault<VdfValue>((Func<VdfValue, bool>)(v => v.Name.Equals("UserConfig", StringComparison.OrdinalIgnoreCase))) is VdfTable source2 && source2.Count > 0)
            {
                foreach (VdfValue vdfValue in source2.Where<VdfValue>((Func<VdfValue, bool>)(v => v.Name.Equals("betakey", StringComparison.OrdinalIgnoreCase))).ToArray<VdfValue>())
                {
                    source2.Remove(vdfValue);
                    flag = true;
                }
            }
            return flag;
        }

        public static SteamCmdAppManifest Deserialize(VdfValue data)
        {
            SteamCmdAppManifest steamCmdAppManifest = new SteamCmdAppManifest();
            if (data is VdfTable source)
            {
                VdfValue data1 = source.FirstOrDefault<VdfValue>((Func<VdfValue, bool>)(v => v.Name.Equals("appid", StringComparison.OrdinalIgnoreCase)));
                if (data1 != null)
                    steamCmdAppManifest.appid = SteamCmdManifestDetailsResult.GetValue(data1);
                if (source.FirstOrDefault<VdfValue>((Func<VdfValue, bool>)(v => v.Name.Equals("UserConfig", StringComparison.OrdinalIgnoreCase))) is VdfTable vdfTable && vdfTable.Count > 0)
                {
                    steamCmdAppManifest.UserConfig = new List<SteamCmdManifestUserConfig>();
                    foreach (VdfValue vdfValue in vdfTable)
                    {
                        if (vdfValue is VdfTable)
                            steamCmdAppManifest.UserConfig.Add(new SteamCmdManifestUserConfig()
                            {
                                betakey = vdfValue.Name
                            });
                    }
                }
            }
            return steamCmdAppManifest;
        }

        public static string GetValue(VdfValue data)
        {
            if (data == null)
                return (string)null;
            switch (data.Type)
            {
                case VdfValueType.String:
                    return ((VdfString)data).Content;
                case VdfValueType.Long:
                    return ((VdfLong)data).Content.ToString("G0");
                case VdfValueType.Decimal:
                    return ((VdfDecimal)data).Content.ToString("G0");
                default:
                    return (string)null;
            }
        }
    }
}
