using System.Collections.Generic;

namespace OphiussaServerManager.Common.Models.SupportedServers {
    public enum EnumServerType {
        ArkSurviveEvolved  = 0,
        ArkSurviveAscended = 1,
        Valheim            = 2
    }

    public class SupportedServersType {
        public string         KeyName                { get; set; }
        public string         ServerTypeDescription  { get; set; }
        public EnumServerType ServerType             { get; set; }
        public string         SteamServerId          { get; set; }
        public string         SavedRelativePath      { get; set; }
        public string         SaveGamesRelativePath  { get; set; }
        public string         SavedFilesRelativePath { get; set; }
        public string         ExecutablePath         { get; set; }
        public string         ExecutablePathApi      { get; set; }
        public string         ProcessName            { get; set; }
        public int            ModAppId               { get; set; }
        public int            SteamClientId          { get; set; }
        public ModSource      ModsSource             { get; set; }
        public string         ManifestFileName       { get; set; }
    }

    public enum ModSource {
        SteamWorkshop,
        CurseForge,
        NexusMods
    }

    public class MapList {
        public string Key         { get; set; }
        public string Description { get; set; }
    }

    public static class SupportedServers {
        public static List<SupportedServersType> ServerTypeList =>
            new List<SupportedServersType> {
                                               new SupportedServersType {
                                                                            KeyName                = "ASE",
                                                                            ServerTypeDescription  = "Ark Survival Evolved",
                                                                            ServerType             = EnumServerType.ArkSurviveEvolved,
                                                                            SteamServerId          = "376030",
                                                                            SaveGamesRelativePath  = "Saved\\SavedArks\\",
                                                                            SavedFilesRelativePath = "Saved\\SavedArks\\",
                                                                            SavedRelativePath      = "Saved\\SavedArks\\",
                                                                            ExecutablePath         = "ShooterGame\\Binaries\\Win64\\ShooterGameServer.exe",
                                                                            ExecutablePathApi      = "",
                                                                            ProcessName            = "ShooterGameServer",
                                                                            ModAppId               = 346110,
                                                                            SteamClientId          = 346110,
                                                                            ModsSource             = ModSource.SteamWorkshop,
                                                                            ManifestFileName       = "appmanifest_376030.acf"
                                                                        },
                                               new SupportedServersType {
                                                                            KeyName                = "ASA",
                                                                            ServerTypeDescription  = "Ark Survival Ascended",
                                                                            ServerType             = EnumServerType.ArkSurviveAscended,
                                                                            SteamServerId          = "2430930",
                                                                            SaveGamesRelativePath  = "Saved\\SavedArks\\",
                                                                            SavedFilesRelativePath = "Saved\\SavedArks\\",
                                                                            SavedRelativePath      = "Saved\\SavedArks\\",
                                                                            ExecutablePath         = "ShooterGame\\Binaries\\Win64\\ArkAscendedServer.exe",
                                                                            ExecutablePathApi      = "ShooterGame\\Binaries\\Win64\\AsaApiLoader.exe",
                                                                            ProcessName            = "ArkAscendedServer",
                                                                            ModAppId               = 83374,
                                                                            SteamClientId          = 2399830,
                                                                            ModsSource             = ModSource.CurseForge,
                                                                            ManifestFileName       = "appmanifest_2430930.acf"
                                                                        },
                                               new SupportedServersType {
                                                                            KeyName                = "VAL",
                                                                            ServerTypeDescription  = "Valheim",
                                                                            ServerType             = EnumServerType.Valheim,
                                                                            SteamServerId          = "896660",
                                                                            SaveGamesRelativePath  = "",
                                                                            SavedFilesRelativePath = "",
                                                                            SavedRelativePath      = "",
                                                                            ExecutablePath         = "valheim_server.exe",
                                                                            ExecutablePathApi      = "",
                                                                            ProcessName            = "valheim_server",
                                                                            ModAppId               = 3667,
                                                                            SteamClientId          = 892970,
                                                                            ModsSource             = ModSource.NexusMods,
                                                                            ManifestFileName       = "appmanifest_896660.acf"
                                                                        }
                                           };

        public static List<MapList> GetMapLists(EnumServerType serverType) {
            switch (serverType) {
                case EnumServerType.ArkSurviveEvolved:
                    return new List<MapList> {
                                                 new MapList { Key = "TheIsland", Description       = "The Island" },
                                                 new MapList { Key = "TheCenter", Description       = "The Center" },
                                                 new MapList { Key = "ScorchedEarth_P", Description = "Scorched Earth" },
                                                 new MapList { Key = "Ragnarok", Description        = "Ragnarok" },
                                                 new MapList { Key = "Aberration_P", Description    = "Aberration" },
                                                 new MapList { Key = "Extinction", Description      = "Extinction" },
                                                 new MapList { Key = "Valguero_P", Description      = "Valguero" },
                                                 new MapList { Key = "Genesis", Description         = "Genesis: Part 1" },
                                                 new MapList { Key = "CrystalIsles", Description    = "Crystal Isles" },
                                                 new MapList { Key = "Gen2", Description            = "Genesis: Part 2" },
                                                 new MapList { Key = "LostIsland", Description      = "Lost Island" },
                                                 new MapList { Key = "Fjordur", Description         = "Fjordur" } //,
                                                 //new MapList(){Key="PGARK" ,Description="Procedurally Generated Maps" }
                                             };
                case EnumServerType.ArkSurviveAscended:
                    return new List<MapList> {
                                                 new MapList { Key = "TheIsland_WP", Description = "The Island" }
                                             };
                case EnumServerType.Valheim:
                    //Nothing todo
                    break;
            }

            return new List<MapList>();
        }
    }
}