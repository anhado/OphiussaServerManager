using System;
using System.Collections.Generic;

namespace OphiussaServerManager.Common.Ini {
    public enum IniFiles {
        Engine,
        Game,
        GameUserSettings
    }

    public enum IniSections {
        GusServerSettings,
        GusShooterGameUserSettings,
        GusScalabilityGroups,
        GusSessionSettings,
        GusGameSession,
        GusMultiHome,
        GusMessageOfTheDay,
        GusRagnarok,
        GameShooterGameMode,
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
                                                                                                               IniSections.GusServerSettings,
                                                                                                               "ServerSettings"
                                                                                                           }, {
                                                                                                               IniSections.GusShooterGameUserSettings,
                                                                                                               "/Script/ShooterGame.ShooterGameUserSettings"
                                                                                                           }, {
                                                                                                               IniSections.GusScalabilityGroups,
                                                                                                               "ScalabilityGroups"
                                                                                                           }, {
                                                                                                               IniSections.GusSessionSettings,
                                                                                                               "SessionSettings"
                                                                                                           }, {
                                                                                                               IniSections.GusGameSession,
                                                                                                               "/Script/Engine.GameSession"
                                                                                                           }, {
                                                                                                               IniSections.GusMultiHome,
                                                                                                               "MultiHome"
                                                                                                           }, {
                                                                                                               IniSections.GusMessageOfTheDay,
                                                                                                               "MessageOfTheDay"
                                                                                                           }, {
                                                                                                               IniSections.GusRagnarok,
                                                                                                               "Ragnarok"
                                                                                                           }, {
                                                                                                               IniSections.GameShooterGameMode,
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