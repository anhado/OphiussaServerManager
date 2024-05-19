using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OphiussaFramework;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.Extensions;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;

namespace VRisingPlugin.Forms {
    public partial class FrmVRising : Form {
        private readonly IPlugin _plugin;
        private readonly Profile Profile;
        private TabPage _tabPage;
        private List<ComboBoxValues> GameModeType = new List<ComboBoxValues>();
        private List<ComboBoxValues> CastleDamageMode = new List<ComboBoxValues>();
        private List<ComboBoxValues> SiegeWeaponHealth = new List<ComboBoxValues>();
        private List<ComboBoxValues> PlayerDamageMode = new List<ComboBoxValues>();
        private List<ComboBoxValues> CastleHeartDamageMode = new List<ComboBoxValues>();
        private List<ComboBoxValues> PvPProtectionMode = new List<ComboBoxValues>();
        private List<ComboBoxValues> DeathContainerPermission = new List<ComboBoxValues>();
        private List<ComboBoxValues> RelicSpawnType = new List<ComboBoxValues>();

        public FrmVRising(IPlugin plugin, TabPage tab) {
            try {
                _plugin = plugin;
                InitializeComponent();
                profileHeader1.Profile = _plugin.Profile;
                Profile = (Profile)_plugin.Profile;
                profileHeader1.Plugin = _plugin;
                automaticManagement1.Profile = _plugin.Profile;
                automaticManagement1.Plugin = _plugin;
                profileHeader1.Tab = tab;
                _tabPage = tab;

                if (_plugin.Profile.CpuAffinityList.Count == 0) Profile.CpuAffinityList = ConnectionController.ProcessorList;

                FillObjectsDefaultData();
                AddBindings();
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
            }
        }

        private void FillObjectsDefaultData() {

            GameModeType.Clear();
            Enum.GetNames(typeof(GameModeType)).ToList().ForEach(e => {
                GameModeType.Add(new ComboBoxValues() {
                    Code = e,
                    Name = e
                });
            });
            cboProfileGameModeType.DataSource = GameModeType;
            cboProfileGameModeType.DisplayMember = "Name";
            cboProfileGameModeType.ValueMember = "Code";


            CastleDamageMode.Clear();
            Enum.GetNames(typeof(CastleDamageMode)).ToList().ForEach(e => {
                CastleDamageMode.Add(new ComboBoxValues() {
                    Code = e,
                    Name = e
                });
            });
            cboProfileCastleDamageMode.DataSource = CastleDamageMode;
            cboProfileCastleDamageMode.DisplayMember = "Name";
            cboProfileCastleDamageMode.ValueMember = "Code";

            SiegeWeaponHealth.Clear();
            Enum.GetNames(typeof(SiegeWeaponHealth)).ToList().ForEach(e => {
                SiegeWeaponHealth.Add(new ComboBoxValues() {
                    Code = e,
                    Name = e
                });
            });
            cboProfileSiegeWeaponHealth.DataSource = SiegeWeaponHealth;
            cboProfileSiegeWeaponHealth.DisplayMember = "Name";
            cboProfileSiegeWeaponHealth.ValueMember = "Code";

            PlayerDamageMode.Clear();
            Enum.GetNames(typeof(PlayerDamageMode)).ToList().ForEach(e => {
                PlayerDamageMode.Add(new ComboBoxValues() {
                    Code = e,
                    Name = e
                });
            });
            cboProfilePlayerDamageMode.DataSource = PlayerDamageMode;
            cboProfilePlayerDamageMode.DisplayMember = "Name";
            cboProfilePlayerDamageMode.ValueMember = "Code";

            CastleHeartDamageMode.Clear();
            Enum.GetNames(typeof(CastleHeartDamageMode)).ToList().ForEach(e => {
                CastleHeartDamageMode.Add(new ComboBoxValues() {
                    Code = e,
                    Name = e
                });
            });
            cboProfileCastleHeartDamageMode.DataSource = CastleHeartDamageMode;
            cboProfileCastleHeartDamageMode.DisplayMember = "Name";
            cboProfileCastleHeartDamageMode.ValueMember = "Code";

            PvPProtectionMode.Clear();
            Enum.GetNames(typeof(PvPProtectionMode)).ToList().ForEach(e => {
                PvPProtectionMode.Add(new ComboBoxValues() {
                    Code = e,
                    Name = e
                });
            });
            cboProfilePvPProtectionMode.DataSource = PvPProtectionMode;
            cboProfilePvPProtectionMode.DisplayMember = "Name";
            cboProfilePvPProtectionMode.ValueMember = "Code";

            DeathContainerPermission.Clear();
            Enum.GetNames(typeof(DeathContainerPermission)).ToList().ForEach(e => {
                DeathContainerPermission.Add(new ComboBoxValues() {
                    Code = e,
                    Name = e
                });
            });
            cboProfileDeathContainerPermission.DataSource = DeathContainerPermission;
            cboProfileDeathContainerPermission.DisplayMember = "Name";
            cboProfileDeathContainerPermission.ValueMember = "Code";

            RelicSpawnType.Clear();
            Enum.GetNames(typeof(RelicSpawnType)).ToList().ForEach(e => {
                RelicSpawnType.Add(new ComboBoxValues() {
                    Code = e,
                    Name = e
                });
            });
            cboProfileRelicSpawnType.DataSource = RelicSpawnType;
            cboProfileRelicSpawnType.DisplayMember = "Name";
            cboProfileRelicSpawnType.ValueMember = "Code";

        }

        private void AddBindings() {

            cboPriority.SelectedValue = Profile.CpuPriority.ToString();
            txtServerPWD.DataBindings.Add("Text", _plugin.Profile, "ServerPassword", true, DataSourceUpdateMode.OnPropertyChanged);
            txtAffinity.DataBindings.Add("Text", _plugin.Profile, "CpuAffinity", true, DataSourceUpdateMode.OnPropertyChanged);
            txtServerName.DataBindings.Add("Text", Profile, "ServerName", true, DataSourceUpdateMode.OnPropertyChanged);
            txtServerDescription.DataBindings.Add("Text", Profile, "Description", true, DataSourceUpdateMode.OnPropertyChanged);
            chkUseRCON.DataBindings.Add("Checked", Profile, "UseRCON", true, DataSourceUpdateMode.OnPropertyChanged);
            txtRconPort.DataBindings.Add("Text", Profile, "RCONPort", true, DataSourceUpdateMode.OnPropertyChanged);
            txtRCONPwd.DataBindings.Add("Text", Profile, "RCONPassword", true, DataSourceUpdateMode.OnPropertyChanged);
            txtServerPort.DataBindings.Add("Text", Profile, "ServerPort", true, DataSourceUpdateMode.OnPropertyChanged);
            txtQueryPort.DataBindings.Add("Text", Profile, "QueryPort", true, DataSourceUpdateMode.OnPropertyChanged);
            chkListOnSteam.DataBindings.Add("Checked", Profile, "ListOnSteam", true, DataSourceUpdateMode.OnPropertyChanged);
            chkListOnEOS.DataBindings.Add("Checked", Profile, "ListOnEOS", true, DataSourceUpdateMode.OnPropertyChanged);
            chkSecure.DataBindings.Add("Checked", Profile, "Secure", true, DataSourceUpdateMode.OnPropertyChanged);
            tbMaxUsers.DataBindings.Add("Value", Profile, "MaxConnectedUsers", true, DataSourceUpdateMode.OnPropertyChanged);
            tbMaxAdmins.DataBindings.Add("Value", Profile, "MaxConnectedAdmins", true, DataSourceUpdateMode.OnPropertyChanged);
            tbServerFPS.DataBindings.Add("Value", Profile, "ServerFps", true, DataSourceUpdateMode.OnPropertyChanged);
            tbAutoSaveCount.DataBindings.Add("Value", Profile, "AutoSaveCount", true, DataSourceUpdateMode.OnPropertyChanged);
            tbAutoSaveInterval.DataBindings.Add("Value", Profile, "AutoSaveInterval", true, DataSourceUpdateMode.OnPropertyChanged);
            chkCompressSaveFiles.DataBindings.Add("Checked", Profile, "CompressSaveFiles", true, DataSourceUpdateMode.OnPropertyChanged);
            chkAdminOnlyDebugEvents.DataBindings.Add("Checked", Profile, "AdminOnlyDebugEvents", true, DataSourceUpdateMode.OnPropertyChanged);
            chkAPIEnabled.DataBindings.Add("Checked", Profile, "APIEnabled", true, DataSourceUpdateMode.OnPropertyChanged);


            tbProfilePlayerInteractionSettingsVSCastleWeekendTimeStartHour.DataBindings.Add("Value", Profile.PlayerInteractionSettings.VSCastleWeekendTime, "StartHour", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfilePlayerInteractionSettingsVSCastleWeekendTimeStartMinute.DataBindings.Add("Value", Profile.PlayerInteractionSettings.VSCastleWeekendTime, "StartMinute", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfilePlayerInteractionSettingsVSCastleWeekendTimeEndHour.DataBindings.Add("Value", Profile.PlayerInteractionSettings.VSCastleWeekendTime, "EndHour", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfilePlayerInteractionSettingsVSCastleWeekendTimeEndMinute.DataBindings.Add("Value", Profile.PlayerInteractionSettings.VSCastleWeekendTime, "EndMinute", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfilePlayerInteractionSettingsVSCastleWeekdayTimeStartHour.DataBindings.Add("Value", Profile.PlayerInteractionSettings.VSCastleWeekdayTime, "StartHour", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfilePlayerInteractionSettingsVSCastleWeekdayTimeStartMinute.DataBindings.Add("Value", Profile.PlayerInteractionSettings.VSCastleWeekdayTime, "StartMinute", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfilePlayerInteractionSettingsVSCastleWeekdayTimeEndHour.DataBindings.Add("Value", Profile.PlayerInteractionSettings.VSCastleWeekdayTime, "EndHour", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfilePlayerInteractionSettingsVSCastleWeekdayTimeEndMinute.DataBindings.Add("Value", Profile.PlayerInteractionSettings.VSCastleWeekdayTime, "EndMinute", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfilePlayerInteractionSettingsVSPlayerWeekendTimeStartHour.DataBindings.Add("Value", Profile.PlayerInteractionSettings.VSPlayerWeekendTime, "StartHour", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfilePlayerInteractionSettingsVSPlayerWeekendTimeStartMinute.DataBindings.Add("Value", Profile.PlayerInteractionSettings.VSPlayerWeekendTime, "StartMinute", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfilePlayerInteractionSettingsVSPlayerWeekendTimeEndHour.DataBindings.Add("Value", Profile.PlayerInteractionSettings.VSPlayerWeekendTime, "EndHour", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfilePlayerInteractionSettingsVSPlayerWeekendTimeEndMinute.DataBindings.Add("Value", Profile.PlayerInteractionSettings.VSPlayerWeekendTime, "EndMinute", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfilePlayerInteractionSettingsVSPlayerWeekdayTimeStartHour.DataBindings.Add("Value", Profile.PlayerInteractionSettings.VSPlayerWeekdayTime, "StartHour", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfilePlayerInteractionSettingsVSPlayerWeekdayTimeStartMinute.DataBindings.Add("Value", Profile.PlayerInteractionSettings.VSPlayerWeekdayTime, "StartMinute", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfilePlayerInteractionSettingsVSPlayerWeekdayTimeEndHour.DataBindings.Add("Value", Profile.PlayerInteractionSettings.VSPlayerWeekdayTime, "EndHour", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfilePlayerInteractionSettingsVSPlayerWeekdayTimeEndMinute.DataBindings.Add("Value", Profile.PlayerInteractionSettings.VSPlayerWeekdayTime, "EndMinute", true, DataSourceUpdateMode.OnPropertyChanged);
            cboProfilePlayerInteractionSettingsTimeZone.DataBindings.Add("SelectedValue", Profile.PlayerInteractionSettings, "TimeZone", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalHeartLimitsLevel5Level.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.HeartLimits.Level5, "Level", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalHeartLimitsLevel5FloorLimit.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.HeartLimits.Level5, "FloorLimit", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalHeartLimitsLevel5ServantLimit.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.HeartLimits.Level5, "ServantLimit", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalHeartLimitsLevel4Level.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.HeartLimits.Level4, "Level", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalHeartLimitsLevel4FloorLimit.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.HeartLimits.Level4, "FloorLimit", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalHeartLimitsLevel4ServantLimit.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.HeartLimits.Level4, "ServantLimit", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalHeartLimitsLevel3Level.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.HeartLimits.Level3, "Level", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalHeartLimitsLevel3FloorLimit.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.HeartLimits.Level3, "FloorLimit", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalHeartLimitsLevel3ServantLimit.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.HeartLimits.Level3, "ServantLimit", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalHeartLimitsLevel2Level.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.HeartLimits.Level2, "Level", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalHeartLimitsLevel2FloorLimit.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.HeartLimits.Level2, "FloorLimit", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalHeartLimitsLevel2ServantLimit.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.HeartLimits.Level2, "ServantLimit", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalHeartLimitsLevel1Level.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.HeartLimits.Level1, "Level", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalHeartLimitsLevel1FloorLimit.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.HeartLimits.Level1, "FloorLimit", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalHeartLimitsLevel1ServantLimit.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.HeartLimits.Level1, "ServantLimit", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalFloorPenaltiesRange5Percentage.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.FloorPenalties.Range5, "Percentage", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalFloorPenaltiesRange5Lower.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.FloorPenalties.Range5, "Lower", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalFloorPenaltiesRange5Higher.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.FloorPenalties.Range5, "Higher", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalFloorPenaltiesRange4Percentage.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.FloorPenalties.Range4, "Percentage", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalFloorPenaltiesRange4Lower.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.FloorPenalties.Range4, "Lower", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalFloorPenaltiesRange4Higher.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.FloorPenalties.Range4, "Higher", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalFloorPenaltiesRange3Percentage.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.FloorPenalties.Range3, "Percentage", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalFloorPenaltiesRange3Lower.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.FloorPenalties.Range3, "Lower", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalFloorPenaltiesRange3Higher.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.FloorPenalties.Range3, "Higher", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalFloorPenaltiesRange2Percentage.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.FloorPenalties.Range2, "Percentage", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalFloorPenaltiesRange2Lower.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.FloorPenalties.Range2, "Lower", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalFloorPenaltiesRange2Higher.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.FloorPenalties.Range2, "Higher", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalFloorPenaltiesRange1Percentage.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.FloorPenalties.Range1, "Percentage", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalFloorPenaltiesRange1Lower.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.FloorPenalties.Range1, "Lower", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalFloorPenaltiesRange1Higher.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.FloorPenalties.Range1, "Higher", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalPylonPenaltiesRange5Percentage.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.PylonPenalties.Range5, "Percentage", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalPylonPenaltiesRange5Lower.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.PylonPenalties.Range5, "Lower", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalPylonPenaltiesRange5Higher.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.PylonPenalties.Range5, "Higher", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalPylonPenaltiesRange4Percentage.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.PylonPenalties.Range4, "Percentage", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalPylonPenaltiesRange4Lower.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.PylonPenalties.Range4, "Lower", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalPylonPenaltiesRange4Higher.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.PylonPenalties.Range4, "Higher", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalPylonPenaltiesRange3Percentage.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.PylonPenalties.Range3, "Percentage", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalPylonPenaltiesRange3Lower.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.PylonPenalties.Range3, "Lower", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalPylonPenaltiesRange3Higher.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.PylonPenalties.Range3, "Higher", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalPylonPenaltiesRange2Percentage.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.PylonPenalties.Range2, "Percentage", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalPylonPenaltiesRange2Lower.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.PylonPenalties.Range2, "Lower", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalPylonPenaltiesRange2Higher.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.PylonPenalties.Range2, "Higher", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalPylonPenaltiesRange1Percentage.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.PylonPenalties.Range1, "Percentage", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalPylonPenaltiesRange1Lower.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.PylonPenalties.Range1, "Lower", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalPylonPenaltiesRange1Higher.DataBindings.Add("Value", Profile.CastleStatModifiers_Global.PylonPenalties.Range1, "Higher", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalTickPeriod.DataBindings.Add("Value", Profile.CastleStatModifiers_Global, "TickPeriod", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalDamageResistance.DataBindings.Add("Value", Profile.CastleStatModifiers_Global, "DamageResistance", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalSafetyBoxLimit.DataBindings.Add("Value", Profile.CastleStatModifiers_Global, "SafetyBoxLimit", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalTombLimit.DataBindings.Add("Value", Profile.CastleStatModifiers_Global, "TombLimit", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalVerminNestLimit.DataBindings.Add("Value", Profile.CastleStatModifiers_Global, "VerminNestLimit", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalPrisonCellLimit.DataBindings.Add("Value", Profile.CastleStatModifiers_Global, "PrisonCellLimit", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleStatModifiersGlobalCastleLimit.DataBindings.Add("Value", Profile.CastleStatModifiers_Global, "CastleLimit", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileEquipmentStatModifiersGlobalMaxEnergyModifier.DataBindings.Add("Value", Profile.EquipmentStatModifiers_Global, "MaxEnergyModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileEquipmentStatModifiersGlobalMaxHealthModifier.DataBindings.Add("Value", Profile.EquipmentStatModifiers_Global, "MaxHealthModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileEquipmentStatModifiersGlobalResourceYieldModifier.DataBindings.Add("Value", Profile.EquipmentStatModifiers_Global, "ResourceYieldModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileEquipmentStatModifiersGlobalPhysicalPowerModifier.DataBindings.Add("Value", Profile.EquipmentStatModifiers_Global, "PhysicalPowerModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileEquipmentStatModifiersGlobalSpellPowerModifier.DataBindings.Add("Value", Profile.EquipmentStatModifiers_Global, "SpellPowerModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileEquipmentStatModifiersGlobalSiegePowerModifier.DataBindings.Add("Value", Profile.EquipmentStatModifiers_Global, "SiegePowerModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileEquipmentStatModifiersGlobalMovementSpeedModifier.DataBindings.Add("Value", Profile.EquipmentStatModifiers_Global, "MovementSpeedModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileUnitStatModifiersVBloodMaxHealthModifier.DataBindings.Add("Value", Profile.UnitStatModifiers_VBlood, "MaxHealthModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileUnitStatModifiersVBloodPowerModifier.DataBindings.Add("Value", Profile.UnitStatModifiers_VBlood, "PowerModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileUnitStatModifiersGlobalMaxHealthModifier.DataBindings.Add("Value", Profile.UnitStatModifiers_Global, "MaxHealthModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileUnitStatModifiersGlobalPowerModifier.DataBindings.Add("Value", Profile.UnitStatModifiers_Global, "PowerModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileVampireStatModifiersMaxHealthModifier.DataBindings.Add("Value", Profile.VampireStatModifiers, "MaxHealthModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileVampireStatModifiersMaxEnergyModifier.DataBindings.Add("Value", Profile.VampireStatModifiers, "MaxEnergyModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileVampireStatModifiersPhysicalPowerModifier.DataBindings.Add("Value", Profile.VampireStatModifiers, "PhysicalPowerModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileVampireStatModifiersSpellPowerModifier.DataBindings.Add("Value", Profile.VampireStatModifiers, "SpellPowerModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileVampireStatModifiersResourcePowerModifier.DataBindings.Add("Value", Profile.VampireStatModifiers, "ResourcePowerModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileVampireStatModifiersSiegePowerModifier.DataBindings.Add("Value", Profile.VampireStatModifiers, "SiegePowerModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileVampireStatModifiersDamageReceivedModifier.DataBindings.Add("Value", Profile.VampireStatModifiers, "DamageReceivedModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileVampireStatModifiersReviveCancelDelay.DataBindings.Add("Value", Profile.VampireStatModifiers, "ReviveCancelDelay", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileGameTimeModifiersDayDurationInSeconds.DataBindings.Add("Value", Profile.GameTimeModifiers, "DayDurationInSeconds", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileGameTimeModifiersDayStartHour.DataBindings.Add("Value", Profile.GameTimeModifiers, "DayStartHour", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileGameTimeModifiersDayStartMinute.DataBindings.Add("Value", Profile.GameTimeModifiers, "DayStartMinute", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileGameTimeModifiersDayEndHour.DataBindings.Add("Value", Profile.GameTimeModifiers, "DayEndHour", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileGameTimeModifiersDayEndMinute.DataBindings.Add("Value", Profile.GameTimeModifiers, "DayEndMinute", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileGameTimeModifiersBloodMoonFrequencyMin.DataBindings.Add("Value", Profile.GameTimeModifiers, "BloodMoonFrequency_Min", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileGameTimeModifiersBloodMoonFrequencyMax.DataBindings.Add("Value", Profile.GameTimeModifiers, "BloodMoonFrequency_Max", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileGameTimeModifiersBloodMoonBuff.DataBindings.Add("Value", Profile.GameTimeModifiers, "BloodMoonBuff", true, DataSourceUpdateMode.OnPropertyChanged);
            chkProfileBloodBoundEquipment.DataBindings.Add("Checked", Profile, "BloodBoundEquipment", true, DataSourceUpdateMode.OnPropertyChanged);
            chkProfileTeleportBoundItems.DataBindings.Add("Checked", Profile, "TeleportBoundItems", true, DataSourceUpdateMode.OnPropertyChanged);
            chkProfileAllowGlobalChat.DataBindings.Add("Checked", Profile, "AllowGlobalChat", true, DataSourceUpdateMode.OnPropertyChanged);
            chkProfileAllWaypointsUnlocked.DataBindings.Add("Checked", Profile, "AllWaypointsUnlocked", true, DataSourceUpdateMode.OnPropertyChanged);
            chkProfileFreeCastleClaim.DataBindings.Add("Checked", Profile, "FreeCastleClaim", true, DataSourceUpdateMode.OnPropertyChanged);
            chkProfileFreeCastleDestroy.DataBindings.Add("Checked", Profile, "FreeCastleDestroy", true, DataSourceUpdateMode.OnPropertyChanged);
            chkProfileInactivityKillEnabled.DataBindings.Add("Checked", Profile, "InactivityKillEnabled", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileInactivityKillTimeMin.DataBindings.Add("Value", Profile, "InactivityKillTimeMin", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileInactivityKillTimeMax.DataBindings.Add("Value", Profile, "InactivityKillTimeMax", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileInactivityKillSafeTimeAddition.DataBindings.Add("Value", Profile, "InactivityKillSafeTimeAddition", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileInactivityKillTimerMaxItemLevel.DataBindings.Add("Value", Profile, "InactivityKillTimerMaxItemLevel", true, DataSourceUpdateMode.OnPropertyChanged);
            chkProfileDisableDisconnectedDeadEnabled.DataBindings.Add("Checked", Profile, "DisableDisconnectedDeadEnabled", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileDisableDisconnectedDeadTimer.DataBindings.Add("Value", Profile, "DisableDisconnectedDeadTimer", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileInventoryStacksModifier.DataBindings.Add("Value", Profile, "InventoryStacksModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileDropTableModifierGeneral.DataBindings.Add("Value", Profile, "DropTableModifier_General", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileDropTableModifierMissions.DataBindings.Add("Value", Profile, "DropTableModifier_Missions", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileMaterialYieldModifierGlobal.DataBindings.Add("Value", Profile, "MaterialYieldModifier_Global", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileBloodEssenceYieldModifier.DataBindings.Add("Value", Profile, "BloodEssenceYieldModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileJournalVBloodSourceUnitMaxDistance.DataBindings.Add("Value", Profile, "JournalVBloodSourceUnitMaxDistance", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfilePvPVampireRespawnModifier.DataBindings.Add("Value", Profile, "PvPVampireRespawnModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleMinimumDistanceInFloors.DataBindings.Add("Value", Profile, "CastleMinimumDistanceInFloors", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileClanSize.DataBindings.Add("Value", Profile, "ClanSize", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileBloodDrainModifier.DataBindings.Add("Value", Profile, "BloodDrainModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileDurabilityDrainModifier.DataBindings.Add("Value", Profile, "DurabilityDrainModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileGarlicAreaStrengthModifier.DataBindings.Add("Value", Profile, "GarlicAreaStrengthModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileHolyAreaStrengthModifier.DataBindings.Add("Value", Profile, "HolyAreaStrengthModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileSilverStrengthModifier.DataBindings.Add("Value", Profile, "SilverStrengthModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileSunDamageModifier.DataBindings.Add("Value", Profile, "SunDamageModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleDecayRateModifier.DataBindings.Add("Value", Profile, "CastleDecayRateModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleBloodEssenceDrainModifier.DataBindings.Add("Value", Profile, "CastleBloodEssenceDrainModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleSiegeTimer.DataBindings.Add("Value", Profile, "CastleSiegeTimer", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCastleUnderAttackTimer.DataBindings.Add("Value", Profile, "CastleUnderAttackTimer", true, DataSourceUpdateMode.OnPropertyChanged);
            chkProfileAnnounceSiegeWeaponSpawn.DataBindings.Add("Checked", Profile, "AnnounceSiegeWeaponSpawn", true, DataSourceUpdateMode.OnPropertyChanged);
            chkProfileShowSiegeWeaponMapIcon.DataBindings.Add("Checked", Profile, "ShowSiegeWeaponMapIcon", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileBuildCostModifier.DataBindings.Add("Value", Profile, "BuildCostModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileRecipeCostModifier.DataBindings.Add("Value", Profile, "RecipeCostModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileCraftRateModifier.DataBindings.Add("Value", Profile, "CraftRateModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileResearchCostModifier.DataBindings.Add("Value", Profile, "ResearchCostModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileRefinementCostModifier.DataBindings.Add("Value", Profile, "RefinementCostModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileRefinementRateModifier.DataBindings.Add("Value", Profile, "RefinementRateModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileResearchTimeModifier.DataBindings.Add("Value", Profile, "ResearchTimeModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileDismantleResourceModifier.DataBindings.Add("Value", Profile, "DismantleResourceModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileServantConvertRateModifier.DataBindings.Add("Value", Profile, "ServantConvertRateModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileRepairCostModifier.DataBindings.Add("Value", Profile, "RepairCostModifier", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileDeathDurabilityFactorLoss.DataBindings.Add("Value", Profile, "Death_DurabilityFactorLoss", true, DataSourceUpdateMode.OnPropertyChanged);
            tbProfileDeathDurabilityLossFactorAsResources.DataBindings.Add("Value", Profile, "Death_DurabilityLossFactorAsResources", true, DataSourceUpdateMode.OnPropertyChanged);
            cboProfileStarterEquipmentId.DataBindings.Add("SelectedValue", Profile, "StarterEquipmentId", true, DataSourceUpdateMode.OnPropertyChanged);
            cboProfileStarterResourcesId.DataBindings.Add("SelectedValue", Profile, "StarterResourcesId", true, DataSourceUpdateMode.OnPropertyChanged);
            chkProfileCanLootEnemyContainers.DataBindings.Add("Checked", Profile, "CanLootEnemyContainers", true, DataSourceUpdateMode.OnPropertyChanged);

            cboProfileGameModeType.SelectedValue = Profile.GameModeType.ToString();
            cboProfileCastleDamageMode.SelectedValue = Profile.CastleDamageMode.ToString();
            cboProfileSiegeWeaponHealth.SelectedValue = Profile.SiegeWeaponHealth.ToString();
            cboProfilePlayerDamageMode.SelectedValue = Profile.PlayerDamageMode.ToString();
            cboProfileCastleHeartDamageMode.SelectedValue = Profile.CastleHeartDamageMode.ToString();
            cboProfilePvPProtectionMode.SelectedValue = Profile.PvPProtectionMode.ToString();
            cboProfileDeathContainerPermission.SelectedValue = Profile.DeathContainerPermission.ToString();
            cboProfileRelicSpawnType.SelectedValue = Profile.RelicSpawnType.ToString();

            //cboProfileGameModeType.DataBindings.Add("SelectedValue", Profile, "GameModeType", true, DataSourceUpdateMode.OnPropertyChanged);
            //cboProfileCastleDamageMode.DataBindings.Add("SelectedValue", Profile, "CastleDamageMode", true, DataSourceUpdateMode.OnPropertyChanged);
            //cboProfileSiegeWeaponHealth.DataBindings.Add("SelectedValue", Profile, "SiegeWeaponHealth", true, DataSourceUpdateMode.OnPropertyChanged);
            //cboProfilePlayerDamageMode.DataBindings.Add("SelectedValue", Profile, "PlayerDamageMode", true, DataSourceUpdateMode.OnPropertyChanged);
            //cboProfileCastleHeartDamageMode.DataBindings.Add("SelectedValue", Profile, "CastleHeartDamageMode", true, DataSourceUpdateMode.OnPropertyChanged);
            //cboProfilePvPProtectionMode.DataBindings.Add("SelectedValue", Profile, "PvPProtectionMode", true, DataSourceUpdateMode.OnPropertyChanged);
            //cboProfileDeathContainerPermission.DataBindings.Add("SelectedValue", Profile, "DeathContainerPermission", true, DataSourceUpdateMode.OnPropertyChanged);
            //cboProfileRelicSpawnType.DataBindings.Add("SelectedValue", Profile, "RelicSpawnType", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void profileHeader1_ClickSave(object sender, EventArgs e) {
            try {


                Profile.GameModeType = cboProfileGameModeType.SelectedValue.ToString().ParseEnum<GameModeType>();
                Profile.CastleDamageMode = cboProfileCastleDamageMode.SelectedValue.ToString().ParseEnum<CastleDamageMode>();
                Profile.SiegeWeaponHealth = cboProfileSiegeWeaponHealth.SelectedValue.ToString().ParseEnum<SiegeWeaponHealth>();
                Profile.PlayerDamageMode = cboProfilePlayerDamageMode.SelectedValue.ToString().ParseEnum<PlayerDamageMode>();
                Profile.CastleHeartDamageMode = cboProfileCastleHeartDamageMode.SelectedValue.ToString().ParseEnum<CastleHeartDamageMode>();
                Profile.PvPProtectionMode = cboProfilePvPProtectionMode.SelectedValue.ToString().ParseEnum<PvPProtectionMode>();
                Profile.DeathContainerPermission = cboProfileDeathContainerPermission.SelectedValue.ToString().ParseEnum<DeathContainerPermission>();
                Profile.RelicSpawnType = cboProfileRelicSpawnType.SelectedValue.ToString().ParseEnum<RelicSpawnType>();


                Profile.AutoManagement = automaticManagement1.GetRestartSettings();
                _plugin.Save();
                automaticManagement1.LoadGrid();
                var msg = _plugin.SaveSettingsToDisk();
                if (msg.Success) MessageBox.Show(msg.MessageText);
                else throw msg.Exception;
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

            this.RefreshBindings();
        }

        private void profileHeader1_ClickSync(object sender, EventArgs e) {
            _plugin.Sync();
        }

        private void profileHeader1_ClickUpgrade(object sender, EventArgs e) {
            try {

                ConnectionController.ServerControllers[Profile.Key].ShowServerInstallationOptions();
            }
            catch (Exception exception) {
                MessageBox.Show(exception.Message);
            }
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
    }
}