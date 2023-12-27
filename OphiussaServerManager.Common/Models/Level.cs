using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Xml.Serialization;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Ini;
using TinyCsvParser.Mapping;

namespace OphiussaServerManager.Common.Models {
    
    public class LevelList : SortableObservableCollection<Level>
    {
        const bool WORKAROUND_FOR_ENGRAM_LIST = true;

        public static readonly Regex XPRegex = new Regex(@"ExperiencePointsForLevel\[(?<level>\d*)]=(?<xp>\d*)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        public static readonly Regex EngramRegex = new Regex(@"OverridePlayerLevelEngramPoints=(?<points>\d*)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        public static int AdditionalLevels = GameData.LevelsPlayerAdditional;

        public void RemoveLevel(Level level)
        {
            base.Remove(level);
            UpdateTotals();
        }

        public void AddRange(IEnumerable<Level> levels)
        {
            foreach (var level in levels)
            {
                base.Add(level);
            }

            UpdateTotals();
        }

        public void AddNewLevel(Level afterLevel)
        {
            AddNewLevel(afterLevel, 1);
        }

        public void AddNewLevel(Level afterLevel, int xpIncrease)
        {
            var newLevel = new Level
            {
                LevelIndex = 0,
                XPRequired = afterLevel.XPRequired + xpIncrease,
                EngramPoints = afterLevel.EngramPoints
            };

            base.Insert(base.IndexOf(afterLevel) + 1, newLevel);
            UpdateTotals();
        }

        public void UpdateTotals()
        {
            int index = 0;
            long xpTotal = 0;
            long engramTotal = 0;
            foreach (var existingLevel in this.OrderBy(l => l.XPRequired))
            {
                xpTotal += existingLevel.XPRequired;
                engramTotal += existingLevel.EngramPoints;

                existingLevel.XPTotal = xpTotal;
                existingLevel.EngramTotal = engramTotal;

                existingLevel.LevelIndex = index;
                existingLevel.ShowColored = index >= this.Count - AdditionalLevels;
                index++;
            }

            base.Sort(f => f.LevelIndex);
        }

        public string ToINIValueForXP()
        {
            var builder = new StringBuilder();
            builder.Append("LevelExperienceRampOverrides=(");
            builder.Append(String.Join(",", this.OrderBy(l => l.XPRequired).Select(l => l.GetINISubValueForXP())));
            builder.Append(')');

            return builder.ToString();
        }

        public List<string> ToINIValuesForEngramPoints()
        {
            var entries = new List<string>();


            if (WORKAROUND_FOR_ENGRAM_LIST)
            {
                entries.Add(new Level().GetINIValueForEngramPointsEarned());
            }

            foreach (var level in this.OrderBy(l => l.XPRequired))
            {
                entries.Add(level.GetINIValueForEngramPointsEarned());
            }

            return entries;
        }

        public static LevelList FromINIValues(string xpValue, IEnumerable<string> engramValues = null)
        {
            var levels = new LevelList();
            var xpResult = XPRegex.Match(xpValue);
            var engramResult = engramValues == null ? null : EngramRegex.Match(String.Join(" ", engramValues));

            if (WORKAROUND_FOR_ENGRAM_LIST)
            {
                if (engramResult != null)
                {
                    engramResult = engramResult.NextMatch();
                }
            }

            while (xpResult.Success && (engramValues == null || engramResult.Success))
            {
                int levelIndex;
                if (!int.TryParse(xpResult.Groups["level"].Value, out levelIndex))
                {
                    Debug.WriteLine(String.Format("Invalid level index value: '{0}'", xpResult.Groups["level"].Value));
                    break;
                }

                long xpRequired;
                if (!long.TryParse(xpResult.Groups["xp"].Value, out xpRequired))
                {
                    Debug.WriteLine(String.Format("Invalid xm required value: '{0}'", xpResult.Groups["xp"].Value));
                    break;
                }

                long engramPoints = 0;
                if (engramResult != null)
                {
                    if (!long.TryParse(engramResult.Groups["points"].Value, out engramPoints))
                    {
                        Debug.WriteLine(String.Format("Invalid engram points value: '{0}'", engramResult.Groups["points"].Value));
                        break;
                    }
                }

                levels.Add(new Level { LevelIndex = levelIndex, XPRequired = xpRequired, EngramPoints = engramPoints });
                xpResult = xpResult.NextMatch();
                if (engramResult != null)
                {
                    engramResult = engramResult.NextMatch();
                }
            }

            levels.UpdateTotals();
            return levels;
        }
    }

    public class Level
    { 
        public int LevelIndex { get; set; } = 0;

        public long XPRequired { get; set; } = 0L;

        public long EngramPoints { get; set; } = 0L;

        [XmlIgnore()]
        public long XPTotal { get; set; } = 0L;

        [XmlIgnore()]
        public long EngramTotal { get; set; } = 0L;

        public string GetINISubValueForXP()
        {
            return String.Format("ExperiencePointsForLevel[{0}]={1}", this.LevelIndex, this.XPRequired);
        }

        public string GetINIValueForEngramPointsEarned()
        {
            return String.Format("OverridePlayerLevelEngramPoints={0}", this.EngramPoints);
        }

        internal Level Duplicate()
        {
            return new Level { XPRequired = this.XPRequired, EngramPoints = this.EngramPoints };
        }

        public bool ShowColored { get; set; } = false;
    }

    public class CsvPlayerLevelMapping : CsvMapping<ImportLevel>
    {
        public CsvPlayerLevelMapping()
            : base()
        {
            MapProperty(0, x => x.LevelIndex);
            MapProperty(1, x => x.XPRequired);
            MapProperty(2, x => x.EngramPoints);
        }
    }

    public class CsvDinoLevelMapping : CsvMapping<ImportLevel>
    {
        public CsvDinoLevelMapping()
            : base()
        {
            MapProperty(0, x => x.LevelIndex);
            MapProperty(1, x => x.XPRequired);
        }
    }

    public class ImportLevel
    {
        public int LevelIndex { get; set; }
        public long XPRequired { get; set; }
        public long EngramPoints { get; set; }

        public Level AsLevel()
        {
            return new Level() { LevelIndex = LevelIndex, XPRequired = XPRequired, EngramPoints = EngramPoints };
        }
    }
}