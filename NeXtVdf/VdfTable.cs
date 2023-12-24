using System;
using System.Collections;
using System.Collections.Generic;

namespace NeXt.Vdf {
    /// <summary>
    ///     A VdfValue that represents a table containing other VdfValues
    /// </summary>
    public sealed class VdfTable : VdfValue, IList<VdfValue> {
        private readonly Dictionary<string, VdfValue> _valuelookup = new Dictionary<string, VdfValue>();

        private readonly List<VdfValue> _values = new List<VdfValue>();

        public VdfTable(string name) : base(name) {
        }

        public VdfTable(string name, IEnumerable<VdfValue> values) : this(name) {
            if (values == null) throw new ArgumentNullException("values");

            foreach (var val in values) Add(val);
        }

        public VdfValue this[string name] => _valuelookup[name];

        public int IndexOf(VdfValue item) {
            return _values.IndexOf(item);
        }

        public void Insert(int index, VdfValue item) {
            if (item == null) throw new ArgumentNullException("item");
            if (index < 0 || index >= _values.Count) throw new ArgumentOutOfRangeException("index");
            if (string.IsNullOrEmpty(item.Name)) throw new ArgumentException("item name cannot be empty or null");
            if (ContainsName(item.Name)) throw new ArgumentException("a value with name " + item.Name + " already exists in the table");


            item.Parent = this;

            _values.Insert(index, item);
            _valuelookup.Add(item.Name, item);
        }

        public void RemoveAt(int index) {
            var val = _values[index];
            _values.RemoveAt(index);
            _valuelookup.Remove(val.Name);
        }

        public VdfValue this[int index] {
            get => _values[index];
            set {
                if (_values[index].Name != value.Name) {
                    _valuelookup.Remove(_values[index].Name);
                    _valuelookup.Add(value.Name, value);
                }
                else {
                    _valuelookup[value.Name] = value;
                }

                _values[index] = value;
            }
        }

        public void Add(VdfValue item) {
            if (item == null) throw new ArgumentNullException("item");
            if (string.IsNullOrEmpty(item.Name)) throw new ArgumentException("item name cannot be empty or null");
            if (ContainsName(item.Name)) throw new ArgumentException("a value with name " + item.Name + " already exists in the table");

            item.Parent = this;

            _values.Add(item);
            _valuelookup.Add(item.Name, item);
        }

        public void Clear() {
            _values.Clear();
            _valuelookup.Clear();
        }

        public bool Contains(VdfValue item) {
            if (item == null) throw new ArgumentNullException("item");
            if (string.IsNullOrEmpty(item.Name)) throw new ArgumentException("item name cannot be empty or null");

            return _valuelookup.ContainsKey(item.Name) && _valuelookup[item.Name] == item;
        }

        public void CopyTo(VdfValue[] array, int arrayIndex) {
            _values.CopyTo(array, arrayIndex);
        }

        public int Count => _values.Count;

        public bool Remove(VdfValue item) {
            if (item == null) throw new ArgumentNullException("item");
            if (string.IsNullOrEmpty(item.Name)) throw new ArgumentException("item name cannot be empty or null");
            if (Contains(item)) {
                _valuelookup.Remove(item.Name);
                _values.Remove(item);
                return true;
            }

            return false;
        }

        public IEnumerator<VdfValue> GetEnumerator() {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return _values.GetEnumerator();
        }

        bool ICollection<VdfValue>.IsReadOnly => false;

        public void InsertAfter(VdfValue item, VdfValue newitem) {
            if (!Contains(item)) throw new ArgumentException("item needs to exist in this table", "item");

            if (string.IsNullOrEmpty(newitem.Name)) throw new ArgumentException("newitem name cannot be empty or null");
            if (ContainsName(newitem.Name)) throw new ArgumentException("a value with name " + newitem.Name + " already exists in the table");

            int i = -1;
            for (i = 0; i < _values.Count; i++)
                if (_values[i] == item)
                    break;

            if (i >= 0 && i < _values.Count) {
                if (i == _values.Count - 1)
                    Add(newitem);
                else
                    Insert(i + 1, newitem);
            }
        }

        public bool ContainsName(string name) {
            if (name == null) throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("name cannot be empty");

            return _valuelookup.ContainsKey(name);
        }

        public VdfValue GetByName(string name) {
            if (ContainsName(name)) return _valuelookup[name];
            return null;
        }

        public void Traverse(Func<VdfValue, bool> call) {
            if (call == null) throw new ArgumentNullException("call");
            foreach (var value in _values)
                if (!call(value))
                    break;
        }

        public void TraverseRecursive(Func<VdfValue, bool> call) {
            if (call == null) throw new ArgumentNullException("call");
            foreach (var value in _values)
                if (value is VdfTable) {
                    ((VdfTable)value).TraverseRecursive(call);
                }
                else {
                    if (!call(value)) break;
                }
        }
    }
}