﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common.Models
{
    public class SteamUserDetail
    {
        public string steamid { get; set; }

        public int communityvisibilitystate { get; set; }

        public int profilestate { get; set; }

        public string personaname { get; set; }

        public int lastlogoff { get; set; }

        public string profileurl { get; set; }

        public string avatar { get; set; }

        public string avatarmedium { get; set; }

        public string avatarfull { get; set; }

        public int personastate { get; set; }
    }
}
