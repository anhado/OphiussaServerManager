using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Tools.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager
{
    internal class ServerTools
    {
        public static void UpdateAllServer()
        {
            AutoUpdate autoUpdate = new AutoUpdate();
            autoUpdate.UpdateAllServers();
        }

        public static void RestartSingleServer(string ProfileKey, bool restartOnlyToUpdate = false)
        {
            AutoUpdate autoUpdate = new AutoUpdate();
            autoUpdate.RestartSingleServer(ProfileKey, restartOnlyToUpdate);
        }

        public static void BackupServer()
        {

        }

    }
}
