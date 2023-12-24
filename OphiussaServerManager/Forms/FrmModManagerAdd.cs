using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models.SupportedServers;

namespace OphiussaServerManager.Forms {
    public partial class FrmModManagerAdd : Form {
        private readonly List<ModListDetails> _allMods = new List<ModListDetails>();
        private          FrmModManager        _frmParent;
        private          Profile              _profile1;

        public FrmModManagerAdd() {
            InitializeComponent();
        }

        internal void LoadAllMods(Profile profile, FrmModManager owner) {
            _frmParent = owner;
            _profile1  = profile;
            var    curseForgeUtils       = new CurseForgeUtils(MainForm.Settings);
            var    steamUtils            = new SteamUtils(MainForm.Settings);
            var    curseForgeFileDetails = new CurseForgeFileDetailResponse();
            var    steamFileDetails      = new WorkshopFileDetailResponse();
            string cacheFolder           = Path.Combine(MainForm.Settings.DataFolder, "cache", profile.Type.KeyName);

            modListDetailsBindingSource.Clear();
            switch (profile.Type.ModsSource) {
                case ModSource.SteamWorkshop:

                    steamFileDetails = steamUtils.GetSteamModDetails(profile.Type.ModAppId.ToString());
                    if (steamFileDetails != null)
                        if (steamFileDetails.Publishedfiledetails.Count > 0) {
                            int i = 1;
                            _allMods.Clear();
                            foreach (var item in steamFileDetails.Publishedfiledetails) {
                                var tags = item;

                                var lstTags = new List<string>();

                                //foreach (var tag in tags)
                                //{
                                //    dynamic d = tag;
                                //    lstTags.Add(d.tag.ToString());
                                //}
                                _allMods.Add(
                                             new ModListDetails {
                                                                    Order             = i,
                                                                    Name              = item.Title == null ? "" : item.Title,
                                                                    ModId             = item.Publishedfileid,
                                                                    LastUpdatedAuthor = item.TimeUpdated.UnixTimeStampToDateTime(),
                                                                    LastDownloaded    = item.TimeUpdated.UnixTimeStampToDateTime(),
                                                                    FolderSize        = item.FileSize,
                                                                    ModType           = string.Join(",", lstTags.ToArray()),
                                                                    TimeStamp         = item.TimeUpdated,
                                                                    Link              = $"https://steamcommunity.com/sharedfiles/filedetails/?id={item.Publishedfileid}",
                                                                    Subscriptions     = item.Subscriptions
                                                                }
                                            );
                                i++;
                            }

                            textBox1.Text                          = _allMods.Count.ToString();
                            textBox2.Text                          = DateTime.Now.ToString();
                            modListDetailsBindingSource.DataSource = _allMods.ToSortableBindingList();
                        }

                    break;
                case ModSource.CurseForge:
                    curseForgeFileDetails = curseForgeUtils.GetCurseForgeModDetails(profile.Type.ModAppId.ToString());
                    if (curseForgeFileDetails != null)
                        if (curseForgeFileDetails.Data.Count > 0) {
                            _allMods.Clear();
                            int i = 1;
                            foreach (var item in curseForgeFileDetails.Data) {
                                _allMods.Add(
                                             new ModListDetails {
                                                                    Order             = i,
                                                                    Name              = item.Name == null ? "" : item.Name,
                                                                    ModId             = item.Id.ToString(),
                                                                    LastUpdatedAuthor = item.DateModified,
                                                                    LastDownloaded    = item.DateModified,
                                                                    FolderSize        = item.LatestFiles.First().FileSizeOnDisk.ToString(),
                                                                    ModType           = string.Join(",", item.Categories.Select(x => x.Name)),
                                                                    TimeStamp         = item.DateModified.Ticks,
                                                                    Link              = item.Links.WebsiteUrl,
                                                                    Subscriptions     = item.DownloadCount
                                                                }
                                            );
                                i++;
                            }

                            //var x4 = allMods.FindAll(x3 => x3.ModID == "932007");
                            textBox1.Text                          = _allMods.Count.ToString();
                            textBox2.Text                          = DateTime.Now.ToString();
                            modListDetailsBindingSource.DataSource = _allMods.ToSortableBindingList();
                        }

                    break;
            }

            ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e) {
            modListDetailsBindingSource.Clear();
            if (string.IsNullOrWhiteSpace(textBox3.Text))
                modListDetailsBindingSource.DataSource = _allMods.ToSortableBindingList();
            else
                modListDetailsBindingSource.DataSource = _allMods.FindAll(x => x.ModId.Contains(textBox3.Text) || x.Name.ToUpper().Contains(textBox3.Text.ToUpper()) || x.ModType.ToUpper().Contains(textBox3.Text.ToUpper())).ToSortableBindingList();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "ModID") {
                int index = e.RowIndex;

                var obj = (ModListDetails)modListDetailsBindingSource[index];

                Process.Start(obj.Link);
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "btaddo") {
                int index = e.RowIndex;

                var obj = (ModListDetails)modListDetailsBindingSource[index];
                obj.Order = _frmParent.modListDetailsBindingSource.Count + 1;


                _frmParent.modListDetailsBindingSource.Add(obj);
            }
        }

        private void FrmModManagerAdd_Load(object sender, EventArgs e) {
            btRefreshToolTip.SetToolTip(button1, "Refresh mod list");
        }

        private void button1_Click(object sender, EventArgs e) {
            var    curseForgeUtils       = new CurseForgeUtils(MainForm.Settings);
            var    steamUtils            = new SteamUtils(MainForm.Settings);
            var    curseForgeFileDetails = new CurseForgeFileDetailResponse();
            var    steamFileDetails      = new WorkshopFileDetailResponse();
            string cacheFolder           = Path.Combine(MainForm.Settings.DataFolder, "cache", _profile1.Type.KeyName);

            modListDetailsBindingSource.Clear();
            switch (_profile1.Type.ModsSource) {
                case ModSource.SteamWorkshop:

                    steamFileDetails = steamUtils.GetSteamModDetails(_profile1.Type.ModAppId.ToString());
                    if (steamFileDetails != null)
                        if (steamFileDetails.Publishedfiledetails.Count > 0) {
                            int i = 1;
                            _allMods.Clear();
                            foreach (var item in steamFileDetails.Publishedfiledetails) {
                                var tags = item;

                                var lstTags = new List<string>();

                                //foreach (var tag in tags)
                                //{
                                //    dynamic d = tag;
                                //    lstTags.Add(d.tag.ToString());
                                //}
                                _allMods.Add(
                                             new ModListDetails {
                                                                    Order             = i,
                                                                    Name              = item.Title == null ? "" : item.Title,
                                                                    ModId             = item.Publishedfileid,
                                                                    LastUpdatedAuthor = item.TimeUpdated.UnixTimeStampToDateTime(),
                                                                    LastDownloaded    = item.TimeUpdated.UnixTimeStampToDateTime(),
                                                                    FolderSize        = item.FileSize,
                                                                    ModType           = string.Join(",", lstTags.ToArray()),
                                                                    TimeStamp         = item.TimeUpdated,
                                                                    Link              = $"https://steamcommunity.com/sharedfiles/filedetails/?id={item.Publishedfileid}",
                                                                    Subscriptions     = item.Subscriptions
                                                                }
                                            );
                                i++;
                            }

                            textBox1.Text                          = _allMods.Count.ToString();
                            textBox2.Text                          = DateTime.Now.ToString();
                            modListDetailsBindingSource.DataSource = _allMods.ToSortableBindingList();
                        }

                    break;
                case ModSource.CurseForge:
                    curseForgeFileDetails = curseForgeUtils.GetCurseForgeModDetails(_profile1.Type.ModAppId.ToString());
                    if (curseForgeFileDetails != null)
                        if (curseForgeFileDetails.Data.Count > 0) {
                            _allMods.Clear();
                            int i = 1;
                            foreach (var item in curseForgeFileDetails.Data) {
                                _allMods.Add(
                                             new ModListDetails {
                                                                    Order             = i,
                                                                    Name              = item.Name == null ? "" : item.Name,
                                                                    ModId             = item.Id.ToString(),
                                                                    LastUpdatedAuthor = item.DateModified,
                                                                    LastDownloaded    = item.DateModified,
                                                                    FolderSize        = item.LatestFiles.First().FileSizeOnDisk.ToString(),
                                                                    ModType           = string.Join(",", item.Categories.Select(x => x.Name)),
                                                                    TimeStamp         = item.DateModified.Ticks,
                                                                    Link              = item.Links.WebsiteUrl,
                                                                    Subscriptions     = item.DownloadCount
                                                                }
                                            );
                                i++;
                            }

                            //var x4 = allMods.FindAll(x3 => x3.ModID == "932007");
                            textBox1.Text                          = _allMods.Count.ToString();
                            textBox2.Text                          = DateTime.Now.ToString();
                            modListDetailsBindingSource.DataSource = _allMods.ToSortableBindingList();
                        }

                    break;
            }
        }
    }
}