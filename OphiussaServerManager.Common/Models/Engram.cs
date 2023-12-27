using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OphiussaServerManager.Common.Helpers;

namespace OphiussaServerManager.Common.Models {
public class Engram 
{ 
    public string EngramClassName { get; set; } = "";

    public int EngramLevelRequirement { get; set; } = 0;

    public int EngramPointsCost { get; set; } = 0;

    public string Mod { get; set; } = "";

    public bool KnownEngram { get; set; } = false;

    public bool IsTekgram { get; set; } =false;

    public string DisplayName => GameData.FriendlyEngramNameForClass(EngramClassName);

    public string DisplayMod => GameData.FriendlyNameForClass($"Mod_{Mod}", true) ?? Mod;

    public Engram Duplicate()
    {
        var properties = this.GetType().GetProperties();

        var result = new Engram();
        foreach (var prop in properties)
        {
            if (prop.CanWrite)
                prop.SetValue(result, prop.GetValue(this));
        }

        return result;
    }
}
}