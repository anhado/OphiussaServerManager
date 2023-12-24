using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting;
using ArkData.Models;
using Newtonsoft.Json;
using SSQLib;

/// <summary>
/// The container for the data.
/// </summary>
namespace ArkData {
    public partial class DataContainer {
        private const int MaxSteamIds     = 100;
        private const int MaxInvalidCount = 10;

        /// <summary>
        ///     Constructs the DataContainer.
        /// </summary>
        public DataContainer() {
            Players     = new List<PlayerData>();
            Tribes      = new List<TribeData>();
            SteamLoaded = false;
        }

        /// <summary>
        ///     A list of all players registered on the server.
        /// </summary>
        public List<PlayerData> Players { get; set; }

        /// <summary>
        ///     A list of all tribes registered on the server.
        /// </summary>
        public List<TribeData> Tribes { get; set; }

        /// <summary>
        ///     Indicates whether the steam user data has been loaded.
        /// </summary>
        private bool SteamLoaded { get; set; }

        /// <summary>
        ///     Links the online players, to the player profiles.
        /// </summary>
        /// <param name="ipString">The server ip address.</param>
        /// <param name="port">The Steam query port.</param>
        private void LinkOnlinePlayers(string ipString, int port) {
            try {
                var online = new Ssql().Players(new IPEndPoint(IPAddress.Parse(ipString), port)).OfType<PlayerInfo>();

                for (int i = 0; i < Players.Count; i++) {
                    var onlinePlayer = online.FirstOrDefault(p => p.Name == Players[i].PlayerName);
                    Players[i].Online = onlinePlayer != null;
                }
            }
            catch (SsqlServerException) {
                throw new ServerException("The connection to the server failed. Please check the configured IP address and port.");
            }
        }

        /// <summary>
        ///     Links the players to their tribes and the tribes to the players.
        /// </summary>
        private void LinkPlayerTribe() {
            for (int i = 0; i < Players.Count; i++) {
                var player = Players[i];
                player.OwnedTribes = Tribes.Where(t => t.OwnerId     == player.CharacterId).ToList();
                player.Tribe       = Tribes.FirstOrDefault(t => t.Id == player.TribeId);
            }

            for (int i = 0; i < Tribes.Count; i++) {
                var tribe = Tribes[i];
                tribe.Owner   = Players.FirstOrDefault(p => p.CharacterId == tribe.OwnerId);
                tribe.Players = Players.Where(p => p.TribeId              == tribe.Id).ToList();
            }
        }

        /// <summary>
        ///     Deserializes JSON from Steam API and links Steam profile to player profile.
        /// </summary>
        /// <param name="jsonString">The JSON data string.</param>
        private void LinkSteamProfiles(string jsonString, DateTime lastSteamUpdateUtc, string[] playerSteamIds) {
            var profiles = JsonConvert.DeserializeObject<SteamResponse<SteamProfile>>(jsonString).Response.Players;

            for (int i = 0; i < profiles.Count; i++) {
                var player = Players.FirstOrDefault(p => p.PlayerId == profiles[i].Steamid);
                if (player != null) {
                    player.PlayerName            = profiles[i].Personaname;
                    player.LastPlatformUpdateUtc = lastSteamUpdateUtc;
                }
            }

            for (int i = 0; i < playerSteamIds.Length; i++) {
                var player                                                                                    = Players.FirstOrDefault(p => p.PlayerId == playerSteamIds[i]);
                if (player != null && player.LastPlatformUpdateUtc == DateTime.MinValue) player.NoUpdateCount = Math.Min(MaxInvalidCount, player.NoUpdateCount + 1);
            }
        }
    }
}