using NLog.Layouts;
using NLog.Targets;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OphiussaServerManager.Common.Models;
using System.IO;
using NLog.Config;
using OphiussaServerManager.Common.Helpers;
using System.Runtime;
using Newtonsoft.Json;
using NLog.Fluent;

namespace OphiussaServerManager
{
    public static class OphiussaLogger
    {
        public static Logger logger;

        public static void UpdateLoggingStatus(Settings settings)
        {
            if (settings.EnableLogs)
            {
                while (!LogManager.IsLoggingEnabled())
                    LogManager.ResumeLogging();
            }
            else
            {
                while (LogManager.IsLoggingEnabled())
                    LogManager.SuspendLogging();
            }
        }
        public static string GetLogFolder(Settings settings) => IOUtils.NormalizePath(Path.Combine(settings.DataFolder, "Logs"));

        public static string GetProfileLogFolder(Settings settings, string profileId) => IOUtils.NormalizePath(Path.Combine(settings.DataFolder, "Logs", profileId.ToLower()));

        public static Logger GetProfileLogger(
          Settings settings,
          string profileId,
          string logName,
          NLog.LogLevel minLevel,
          NLog.LogLevel maxLevel)
        {
            if (string.IsNullOrWhiteSpace(profileId) || string.IsNullOrWhiteSpace(logName))
                return (Logger)null;
            string str = (profileId.ToLower() + "_" + logName).Replace(" ", "_");
            if (LogManager.Configuration.FindTargetByName(str) == null)
            {
                string profileLogFolder = GetProfileLogFolder(settings, profileId);
                FileTarget fileTarget1 = new FileTarget(str);
                fileTarget1.FileName = (Layout)Path.Combine(profileLogFolder, logName + ".log");
                fileTarget1.Layout = (Layout)"${time} [${level:uppercase=true}] ${message}";
                fileTarget1.ArchiveFileName = (Layout)Path.Combine(profileLogFolder, logName + ".{#}.log");
                fileTarget1.ArchiveNumbering = ArchiveNumberingMode.DateAndSequence;
                fileTarget1.ArchiveEvery = FileArchivePeriod.Day;
                fileTarget1.ArchiveDateFormat = "yyyyMMdd";
                fileTarget1.ArchiveOldFileOnStartup = true;
                fileTarget1.MaxArchiveFiles = settings.MaxLogFiles;
                fileTarget1.MaxArchiveDays = settings.MaxLogsDays;
                fileTarget1.CreateDirs = true;
                FileTarget fileTarget2 = fileTarget1;
                LogManager.Configuration.AddTarget(str, (Target)fileTarget2);
                LogManager.Configuration.LoggingRules.Add(new LoggingRule(str, minLevel, maxLevel, (Target)fileTarget2));
                LogManager.ReconfigExistingLoggers();
            }
            return LogManager.GetLogger(str);
        }

        public static void ReconfigureLogging()
        {
            try
            {
                Settings Settings = JsonConvert.DeserializeObject<Common.Models.Settings>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")));
                //OphiussaLogger.ReconfigureLogging(Settings);
                ReconfigureLogging(Settings);
            }
            catch (Exception ex)
            {
                string tmpFile = Path.GetTempFileName();
                if (!File.Exists(tmpFile)) File.Create(tmpFile);
                File.AppendAllText(tmpFile, "error:" + ex.Message);
            }
        }
        public static void ReconfigureLogging(Settings settings)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(settings.DataFolder))
                    return;
                UpdateLoggingStatus(settings);
                string str = Path.Combine(settings.DataFolder, "Logs");
                if (!System.IO.Directory.Exists(str))
                    System.IO.Directory.CreateDirectory(str);
                LogManager.Configuration.Variables["logDir"] = (Layout)str;
                foreach (FileTarget fileTarget in LogManager.Configuration.AllTargets.OfType<FileTarget>())
                {
                    string withoutExtension = Path.GetFileNameWithoutExtension(fileTarget.FileName.ToString());
                    fileTarget.FileName = (Layout)Path.Combine(str, withoutExtension + ".log");
                    fileTarget.ArchiveFileName = (Layout)Path.Combine(str, withoutExtension + ".{#}.log");
                    fileTarget.MaxArchiveFiles = settings.MaxLogFiles;
                    fileTarget.MaxArchiveDays = settings.MaxLogsDays;
                    fileTarget.CreateDirs = true;
                }
                LogManager.ReconfigExistingLoggers();
                logger = LogManager.GetCurrentClassLogger();
            }
            catch (Exception ex)
            {
                string tmpFile = Path.GetTempFileName();
                if (!File.Exists(tmpFile)) File.Create(tmpFile);
                File.AppendAllText(tmpFile, "error:" + ex.Message);
            }
        }
    }
}
