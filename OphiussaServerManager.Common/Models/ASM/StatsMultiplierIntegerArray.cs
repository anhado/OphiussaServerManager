using System;
using System.Collections.Generic;
using System.Linq;
using OphiussaServerManager.Common.Helpers.Ini;

namespace OphiussaServerManager.Common.Models {
    public class StatsMultiplierIntegerArray : IntegerIniValueArray {
        protected StatsMultiplierIntegerArray(string iniKeyName, Func<IEnumerable<int>> resetFunc, bool[] inclusions)
            : base(iniKeyName, resetFunc) {
            Inclusions = inclusions;
        }

        public StatsMultiplierIntegerArray(string iniKeyName, Func<IEnumerable<int>> resetFunc, bool[] inclusions, bool onlyWriteNonDefaults)
            : base(iniKeyName, resetFunc) {
            Inclusions = inclusions;

            if (onlyWriteNonDefaults && resetFunc != null) {
                DefaultValues = new StatsMultiplierIntegerArray(iniKeyName, null, inclusions);
                DefaultValues.AddRange(resetFunc());
            }
        }

        public bool IsEnabled {
            get {
                for (int i = 0; i < DefaultValues.Count; i++)
                    if (Count == DefaultValues.Count)
                        if (this[i] != DefaultValues[i]) {
                            base.IsEnabled = true;
                            break;
                        }

                return base.IsEnabled;
            }
            set => base.IsEnabled = value;
        }

        public bool[] Inclusions { get; }

        private StatsMultiplierIntegerArray DefaultValues { get; }

        public override void FromIniValues(IEnumerable<string> values) {
            Clear();

            var list = new List<int>();
            if (ResetFunc != null)
                list.AddRange(ResetFunc());

            foreach (string v in values) {
                int indexStart = v.IndexOf('[');
                int indexEnd   = v.IndexOf(']');

                if (indexStart >= indexEnd)
                    // Invalid format
                    continue;

                if (!int.TryParse(v.Substring(indexStart + 1, indexEnd - indexStart - 1), out int index))
                    // Invalid index
                    continue;

                if (index >= list.Count)
                    // Unexpected size
                    continue;

                list[index] = FromIniValue(v.Substring(v.IndexOf('=') + 1).Trim());
                IsEnabled   = true;
            }

            AddRange(list);
        }

        public override IEnumerable<string> ToIniValues() {
            var values = new List<string>();
            for (int i = 0; i < Count; i++) {
                if (!(Inclusions?.ElementAtOrDefault(i) ?? true))
                    continue;
                if (DefaultValues != null && Equals(DefaultValues[i], this[i]))
                    continue;

                if (string.IsNullOrWhiteSpace(IniCollectionKey))
                    values.Add(ToIniValue(this[i]));
                else
                    values.Add($"{IniCollectionKey}[{i}]={ToIniValue(this[i])}");
            }

            return values;
        }
    }
}