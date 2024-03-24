﻿namespace OphiussaFramework.Models {
    public class PluginInfo {
        public string PluginName { get; set; }
        public string GameType   { get; set; }
        public string GameName   { get; set; }
        public string Version    { get; set; }
        public bool   Loaded     { get; set; }
    }
}