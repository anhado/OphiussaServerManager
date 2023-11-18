using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Models.SupportedServers
{
    public enum EnumServerType
    {
        ArkSurviveEvolved = 0,
        ArkSurviveAscended = 1
    }

    public class SupportedServersType
    {
        public string KeyName { get; set; }
        public string ServerTypeDescription { get; set; }
        public EnumServerType ServerType { get; set; }
        public string SteamAppID { get; set; }
        public string SavedRelativePath { get; set; }
        public string SaveGamesRelativePath { get; set; }
        public string SavedFilesRelativePath { get; set; }
        public string ExecutablePath { get; set; }
        public string ProcessName { get; set; }

    }


    public class MapList
    {
        public string Key { get; set; }
        public string Description { get; set; }
    }

    public static class SupportedServers
    {
        public static List<SupportedServersType> ServerTypeList
        {
            get
            {
                return new List<SupportedServersType>() {
                    new SupportedServersType() {
                        KeyName="ASE",
                        ServerTypeDescription="Ark Survival Evolved",
                        ServerType =EnumServerType.ArkSurviveEvolved,
                        SteamAppID="376030",
                        SaveGamesRelativePath="Saved\\SavedArks\\TheIsland_WP",
                        SavedFilesRelativePath="Saved\\SavedArks\\TheIsland_WP",
                        SavedRelativePath="Saved\\SavedArks\\TheIsland_WP",
                        ExecutablePath="ShooterGame\\Binaries\\Win64\\ShooterGameServer.exe",
                        ProcessName="ShooterGameServer"
                    },
                    new SupportedServersType() {
                        KeyName="ASA",
                        ServerTypeDescription="Ark Survival Ascended",
                        ServerType=EnumServerType.ArkSurviveAscended,
                        SteamAppID="2430930",
                        SaveGamesRelativePath="Saved\\SavedArks\\TheIsland_WP",
                        SavedFilesRelativePath="Saved\\SavedArks\\TheIsland_WP",
                        SavedRelativePath="Saved\\SavedArks\\TheIsland_WP",
                        ExecutablePath="ShooterGame\\Binaries\\Win64\\ArkAscendedServer.exe",
                        ProcessName="ArkAscendedServer" }
                };
            }
        }

        public static List<MapList> GetMapLists(EnumServerType ServerType)
        {
            switch (ServerType)
            {
                case EnumServerType.ArkSurviveEvolved:
                    return new List<MapList>()
                    {
                        new MapList(){Key="TheIsland" ,Description="The Island" },
                        new MapList(){Key="TheCenter" ,Description="The Center" },
                        new MapList(){Key="ScorchedEarth_P" ,Description="Scorched Earth" },
                        new MapList(){Key="Ragnarok" ,Description="Ragnarok" },
                        new MapList(){Key="Aberration_P" ,Description="Aberration" },
                        new MapList(){Key="Extinction" ,Description="Extinction" },
                        new MapList(){Key="Valguero_P" ,Description="Valguero" },
                        new MapList(){Key="Genesis" ,Description="Genesis: Part 1" },
                        new MapList(){Key="CrystalIsles" ,Description="Crystal Isles" },
                        new MapList(){Key="Gen2" ,Description="Genesis: Part 2" },
                        new MapList(){Key="LostIsland" ,Description="Lost Island" },
                        new MapList(){Key="Fjordur" ,Description="Fjordur" }//,
                        //new MapList(){Key="PGARK" ,Description="Procedurally Generated Maps" }
                    };
                case EnumServerType.ArkSurviveAscended:
                    return new List<MapList>()
                    {
                        new MapList(){Key="TheIsland_WP" ,Description="The Island" }
                    };
            }
            return new List<MapList>();
        }
    }
}
