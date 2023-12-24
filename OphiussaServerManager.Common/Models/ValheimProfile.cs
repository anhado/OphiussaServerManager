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
            if (!string.IsNullOrEmpty(Administration.SaveLocation))
                hifenArgs.Add($" -savedir \"{Administration.SaveLocation}\"");
            hifenArgs.Add($" -public {(Administration.Public ? 1 : 0)}");
            if (!string.IsNullOrEmpty(Administration.LogFileLocation))
                hifenArgs.Add($" -logFile \"{Administration.LogFileLocation}\\VAL_{profile.Key}.log\"");
            hifenArgs.Add($" -saveinterval {Administration.AutoSavePeriod * 60}");
            hifenArgs.Add($" -backups {Administration.TotalBackups}");
            hifenArgs.Add($" -backupshort {Administration.BackupShort * 60}");
            hifenArgs.Add($" -backuplong {Administration.BackupLong   * 60}");
            if (Administration.Crossplay)
                hifenArgs.Add(" -crossplay");
            if (!string.IsNullOrEmpty(Administration.InstanceId))
                hifenArgs.Add($" -instanceid \"{Administration.InstanceId}\"");
            hifenArgs.Add($" -preset {Administration.Preset.ToString().ToLower()}");
            if (Administration.Combat != Combat.Default)
                hifenArgs.Add($" -modifier combat {Administration.Combat.ToString().ToLower()}");
            if (Administration.DeathPenalty != DeathPenalty.Default)
                hifenArgs.Add($" -modifier deathpenalty {Administration.DeathPenalty.ToString().ToLower()}");
            if (Administration.Resources != Resources.Default)
                hifenArgs.Add($" -modifier resources {Administration.Resources.ToString().ToLower()}");
            if (Administration.Raids != Raids.Default)
                hifenArgs.Add($" -modifier raids {Administration.Raids.ToString().ToLower()}");
            if (Administration.Portals != Portals.Default)
                hifenArgs.Add($" -modifier portals {Administration.Portals.ToString().ToLower()}");
            if (Administration.NoBuildcost)
                hifenArgs.Add(" -setkey nobuildcost");
            if (Administration.PlayerEvents)
                hifenArgs.Add(" -setkey playerevents");
            if (Administration.PassiveMobs)
                hifenArgs.Add(" -setkey passivemobs");
            if (Administration.NoMap)
                hifenArgs.Add(" -setkey nomap");


            cmd += string.Join("", hifenArgs.ToArray());

            return cmd;
        }
    }

    public class Administration : BaseAdministration {
        public string       Branch          { get; set; } = "Live";
        public bool         Crossplay       { get; set; } = false;
        public bool         Public          { get; set; } = false;
        public string       InstanceId      { get; set; } = "";
        public string       WordName        { get; set; } = "NewWorld";
        public Preset       Preset          { get; set; } = Preset.Normal;
        public int          AutoSavePeriod  { get; set; } = 30;
        public int          TotalBackups    { get; set; } = 3;
        public int          BackupShort     { get; set; } = 120;
        public int          BackupLong      { get; set; } = 720;
        public bool         NoBuildcost     { get; set; } = false;
        public bool         PlayerEvents    { get; set; } = false;
        public bool         PassiveMobs     { get; set; } = false;
        public bool         NoMap           { get; set; } = false;
        public Combat       Combat          { get; set; } = Combat.Default;
        public DeathPenalty DeathPenalty    { get; set; } = DeathPenalty.Default;
        public Resources    Resources       { get; set; } = Resources.Default;
        public Portals      Portals         { get; set; } = Portals.Default;
        public Raids        Raids           { get; set; } = Raids.Default;
        public string       SaveLocation    { get; set; } = "";
        public string       LogFileLocation { get; set; } = "";
    }
}