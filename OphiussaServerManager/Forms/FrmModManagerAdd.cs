using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models.SupportedServers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OphiussaServerManager.Common.Helpers;

namespace OphiussaServerManager.Forms
{
    public partial class FrmModManagerAdd : Form
    {
        List<ModListDetails> allMods = new List<ModListDetails>();
        Profile profile1 = null;
        FrmModManager frmParent;
        public FrmModManagerAdd()
        {
            InitializeComponent();
        }

        internal void LoadAllMods(Profile profile, FrmModManager owner)
        {
            frmParent = owner;
            profile1 = profile;
            CurseForgeUtils curseForgeUtils = new CurseForgeUtils(MainForm.Settings);
            SteamUtils steamUtils = new SteamUtils(MainForm.Settings);
            CurseForgeFileDetailResponse curseForgeFileDetails = new CurseForgeFileDetailResponse();
            WorkshopFileDetailResponse steamFileDetails = new WorkshopFileDetailResponse();
            string CacheFolder = System.IO.Path.Combine(MainForm.Settings.DataFolder, "cache", profile.Type.KeyName);

            modListDetailsBindingSource.Clear();
            switch (profile.Type.ModsSource)
            {
                case ModSource.SteamWorkshop:

                    steamFileDetails = steamUtils.GetSteamModDetails(profile.Type.ModAppID.ToString());
                    if (steamFileDetails != null)
                    {
                        if (steamFileDetails.publishedfiledetails.Count > 0)
                        {
                            int i = 1;
                            allMods.Clear();
                            foreach (var item in steamFileDetails.publishedfiledetails)
                            {
                                var tags = item;

                                List<string> lstTags = new List<string>();
                                //foreach (var tag in tags)
                                //{
                                //    dynamic d = tag;
                                //    lstTags.Add(d.tag.ToString());
                                //}

                                allMods.Add(
                                    new ModListDetails()
                                    {
                                        Order = i,
                                        Name = item.title == null ? "" : item.title,
                                        ModID = item.publishedfileid,
                                        LastUpdatedAuthor = item.time_updated.UnixTimeStampToDateTime(),
                                        LastDownloaded = item.time_updated.UnixTimeStampToDateTime(),
                                        FolderSize = item.file_size,
                                        ModType = string.Join(",", lstTags.ToArray()),
                                        TimeStamp = item.time_updated,
                                        Link = $"https://steamcommunity.com/sharedfiles/filedetails/?id={item.publishedfileid}",
                                        Subscriptions = item.subscriptions
                                    }
                                    );
                                i++;
                            }

                            textBox1.Text = allMods.Count.ToString();
                            textBox2.Text = DateTime.Now.ToString();
                            modListDetailsBindingSource.DataSource = allMods.ToSortableBindingList();
                        }
                    }

                    break;
                case ModSource.CurseForge:
                    curseForgeFileDetails = curseForgeUtils.GetCurseForgeModDetails(profile.Type.ModAppID.ToString());
                    if (curseForgeFileDetails != null)
                    {
                        if (curseForgeFileDetails.data.Count > 0)
                        {
                            allMods.Clear();
                            int i = 1;
                            foreach (var item in curseForgeFileDetails.data)
                            {
                                allMods.Add(
                                    new ModListDetails()
                                    {
                                        Order = i,
                                        Name = item.name == null ? "" : item.name,
                                        ModID = item.id.ToString(),
                                        LastUpdatedAuthor = item.dateModified,
                                        LastDownloaded = item.dateModified,
                                        FolderSize = item.latestFiles.First().fileSizeOnDisk.ToString(),
                                        ModType = string.Join(",", item.categories.Select(x => x.name)),
                                        TimeStamp = item.dateModified.Ticks,
                                        Link = item.links.websiteUrl,
                                        Subscriptions = item.downloadCount
                                    }
                                    );
                                i++;
                            }
                            //var x4 = allMods.FindAll(x3 => x3.ModID == "932007");
                            textBox1.Text = allMods.Count.ToString();
                            textBox2.Text = DateTime.Now.ToString();
                            modListDetailsBindingSource.DataSource = allMods.ToSortableBindingList();
                        }
                    }
                    break;
            }

            base.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            modListDetailsBindingSource.Clear();
            if (string.IsNullOrWhiteSpace(textBox3.Text))
                modListDetailsBindingSource.DataSource = allMods.ToSortableBindingList();
            else
                modListDetailsBindingSource.DataSource = allMods.FindAll(x => x.ModID.Contains(textBox3.Text) || x.Name.ToUpper().Contains(textBox3.Text.ToUpper()) || x.ModType.ToUpper().Contains(textBox3.Text.ToUpper())).ToSortableBindingList();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "ModID")
            {

                int index = e.RowIndex;

                ModListDetails obj = (ModListDetails)this.modListDetailsBindingSource[index];

                System.Diagnostics.Process.Start(obj.Link);
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "btaddo")
            {

                int index = e.RowIndex;

                ModListDetails obj = (ModListDetails)this.modListDetailsBindingSource[index];
                obj.Order = frmParent.modListDetailsBindingSource.Count + 1;


                frmParent.modListDetailsBindingSource.Add(obj);
            }
        }

        private void FrmModManagerAdd_Load(object sender, EventArgs e)
        {
            btRefreshToolTip.SetToolTip(button1, "Refresh mod list");
        }

        private void button1_Click(object sender, EventArgs e)
        {

            CurseForgeUtils curseForgeUtils = new CurseForgeUtils(MainForm.Settings);
            SteamUtils steamUtils = new SteamUtils(MainForm.Settings);
            CurseForgeFileDetailResponse curseForgeFileDetails = new CurseForgeFileDetailResponse();
            WorkshopFileDetailResponse steamFileDetails = new WorkshopFileDetailResponse();
            string CacheFolder = System.IO.Path.Combine(MainForm.Settings.DataFolder, "cache", profile1.Type.KeyName);

            modListDetailsBindingSource.Clear();
            switch (profile1.Type.ModsSource)
            {
                case ModSource.SteamWorkshop:

                    steamFileDetails = steamUtils.GetSteamModDetails(profile1.Type.ModAppID.ToString());
                    if (steamFileDetails != null)
                    {
                        if (steamFileDetails.publishedfiledetails.Count > 0)
                        {
                            int i = 1;
                            allMods.Clear();
                            foreach (var item in steamFileDetails.publishedfiledetails)
                            {
                                var tags = item;

                                List<string> lstTags = new List<string>();
                                //foreach (var tag in tags)
                                //{
                                //    dynamic d = tag;
                                //    lstTags.Add(d.tag.ToString());
                                //}

                                allMods.Add(
                                    new ModListDetails()
                                    {
                                        Order = i,
                                        Name = item.title == null ? "" : item.title,
                                        ModID = item.publishedfileid,
                                        LastUpdatedAuthor = item.time_updated.UnixTimeStampToDateTime(),
                                        LastDownloaded = item.time_updated.UnixTimeStampToDateTime(),
                                        FolderSize = item.file_size,
                                        ModType = string.Join(",", lstTags.ToArray()),
                                        TimeStamp = item.time_updated,
                                        Link = $"https://steamcommunity.com/sharedfiles/filedetails/?id={item.publishedfileid}",
                                        Subscriptions = item.subscriptions
                                    }
                                    );
                                i++;
                            }

                            textBox1.Text = allMods.Count.ToString();
                            textBox2.Text = DateTime.Now.ToString();
                            modListDetailsBindingSource.DataSource = allMods.ToSortableBindingList();
                        }
                    }

                    break;
                case ModSource.CurseForge:
                    curseForgeFileDetails = curseForgeUtils.GetCurseForgeModDetails(profile1.Type.ModAppID.ToString());
                    if (curseForgeFileDetails != null)
                    {
                        if (curseForgeFileDetails.data.Count > 0)
                        {
                            allMods.Clear();
                            int i = 1;
                            foreach (var item in curseForgeFileDetails.data)
                            {
                                allMods.Add(
                                    new ModListDetails()
                                    {
                                        Order = i,
                                        Name = item.name == null ? "" : item.name,
                                        ModID = item.id.ToString(),
                                        LastUpdatedAuthor = item.dateModified,
                                        LastDownloaded = item.dateModified,
                                        FolderSize = item.latestFiles.First().fileSizeOnDisk.ToString(),
                                        ModType = string.Join(",", item.categories.Select(x => x.name)),
                                        TimeStamp = item.dateModified.Ticks,
                                        Link = item.links.websiteUrl,
                                        Subscriptions = item.downloadCount
                                    }
                                    );
                                i++;
                            }
                            //var x4 = allMods.FindAll(x3 => x3.ModID == "932007");
                            textBox1.Text = allMods.Count.ToString();
                            textBox2.Text = DateTime.Now.ToString();
                            modListDetailsBindingSource.DataSource = allMods.ToSortableBindingList();
                        }
                    }
                    break;
            }
        }
    }
}
