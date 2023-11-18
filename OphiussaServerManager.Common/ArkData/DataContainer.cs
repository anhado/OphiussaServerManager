using OphiussaServerManager.Common.Models;
using Newtonsoft.Json;
using SSQLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting;
using System.Threading.Tasks;
using PlayerInfo = SSQLib.PlayerInfo;

namespace OphiussaServerManager.Common
{
    public class DataContainer
    {
        private const int MAX_STEAM_IDS = 100;
        private const int MAX_INVALID_COUNT = 10;

        public static async Task<DataContainer> CreateAsync(
          string playerFileFolder,
          string tribeFileFolder)
        {
            IEnumerable<string> strings = (IEnumerable<string>)null;
            IEnumerable<string> tribeFiles = (IEnumerable<string>)null;
            if (Directory.Exists(playerFileFolder))
                strings = ((IEnumerable<string>)Directory.GetFiles(playerFileFolder)).Where<string>((Func<string, bool>)(f => Path.GetFileNameWithoutExtension(f).StartsWith(DataFileDetails.PlayerFilePrefix) && Path.GetFileNameWithoutExtension(f).EndsWith(DataFileDetails.PlayerFileSuffix) && Path.GetExtension(f).Equals(DataFileDetails.PlayerFileExtension)));
            if (Directory.Exists(tribeFileFolder))
                tribeFiles = ((IEnumerable<string>)Directory.GetFiles(tribeFileFolder)).Where<string>((Func<string, bool>)(f => Path.GetFileNameWithoutExtension(f).StartsWith(DataFileDetails.TribeFilePrefix) && Path.GetFileNameWithoutExtension(f).EndsWith(DataFileDetails.TribeFileSuffix) && Path.GetExtension(f).Equals(DataFileDetails.TribeFileExtension)));
            DataContainer container = new DataContainer();
            if (strings != null)
            {
                foreach (string fileName in strings)
                {
                    List<PlayerData> playerDataList = container.Players;
                    playerDataList.Add(await Parser.ParsePlayerAsync(fileName));
                    playerDataList = (List<PlayerData>)null;
                }
            }
            if (tribeFiles != null)
            {
                foreach (string fileName in tribeFiles)
                {
                    List<TribeData> tribeDataList = container.Tribes;
                    tribeDataList.Add(await Parser.ParseTribeAsync(fileName));
                    tribeDataList = (List<TribeData>)null;
                }
            }
            container.LinkPlayerTribe();
            DataContainer async = container;
            tribeFiles = (IEnumerable<string>)null;
            container = (DataContainer)null;
            return async;
        }

        public async Task<DateTime> LoadSteamAsync(string apiKey, int steamUpdateInterval = 0)
        {
            DateTime lastSteamUpdateUtc = DateTime.UtcNow;
            int startIndex = 0;
            string[] playerSteamIds;
            int steamIdsCount;
            for (playerSteamIds = this.Players.Where<PlayerData>((Func<PlayerData, bool>)(p => p.LastPlatformUpdateUtc.AddMinutes((double)steamUpdateInterval) < DateTime.UtcNow && p.NoUpdateCount < 10)).Select<PlayerData, string>((Func<PlayerData, string>)(p => p.PlayerId)).ToArray<string>(); startIndex < ((IEnumerable<string>)playerSteamIds).Count<string>(); startIndex += steamIdsCount)
            {
                steamIdsCount = Math.Min(100, ((IEnumerable<string>)playerSteamIds).Count<string>() - startIndex);
                string str = string.Join(",", playerSteamIds, startIndex, steamIdsCount);
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.steampowered.com/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage async = await client.GetAsync(string.Format("ISteamUser/GetPlayerSummaries/v0002/?key={0}&steamids={1}", (object)apiKey, (object)str));
                    if (!async.IsSuccessStatusCode)
                        throw new WebException("The Steam API request was unsuccessful. Are you using a valid key?");
                    using (StreamReader reader = new StreamReader(await async.Content.ReadAsStreamAsync()))
                        this.LinkSteamProfiles(await reader.ReadToEndAsync(), lastSteamUpdateUtc, playerSteamIds);
                }
            }
            this.SteamLoaded = true;
            DateTime dateTime = lastSteamUpdateUtc;
            playerSteamIds = (string[])null;
            return dateTime;
        }

        public Task LoadOnlinePlayersAsync(string ipString, int port)
        {
            if (this.SteamLoaded)
                return Task.Run((Action)(() => this.LinkOnlinePlayers(ipString, port)));
            throw new Exception("The Steam user data should be loaded before the server status can be checked.");
        }

        public List<PlayerData> Players { get; set; }

        public List<TribeData> Tribes { get; set; }

        private bool SteamLoaded { get; set; }

        public DataContainer()
        {
            this.Players = new List<PlayerData>();
            this.Tribes = new List<TribeData>();
            this.SteamLoaded = false;
        }

        private void LinkOnlinePlayers(string ipString, int port)
        {
            try
            {
                IEnumerable<PlayerInfo> source = new SSQL().Players(new IPEndPoint(IPAddress.Parse(ipString), port)).OfType<PlayerInfo>();
                for (int i = 0; i < this.Players.Count; i++)
                {
                    PlayerInfo playerInfo = source.FirstOrDefault<PlayerInfo>((Func<PlayerInfo, bool>)(p => p.Name == this.Players[i].PlayerName));
                    this.Players[i].Online = playerInfo != null;
                }
            }
            catch (SSQLServerException ex)
            {
                //ServerException this was before
                throw new Exception("The connection to the server failed. Please check the configured IP address and port.");
            }
        }

        private void LinkPlayerTribe()
        {
            for (int index = 0; index < this.Players.Count; ++index)
            {
                PlayerData player = this.Players[index];
                player.OwnedTribes = this.Tribes.Where<TribeData>((Func<TribeData, bool>)(t =>
                {
                    int? ownerId = t.OwnerId;
                    long? nullable = ownerId.HasValue ? new long?((long)ownerId.GetValueOrDefault()) : new long?();
                    long characterId = player.CharacterId;
                    return nullable.GetValueOrDefault() == characterId & nullable.HasValue;
                })).ToList<TribeData>();
                player.Tribe = this.Tribes.FirstOrDefault<TribeData>((Func<TribeData, bool>)(t =>
                {
                    int id = t.Id;
                    int? tribeId = player.TribeId;
                    int valueOrDefault = tribeId.GetValueOrDefault();
                    return id == valueOrDefault & tribeId.HasValue;
                }));
            }
            for (int index = 0; index < this.Tribes.Count; ++index)
            {
                TribeData tribe = this.Tribes[index];
                tribe.Owner = this.Players.FirstOrDefault<PlayerData>((Func<PlayerData, bool>)(p =>
                {
                    long characterId = p.CharacterId;
                    int? ownerId = tribe.OwnerId;
                    long? nullable = ownerId.HasValue ? new long?((long)ownerId.GetValueOrDefault()) : new long?();
                    long valueOrDefault = nullable.GetValueOrDefault();
                    return characterId == valueOrDefault & nullable.HasValue;
                }));
                tribe.Players = (ICollection<PlayerData>)this.Players.Where<PlayerData>((Func<PlayerData, bool>)(p =>
                {
                    int? tribeId = p.TribeId;
                    int id = tribe.Id;
                    return tribeId.GetValueOrDefault() == id & tribeId.HasValue;
                })).ToList<PlayerData>();
            }
        }

        private void LinkSteamProfiles(
          string jsonString,
          DateTime lastSteamUpdateUtc,
          string[] playerSteamIds)
        {
            List<SteamProfile> profiles = JsonConvert.DeserializeObject<SteamResponse<SteamProfile>>(jsonString).response.players;
            for (int i = 0; i < profiles.Count; i++)
            {
                PlayerData playerData = this.Players.FirstOrDefault<PlayerData>((Func<PlayerData, bool>)(p => p.PlayerId == profiles[i].steamid));
                if (playerData != null)
                {
                    playerData.PlayerName = profiles[i].personaname;
                    playerData.LastPlatformUpdateUtc = lastSteamUpdateUtc;
                }
            }
            for (int i = 0; i < playerSteamIds.Length; i++)
            {
                PlayerData playerData = this.Players.FirstOrDefault<PlayerData>((Func<PlayerData, bool>)(p => p.PlayerId == playerSteamIds[i]));
                if (playerData != null && playerData.LastPlatformUpdateUtc == DateTime.MinValue)
                    playerData.NoUpdateCount = Math.Min(10, playerData.NoUpdateCount + 1);
            }
        }

        public static DataContainer Create(string playerFileFolder, string tribeFileFolder)
        {
            IEnumerable<string> strings1 = (IEnumerable<string>)null;
            IEnumerable<string> strings2 = (IEnumerable<string>)null;
            if (Directory.Exists(playerFileFolder))
                strings1 = ((IEnumerable<string>)Directory.GetFiles(playerFileFolder)).Where<string>((Func<string, bool>)(f => Path.GetFileNameWithoutExtension(f).StartsWith(DataFileDetails.PlayerFilePrefix) && Path.GetFileNameWithoutExtension(f).EndsWith(DataFileDetails.PlayerFileSuffix) && Path.GetExtension(f).Equals(DataFileDetails.PlayerFileExtension)));
            if (Directory.Exists(tribeFileFolder))
                strings2 = ((IEnumerable<string>)Directory.GetFiles(tribeFileFolder)).Where<string>((Func<string, bool>)(f => Path.GetFileNameWithoutExtension(f).StartsWith(DataFileDetails.TribeFilePrefix) && Path.GetFileNameWithoutExtension(f).EndsWith(DataFileDetails.TribeFileSuffix) && Path.GetExtension(f).Equals(DataFileDetails.TribeFileExtension)));
            DataContainer dataContainer = new DataContainer();
            if (strings1 != null)
            {
                foreach (string fileName in strings1)
                    dataContainer.Players.Add(Parser.ParsePlayer(fileName));
            }
            if (strings2 != null)
            {
                foreach (string fileName in strings2)
                    dataContainer.Tribes.Add(Parser.ParseTribe(fileName));
            }
            dataContainer.LinkPlayerTribe();
            return dataContainer;
        }

        public DateTime LoadSteam(string apiKey, int steamUpdateInterval = 0)
        {
            DateTime utcNow = DateTime.UtcNow;
            int startIndex = 0;
            int count;
            for (string[] array = this.Players.Where<PlayerData>((Func<PlayerData, bool>)(p => p.LastPlatformUpdateUtc.AddMinutes((double)steamUpdateInterval) < DateTime.UtcNow && p.NoUpdateCount < 10)).Select<PlayerData, string>((Func<PlayerData, string>)(p => p.PlayerId)).ToArray<string>(); startIndex < ((IEnumerable<string>)array).Count<string>(); startIndex += count)
            {
                count = Math.Min(100, ((IEnumerable<string>)array).Count<string>() - startIndex);
                string str = string.Join(",", array, startIndex, count);
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("https://api.steampowered.com/");
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage result = httpClient.GetAsync(string.Format("ISteamUser/GetPlayerSummaries/v0002/?key={0}&steamids={1}", (object)apiKey, (object)str)).Result;
                    if (!result.IsSuccessStatusCode)
                        throw new WebException("The Steam API request was unsuccessful. Are you using a valid key?");
                    using (StreamReader streamReader = new StreamReader(result.Content.ReadAsStreamAsync().Result))
                        this.LinkSteamProfiles(streamReader.ReadToEnd(), utcNow, array);
                }
            }
            this.SteamLoaded = true;
            return utcNow;
        }

        public void LoadOnlinePlayers(string ipString, int port)
        {
            if (!this.SteamLoaded)
                throw new Exception("The Steam user data should be loaded before the server status can be checked.");
            this.LinkOnlinePlayers(ipString, port);
        }
    }
}
