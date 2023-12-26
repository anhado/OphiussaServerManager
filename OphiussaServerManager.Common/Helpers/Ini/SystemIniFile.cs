using System;
using System.Collections.Generic;

namespace OphiussaServerManager.Common.Ini {
    public enum IniFiles {
        Engine,
        Game,
        GameUserSettings
    }

    public enum IniSections {
        GUS_ServerSettings,
        GusShooterGameUserSettings,
        GusScalabilityGroups,
        GUS_SessionSettings,
        GUS_GameSession,
        GUS_MultiHome,
        GUS_MessageOfTheDay,
        GUS_Ragnarok,
        Game_ShooterGameMode,
        Custom
    }

    public class SystemIniFile : BaseSystemIniFile {
        public static readonly Dictionary<Enum, string> IniFileNames = new Dictionary<Enum, string> {
                                                                                                        {
                                                                                                            IniFiles.GameUserSettings,
                                                                                                            "ShooterGame\\Saved\\Config\\WindowsServer\\GameUserSettings.ini"
                                                                                                        }, {
                                                                                                            IniFiles.Game,
                                                                                                            "ShooterGame\\Saved\\Config\\WindowsServer\\Game.ini"
                                                                                                        }, {
                                                                                                            IniFiles.Engine,
                                                                                                            "ShooterGame\\Saved\\Config\\WindowsServer\\Engine.ini"
                                                                                                        }
                                                                                                    };

        public static readonly Dictionary<Enum, string> IniSectionNames = new Dictionary<Enum, string> {
                                                                                                           {
                                                                                                               IniSections.GUS_ServerSettings,
                                                                                                               "ServerSettings"
                                                                                                           }, {
                                                                                                               IniSections.GusShooterGameUserSettings,
                                                                                                               "/Script/ShooterGame.ShooterGameUserSettings"
                                                                                                           }, {
                                                                                                               IniSections.GusScalabilityGroups,
                                                                                                               "ScalabilityGroups"
                                                                                                           }, {
                                                                                                               IniSections.GUS_SessionSettings,
                                                                                                               "SessionSettings"
                                                                                                           }, {
                                                                                                               IniSections.GUS_GameSession,
                                                                                                               "/Script/Engine.GameSession"
                                                                                                           }, {
                                                                                                               IniSections.GUS_MultiHome,
                                                                                                               "MultiHome"
                                                                                                           }, {
                                                                                                               IniSections.GUS_MessageOfTheDay,
                                                                                                               "MessageOfTheDay"
                                                                                                           }, {
                                                                                                               IniSections.GUS_Ragnarok,
                                                                                                               "Ragnarok"
                                                                                                           }, {
                                                                                                               IniSections.Game_ShooterGameMode,
                                                                                                               "/script/shootergame.shootergamemode"
                                                                                                           }
                                                                                                       };

        public SystemIniFile(string iniPath)
            : base(iniPath) {
        }

        public override Dictionary<Enum, string> FileNames => IniFileNames;

        public override Dictionary<Enum, string> SectionNames => IniSectionNames;
    }
}