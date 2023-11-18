using OphiussaServerManager.Ini;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Ini
{
    public enum IniFiles
    {
        Engine,
        Game,
        GameUserSettings,
    }

    public enum IniSections
    {
        GUS_ServerSettings,
        GUS_ShooterGameUserSettings,
        GUS_ScalabilityGroups,
        GUS_SessionSettings,
        GUS_GameSession,
        GUS_MultiHome,
        GUS_MessageOfTheDay,
        GUS_Ragnarok,
        Game_ShooterGameMode,
        Custom,
    }

    public class SystemIniFile : BaseSystemIniFile
    {
        public static readonly Dictionary<Enum, string> IniFileNames = new Dictionary<Enum, string>()
        {
            {
        (Enum) IniFiles.GameUserSettings,
        "ShooterGame\\Saved\\Config\\WindowsServer\\GameUserSettings.ini"
      },
      {
        (Enum) IniFiles.Game,
        "ShooterGame\\Saved\\Config\\WindowsServer\\Game.ini"
      },
      {
        (Enum) IniFiles.Engine,
        "ShooterGame\\Saved\\Config\\WindowsServer\\Engine.ini"
      }
    };
        public static readonly Dictionary<Enum, string> IniSectionNames = new Dictionary<Enum, string>()
        {
            {
        (Enum) IniSections.GUS_ServerSettings,
                "ServerSettings"
            },
            {
        (Enum) IniSections.GUS_ShooterGameUserSettings,
        "/Script/ShooterGame.ShooterGameUserSettings"
      },
      {
        (Enum) IniSections.GUS_ScalabilityGroups,
        "ScalabilityGroups"
      },
      {
        (Enum) IniSections.GUS_SessionSettings,
        "SessionSettings"
      },
      {
        (Enum) IniSections.GUS_GameSession,
        "/Script/Engine.GameSession"
      },
      {
        (Enum) IniSections.GUS_MultiHome,
        "MultiHome"
      },
      {
        (Enum) IniSections.GUS_MessageOfTheDay,
        "MessageOfTheDay"
      },
      {
                (Enum) IniSections.GUS_Ragnarok,
        "Ragnarok"
      },
      {
        (Enum) IniSections.Game_ShooterGameMode,
        "/script/shootergame.shootergamemode"
      }
    };

        public override Dictionary<Enum, string> FileNames => SystemIniFile.IniFileNames;

        public override Dictionary<Enum, string> SectionNames => SystemIniFile.IniSectionNames;

        public SystemIniFile(string iniPath)
          : base(iniPath)
        {
        }
    }
}
