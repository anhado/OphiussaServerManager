﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.DataBaseUtils;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;

namespace OphiussaFramework {
    public static class ConnectionController {
        public static List<NetworkTools.IpList>            IpLists       = new List<NetworkTools.IpList>();
        public static List<ProcessorAffinityModel>         AffinityModel = new List<ProcessorAffinityModel>();
        public static List<ProcessorAffinity>              ProcessorList = new List<ProcessorAffinity>();
        public static Dictionary<string, PluginController> Plugins;
        public static Dictionary<string, PluginController> ServerControllers = new Dictionary<string, PluginController>();
        public static SqlLite                              SqlLite  { get; set; }
        public static Settings                             Settings { get; set; }
        public static Form                                 MainForm { get; internal set; }


        public static void Initialize() {
            SqlLite  = new SqlLite();
            Settings = SqlLite.GetRecord<Settings>();
            IpLists  = NetworkTools.GetAllHostIp();


            //get CPU Affinities
            AffinityModel.Clear();
            Enum.GetNames(typeof(ProcessPriority)).ToList().ForEach(e => {
                                                                        AffinityModel.Add(new ProcessorAffinityModel {
                                                                                                                         Code = e,
                                                                                                                         Name = e
                                                                                                                     });
                                                                    });

            //get CPU List
            ProcessorList.Clear();
            for (int i = 0; i < Utils.GetProcessorCount(); i++)
                ProcessorList.Add(
                                  new ProcessorAffinity {
                                                            ProcessorNumber = i,
                                                            Selected        = true
                                                        }
                                 );

            LoadPlugins();
        }

        public static void SetMainForm(Form frm) {
            MainForm = frm;
        }


        public static void LoadPlugins() {
            Plugins = new Dictionary<string, PluginController>();

            var l = SqlLite.GetRecords<IPlugin>("Loaded = 1");

            if (l == null) return;
            foreach (var info in l)
                try {
                    Plugins.Add(info.GameType, new PluginController(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "plugins\\") + info.PluginName));
                }
                catch (ReflectionTypeLoadException e) {
                    MessageBox.Show($"Error Loading Plugins {info.PluginName}:" + e.Message);
                }
                catch (TypeLoadException e) {
                    MessageBox.Show($"Error Loading Plugins {info.PluginName}:" + e.Message);
                }
                catch (Exception e) {
                    MessageBox.Show($"Error Loading Plugins {info.PluginName}:" + e.Message);
                }
        }

        public static List<PluginType> GetServerTypes() {
            var ret = new List<PluginType>();
            foreach (string k in Plugins.Keys) ret.Add(new PluginType { GameType = Plugins[k].GameType, Name = Plugins[k].GameName });

            return ret;
        }

        public static bool UnloadPlugins(PluginInfo plugin) {
            try {
                string sc = ServerControllers.Keys.First(k => ServerControllers[k].GameType == plugin.GameType);

                if (sc != null) {
                    MessageBox.Show("You have a server configurations using this plugin!");
                    return false;
                }


                return Plugins.Remove(plugin.GameType);
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
                return false;
            }
        }
    }
}