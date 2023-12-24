using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OphiussaServerManager.Common {
    public class NullableValue<T> : INotifyPropertyChanged, INullableValue {
        private Dictionary<string, object> _properties = new Dictionary<string, object>();

        public NullableValue() {
            HasValue     = false;
            Value        = default;
            DefaultValue = default;
        }

        public NullableValue(T value) {
            HasValue     = value != null;
            Value        = value;
            DefaultValue = value;
        }

        public NullableValue(bool hasValue, T value) {
            HasValue     = hasValue;
            Value        = value;
            DefaultValue = value;
        }

        public NullableValue(T value, T defaultValue)
            : this(value) {
            DefaultValue = defaultValue;
        }

        public NullableValue(bool hasValue, T value, T defaultValue)
            : this(hasValue, value) {
            DefaultValue = defaultValue;
        }

        public T DefaultValue {
            get => Get<T>();
            set => Set(value);
        }

        public T Value {
            get => Get<T>();
            set => Set(value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool HasValue {
            get => Get<bool>();
            set => Set(value);
        }

        public INullableValue Clone() {
            return new NullableValue<T>(HasValue, Value, DefaultValue);
        }

        public void SetValue(object value) {
            if (value == null || !(value is NullableValue<T>))
                return;
            HasValue     = ((NullableValue<T>)value).HasValue;
            Value        = ((NullableValue<T>)value).Value;
            DefaultValue = ((NullableValue<T>)value).DefaultValue;
        }

        public bool Equals(T value) {
            return HasValue && Equals(value, Value);
        }

        public NullableValue<T> SetValue(T value) {
            HasValue = true;
            Value    = value;
            return this;
        }

        public NullableValue<T> SetValue(bool hasValue, T value) {
            HasValue = hasValue;
            Value    = value;
            return this;
        }

        public override string ToString() {
            return !HasValue ? string.Empty : Value.ToString();
        }

        protected T2 Get<T2>([CallerMemberName] string name = null) {
            object obj        = null;
            var    properties = _properties;
            // ISSUE: explicit non-virtual call
            if ((properties != null ? properties.TryGetValue(name, out obj) ? 1 : 0 : 0) == 0)
                return default;
            return obj != null ? (T2)obj : default;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            var propertyChanged = PropertyChanged;
            if (propertyChanged == null)
                return;
            propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void Set<T2>(T2 value, [CallerMemberName] string name = null) {
            if (Equals(value, Get<T2>(name)))
                return;
            if (_properties == null)
                _properties = new Dictionary<string, object>();
            _properties[name] = value;
            OnPropertyChanged(name);
        }
    }
}