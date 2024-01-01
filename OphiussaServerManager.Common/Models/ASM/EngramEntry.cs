using System;
using System.Collections.Generic;
using System.Linq;
using OphiussaServerManager.Common.Helpers.Ini;
using OphiussaServerManager.Common.Ini;

namespace OphiussaServerManager.Common.Models {
    public class EngramEntryList : AggregateIniValueList<EngramEntry> {
        public EngramEntryList(string aggregateValueName)
            : base(aggregateValueName, null) {
        }

        public override void FromIniValues(IEnumerable<string> iniValues) {
            var items = iniValues?.Select(AggregateIniValue.FromINIValue<EngramEntry>);

            Clear();

            AddRange(items.Where(i => !this.Any(e => e.IsEquivalent(i))));

            foreach (var item in items.Where(i => this.Any(e => e.IsEquivalent(i)))) {
                var e = this.FirstOrDefault(r => r.IsEquivalent(item));
                e.EngramLevelRequirement = item.EngramLevelRequirement;
                e.EngramPointsCost       = item.EngramPointsCost;
                e.EngramHidden           = item.EngramHidden;
                e.RemoveEngramPreReq     = item.RemoveEngramPreReq;
            }

            IsEnabled = Count != 0;

            Sort(AggregateIniValue.SortKeySelector);
        }

        public override IEnumerable<string> ToIniValues() {
            if (string.IsNullOrWhiteSpace(IniCollectionKey))
                return this.Where(d => d.ShouldSave()).Select(d => d.ToINIValue());

            return this.Where(d => d.ShouldSave()).Select(d => $"{IniCollectionKey}={d.ToINIValue()}");
        }
    }

    public class EngramEntry : AggregateIniValue {
        [AggregateIniValueEntry] public string EngramClassName { get; set; } = "";

        [AggregateIniValueEntry] public int EngramLevelRequirement { get; set; }

        [AggregateIniValueEntry] public int EngramPointsCost { get; set; }

        [AggregateIniValueEntry] public bool EngramHidden { get; set; }

        [AggregateIniValueEntry] public bool RemoveEngramPreReq { get; set; }

        public static EngramEntry FromINIValue(string iniValue) {
            var engramEntry = new EngramEntry();
            engramEntry.InitializeFromINIValue(iniValue);
            return engramEntry;
        }

        public override string GetSortKey() {
            return null;
        }

        public override void InitializeFromINIValue(string value) {
            base.InitializeFromINIValue(value);
        }

        public override bool IsEquivalent(AggregateIniValue other) {
            return string.Equals(EngramClassName, ((EngramEntry)other).EngramClassName, StringComparison.OrdinalIgnoreCase);
        }

        public override string ToINIValue() {
            return base.ToINIValue();
        }
    }
}