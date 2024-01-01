using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Ini;
using TinyCsvParser.Mapping;

namespace OphiussaServerManager.Common.Models {
    public class LevelList : SortableObservableCollection<Level> {
        private const bool WORKAROUND_FOR_ENGRAM_LIST = true;

        public static readonly Regex XPRegex     = new Regex(@"ExperiencePointsForLevel\[(?<level>\d*)]=(?<xp>\d*)", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        public static readonly Regex EngramRegex = new Regex(@"OverridePlayerLevelEngramPoints=(?<points>\d*)",      RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

        public static int AdditionalLevels = GameData.LevelsPlayerAdditional;

        public void RemoveLevel(Level level) {
            Remove(level);
            UpdateTotals();
        }

        public void AddRange(IEnumerable<Level> levels) {
            foreach (var level in levels) Add(level);

            UpdateTotals();
        }

        public void AddNewLevel(Level afterLevel) {
            AddNewLevel(afterLevel, 1);
        }

        public void AddNewLevel(Level afterLevel, int xpIncrease) {
            var newLevel = new Level {
                                         LevelIndex   = 0,
                                         XPRequired   = afterLevel.XPRequired + xpIncrease,
                                         EngramPoints = afterLevel.EngramPoints
                                     };

            Insert(IndexOf(afterLevel) + 1, newLevel);
            UpdateTotals();
        }

        public void UpdateTotals() {
            int  index       = 0;
            long xpTotal     = 0;
            long engramTotal = 0;
            foreach (var existingLevel in this.OrderBy(l => l.XPRequired)) {
                xpTotal     += existingLevel.XPRequired;
                engramTotal += existingLevel.EngramPoints;

                existingLevel.XPTotal     = xpTotal;
                existingLevel.EngramTotal = engramTotal;

                existingLevel.LevelIndex  = index;
                existingLevel.ShowColored = index >= Count - AdditionalLevels;
                index++;
            }

            Sort(f => f.LevelIndex);
        }

        public string ToINIValueForXP() {
            var builder = new StringBuilder();
            builder.Append("LevelExperienceRampOverrides=(");
            builder.Append(string.Join(",", this.OrderBy(l => l.XPRequired).Select(l => l.GetINISubValueForXP())));
            builder.Append(')');

            return builder.ToString();
        }

        public List<string> ToINIValuesForEngramPoints() {
            var entries = new List<string>();


            if (WORKAROUND_FOR_ENGRAM_LIST) entries.Add(new Level().GetINIValueForEngramPointsEarned());

            foreach (var level in this.OrderBy(l => l.XPRequired)) entries.Add(level.GetINIValueForEngramPointsEarned());

            return entries;
        }

        public static LevelList FromINIValues(string xpValue, IEnumerable<string> engramValues = null) {
            var levels       = new LevelList();
            var xpResult     = XPRegex.Match(xpValue);
            var engramResult = engramValues == null ? null : EngramRegex.Match(string.Join(" ", engramValues));

            if (WORKAROUND_FOR_ENGRAM_LIST)
                if (engramResult != null)
                    engramResult = engramResult.NextMatch();

            while (xpResult.Success && (engramValues == null || engramResult.Success)) {
                int levelIndex;
                if (!int.TryParse(xpResult.Groups["level"].Value, out levelIndex)) {
                    Debug.WriteLine(string.Format("Invalid level index value: '{0}'", xpResult.Groups["level"].Value));
                    break;
                }

                long xpRequired;
                if (!long.TryParse(xpResult.Groups["xp"].Value, out xpRequired)) {
                    Debug.WriteLine(string.Format("Invalid xm required value: '{0}'", xpResult.Groups["xp"].Value));
                    break;
                }

                long engramPoints = 0;
                if (engramResult != null)
                    if (!long.TryParse(engramResult.Groups["points"].Value, out engramPoints)) {
                        Debug.WriteLine(string.Format("Invalid engram points value: '{0}'", engramResult.Groups["points"].Value));
                        break;
                    }

                levels.Add(new Level { LevelIndex = levelIndex, XPRequired = xpRequired, EngramPoints = engramPoints });
                xpResult = xpResult.NextMatch();
                if (engramResult != null) engramResult = engramResult.NextMatch();
            }

            levels.UpdateTotals();
            return levels;
        }
    }

    public class Level {
        public int LevelIndex { get; set; }

        public long XPRequired { get; set; }

        public long EngramPoints { get; set; }

        [XmlIgnore] public long XPTotal { get; set; }

        [XmlIgnore] public long EngramTotal { get; set; }

        public bool ShowColored { get; set; }

        public string GetINISubValueForXP() {
            return string.Format("ExperiencePointsForLevel[{0}]={1}", LevelIndex, XPRequired);
        }

        public string GetINIValueForEngramPointsEarned() {
            return string.Format("OverridePlayerLevelEngramPoints={0}", EngramPoints);
        }

        internal Level Duplicate() {
            return new Level { XPRequired = XPRequired, EngramPoints = EngramPoints };
        }
    }

    public class CsvPlayerLevelMapping : CsvMapping<ImportLevel> {
        public CsvPlayerLevelMapping() {
            MapProperty(0, x => x.LevelIndex);
            MapProperty(1, x => x.XPRequired);
            MapProperty(2, x => x.EngramPoints);
        }
    }

    public class CsvDinoLevelMapping : CsvMapping<ImportLevel> {
        public CsvDinoLevelMapping() {
            MapProperty(0, x => x.LevelIndex);
            MapProperty(1, x => x.XPRequired);
        }
    }

    public class ImportLevel {
        public int  LevelIndex   { get; set; }
        public long XPRequired   { get; set; }
        public long EngramPoints { get; set; }

        public Level AsLevel() {
            return new Level { LevelIndex = LevelIndex, XPRequired = XPRequired, EngramPoints = EngramPoints };
        }
    }
}