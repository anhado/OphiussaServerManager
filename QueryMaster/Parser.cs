using System;
using System.Linq;
using System.Text;

namespace QueryMaster {
    internal class Parser {
        private readonly byte[] _data;
        private readonly int    _lastPosition;
        private          int    _currentPosition = -1;

        internal Parser(byte[] data) {
            _data            = data;
            _currentPosition = -1;
            _lastPosition    = _data.Length - 1;
        }

        internal bool HasUnParsedBytes => _currentPosition < _lastPosition;

        internal byte ReadByte() {
            _currentPosition++;
            if (_currentPosition > _lastPosition)
                throw new ParseException("Index was outside the bounds of the byte array.");
            return _data[_currentPosition];
        }

        internal short ReadShort() {
            _currentPosition++;
            if (_currentPosition + 3 > _lastPosition)
                throw new ParseException("Unable to parse bytes to short.");
            short num;
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(_data, _currentPosition, 2);
            num = BitConverter.ToInt16(_data, _currentPosition);
            _currentPosition++;
            return num;
        }

        internal int ReadInt() {
            _currentPosition++;
            if (_currentPosition + 3 > _lastPosition)
                throw new ParseException("Unable to parse bytes to int.");
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(_data, _currentPosition, 4);
            int num = BitConverter.ToInt32(_data, _currentPosition);
            _currentPosition += 3;
            return num;
        }

        internal float ReadFloat() {
            _currentPosition++;
            if (_currentPosition + 3 > _lastPosition)
                throw new ParseException("Unable to parse bytes to float.");
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(_data, _currentPosition, 4);
            float num = BitConverter.ToSingle(_data, _currentPosition);
            _currentPosition += 3;
            return num;
        }

        internal string ReadString() {
            _currentPosition++;
            int temp = _currentPosition;
            while (_data[_currentPosition] != 0x00) {
                _currentPosition++;
                if (_currentPosition > _lastPosition)
                    throw new ParseException("Unable to parse bytes to string.");
            }

            return Encoding.UTF8.GetString(_data, temp, _currentPosition - temp);
        }

        internal void Skip(byte count) {
            _currentPosition += count;
            if (_currentPosition > _lastPosition)
                throw new ParseException("skip count was outside the bounds of the byte array.");
        }

        internal byte[] GetUnParsedData() {
            return _data.Skip(_currentPosition + 1).ToArray();
        }
    }
}