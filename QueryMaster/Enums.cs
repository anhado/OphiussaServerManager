namespace QueryMaster {
    /// <summary>
    ///     Specifies the type of engine used by server
    /// </summary>
    public enum EngineType {
        /// <summary>
        ///     Source Engine
        /// </summary>
        Source,

        /// <summary>
        ///     Gold Source Engine
        /// </summary>
        GoldSource
    }

    /// <summary>
    ///     Specifies the game
    /// </summary>
    public enum Game {
        //Gold Source Games
        /// <summary>
        ///     Counter-Strike
        /// </summary>
        CounterStrike = 10,

        /// <summary>
        ///     Team Fortress Classic
        /// </summary>
        TeamFortressClassic = 20,

        /// <summary>
        ///     Day Of Defeat
        /// </summary>
        DayOfDefeat = 30,

        /// <summary>
        ///     Deathmatch Classic
        /// </summary>
        DeathmatchClassic = 40,

        /// <summary>
        ///     Opposing Force
        /// </summary>
        OpposingForce = 50,

        /// <summary>
        ///     Ricochet
        /// </summary>
        Ricochet = 60,

        /// <summary>
        ///     Half-Life
        /// </summary>
        HalfLife = 70,

        /// <summary>
        ///     Condition Zero
        /// </summary>
        ConditionZero = 80,

        /// <summary>
        ///     CounterStrike 1.6 dedicated server
        /// </summary>
        CounterStrike16DedicatedServer = 90,

        /// <summary>
        ///     Condition Zero Deleted Scenes
        /// </summary>
        ConditionZeroDeletedScenes = 100,

        /// <summary>
        ///     Half-Life:Blue Shift
        /// </summary>
        HalfLifeBlueShift = 130,

        //Source Games
        /// <summary>
        ///     Half-Life 2
        /// </summary>
        HalfLife2 = 220,

        /// <summary>
        ///     Counter-Strike: Source
        /// </summary>
        CounterStrikeSource = 240,

        /// <summary>
        ///     Half-Life: Source
        /// </summary>
        HalfLifeSource = 280,

        /// <summary>
        ///     Day of Defeat: Source
        /// </summary>
        DayOfDefeatSource = 300,

        /// <summary>
        ///     Half-Life 2: Deathmatch
        /// </summary>
        HalfLife2Deathmatch = 320,

        /// <summary>
        ///     Half-Life 2: Lost Coast
        /// </summary>
        HalfLife2LostCoast = 340,

        /// <summary>
        ///     Half-Life Deathmatch: Source
        /// </summary>
        HalfLifeDeathmatchSource = 360,

        /// <summary>
        ///     Half-Life 2: Episode One
        /// </summary>
        HalfLife2EpisodeOne = 380,

        /// <summary>
        ///     Portal
        /// </summary>
        Portal = 400,

        /// <summary>
        ///     Half-Life 2: Episode Two
        /// </summary>
        HalfLife2EpisodeTwo = 420,

        /// <summary>
        ///     Team Fortress 2
        /// </summary>
        TeamFortress2 = 440,

        /// <summary>
        ///     Left 4 Dead
        /// </summary>
        Left4Dead = 500,

        /// <summary>
        ///     Left 4 Dead 2
        /// </summary>
        Left4Dead2 = 550,

        /// <summary>
        ///     Dota 2
        /// </summary>
        Dota2 = 570,

        /// <summary>
        ///     Portal 2
        /// </summary>
        Portal2 = 620,

        /// <summary>
        ///     Alien Swarm
        /// </summary>
        AlienSwarm = 630,

        /// <summary>
        ///     Counter-Strike: Global Offensive
        /// </summary>
        CounterStrikeGlobalOffensive = 1800,

        /// <summary>
        ///     SiN Episodes: Emergence
        /// </summary>
        SiNEpisodesEmergence = 1300,

        /// <summary>
        ///     Dark Messiah of Might and Magic
        /// </summary>
        DarkMessiahOfMightAndMagic = 2100,

        /// <summary>
        ///     Dark Messiah Might and Magic Multi-Player
        /// </summary>
        DarkMessiahMightAndMagicMultiPlayer = 2130,

        /// <summary>
        ///     The Ship
        /// </summary>
        TheShip = 2400,

        /// <summary>
        ///     Bloody Good Time
        /// </summary>
        BloodyGoodTime = 2450,

        /// <summary>
        ///     Vampire The Masquerade - Bloodlines
        /// </summary>
        VampireTheMasqueradeBloodlines = 2600,

        /// <summary>
        ///     Garry's Mod
        /// </summary>
        GarrysMod = 4000,

        /// <summary>
        ///     Zombie Panic! Source
        /// </summary>
        ZombiePanicSource = 17500,

        /// <summary>
        ///     Age of Chivalry
        /// </summary>
        AgeOfChivalry = 17510,

        /// <summary>
        ///     Synergy
        /// </summary>
        Synergy = 17520,

        /// <summary>
        ///     D.I.P.R.I.P.
        /// </summary>
        Diprip = 17530,

        /// <summary>
        ///     Eternal Silence
        /// </summary>
        EternalSilence = 17550,

        /// <summary>
        ///     Pirates, Vikings, and Knights II
        /// </summary>
        PiratesVikingsAndKnightsIi = 17570,

        /// <summary>
        ///     Dystopia
        /// </summary>
        Dystopia = 17580,

        /// <summary>
        ///     Insurgency
        /// </summary>
        Insurgency = 17700,

        /// <summary>
        ///     Nuclear Dawn
        /// </summary>
        NuclearDawn = 17710,

        /// <summary>
        ///     Smashball
        /// </summary>
        Smashball = 17730
    }

    /// <summary>
    ///     Specifies the Region
    /// </summary>
    public enum Region : byte {
        /// <summary>
        ///     US East coast
        /// </summary>
        UsEastCoast,

        /// <summary>
        ///     US West coast
        /// </summary>
        UsWestCoast,

        /// <summary>
        ///     South America
        /// </summary>
        SouthAmerica,

        /// <summary>
        ///     Europe
        /// </summary>
        Europe,

        /// <summary>
        ///     Asia
        /// </summary>
        Asia,

        /// <summary>
        ///     Australia
        /// </summary>
        Australia,

        /// <summary>
        ///     Middle East
        /// </summary>
        MiddleEast,

        /// <summary>
        ///     Africa
        /// </summary>
        Africa,

        /// <summary>
        ///     Rest of the world
        /// </summary>
        RestOfTheWorld = 0xFF
    }

    internal enum SocketType {
        Udp,
        Tcp
    }

    internal enum ResponseMsgHeader : byte {
        A2SInfo                    = 0x49,
        A2SInfoObsolete            = 0x6D,
        A2SPlayer                  = 0x44,
        A2SRules                   = 0x45,
        A2SServerqueryGetchallenge = 0x41
    }

    //Used in Source Rcon
    internal enum PacketId {
        Empty   = 10,
        ExecCmd = 11
    }

    internal enum PacketType {
        Auth         = 3,
        AuthResponse = 2,
        Exec         = 2,
        ExecResponse = 0
    }
}