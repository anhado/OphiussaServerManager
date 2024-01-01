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
    public partial class FrmModManager : Form {
        private Profile _profile1;
        public Action<List<ModListDetails>> UpdateModList;

        public FrmModManager() {
            InitializeComponent();
        }

        private void FrmModManager_Load(object sender, EventArgs e) {
            btSavetooltip.SetToolTip(button1, "Save changes");
        }

        public void LoadMods(ref Profile profile, string modIDs) {

            _profile1 = profile;
            var curseForgeUtils = new CurseForgeUtils(MainForm.Settings);
            var steamUtils = new SteamUtils(MainForm.Settings);
            var curseForgeFileDetails = new CurseForgeFileDetailResponse();
            var steamFileDetails = new PublishedFileDetailsResponse();
            string cacheFolder = Path.Combine(MainForm.Settings.DataFolder, "cache", profile.Type.KeyName);

            var modlst = modIDs.Split(',').ToList();
            modListDetailsBindingSource.Clear();
            switch (profile.Type.ModsSource) {
                case ModSource.SteamWorkshop:
                    steamFileDetails = steamUtils.GetSteamModDetails(modlst);
                    if (steamFileDetails != null)
                        if (steamFileDetails.Publishedfiledetails.Count > 0) {
                            int i = 1;
                            foreach (var item in steamFileDetails.Publishedfiledetails) {
                                var tags = item.Tags;

                                var lstTags = new List<string>();
                                foreach (object tag in tags) {
                                    dynamic d = tag;
                                    lstTags.Add(d.tag.ToString());
                                }

                                modListDetailsBindingSource.Add(
                                                                new ModListDetails {
                                                                    Order = i,
                                                                    Name = item.Title,
                                                                    ModId = item.Publishedfileid,
                                                                    LastUpdatedAuthor = item.TimeUpdated.UnixTimeStampToDateTime(),
                                                                    LastDownloaded = item.TimeUpdated.UnixTimeStampToDateTime(),
                                                                    FolderSize = item.FileSize,
                                                                    ModType = string.Join(",", lstTags.ToArray()),
                                                                    TimeStamp = item.TimeUpdated,
                                                                    Link = $"https://steamcommunity.com/sharedfiles/filedetails/?id={item.Publishedfileid}"
                                                                }
                                                               );
                                i++;
                            }
                        }

                    break;
                case ModSource.CurseForge:
                    curseForgeFileDetails = curseForgeUtils.GetCurseForgeModDetails(modlst);
                    if (curseForgeFileDetails != null)
                        if (curseForgeFileDetails.Data.Count > 0) {
                            int i = 1;
                            foreach (var item in curseForgeFileDetails.Data) {
                                modListDetailsBindingSource.Add(
                                                                new ModListDetails {
                                                                    Order = i,
                                                                    Name = item.Name,
                                                                    ModId = item.Id.ToString(),
                                                                    LastUpdatedAuthor = item.DateModified,
                                                                    LastDownloaded = item.DateModified,
                                                                    FolderSize = item.LatestFiles.First().FileSizeOnDisk.ToString(),
                                                                    ModType = string.Join(",", item.Categories.Select(x => x.Name)),
                                                                    TimeStamp = item.DateModified.Ticks,
                                                                    Link = item.Links.WebsiteUrl
                                                                }
                                                               );
                                i++;
                            }
                        }

                    break;
            }

            ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) {
            try {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "clUp") {
                    int index = e.RowIndex;

                    dataGridView1.ClearSelection();
                    if (index == 0) return; //can go down
                    object tempKey = modListDetailsBindingSource[index];
                    modListDetailsBindingSource.RemoveAt(index);
                    modListDetailsBindingSource.Insert(index - 1, tempKey);
                    dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex - 1].Cells[e.ColumnIndex];
                }

                if (dataGridView1.Columns[e.ColumnIndex].Name == "clDown") {
                    int index = e.RowIndex;

                    dataGridView1.ClearSelection();
                    if (index == dataGridView1.Rows.Count - 1) return; //can go down
                    object tempKey = modListDetailsBindingSource[index];
                    modListDetailsBindingSource.RemoveAt(index);
                    modListDetailsBindingSource.Insert(index + 1, tempKey);
                    dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex + 1].Cells[e.ColumnIndex];
                }

                if (dataGridView1.Columns[e.ColumnIndex].Name == "clDelete") {
                    int index = e.RowIndex;

                    if (MessageBox.Show("Do you want remove this mod?", "Remove MOD", MessageBoxButtons.OKCancel) == DialogResult.OK) modListDetailsBindingSource.RemoveAt(index);
                }

                if (dataGridView1.Columns[e.ColumnIndex].Name == "modIDDataGridViewTextBoxColumn") {
                    int index = e.RowIndex;

                    var obj = (ModListDetails)modListDetailsBindingSource[index];

                    Process.Start(obj.Link);
                }
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Do you want remove all mods?", "Remove Mods", MessageBoxButtons.OKCancel) == DialogResult.OK) modListDetailsBindingSource.Clear();
        }

        private void button2_Click(object sender, EventArgs e) {
            var frm = new FrmModManagerAdd();
            frm.LoadAllMods(_profile1, this);
        }

        private void button1_Click_1(object sender, EventArgs e) {
            var lst = new List<ModListDetails>();

            foreach (object item in modListDetailsBindingSource) lst.Add((ModListDetails)item);

            UpdateModList.Invoke(lst);
            Close();
        }
    }
}