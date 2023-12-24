using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;

namespace OphiussaServerManager.Tools {
    internal class AutoBackup {
        public event EventHandler<ProcessEventArg> ProgressChanged;
        public event EventHandler<ProcessEventArg> ProcessStarted;
        public event EventHandler<ProcessEventArg> ProcessCompleted;
        public event EventHandler<ProcessEventArg> ProcessError;

        protected virtual bool OnProgressChanged(ProcessEventArg e) {
            OphiussaLogger.Logger.Info(e.Message);
            ProgressChanged?.Invoke(this, e);
            return true;
        }

        protected virtual void OnProcessCompleted(ProcessEventArg e) {
            OphiussaLogger.Logger.Info(e.Message);
            ProcessCompleted?.Invoke(this, e);
        }

        protected virtual void OnProcessStarted(ProcessEventArg e) {
            OphiussaLogger.Logger.Info(e.Message);
            ProcessStarted?.Invoke(this, e);
        }

        protected virtual void OnProcessError(ProcessEventArg e) {
            OphiussaLogger.Logger.Info(e.Message);
            OphiussaLogger.Logger.Error(e.Message);
            ProcessError?.Invoke(this, e);
        }

        internal void BackupAllServers() {
            try {
                OnProcessStarted(new ProcessEventArg { Message = "Auto-Backup Started", Sucessful = true });
                var    settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")));
                string dir      = settings.DataFolder + "Profiles\\";
                if (!Directory.Exists(dir)) return;

                var profiles = new List<Profile>();

                OnProgressChanged(new ProcessEventArg { Message = "Loading Profiles", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                string[] files = Directory.GetFiles(dir);
                foreach (string file in files) {
                    var p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(file));
                    if (p.AutoManageSettings.IncludeInAutoBackup) profiles.Add(p);
                }

                var tasks = new List<Task>();
                foreach (var p in profiles) {
                    var t = Task.Run(() => { BackupSingleServers(settings, p); });
                    tasks.Add(t);
                }

                Task.WaitAll(tasks.ToArray());

                OnProcessCompleted(new ProcessEventArg { Message = "Auto-Backup Ended", Sucessful = true });
            }
            catch (Exception ex) {
                OphiussaLogger.Logger.Error(ex);
                OnProcessError(new ProcessEventArg { Message = ex.Message, Sucessful = false, IsError = true });
            }
        }

        internal void BackupSingleServers(string profileKey) {
            var    settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")));
            string dir      = settings.DataFolder + "Profiles\\";
            if (!Directory.Exists(dir)) return;
            string[] files = Directory.GetFiles(dir);

            string file = files.First(f => f.Contains(profileKey));
            if (!string.IsNullOrEmpty(file)) {
                var p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(file));
                if (p != null) BackupSingleServers(settings, p);
            }
        }

        internal void BackupSingleServers(Settings settings, Profile profile) {
            OnProgressChanged(new ProcessEventArg { Message = "Backup Server => " + profile.Type.KeyName, IsStarting = false, Sucessful = true, SendToDiscord = false });

            var t = Task.Run(() => profile.BackupServer(settings));

            t.Wait();

            OnProgressChanged(new ProcessEventArg { Message = "Backup Server Ended => " + profile.Type.KeyName, IsStarting = false, Sucessful = true, SendToDiscord = false });
        }
    }
}