using System;
using System.Collections.Generic;
using System.Linq;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Helpers.Ini;
using OphiussaServerManager.Common.Ini;

namespace OphiussaServerManager.Common.Models {
    public class DinoSettingsList : SortableObservableCollection<DinoSettings> {
        public DinoSettingsList() {
            Reset();
        }

        public DinoSettingsList(AggregateIniValueList<DinoSpawn>       dinoSpawnWeightMultipliers,      StringIniValueList                     preventDinoTameClassNames, StringIniValueList preventBreedingForClassNames, AggregateIniValueList<NPCReplacement> npcReplacements,
                                AggregateIniValueList<ClassMultiplier> tamedDinoClassDamageMultipliers, AggregateIniValueList<ClassMultiplier> tamedDinoClassResistanceMultipliers,
                                AggregateIniValueList<ClassMultiplier> dinoClassDamageMultipliers,      AggregateIniValueList<ClassMultiplier> dinoClassResistanceMultipliers) {
            DinoSpawnWeightMultipliers          = dinoSpawnWeightMultipliers;
            PreventDinoTameClassNames           = preventDinoTameClassNames;
            PreventBreedingForClassNames        = preventBreedingForClassNames;
            NpcReplacements                     = npcReplacements;
            TamedDinoClassDamageMultipliers     = tamedDinoClassDamageMultipliers;
            TamedDinoClassResistanceMultipliers = tamedDinoClassResistanceMultipliers;
            DinoClassDamageMultipliers          = dinoClassDamageMultipliers;
            DinoClassResistanceMultipliers      = dinoClassResistanceMultipliers;
            Reset();
        }

        public AggregateIniValueList<DinoSpawn>       DinoSpawnWeightMultipliers          { get; }
        public StringIniValueList                     PreventDinoTameClassNames           { get; }
        public StringIniValueList                     PreventBreedingForClassNames        { get; }
        public AggregateIniValueList<NPCReplacement>  NpcReplacements                     { get; }
        public AggregateIniValueList<ClassMultiplier> TamedDinoClassDamageMultipliers     { get; }
        public AggregateIniValueList<ClassMultiplier> TamedDinoClassResistanceMultipliers { get; }
        public AggregateIniValueList<ClassMultiplier> DinoClassDamageMultipliers          { get; }
        public AggregateIniValueList<ClassMultiplier> DinoClassResistanceMultipliers      { get; }

        private DinoSettings CreateDinoSetting(string className, string mod, bool knownDino, bool hasNameTag, bool hasClassName) {
            string nameTag        = GameData.NameTagForClass(className);
            bool   isSpawnable    = GameData.IsSpawnableForClass(className);
            var    isTameable     = GameData.IsTameableForClass(className);
            var    isBreedingable = GameData.IsBreedingableForClass(className);

            var ds = new DinoSettings {
                                          ClassName = className,
                                          Mod       = mod,
                                          KnownDino = knownDino,
                                          NameTag   = nameTag,

                                          CanSpawn         = true,
                                          CanTame          = isTameable     != DinoTamable.False,
                                          CanBreeding      = isBreedingable != DinoBreedingable.False,
                                          ReplacementClass = className,

                                          SpawnWeightMultiplier                = DinoSpawn.DEFAULT_SPAWN_WEIGHT_MULTIPLIER,
                                          OverrideSpawnLimitPercentage         = DinoSpawn.DEFAULT_OVERRIDE_SPAWN_LIMIT_PERCENTAGE,
                                          SpawnLimitPercentage                 = DinoSpawn.DEFAULT_SPAWN_LIMIT_PERCENTAGE,
                                          OriginalSpawnWeightMultiplier        = DinoSpawn.DEFAULT_SPAWN_WEIGHT_MULTIPLIER,
                                          OriginalOverrideSpawnLimitPercentage = DinoSpawn.DEFAULT_OVERRIDE_SPAWN_LIMIT_PERCENTAGE,
                                          OriginalSpawnLimitPercentage         = DinoSpawn.DEFAULT_SPAWN_LIMIT_PERCENTAGE,

                                          TamedDamageMultiplier     = ClassMultiplier.DEFAULT_MULTIPLIER,
                                          TamedResistanceMultiplier = ClassMultiplier.DEFAULT_MULTIPLIER,
                                          WildDamageMultiplier      = ClassMultiplier.DEFAULT_MULTIPLIER,
                                          WildResistanceMultiplier  = ClassMultiplier.DEFAULT_MULTIPLIER,

                                          HasClassName   = hasClassName,
                                          HasNameTag     = hasNameTag,
                                          IsSpawnable    = isSpawnable,
                                          IsTameable     = isTameable,
                                          IsBreedingable = isBreedingable
                                      };
            ds.OriginalSetting = ds;
            return ds;
        }

        public List<DinoSettingsJSON> GetChangedSettingsList() {
            var ret = new List<DinoSettingsJSON>();
            ret.Clear();

            foreach (var currSetting in this) {
                var origSetting = (DinoSettings)currSetting.OriginalSetting;
                if (!isEqual(origSetting, currSetting))
                    //origSetting.OriginalSetting = null;
                    ret.Add(new DinoSettingsJSON {
                                                     ClassName                    = origSetting.ClassName,
                                                     ReplacementClass             = origSetting.ReplacementClass,
                                                     WildDamageMultiplier         = origSetting.WildDamageMultiplier,
                                                     TamedDamageMultiplier        = origSetting.TamedDamageMultiplier,
                                                     TamedResistanceMultiplier    = origSetting.TamedResistanceMultiplier,
                                                     WildResistanceMultiplier     = origSetting.WildResistanceMultiplier,
                                                     SpawnWeightMultiplier        = origSetting.SpawnWeightMultiplier,
                                                     SpawnLimitPercentage         = origSetting.SpawnLimitPercentage,
                                                     CanBreeding                  = origSetting.CanBreeding,
                                                     OverrideSpawnLimitPercentage = origSetting.OverrideSpawnLimitPercentage,
                                                     CanSpawn                     = origSetting.CanSpawn,
                                                     CanTame                      = origSetting.CanSpawn
                                                 });
            }

            return ret;
        }

        private bool isEqual(DinoSettings orig, DinoSettings setting) {
            //TODO: THIS COMPARISION SO WRONG, DO IT RIGHT WHEN IS READY TO SAVE
            return orig.ClassName                    == setting.ClassName                                 &&
                   orig.Mod                          == setting.Mod                                       &&
                   orig.KnownDino                    == setting.KnownDino                                 &&
                   orig.NameTag                      == setting.NameTag                                   &&
                   orig.CanSpawn                     == setting.CanSpawn                                  &&
                   orig.CanTame                      == setting.CanTame                                   &&
                   orig.CanBreeding                  == setting.CanBreeding                               &&
                   orig.ReplacementClass             == setting.ReplacementClass                          &&
                   orig.SpawnWeightMultiplier        == DinoSpawn.DEFAULT_SPAWN_WEIGHT_MULTIPLIER         &&
                   orig.OverrideSpawnLimitPercentage == DinoSpawn.DEFAULT_OVERRIDE_SPAWN_LIMIT_PERCENTAGE &&
                   orig.SpawnLimitPercentage         == DinoSpawn.DEFAULT_SPAWN_LIMIT_PERCENTAGE          &&
                   orig.TamedDamageMultiplier        == ClassMultiplier.DEFAULT_MULTIPLIER                &&
                   orig.TamedResistanceMultiplier    == ClassMultiplier.DEFAULT_MULTIPLIER                &&
                   orig.WildDamageMultiplier         == ClassMultiplier.DEFAULT_MULTIPLIER                &&
                   orig.WildResistanceMultiplier     == ClassMultiplier.DEFAULT_MULTIPLIER                &&
                   orig.HasClassName                 == setting.HasClassName                              &&
                   orig.HasNameTag                   == setting.HasNameTag;
        }


        public void Reset() {
            Clear();

            var dinoSpawns = GameData.GetDinoSpawns();
            foreach (var entry in dinoSpawns) Add(CreateDinoSetting(entry.ClassName, entry.Mod, entry.KnownDino, entry.DinoNameTag != null, true));

            Sort(d => d.NameSort);
        }

        public void RenderToView() {
            Reset();

            foreach (var entry in DinoSpawnWeightMultipliers.Where(e => !string.IsNullOrWhiteSpace(e.DinoNameTag)))
                if (this.Any(d => d.NameTag == entry.DinoNameTag)) {
                    foreach (var dinoSetting in this.Where(d => d.NameTag == entry.DinoNameTag)) {
                        dinoSetting.SpawnWeightMultiplier        = entry.SpawnWeightMultiplier;
                        dinoSetting.OverrideSpawnLimitPercentage = entry.OverrideSpawnLimitPercentage;
                        dinoSetting.SpawnLimitPercentage         = entry.SpawnLimitPercentage;

                        dinoSetting.OriginalSpawnWeightMultiplier        = entry.SpawnWeightMultiplier;
                        dinoSetting.OriginalOverrideSpawnLimitPercentage = entry.OverrideSpawnLimitPercentage;
                        dinoSetting.OriginalSpawnLimitPercentage         = entry.SpawnLimitPercentage;
                    }
                }
                else {
                    var dinoSetting = CreateDinoSetting(entry.DinoNameTag, entry.Mod, entry.KnownDino, true, false);
                    dinoSetting.SpawnWeightMultiplier        = entry.SpawnWeightMultiplier;
                    dinoSetting.OverrideSpawnLimitPercentage = entry.OverrideSpawnLimitPercentage;
                    dinoSetting.SpawnLimitPercentage         = entry.SpawnLimitPercentage;

                    dinoSetting.OriginalSpawnWeightMultiplier        = entry.SpawnWeightMultiplier;
                    dinoSetting.OriginalOverrideSpawnLimitPercentage = entry.OverrideSpawnLimitPercentage;
                    dinoSetting.OriginalSpawnLimitPercentage         = entry.SpawnLimitPercentage;

                    Add(dinoSetting);
                }

            foreach (string entry in PreventDinoTameClassNames.Where(e => !string.IsNullOrWhiteSpace(e)))
                if (this.Any(d => d.ClassName == entry)) {
                    foreach (var dinoSetting in this.Where(d => d.ClassName == entry && d.CanTame)) dinoSetting.CanTame = false;
                }
                else {
                    var dinoSetting = CreateDinoSetting(entry, GameData.MOD_UNKNOWN, false, false, true);
                    dinoSetting.CanTame = false;

                    Add(dinoSetting);
                }

            foreach (string entry in PreventBreedingForClassNames.Where(e => !string.IsNullOrWhiteSpace(e)))
                if (this.Any(d => d.ClassName == entry)) {
                    foreach (var dinoSetting in this.Where(d => d.ClassName == entry && d.CanBreeding)) dinoSetting.CanBreeding = false;
                }
                else {
                    var dinoSetting = CreateDinoSetting(entry, GameData.MOD_UNKNOWN, false, false, true);
                    dinoSetting.CanBreeding = false;

                    Add(dinoSetting);
                }

            foreach (var entry in NpcReplacements.Where(e => !string.IsNullOrWhiteSpace(e.FromClassName)))
                if (this.Any(d => d.ClassName == entry.FromClassName)) {
                    foreach (var dinoSetting in this.Where(d => d.ClassName == entry.FromClassName)) {
                        dinoSetting.CanSpawn         = !string.IsNullOrWhiteSpace(entry.ToClassName);
                        dinoSetting.ReplacementClass = dinoSetting.CanSpawn ? entry.ToClassName : dinoSetting.ClassName;
                    }
                }
                else {
                    var dinoSetting = CreateDinoSetting(entry.FromClassName, GameData.MOD_UNKNOWN, false, false, true);
                    dinoSetting.CanSpawn         = !string.IsNullOrWhiteSpace(entry.ToClassName);
                    dinoSetting.ReplacementClass = dinoSetting.CanSpawn ? entry.ToClassName : dinoSetting.ClassName;

                    Add(dinoSetting);
                }

            foreach (var entry in TamedDinoClassDamageMultipliers.Where(e => !string.IsNullOrWhiteSpace(e.ClassName)))
                if (this.Any(d => d.ClassName == entry.ClassName)) {
                    foreach (var dinoSetting in this.Where(d => d.ClassName == entry.ClassName && d.TamedDamageMultiplier != entry.Multiplier)) dinoSetting.TamedDamageMultiplier = entry.Multiplier;
                }
                else {
                    var dinoSetting = CreateDinoSetting(entry.ClassName, GameData.MOD_UNKNOWN, false, false, true);
                    dinoSetting.TamedDamageMultiplier = entry.Multiplier;

                    Add(dinoSetting);
                }

            foreach (var entry in TamedDinoClassResistanceMultipliers.Where(e => !string.IsNullOrWhiteSpace(e.ClassName)))
                if (this.Any(d => d.ClassName == entry.ClassName)) {
                    foreach (var dinoSetting in this.Where(d => d.ClassName == entry.ClassName && d.TamedResistanceMultiplier != entry.Multiplier)) dinoSetting.TamedResistanceMultiplier = entry.Multiplier;
                }
                else {
                    var dinoSetting = CreateDinoSetting(entry.ClassName, GameData.MOD_UNKNOWN, false, false, true);
                    dinoSetting.TamedResistanceMultiplier = entry.Multiplier;

                    Add(dinoSetting);
                }

            foreach (var entry in DinoClassDamageMultipliers.Where(e => !string.IsNullOrWhiteSpace(e.ClassName)))
                if (this.Any(d => d.ClassName == entry.ClassName)) {
                    foreach (var dinoSetting in this.Where(d => d.ClassName == entry.ClassName && d.WildDamageMultiplier != entry.Multiplier)) dinoSetting.WildDamageMultiplier = entry.Multiplier;
                }
                else {
                    var dinoSetting = CreateDinoSetting(entry.ClassName, GameData.MOD_UNKNOWN, false, false, true);
                    dinoSetting.WildDamageMultiplier = entry.Multiplier;

                    Add(dinoSetting);
                }

            foreach (var entry in DinoClassResistanceMultipliers.Where(e => !string.IsNullOrWhiteSpace(e.ClassName)))
                if (this.Any(d => d.ClassName == entry.ClassName)) {
                    foreach (var dinoSetting in this.Where(d => d.ClassName == entry.ClassName && d.WildResistanceMultiplier != entry.Multiplier)) dinoSetting.WildResistanceMultiplier = entry.Multiplier;
                }
                else {
                    var dinoSetting = CreateDinoSetting(entry.ClassName, GameData.MOD_UNKNOWN, false, false, true);
                    dinoSetting.WildResistanceMultiplier = entry.Multiplier;

                    Add(dinoSetting);
                }

            Sort(d => d.NameSort);
        }

        public void RenderToModel() {
            DinoSpawnWeightMultipliers.Clear();
            PreventDinoTameClassNames.Clear();
            PreventDinoTameClassNames.IsEnabled = true;
            PreventBreedingForClassNames.Clear();
            PreventBreedingForClassNames.IsEnabled = true;
            NpcReplacements.Clear();
            NpcReplacements.IsEnabled = true;
            TamedDinoClassDamageMultipliers.Clear();
            TamedDinoClassResistanceMultipliers.Clear();
            DinoClassDamageMultipliers.Clear();
            DinoClassResistanceMultipliers.Clear();

            foreach (var entry in this) {
                if (entry.HasNameTag && !string.IsNullOrWhiteSpace(entry.NameTag))
                    if (!entry.KnownDino                                                                              ||
                        !entry.OverrideSpawnLimitPercentage.Equals(DinoSpawn.DEFAULT_OVERRIDE_SPAWN_LIMIT_PERCENTAGE) ||
                        !entry.SpawnLimitPercentage.Equals(DinoSpawn.DEFAULT_SPAWN_LIMIT_PERCENTAGE)                  ||
                        !entry.SpawnWeightMultiplier.Equals(DinoSpawn.DEFAULT_SPAWN_WEIGHT_MULTIPLIER)) {
                        if (DinoSpawnWeightMultipliers.Any(d => d.DinoNameTag.Equals(entry.NameTag, StringComparison.OrdinalIgnoreCase))) {
                            foreach (var dinoSpawn in DinoSpawnWeightMultipliers.Where(d => d.DinoNameTag.Equals(entry.NameTag, StringComparison.OrdinalIgnoreCase)))
                                if (entry.SpawnWeightMultiplier        != entry.OriginalSpawnWeightMultiplier        ||
                                    entry.OverrideSpawnLimitPercentage != entry.OriginalOverrideSpawnLimitPercentage ||
                                    entry.SpawnLimitPercentage         != entry.OriginalSpawnLimitPercentage) {
                                    dinoSpawn.OverrideSpawnLimitPercentage = entry.OverrideSpawnLimitPercentage;
                                    dinoSpawn.SpawnLimitPercentage         = entry.SpawnLimitPercentage;
                                    dinoSpawn.SpawnWeightMultiplier        = entry.SpawnWeightMultiplier;
                                }
                        }
                        else {
                            DinoSpawnWeightMultipliers.Add(new DinoSpawn {
                                                                             ClassName                    = entry.ClassName,
                                                                             DinoNameTag                  = entry.NameTag,
                                                                             OverrideSpawnLimitPercentage = entry.OverrideSpawnLimitPercentage,
                                                                             SpawnLimitPercentage         = entry.SpawnLimitPercentage,
                                                                             SpawnWeightMultiplier        = entry.SpawnWeightMultiplier
                                                                         });
                        }
                    }

                if (entry.HasClassName && !string.IsNullOrWhiteSpace(entry.ClassName)) {
                    if (entry.IsTameable != DinoTamable.False && !entry.CanTame) PreventDinoTameClassNames.Add(entry.ClassName);

                    if (entry.IsBreedingable != DinoBreedingable.False && !entry.CanBreeding) PreventBreedingForClassNames.Add(entry.ClassName);

                    NpcReplacements.Add(new NPCReplacement { FromClassName = entry.ClassName, ToClassName = entry.CanSpawn ? entry.ReplacementClass : string.Empty });

                    if (entry.IsTameable != DinoTamable.False) {
                        // check if the value has changed.
                        if (!entry.TamedDamageMultiplier.Equals(ClassMultiplier.DEFAULT_MULTIPLIER)) TamedDinoClassDamageMultipliers.Add(new ClassMultiplier { ClassName = entry.ClassName, Multiplier = entry.TamedDamageMultiplier });

                        // check if the value has changed.
                        if (!entry.TamedResistanceMultiplier.Equals(ClassMultiplier.DEFAULT_MULTIPLIER)) TamedDinoClassResistanceMultipliers.Add(new ClassMultiplier { ClassName = entry.ClassName, Multiplier = entry.TamedResistanceMultiplier });
                    }

                    // check if the value has changed.
                    if (!entry.WildDamageMultiplier.Equals(ClassMultiplier.DEFAULT_MULTIPLIER)) DinoClassDamageMultipliers.Add(new ClassMultiplier { ClassName = entry.ClassName, Multiplier = entry.WildDamageMultiplier });

                    // check if the value has changed.
                    if (!entry.WildResistanceMultiplier.Equals(ClassMultiplier.DEFAULT_MULTIPLIER)) DinoClassResistanceMultipliers.Add(new ClassMultiplier { ClassName = entry.ClassName, Multiplier = entry.WildResistanceMultiplier });
                }
            }
        }
    }
}