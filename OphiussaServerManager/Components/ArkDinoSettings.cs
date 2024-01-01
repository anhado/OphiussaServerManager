using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Tools;

namespace OphiussaServerManager.Components {
    public partial class ArkDinoSettings : UserControl {
        private ArkProfile _profile;

        public ArkDinoSettings() {
            InitializeComponent();
        }


        public void LoadData(ref ArkProfile profile) {
            _profile = profile;
            var sw = new Stopwatch();

            sw.Start();


            UsefullTools.LoadValuesToFields(_profile, Controls);


            profile.DinoSettings.Reset();
            foreach (var dinoSetting in profile.DinoSettings) {
                var j = profile.ChangedDinoSettings?.Find(ds => ds.ClassName == dinoSetting.ClassName);
                if (j != null) {
                    dinoSetting.ReplacementClass             = j.ReplacementClass;
                    dinoSetting.CanTame                      = j.CanTame;
                    dinoSetting.CanBreeding                  = j.CanBreeding;
                    dinoSetting.CanSpawn                     = j.CanSpawn;
                    dinoSetting.OverrideSpawnLimitPercentage = j.OverrideSpawnLimitPercentage;
                    dinoSetting.SpawnWeightMultiplier        = j.SpawnWeightMultiplier;
                    dinoSetting.SpawnLimitPercentage         = j.SpawnLimitPercentage;
                    dinoSetting.TamedDamageMultiplier        = j.TamedDamageMultiplier;
                    dinoSetting.TamedResistanceMultiplier    = j.TamedResistanceMultiplier;
                    dinoSetting.WildDamageMultiplier         = j.WildDamageMultiplier;
                    dinoSetting.WildResistanceMultiplier     = j.WildResistanceMultiplier;
                }
            }

            bindingSource2.Clear();
            bindingSource2.AddRange(profile.DinoSettings.ToArray());


            var cbo = replacementClassDataGridViewTextBoxColumn;
            //cbo.Items.Clear();
            cbo.DataSource    = GameData.GetDinoSpawns().ToArray();
            cbo.DisplayMember = "DinoNameTag";
            cbo.ValueMember   = "ClassName";

            //dinoSettingsBindingSource.DataSource = profile.DinoSettings;


            if (profile.PerLevelStatsMultiplier_DinoWild.Count != 12) profile.PerLevelStatsMultiplier_DinoWild.Reset();
            chkPerLevelStatsMultiplierWild.Checked            = profile.PerLevelStatsMultiplier_DinoWild.IsEnabled;
            txttbPerLevelStatsMultiplierWildHealth.Value      = profile.PerLevelStatsMultiplier_DinoWild[0];
            txttbPerLevelStatsMultiplierWildStamina.Value     = profile.PerLevelStatsMultiplier_DinoWild[1];
            txttbPerLevelStatsMultiplierWildOxygen.Value      = profile.PerLevelStatsMultiplier_DinoWild[2];
            txttbPerLevelStatsMultiplierWildFood.Value        = profile.PerLevelStatsMultiplier_DinoWild[3];
            txttbPerLevelStatsMultiplierWildTemperature.Value = profile.PerLevelStatsMultiplier_DinoWild[4];
            txttbPerLevelStatsMultiplierWildWeight.Value      = profile.PerLevelStatsMultiplier_DinoWild[5];
            txttbPerLevelStatsMultiplierWildDamage.Value      = profile.PerLevelStatsMultiplier_DinoWild[6];
            txttbPerLevelStatsMultiplierWildSped.Value        = profile.PerLevelStatsMultiplier_DinoWild[7];
            txttbPerLevelStatsMultiplierWildCrafting.Value    = profile.PerLevelStatsMultiplier_DinoWild[8];
            if (profile.PerLevelStatsMultiplier_DinoTamed.Count != 12) profile.PerLevelStatsMultiplier_DinoTamed.Reset();
            chkPerLevelStatsMultiplierTamed.Checked            = profile.PerLevelStatsMultiplier_DinoTamed.IsEnabled;
            txttbPerLevelStatsMultiplierTamedHealth.Value      = profile.PerLevelStatsMultiplier_DinoTamed[0];
            txttbPerLevelStatsMultiplierTamedStamina.Value     = profile.PerLevelStatsMultiplier_DinoTamed[1];
            txttbPerLevelStatsMultiplierTamedOxygen.Value      = profile.PerLevelStatsMultiplier_DinoTamed[2];
            txttbPerLevelStatsMultiplierTamedFood.Value        = profile.PerLevelStatsMultiplier_DinoTamed[3];
            txttbPerLevelStatsMultiplierTamedTemperature.Value = profile.PerLevelStatsMultiplier_DinoTamed[4];
            txttbPerLevelStatsMultiplierTamedWeight.Value      = profile.PerLevelStatsMultiplier_DinoTamed[5];
            txttbPerLevelStatsMultiplierTamedDamage.Value      = profile.PerLevelStatsMultiplier_DinoTamed[6];
            txttbPerLevelStatsMultiplierTamedSpeed.Value       = profile.PerLevelStatsMultiplier_DinoTamed[7];
            txttbPerLevelStatsMultiplierTamedCrafting.Value    = profile.PerLevelStatsMultiplier_DinoTamed[8];
            if (profile.PerLevelStatsMultiplier_DinoTamed_Add.Count != 12) profile.PerLevelStatsMultiplier_DinoTamed_Add.Reset();
            chkPerLevelStatMultiplierTamedAdd.Checked             = profile.PerLevelStatsMultiplier_DinoTamed_Add.IsEnabled;
            txttbPerLevelStatsMultiplierTamedAddHealth.Value      = profile.PerLevelStatsMultiplier_DinoTamed_Add[0];
            txttbPerLevelStatsMultiplierTamedAddStamina.Value     = profile.PerLevelStatsMultiplier_DinoTamed_Add[1];
            txttbPerLevelStatsMultiplierTamedAddTorpidity.Value   = profile.PerLevelStatsMultiplier_DinoTamed_Add[2];
            txttbPerLevelStatsMultiplierTamedAddOxygen.Value      = profile.PerLevelStatsMultiplier_DinoTamed_Add[3];
            txttbPerLevelStatsMultiplierTamedAddFood.Value        = profile.PerLevelStatsMultiplier_DinoTamed_Add[4];
            txttbPerLevelStatsMultiplierTamedAddWater.Value       = profile.PerLevelStatsMultiplier_DinoTamed_Add[5];
            txttbPerLevelStatsMultiplierTamedAddTemperature.Value = profile.PerLevelStatsMultiplier_DinoTamed_Add[6];
            txttbPerLevelStatsMultiplierTamedAddWeight.Value      = profile.PerLevelStatsMultiplier_DinoTamed_Add[7];
            txttbPerLevelStatsMultiplierTamedAddDamage.Value      = profile.PerLevelStatsMultiplier_DinoTamed_Add[8];
            txttbPerLevelStatsMultiplierTamedAddSpeed.Value       = profile.PerLevelStatsMultiplier_DinoTamed_Add[9];
            txttbPerLevelStatsMultiplierTamedAddFortitude.Value   = profile.PerLevelStatsMultiplier_DinoTamed_Add[10];
            txttbPerLevelStatsMultiplierTamedAddCrafting.Value    = profile.PerLevelStatsMultiplier_DinoTamed_Add[11];
            if (profile.PerLevelStatsMultiplier_DinoTamed_Affinity.Count != 12) profile.PerLevelStatsMultiplier_DinoTamed_Affinity.Reset();
            chkPerLevelStatsMultiplierTamedAffinity.Checked            = profile.PerLevelStatsMultiplier_DinoTamed_Affinity.IsEnabled;
            txttbPerLevelStatsMultiplierTamedAffinityHealth.Value      = profile.PerLevelStatsMultiplier_DinoTamed_Affinity[0];
            txttbPerLevelStatsMultiplierTamedAffinityStamina.Value     = profile.PerLevelStatsMultiplier_DinoTamed_Affinity[1];
            txttbPerLevelStatsMultiplierTamedAffinityTorpidity.Value   = profile.PerLevelStatsMultiplier_DinoTamed_Affinity[2];
            txttbPerLevelStatsMultiplierTamedAffinityOxygen.Value      = profile.PerLevelStatsMultiplier_DinoTamed_Affinity[3];
            txttbPerLevelStatsMultiplierTamedAffinityFood.Value        = profile.PerLevelStatsMultiplier_DinoTamed_Affinity[4];
            txttbPerLevelStatsMultiplierTamedAffinityWater.Value       = profile.PerLevelStatsMultiplier_DinoTamed_Affinity[5];
            txttbPerLevelStatsMultiplierTamedAffinityTemperature.Value = profile.PerLevelStatsMultiplier_DinoTamed_Affinity[6];
            txttbPerLevelStatsMultiplierTamedAffinityWeight.Value      = profile.PerLevelStatsMultiplier_DinoTamed_Affinity[7];
            txttbPerLevelStatsMultiplierTamedAffinityDamage.Value      = profile.PerLevelStatsMultiplier_DinoTamed_Affinity[8];
            txttbPerLevelStatsMultiplierTamedAffinitySpeed.Value       = profile.PerLevelStatsMultiplier_DinoTamed_Affinity[9];
            txttbPerLevelStatsMultiplierTamedAffinityFortitude.Value   = profile.PerLevelStatsMultiplier_DinoTamed_Affinity[10];
            txttbPerLevelStatsMultiplierTamedAffinityCrafting.Value    = profile.PerLevelStatsMultiplier_DinoTamed_Affinity[11];
            if (profile.MutagenLevelBoost.Count != 12) profile.MutagenLevelBoost.Reset();
            chkMutagenLevelBoostWild.Checked            = profile.MutagenLevelBoost.IsEnabled;
            txttbMutagenLevelBoostWildHealth.Value      = profile.MutagenLevelBoost[0];
            txttbMutagenLevelBoostWildStamina.Value     = profile.MutagenLevelBoost[1];
            txttbMutagenLevelBoostWildTorpidity.Value   = profile.MutagenLevelBoost[2];
            txttbMutagenLevelBoostWildOxygen.Value      = profile.MutagenLevelBoost[3];
            txttbMutagenLevelBoostWildFood.Value        = profile.MutagenLevelBoost[4];
            txttbMutagenLevelBoostWildWater.Value       = profile.MutagenLevelBoost[5];
            txttbMutagenLevelBoostWildTemperature.Value = profile.MutagenLevelBoost[6];
            txttbMutagenLevelBoostWildWeight.Value      = profile.MutagenLevelBoost[7];
            txttbMutagenLevelBoostWildDamage.Value      = profile.MutagenLevelBoost[8];
            txttbMutagenLevelBoostWildSpeed.Value       = profile.MutagenLevelBoost[9];
            txttbMutagenLevelBoostWildFortitude.Value   = profile.MutagenLevelBoost[10];
            txttbMutagenLevelBoostWildCrafting.Value    = profile.MutagenLevelBoost[11];
            if (profile.MutagenLevelBoost_Bred.Count != 12) profile.MutagenLevelBoost_Bred.Reset();
            chkMutagenLevelBoostWild.Checked            = profile.MutagenLevelBoost_Bred.IsEnabled;
            txttbMutagenLevelBoostBredHealth.Value      = profile.MutagenLevelBoost_Bred[0];
            txttbMutagenLevelBoostBredStamina.Value     = profile.MutagenLevelBoost_Bred[1];
            txttbMutagenLevelBoostBredTorpidity.Value   = profile.MutagenLevelBoost_Bred[2];
            txttbMutagenLevelBoostBredOxygen.Value      = profile.MutagenLevelBoost_Bred[3];
            txttbMutagenLevelBoostBredFood.Value        = profile.MutagenLevelBoost_Bred[4];
            txttbMutagenLevelBoostBredWater.Value       = profile.MutagenLevelBoost_Bred[5];
            txttbMutagenLevelBoostBredTemperature.Value = profile.MutagenLevelBoost_Bred[6];
            txttbMutagenLevelBoostBredWeigth.Value      = profile.MutagenLevelBoost_Bred[7];
            txttbMutagenLevelBoostBredDamage.Value      = profile.MutagenLevelBoost_Bred[8];
            txttbMutagenLevelBoostBredSpeed.Value       = profile.MutagenLevelBoost_Bred[9];
            txttbMutagenLevelBoostBredFortitude.Value   = profile.MutagenLevelBoost_Bred[10];
            txttbMutagenLevelBoostBredCrafting.Value    = profile.MutagenLevelBoost_Bred[11];


            UsefullTools.ManageCheckGroupBox(chkPerLevelStatsMultiplierTamedAffinity, groupBox3);
            UsefullTools.ManageCheckGroupBox(chkAllowRaidDinoFeeding,                 groupBox33);
            UsefullTools.ManageCheckGroupBox(chkChangeFlyerRiding,                    groupBox34);
            UsefullTools.ManageCheckGroupBox(chkPerLevelStatsMultiplierWild,          groupBox31);
            UsefullTools.ManageCheckGroupBox(chkPerLevelStatsMultiplierTamed,         groupBox1);
            UsefullTools.ManageCheckGroupBox(chkPerLevelStatMultiplierTamedAdd,       groupBox2);
            UsefullTools.ManageCheckGroupBox(chkMutagenLevelBoostWild,                groupBox4);
            UsefullTools.ManageCheckGroupBox(chkMutagenLevelBoostBred,                groupBox5);


            Console.WriteLine("ArkDinoSettings={0}", sw.Elapsed.TotalSeconds);
        }

        public void GetData(ref ArkProfile profile) {
            var sw = new Stopwatch();

            sw.Start();
            UsefullTools.LoadFieldsToObject(ref _profile, Controls);

            //TODO:FILL DINO COSTUMIZATION
            _profile.DinoSettings.Clear();
            foreach (object o in bindingSource2.List) {
                _profile.DinoSettings.Add((DinoSettings)o);
            }

            _profile.ChangedDinoSettings = _profile.DinoSettings.GetChangedSettingsList();

            _profile.PreventDinoTameClassNames.Clear();
            foreach (var setting in _profile.ChangedDinoSettings) {
                DinoSettings ds = _profile.DinoSettings.First(f => f.ClassName == setting.ClassName);

                if (ds != null) {
                    if (setting.OverrideSpawnLimitPercentage != ds.OriginalOverrideSpawnLimitPercentage ||
                        setting.SpawnLimitPercentage         != ds.OriginalSpawnLimitPercentage         ||
                        setting.SpawnWeightMultiplier        != ds.OriginalSpawnWeightMultiplier)
                        _profile.DinoSpawnWeightMultipliers.Add(new DinoSpawn() {
                                                                                    ClassName                    = ds.ClassName,
                                                                                    OverrideSpawnLimitPercentage = setting.OverrideSpawnLimitPercentage,
                                                                                    SpawnLimitPercentage         = setting.SpawnLimitPercentage,
                                                                                    SpawnWeightMultiplier        = setting.SpawnWeightMultiplier,
                                                                                    Mod                          = ds.Mod,
                                                                                    KnownDino                    = ds.KnownDino,
                                                                                    DinoNameTag                  = ds.NameTag
                                                                                });

                    if (setting.TamedDamageMultiplier != DinoSpawn.DEFAULT_SPAWN_WEIGHT_MULTIPLIER)
                        _profile.TamedDinoClassDamageMultipliers.Add(new ClassMultiplier() {
                                                                                               ClassName  = ds.ClassName,
                                                                                               Multiplier = setting.TamedDamageMultiplier
                                                                                           });

                    if (setting.TamedResistanceMultiplier != DinoSpawn.DEFAULT_SPAWN_WEIGHT_MULTIPLIER)
                        _profile.TamedDinoClassResistanceMultipliers.Add(new ClassMultiplier() {
                                                                                                   ClassName  = ds.ClassName,
                                                                                                   Multiplier = setting.TamedResistanceMultiplier
                                                                                               });

                    if (setting.WildDamageMultiplier != DinoSpawn.DEFAULT_SPAWN_WEIGHT_MULTIPLIER)
                        _profile.DinoClassDamageMultipliers.Add(new ClassMultiplier() {
                                                                                          ClassName  = ds.ClassName,
                                                                                          Multiplier = setting.WildDamageMultiplier
                                                                                      });

                    if (setting.WildResistanceMultiplier != DinoSpawn.DEFAULT_SPAWN_WEIGHT_MULTIPLIER)
                        _profile.DinoClassResistanceMultipliers.Add(new ClassMultiplier() {
                                                                                              ClassName  = ds.ClassName,
                                                                                              Multiplier = setting.WildResistanceMultiplier
                                                                                          });


                    if (setting.ClassName != setting.ReplacementClass)
                        _profile.NPCReplacements.Add(new NPCReplacement() {
                                                                              FromClassName = ds.ClassName,
                                                                              ToClassName   = setting.ReplacementClass
                                                                          });

                    if (!setting.CanSpawn && ds.IsSpawnable == true) {
                        _profile.PreventDinoTameClassNames.Add(setting.ReplacementClass);
                    }

                    if (!setting.CanTame && ds.IsTameable == DinoTamable.True) {
                        _profile.PreventDinoTameClassNames.Add(setting.ReplacementClass);
                    }

                    if (!setting.CanBreeding && ds.IsBreedingable == DinoBreedingable.True) {
                        _profile.PreventDinoTameClassNames.Add(setting.ReplacementClass);
                    }
                }
            }

            _profile.PerLevelStatsMultiplier_DinoWild.IsEnabled = chkPerLevelStatsMultiplierWild.Checked;
            if (_profile.PerLevelStatsMultiplier_DinoWild.IsEnabled) {
                _profile.PerLevelStatsMultiplier_DinoWild[0] = txttbPerLevelStatsMultiplierWildHealth.Value;
                _profile.PerLevelStatsMultiplier_DinoWild[1] = txttbPerLevelStatsMultiplierWildStamina.Value;
                _profile.PerLevelStatsMultiplier_DinoWild[2] = txttbPerLevelStatsMultiplierWildOxygen.Value;
                _profile.PerLevelStatsMultiplier_DinoWild[3] = txttbPerLevelStatsMultiplierWildFood.Value;
                _profile.PerLevelStatsMultiplier_DinoWild[4] = txttbPerLevelStatsMultiplierWildTemperature.Value;
                _profile.PerLevelStatsMultiplier_DinoWild[5] = txttbPerLevelStatsMultiplierWildWeight.Value;
                _profile.PerLevelStatsMultiplier_DinoWild[6] = txttbPerLevelStatsMultiplierWildDamage.Value;
                _profile.PerLevelStatsMultiplier_DinoWild[7] = txttbPerLevelStatsMultiplierWildSped.Value;
                _profile.PerLevelStatsMultiplier_DinoWild[8] = txttbPerLevelStatsMultiplierWildCrafting.Value;
            }
            else {
                _profile.PerLevelStatsMultiplier_DinoWild.Reset();
            }

            _profile.PerLevelStatsMultiplier_DinoTamed.IsEnabled = chkPerLevelStatsMultiplierTamed.Checked;
            if (_profile.PerLevelStatsMultiplier_DinoTamed.IsEnabled) {
                _profile.PerLevelStatsMultiplier_DinoTamed[0] = txttbPerLevelStatsMultiplierTamedHealth.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed[1] = txttbPerLevelStatsMultiplierTamedStamina.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed[2] = txttbPerLevelStatsMultiplierTamedOxygen.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed[3] = txttbPerLevelStatsMultiplierTamedFood.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed[4] = txttbPerLevelStatsMultiplierTamedTemperature.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed[5] = txttbPerLevelStatsMultiplierTamedWeight.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed[6] = txttbPerLevelStatsMultiplierTamedDamage.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed[7] = txttbPerLevelStatsMultiplierTamedSpeed.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed[8] = txttbPerLevelStatsMultiplierTamedCrafting.Value;
            }
            else {
                _profile.PerLevelStatsMultiplier_DinoWild.Reset();
            }

            _profile.PerLevelStatsMultiplier_DinoTamed_Add.IsEnabled = chkPerLevelStatMultiplierTamedAdd.Checked;
            if (_profile.PerLevelStatsMultiplier_DinoTamed_Add.IsEnabled) {
                _profile.PerLevelStatsMultiplier_DinoTamed_Add[0]  = txttbPerLevelStatsMultiplierTamedAddHealth.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed_Add[1]  = txttbPerLevelStatsMultiplierTamedAddStamina.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed_Add[2]  = txttbPerLevelStatsMultiplierTamedAddTorpidity.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed_Add[3]  = txttbPerLevelStatsMultiplierTamedAddOxygen.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed_Add[4]  = txttbPerLevelStatsMultiplierTamedAddFood.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed_Add[5]  = txttbPerLevelStatsMultiplierTamedAddWater.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed_Add[6]  = txttbPerLevelStatsMultiplierTamedAddTemperature.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed_Add[7]  = txttbPerLevelStatsMultiplierTamedAddWeight.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed_Add[8]  = txttbPerLevelStatsMultiplierTamedAddDamage.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed_Add[9]  = txttbPerLevelStatsMultiplierTamedAddSpeed.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed_Add[10] = txttbPerLevelStatsMultiplierTamedAddFortitude.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed_Add[11] = txttbPerLevelStatsMultiplierTamedAddCrafting.Value;
            }
            else {
                _profile.PerLevelStatsMultiplier_DinoTamed_Add.Reset();
            }

            _profile.PerLevelStatsMultiplier_DinoTamed_Affinity.IsEnabled = chkPerLevelStatsMultiplierTamedAffinity.Checked;
            if (_profile.PerLevelStatsMultiplier_DinoTamed_Affinity.IsEnabled) {
                _profile.PerLevelStatsMultiplier_DinoTamed_Affinity[0]  = txttbPerLevelStatsMultiplierTamedAffinityHealth.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed_Affinity[1]  = txttbPerLevelStatsMultiplierTamedAffinityStamina.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed_Affinity[2]  = txttbPerLevelStatsMultiplierTamedAffinityTorpidity.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed_Affinity[3]  = txttbPerLevelStatsMultiplierTamedAffinityOxygen.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed_Affinity[4]  = txttbPerLevelStatsMultiplierTamedAffinityFood.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed_Affinity[5]  = txttbPerLevelStatsMultiplierTamedAffinityWater.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed_Affinity[6]  = txttbPerLevelStatsMultiplierTamedAffinityTemperature.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed_Affinity[7]  = txttbPerLevelStatsMultiplierTamedAffinityWeight.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed_Affinity[8]  = txttbPerLevelStatsMultiplierTamedAffinityDamage.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed_Affinity[9]  = txttbPerLevelStatsMultiplierTamedAffinitySpeed.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed_Affinity[10] = txttbPerLevelStatsMultiplierTamedAffinityFortitude.Value;
                _profile.PerLevelStatsMultiplier_DinoTamed_Affinity[11] = txttbPerLevelStatsMultiplierTamedAffinityCrafting.Value;
            }
            else {
                _profile.PerLevelStatsMultiplier_DinoTamed_Affinity.Reset();
            }

            _profile.MutagenLevelBoost.IsEnabled = chkMutagenLevelBoostWild.Checked;
            if (_profile.MutagenLevelBoost.IsEnabled) {
                _profile.MutagenLevelBoost[0]  = txttbMutagenLevelBoostWildHealth.Value.ToString(CultureInfo.InvariantCulture).ToInt();
                _profile.MutagenLevelBoost[1]  = txttbMutagenLevelBoostWildStamina.Value.ToString(CultureInfo.InvariantCulture).ToInt();
                _profile.MutagenLevelBoost[2]  = txttbMutagenLevelBoostWildTorpidity.Value.ToString(CultureInfo.InvariantCulture).ToInt();
                _profile.MutagenLevelBoost[3]  = txttbMutagenLevelBoostWildOxygen.Value.ToString(CultureInfo.InvariantCulture).ToInt();
                _profile.MutagenLevelBoost[4]  = txttbMutagenLevelBoostWildFood.Value.ToString(CultureInfo.InvariantCulture).ToInt();
                _profile.MutagenLevelBoost[5]  = txttbMutagenLevelBoostWildWater.Value.ToString(CultureInfo.InvariantCulture).ToInt();
                _profile.MutagenLevelBoost[6]  = txttbMutagenLevelBoostWildTemperature.Value.ToString(CultureInfo.InvariantCulture).ToInt();
                _profile.MutagenLevelBoost[7]  = txttbMutagenLevelBoostWildWeight.Value.ToString(CultureInfo.InvariantCulture).ToInt();
                _profile.MutagenLevelBoost[8]  = txttbMutagenLevelBoostWildDamage.Value.ToString(CultureInfo.InvariantCulture).ToInt();
                _profile.MutagenLevelBoost[9]  = txttbMutagenLevelBoostWildSpeed.Value.ToString(CultureInfo.InvariantCulture).ToInt();
                _profile.MutagenLevelBoost[10] = txttbMutagenLevelBoostWildFortitude.Value.ToString(CultureInfo.InvariantCulture).ToInt();
                _profile.MutagenLevelBoost[11] = txttbMutagenLevelBoostWildCrafting.Value.ToString(CultureInfo.InvariantCulture).ToInt();
            }
            else {
                _profile.MutagenLevelBoost.Reset();
            }

            _profile.MutagenLevelBoost_Bred.IsEnabled = chkMutagenLevelBoostBred.Checked;
            if (_profile.MutagenLevelBoost_Bred.IsEnabled) {
                _profile.MutagenLevelBoost_Bred[0]  = txttbMutagenLevelBoostBredHealth.Value.ToString(CultureInfo.InvariantCulture).ToInt();
                _profile.MutagenLevelBoost_Bred[1]  = txttbMutagenLevelBoostBredStamina.Value.ToString(CultureInfo.InvariantCulture).ToInt();
                _profile.MutagenLevelBoost_Bred[2]  = txttbMutagenLevelBoostBredTorpidity.Value.ToString(CultureInfo.InvariantCulture).ToInt();
                _profile.MutagenLevelBoost_Bred[3]  = txttbMutagenLevelBoostBredOxygen.Value.ToString(CultureInfo.InvariantCulture).ToInt();
                _profile.MutagenLevelBoost_Bred[4]  = txttbMutagenLevelBoostBredFood.Value.ToString(CultureInfo.InvariantCulture).ToInt();
                _profile.MutagenLevelBoost_Bred[5]  = txttbMutagenLevelBoostBredWater.Value.ToString(CultureInfo.InvariantCulture).ToInt();
                _profile.MutagenLevelBoost_Bred[6]  = txttbMutagenLevelBoostBredTemperature.Value.ToString(CultureInfo.InvariantCulture).ToInt();
                _profile.MutagenLevelBoost_Bred[7]  = txttbMutagenLevelBoostBredWeigth.Value.ToString(CultureInfo.InvariantCulture).ToInt();
                _profile.MutagenLevelBoost_Bred[8]  = txttbMutagenLevelBoostBredDamage.Value.ToString(CultureInfo.InvariantCulture).ToInt();
                _profile.MutagenLevelBoost_Bred[9]  = txttbMutagenLevelBoostBredSpeed.Value.ToString(CultureInfo.InvariantCulture).ToInt();
                _profile.MutagenLevelBoost_Bred[10] = txttbMutagenLevelBoostBredFortitude.Value.ToString(CultureInfo.InvariantCulture).ToInt();
                _profile.MutagenLevelBoost_Bred[11] = txttbMutagenLevelBoostBredCrafting.Value.ToString(CultureInfo.InvariantCulture).ToInt();
            }
            else {
                _profile.MutagenLevelBoost_Bred.Reset();
            }

            sw.Stop();

            Console.WriteLine("ArkDinoSettings={0}", sw.Elapsed.TotalSeconds);
        }

        private void ArkDinoSettings_Load(object sender, EventArgs e) {
        }

        private void chkPerLevelStatsMultiplierTamedAffinity_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkPerLevelStatsMultiplierTamedAffinity, groupBox3);
        }

        private void chkAllowRaidDinoFeeding_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkAllowRaidDinoFeeding, groupBox33);
        }

        private void chkChangeFlyerRiding_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkChangeFlyerRiding, groupBox34);
        }

        private void chkPerLevelStatsMultiplierWild_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkPerLevelStatsMultiplierWild, groupBox31);
        }

        private void chkPerLevelStatsMultiplierTamed_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkPerLevelStatsMultiplierTamed, groupBox1);
        }

        private void chkPerLevelStatMultiplierTamedAdd_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkPerLevelStatMultiplierTamedAdd, groupBox2);
        }

        private void chkMutagenLevelBoostWild_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkMutagenLevelBoostWild, groupBox4);
        }

        private void chkMutagenLevelBoostBred_CheckedChanged(object sender, EventArgs e) {
            UsefullTools.ManageCheckGroupBox(chkMutagenLevelBoostBred, groupBox5);
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) {
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e) {
            try {
                var obj = (DinoSettings)bindingSource2[e.RowIndex];
                if (obj != null) {
                    if (obj.IsSpawnable == false && e.ColumnIndex == 2) {
                        e.PaintBackground(e.ClipBounds, true);
                        e.Handled = true;
                    }

                    if (obj.IsTameable == DinoTamable.False && e.ColumnIndex == 3) {
                        e.PaintBackground(e.ClipBounds, true);
                        e.Handled = true;
                    }

                    if (obj.IsBreedingable == DinoBreedingable.False && e.ColumnIndex == 4) {
                        e.PaintBackground(e.ClipBounds, true);
                        e.Handled = true;
                    }

                    if (obj.IsSpawnable == false && e.ColumnIndex == 6) {
                        e.PaintBackground(e.ClipBounds, true);
                        e.Handled = true;
                    }

                    if (obj.IsSpawnable == false && e.ColumnIndex == 7) {
                        e.PaintBackground(e.ClipBounds, true);
                        e.Handled = true;
                    }

                    if (obj.IsSpawnable == false && e.ColumnIndex == 8) {
                        e.PaintBackground(e.ClipBounds, true);
                        e.Handled = true;
                    }

                    if (obj.IsTameable == DinoTamable.False && e.ColumnIndex == 9) {
                        e.PaintBackground(e.ClipBounds, true);
                        e.Handled = true;
                    }

                    if (obj.IsTameable == DinoTamable.False && e.ColumnIndex == 10) {
                        e.PaintBackground(e.ClipBounds, true);
                        e.Handled = true;
                    }
                }
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                OphiussaLogger.Logger.Error(exception);
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e) {
            try {
                var obj = (DinoSettings)bindingSource2[e.RowIndex];
                if (obj != null) {
                    if (obj.IsSpawnable == false && e.ColumnIndex == 2) {
                        e.Cancel = true;
                    }

                    if (obj.IsTameable == DinoTamable.False && e.ColumnIndex == 3) {
                        e.Cancel = true;
                    }

                    if (obj.IsBreedingable == DinoBreedingable.False && e.ColumnIndex == 4) {
                        e.Cancel = true;
                    }

                    if (obj.IsSpawnable == false && e.ColumnIndex == 6) {
                        e.Cancel = true;
                    }

                    if (obj.IsSpawnable == false && e.ColumnIndex == 7) {
                        e.Cancel = true;
                    }

                    if (obj.IsSpawnable == false && e.ColumnIndex == 8) {
                        e.Cancel = true;
                    }

                    if (obj.IsTameable == DinoTamable.False && e.ColumnIndex == 9) {
                        e.Cancel = true;
                    }

                    if (obj.IsTameable == DinoTamable.False && e.ColumnIndex == 10) {
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception exception) {
                Console.WriteLine(exception);
                OphiussaLogger.Logger.Error(exception);
            }
        }
    }
}