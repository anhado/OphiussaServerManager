using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace QueryMaster {
    /// <summary>
    ///     Represents the connected server.Provides methods to query,listen to server logs and control the server
    /// </summary>
    public abstract class Server : IDisposable {
        private readonly IPEndPoint _serverEndPoint;
        private readonly EngineType _type;
        private          bool       _isDisposed;
        private          bool       _isPlayerChallengeId;
        private          bool       _isRuleChallengeId;
        private          long       _latency;
        private          Logs       _logs;
        private          byte[]     _playerChallengeId;
        private          byte[]     _ruleChallengeId;
        internal         Rcon       RConObj;
        internal         UdpQuery   Socket;

        internal Server(IPEndPoint address, EngineType type, bool? isObsolete, int sendTimeOut, int receiveTimeOut) {
            _serverEndPoint = address;
            Socket          = new UdpQuery(address, sendTimeOut, receiveTimeOut);
            _isDisposed     = false;
            _type           = type;
            if (isObsolete == null)
                try {
                    if (Socket.GetResponse(QueryMsg.ObsoleteInfoQuery, _type)[0] == 0x6D)
                        IsObsolete = true;
                }
                catch (SocketException e) {
                    if (e.ErrorCode == 10060)
                        IsObsolete = false;
                }
            else
                IsObsolete = isObsolete == true ? true : false;
        }

        /// <summary>
        ///     Returns true if server replies only to half life protocol messages.
        /// </summary>
        public bool IsObsolete { get; }

        /// <summary>
        ///     Disposes all the resources used by the server object
        /// </summary>
        public void Dispose() {
            if (_isDisposed)
                return;
            if (Socket != null)
                Socket.Dispose();
            if (RConObj != null)
                RConObj.Dispose();
            if (_logs != null)
                _logs.Dispose();
            _isDisposed = true;
        }

        /// <summary>
        ///     Retrieves information about the server
        /// </summary>
        /// <returns>Instance of ServerInfo class</returns>
        public virtual ServerInfo GetInfo(byte[] data = null) {
            // If we already have pre-formed request in data parameter, we will populate Query with it.
            byte[] query;

            if (data != null && data.Length > 0) {
                query = data;
            }
            else {
                if (IsObsolete)
                    query = QueryMsg.ObsoleteInfoQuery;
                else
                    query = QueryMsg.InfoQuery;
            }

            var    sw       = Stopwatch.StartNew();
            byte[] recvData = Socket.GetResponse(query, _type);
            sw.Stop();
            _latency = sw.ElapsedMilliseconds;

            try {
                // first, let's check if we have a challenge received instead of S2A_INFO_SRC
                // according to https://steamcommunity.com/discussions/forum/14/2974028351344359625/
                // server can reply with S2C_CHALLENGE in form of 0x41(4 byte) if it suspects client
                // in IP spoofing

                if (recvData[0] == 0x41) {
                    // we will get current Query and append recvData to its end. Skipping 0x41 and 
                    // honoring the trailing 0x00 at the Query end

                    byte[] signedQuery = new byte[query.Length + (recvData.Length - 1)];
                    query.CopyTo(signedQuery, 0);

                    for (int bId = recvData.Length - 1; bId > 0; bId--) signedQuery[signedQuery.Length - bId] = recvData[recvData.Length - bId];

                    // finally, we are calling GetInfo() recursively with our new signed query
                    // to get pass the S2C_CHALLENGE
                    return GetInfo(signedQuery);
                }

                switch (recvData[0]) {
                    case 0x49:
                        return Current(recvData);
                    case 0x6D:
                        return Obsolete(recvData);
                    default:
                        throw new InvalidHeaderException("packet header is not valid");
                }
            }
            catch (Exception e) {
                e.Data.Add("ReceivedData", recvData);
                throw;
            }
        }

        private ServerInfo Current(byte[] data) {
            var parser = new Parser(data);
            if (parser.ReadByte() != (byte)ResponseMsgHeader.A2SInfo)
                throw new InvalidHeaderException("A2S_INFO message header is not valid");
            var server = new ServerInfo();
            server.IsObsolete  = false;
            server.Protocol    = parser.ReadByte();
            server.Name        = parser.ReadString();
            server.Map         = parser.ReadString();
            server.Directory   = parser.ReadString();
            server.Description = parser.ReadString();
            server.Id          = parser.ReadShort();
            server.Players     = parser.ReadByte();
            server.MaxPlayers  = parser.ReadByte();
            server.Bots        = parser.ReadByte();
            server.ServerType = new Func<string>(() => {
                                                     switch ((char)parser.ReadByte()) {
                                                         case 'l': return "Listen";
                                                         case 'd': return "Dedicated";
                                                         case 'p': return "SourceTV";
                                                     }

                                                     return "";
                                                 })();
            server.Environment = new Func<string>(() => {
                                                      switch ((char)parser.ReadByte()) {
                                                          case 'l': return "Linux";
                                                          case 'w': return "Windows";
                                                          case 'm': return "Mac";
                                                      }

                                                      return "";
                                                  })();
            server.IsPrivate = Convert.ToBoolean(parser.ReadByte());
            server.IsSecure  = Convert.ToBoolean(parser.ReadByte());
            if (server.Id >= 2400 && server.Id <= 2412) {
                var ship = new TheShip();
                switch (parser.ReadByte()) {
                    case 0:
                        ship.Mode = "Hunt";
                        break;
                    case 1:
                        ship.Mode = "Elimination";
                        break;
                    case 2:
                        ship.Mode = "Duel";
                        break;
                    case 3:
                        ship.Mode = "Deathmatch";
                        break;
                    case 4:
                        ship.Mode = "VIP Team";
                        break;
                    case 5:
                        ship.Mode = "Team Elimination";
                        break;
                    default:
                        ship.Mode = "";
                        break;
                }

                ship.Witnesses  = parser.ReadByte();
                ship.Duration   = parser.ReadByte();
                server.ShipInfo = ship;
            }

            server.GameVersion = parser.ReadString();

            if (parser.HasUnParsedBytes) {
                byte edf  = parser.ReadByte();
                var  info = new ExtraInfo();
                info.Port    = (edf & 0x80) > 0 ? parser.ReadShort() : (short)0;
                info.SteamId = (edf & 0x10) > 0 ? parser.ReadInt() : 0;
                if ((edf & 0x40) > 0)
                    info.SpecInfo = new SourceTvInfo { Port = parser.ReadShort(), Name = parser.ReadString() };
                info.Keywords = (edf & 0x20) > 0 ? parser.ReadString() : string.Empty;
                info.GameId   = (edf & 0x10) > 0 ? parser.ReadInt() : 0;
                server.Extra  = info;
            }

            server.Address = Socket.Address.Address + ":" + Socket.Address.Port;
            server.Ping    = _latency;
            return server;
        }

        private ServerInfo Obsolete(byte[] data) {
            var parser = new Parser(data);
            if (parser.ReadByte() != (byte)ResponseMsgHeader.A2SInfoObsolete)
                throw new InvalidHeaderException("A2S_INFO(obsolete) message header is not valid");
            var server = new ServerInfo();
            server.IsObsolete = true;
            server.Address    = parser.ReadString();
            server.Name       = parser.ReadString();
            server.Map        = parser.ReadString();
            server.Directory  = parser.ReadString();
            server.Id         = Util.GetGameId(parser.ReadString());
            server.Players    = parser.ReadByte();
            server.MaxPlayers = parser.ReadByte();
            server.Protocol   = parser.ReadByte();
            server.ServerType = new Func<string>(() => {
                                                     switch ((char)parser.ReadByte()) {
                                                         case 'L': return "non-dedicated server";
                                                         case 'D': return "dedicated";
                                                         case 'P': return "HLTV server";
                                                     }

                                                     return "";
                                                 })();
            server.Environment = new Func<string>(() => {
                                                      switch ((char)parser.ReadByte()) {
                                                          case 'L': return "Linux";
                                                          case 'W': return "Windows";
                                                      }

                                                      return "";
                                                  })();
            server.IsPrivate = Convert.ToBoolean(parser.ReadByte());
            byte mod = parser.ReadByte();
            server.IsModded = mod > 0 ? true : false;
            if (server.IsModded) {
                var modinfo = new Mod();
                modinfo.Link         = parser.ReadString();
                modinfo.DownloadLink = parser.ReadString();
                parser.ReadByte(); //0x00
                modinfo.Version           = parser.ReadInt();
                modinfo.Size              = parser.ReadInt();
                modinfo.IsOnlyMultiPlayer = parser.ReadByte() > 0 ? true : false;
                modinfo.IsHalfLifeDll     = parser.ReadByte() < 0 ? true : false;
                server.ModInfo            = modinfo;
            }

            server.IsSecure    = Convert.ToBoolean(parser.ReadByte());
            server.Bots        = parser.ReadByte();
            server.GameVersion = "server is obsolete,does not provide this information";
            server.Ping        = _latency;
            return server;
        }

        /// <summary>
        ///     Retrieves information about the players currently on the server
        /// </summary>
        /// <returns>ReadOnlyCollection of Player instances</returns>
        public virtual ReadOnlyCollection<Player> GetPlayers() {
            byte[] recvData = null;

            if (IsObsolete) {
                recvData = Socket.GetResponse(QueryMsg.ObsoletePlayerQuery, _type);
            }
            else {
                if (_playerChallengeId == null) {
                    recvData = GetPlayerChallengeId();
                    if (_isPlayerChallengeId)
                        _playerChallengeId = recvData;
                }

                if (_isPlayerChallengeId)
                    recvData = Socket.GetResponse(Util.MergeByteArrays(QueryMsg.PlayerQuery, _playerChallengeId), _type);
            }

            try {
                var parser = new Parser(recvData);
                if (parser.ReadByte() != (byte)ResponseMsgHeader.A2SPlayer)
                    throw new InvalidHeaderException("A2S_PLAYER message header is not valid");
                int playerCount = parser.ReadByte();
                var players     = new List<Player>(playerCount);
                for (int i = 0; i < playerCount; i++) {
                    parser.ReadByte(); //index,always equal to 0
                    players.Add(new Player {
                                               Name  = parser.ReadString(),
                                               Score = parser.ReadInt(),
                                               Time  = TimeSpan.FromSeconds(parser.ReadFloat())
                                           });
                }

                if (playerCount == 1 && players[0].Name == "Max Players")
                    return null;
                return players.AsReadOnly();
            }
            catch (Exception e) {
                e.Data.Add("ReceivedData", recvData);
                throw;
            }
        }

        /// <summary>
        ///     Retrieves  server rules
        /// </summary>
        /// <returns>ReadOnlyCollection of Rule instances</returns>
        public ReadOnlyCollection<Rule> GetRules() {
            byte[] recvData = null;
            if (IsObsolete) {
                recvData = Socket.GetResponse(QueryMsg.ObsoleteRuleQuery, _type);
            }
            else {
                if (_ruleChallengeId == null) {
                    recvData = GetRuleChallengeId();
                    if (_isRuleChallengeId)
                        _ruleChallengeId = recvData;
                }

                if (_isRuleChallengeId)
                    recvData = Socket.GetResponse(Util.MergeByteArrays(QueryMsg.RuleQuery, _ruleChallengeId), _type);
            }

            try {
                var parser = new Parser(recvData);
                if (parser.ReadByte() != (byte)ResponseMsgHeader.A2SRules)
                    throw new InvalidHeaderException("A2S_RULES message header is not valid");

                int count = parser.ReadShort(); //number of rules
                var rules = new List<Rule>(count);
                for (int i = 0; i < count; i++) rules.Add(new Rule { Name = parser.ReadString(), Value = parser.ReadString() });
                return rules.AsReadOnly();
            }
            catch (Exception e) {
                e.Data.Add("ReceivedData", recvData);
                throw;
            }
        }

        private byte[] GetPlayerChallengeId() {
            byte[] recvBytes = null;
            byte   header    = 0;
            Parser parser    = null;
            recvBytes = Socket.GetResponse(QueryMsg.PlayerChallengeQuery, _type);
            try {
                parser = new Parser(recvBytes);
                header = parser.ReadByte();
                switch (header) {
                    case (byte)ResponseMsgHeader.A2SServerqueryGetchallenge:
                        _isPlayerChallengeId = true;
                        return parser.GetUnParsedData();
                    case (byte)ResponseMsgHeader.A2SPlayer:
                        _isPlayerChallengeId = false;
                        return recvBytes;
                    default: throw new InvalidHeaderException("A2S_SERVERQUERY_GETCHALLENGE message header is not valid");
                }
            }
            catch (Exception e) {
                e.Data.Add("ReceivedData", recvBytes);
                throw;
            }
        }

        private byte[] GetRuleChallengeId() {
            byte[] recvBytes = null;
            byte   header    = 0;
            Parser parser    = null;
            recvBytes = Socket.GetResponse(QueryMsg.RuleChallengeQuery, _type);
            try {
                parser = new Parser(recvBytes);
                header = parser.ReadByte();

                switch (header) {
                    case (byte)ResponseMsgHeader.A2SServerqueryGetchallenge:
                        _isRuleChallengeId = true;
                        return BitConverter.GetBytes(parser.ReadInt());
                    case (byte)ResponseMsgHeader.A2SRules:
                        _isRuleChallengeId = false;
                        return recvBytes;
                    default: throw new InvalidHeaderException("A2S_SERVERQUERY_GETCHALLENGE message header is not valid");
                }
            }
            catch (Exception e) {
                e.Data.Add("ReceivedData", recvBytes);
                throw;
            }
        }

        /// <summary>
        ///     Listen to server logs.
        /// </summary>
        /// <param name="port">Local port</param>
        /// <returns>Instance of Log class</returns>
        /// <remarks>Receiver's socket address must be added to server's logaddress list before listening</remarks>
        public Logs GetLogs(int port) {
            _logs = new Logs(_type, port, _serverEndPoint);
            return _logs;
        }

        /// <summary>
        ///     Gets valid rcon object that can be used to send rcon commands to server
        /// </summary>
        /// <param name="pass">Rcon password of server</param>
        /// <returns>Instance of Rcon class</returns>
        public abstract Rcon GetControl(string pass);

        /// <summary>
        ///     Gets Round-trip delay time
        /// </summary>
        /// <returns>Elapsed milliseconds</returns>
        public long Ping() {
            Stopwatch sw = null;
            if (IsObsolete) {
                sw = Stopwatch.StartNew();
                byte[] msg = Socket.GetResponse(QueryMsg.ObsoletePingQuery, _type);
                sw.Stop();
            }
            else {
                sw = Stopwatch.StartNew();
                byte[] msg = Socket.GetResponse(QueryMsg.InfoQuery, _type);
                sw.Stop();
            }

            return sw.ElapsedMilliseconds;
        }
    }
}