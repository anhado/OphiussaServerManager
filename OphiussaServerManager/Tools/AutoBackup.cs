using Newtonsoft.Json;
using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OphiussaServerManager.Tools
{
    internal class AutoBackup
    {

        public event EventHandler<ProcessEventArg> ProgressChanged;
        public event EventHandler<ProcessEventArg> ProcessStarted;
        public event EventHandler<ProcessEventArg> ProcessCompleted;
        public event EventHandler<ProcessEventArg> ProcessError;

        protected virtual bool OnProgressChanged(ProcessEventArg e)
        {

            OphiussaLogger.logger.Info(e.Message);
            ProgressChanged?.Invoke(this, e);
            return true;
        }
        protected virtual void OnProcessCompleted(ProcessEventArg e)
        {
            OphiussaLogger.logger.Info(e.Message);
            ProcessCompleted?.Invoke(this, e);
        }
        protected virtual void OnProcessStarted(ProcessEventArg e)
        {
            OphiussaLogger.logger.Info(e.Message);
            ProcessStarted?.Invoke(this, e);
        }
        protected virtual void OnProcessError(ProcessEventArg e)
        {
            OphiussaLogger.logger.Info(e.Message);
            OphiussaLogger.logger.Error(e.Message);
            ProcessError?.Invoke(this, e);
        }

        internal void BackupAllServers()
        {
            try
            {
                OnProcessStarted(new ProcessEventArg() { Message = "Auto-Backup Started", Sucessful = true });
                Common.Models.Settings Settings = JsonConvert.DeserializeObject<Common.Models.Settings>(File.ReadAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")));
                string dir = Settings.DataFolder + "Profiles\\";
                if (!Directory.Exists(dir))
                {
                    return;
                }

                List<Profile> profiles = new List<Profile>();

                OnProgressChanged(new ProcessEventArg() { Message = "Loading Profiles", IsStarting = false, ProcessedFileCount = 0, Sucessful = true, TotalFiles = 0, SendToDiscord = true });

                string[] files = Directory.GetFiles(dir);
                foreach (string file in files)
                {
                    Profile p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(file));
                    if (p.AutoManageSettings.IncludeInAutoBackup)
                    {
                        profiles.Add(p);
                    }
                }

                List<Task> tasks = new List<Task>();
                foreach (var p in profiles)
                {
                    Task t = Task.Run(() =>
                    {
                        BackupSingleServers(Settings, p);
                    });
                    tasks.Add(t);
                }

                Task.WaitAll(tasks.ToArray());

                OnProcessCompleted(new ProcessEventArg() { Message = "Auto-Backup Ended", Sucessful = true });
            }
            catch (Exception ex)
            {
                OphiussaLogger.logger.Error(ex);
                OnProcessError(new ProcessEventArg() { Message = ex.Message, Sucessful = false, isError = true });
            }
        }

        internal void BackupSingleServers(string profileKey)
        {
            Common.Models.Settings Settings = JsonConvert.DeserializeObject<Common.Models.Settings>(File.ReadAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json")));
            string dir = Settings.DataFolder + "Profiles\\";
            if (!Directory.Exists(dir))
            {
                return;
            }
            string[] files = Directory.GetFiles(dir);

            string file = files.First(f => f.Contains(profileKey));
            if (!string.IsNullOrEmpty(file))
            {

                Profile p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(file));
                if (p != null)
                {
                    BackupSingleServers(Settings, p);
                }
            }
        }

        internal void BackupSingleServers(Settings settings, Profile profile)
        {
            OnProgressChanged(new ProcessEventArg() { Message = "Backup Server => " + profile.Type.KeyName, IsStarting = false, Sucessful = true, SendToDiscord = false });

            Task t = Task.Run(() => profile.BackupServer(settings));

            t.Wait();

            OnProgressChanged(new ProcessEventArg() { Message = "Backup Server Ended => " + profile.Type.KeyName, IsStarting = false, Sucessful = true, SendToDiscord = false });

        }
    }
}
