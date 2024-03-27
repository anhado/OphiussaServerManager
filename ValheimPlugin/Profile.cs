using System;
using System.Diagnostics;
using System.Reflection;
using System.Web.Security;
using OphiussaFramework.Interfaces;

namespace BasePlugin {
    public enum Preset {
        Normal,
        Casual,
        Easy,
        Hard,
        Hardcore,
        Immersive,
        Hammer
    }

    public enum Combat {
        Default,
        VeryEasy,
        Easy,
        Hard,
        VeryHard
    }

    public enum DeathPenalty {
        Default,
        Casual,
        VeryEasy,
        Easy,
        Hard,
        HardCore
    }

    public enum Resources {
        Default,
        MuchLess,
        Less,
        More,
        MuchMore,
        Most
    }

    public enum Portals {
        Default,
        Casual,
        Hard,
        VeryHard
    }

    public enum Raids {
        Default,
        None,
        MuchLess,
        Less,
        More,
        MuchMore
    }
    public class Profile : IProfile {
        public string       Key                   { get; set; } = Guid.NewGuid().ToString();
        public string       Name                  { get; set; } = "New Server";
        public string       Type                  { get; set; }
        public string       PluginVersion         { get; set; } =  FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
        public string       InstallationFolder    { get; set; } = "";
        public string       AdditionalSettings    { get; set; } = "";
        public string       AdditionalCommands    { get; set; } = "";
        public string       Branch                { get; set; }
        public int          SteamServerId         { get; set; } = 0;
        public int          SteamApplicationID    { get; set; } = 0;
        public int          CurseForgeId          { get; set; } = 0;
        public int          ServerPort            { get; set; } = 0;
        public int          PeerPort              { get; set; } = 0;
        public int          QueryPort             { get; set; } = 0;
        public int          RCONPort              { get; set; } = 0;
        public string       ServerVersion         { get; set; } = "";
        public string       ServerPassword        { get; set; } = "";
        public string       ServerBuildVersion    { get; set; } = "";
        public bool         StartOnBoot           { get; set; } = false;
        public bool         IncludeAutoBackup     { get; set; } = false;
        public bool         IncludeAutoUpdate     { get; set; } = false;
        public bool         RestartIfShutdown     { get; set; } = false;
        public string       RCONPassword          { get; set; } = Membership.GeneratePassword(10, 6);
        public bool         UseRCON               { get; set; } = false;
        public string       ExecutablePath        { get; set; } = "valheim_server.exe";
        public bool         Crossplay             { get; set; } = false;
        public bool         Public                { get; set; } = false;
        public string       InstanceId            { get; set; } = "";
        public string       WordName              { get; set; } = "NewWorld";
        public Preset       Preset                { get; set; } = Preset.Normal;
        public int          AutoSavePeriod        { get; set; } = 30;
        public int          TotalBackups          { get; set; } = 3;
        public int          BackupShort           { get; set; } = 120;
        public int          BackupLong            { get; set; } = 720;
        public Combat       Combat                { get; set; } = Combat.Default;
        public DeathPenalty DeathPenalty          { get; set; } = DeathPenalty.Default;
        public Resources    Resources             { get; set; } = Resources.Default;
        public Portals      Portals               { get; set; } = Portals.Default;
        public Raids        Raids                 { get; set; } = Raids.Default;
        public string       SaveLocation          { get; set; } = "";
        public string       LogFileLocation       { get; set; } = "";
        public bool         NoBuildcost           { get; set; } = false;
        public bool         PlayerEvents          { get; set; } = false;
        public bool         PassiveMobs           { get; set; } = false;
        public bool         AllPiecesUnlocked     { get; set; } = false;
        public bool         AllRecipesUnlocked    { get; set; } = false;
        public bool         DeathDeleteItems      { get; set; } = false;
        public bool         DeathDeleteUnequipped { get; set; } = false;
        public bool         DeathKeepEquip        { get; set; } = false;
        public bool         DeathSkillsReset      { get; set; } = false;
        public bool         DungeonBuild          { get; set; } = false;
        public bool         NoCraftCost           { get; set; } = false;
        public bool         NoBossPortals         { get; set; } = false;
        public bool         NoMap                 { get; set; } = false;
        public bool         NoPortals             { get; set; } = false;
        public bool         NoWorkbench           { get; set; } = false;
        public bool         TeleportAll           { get; set; } = false;
        public float        DamageTaken           { get; set; } = 100f;
        public float        EnemyDamage           { get; set; } = 100f;
        public float        EnemyLevelUpRate      { get; set; } = 100f;
        public float        EnemySpeedSize        { get; set; } = 100f;
        public float        EventRate             { get; set; } = 100f;
        public float        MoveStaminaRate       { get; set; } = 100f;
        public float        PlayerDamage          { get; set; } = 100f;
        public float        ResourceRate          { get; set; } = 100f;
        public float        SkillGainRate         { get; set; } = 100f;
        public float        SkillReductionRate    { get; set; } = 100f;
        public float        StaminaRate           { get; set; } = 100f;
        public float        StaminaRegenRate      { get; set; } = 100f;

    }
}