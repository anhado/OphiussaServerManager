using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OphiussaFramework.DataBaseUtils;
using OphiussaFramework.Extensions;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;

namespace OphiussaServerManagerV2
{
    internal static class Global
    {
        internal static Dictionary<string, PluginController> plugins;
        internal static Dictionary<string, PluginController> serverControllers = new Dictionary<string, PluginController>();

        internal static SqlLite SqlLite { get; set; }
        internal static Settings Settings { get; set; }

        internal static void Initialize()
        {
            SqlLite = new SqlLite();
            Global.Settings = Global.SqlLite.GetSettings();
            LoadPlugins();
        }

        internal static void LoadPlugins()
        {
            plugins = new Dictionary<string, PluginController>();

            List<PluginInfo> l = SqlLite.GetPluginInfoList();

            if (l == null) return;
            foreach (PluginInfo info in l)
            {
                try
                {
                    plugins.Add(info.GameType, new PluginController(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "plugins\\") + info.PluginName));
                }
                catch (ReflectionTypeLoadException e)
                {
                    MessageBox.Show(e.Message);
                }
                catch (TypeLoadException e)
                {
                    MessageBox.Show(e.Message);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        internal static List<PluginType> GetServerTypes()
        {
            List<PluginType> ret = new List<PluginType>();
            foreach (string k in plugins.Keys)
            {
                ret.Add(new PluginType() { GameType = plugins[k].GameType, Name = plugins[k].GameName });
            }

            return ret;
        }

        internal static bool UnloadPlugins(PluginInfo plugin)
        {
            try
            {
                var sc = serverControllers.Keys.First(k => serverControllers[k].GameType == plugin.GameType);

                if (sc != null)
                {
                    MessageBox.Show("You have a server configurations using this plugin!");
                    return false;
                }
                 

                return plugins.Remove(plugin.GameType);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }
    }
}
