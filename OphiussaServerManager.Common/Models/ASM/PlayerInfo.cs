using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ArkData;

namespace OphiussaServerManager.Common.Models {
    public class PlayerInfo : INotifyPropertyChanged {
        private Dictionary<string, object> _properties = new Dictionary<string, object>();

        public PlayerInfo() {
            PlayerId      = string.Empty;
            PlayerName    = string.Empty;
            CharacterName = string.Empty;
            IsOnline      = false;
            IsAdmin       = false;
            IsWhitelisted = false;
            TribeName     = string.Empty;
            LastUpdated   = DateTime.MinValue;
            IsValid       = true;
            PlayerData    = null;
        }

        public string PlayerId {
            get => Get<string>();
            set => Set(value);
        }

        public string PlayerName {
            get => Get<string>();
            set {
                Set(value);
                PlayerNameFilterString = value?.ToLower();
            }
        }

        public string PlayerNameFilterString { get; private set; }

        public long CharacterId {
            get => Get<long>();
            set => Set(value);
        }

        public string CharacterName {
            get => Get<string>();
            set {
                Set(value);
                CharacterNameFilterString = value?.ToLower();
            }
        }

        public string CharacterNameFilterString { get; private set; }

        public bool IsOnline {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsAdmin {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsWhitelisted {
            get => Get<bool>();
            set => Set(value);
        }

        public string TribeName {
            get => Get<string>();
            set {
                Set(value);
                TribeNameFilterString = value?.ToLower();
            }
        }

        public string TribeNameFilterString { get; private set; }

        public DateTime LastUpdated {
            get => Get<DateTime>();
            set => Set(value);
        }

        public bool IsValid {
            get => Get<bool>();
            set => Set(value);
        }

        public PlayerData PlayerData {
            get => Get<PlayerData>();
            set => Set(value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void UpdateData(PlayerData playerData) {
            PlayerData    = playerData;
            PlayerId      = playerData?.PlayerId;
            CharacterId   = playerData != null ? playerData.CharacterId : 0L;
            CharacterName = playerData?.CharacterName;
            TribeName     = playerData?.Tribe?.Name;
            LastUpdated   = playerData != null ? playerData.FileUpdated : DateTime.MinValue;
        }

        public void UpdatePlatformData(PlayerData playerData) {
            if (playerData == null)
                return;
            if (PlayerData?.PlayerName != null)
                playerData.PlayerName = PlayerData.PlayerName;
            var playerData1 = playerData;
            var playerData2 = PlayerData;
            var dateTime    = playerData2 != null ? playerData2.LastPlatformUpdateUtc : DateTime.MinValue;
            playerData1.LastPlatformUpdateUtc = dateTime;
            var playerData3   = playerData;
            var playerData4   = PlayerData;
            int noUpdateCount = playerData4 != null ? playerData4.NoUpdateCount : 0;
            playerData3.NoUpdateCount = noUpdateCount;
        }

        protected T Get<T>([CallerMemberName] string name = null) {
            object obj        = null;
            var    properties = _properties;
            if ((properties != null ? properties.TryGetValue(name, out obj) ? 1 : 0 : 0) == 0)
                return default;
            return obj != null ? (T)obj : default;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            var propertyChanged = PropertyChanged;
            if (propertyChanged == null)
                return;
            propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void Set<T>(T value, [CallerMemberName] string name = null) {
            if (Equals(value, Get<T>(name)))
                return;
            if (_properties == null)
                _properties = new Dictionary<string, object>();
            _properties[name] = value;
            OnPropertyChanged(name);
        }
    }
}