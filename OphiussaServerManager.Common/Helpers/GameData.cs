using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OphiussaServerManager.Common.Models;

namespace OphiussaServerManager.Common.Helpers {
    public static class GameData {
        public const string MOD_ALL     = "All";
        public const string MOD_UNKNOWN = "Unknown";

        public const string RCONINPUTMODE_COMMAND = "Command";

        public static string MainDataFolder = Path.Combine(Environment.CurrentDirectory, "GameData");
        public static string UserDataFolder = Path.Combine(Settings.GetDataFolder(),     "GameData");

        public static long DefaultMaxExperiencePointsDino   = 10;
        public static long DefaultMaxExperiencePointsPlayer = 5;

        public static MainGameData gameData;
        public static List<string> OfficialMods { get; set; } = new List<string>();

        public static event EventHandler GameDataLoaded;

        public static void Initialize() {
            Load();
            OnGameDataLoaded();
        }

        public static void AddOfficialMods(List<string> modIds) {
            if (OfficialMods == null)
                OfficialMods = new List<string>();

            if (modIds != null) {
                var modIdsToAdd = modIds.Where(m => !string.IsNullOrWhiteSpace(m) && !OfficialMods.Contains(m)).Distinct();
                if (modIdsToAdd.Any()) OfficialMods.AddRange(modIdsToAdd);
            }
        }

        private static void Load() {
            // read static game data
            GameDataUtils.ReadAllData(out gameData, MainDataFolder, ".gamedata", "ark");

            // read user game data
            var userGameData = new MainGameData();
            if (!UserDataFolder.Equals(UserDataFolder, StringComparison.OrdinalIgnoreCase)) GameDataUtils.ReadAllData(out userGameData, UserDataFolder, ".gamedata", "ark", true);

            // creatures
            gameData.Creatures.AddRange(userGameData.Creatures);

            dinoSpawns      = gameData.Creatures.ConvertAll(item => new DinoSpawn { ClassName       = item.ClassName, Mod = item.Mod, KnownDino = true, DinoNameTag = item.NameTag }).ToArray();
            dinoMultipliers = gameData.Creatures.ConvertAll(item => new ClassMultiplier { ClassName = item.ClassName }).ToArray();

            // engrams
            gameData.Engrams.AddRange(userGameData.Engrams);

            engrams = gameData.Engrams.ConvertAll(item => new Engram { EngramClassName = item.ClassName, EngramLevelRequirement = item.Level, EngramPointsCost = item.Points, Mod = item.Mod, KnownEngram = true, IsTekgram = item.IsTekGram }).ToArray();

            // items
            gameData.Items.AddRange(userGameData.Items);

            items = gameData.Items.ConvertAll(item => new PrimalItem { ClassName = item.ClassName, Mod = item.Mod, KnownItem = true, Category = item.Category }).ToArray();

            // resources
            resourceMultipliers = gameData.Items.Where(item => item.IsHarvestable).Select(item => new ResourceClassMultiplier { ClassName = item.ClassName, Mod = item.Mod, KnownResource = true }).ToArray();

            // map spawners
            gameData.MapSpawners.AddRange(userGameData.MapSpawners);

            mapSpawners = gameData.MapSpawners.ConvertAll(item => new MapSpawner { ClassName = item.ClassName, Mod = item.Mod, KnownSpawner = true }).ToArray();

            // supply crates
            gameData.SupplyCrates.AddRange(userGameData.SupplyCrates);

            var crates = gameData.SupplyCrates.ConvertAll(item => new SupplyCrate { ClassName = item.ClassName, Mod = item.Mod, KnownSupplyCrate = true });

            // inventories
            gameData.Inventories.AddRange(userGameData.Inventories);

            crates.AddRange(gameData.Inventories.ConvertAll(item => new SupplyCrate { ClassName = item.ClassName, Mod = item.Mod, KnownSupplyCrate = true }));

            supplyCrates = crates.ToArray();

            // game maps
            gameData.GameMaps.AddRange(userGameData.GameMaps);

            //TODO:GAME DATA MAPS
            if (gameData.GameMaps.Count > 0) {
                /*
                var maps1 = gameMaps.ToList();
                maps1.AddRange(gameData.GameMaps.Where(item => !item.IsSotF).Select(item => new ComboBoxItem { ValueMember = item.ClassName, DisplayMember = item.Description }));
                var maps2 = gameMapsSotF.ToList();
                maps2.AddRange(gameData.GameMaps.Where(item => item.IsSotF).Select(item => new ComboBoxItem { ValueMember = item.ClassName, DisplayMember = item.Description }));

                gameMaps     = maps1.ToArray();
                gameMapsSotF = maps2.ToArray();*/
            }

            // total conversion mods
            gameData.TotalConversions.AddRange(userGameData.TotalConversions);

            //TODO:GAME DATA TotalConversions
            if (gameData.TotalConversions.Count > 0) {
                /* var mods1 = totalConversions.ToList();
                 mods1.AddRange(gameData.TotalConversions.Where(item => !item.IsSotF).Select(item => new ComboBoxItem { ValueMember = item.ClassName, DisplayMember = item.Description }));
                 var mods2 = totalConversionsSotF.ToList();
                 mods2.AddRange(gameData.TotalConversions.Where(item => item.IsSotF).Select(item => new ComboBoxItem { ValueMember = item.ClassName, DisplayMember = item.Description }));

                 totalConversions     = mods1.ToArray();
                 totalConversionsSotF = mods2.ToArray();*/
            }

            // creature levels
            if (userGameData.CreatureLevels.Count > 0)
                gameData.CreatureLevels = userGameData.CreatureLevels;

            if (gameData.CreatureLevels.Count > 0) {
                levelsDino                     = gameData.CreatureLevels.ConvertAll(item => new Level { XPRequired = item.XPRequired }).ToArray();
                DefaultMaxExperiencePointsDino = levelsDino.Max(l => l.XPRequired) + 1;
            }

            // player levels
            if (userGameData.PlayerLevels.Count > 0)
                gameData.PlayerLevels = userGameData.PlayerLevels;

            LevelsPlayerAdditional = userGameData.PlayerAdditionalLevels;

            if (gameData.PlayerLevels.Count > 0) {
                levelsPlayer                     = gameData.PlayerLevels.ConvertAll(item => new Level { EngramPoints = item.EngramPoints, XPRequired = item.XPRequired }).ToArray();
                DefaultMaxExperiencePointsPlayer = levelsPlayer.Max(l => l.XPRequired) + 1;
            }

            if (gameData.PlayerAdditionalLevels > LevelsPlayerAdditional)
                LevelsPlayerAdditional = gameData.PlayerAdditionalLevels;

            // branches
            gameData.Branches.AddRange(userGameData.Branches);

            //TODO:GAME DATA BRANCHES
            if (gameData.Branches.Count > 0) {
                /* var branches1 = branches.ToList();
                 branches1.AddRange(gameData.Branches.Where(item => !item.IsSotF).Select(item => new ComboBoxItem { ValueMember = item.BranchName, DisplayMember = item.Description }));
                 var branches2 = branchesSotF.ToList();
                 branches2.AddRange(gameData.Branches.Where(item => item.IsSotF).Select(item => new ComboBoxItem { ValueMember = item.BranchName, DisplayMember = item.Description }));

                 branches     = branches1.ToArray();
                 branchesSotF = branches2.ToArray();*/
            }

            // events
            gameData.Events.AddRange(userGameData.Events);

            //TODO:GAME DATA Events
            if (gameData.Events.Count > 0) {
                /*var events1 = events.ToList();
                events1.AddRange(gameData.Events.Where(item => !item.IsSotF).Select(item => new ComboBoxItem { ValueMember = item.EventName, DisplayMember = item.Description }));
                var events2 = eventsSotF.ToList();
                events2.AddRange(gameData.Events.Where(item => item.IsSotF).Select(item => new ComboBoxItem { ValueMember = item.EventName, DisplayMember = item.Description }));

                events     = events1.ToArray();
                eventsSotF = events2.ToArray();*/
            }

            // official mods
            gameData.OfficialMods.AddRange(userGameData.OfficialMods);

            if (gameData.OfficialMods.Count > 0) AddOfficialMods(gameData.OfficialMods.Where(m => !string.IsNullOrWhiteSpace(m.ModId)).Select(m => m.ModId).ToList());

            // rcon input modes
            gameData.RconInputModes.AddRange(userGameData.RconInputModes);

            //TODO:GAME DATA RconInputModes
            if (gameData.RconInputModes.Count > 0) {
                /* var modes1 = rconInputModes.ToList();
                 modes1.AddRange(gameData.RconInputModes.Select(item => new ComboBoxItem { ValueMember = item.Command, DisplayMember = item.Description }));

                 rconInputModes = modes1.ToArray();*/
            }
        }

        private static void OnGameDataLoaded() {
            GameDataLoaded?.Invoke(null, EventArgs.Empty);
        }

        public static void Reload() {
            gameData = null;

            Load();
            OnGameDataLoaded();
        }

        public static string FriendlyNameForClass(string className, bool returnNullIfNotFound = false) {
            return string.IsNullOrWhiteSpace(className) ? returnNullIfNotFound ? null : string.Empty : returnNullIfNotFound ? null : className;
        }

        #region Rcon input Modes

        /*public static ComboBoxItem[] rconInputModes = new[] {
                                                                 new ComboBoxItem { ValueMember = RCONINPUTMODE_COMMAND, DisplayMember = FriendlyNameForClass($"InputMode_{RCONINPUTMODE_COMMAND}") },
                                                             };

        public static IEnumerable<ComboBoxItem> GetAllRconInputModes() => rconInputModes.Select(m => m.Duplicate());

        public static IEnumerable<ComboBoxItem> GetMessageRconInputModes() => rconInputModes.Where(m => !m.ValueMember.Equals(RCONINPUTMODE_COMMAND, StringComparison.OrdinalIgnoreCase)).Select(m => m.Duplicate());
*/
        public static string FriendlyRconInputModeName(string rconInputMode, bool returnEmptyIfNotFound = false) {
            return string.IsNullOrWhiteSpace(rconInputMode)
                       ? string.Empty
                       : gameData?.RconInputModes?.FirstOrDefault(i => i.Command.Equals(rconInputMode))?.Description ??
                         (returnEmptyIfNotFound ? string.Empty : rconInputMode);
        }

        #endregion

        #region Creatures

        public static DinoSpawn[] dinoSpawns = new DinoSpawn[0];

        public static IEnumerable<DinoSpawn> GetDinoSpawns() {
            return dinoSpawns.Select(d => d.Duplicate<DinoSpawn>());
        }

        public static IEnumerable<NPCReplacement> GetNPCReplacements() {
            return dinoSpawns.Select(d => new NPCReplacement { FromClassName = d.ClassName, ToClassName = d.ClassName });
        }

        public static bool HasCreatureForClass(string className) {
            return !string.IsNullOrWhiteSpace(className) && dinoSpawns.Any(e => e.ClassName.Equals(className));
        }

        public static bool IsSpawnableForClass(string className) {
            return gameData?.Creatures?.FirstOrDefault(c => c.ClassName.Equals(className))?.IsSpawnable ?? true;
        }

        public static DinoTamable IsTameableForClass(string className) {
            return gameData?.Creatures?.FirstOrDefault(c => c.ClassName.Equals(className))?.IsTameable ?? DinoTamable.True;
        }

        public static DinoBreedingable IsBreedingableForClass(string className) {
            return gameData?.Creatures?.FirstOrDefault(c => c.ClassName.Equals(className))?.IsBreedingable ?? DinoBreedingable.True;
        }

        public static string NameTagForClass(string className, bool returnEmptyIfNotFound = false) {
            return gameData?.Creatures?.FirstOrDefault(c => c.ClassName.Equals(className))?.NameTag ?? (returnEmptyIfNotFound ? string.Empty : className);
        }

        public static string FriendlyCreatureNameForClass(string className, bool returnEmptyIfNotFound = false) {
            return string.IsNullOrWhiteSpace(className) ? string.Empty : gameData?.Creatures?.FirstOrDefault(i => i.ClassName.Equals(className))?.Description ?? (returnEmptyIfNotFound ? string.Empty : className);
        }

        public static ClassMultiplier[] dinoMultipliers = new ClassMultiplier[0];

        public static IEnumerable<ClassMultiplier> GetDinoMultipliers() {
            return dinoMultipliers.Select(d => d.Duplicate<ClassMultiplier>());
        }

        #endregion

        #region Engrams

        public static Engram[] engrams = new Engram[0];

        public static IEnumerable<Engram> GetEngrams() {
            return engrams.Select(d => d.Duplicate());
        }

        public static IEnumerable<EngramEntry> GetEngramEntries() {
            return engrams.Select(d => new EngramEntry { EngramClassName = d.EngramClassName, EngramLevelRequirement = d.EngramLevelRequirement, EngramPointsCost = d.EngramPointsCost });
        }

        public static Engram GetEngramForClass(string className) {
            return engrams.FirstOrDefault(e => e.EngramClassName.Equals(className));
        }

        public static bool HasEngramForClass(string className) {
            return !string.IsNullOrWhiteSpace(className) && engrams.Any(e => e.EngramClassName.Equals(className));
        }

        public static bool IsTekgram(string className) {
            return engrams.Any(e => e.EngramClassName.Equals(className) && e.IsTekgram);
        }

        public static string FriendlyEngramNameForClass(string className, bool returnEmptyIfNotFound = false) {
            return string.IsNullOrWhiteSpace(className) ? string.Empty : gameData?.Engrams?.FirstOrDefault(i => i.ClassName.Equals(className))?.Description ?? (returnEmptyIfNotFound ? string.Empty : className);
        }

        #endregion

        #region Items

        public static PrimalItem[] items = new PrimalItem[0];

        public static IEnumerable<PrimalItem> GetItems() {
            return items.Select(d => d.Duplicate());
        }

        public static PrimalItem GetItemForClass(string className) {
            return items.FirstOrDefault(e => e.ClassName.Equals(className));
        }

        public static bool HasItemForClass(string className) {
            return !string.IsNullOrWhiteSpace(className) && items.Any(e => e.ClassName.Equals(className));
        }

        public static string FriendlyItemNameForClass(string className, bool returnEmptyIfNotFound = false) {
            return string.IsNullOrWhiteSpace(className) ? string.Empty : gameData?.Items?.FirstOrDefault(i => i.ClassName.Equals(className))?.Description ?? (returnEmptyIfNotFound ? string.Empty : className);
        }

        public static string FriendlyItemModNameForClass(string className) {
            return string.IsNullOrWhiteSpace(className) ? string.Empty : FriendlyNameForClass($"Mod_{gameData?.Items?.FirstOrDefault(i => i.ClassName.Equals(className))?.Mod}", true) ?? string.Empty;
        }

        #endregion

        #region Resources

        public static ResourceClassMultiplier[] resourceMultipliers = new ResourceClassMultiplier[0];

        public static IEnumerable<ResourceClassMultiplier> GetResourceMultipliers() {
            return resourceMultipliers.Select(d => d.Duplicate<ResourceClassMultiplier>());
        }

        public static ResourceClassMultiplier GetResourceMultiplierForClass(string className) {
            return resourceMultipliers.FirstOrDefault(e => e.ClassName.Equals(className));
        }

        public static bool HasResourceMultiplierForClass(string className) {
            return !string.IsNullOrWhiteSpace(className) && resourceMultipliers.Any(e => e.ClassName.Equals(className));
        }

        public static string FriendlyResourceNameForClass(string className) {
            return string.IsNullOrWhiteSpace(className) ? string.Empty : gameData?.Items?.FirstOrDefault(i => i.ClassName.Equals(className) && i.IsHarvestable)?.Description ?? className;
        }

        #endregion

        #region Map Spawners

        public static MapSpawner[] mapSpawners = new MapSpawner[0];

        public static IEnumerable<MapSpawner> GetMapSpawners() {
            return mapSpawners.Select(d => d.Duplicate());
        }

        public static MapSpawner GetMapSpawnerForClass(string className) {
            return mapSpawners.FirstOrDefault(e => e.ClassName.Equals(className));
        }

        public static bool HasMapSpawnerForClass(string className) {
            return !string.IsNullOrWhiteSpace(className) && mapSpawners.Any(e => e.ClassName.Equals(className));
        }

        public static string FriendlyMapSpawnerNameForClass(string className, bool returnEmptyIfNotFound = false) {
            return string.IsNullOrWhiteSpace(className) ? string.Empty : gameData?.MapSpawners?.FirstOrDefault(i => i.ClassName.Equals(className))?.Description ?? (returnEmptyIfNotFound ? string.Empty : className);
        }

        #endregion

        #region Supply Crates

        public static SupplyCrate[] supplyCrates = new SupplyCrate[0];

        public static IEnumerable<SupplyCrate> GetSupplyCrates() {
            return supplyCrates.Select(d => d.Duplicate());
        }

        public static SupplyCrate GetSupplyCrateForClass(string className) {
            return supplyCrates.FirstOrDefault(e => e.ClassName.Equals(className));
        }

        public static bool HasSupplyCrateForClass(string className) {
            return !string.IsNullOrWhiteSpace(className) && supplyCrates.Any(e => e.ClassName.Equals(className));
        }

        public static string FriendlySupplyCrateNameForClass(string className, bool returnEmptyIfNotFound = false) {
            return string.IsNullOrWhiteSpace(className) ? string.Empty : gameData?.SupplyCrates?.FirstOrDefault(i => i.ClassName.Equals(className))?.Description ?? (returnEmptyIfNotFound ? string.Empty : className);
        }

        public static string FriendlySupplyCrateModNameForClass(string className) {
            return string.IsNullOrWhiteSpace(className) ? string.Empty : FriendlyNameForClass($"Mod_{gameData?.SupplyCrates?.FirstOrDefault(i => i.ClassName.Equals(className))?.Mod}", true) ?? string.Empty;
        }

        #endregion

        #region Game Maps

        /*public static ComboBoxItem[] gameMaps = new[] {
                                                           new ComboBoxItem { ValueMember = "", DisplayMember = "" },
                                                       };

        public static IEnumerable<ComboBoxItem> GetGameMaps() => gameMaps.Select(d => d.Duplicate());*/

        public static string FriendlyMapNameForClass(string className, bool returnEmptyIfNotFound = false) {
            return string.IsNullOrWhiteSpace(className)
                       ? string.Empty
                       : gameData?.GameMaps?.FirstOrDefault(i => i.ClassName.Equals(className) && !i.IsSotF)?.Description ?? (returnEmptyIfNotFound ? string.Empty : className);
        }

        /*public static ComboBoxItem[] gameMapsSotF = new[] {
                                                       new ComboBoxItem { ValueMember = "", DisplayMember = "" },
                                                   };

public static IEnumerable<ComboBoxItem> GetGameMapsSotF() => gameMapsSotF.Select(d => d.Duplicate());*/

        public static string FriendlyMapSotFNameForClass(string className, bool returnEmptyIfNotFound = false) {
            return string.IsNullOrWhiteSpace(className)
                       ? string.Empty
                       : gameData?.GameMaps?.FirstOrDefault(i => i.ClassName.Equals(className) && i.IsSotF)?.Description ?? (returnEmptyIfNotFound ? string.Empty : className);
        }

        #endregion

        #region Total Conversions

        // public static ComboBoxItem[] totalConversions = new[] {
        //                                                            new ComboBoxItem { ValueMember = "", DisplayMember = "" },
        //                                                        };

        // public static IEnumerable<ComboBoxItem> GetTotalConversions() => totalConversions.Select(d => d.Duplicate());

        public static string FriendlyTotalConversionNameForClass(string className, bool returnEmptyIfNotFound = false) {
            return string.IsNullOrWhiteSpace(className)
                       ? string.Empty
                       : gameData?.TotalConversions?.FirstOrDefault(i => i.ClassName.Equals(className) && !i.IsSotF)?.Description ??
                         (returnEmptyIfNotFound ? string.Empty : className);
        }

        //public static ComboBoxItem[] totalConversionsSotF = new[] {
        //                                                               new ComboBoxItem { ValueMember = "", DisplayMember = "" },
        //                                                           };

        //public static IEnumerable<ComboBoxItem> GetTotalConversionsSotF() => totalConversionsSotF.Select(d => d.Duplicate());

        public static string FriendlyTotalConversionSotFNameForClass(string className, bool returnEmptyIfNotFound = false) {
            return string.IsNullOrWhiteSpace(className)
                       ? string.Empty
                       : gameData?.TotalConversions?.FirstOrDefault(i => i.ClassName.Equals(className) && i.IsSotF)?.Description ??
                         (returnEmptyIfNotFound ? string.Empty : className);
        }

        #endregion

        #region Stats Multipliers

        public static IEnumerable<float> GetPerLevelStatsMultipliers_DinoWild() {
            return new[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f };
        }

        public static IEnumerable<float> GetPerLevelStatsMultipliers_DinoTamed() {
            return new[] { 0.2f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.17f, 1.0f, 1.0f, 1.0f };
        }

        public static IEnumerable<float> GetPerLevelStatsMultipliers_DinoTamedAdd() {
            return new[] { 0.14f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.14f, 1.0f, 1.0f, 1.0f };
        }

        public static IEnumerable<float> GetPerLevelStatsMultipliers_DinoTamedAffinity() {
            return new[] { 0.44f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.44f, 1.0f, 1.0f, 1.0f };
        }

        public static IEnumerable<float> GetBaseStatMultipliers_Player() {
            return new[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f };
        }

        public static IEnumerable<float> GetPerLevelStatsMultipliers_Player() {
            return new[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f };
        }

        public static IEnumerable<int> GetPerLevelMutagenLevelBoost_DinoWild() {
            return new[] { 5, 5, 0, 0, 0, 0, 0, 5, 5, 0, 0, 0 };
        }

        public static IEnumerable<int> GetPerLevelMutagenLevelBoost_DinoTamed() {
            return new[] { 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0 };
        }

        public static bool[] GetStatMultiplierInclusions_DinoWildPerLevel() {
            return new[] { true, true, false, true, true, false, true, true, true, true, false, true };
        }

        public static bool[] GetStatMultiplierInclusions_DinoTamedPerLevel() {
            return new[] { true, true, false, true, true, false, true, true, true, true, false, true };
        }

        public static bool[] GetStatMultiplierInclusions_DinoTamedAdd() {
            return new[] { true, true, true, true, true, true, true, true, true, true, true, true };
        }

        public static bool[] GetStatMultiplierInclusions_DinoTamedAffinity() {
            return new[] { true, true, true, true, true, true, true, true, true, true, true, true };
        }

        public static bool[] GetStatMultiplierInclusions_PlayerBase() {
            return new[] { true, true, true, true, true, true, true, true, true, true, true, true };
        }

        public static bool[] GetStatMultiplierInclusions_PlayerPerLevel() {
            return new[] { true, true, false, true, true, true, true, true, true, true, true, true };
        }

        public static bool[] GetMutagenLevelBoostInclusions_DinoWild() {
            return new[] { true, true, true, true, true, true, true, true, true, true, true, true };
        }

        public static bool[] GetMutagenLevelBoostInclusions_DinoTamed() {
            return new[] { true, true, true, true, true, true, true, true, true, true, true, true };
        }

        #endregion

        #region Levels

        public static Level[] levelsDino = {
                                               new Level { XPRequired = 10 }
                                           };

        public static Level[] levelsPlayer = {
                                                 new Level { XPRequired = 5, EngramPoints = 8 }
                                             };

        public static IEnumerable<Level> LevelsDino => levelsDino.Select(l => l.Duplicate());

        public static IEnumerable<Level> LevelsPlayer => levelsPlayer.Select(l => l.Duplicate());

        public static int LevelsPlayerAdditional;

        #endregion

        #region Branches

        //public static ComboBoxItem[] branches = new[] {
        //                                                   new ComboBoxItem { ValueMember = "", DisplayMember = FriendlyNameForClass(Config.Default.DefaultServerBranchName) },
        //                                               };

        //public static IEnumerable<ComboBoxItem> GetBranches() => branches.Select(d => d.Duplicate());

        public static string FriendlyBranchName(string branchName, bool returnEmptyIfNotFound = false) {
            return string.IsNullOrWhiteSpace(branchName)
                       ? string.Empty
                       : gameData?.Branches?.FirstOrDefault(i => i.BranchName.Equals(branchName) && !i.IsSotF)?.Description ?? (returnEmptyIfNotFound ? string.Empty : branchName);
        }

        //public static ComboBoxItem[] branchesSotF = new[] {
        //                                                       new ComboBoxItem { ValueMember = "", DisplayMember = FriendlyNameForClass(Config.Default.DefaultServerBranchName) },
        //                                                   };

        //public static IEnumerable<ComboBoxItem> GetBranchesSotF() => branchesSotF.Select(d => d.Duplicate());

        public static string FriendlyBranchSotFName(string branchName, bool returnEmptyIfNotFound = false) {
            return string.IsNullOrWhiteSpace(branchName)
                       ? string.Empty
                       : gameData?.Branches?.FirstOrDefault(i => i.BranchName.Equals(branchName) && i.IsSotF)?.Description ?? (returnEmptyIfNotFound ? string.Empty : branchName);
        }

        #endregion

        #region Events

        /* public static ComboBoxItem[] events = new[] {
                                                          new ComboBoxItem { ValueMember = "", DisplayMember = string.Empty },
                                                      };

         public static IEnumerable<ComboBoxItem> GetEvents() => events.Select(d => d.Duplicate());*/

        public static string FriendlyEventName(string eventName, bool returnEmptyIfNotFound = false) {
            return string.IsNullOrWhiteSpace(eventName)
                       ? string.Empty
                       : gameData?.Events?.FirstOrDefault(i => i.EventName.Equals(eventName) && !i.IsSotF)?.Description ?? (returnEmptyIfNotFound ? string.Empty : eventName);
        }

        /*public static ComboBoxItem[] eventsSotF = new[] {
                                                             new ComboBoxItem { ValueMember = "", DisplayMember = string.Empty },
                                                         };

        public static IEnumerable<ComboBoxItem> GetEventsSotF() => eventsSotF.Select(d => d.Duplicate());*/

        public static string FriendlyEventSotFName(string eventName, bool returnEmptyIfNotFound = false) {
            return string.IsNullOrWhiteSpace(eventName)
                       ? string.Empty
                       : gameData?.Events?.FirstOrDefault(i => i.EventName.Equals(eventName) && i.IsSotF)?.Description ?? (returnEmptyIfNotFound ? string.Empty : eventName);
        }

        #endregion
    }
}