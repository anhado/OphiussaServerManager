using System;
using OphiussaServerManager.Common.Ini;

namespace OphiussaServerManager.Common.Models {
    public class NPCReplacement : AggregateIniValue {
        [AggregateIniValueEntry] public string FromClassName { get; set; } = "";

        [AggregateIniValueEntry] public string ToClassName { get; set; } = "";

        public static NPCReplacement FromINIValue(string iniValue) {
            var newSpawn = new NPCReplacement();
            newSpawn.InitializeFromINIValue(iniValue);
            return newSpawn;
        }

        public override string GetSortKey() {
            return FromClassName;
        }

        public override bool IsEquivalent(AggregateIniValue other) {
            return string.Equals(FromClassName, ((NPCReplacement)other).FromClassName, StringComparison.OrdinalIgnoreCase);
        }

        public override bool ShouldSave() {
            return !string.Equals(FromClassName, ToClassName, StringComparison.OrdinalIgnoreCase);
        }
    }
}