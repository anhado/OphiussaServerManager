﻿using System;
using System.Runtime.Serialization;
using System.Windows;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Ini;

namespace OphiussaServerManager.Common.Models {
public class ClassMultiplier : AggregateIniValue
{
    public const float DEFAULT_MULTIPLIER = 1.0f;
 
    [AggregateIniValueEntry] public string ClassName { get; set; } = "";
 
    [AggregateIniValueEntry]
    public float Multiplier { get; set; } = DEFAULT_MULTIPLIER;

    public virtual string DisplayName => GameData.FriendlyNameForClass(ClassName);

    public static ClassMultiplier FromINIValue(string iniValue)
    {
        var newSpawn = new ClassMultiplier();
        newSpawn.InitializeFromINIValue(iniValue);
        return newSpawn;
    }

    public override string GetSortKey() => GameData.FriendlyNameForClass(this.ClassName);

    public override bool IsEquivalent(AggregateIniValue other) => String.Equals(this.ClassName, ((ClassMultiplier)other).ClassName, StringComparison.OrdinalIgnoreCase);
}
}