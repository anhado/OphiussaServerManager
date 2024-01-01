using System;
using System.Collections.Generic;
using System.Linq;
using OphiussaServerManager.Common.Ini;

namespace OphiussaServerManager.Common.Helpers.Ini {
    public class StringIniValueList : IniValueList<string>, IIniValuesList {
        public StringIniValueList(string iniKeyName, Func<IEnumerable<string>> resetFunc) :
            base(iniKeyName, resetFunc, string.Equals, m => m, ToIniValueInternal, FromIniValueInternal) {
        }

        public override bool IsArray => false;

        public IEnumerable<string> ToIniValues(object excludeIfValue) {
            string excludeIfStringValue = excludeIfValue is string ? (string)excludeIfValue : null;

            var values = new List<string>();
            if (string.IsNullOrWhiteSpace(IniCollectionKey))
                values.AddRange(this.Where(v => !EquivalencyFunc(v, excludeIfStringValue)).Select(d => ToIniValueInternal(d)));
            else
                values.AddRange(this.Where(v => !EquivalencyFunc(v, excludeIfStringValue)).Select(d => $"{IniCollectionKey}={ToIniValueInternal(d)}"));
            return values;
        }

        private static string ToIniValueInternal(string val) {
            return "\"" + val + "\"";
        }

        private static string FromIniValueInternal(string iniVal) {
            return iniVal.Trim('"');
        }
    }
}