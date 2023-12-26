using System;
using OphiussaServerManager.Common.Ini;

namespace OphiussaServerManager.Common.Helpers {
    
    public enum ServerProfileCategory
    {
        Unknown,
        Administration,
        AutomaticManagement,
        DiscordBot,
        ServerDetails,
        Rules,
        ChatAndNotifications,
        HudAndVisuals,
        Players,
        Dinos,
        Environment,
        Structures,
        Engrams,
        ServerFiles,
        CustomGameUserSettings,
        CustomGameSettings,
        CustomEngineSettings,
        CustomLevels,
        MapSpawnerOverrides,
        CraftingOverrides,
        SupplyCrateOverrides,
        ExcludeItemIndicesOverrides,
        StackSizeOverrides,
        PreventTransferOverrides,
        PGM,
        SOTF,
    }
      
    public class IniFileEntryAttribute : BaseIniFileEntryAttribute
    {
        public IniFileEntryAttribute(
            IniFiles              file,
            IniSections           section,
            ServerProfileCategory category,
            string                key = "")
            : base((Enum) file, (Enum) section, (Enum) category, key)
        {
        }
    } 
}