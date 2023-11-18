using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager
{

    public class NullableValue<T> : INotifyPropertyChanged, INullableValue
    {
        private Dictionary<string, object> _properties = new Dictionary<string, object>();

        public NullableValue()
        {
            this.HasValue = false;
            this.Value = default(T);
            this.DefaultValue = default(T);
        }

        public NullableValue(T value)
        {
            this.HasValue = (object)value != null;
            this.Value = value;
            this.DefaultValue = value;
        }

        public NullableValue(bool hasValue, T value)
        {
            this.HasValue = hasValue;
            this.Value = value;
            this.DefaultValue = value;
        }

        public NullableValue(T value, T defaultValue)
          : this(value)
        {
            this.DefaultValue = defaultValue;
        }

        public NullableValue(bool hasValue, T value, T defaultValue)
          : this(hasValue, value)
        {
            this.DefaultValue = defaultValue;
        }

        public bool HasValue
        {
            get => this.Get<bool>(nameof(HasValue));
            set => this.Set<bool>(value, nameof(HasValue));
        }

        public T DefaultValue
        {
            get => this.Get<T>(nameof(DefaultValue));
            set => this.Set<T>(value, nameof(DefaultValue));
        }

        public T Value
        {
            get => this.Get<T>(nameof(Value));
            set => this.Set<T>(value, nameof(Value));
        }

        public INullableValue Clone() => (INullableValue)new NullableValue<T>(this.HasValue, this.Value, this.DefaultValue);

        public bool Equals(T value) => this.HasValue && object.Equals((object)value, (object)this.Value);

        public NullableValue<T> SetValue(T value)
        {
            this.HasValue = true;
            this.Value = value;
            return this;
        }

        public NullableValue<T> SetValue(bool hasValue, T value)
        {
            this.HasValue = hasValue;
            this.Value = value;
            return this;
        }

        public void SetValue(object value)
        {
            if (value == null || !(value is NullableValue<T>))
                return;
            this.HasValue = ((NullableValue<T>)value).HasValue;
            this.Value = ((NullableValue<T>)value).Value;
            this.DefaultValue = ((NullableValue<T>)value).DefaultValue;
        }

        public override string ToString() => !this.HasValue ? string.Empty : this.Value.ToString();

        public event PropertyChangedEventHandler PropertyChanged;

        protected T2 Get<T2>([CallerMemberName] string name = null)
        {
            object obj = (object)null;
            Dictionary<string, object> properties = this._properties;
            // ISSUE: explicit non-virtual call
            if ((properties != null ? (properties.TryGetValue(name, out obj) ? 1 : 0) : 0) == 0)
                return default(T2);
            return obj != null ? (T2)obj : default(T2);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged == null)
                return;
            propertyChanged((object)this, new PropertyChangedEventArgs(propertyName));
        }

        protected void Set<T2>(T2 value, [CallerMemberName] string name = null)
        {
            if (object.Equals((object)value, (object)this.Get<T2>(name)))
                return;
            if (this._properties == null)
                this._properties = new Dictionary<string, object>();
            this._properties[name] = (object)value;
            this.OnPropertyChanged(name);
        }
    }
}
