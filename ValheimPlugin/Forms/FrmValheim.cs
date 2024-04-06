using System;
using System.Linq;
using System.Windows.Forms;
using OphiussaFramework;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.Forms;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;

namespace ValheimPlugin.Forms {
    public partial class FrmValheim : Form {
        private readonly IPlugin _plugin;
        private readonly Profile Profile;
        private          TabPage _tabPage;

        public FrmValheim(IPlugin plugin, TabPage tab) {
            _plugin = plugin;
            InitializeComponent();
            profileHeader1.Profile       = _plugin.Profile;
            Profile                      = (Profile)_plugin.Profile;
            profileHeader1.Plugin        = _plugin;
            automaticManagement1.Profile = _plugin.Profile;
            automaticManagement1.Plugin  = _plugin;
            profileHeader1.Tab           = tab;
            _tabPage                     = tab;

            if (_plugin.Profile.CpuAffinityList.Count == 0) _plugin.Profile.CpuAffinityList = ConnectionController.ProcessorList;

            txtLocalIP.DataSource    = ConnectionController.IpLists;
            txtLocalIP.ValueMember   = "IP";
            txtLocalIP.DisplayMember = "Description";

            cboPriority.DataSource    = ConnectionController.AffinityModel;
            cboPriority.ValueMember   = "Code";
            cboPriority.DisplayMember = "Name";
            AddDataBindings();
        }

        private void AddDataBindings() {
            cboPriority.SelectedValue = Profile.CpuPriority.ToString();

            tbPresetNormal.Checked    = Profile.Preset == Preset.Normal;
            tbPresetCasual.Checked    = Profile.Preset == Preset.Casual;
            tbPresetEasy.Checked      = Profile.Preset == Preset.Easy;
            tbPresetHard.Checked      = Profile.Preset == Preset.Hard;
            tbPresetHardcore.Checked  = Profile.Preset == Preset.Hardcore;
            tbPresetImmersive.Checked = Profile.Preset == Preset.Immersive;
            tbPresetHammer.Checked    = Profile.Preset == Preset.Hammer;

            rbCombatNone.Checked     = Profile.Combat == Combat.Default;
            rbCombatVeryEasy.Checked = Profile.Combat == Combat.VeryEasy;
            rbCombatEasy.Checked     = Profile.Combat == Combat.Easy;
            rbCombatHard.Checked     = Profile.Combat == Combat.Hard;
            rbCombatVeryHard.Checked = Profile.Combat == Combat.VeryHard;

            rbDeathPenaltyNone.Checked     = Profile.DeathPenalty == DeathPenalty.Default;
            rbDeathPenaltyCasual.Checked   = Profile.DeathPenalty == DeathPenalty.Casual;
            rbDeathPenaltyVeryEasy.Checked = Profile.DeathPenalty == DeathPenalty.VeryEasy;
            rbDeathPenaltyEasy.Checked     = Profile.DeathPenalty == DeathPenalty.Easy;
            rbDeathPenaltyHard.Checked     = Profile.DeathPenalty == DeathPenalty.Hard;
            rbDeathPenaltyHardCore.Checked = Profile.DeathPenalty == DeathPenalty.HardCore;

            rbResourcesNone.Checked     = Profile.Resources == Resources.Default;
            rbResourcesMuchLess.Checked = Profile.Resources == Resources.MuchLess;
            rbResourcesLess.Checked     = Profile.Resources == Resources.Less;
            rbResourcesMore.Checked     = Profile.Resources == Resources.More;
            rbResourcesMuchMore.Checked = Profile.Resources == Resources.MuchMore;
            rbResourcesMost.Checked     = Profile.Resources == Resources.Most;

            rbRaidsDefault.Checked  = Profile.Raids == Raids.Default;
            rbRaidsNone.Checked     = Profile.Raids == Raids.None;
            rbRaidsMuchLess.Checked = Profile.Raids == Raids.MuchLess;
            rbRaidsLess.Checked     = Profile.Raids == Raids.Less;
            rbRaidsMore.Checked     = Profile.Raids == Raids.More;
            rbRaidsMuchMore.Checked = Profile.Raids == Raids.MuchMore;

            rbPortalsNone.Checked     = Profile.Portals == Portals.Default;
            rbPortalsCasual.Checked   = Profile.Portals == Portals.Casual;
            rbPortalsHard.Checked     = Profile.Portals == Portals.Hard;
            rbPortalsVeryHard.Checked = Profile.Portals == Portals.VeryHard;


            txtServerName.DataBindings.Add("Text", _plugin.Profile, "Name");
            txtServerPWD.DataBindings.Add("Text", _plugin.Profile, "ServerPassword");
            txtLocalIP.DataBindings.Add("Text", _plugin.Profile, "MultiHome");
            txtServerPort.DataBindings.Add("Text", _plugin.Profile, "ServerPort");
            txtPeerPort.DataBindings.Add("Text", _plugin.Profile, "PeerPort");
            chkPublic.DataBindings.Add("Checked", _plugin.Profile, "Public");
            chkCrossplay.DataBindings.Add("Checked", _plugin.Profile, "Crossplay");
            txtInstanceID.DataBindings.Add("Text", _plugin.Profile, "InstanceId");
            txtWorldName.DataBindings.Add("Text", _plugin.Profile, "WordName");
            //cboPriority.DataBindings.Add("SelectedValue", _plugin.Profile, "CpuPriority");
            txtAffinity.DataBindings.Add("Text", _plugin.Profile, "CpuAffinity");
            chkAllPiecesUnlocked.DataBindings.Add("Checked", _plugin.Profile, "AllPiecesUnlocked");
            chkNoCraftCost.DataBindings.Add("Checked", _plugin.Profile, "NoCraftCost");
            chkAllRecipesUnlocked.DataBindings.Add("Checked", _plugin.Profile, "AllRecipesUnlocked");
            chkNoBossPortals.DataBindings.Add("Checked", _plugin.Profile, "NoBossPortals");
            chkDeathDeleteItems.DataBindings.Add("Checked", _plugin.Profile, "DeathDeleteItems");
            chkNoMap.DataBindings.Add("Checked", _plugin.Profile, "NoMap");
            chkDeathKeepEquip.DataBindings.Add("Checked", _plugin.Profile, "DeathKeepEquip");
            chkNoPortals.DataBindings.Add("Checked", _plugin.Profile, "NoPortals");
            chkDeathSkillsReset.DataBindings.Add("Checked", _plugin.Profile, "DeathSkillsReset");
            chkNoWorkbench.DataBindings.Add("Checked", _plugin.Profile, "NoWorkbench");
            chkDungeonBuild.DataBindings.Add("Checked", _plugin.Profile, "DungeonBuild");
            chkPassiveMobs.DataBindings.Add("Checked", _plugin.Profile, "PassiveMobs");
            chkNoBuildcost.DataBindings.Add("Checked", _plugin.Profile, "NoBuildcost");
            chkPlayerEvents.DataBindings.Add("Checked", _plugin.Profile, "PlayerEvents");
            chkTeleportAll.DataBindings.Add("Checked", _plugin.Profile, "TeleportAll");
            chkDeathDeleteUnequipped.DataBindings.Add("Checked", _plugin.Profile, "DeathDeleteUnequipped");

            tbDamageTaken.DataBindings.Add("Value", _plugin.Profile, "DamageTaken");
            tbEnemyDamage.DataBindings.Add("Value", _plugin.Profile, "EnemyDamage");
            tbEnemyLevelUpRate.DataBindings.Add("Value", _plugin.Profile, "EnemyLevelUpRate");
            tbEnemySpeedSize.DataBindings.Add("Value", _plugin.Profile, "EnemySpeedSize");
            tbEventRate.DataBindings.Add("Value", _plugin.Profile, "EventRate");
            tbMoveStaminaRate.DataBindings.Add("Value", _plugin.Profile, "MoveStaminaRate");
            tbPlayerDamage.DataBindings.Add("Value", _plugin.Profile, "PlayerDamage");
            tbResourceRate.DataBindings.Add("Value", _plugin.Profile, "ResourceRate");
            tbSkillGainRate.DataBindings.Add("Value", _plugin.Profile, "SkillGainRate");
            tbSkillReductionRate.DataBindings.Add("Value", _plugin.Profile, "SkillReductionRate");
            tbStaminaRate.DataBindings.Add("Value", _plugin.Profile, "StaminaRate");
            tbStaminaRegenRate.DataBindings.Add("Value", _plugin.Profile, "StaminaRegenRate");
            tbAutoSavePeriod.DataBindings.Add("Value", _plugin.Profile, "AutoSavePeriod");
            tbFirstBackup.DataBindings.Add("Value", _plugin.Profile, "BackupShort");
            tbSubBackups.DataBindings.Add("Value", _plugin.Profile, "BackupLong");
            txtBackupToKeep.DataBindings.Add("Text", _plugin.Profile, "TotalBackups");
            txtSaveLocation.DataBindings.Add("Text", _plugin.Profile, "SaveLocation");
            txtLogLocation.DataBindings.Add("Text", _plugin.Profile, "LogFileLocation");

            txtCommand.Text = _plugin.GetCommandLinesArguments();
        }


        private void FillProfile() {
            if (tbPresetNormal.Checked) Profile.Preset    = Preset.Normal;
            if (tbPresetCasual.Checked) Profile.Preset    = Preset.Casual;
            if (tbPresetEasy.Checked) Profile.Preset      = Preset.Easy;
            if (tbPresetHard.Checked) Profile.Preset      = Preset.Hard;
            if (tbPresetHardcore.Checked) Profile.Preset  = Preset.Hardcore;
            if (tbPresetImmersive.Checked) Profile.Preset = Preset.Immersive;
            if (tbPresetHammer.Checked) Profile.Preset    = Preset.Hammer;


            if (rbCombatNone.Checked) Profile.Combat     = Combat.Default;
            if (rbCombatVeryEasy.Checked) Profile.Combat = Combat.VeryEasy;
            if (rbCombatEasy.Checked) Profile.Combat     = Combat.Easy;
            if (rbCombatHard.Checked) Profile.Combat     = Combat.Hard;
            if (rbCombatVeryHard.Checked) Profile.Combat = Combat.VeryHard;


            if (rbDeathPenaltyNone.Checked) Profile.DeathPenalty     = DeathPenalty.Default;
            if (rbDeathPenaltyCasual.Checked) Profile.DeathPenalty   = DeathPenalty.Casual;
            if (rbDeathPenaltyVeryEasy.Checked) Profile.DeathPenalty = DeathPenalty.VeryEasy;
            if (rbDeathPenaltyEasy.Checked) Profile.DeathPenalty     = DeathPenalty.Easy;
            if (rbDeathPenaltyHard.Checked) Profile.DeathPenalty     = DeathPenalty.Hard;
            if (rbDeathPenaltyHardCore.Checked) Profile.DeathPenalty = DeathPenalty.HardCore;


            if (rbResourcesNone.Checked) Profile.Resources     = Resources.Default;
            if (rbResourcesMuchLess.Checked) Profile.Resources = Resources.MuchLess;
            if (rbResourcesLess.Checked) Profile.Resources     = Resources.Less;
            if (rbResourcesMore.Checked) Profile.Resources     = Resources.More;
            if (rbResourcesMuchMore.Checked) Profile.Resources = Resources.MuchMore;
            if (rbResourcesMost.Checked) Profile.Resources     = Resources.Most;

            if (rbRaidsDefault.Checked) Profile.Raids  = Raids.Default;
            if (rbRaidsNone.Checked) Profile.Raids     = Raids.None;
            if (rbRaidsMuchLess.Checked) Profile.Raids = Raids.MuchLess;
            if (rbRaidsLess.Checked) Profile.Raids     = Raids.Less;
            if (rbRaidsMore.Checked) Profile.Raids     = Raids.More;
            if (rbRaidsMuchMore.Checked) Profile.Raids = Raids.MuchMore;

            if (rbPortalsNone.Checked) Profile.Portals     = Portals.Default;
            if (rbPortalsCasual.Checked) Profile.Portals   = Portals.Casual;
            if (rbPortalsHard.Checked) Profile.Portals     = Portals.Hard;
            if (rbPortalsVeryHard.Checked) Profile.Portals = Portals.VeryHard;

            //the databindgs dont work well in costum controls, so i need to force the value into profile 
            Profile.DamageTaken        = tbDamageTaken.Value;
            Profile.EnemyDamage        = tbEnemyDamage.Value;
            Profile.EnemyLevelUpRate   = tbEnemyLevelUpRate.Value;
            Profile.EnemySpeedSize     = tbEnemySpeedSize.Value;
            Profile.EventRate          = tbEventRate.Value;
            Profile.MoveStaminaRate    = tbMoveStaminaRate.Value;
            Profile.PlayerDamage       = tbPlayerDamage.Value;
            Profile.ResourceRate       = tbResourceRate.Value;
            Profile.SkillGainRate      = tbSkillGainRate.Value;
            Profile.SkillReductionRate = tbSkillReductionRate.Value;
            Profile.StaminaRate        = tbStaminaRate.Value;
            Profile.StaminaRegenRate   = tbStaminaRegenRate.Value;

            Profile.CpuPriority = (ProcessPriority)Enum.Parse(typeof(ProcessPriority), cboPriority.SelectedValue.ToString(), true);
        }

        private void profileHeader1_ClickSave(object sender, EventArgs e) {
            try {
                FillProfile();

                _plugin.Profile.AutoManagement = automaticManagement1.GetRestartSettings();
                _plugin.Save();
                automaticManagement1.LoadGrid();

                MessageBox.Show("Profile Saved");
            }
            catch (Exception exception) {
                MessageBox.Show(exception.Message);
                OphiussaLogger.Logger.Error(exception);
            }
        }

        private void profileHeader1_ClickRCON(object sender, EventArgs e) {
            _plugin.OpenRCON();
        }

        private void profileHeader1_ClickReload(object sender, EventArgs e) {
            _plugin.Reload();
        }

        private void profileHeader1_ClickSync(object sender, EventArgs e) {
            _plugin.Sync();
        }

        private void profileHeader1_ClickUpgrade(object sender, EventArgs e) {
            _plugin.InstallServer();
        }

        private void profileHeader1_ClickStartStop(object sender, EventArgs e) {
            if (!profileHeader1.IsRunning)
                _plugin.StartServer();
            else
                _plugin.StopServer();
        }

        private void FrmConfigurationForm_Load(object sender, EventArgs e) {
        }

        private void profileHeader1_TabHeaderChange(object sender, OphiussaEventArgs e) {
            _plugin.TabHeaderChange();
        }

        private void button1_Click(object sender, EventArgs e) {
            var cmdBuilder = new CommandBuilder(_plugin.DefaultCommands);
            cmdBuilder.OpenCommandEditor(fullCommand => {
                                             _plugin.DefaultCommands = fullCommand.ComandList;
                                             MessageBox.Show(fullCommand.ToString());
                                         });
        }

        private void button2_Click(object sender, EventArgs e) {
            fdDiag.SelectedPath = txtSaveLocation.Text;
            fdDiag.ShowDialog();
            txtSaveLocation.Text = fdDiag.SelectedPath;
        }

        private void button3_Click(object sender, EventArgs e) {
            fdDiag.SelectedPath = txtLogLocation.Text;
            fdDiag.ShowDialog();
            txtLogLocation.Text = fdDiag.SelectedPath;
        }

        private void button4_Click(object sender, EventArgs e) {
            FillProfile();
            txtCommand.Text = _plugin.GetCommandLinesArguments();
        }

        private void btProcessorAffinity_Click(object sender, EventArgs e) {
            var frm = new FrmProcessors(Profile.CpuAffinity == "All",
                                        Profile.CpuAffinityList);
            frm.UpdateCpuAffinity = (all, lst) => {
                                        Profile.CpuAffinity = all
                                                                  ? "All"
                                                                  : string.Join(",", lst.FindAll(x => x.Selected).Select(x => x.ProcessorNumber.ToString()));
                                        Profile.CpuAffinityList = lst;
                                        txtAffinity.Text        = Profile.CpuAffinity;
                                    };
            frm.ShowDialog();
        }

        private void txtServerPWD_DoubleClick(object sender, EventArgs e) {
            txtServerPWD.PasswordChar = txtServerPWD.PasswordChar == '\0' ? '*' : '\0';
        }

        private void txtServerPort_TextChanged(object sender, EventArgs e) {
            int port;
            if (int.TryParse(txtServerPort.Text, out port)) txtPeerPort.Text = (port + 1).ToString();
        }
    }
}