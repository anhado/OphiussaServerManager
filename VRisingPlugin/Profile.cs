using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web.Security;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;

namespace VRisingPlugin {
    public class Profile : IProfile {
        public string                  Key                { get; set; } = Guid.NewGuid().ToString();
        public string                  Name               { get; set; } = "New Server";
        public string                  Type               { get; set; }
        public string                  PluginVersion      { get; set; } = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
        public string                  InstallationFolder { get; set; } = "";
        public string                  AdditionalSettings { get; set; } = "";
        public string                  AdditionalCommands { get; set; } = "";
        public string                  Branch             { get; set; }
        public string                  BetaName           { get; set; }
        public string                  BetaPassword       { get; set; }
        public int                     SteamServerId      { get; set; } = 1829350;
        public int                     SteamApplicationID { get; set; } = 1604030;
        public int                     CurseForgeId       { get; set; } = 0;
        public int                     ServerPort         { get; set; } = 9876;
        public int                     PeerPort           { get; set; } = 0;
        public int                     QueryPort          { get; set; } = 9877;
        public int RCONPort {
            get => Rcon.Port;
            set => Rcon.Port = value;
        }
        public string                  ServerVersion      { get; set; } = "";
        public string                  ServerBuildVersion { get; set; } = "";
        public bool                    AutoStartServer    { get; set; } = false;
        public bool                    StartOnBoot        { get; set; } = false;
        public bool                    IncludeAutoBackup  { get; set; } = false;
        public bool                    IncludeAutoUpdate  { get; set; } = false;
        public bool                    RestartIfShutdown  { get; set; } = false;
        public string RCONPassword {
            get => Rcon.Password;
            set => Rcon.Password = value;
        }
        public bool UseRCON {
            get => Rcon.Enabled;
            set => Rcon.Enabled = value;
        }
        public string                  ExecutablePath     { get; set; } = "VRisingServer.exe";
        public string                  ServerPassword     { get; set; }
        public ProcessPriority         CpuPriority        { get; set; } = ProcessPriority.Normal;
        public string                  CpuAffinity        { get; set; } = "All";
        public List<ProcessorAffinity> CpuAffinityList    { get; set; } = new List<ProcessorAffinity>();
        public List<AutoManagement>    AutoManagement     { get; set; } = new List<AutoManagement>();

        //ServerHostSettings
        public string ServerName           { get; set; } = "";
        public string Description          { get; set; } = "";
        public int    MaxConnectedUsers    { get; set; } = 40;
        public int    MaxConnectedAdmins   { get; set; } = 4;
        public int    ServerFps            { get; set; } = 30;
        public string SaveName             { get; set; } = "world1";
        public bool   Secure               { get; set; } = true;
        public bool   ListOnSteam          { get; set; } = false;
        public bool   ListOnEOS            { get; set; } = false;
        public int    AutoSaveCount        { get; set; } = 20;
        public int    AutoSaveInterval     { get; set; } = 120;
        public bool   CompressSaveFiles    { get; set; } = true;
        public string GameSettingsPreset   { get; set; } = "";
        public bool   AdminOnlyDebugEvents { get; set; } = true;
        public bool   DisableDebugEvents   { get; set; } = false;
        public bool   APIEnabled           {
            get => API.Enabled;
            set => API.Enabled = value;
        }

        //ServerGameSettings
        public GameModeType                  GameModeType                          { get; set; } = GameModeType.PvP;
        public CastleDamageMode              CastleDamageMode                      { get; set; } = CastleDamageMode.Always;
        public SiegeWeaponHealth             SiegeWeaponHealth                     { get; set; } = SiegeWeaponHealth.Normal;
        public PlayerDamageMode              PlayerDamageMode                      { get; set; } = PlayerDamageMode.Always;
        public CastleHeartDamageMode         CastleHeartDamageMode                 { get; set; } = CastleHeartDamageMode.CanBeDestroyedOnlyWhenDecaying;
        public PvPProtectionMode             PvPProtectionMode                     { get; set; } = PvPProtectionMode.Medium;
        public DeathContainerPermission      DeathContainerPermission              { get; set; } = DeathContainerPermission.ClanMembers;
        public RelicSpawnType                RelicSpawnType                        { get; set; } = RelicSpawnType.Plentiful;
        public bool                          CanLootEnemyContainers                { get; set; } = false;
        public bool                          BloodBoundEquipment                   { get; set; } = true;
        public bool                          TeleportBoundItems                    { get; set; } = false;
        public bool                          AllowGlobalChat                       { get; set; } = true;
        public bool                          AllWaypointsUnlocked                  { get; set; } = false;
        public bool                          FreeCastleClaim                       { get; set; } = false;
        public bool                          FreeCastleDestroy                     { get; set; } = false;
        public bool                          InactivityKillEnabled                 { get; set; } = true;
        public int                           InactivityKillTimeMin                 { get; set; } = 3600;
        public int                           InactivityKillTimeMax                 { get; set; } = 604800;
        public int                           InactivityKillSafeTimeAddition        { get; set; } = 172800;
        public int                           InactivityKillTimerMaxItemLevel       { get; set; } = 84;
        public bool                          DisableDisconnectedDeadEnabled        { get; set; } = true;
        public int                           DisableDisconnectedDeadTimer          { get; set; } = 60;
        public float                         InventoryStacksModifier               { get; set; } = 1.0f;
        public float                         DropTableModifier_General             { get; set; } = 1.0f;
        public float                         DropTableModifier_Missions            { get; set; } = 1.0f;
        public float                         MaterialYieldModifier_Global          { get; set; } = 1.0f;
        public float                         BloodEssenceYieldModifier             { get; set; } = 1.0f;
        public float                         JournalVBloodSourceUnitMaxDistance    { get; set; } = 25.0f;
        public float                         PvPVampireRespawnModifier             { get; set; } = 1.0f;
        public int                           CastleMinimumDistanceInFloors         { get; set; } = 2;
        public int                           ClanSize                              { get; set; } = 4;
        public float                         BloodDrainModifier                    { get; set; } = 1.0f;
        public float                         DurabilityDrainModifier               { get; set; } = 1.0f;
        public float                         GarlicAreaStrengthModifier            { get; set; } = 1.0f;
        public float                         HolyAreaStrengthModifier              { get; set; } = 1.0f;
        public float                         SilverStrengthModifier                { get; set; } = 1.0f;
        public float                         SunDamageModifier                     { get; set; } = 1.0f;
        public float                         CastleDecayRateModifier               { get; set; } = 0.5f;
        public float                         CastleBloodEssenceDrainModifier       { get; set; } = 0.5f;
        public float                         CastleSiegeTimer                      { get; set; } = 420.0f;
        public float                         CastleUnderAttackTimer                { get; set; } = 60.0f;
        public bool                          AnnounceSiegeWeaponSpawn              { get; set; } = true;
        public bool                          ShowSiegeWeaponMapIcon                { get; set; } = false;
        public float                         BuildCostModifier                     { get; set; } = 1.0f;
        public float                         RecipeCostModifier                    { get; set; } = 1.0f;
        public float                         CraftRateModifier                     { get; set; } = 1.0f;
        public float                         ResearchCostModifier                  { get; set; } = 1.0f;
        public float                         RefinementCostModifier                { get; set; } = 1.0f;
        public float                         RefinementRateModifier                { get; set; } = 1.0f;
        public float                         ResearchTimeModifier                  { get; set; } = 1.0f;
        public float                         DismantleResourceModifier             { get; set; } = 1.0f;
        public float                         ServantConvertRateModifier            { get; set; } = 1.0f;
        public float                         RepairCostModifier                    { get; set; } = 1.0f;
        public float                         Death_DurabilityFactorLoss            { get; set; } = 0.125f;
        public float                         Death_DurabilityLossFactorAsResources { get; set; } = 1.0f;
        public int                           StarterEquipmentId                    { get; set; } = 0;
        public int                           StarterResourcesId                    { get; set; } = 0;
        public List<VBloodUnitSettings>      VBloodUnitSettings                    { get; set; } = new List<VBloodUnitSettings>();
        public List<Int64>                   UnlockedAchievements                  { get; set; } = new List<Int64>();
        public List<Int64>                   UnlockedResearchs                     { get; set; } = new List<Int64>();
        public GameTimeModifiers             GameTimeModifiers                     { get; set; } = new GameTimeModifiers();
        public VampireStatModifiers          VampireStatModifiers                  { get; set; } = new VampireStatModifiers();
        public UnitStatModifiers             UnitStatModifiers_Global              { get; set; } = new UnitStatModifiers();
        public UnitStatModifiers             UnitStatModifiers_VBlood              { get; set; } = new UnitStatModifiers();
        public EquipmentStatModifiers_Global EquipmentStatModifiers_Global         { get; set; } = new EquipmentStatModifiers_Global();
        public CastleStatModifiers_Global    CastleStatModifiers_Global            { get; set; } = new CastleStatModifiers_Global();
        public PlayerInteractionSettings     PlayerInteractionSettings             { get; set; } = new PlayerInteractionSettings();

        public API  API  { get; set; } = new API();
        public Rcon Rcon { get; set; } = new Rcon();
    }

    public class Rcon {
        public bool   Enabled  { get; set; } = false;
        public int    Port     { get; set; } = 25575;
        public string Password { get; set; } = Membership.GeneratePassword(10, 2);
    }

    public class API {
        public bool Enabled { get; set; } = false;
    }
    public class VBloodUnitSettings {
        public Int64 UnitId { get; set; }
        public int UnitLevel { get; set; }
        public bool DefaultUnlocked { get; set; }
    }

    public class GameTimeModifiers {
        public float DayDurationInSeconds { get; set; } = 1080.0f;
        public int DayStartHour { get; set; } = 9;
        public int DayStartMinute { get; set; } = 0;
        public int DayEndHour { get; set; } = 17;
        public int DayEndMinute { get; set; } = 0;
        public int BloodMoonFrequency_Min { get; set; } = 10;
        public int BloodMoonFrequency_Max { get; set; } = 18;
        public float BloodMoonBuff { get; set; } = 0.2f;
    }

    public class VampireStatModifiers { 
        public float MaxHealthModifier      { get; set; } = 1.0f; 
        public float MaxEnergyModifier      { get; set; } = 1.0f;
        public float PhysicalPowerModifier  { get; set; } = 1.0f;
        public float SpellPowerModifier     { get; set; } = 1.0f;
        public float ResourcePowerModifier  { get; set; } = 1.0f;
        public float SiegePowerModifier     { get; set; } = 1.0f;
        public float DamageReceivedModifier { get; set; } = 1.0f;
        public float ReviveCancelDelay      { get; set; } = 5.0f;
    }

    public class UnitStatModifiers {
        public float MaxHealthModifier { get; set; } = 1.0f;
        public float PowerModifier { get; set; } = 1.0f;
    }

    public class EquipmentStatModifiers_Global {
        public float MaxEnergyModifier { get; set; } = 1.0f;
        public float MaxHealthModifier { get; set; } = 1.0f;
        public float ResourceYieldModifier { get; set; } = 1.0f;
        public float PhysicalPowerModifier { get; set; } = 1.0f;
        public float SpellPowerModifier { get; set; } = 1.0f;
        public float SiegePowerModifier { get; set; } = 1.0f;
        public float MovementSpeedModifier { get; set; } = 1.0f;   
    }

    public class CastleStatModifiers_Global {
        public float          TickPeriod       { get; set; } = 5.0f;
        public float          DamageResistance { get; set; } = 0.0f;
        public int            SafetyBoxLimit   { get; set; } = 1;
        public int            TombLimit        { get; set; } = 12;
        public int            VerminNestLimit  { get; set; } = 4;
        public int            PrisonCellLimit  { get; set; } = 16;
        public PylonPenalties PylonPenalties   { get; set; } = new PylonPenalties();
        public FloorPenalties FloorPenalties   { get; set; } = new FloorPenalties();
        public HeartLimits    HeartLimits      { get; set; } = new HeartLimits();
        public int            CastleLimit      { get; set; } = 2;
    }

    public class Range {
        public float Percentage { get; set; } = 0.0f;
        public int Lower { get; set; } = 0;
        public int Higher { get; set; } = 0;
    }

    public class PylonPenalties {
        public Range Range1 { get; set; } = new Range() { Percentage = 0, Lower = 0, Higher = 2 };
        public Range Range2 { get; set; } = new Range() { Percentage = 0, Lower = 3, Higher = 3 };
        public Range Range3 { get; set; } = new Range() { Percentage = 0, Lower = 4, Higher = 4 };
        public Range Range4 { get; set; } = new Range() { Percentage = 0, Lower = 5, Higher = 5 };
        public Range Range5 { get; set; } = new Range() { Percentage = 0, Lower = 6, Higher = 254 };
    }

    public class FloorPenalties {
        public Range Range1 { get; set; } = new Range() { Percentage = 0, Lower = 0, Higher = 20 };
        public Range Range2 { get; set; } = new Range() { Percentage = 0, Lower = 21, Higher = 50 };
        public Range Range3 { get; set; } = new Range() { Percentage = 0, Lower = 51, Higher = 80 };
        public Range Range4 { get; set; } = new Range() { Percentage = 0, Lower = 81, Higher = 160 };
        public Range Range5 { get; set; } = new Range() { Percentage = 0, Lower = 161, Higher = 254 };
    }

    public class Level {
        public int level { get; set; } = 0;
        public int FloorLimit { get; set; } = 0;
        public int ServantLimit { get; set; } = 0;
    }

    public class HeartLimits {
        public Level Level1 { get; set; } = new Level() { level = 1, FloorLimit = 40, ServantLimit = 4 };
        public Level Level2 { get; set; } = new Level() { level = 2, FloorLimit = 100, ServantLimit = 5 };
        public Level Level3 { get; set; } = new Level() { level = 3, FloorLimit = 180, ServantLimit = 6 };
        public Level Level4 { get; set; } = new Level() { level = 4, FloorLimit = 260, ServantLimit = 7 };
        public Level Level5 { get; set; } = new Level() { level = 6, FloorLimit = 420, ServantLimit = 8 };
    }

    public class PlayerInteractionSettings {
        public string TimeZone { get; set; } = "Local";
        public Vstime VSPlayerWeekdayTime { get; set; } = new Vstime() { StartHour = 20, StartMinute = 0, EndHour = 22, EndMinute = 0 };
        public Vstime VSPlayerWeekendTime { get; set; } = new Vstime() { StartHour = 20, StartMinute = 0, EndHour = 22, EndMinute = 0 };
        public Vstime VSCastleWeekdayTime { get; set; } = new Vstime() { StartHour = 20, StartMinute = 0, EndHour = 22, EndMinute = 0 };
        public Vstime VSCastleWeekendTime { get; set; } = new Vstime() { StartHour = 20, StartMinute = 0, EndHour = 22, EndMinute = 0 };
    }

    public class Vstime {
        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int EndHour { get; set; }
        public int EndMinute { get; set; }
    }
}

 