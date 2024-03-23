using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using OphiussaFramework.Interfaces;

namespace OphiussaServerManagerV2
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void LoadSettings()
        {
            this.txtGUID.DataBindings.Add("Text", Global.Settings, "GUID");
            this.txtDefaultInstallationFolder.DataBindings.Add("Text", Global.Settings, "DefaultInstallFolder");
            this.txtBackupFolder.DataBindings.Add("Text", Global.Settings, "BackupFolder");
            this.txtDataFolder.DataBindings.Add("Text", Global.Settings, "DataFolder");
            this.txtSteamWebApiKey.DataBindings.Add("Text", Global.Settings, "SteamWepApiKey");
            this.txtCurseForgeKey.DataBindings.Add("Text", Global.Settings, "CurseForgeApiKey");
            this.chkEnableLogs.DataBindings.Add("Checked", Global.Settings, "EnableLogs");
            this.txtMaxDays.DataBindings.Add("Text", Global.Settings, "MaxLogsDays");
            this.txtMaxFiles.DataBindings.Add("Text", Global.Settings, "MaxLogFiles");

        }

        private void FrmSettings_FormClosing(object sender, FormClosingEventArgs e)
        { 
            Global.SqlLite.UpSertSettings(Global.Settings);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fd.SelectedPath = txtDataFolder.Text; 
            fd.ShowDialog();
            txtDataFolder.Text = fd.SelectedPath;
        }

        private void button4_Click(object sender, EventArgs e)
        { 
            Process.Start("https://steamcommunity.com/dev/apikey");
        } 
        private void button5_Click(object sender, EventArgs e)
        {
            Process.Start("https://console.curseforge.com/?#/api-keys");
        }

        private void expandCollapsePanel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
