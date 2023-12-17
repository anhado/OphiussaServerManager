using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common.Models.SupportedServers
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
        public string SteamServerID { get; set; }
        public string SavedRelativePath { get; set; }
        public string SaveGamesRelativePath { get; set; }
        public string SavedFilesRelativePath { get; set; }
        public string ExecutablePath { get; set; }
        public string ExecutablePathAPI { get; set; }
        public string ProcessName { get; set; }
        public int ModAppID { get; set; }
        public int SteamClientID { get; set; }
        public ModSource ModsSource { get; set; }
        public string ManifestFileName { get; set; }
    }

    public enum ModSource
    {
        SteamWorkshop,
        CurseForge
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
                        SteamServerID="376030",
                        SaveGamesRelativePath="Saved\\SavedArks\\",
                        SavedFilesRelativePath="Saved\\SavedArks\\",
                        SavedRelativePath="Saved\\SavedArks\\",
                        ExecutablePath="ShooterGame\\Binaries\\Win64\\ShooterGameServer.exe",
                        ExecutablePathAPI="",
                        ProcessName="ShooterGameServer",
                        ModAppID = 346110,
                        SteamClientID =346110,
                        ModsSource = ModSource.SteamWorkshop,
                        ManifestFileName="appmanifest_376030.acf"
                    },
                    new SupportedServersType() {
                        KeyName="ASA",
                        ServerTypeDescription="Ark Survival Ascended",
                        ServerType=EnumServerType.ArkSurviveAscended,
                        SteamServerID="2430930",
                        SaveGamesRelativePath="Saved\\SavedArks\\",
                        SavedFilesRelativePath="Saved\\SavedArks\\",
                        SavedRelativePath="Saved\\SavedArks\\",
                        ExecutablePath="ShooterGame\\Binaries\\Win64\\ArkAscendedServer.exe",
                        ExecutablePathAPI="ShooterGame\\Binaries\\Win64\\AsaApiLoader.exe",
                        ProcessName="ArkAscendedServer",
                        ModAppID = 83374,
                        SteamClientID =2399830,
                        ModsSource = ModSource.CurseForge,
                        ManifestFileName="appmanifest_2430930.acf"
                    }
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
