﻿using System;
using System.Collections.Generic;
using System.Globalization;
using OphiussaServerManager.Common.Ini;

namespace OphiussaServerManager.Common.Helpers.Ini {
    public class IntegerIniValueArray : IniValueList<int>, IIniValuesList
    { 
        public IntegerIniValueArray(string iniKeyName, Func<IEnumerable<int>> resetFunc) : 
            base(iniKeyName, resetFunc, (a, b) => a == b, m => m, ToIniValueInternal, FromIniValueInternal)
        {
        }

        public override bool IsArray => true;

        private static string ToIniValueInternal(int val)
        {
            return val.ToString("0", CultureInfo.InvariantCulture);
        }

        private static int FromIniValueInternal(string iniVal)
        {
            return int.Parse(iniVal, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
        }

        public IEnumerable<string> ToIniValues(object excludeIfValue)
        {
            var excludeIfIntegerValue = excludeIfValue is int ? (int)excludeIfValue : int.MinValue;

            var values = new List<string>();
            for (var i = 0; i < this.Count; i++)
            {
                if (!EquivalencyFunc(this[i], excludeIfIntegerValue))
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