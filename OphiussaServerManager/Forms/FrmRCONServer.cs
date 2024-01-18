using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArkData;
using CoreRCON;
using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Components;

namespace OphiussaServerManager.Forms {
    public partial class FrmRconServer : Form {
        private readonly List<PlayerList>                         _playerLists = new List<PlayerList>();
        private readonly ConcurrentDictionary<string, PlayerInfo> _players     = new ConcurrentDictionary<string, PlayerInfo>();
        private          bool                                     _isConnected;
        private          PlayerListParameters                     _playerListParameters;
        private          RCON                                     _rcon;

        public FrmRconServer(Profile profile) {
            Profile = profile;
            InitializeComponent();

            InitConnection();

            try {
                UpdatePlayerDetailsAsync();
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
            }

            Text = "RCON - " + profile.Name;
        }

        private Profile Profile { get; }

        private async void PrintFirstLines() {
            try {
                string respnose = await _rcon.SendCommandAsync("getgamelog");
                if (respnose != "Server received, But no response!!") {
                    string[] response = respnose.Split('\r');
                    foreach (string res in response) txtChat.AppendTextWithTimeStamp(res.Replace("\n", ""), Color.Black);
                }

                SetStatus(true);
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                SetStatus(false);
            }
        }

        private async void UpdatePlayerDetailsAsync() {
            string        profileSavePath = Profile.GetProfileSavePath(Profile, Profile.InstallLocation, Profile.ArkConfiguration?.AlternateSaveDirectoryName);
            DataContainer dataContainer   = null;
            var           minValue        = DateTime.MinValue;

            try {
                dataContainer = await DataContainer.CreateAsync(profileSavePath, profileSavePath);
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                return;
            }

            await Task.Run(() => {
                               foreach (var player in dataContainer.Players) {
                                   string playerId = player.PlayerId;
                                   if (!string.IsNullOrWhiteSpace(playerId)) {
                                       PlayerInfo playerInfo;
                                       _players.TryGetValue(playerId, out playerInfo);
                                       playerInfo?.UpdatePlatformData(player);
                                   }
                               }
                           });

            try {
                var steam    = new SteamUtils(MainForm.Settings);
                var dateTime = await dataContainer.LoadSteamAsync(steam.SteamWebApiKey, 60);
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
            }

            int invalid = 0;
            int count   = dataContainer.Players.Count;
            foreach (var player1 in dataContainer.Players) {
                var playerData = player1;
                await Task.Run(async () => {
                                   string key = playerData.PlayerId;
                                   if (!string.IsNullOrWhiteSpace(key)) {
                                       var addValue = new PlayerInfo {
                                                                         PlayerId   = playerData.PlayerId,
                                                                         PlayerName = playerData.PlayerName,
                                                                         IsValid    = true
                                                                     };
                                       _players.AddOrUpdate(key, addValue, (k, v) => {
                                                                               v.PlayerName = playerData.PlayerName;
                                                                               v.IsValid    = true;
                                                                               return v;
                                                                           });
                                   }
                                   else {
                                       key = Path.GetFileNameWithoutExtension(playerData.Filename);
                                       if (!string.IsNullOrWhiteSpace(key)) {
                                           var addValue = new PlayerInfo {
                                                                             PlayerId   = key,
                                                                             PlayerName = "< corrupted profile >",
                                                                             IsValid    = false
                                                                         };
                                           _players.AddOrUpdate(key, addValue, (k, v) => {
                                                                                   v.PlayerName = "< corrupted profile >";
                                                                                   v.IsValid    = false;
                                                                                   invalid++;
                                                                                   return v;
                                                                               });
                                       }
                                       else {
                                           OphiussaLogger.Logger.Debug("UpdatePlayerDetailsAsync - Error: corrupted profile.\r\n" + playerData.Filename + ".");
                                       }
                                   }

                                   PlayerInfo player;
                                   if (!_players.TryGetValue(key, out player) || player == null)
                                       return;
                                   player.UpdateData(playerData);
                                   await TaskUtils.RunOnUIThreadAsync(() => {
                                                                          var   playerInfo3           = player;
                                                                          var   playerListParameters3 = _playerListParameters;
                                                                          bool? nullable4;
                                                                          if (playerListParameters3 == null)
                                                                              nullable4 = new bool?();
                                                                          else
                                                                              nullable4 = new bool?();
                                                                          //TODO:Cenas do server
                                                                          //Server server = playerListParameters3.Server;
                                                                          //if (server == null)
                                                                          //{
                                                                          //    nullable4 = new bool?();
                                                                          //}
                                                                          //else
                                                                          //{
                                                                          //    ServerProfile profile = server.Profile;
                                                                          //    if (profile == null)
                                                                          //    {
                                                                          //        nullable4 = new bool?();
                                                                          //    }
                                                                          //    else
                                                                          //    {
                                                                          //        PlayerUserList serverFilesAdmins = profile.ServerFilesAdmins;
                                                                          //        nullable4 = serverFilesAdmins != null ? new bool?(serverFilesAdmins.Any<PlayerUserItem>((Func<PlayerUserItem, bool>)(u => u.PlayerId.Equals(player.PlayerId, StringComparison.OrdinalIgnoreCase)))) : new bool?();
                                                                          //    }
                                                                          //}
                                                                          bool? nullable5 = nullable4;
                                                                          int   num3      = nullable5.GetValueOrDefault() ? 1 : 0;
                                                                          playerInfo3.IsAdmin = num3 != 0;
                                                                          var   playerInfo4           = player;
                                                                          var   playerListParameters4 = _playerListParameters;
                                                                          bool? nullable6;
                                                                          if (playerListParameters4 == null) {
                                                                              nullable5 = new bool?();
                                                                              nullable6 = nullable5;
                                                                          }
                                                                          else {
                                                                              nullable6 = new bool?();
                                                                              //TODO:Cenas do server
                                                                              //Server server = playerListParameters4.Server;
                                                                              //if (server == null)
                                                                              //{
                                                                              //    nullable5 = new bool?();
                                                                              //    nullable6 = nullable5;
                                                                              //}
                                                                              //else
                                                                              //{
                                                                              //    ServerProfile profile = server.Profile;
                                                                              //    if (profile == null)
                                                                              //    {
                                                                              //        nullable5 = new bool?();
                                                                              //        nullable6 = nullable5;
                                                                              //    }
                                                                              //    else
                                                                              //    {
                                                                              //        PlayerUserList filesWhitelisted = profile.ServerFilesWhitelisted;
                                                                              //        if (filesWhitelisted == null)
                                                                              //        {
                                                                              //            nullable5 = new bool?();
                                                                              //            nullable6 = nullable5;
                                                                              //        }
                                                                              //        else
                                                                              //            nullable6 = new bool?(filesWhitelisted.Any<PlayerUserItem>((Func<PlayerUserItem, bool>)(u => u.PlayerId.Equals(player.PlayerId, StringComparison.OrdinalIgnoreCase))));
                                                                              //    }
                                                                              //}
                                                                          }

                                                                          nullable5 = nullable6;
                                                                          int num4 = nullable5.GetValueOrDefault() ? 1 : 0;
                                                                          playerInfo4.IsWhitelisted = num4 != 0;
                                                                      });
                               });
            }

            txtProfiles.Text = dataContainer.Players.Count.ToString();
            txtInvalid.Text  = invalid.ToString();
        }

        private async void InitConnection() {
            try {
                _rcon = new RCON(IPAddress.Parse(Profile.ArkConfiguration.LocalIp), ushort.Parse(Profile.ArkConfiguration.RconPort), Profile.ArkConfiguration.ServerAdminPassword);
                await _rcon.ConnectAsync();
                txtChat.AppendTextWithTimeStamp("Connection established.", Color.Orange);
                SetStatus(true);
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                txtChat.AppendTextWithTimeStamp("Error:" + ex.Message, Color.Red);
                SetStatus(false);
            }
        }

        private void SetStatus(bool status) {
            if (status) {
                _isConnected        = true;
                lblStatus.Text      = "Connected";
                lblStatus.ForeColor = Color.Green;
            }
            else {
                _isConnected        = false;
                lblStatus.Text      = "Disconnected";
                lblStatus.ForeColor = Color.Red;
            }
        }

        private void RCONServer_Load(object sender, EventArgs e) {
            txtChat.AppendText("Enter commands or chat into the box at the bottom.",                              Color.Green);
            txtChat.AppendText("In Command mode, everything you enter will be a normal admin command.",           Color.Green);
            txtChat.AppendText("In Broadcast mode, everything you enter will be a global broadcast.",             Color.Green);
            txtChat.AppendText("You may always prefix a command with / to be treated as a command and not chat.", Color.Green);
            txtChat.AppendText("Right click on players in the list to access player commands.",                   Color.Green);
            txtChat.AppendText("Type /help to get help.",                                                         Color.Green);
            txtChat.AppendText("",                                                                                Color.Green);
            comboBox1.SelectedIndex = 0;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == '\r')
                if (textBox1.Text.ToLower() == "/help") {
                    txtChat.AppendText("Known commands:",                                                                                                                                                                                                 Color.Green);
                    txtChat.AppendText("    AllowPlayerToJoinNoCheck <steam id> - Adds the specified player to the server's whitelist.",                                                                                                                  Color.Green);
                    txtChat.AppendText("    BanPlayer <steam id> - Add the specified player to the server's banned list.",                                                                                                                                Color.Green);
                    txtChat.AppendText("    Broadcast <message> - Broadcast a message to all players on the server.",                                                                                                                                     Color.Green);
                    txtChat.AppendText("    DestroyAll <class name> - Destroys ALL creatures of the specified class.",                                                                                                                                    Color.Green);
                    txtChat.AppendText("    DestroyAllEnemies - Destroys all non-player creatures on the map, including tamed creatures. This does not prevent new ones from spawning as usual.",                                                         Color.Green);
                    txtChat.AppendText("    DestroyStructures - Destroys all structures owned by all players on the map.",                                                                                                                                Color.Green);
                    txtChat.AppendText("    DestroyWildDinos - Destroys all untamed creatures on the map. Useful for helping newly-released creatures to spawn.",                                                                                         Color.Green);
                    txtChat.AppendText("    DisallowPlayerToJoinNoCheck <steam id> - Removes the specified player from the server's whitelist.",                                                                                                          Color.Green);
                    txtChat.AppendText("    DoExit - Shuts down the server as soon as possible.",                                                                                                                                                         Color.Green);
                    txtChat.AppendText("    GetChat - Returns the latest chat buffer (the same amount that the clients see).",                                                                                                                            Color.Green);
                    txtChat.AppendText("    GiveItemNumToPlayer <player id> <item id> <quantity> <quality> <blueprint> - Adds the specified item to the player's inventory (or its blueprint) in the specified quantity and with the specified quality.", Color.Green);
                    txtChat.AppendText("    GiveExpToPlayer <player id> <how much> <from tribe share> <prevent sharing with tribe> - Gives the specified player the specified amount of experience points.",                                              Color.Green);
                    txtChat.AppendText("    KickPlayer <steam id> - Forcibly disconnect the specified player from the server.",                                                                                                                           Color.Green);
                    txtChat.AppendText("    KillPlayer <player id> - Kills the specified player.",                                                                                                                                                        Color.Green);
                    txtChat.AppendText("    ListPlayers - List all connected players and their Steam IDs.",                                                                                                                                               Color.Green);
                    txtChat.AppendText("    PlayersOnly - Stops all creature movement in the game world and halts crafting. Players can still move normally. Repeat the command to disable its effects.",                                                 Color.Green);
                    txtChat.AppendText("    RenamePlayer \"<player>\" <new name> - Renames the player specified by their in-game string name.",                                                                                                           Color.Green);
                    txtChat.AppendText("    RenameTribe \"<tribe>\" <new name> - Renames the tribe specified by it's string name.",                                                                                                                       Color.Green);
                    txtChat.AppendText("    SaveWorld - Forces the server to save the game world to disk in its current state.",                                                                                                                          Color.Green);
                    txtChat.AppendText("    ServerChat <message> - Sends a chat message to all currently connected players.",                                                                                                                             Color.Green);
                    txtChat.AppendText("    ServerChatTo \"<steam id>\" <message> - Sends a direct chat message to the player specified by their int64 encoded steam id.",                                                                                Color.Green);
                    txtChat.AppendText("    ServerChatToPlayer \"<player>\" <message> - Sends a direct chat message to the player specified by their in-game player name.",                                                                               Color.Green);
                    txtChat.AppendText("    SetMessageOfTheDay <message> - Sets the server's 'message of the day', displayed to players when they connect to it.",                                                                                        Color.Green);
                    txtChat.AppendText("    SetTimeOfDay <hour>:<minute>[:<second>] - Sets the game world's time of day to the specified time.",                                                                                                          Color.Green);
                    txtChat.AppendText("    ShowMessageOfTheDay - Displays the message of the day.",                                                                                                                                                      Color.Green);
                    txtChat.AppendText("    Slomo <factor> - Sets the game speed multiplier. Lower values slow time, change back to 1 to set back to normal.",                                                                                            Color.Green);
                    txtChat.AppendText("    UnBanPlayer <steam id> - Remove the specified player from the server's banned list.",                                                                                                                         Color.Green);
                    txtChat.AppendText("where:",                                                                                                                                                                                                          Color.Green);
                    txtChat.AppendText("    <player> specifies the character name of the player",                                                                                                                                                         Color.Green);
                    txtChat.AppendText("    <steam id> is the long numerical id of the player",                                                                                                                                                           Color.Green);
                    txtChat.AppendText("    <player id> specifies the ingame UE4 ID of the player",                                                                                                                                                       Color.Green);
                }
        }

        private async void timerPlayers_Tick(object sender, EventArgs e) {
            try {
                timerPlayers.Enabled = false;
                _playerLists.Clear();
                string respnose = await _rcon.SendCommandAsync("ListPlayers");
                if (respnose == "No Players Connected") {
                    lblPlayers.Text = $"0/{Profile.ArkConfiguration.MaxPlayers}";
                }
                else {
                    string[] players = respnose.Split('\r');

                    foreach (string player in players)
                        if (player != "") {
                            string[] data = player.Replace("\n", "").Replace(",", "").Replace(".", "").Split(' ');

                            //listBox1.Items.Add(data[1]);
                            _playerLists.Add(new PlayerList { PlayerNum = data[0], Name = data[1], SteamId = data[2] });
                        }

                    //lbPlayers.Items.Clear();
                    var l = new List<ExListBoxItem>();
                    foreach (var player in _playerLists) l.Add(new ExListBoxItem(player.PlayerNum, player.Name, "Steam ID:" + player.SteamId, player));
                    lbPlayers.DataSource    = l;
                    lbPlayers.ValueMember   = "SteamID";
                    lbPlayers.DisplayMember = "Name";
                    //TODO: link to players from disk files
                    lblPlayers.Text = $"{lbPlayers.Items.Count}/{Profile.ArkConfiguration.MaxPlayers}";
                }

                SetStatus(true);
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                SetStatus(false);
            }

            timerPlayers.Enabled = true;
        }

        private async void timersChat_Tick(object sender, EventArgs e) {
            try {
                timersChat.Enabled = false;
                string respnose = await _rcon.SendCommandAsync("GetChat");
                if (respnose != "Server received, But no response!!") {
                    string[] response = respnose.Split('\r');
                    foreach (string res in response) txtChat.AppendTextWithTimeStamp(res.Replace("\n", ""), Color.Black);
                }

                SetStatus(true);
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                SetStatus(false);
            }

            timersChat.Enabled = true;
        }

        private void label3_Click(object sender, EventArgs e) {
        }

        private void chatToPlayerToolStripMenuItem_Click(object sender, EventArgs e) {
        }

        private void renamePlayerToolStripMenuItem_Click(object sender, EventArgs e) {
        }

        private void renameTribeToolStripMenuItem_Click(object sender, EventArgs e) {
        }

        private void viewProfileToolStripMenuItem_Click(object sender, EventArgs e) {
        }

        private void viewTribeToolStripMenuItem_Click(object sender, EventArgs e) {
        }

        private void copyIDToolStripMenuItem_Click(object sender, EventArgs e) {
        }

        private void copyPlayerIDToolStripMenuItem_Click(object sender, EventArgs e) {
        }

        private void timerConnection_Tick(object sender, EventArgs e) {
            if (!_isConnected) InitConnection();
        }

        private void timerUpdatePlayersFromDisk_Tick(object sender, EventArgs e) {
            try {
                UpdatePlayerDetailsAsync();
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
            }
        }

        private void viewLogsToolStripMenuItem_Click(object sender, EventArgs e) {
        }

        private async void confirmToolStripMenuItem1_Click(object sender, EventArgs e) {
            try {
                string respnose = await _rcon.SendCommandAsync("SaveWorld");
                if (respnose != "Server received, But no response!!") {
                    string[] response = respnose.Split('\r');
                    foreach (string res in response) txtChat.AppendTextWithTimeStamp(res.Replace("\n", ""), Color.Black);
                }

                SetStatus(true);
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                SetStatus(false);
            }
        }

        private async void confirmToolStripMenuItem2_Click(object sender, EventArgs e) {
            try {
                string respnose = await _rcon.SendCommandAsync("DestroyWildDinos");
                if (respnose != "Server received, But no response!!") {
                    string[] response = respnose.Split('\r');
                    foreach (string res in response) txtChat.AppendTextWithTimeStamp(res.Replace("\n", ""), Color.Black);
                }

                SetStatus(true);
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                SetStatus(false);
            }
        }
    }
}