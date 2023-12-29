namespace OphiussaServerManager.Common.Models {
    public class DinoSettingsJSON {
        public string ClassName                    { get; set; }
        public string ReplacementClass             { get; set; }
        public bool   CanTame                      { get; set; }
        public bool   CanBreeding                  { get; set; }
        public bool   CanSpawn                     { get; set; }
        public bool   OverrideSpawnLimitPercentage { get; set; }
        public float  SpawnWeightMultiplier        { get; set; }
        public float  SpawnLimitPercentage         { get; set; }
        public float  TamedDamageMultiplier        { get; set; }
        public float  TamedResistanceMultiplier    { get; set; }
        public float  WildDamageMultiplier         { get; set; }
        public float  WildResistanceMultiplier     { get; set; }
    }
}