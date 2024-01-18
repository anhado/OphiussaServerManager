using System.Collections.Generic;
using OphiussaServerManager.Common.Models.Profiles;

namespace OphiussaServerManager.Common.Models.ValheimProfile {
    public enum Preset {
        Normal,
        Casual,
        Easy,
        Hard,
        Hardcore,
        Immersive,
        Hammer
    }

    public enum Combat {
        Default,
        VeryEasy,
        Easy,
        Hard,
        VeryHard
    }

    public enum DeathPenalty {
        Default,
        Casual,
        VeryEasy,
        Easy,
        Hard,
        HardCore
    }

    public enum Resources {
        Default,
        MuchLess,
        Less,
        More,
        MuchMore,
        Most
    }

    public enum Portals {
        Default,
        Casual,
        Hard,
        VeryHard
    }

    public enum Raids {
        Default,
        None,
        MuchLess,
        Less,
        More,
        MuchMore
    }


    public class ValheimProfile {
        public ValheimProfile() {
            Administration = new Administration();
        }

        public Administration Administration { get; set; }

        public string GetCommandLinesArguments(Settings settings, Profile profile, string locaIp) {
            string cmd = string.Empty;

            var hifenArgs = new List<string>();

            hifenArgs.Add(" -nographics");
            hifenArgs.Add(" -batchmode");
            hifenArgs.Add($" -name \"{Administration.ServerName}\"");
            hifenArgs.Add($" -port {Administration.ServerPort}");
            hifenArgs.Add($" -world \"{Administration.WordName}\"");
            hifenArgs.Add($" -password \"{Administration.ServerPassword}\"");

            if (!string.IsNullOrEmpty(Administration.SaveLocation)) hifenArgs.Add($" -savedir \"{Administration.SaveLocation}\"");

            hifenArgs.Add($" -public {(Administration.Public ? 1 : 0)}");

            if (!string.IsNullOrEmpty(Administration.LogFileLocation)) hifenArgs.Add($" -logFile \"{Administration.LogFileLocation}\\VAL_{profile.Key}.log\"");

            hifenArgs.Add($" -saveinterval {Administration.AutoSavePeriod * 60}");
            hifenArgs.Add($" -backups {Administration.TotalBackups}");
            hifenArgs.Add($" -backupshort {Administration.BackupShort * 60}");
            hifenArgs.Add($" -backuplong {Administration.BackupLong   * 60}");

            if (Administration.Crossplay) hifenArgs.Add(" -crossplay");
            if (!string.IsNullOrEmpty(Administration.InstanceId)) hifenArgs.Add($" -instanceid \"{Administration.InstanceId}\"");

            hifenArgs.Add($" -preset {Administration.Preset.ToString().ToLower()}");

            if (Administration.Combat       != Combat.Default) hifenArgs.Add($" -modifier combat {Administration.Combat.ToString().ToLower()}");
            if (Administration.DeathPenalty != DeathPenalty.Default) hifenArgs.Add($" -modifier deathpenalty {Administration.DeathPenalty.ToString().ToLower()}");
            if (Administration.Resources    != Resources.Default) hifenArgs.Add($" -modifier resources {Administration.Resources.ToString().ToLower()}");
            if (Administration.Raids        != Raids.Default) hifenArgs.Add($" -modifier raids {Administration.Raids.ToString().ToLower()}");
            if (Administration.Portals      != Portals.Default) hifenArgs.Add($" -modifier portals {Administration.Portals.ToString().ToLower()}");

            if (Administration.NoBuildcost) hifenArgs.Add(" -setkey nobuildcost");
            if (Administration.PlayerEvents) hifenArgs.Add(" -setkey playerevents");
            if (Administration.PassiveMobs) hifenArgs.Add(" -setkey passivemobs");
            if (Administration.NoMap) hifenArgs.Add(" -setkey nomap");
            if (Administration.AllPiecesUnlocked) hifenArgs.Add(" -setkey AllPiecesUnlocked");
            if (Administration.AllRecipesUnlocked) hifenArgs.Add(" -setkey AllRecipesUnlocked");
            if (Administration.DeathDeleteItems) hifenArgs.Add(" -setkey DeathDeleteItems");
            if (Administration.DeathDeleteUnequipped) hifenArgs.Add(" -setkey DeathDeleteUnequipped");
            if (Administration.DeathKeepEquip) hifenArgs.Add(" -setkey DeathKeepEquip");
            if (Administration.DeathSkillsReset) hifenArgs.Add(" -setkey DeathSkillsReset");
            if (Administration.DungeonBuild) hifenArgs.Add(" -setkey DungeonBuild");
            if (Administration.NoCraftCost) hifenArgs.Add(" -setkey NoCraftCost");
            if (Administration.NoBossPortals) hifenArgs.Add(" -setkey NoBossPortals");
            if (Administration.NoPortals) hifenArgs.Add(" -setkey NoPortals");
            if (Administration.NoWorkbench) hifenArgs.Add(" -setkey NoWorkbench");
            if (Administration.TeleportAll) hifenArgs.Add(" -setkey TeleportAll");

            if (Administration.DamageTaken        != 100f) hifenArgs.Add($" -setkey DamageTaken {Administration.DamageTaken}");
            if (Administration.EnemyDamage        != 100f) hifenArgs.Add($" -setkey EnemyDamage {Administration.EnemyDamage}");
            if (Administration.EnemyLevelUpRate   != 100f) hifenArgs.Add($" -setkey EnemyLevelUpRate {Administration.EnemyLevelUpRate}");
            if (Administration.EnemySpeedSize     != 100f) hifenArgs.Add($" -setkey EnemySpeedSize {Administration.EnemySpeedSize}");
            if (Administration.EventRate          != 100f) hifenArgs.Add($" -setkey EventRate {Administration.EventRate}");
            if (Administration.MoveStaminaRate    != 100f) hifenArgs.Add($" -setkey MoveStaminaRate {Administration.MoveStaminaRate}");
            if (Administration.PlayerDamage       != 100f) hifenArgs.Add($" -setkey PlayerDamage {Administration.PlayerDamage}");
            if (Administration.ResourceRate       != 100f) hifenArgs.Add($" -setkey ResourceRate {Administration.ResourceRate}");
            if (Administration.SkillGainRate      != 100f) hifenArgs.Add($" -setkey SkillGainRate {Administration.SkillGainRate}");
            if (Administration.SkillReductionRate != 100f) hifenArgs.Add($" -setkey SkillReductionRate {Administration.SkillReductionRate}");
            if (Administration.StaminaRate        != 100f) hifenArgs.Add($" -setkey StaminaRate  {Administration.StaminaRate}");
            if (Administration.StaminaRegenRate   != 100f) hifenArgs.Add($" -setkey StaminaRegenRate {Administration.StaminaRegenRate}");

            cmd += string.Join("", hifenArgs.ToArray());

            return cmd;
        }
    }

    public class Administration : BaseAdministration {
        public string       Branch                { get; set; } = "Live";
        public bool         Crossplay             { get; set; } = false;
        public bool         Public                { get; set; } = false;
        public string       InstanceId            { get; set; } = "";
        public string       WordName              { get; set; } = "NewWorld";
        public Preset       Preset                { get; set; } = Preset.Normal;
        public int          AutoSavePeriod        { get; set; } = 30;
        public int          TotalBackups          { get; set; } = 3;
        public int          BackupShort           { get; set; } = 120;
        public int          BackupLong            { get; set; } = 720;
        public Combat       Combat                { get; set; } = Combat.Default;
        public DeathPenalty DeathPenalty          { get; set; } = DeathPenalty.Default;
        public Resources    Resources             { get; set; } = Resources.Default;
        public Portals      Portals               { get; set; } = Portals.Default;
        public Raids        Raids                 { get; set; } = Raids.Default;
        public string       SaveLocation          { get; set; } = "";
        public string       LogFileLocation       { get; set; } = "";
        public bool         NoBuildcost           { get; set; } = false;
        public bool         PlayerEvents          { get; set; } = false;
        public bool         PassiveMobs           { get; set; } = false;
        public bool         AllPiecesUnlocked     { get; set; } = false;
        public bool         AllRecipesUnlocked    { get; set; } = false;
        public bool         DeathDeleteItems      { get; set; } = false;
        public bool         DeathDeleteUnequipped { get; set; } = false;
        public bool         DeathKeepEquip        { get; set; } = false;
        public bool         DeathSkillsReset      { get; set; } = false;
        public bool         DungeonBuild          { get; set; } = false;
        public bool         NoCraftCost           { get; set; } = false;
        public bool         NoBossPortals         { get; set; } = false;
        public bool         NoMap                 { get; set; } = false;
        public bool         NoPortals             { get; set; } = false;
        public bool         NoWorkbench           { get; set; } = false;
        public bool         TeleportAll           { get; set; } = false;
        public float        DamageTaken           { get; set; } = 100f;
        public float        EnemyDamage           { get; set; } = 100f;
        public float        EnemyLevelUpRate      { get; set; } = 100f;
        public float        EnemySpeedSize        { get; set; } = 100f;
        public float        EventRate             { get; set; } = 100f;
        public float        MoveStaminaRate       { get; set; } = 100f;
        public float        PlayerDamage          { get; set; } = 100f;
        public float        ResourceRate          { get; set; } = 100f;
        public float        SkillGainRate         { get; set; } = 100f;
        public float        SkillReductionRate    { get; set; } = 100f;
        public float        StaminaRate           { get; set; } = 100f;
        public float        StaminaRegenRate      { get; set; } = 100f;
    }
}