using System;
using System.Collections.Generic;
using System.Linq;
using NeXt.Vdf;

namespace OphiussaServerManager.Common.Models {
    public class SteamCmdWorkshopDetailsResult {
        public static SteamCmdAppWorkshop Deserialize(VdfValue data) {
            var steamCmdAppWorkshop = new SteamCmdAppWorkshop();
            if (data is VdfTable source1) {
                var data1 = source1.FirstOrDefault(v => v.Name.Equals("appid", StringComparison.OrdinalIgnoreCase));
                if (data1 != null)
                    steamCmdAppWorkshop.Appid = GetValue(data1);
                var data2 = source1.FirstOrDefault(v => v.Name.Equals("SizeOnDisk", StringComparison.OrdinalIgnoreCase));
                if (data2 != null)
                    steamCmdAppWorkshop.SizeOnDisk = GetValue(data2);
                var data3 = source1.FirstOrDefault(v => v.Name.Equals("NeedsUpdate", StringComparison.OrdinalIgnoreCase));
                if (data3 != null)
                    steamCmdAppWorkshop.NeedsUpdate = GetValue(data3);
                var data4 = source1.FirstOrDefault(v => v.Name.Equals("NeedsDownload", StringComparison.OrdinalIgnoreCase));
                if (data4 != null)
                    steamCmdAppWorkshop.NeedsDownload = GetValue(data4);
                var data5 = source1.FirstOrDefault(v => v.Name.Equals("TimeLastUpdated", StringComparison.OrdinalIgnoreCase));
                if (data5 != null)
                    steamCmdAppWorkshop.TimeLastUpdated = GetValue(data5);
                var data6 = source1.FirstOrDefault(v => v.Name.Equals("TimeLastAppRan", StringComparison.OrdinalIgnoreCase));
                if (data6 != null)
                    steamCmdAppWorkshop.TimeLastAppRan = GetValue(data6);
                if (source1.FirstOrDefault(v => v.Name.Equals("WorkshopItemsInstalled", StringComparison.OrdinalIgnoreCase)) is VdfTable vdfTable1 && vdfTable1.Count > 0) {
                    steamCmdAppWorkshop.WorkshopItemsInstalled = new List<SteamCmdWorkshopItemsInstalled>();
                    foreach (var source in vdfTable1)
                        if (source is VdfTable) {
                            var workshopItemsInstalled = new SteamCmdWorkshopItemsInstalled();
                            workshopItemsInstalled.Publishedfileid = source.Name;
                            var data7 = ((IEnumerable<VdfValue>)source).FirstOrDefault(v => v.Name.Equals("manifest", StringComparison.OrdinalIgnoreCase));
                            if (data7 != null)
                                workshopItemsInstalled.Manifest = GetValue(data7);
                            var data8 = ((IEnumerable<VdfValue>)source).FirstOrDefault(v => v.Name.Equals("size", StringComparison.OrdinalIgnoreCase));
                            if (data8 != null)
                                workshopItemsInstalled.Size = GetValue(data8);
                            var data9 = ((IEnumerable<VdfValue>)source).FirstOrDefault(v => v.Name.Equals("timeupdated", StringComparison.OrdinalIgnoreCase));
                            if (data9 != null)
                                workshopItemsInstalled.Timeupdated = GetValue(data9);
                            steamCmdAppWorkshop.WorkshopItemsInstalled.Add(workshopItemsInstalled);
                        }
                }

                if (source1.FirstOrDefault(v => v.Name.Equals("WorkshopItemDetails", StringComparison.OrdinalIgnoreCase)) is VdfTable vdfTable2 && vdfTable2.Count > 0) {
                    steamCmdAppWorkshop.WorkshopItemDetails = new List<SteamCmdWorkshopItemDetails>();
                    foreach (var source in vdfTable2)
                        if (source is VdfTable) {
                            var workshopItemDetails = new SteamCmdWorkshopItemDetails();
                            workshopItemDetails.Publishedfileid = source.Name;
                            var data10 = ((IEnumerable<VdfValue>)source).FirstOrDefault(v => v.Name.Equals("manifest", StringComparison.OrdinalIgnoreCase));
                            if (data10 != null)
                                workshopItemDetails.Manifest = GetValue(data10);
                            var data11 = ((IEnumerable<VdfValue>)source).FirstOrDefault(v => v.Name.Equals("timeupdated", StringComparison.OrdinalIgnoreCase));
                            if (data11 != null)
                                workshopItemDetails.Timeupdated = GetValue(data11);
                            var data12 = ((IEnumerable<VdfValue>)source).FirstOrDefault(v => v.Name.Equals("timetouched", StringComparison.OrdinalIgnoreCase));
                            if (data12 != null)
                                workshopItemDetails.Timetouched = GetValue(data12);
                            steamCmdAppWorkshop.WorkshopItemDetails.Add(workshopItemDetails);
                        }
                }
            }

            return steamCmdAppWorkshop;
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