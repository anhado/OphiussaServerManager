using System;
using System.Windows;
using OphiussaServerManager.Common.Helpers;

namespace OphiussaServerManager.Common.Models {
    public class DinoSettings {
        public string ClassName { get; set; } = "";

        public string Mod { get; set; } = "";

        public bool KnownDino { get; set; } = false;

        public bool CanTame { get; set; } = true;

        public bool CanBreeding { get; set; } = true;

        public bool CanSpawn { get; set; } = true;

        public string ReplacementClass = "";

        public float SpawnWeightMultiplier { get; set; } = DinoSpawn.DEFAULT_SPAWN_WEIGHT_MULTIPLIER;

        public bool OverrideSpawnLimitPercentage { get; set; } = DinoSpawn.DEFAULT_OVERRIDE_SPAWN_LIMIT_PERCENTAGE;

        public float SpawnLimitPercentage { get; set; } = DinoSpawn.DEFAULT_SPAWN_LIMIT_PERCENTAGE;

        public float TamedDamageMultiplier { get; set; } = ClassMultiplier.DEFAULT_MULTIPLIER;

        public float TamedResistanceMultiplier { get; set; } = ClassMultiplier.DEFAULT_MULTIPLIER;

        public float WildDamageMultiplier { get; set; } = ClassMultiplier.DEFAULT_MULTIPLIER;

        public float WildResistanceMultiplier { get; set; } = ClassMultiplier.DEFAULT_MULTIPLIER;

        public string           DisplayName            => GameData.FriendlyCreatureNameForClass(ClassName);
        public string           DisplayMod             => GameData.FriendlyNameForClass($"Mod_{Mod}", true) ?? Mod;
        public string           NameTag                { get; internal set; }
        public bool             HasNameTag             { get; internal set; }
        public bool             HasClassName           { get; internal set; }
        public bool             IsSpawnable            { get; internal set; }
        public DinoTamable      IsTameable             { get; internal set; }
        public DinoBreedingable IsBreedingable         { get; internal set; }
        public string           DisplayReplacementName => GameData.FriendlyCreatureNameForClass(ReplacementClass);

        public float OriginalSpawnWeightMultiplier        { get; internal set; }
        public bool  OriginalOverrideSpawnLimitPercentage { get; internal set; }
        public float OriginalSpawnLimitPercentage         { get; internal set; }

        #region Sort Properties

        public string NameSort                         => $"{DisplayName}|{Mod}";
        public string ModSort                          => $"{Mod}|{DisplayName}";
        public string CanSpawnSort                     => $"{IsSpawnable}|{CanSpawn}|{DisplayName}|{Mod}";
        public string CanTameSort                      => $"{IsTameable     != DinoTamable.False}|{CanTame}|{DisplayName}|{Mod}";
        public string CanBreedingSort                  => $"{IsBreedingable != DinoBreedingable.False}|{CanBreeding}|{DisplayName}|{Mod}";
        public string ReplacementNameSort              => $"{DisplayReplacementName}|{Mod}";
        public string SpawnWeightMultiplierSort        => $"{SpawnWeightMultiplier:0000000000.0000000000}|{DisplayName}|{Mod}";
        public string OverrideSpawnLimitPercentageSort => $"{OverrideSpawnLimitPercentage}|{DisplayName}|{Mod}";
        public string SpawnLimitPercentageSort         => $"{SpawnLimitPercentage:0000000000.0000000000}|{DisplayName}|{Mod}";
        public string TamedDamageMultiplierSort        => $"{TamedDamageMultiplier:0000000000.0000000000}|{DisplayName}|{Mod}";
        public string TamedResistanceMultiplierSort    => $"{TamedResistanceMultiplier:0000000000.0000000000}|{DisplayName}|{Mod}";
        public string WildDamageMultiplierSort         => $"{WildDamageMultiplier:0000000000.0000000000}|{DisplayName}|{Mod}";
        public string WildResistanceMultiplierSort     => $"{WildResistanceMultiplier:0000000000.0000000000}|{DisplayName}|{Mod}";

        #endregion
    }
}