using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common.Models
{
    public enum AutoStart
    {
        onBoot = 0,
        onLogin = 1
    }

    public class AutoManageSettings
    {
        public bool AutoStartServer { get; set; } = false;
        public AutoStart AutoStartOn { get; set; } = AutoStart.onBoot;
        public bool ShutdownServer1 { get; set; } = false;
        public string ShutdownServer1Hour { get; set; } = "0100";
        public bool ShutdownServer1Sunday { get; set; } = false;
        public bool ShutdownServer1Monday { get; set; } = false;
        public bool ShutdownServer1Tuesday { get; set; } = false;
        public bool ShutdownServer1Wednesday { get; set; } = false;
        public bool ShutdownServer1Thu { get; set; } = false;
        public bool ShutdownServer1Friday { get; set; } = false;
        public bool ShutdownServer1Saturday { get; set; } = false;
        public bool ShutdownServer1PerformUpdate { get; set; } = false;
        public bool ShutdownServer1Restart { get; set; } = false;
        public bool ShutdownServer2 { get; set; } = false;
        public string ShutdownServer2Hour { get; set; } = "0100";
        public bool ShutdownServer2Sunday { get; set; } = false;
        public bool ShutdownServer2Monday { get; set; } = false;
        public bool ShutdownServer2Tuesday { get; set; } = false;
        public bool ShutdownServer2Wednesday { get; set; } = false;
        public bool ShutdownServer2Thu { get; set; } = false;
        public bool ShutdownServer2Friday { get; set; } = false;
        public bool ShutdownServer2Saturday { get; set; } = false;
        public bool ShutdownServer2PerformUpdate { get; set; } = false;
        public bool ShutdownServer2Restart { get; set; } = false;
        public bool IncludeInAutoBackup { get; set; } = false;
        public bool IncludeInAutoUpdate { get; set; } = false;
        public bool RestartIfShutdown { get; set; } = false;

        public AutoManageSettings()
        {
        }
    }
}
