namespace SSQLib
{
    public class ServerInfo
    {
        private string name = "";
        private string ip = "";
        private string port = "";
        private string game = "";
        private string gameVersion = "";
        private string appID = "";
        private string map = "";
        private string playerCount = "";
        private string botCount = "";
        private string maxPlayers = "";
        private bool passworded;
        private bool vac;
        private ServerInfo.DedicatedType dedicated;
        private ServerInfo.OSType os;

        public string Name
        {
            get => this.name;
            set => this.name = value;
        }

        public string IP
        {
            get => this.ip;
            set => this.ip = value;
        }

        public string Port
        {
            get => this.port;
            set => this.port = value;
        }

        public string Game
        {
            get => this.game;
            set => this.game = value;
        }

        public string Version
        {
            get => this.gameVersion;
            set => this.gameVersion = value;
        }

        public string Map
        {
            get => this.map;
            set => this.map = value;
        }

        public string PlayerCount
        {
            get => this.playerCount;
            set => this.playerCount = value;
        }

        public string BotCount
        {
            get => this.botCount;
            set => this.botCount = value;
        }

        public string MaxPlayers
        {
            get => this.maxPlayers;
            set => this.maxPlayers = value;
        }

        public bool Password
        {
            get => this.passworded;
            set => this.passworded = value;
        }

        public bool VAC
        {
            get => this.vac;
            set => this.vac = value;
        }

        public string AppID
        {
            get => this.appID;
            set => this.appID = value;
        }

        public ServerInfo.DedicatedType Dedicated
        {
            get => this.dedicated;
            set => this.dedicated = value;
        }

        public ServerInfo.OSType OS
        {
            get => this.os;
            set => this.os = value;
        }

        public enum DedicatedType
        {
            NONE,
            LISTEN,
            DEDICATED,
            SOURCETV,
        }

        public enum OSType
        {
            NONE,
            WINDOWS,
            LINUX,
        }
    }
}
