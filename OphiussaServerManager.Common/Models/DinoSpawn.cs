using System;
using System.Runtime.Serialization;
using System.Windows;
using System.Xml.Serialization;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Ini;

namespace OphiussaServerManager.Common.Models {
    public class DinoSpawn : AggregateIniValue
{
    public const bool DEFAULT_OVERRIDE_SPAWN_LIMIT_PERCENTAGE = true;
    public const float DEFAULT_SPAWN_LIMIT_PERCENTAGE = ClassMultiplier.DEFAULT_MULTIPLIER;
    public const float DEFAULT_SPAWN_WEIGHT_MULTIPLIER = ClassMultiplier.DEFAULT_MULTIPLIER;
 
    public string ClassName { get; set; } = "";

    public string Mod { get; set; } = "";

    public bool KnownDino { get; set; } = false;

    [XmlElement(ElementName="Name")]
    [AggregateIniValueEntry]
    public string DinoNameTag { get; set; } = "";

    [AggregateIniValueEntry] public bool OverrideSpawnLimitPercentage { get; set; } = DEFAULT_OVERRIDE_SPAWN_LIMIT_PERCENTAGE;

    [AggregateIniValueEntry]
    public float SpawnLimitPercentage { get; set; } = DEFAULT_SPAWN_LIMIT_PERCENTAGE;

    [AggregateIniValueEntry]
    public float SpawnWeightMultiplier { get; set; } = DEFAULT_SPAWN_WEIGHT_MULTIPLIER;

    public string DisplayName => GameData.FriendlyCreatureNameForClass(ClassName);

    public string DisplayMod => GameData.FriendlyNameForClass($"Mod_{Mod}", true) ?? Mod;

    public static DinoSpawn FromINIValue(string iniValue)
    {
        var newSpawn = new DinoSpawn();
        newSpawn.InitializeFromINIValue(iniValue);
        return newSpawn;
    }

    public override string GetSortKey()
    {
        return this.DinoNameTag;
    }

    public override bool IsEquivalent(AggregateIniValue other)
    {
        return String.Equals(this.DinoNameTag, ((DinoSpawn)other).DinoNameTag, StringComparison.OrdinalIgnoreCase);
    }
}
}