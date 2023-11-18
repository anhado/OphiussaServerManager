
using ArkData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OphiussaServerManager.Models
{
    public class PlayerInfo : INotifyPropertyChanged
    {
        private Dictionary<string, object> _properties = new Dictionary<string, object>();

        public PlayerInfo()
        {
            this.PlayerId = string.Empty;
            this.PlayerName = string.Empty;
            this.CharacterName = string.Empty;
            this.IsOnline = false;
            this.IsAdmin = false;
            this.IsWhitelisted = false;
            this.TribeName = string.Empty;
            this.LastUpdated = DateTime.MinValue;
            this.IsValid = true;
            this.PlayerData = (PlayerData)null;
        }

        public string PlayerId
        {
            get => this.Get<string>(nameof(PlayerId));
            set => this.Set<string>(value, nameof(PlayerId));
        }

        public string PlayerName
        {
            get => this.Get<string>(nameof(PlayerName));
            set
            {
                this.Set<string>(value, nameof(PlayerName));
                this.PlayerNameFilterString = value?.ToLower();
            }
        }

        public string PlayerNameFilterString { get; private set; }

        public long CharacterId
        {
            get => this.Get<long>(nameof(CharacterId));
            set => this.Set<long>(value, nameof(CharacterId));
        }

        public string CharacterName
        {
            get => this.Get<string>(nameof(CharacterName));
            set
            {
                this.Set<string>(value, nameof(CharacterName));
                this.CharacterNameFilterString = value?.ToLower();
            }
        }

        public string CharacterNameFilterString { get; private set; }

        public bool IsOnline
        {
            get => this.Get<bool>(nameof(IsOnline));
            set => this.Set<bool>(value, nameof(IsOnline));
        }

        public bool IsAdmin
        {
            get => this.Get<bool>(nameof(IsAdmin));
            set => this.Set<bool>(value, nameof(IsAdmin));
        }

        public bool IsWhitelisted
        {
            get => this.Get<bool>(nameof(IsWhitelisted));
            set => this.Set<bool>(value, nameof(IsWhitelisted));
        }

        public string TribeName
        {
            get => this.Get<string>(nameof(TribeName));
            set
            {
                this.Set<string>(value, nameof(TribeName));
                this.TribeNameFilterString = value?.ToLower();
            }
        }

        public string TribeNameFilterString { get; private set; }

        public DateTime LastUpdated
        {
            get => this.Get<DateTime>(nameof(LastUpdated));
            set => this.Set<DateTime>(value, nameof(LastUpdated));
        }

        public bool IsValid
        {
            get => this.Get<bool>(nameof(IsValid));
            set => this.Set<bool>(value, nameof(IsValid));
        }

        public PlayerData PlayerData
        {
            get => this.Get<PlayerData>(nameof(PlayerData));
            set => this.Set<PlayerData>(value, nameof(PlayerData));
        }

        public void UpdateData(PlayerData playerData)
        {
            this.PlayerData = playerData;
            this.PlayerId = playerData?.PlayerId;
            this.CharacterId = playerData != null ? playerData.CharacterId : 0L;
            this.CharacterName = playerData?.CharacterName;
            this.TribeName = playerData?.Tribe?.Name;
            this.LastUpdated = playerData != null ? playerData.FileUpdated : DateTime.MinValue;
        }

        public void UpdatePlatformData(PlayerData playerData)
        {
            if (playerData == null)
                return;
            if (this.PlayerData?.PlayerName != null)
                playerData.PlayerName = this.PlayerData.PlayerName;
            PlayerData playerData1 = playerData;
            PlayerData playerData2 = this.PlayerData;
            DateTime dateTime = playerData2 != null ? playerData2.LastPlatformUpdateUtc : DateTime.MinValue;
            playerData1.LastPlatformUpdateUtc = dateTime;
            PlayerData playerData3 = playerData;
            PlayerData playerData4 = this.PlayerData;
            int noUpdateCount = playerData4 != null ? playerData4.NoUpdateCount : 0;
            playerData3.NoUpdateCount = noUpdateCount;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected T Get<T>([CallerMemberName] string name = null)
        {
            object obj = (object)null;
            Dictionary<string, object> properties = this._properties;
            if ((properties != null ? (properties.TryGetValue(name, out obj) ? 1 : 0) : 0) == 0)
                return default(T);
            return obj != null ? (T)obj : default(T);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged == null)
                return;
            propertyChanged((object)this, new PropertyChangedEventArgs(propertyName));
        }

        protected void Set<T>(T value, [CallerMemberName] string name = null)
        {
            if (object.Equals((object)value, (object)this.Get<T>(name)))
                return;
            if (this._properties == null)
                this._properties = new Dictionary<string, object>();
            this._properties[name] = (object)value;
            this.OnPropertyChanged(name);
        }
    }
}
