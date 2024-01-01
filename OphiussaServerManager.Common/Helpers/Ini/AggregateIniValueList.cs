using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using OphiussaServerManager.Common.Ini;

namespace OphiussaServerManager.Common.Helpers.Ini {
    public class AggregateIniValueList<T> : SortableObservableCollection<T>, IIniValuesCollection
        where T : AggregateIniValue, new() {
        protected readonly Func<IEnumerable<T>> _resetFunc;
        private            bool                 _isEnabled;

        public AggregateIniValueList(string aggregateValueName, Func<IEnumerable<T>> resetFunc) {
            IniCollectionKey = aggregateValueName;
            _resetFunc       = resetFunc;
        }

        public string IniCollectionKey { get; }

        public bool IsEnabled {
            get => _isEnabled;
            set {
                _isEnabled = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsEnabled)));
            }
        }

        public bool IsArray => false;

        public virtual void FromIniValues(IEnumerable<string> iniValues) {
            var items = iniValues?.Select(AggregateIniValue.FromINIValue<T>);

            Clear();
            AddRange(items);
            IsEnabled = Count > 0;

            // Add any default values which were missing
            if (_resetFunc != null) AddRange(_resetFunc().Where(r => !this.Any(v => v.IsEquivalent(r))));

            Sort(AggregateIniValue.SortKeySelector);
        }

        public virtual IEnumerable<string> ToIniValues() {
            if (string.IsNullOrWhiteSpace(IniCollectionKey))
                return this.Where(d => d.ShouldSave()).Select(d => d.ToINIValue());

            return this.Where(d => d.ShouldSave()).Select(d => $"{IniCollectionKey}={d.ToINIValue()}");
        }

        public void AddRange(IEnumerable<T> values) {
            if (values == null)
                return;

            foreach (var value in values) {
                var item = this.FirstOrDefault(i => i.IsEquivalent(value));
                if (item == null)
                    Add(value);
                else
                    item.Update(value);
            }

            Sort(AggregateIniValue.SortKeySelector);
        }

        public virtual void Reset() {
            Clear();
            if (_resetFunc != null)
                AddRange(_resetFunc());
            IsEnabled = Count > 0;

            Sort(AggregateIniValue.SortKeySelector);
        }
    }
}