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

        public static void RestartSingleServer(string ProfileKey)
        {
            AutoUpdate autoUpdate = new AutoUpdate();
            autoUpdate.RestartSingleServer(ProfileKey);
        }

        public static void BackupServer()
        {

        }

    }
}
