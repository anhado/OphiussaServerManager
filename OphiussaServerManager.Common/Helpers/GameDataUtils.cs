using System;
using System.Collections.Generic;
using System.IO;
using OphiussaServerManager.Common.Models;

namespace OphiussaServerManager.Common.Helpers {
    public static class GameDataUtils {
        public static void ReadAllData(out MainGameData data, string dataFolder, string extension, string application, bool isUserData = false) {
            data = new MainGameData();

            if (string.IsNullOrWhiteSpace(dataFolder))
                return;

            if (!Directory.Exists(dataFolder))
                return;

            foreach (string file in Directory.GetFiles(dataFolder, $"*{extension}", SearchOption.TopDirectoryOnly))
                try {
                    var fileData = MainGameData.Load(file, isUserData);
                    if (fileData == null)
                        continue;

                    if (!fileData.Application.Equals(application, StringComparison.OrdinalIgnoreCase))
                        continue;

                    data.Creatures.AddRange(fileData.Creatures);
                    data.Engrams.AddRange(fileData.Engrams);
                    data.Items.AddRange(fileData.Items);
                    data.MapSpawners.AddRange(fileData.MapSpawners);
                    data.SupplyCrates.AddRange(fileData.SupplyCrates);
                    data.Inventories.AddRange(fileData.Inventories);
                    data.GameMaps.AddRange(fileData.GameMaps);
                    data.TotalConversions.AddRange(fileData.TotalConversions);
                    data.PlayerLevels.AddRange(fileData.PlayerLevels);
                    data.CreatureLevels.AddRange(fileData.CreatureLevels);
                    data.Branches.AddRange(fileData.Branches);
                    data.Events.AddRange(fileData.Events);
                    data.OfficialMods.AddRange(fileData.OfficialMods);
                    data.RconInputModes.AddRange(fileData.RconInputModes);

                    if (fileData.PlayerAdditionalLevels > 0 && fileData.PlayerAdditionalLevels > data.PlayerAdditionalLevels)
                        data.PlayerAdditionalLevels = fileData.PlayerAdditionalLevels;
                }
                catch {
                    // do nothing, just swallow the error
                }
        }
    }

    public class BaseGameData {
        public string   Application  = string.Empty;
        public string   Color        = "White";
        public DateTime Created      = DateTime.UtcNow;
        public string   GameDataFile = string.Empty;
        public string   Version      = "1.0.0";

        public static BaseGameData Load(string file) {
            if (string.IsNullOrWhiteSpace(file) || !File.Exists(file))
                return null;

            var data                            = JsonUtils.DeserializeFromFile<BaseGameData>(file);
            if (data != null) data.GameDataFile = file;
            return data;
        }

        public bool Save(string file) {
            string folder = Path.GetDirectoryName(file);
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            return JsonUtils.SerializeToFile(this, file);
        }
    }

    public class MainGameData : BaseGameData {
        public List<BranchDataItem> Branches = new List<BranchDataItem>();

        public List<CreatureLevelDataItem> CreatureLevels = new List<CreatureLevelDataItem>();
        public List<CreatureDataItem>      Creatures      = new List<CreatureDataItem>();

        public List<EngramDataItem> Engrams = new List<EngramDataItem>();

        public List<EventDataItem> Events = new List<EventDataItem>();

        public List<GameMapDataItem> GameMaps = new List<GameMapDataItem>();

        public List<InventoryDataItem> Inventories = new List<InventoryDataItem>();

        public List<ItemDataItem> Items = new List<ItemDataItem>();

        public List<MapSpawnerDataItem> MapSpawners = new List<MapSpawnerDataItem>();

        public List<OfficialModItem> OfficialMods = new List<OfficialModItem>();

        public int PlayerAdditionalLevels;

        public List<PlayerLevelDataItem> PlayerLevels = new List<PlayerLevelDataItem>();

        public List<RconInputModeItem> RconInputModes = new List<RconInputModeItem>();

        public List<SupplyCrateDataItem> SupplyCrates = new List<SupplyCrateDataItem>();

        public List<TotalConversionDataItem> TotalConversions = new List<TotalConversionDataItem>();

        public static MainGameData Load(string file, bool isUserData) {
            if (string.IsNullOrWhiteSpace(file) || !File.Exists(file))
                return null;

            var data = JsonUtils.DeserializeFromFile<MainGameData>(file);
            if (data != null) {
                data.GameDataFile = file;
                data.Creatures.ForEach(c => c.IsUserData        = isUserData);
                data.Engrams.ForEach(c => c.IsUserData          = isUserData);
                data.Items.ForEach(c => c.IsUserData            = isUserData);
                data.MapSpawners.ForEach(c => c.IsUserData      = isUserData);
                data.SupplyCrates.ForEach(c => c.IsUserData     = isUserData);
                data.Inventories.ForEach(c => c.IsUserData      = isUserData);
                data.GameMaps.ForEach(c => c.IsUserData         = isUserData);
                data.TotalConversions.ForEach(c => c.IsUserData = isUserData);
                data.Branches.ForEach(c => c.IsUserData         = isUserData);
                data.Events.ForEach(c => c.IsUserData           = isUserData);
                data.OfficialMods.ForEach(c => c.IsUserData     = isUserData);
                data.RconInputModes.ForEach(c => c.IsUserData   = isUserData);
            }

            return data;
        }
    }

    public class BaseDataItem {
        public string ClassName   = string.Empty;
        public string Description = string.Empty;

        public bool   IsUserData;
        public string Mod = string.Empty;
    }

    public class CreatureDataItem : BaseDataItem {
        public DinoBreedingable IsBreedingable = DinoBreedingable.False;
        public bool             IsSpawnable    = false;

        public DinoTamable IsTameable = DinoTamable.False;
        public string      NameTag    = string.Empty;

        public string IsTameableString {
            get => IsTameable.ToString();
            set {
                if (!Enum.TryParse(value, true, out IsTameable))
                    IsTameable = DinoTamable.False;
            }
        }

        public string IsBreedingableString {
            get => IsBreedingable.ToString();
            set {
                if (!Enum.TryParse(value, true, out IsBreedingable))
                    IsBreedingable = DinoBreedingable.False;
            }
        }
    }

    public class EngramDataItem : BaseDataItem {
        public bool IsTekGram = false;
        public int  Level     = 0;
        public int  Points    = 0;
    }

    public class ItemDataItem : BaseDataItem {
        public string Category      = string.Empty;
        public bool   IsHarvestable = false;
    }

    public class MapSpawnerDataItem : BaseDataItem {
    }

    public class SupplyCrateDataItem : BaseDataItem {
    }

    public class InventoryDataItem : BaseDataItem {
    }

    public class GameMapDataItem : BaseDataItem {
        public bool IsSotF = false;
    }

    public class TotalConversionDataItem : BaseDataItem {
        public bool IsSotF = false;
    }

    public class PlayerLevelDataItem {
        public long EngramPoints = 0;
        public long XPRequired   = 0;
    }

    public class CreatureLevelDataItem {
        public long XPRequired = 0;
    }

    public class BranchDataItem {
        public string BranchName  = string.Empty;
        public string Description = string.Empty;
        public bool   IsSotF      = false;

        public bool IsUserData;
    }

    public class EventDataItem {
        public string Description = string.Empty;
        public string EventName   = string.Empty;
        public bool   IsSotF      = false;

        public bool IsUserData;
    }

    public class OfficialModItem {
        public bool   IsUserData;
        public string ModId   = string.Empty;
        public string ModName = string.Empty;
    }

    public class RconInputModeItem {
        public string Command     = string.Empty;
        public string Description = string.Empty;

        public bool IsUserData;
    }
}