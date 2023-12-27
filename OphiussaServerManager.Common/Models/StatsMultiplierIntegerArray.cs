﻿using System;
using System.Collections.Generic;
using System.Linq;
using OphiussaServerManager.Common.Helpers.Ini;

namespace OphiussaServerManager.Common.Models {
   public class StatsMultiplierIntegerArray : IntegerIniValueArray
{
    protected StatsMultiplierIntegerArray(string iniKeyName, Func<IEnumerable<int>> resetFunc, bool[] inclusions)
        : base(iniKeyName, resetFunc)
    {
        Inclusions = inclusions;
    }

    public StatsMultiplierIntegerArray(string iniKeyName, Func<IEnumerable<int>> resetFunc, bool[] inclusions, bool onlyWriteNonDefaults)
        : base(iniKeyName, resetFunc)
    {
        Inclusions = inclusions;

        if (onlyWriteNonDefaults && resetFunc != null)
        {
            DefaultValues = new StatsMultiplierIntegerArray(iniKeyName, null, inclusions);
            DefaultValues.AddRange(resetFunc());
        }
    }

    public bool[] Inclusions { get; private set; } = null;

    private StatsMultiplierIntegerArray DefaultValues { get; set; } = null;

    public override void FromIniValues(IEnumerable<string> values)
    {
        this.Clear();

        var list = new List<int>();
        if (this.ResetFunc != null)
            list.AddRange(this.ResetFunc());

        foreach (var v in values)
        {
            var indexStart = v.IndexOf('[');
            var indexEnd = v.IndexOf(']');

            if (indexStart >= indexEnd)
            {
                // Invalid format
                continue;
            }

            if (!int.TryParse(v.Substring(indexStart + 1, indexEnd - indexStart - 1), out int index))
            {
                // Invalid index
                continue;
            }

            if (index >= list.Count)
            {
                // Unexpected size
                continue;
            }

            list[index] = this.FromIniValue(v.Substring(v.IndexOf('=') + 1).Trim());
            this.IsEnabled = true;
        }

        this.AddRange(list);
    }

    public override IEnumerable<string> ToIniValues()
    {
        var values = new List<string>();
        for (var i = 0; i < this.Count; i++)
        {
            if (!(Inclusions?.ElementAtOrDefault(i) ?? true))
                continue;
            if (DefaultValues != null && Equals(DefaultValues[i], this[i]))
                continue;

            if (string.IsNullOrWhiteSpace(IniCollectionKey))
                values.Add(this.ToIniValue(this[i]));
            else
                values.Add($"{this.IniCollectionKey}[{i}]={this.ToIniValue(this[i])}");
        }
        return values;
    }
}
}