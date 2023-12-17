using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models.SupportedServers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OphiussaServerManager.Common.Helpers;
using Microsoft.Win32.TaskScheduler;

namespace OphiussaServerManager.Forms
{
    public partial class FrmModManager : Form
    {
        Profile profile1 = null;
        FrmArk frmParent = null;
        public Action<List<ModListDetails>> updateModList;

        public FrmModManager()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void FrmModManager_Load(object sender, EventArgs e)
        {
            btSavetooltip.SetToolTip(button1, "Save changes");
        }

        public void LoadMods(ref Profile profile, string modIDs, FrmArk owner)
        {
            frmParent = owner;

            profile1 = profile;
            CurseForgeUtils curseForgeUtils = new CurseForgeUtils(MainForm.Settings);
            SteamUtils steamUtils = new SteamUtils(MainForm.Settings);
            CurseForgeFileDetailResponse curseForgeFileDetails = new CurseForgeFileDetailResponse();
            PublishedFileDetailsResponse steamFileDetails = new PublishedFileDetailsResponse();
            string CacheFolder = System.IO.Path.Combine(MainForm.Settings.DataFolder, "cache", profile.Type.KeyName);

            List<string> modlst = modIDs.Split(',').ToList();
            modListDetailsBindingSource.Clear();
            switch (profile.Type.ModsSource)
            {
                case ModSource.SteamWorkshop:
                    steamFileDetails = steamUtils.GetSteamModDetails(modlst);
                    if (steamFileDetails != null)
                    {
                        if (steamFileDetails.publishedfiledetails.Count > 0)
                        {
                            int i = 1;
                            foreach (var item in steamFileDetails.publishedfiledetails)
                            {
                                var tags =  item.tags;

                                List<string> lstTags = new List<string>();
                                foreach (var tag in tags)
                                {
                                    dynamic d = tag;
                                    lstTags.Add(d.tag.ToString());
                                }

                                modListDetailsBindingSource.Add(
                                    new ModListDetails()
                                    {
                                        Order = i,
                                        Name = item.title,
                                        ModID = item.publishedfileid,
                                        LastUpdatedAuthor = item.time_updated.UnixTimeStampToDateTime(),
                                        LastDownloaded = item.time_updated.UnixTimeStampToDateTime(),
                                        FolderSize = item.file_size,
                                        ModType = string.Join(",", lstTags.ToArray()),
                                        TimeStamp = item.time_updated,
                                        Link = $"https://steamcommunity.com/sharedfiles/filedetails/?id={item.publishedfileid}",
                                    }
                                    );
                                i++;
                            }
                        }
                    }
                     
                    break; 
                case ModSource.CurseForge:
                    curseForgeFileDetails = curseForgeUtils.GetCurseForgeModDetails(modlst);
                    if (curseForgeFileDetails != null)
                    {
                        if (curseForgeFileDetails.data.Count > 0)
                        {
                            int i = 1;
                            foreach (var item in curseForgeFileDetails.data)
                            {
                                modListDetailsBindingSource.Add(
                                    new ModListDetails()
                                    {
                                        Order = i,
                                        Name = item.name,
                                        ModID = item.id.ToString(),
                                        LastUpdatedAuthor = item.dateModified,
                                        LastDownloaded = item.dateModified,
                                        FolderSize = item.latestFiles.First().fileSizeOnDisk.ToString(),
                                        ModType = string.Join(",", item.categories.Select(x => x.name)),
                                        TimeStamp = item.dateModified.Ticks,
                                        Link = item.links.websiteUrl,
                                    }
                                    );
                                i++;
                            }
                        }
                    }
                    break;
            }

            base.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "clUp")
                {
                    int index = e.RowIndex;

                    this.dataGridView1.ClearSelection();
                    if (index == 0)
                    {
                        return;//can go down
                    }
                    var tempKey = this.modListDetailsBindingSource[index];
                    this.modListDetailsBindingSource.RemoveAt(index);
                    this.modListDetailsBindingSource.Insert(index - 1, tempKey);
                    this.dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex - 1].Cells[e.ColumnIndex];
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "clDown")
                {

                    int index = e.RowIndex;

                    this.dataGridView1.ClearSelection();
                    if (index == this.dataGridView1.Rows.Count - 1)
                    {
                        return;//can go down
                    }
                    var tempKey = this.modListDetailsBindingSource[index];
                    this.modListDetailsBindingSource.RemoveAt(index);
                    this.modListDetailsBindingSource.Insert(index + 1, tempKey);
                    this.dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex + 1].Cells[e.ColumnIndex];

                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "clDelete")
                {
                    int index = e.RowIndex;

                    if (MessageBox.Show("Do you want remove this mod?", "Remove MOD", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        this.modListDetailsBindingSource.RemoveAt(index);
                    }
                }
                if (dataGridView1.Columns[e.ColumnIndex].Name == "modIDDataGridViewTextBoxColumn")
                {

                    int index = e.RowIndex;

                    ModListDetails obj = (ModListDetails)this.modListDetailsBindingSource[index];

                    System.Diagnostics.Process.Start(obj.Link);
                }

            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want remove all mods?", "Remove Mods", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.modListDetailsBindingSource.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmModManagerAdd frm = new FrmModManagerAdd();
            frm.LoadAllMods(profile1, this);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            List<ModListDetails> lst = new List<ModListDetails>();

            foreach (var item in modListDetailsBindingSource)
            {
                lst.Add((ModListDetails)item);
            }

            updateModList.Invoke(lst);
            this.Close();
        }
    }
}
