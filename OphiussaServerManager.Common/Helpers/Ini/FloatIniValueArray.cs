using System;
using System.Collections.Generic;
using System.Globalization;

namespace OphiussaServerManager.Common.Ini {
    public class FloatIniValueArray : IniValueList<float>, IIniValuesList {
        public FloatIniValueArray(string iniKeyName, Func<IEnumerable<float>> resetFunc) :
            base(iniKeyName, resetFunc, (a, b) => a == b, m => m, ToIniValueInternal, FromIniValueInternal) {
        }

        public override bool IsArray => true;

        public IEnumerable<string> ToIniValues(object excludeIfValue) {
            float excludeIfFloatValue = excludeIfValue is float ? (float)excludeIfValue : float.NaN;

            var values = new List<string>();
            for (int i = 0; i < Count; i++)
                if (!EquivalencyFunc(this[i], excludeIfFloatValue)) {
                    if (string.IsNullOrWhiteSpace(IniCollectionKey))
                        values.Add(ToIniValue(this[i]));
                    else
                        values.Add($"{IniCollectionKey}[{i}]={ToIniValue(this[i])}");
                }

            return values;
        }

        private static string ToIniValueInternal(float val) {
            return val.ToString("0.0#########", CultureInfo.InvariantCulture);
        }

        private static float FromIniValueInternal(string iniVal) {
            string tempValue = iniVal.Replace("f", "");
            return float.Parse(tempValue, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
        }
    }
}