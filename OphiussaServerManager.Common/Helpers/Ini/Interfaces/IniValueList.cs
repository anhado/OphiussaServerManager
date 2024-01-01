using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace OphiussaServerManager.Common.Ini {
    public abstract class IniValueList<T> : SortableObservableCollection<T>, IIniValuesCollection {
        private bool _isEnabled;

        protected IniValueList(string aggregateValueName, Func<IEnumerable<T>> resetFunc, Func<T, T, bool> equivalencyFunc, Func<T, object> sortKeySelectorFunc, Func<T, string> toIniValue, Func<string, T> fromIniValue) {
            ToIniValue          = toIniValue;
            FromIniValue        = fromIniValue;
            ResetFunc           = resetFunc;
            EquivalencyFunc     = equivalencyFunc;
            SortKeySelectorFunc = sortKeySelectorFunc;
            IniCollectionKey    = aggregateValueName;

            Reset();
            IsEnabled = false;
        }

        public    Func<T, string>      ToIniValue          { get; }
        public    Func<string, T>      FromIniValue        { get; }
        protected Func<IEnumerable<T>> ResetFunc           { get; }
        public    Func<T, T, bool>     EquivalencyFunc     { get; }
        protected Func<T, object>      SortKeySelectorFunc { get; }

        public bool IsEnabled {
            get => _isEnabled;
            set {
                _isEnabled = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsEnabled)));
            }
        }

        public abstract bool IsArray { get; }

        public string IniCollectionKey { get; }

        public virtual void FromIniValues(IEnumerable<string> values) {
            Clear();

            if (IsArray) {
                var list = new List<T>();
                if (ResetFunc != null)
                    list.AddRange(ResetFunc());

                foreach (string v in values) {
                    int indexStart = v.IndexOf('[');
                    int indexEnd   = v.IndexOf(']');

                    if (indexStart >= indexEnd)
                        // Invalid format
                        continue;

                    int index;
                    if (!int.TryParse(v.Substring(indexStart + 1, indexEnd - indexStart - 1), out index))
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
            else {
                AddRange(values.Select(v => v.Substring(v.IndexOf('=') + 1)).Select(FromIniValue));
                IsEnabled = Count != 0;

                // Add any default values which were missing
                if (ResetFunc != null) {
                    AddRange(ResetFunc().Where(r => !this.Any(v => EquivalencyFunc(v, r))));
                    Sort(SortKeySelectorFunc);
                }
            }
        }

        public virtual IEnumerable<string> ToIniValues() {
            var values = new List<string>();
            if (IsArray) {
                for (int i = 0; i < Count; i++)
                    if (string.IsNullOrWhiteSpace(IniCollectionKey))
                        values.Add(ToIniValue(this[i]));
                    else
                        values.Add($"{IniCollectionKey}[{i}]={ToIniValue(this[i])}");
            }
            else {
                if (string.IsNullOrWhiteSpace(IniCollectionKey))
                    values.AddRange(this.Select(d => ToIniValue(d)));
                else
                    values.AddRange(this.Select(d => $"{IniCollectionKey}={ToIniValue(d)}"));
            }

            return values;
        }

        public void AddRange(IEnumerable<T> values) {
            foreach (var value in values) Add(value);
        }

        public void Reset() {
            Clear();
            if (ResetFunc != null)
                AddRange(ResetFunc());
        }
    }
}