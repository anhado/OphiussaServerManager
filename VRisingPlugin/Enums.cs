using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VRisingPlugin {
    
    public enum GameDifficulty {
        Easy,
        Normal,
        Brutal
    }
    public enum GameModeType {
        PvP,
        PvE
    }
    public enum CastleDamageMode {
        Always,
        Never,
        TimeRestricted
    }
    public enum SiegeWeaponHealth {
        VeryLow,
        Low,
        Normal,
        High,
        VeryHigh
    }
    public enum PlayerDamageMode {
        Always,
        TimeRestricted
    }
    public enum CastleHeartDamageMode {
        CanBeDestroyedOnlyWhenDecaying,
        CanBeDestroyedByPlayers,
        CanBeSeizedOrDestroyedByPlayers
    }
    public enum PvPProtectionMode {
        VeryShort,
        Short,
        Medium,
        Long
    }
    public enum DeathContainerPermission {
        Anyone,
        ClanMembers
    }
    public enum RelicSpawnType {
        Unique,
        Plentiful
    }
}
