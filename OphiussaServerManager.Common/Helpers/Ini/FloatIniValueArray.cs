using System;
using System.Collections.Generic;
using System.Globalization;

namespace OphiussaServerManager.Common.Ini {

    public class FloatIniValueArray : IniValueList<float>, IIniValuesList
    { 
        public FloatIniValueArray(string iniKeyName, Func<IEnumerable<float>> resetFunc) : 
            base(iniKeyName, resetFunc, (a, b) => a == b, m => m, ToIniValueInternal, FromIniValueInternal)
        {
        }

        public override bool IsArray => true;

        private static string ToIniValueInternal(float val)
        {
            return val.ToString("0.0#########", CultureInfo.InvariantCulture);
        }

        private static float FromIniValueInternal(string iniVal)
        {
            var tempValue = iniVal.Replace("f", "");
            return float.Parse(tempValue, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
        }

        public IEnumerable<string> ToIniValues(object excludeIfValue)
        {
            var excludeIfFloatValue = excludeIfValue is float ? (float)excludeIfValue : float.NaN;

            var values = new List<string>();
            for (var i = 0; i < this.Count; i++)
            {
                if (!EquivalencyFunc(this[i], excludeIfFloatValue))
                {
                    if (string.IsNullOrWhiteSpace(IniCollectionKey))
                        values.Add(this.ToIniValue(this[i]));
                    else
                        values.Add($"{this.IniCollectionKey}[{i}]={this.ToIniValue(this[i])}");
                }
            }
            return values;
        }
    }
}