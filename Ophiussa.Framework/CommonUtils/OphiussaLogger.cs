﻿using System;
using System.IO;
using System.Linq;
using NLog;
using NLog.Config;
using NLog.Targets;
using OphiussaFramework.DataBaseUtils;
using OphiussaFramework.Models;

namespace OphiussaFramework.CommonUtils {
    public static class OphiussaLogger {
        //public static           Logger       Logger;
        public static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void UpdateLoggingStatus(Settings settings) {
            if (settings.EnableLogs)
                while (!LogManager.IsLoggingEnabled())
                    LogManager.ResumeLogging();
            else
                while (LogManager.IsLoggingEnabled())
                    LogManager.SuspendLogging();
        }

        public static string GetLogFolder(Settings settings) {
            return IOUtils.NormalizePath(Path.Combine(settings.DataFolder, "Logs"));
        }

        public static string GetProfileLogFolder(Settings settings, string profileId) {
            return IOUtils.NormalizePath(Path.Combine(settings.DataFolder, "Logs", profileId.ToLower()));
        }

        public static Logger GetProfileLogger(
            Settings settings,
            string   profileId,
            string   logName,
            LogLevel minLevel,
            LogLevel maxLevel) {
            if (string.IsNullOrWhiteSpace(profileId) || string.IsNullOrWhiteSpace(logName))
                return null;
            string str = (profileId.ToLower() + "_" + logName).Replace(" ", "_");
            if (LogManager.Configuration.FindTargetByName(str) == null) {
                string profileLogFolder = GetProfileLogFolder(settings, profileId);
                var    fileTarget1      = new FileTarget(str);
                fileTarget1.FileName                = Path.Combine(profileLogFolder, logName + ".log");
                fileTarget1.Layout                  = "${time} [${level:uppercase=true}] ${message}";
                fileTarget1.ArchiveFileName         = Path.Combine(profileLogFolder, logName + ".{#}.log");
                fileTarget1.ArchiveNumbering        = ArchiveNumberingMode.DateAndSequence;
                fileTarget1.ArchiveEvery            = FileArchivePeriod.Day;
                fileTarget1.ArchiveDateFormat       = "yyyyMMdd";
                fileTarget1.ArchiveOldFileOnStartup = true;
                fileTarget1.MaxArchiveFiles         = settings.MaxLogFiles;
                fileTarget1.MaxArchiveDays          = settings.MaxLogsDays;
                fileTarget1.CreateDirs              = true;
                var fileTarget2 = fileTarget1;
                LogManager.Configuration.AddTarget(str, fileTarget2);
                LogManager.Configuration.LoggingRules.Add(new LoggingRule(str, minLevel, maxLevel, fileTarget2));
                LogManager.ReconfigExistingLoggers();
            }

            return LogManager.GetLogger(str);
        }

        public static void ReconfigureLogging() {
            try {
                var setting = new SqlLite().GetRecord<Settings>();
                if (setting == null) throw new Exception("Could Not Load Settings");

                //ReconfigureLogging(setting);
            }
            catch (Exception ex) {
                string tmpFile = Path.GetTempFileName();
                if (!File.Exists(tmpFile)) File.Create(tmpFile);
                File.AppendAllText(tmpFile, "\nerror:" + ex.Message);
            }
        }

        public static void ReconfigureLogging(Settings settings) {
            try {
                if (string.IsNullOrWhiteSpace(settings?.DataFolder))
                    return;
                UpdateLoggingStatus(settings);
                string str = Path.Combine(settings.DataFolder, "Logs");
                if (!Directory.Exists(str))
                    Directory.CreateDirectory(str);
                LogManager.Configuration.Variables["logDir"] = str;
                foreach (var fileTarget in LogManager.Configuration.AllTargets.OfType<FileTarget>()) {
                    string withoutExtension = Path.GetFileNameWithoutExtension(fileTarget.FileName.ToString());
                    fileTarget.FileName        = Path.Combine(str, withoutExtension + ".log");
                    fileTarget.ArchiveFileName = Path.Combine(str, withoutExtension + ".{#}.log");
                    fileTarget.MaxArchiveFiles = settings.MaxLogFiles;
                    fileTarget.MaxArchiveDays  = settings.MaxLogsDays;
                    fileTarget.CreateDirs      = true;
                }

                LogManager.ReconfigExistingLoggers();
                //Logger = LogManager.GetCurrentClassLogger();
            }
            catch (Exception ex) {
                string tmpFile = Path.GetTempFileName();
                if (!File.Exists(tmpFile)) File.Create(tmpFile);
                File.AppendAllText(tmpFile, "error:" + ex.Message);
            }
        }
    }
}