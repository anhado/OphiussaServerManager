namespace SSQLib
{
    public class PlayerInfo
    {
        private string name = "";
        private int index = -9999;
        private int kills = -9999;
        private int deaths = -9999;
        private int score = -9999;
        private int ping = -9999;
        private int rate = -9999;
        private float time;

        public string Name
        {
            get => this.name;
            set => this.name = value;
        }

        public int Kills
        {
            get => this.kills;
            set => this.kills = value;
        }

        public int Deaths
        {
            get => this.deaths;
            set => this.deaths = value;
        }

        public int Score
        {
            get => this.score;
            set => this.score = value;
        }

        public int Ping
        {
            get => this.ping;
            set => this.ping = value;
        }

        public int Rate
        {
            get => this.rate;
            set => this.rate = value;
        }

        public int Index
        {
            get => this.index;
            set => this.index = value;
        }

        public float Time
        {
            get => this.time;
            set => this.time = value;
        }
    }
}
