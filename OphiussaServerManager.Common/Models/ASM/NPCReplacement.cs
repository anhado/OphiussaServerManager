using System;
using System.Runtime.Serialization;
using System.Windows;
using OphiussaServerManager.Common.Ini;

namespace OphiussaServerManager.Common.Models {
   
    public class NPCReplacement : AggregateIniValue {
        [AggregateIniValueEntry] public string FromClassName { get; set; } = "";

        [AggregateIniValueEntry] public string ToClassName { get; set; } = "";
      
        public static NPCReplacement FromINIValue(string iniValue)
        {
            var newSpawn = new NPCReplacement();
            newSpawn.InitializeFromINIValue(iniValue);
            return newSpawn;
        }

        public override string GetSortKey()
        {
            return this.FromClassName;
        }

        public override bool IsEquivalent(AggregateIniValue other)
        {
            return String.Equals(this.FromClassName, ((NPCReplacement)other).FromClassName, StringComparison.OrdinalIgnoreCase);
        }

        public override bool ShouldSave()
        {
            return (!String.Equals(FromClassName, ToClassName, StringComparison.OrdinalIgnoreCase));
        }
    }
}