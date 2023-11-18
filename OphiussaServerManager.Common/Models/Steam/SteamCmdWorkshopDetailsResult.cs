
using NeXt.Vdf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OphiussaServerManager.Common.Models
{
    public class SteamCmdWorkshopDetailsResult
    {
        public static SteamCmdAppWorkshop Deserialize(VdfValue data)
        {
            SteamCmdAppWorkshop steamCmdAppWorkshop = new SteamCmdAppWorkshop();
            if (data is VdfTable source1)
            {
                VdfValue data1 = source1.FirstOrDefault<VdfValue>((Func<VdfValue, bool>)(v => v.Name.Equals("appid", StringComparison.OrdinalIgnoreCase)));
                if (data1 != null)
                    steamCmdAppWorkshop.appid = SteamCmdWorkshopDetailsResult.GetValue(data1);
                VdfValue data2 = source1.FirstOrDefault<VdfValue>((Func<VdfValue, bool>)(v => v.Name.Equals("SizeOnDisk", StringComparison.OrdinalIgnoreCase)));
                if (data2 != null)
                    steamCmdAppWorkshop.SizeOnDisk = SteamCmdWorkshopDetailsResult.GetValue(data2);
                VdfValue data3 = source1.FirstOrDefault<VdfValue>((Func<VdfValue, bool>)(v => v.Name.Equals("NeedsUpdate", StringComparison.OrdinalIgnoreCase)));
                if (data3 != null)
                    steamCmdAppWorkshop.NeedsUpdate = SteamCmdWorkshopDetailsResult.GetValue(data3);
                VdfValue data4 = source1.FirstOrDefault<VdfValue>((Func<VdfValue, bool>)(v => v.Name.Equals("NeedsDownload", StringComparison.OrdinalIgnoreCase)));
                if (data4 != null)
                    steamCmdAppWorkshop.NeedsDownload = SteamCmdWorkshopDetailsResult.GetValue(data4);
                VdfValue data5 = source1.FirstOrDefault<VdfValue>((Func<VdfValue, bool>)(v => v.Name.Equals("TimeLastUpdated", StringComparison.OrdinalIgnoreCase)));
                if (data5 != null)
                    steamCmdAppWorkshop.TimeLastUpdated = SteamCmdWorkshopDetailsResult.GetValue(data5);
                VdfValue data6 = source1.FirstOrDefault<VdfValue>((Func<VdfValue, bool>)(v => v.Name.Equals("TimeLastAppRan", StringComparison.OrdinalIgnoreCase)));
                if (data6 != null)
                    steamCmdAppWorkshop.TimeLastAppRan = SteamCmdWorkshopDetailsResult.GetValue(data6);
                if (source1.FirstOrDefault<VdfValue>((Func<VdfValue, bool>)(v => v.Name.Equals("WorkshopItemsInstalled", StringComparison.OrdinalIgnoreCase))) is VdfTable vdfTable1 && vdfTable1.Count > 0)
                {
                    steamCmdAppWorkshop.WorkshopItemsInstalled = new List<SteamCmdWorkshopItemsInstalled>();
                    foreach (VdfValue source in vdfTable1)
                    {
                        if (source is VdfTable)
                        {
                            SteamCmdWorkshopItemsInstalled workshopItemsInstalled = new SteamCmdWorkshopItemsInstalled();
                            workshopItemsInstalled.publishedfileid = source.Name;
                            VdfValue data7 = ((IEnumerable<VdfValue>)source).FirstOrDefault<VdfValue>((Func<VdfValue, bool>)(v => v.Name.Equals("manifest", StringComparison.OrdinalIgnoreCase)));
                            if (data7 != null)
                                workshopItemsInstalled.manifest = SteamCmdWorkshopDetailsResult.GetValue(data7);
                            VdfValue data8 = ((IEnumerable<VdfValue>)source).FirstOrDefault<VdfValue>((Func<VdfValue, bool>)(v => v.Name.Equals("size", StringComparison.OrdinalIgnoreCase)));
                            if (data8 != null)
                                workshopItemsInstalled.size = SteamCmdWorkshopDetailsResult.GetValue(data8);
                            VdfValue data9 = ((IEnumerable<VdfValue>)source).FirstOrDefault<VdfValue>((Func<VdfValue, bool>)(v => v.Name.Equals("timeupdated", StringComparison.OrdinalIgnoreCase)));
                            if (data9 != null)
                                workshopItemsInstalled.timeupdated = SteamCmdWorkshopDetailsResult.GetValue(data9);
                            steamCmdAppWorkshop.WorkshopItemsInstalled.Add(workshopItemsInstalled);
                        }
                    }
                }
                if (source1.FirstOrDefault<VdfValue>((Func<VdfValue, bool>)(v => v.Name.Equals("WorkshopItemDetails", StringComparison.OrdinalIgnoreCase))) is VdfTable vdfTable2 && vdfTable2.Count > 0)
                {
                    steamCmdAppWorkshop.WorkshopItemDetails = new List<SteamCmdWorkshopItemDetails>();
                    foreach (VdfValue source in vdfTable2)
                    {
                        if (source is VdfTable)
                        {
                            SteamCmdWorkshopItemDetails workshopItemDetails = new SteamCmdWorkshopItemDetails();
                            workshopItemDetails.publishedfileid = source.Name;
                            VdfValue data10 = ((IEnumerable<VdfValue>)source).FirstOrDefault<VdfValue>((Func<VdfValue, bool>)(v => v.Name.Equals("manifest", StringComparison.OrdinalIgnoreCase)));
                            if (data10 != null)
                                workshopItemDetails.manifest = SteamCmdWorkshopDetailsResult.GetValue(data10);
                            VdfValue data11 = ((IEnumerable<VdfValue>)source).FirstOrDefault<VdfValue>((Func<VdfValue, bool>)(v => v.Name.Equals("timeupdated", StringComparison.OrdinalIgnoreCase)));
                            if (data11 != null)
                                workshopItemDetails.timeupdated = SteamCmdWorkshopDetailsResult.GetValue(data11);
                            VdfValue data12 = ((IEnumerable<VdfValue>)source).FirstOrDefault<VdfValue>((Func<VdfValue, bool>)(v => v.Name.Equals("timetouched", StringComparison.OrdinalIgnoreCase)));
                            if (data12 != null)
                                workshopItemDetails.timetouched = SteamCmdWorkshopDetailsResult.GetValue(data12);
                            steamCmdAppWorkshop.WorkshopItemDetails.Add(workshopItemDetails);
                        }
                    }
                }
            }
            return steamCmdAppWorkshop;
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
