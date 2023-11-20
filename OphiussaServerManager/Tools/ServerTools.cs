using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager
{
    internal class ServerTools
    {  
        public static void UpdateServer()
        { 
            AutoUpdate autoUpdate = new AutoUpdate();
            autoUpdate.UpdateServers();
        }

        public static void BackupServer()
        {

        }

    }
}
