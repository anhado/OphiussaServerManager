using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.DataBaseUtils;
using OphiussaFramework.Enums;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;

namespace OphiussaFramework {
    public static class ConnectionController {
        public static  List<NetworkTools.IpList>            IpLists       = new List<NetworkTools.IpList>();
        public static  List<ProcessorAffinityModel>         AffinityModel = new List<ProcessorAffinityModel>();
        public static  List<ProcessorAffinity>              ProcessorList = new List<ProcessorAffinity>();
        public static  Dictionary<string, PluginController> Plugins;
        public static  Dictionary<string, PluginController> ServerControllers = new Dictionary<string, PluginController>();
        public static  SqlLite                              SqlLite  { get; set; }
        public static  Settings                             Settings { get; set; }
        public static  Form                                 MainForm { get; internal set; }
        public static  List<Branches>                       Branches { get; internal set; } = new List<Branches>();
        private static Thread                               ThreadServerMonitor;
        private static Thread                               ThreadTaskMonitor;
        public static  TaskStatus                           BackupTaskStatus     { get; internal set; } = TaskStatus.NotRunning;
        public static  TaskStatus                           AutoUpdateTaskStatus { get; internal set; } = TaskStatus.NotRunning;



        public static void Initialize() {
            SqlLite  = new SqlLite();
            Settings = SqlLite.GetRecord<Settings>();
            LoadBranches();
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

        public static void LoadBranches() { Branches = ConnectionController.SqlLite.GetRecords<Branches>(); }


        public static void StartServerMonitor() { 
            ThreadServerMonitor = new Thread(ServerMonitorTask);
            ThreadServerMonitor.Start();
        }

        public static void StartTaskMonitor() {
            ThreadTaskMonitor = new Thread(TaskMonitor);
            ThreadTaskMonitor.Start(); 
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

        public static bool UnloadPlugins(IPlugin plugin) { 
            string sc = ServerControllers.Keys.First(k => ServerControllers[k].GameType == plugin.GameType);

            if (sc != null) {
                throw new Exception("You have a server configurations using this plugin!"); 
            } 
            return Plugins.Remove(plugin.GameType); 
        }

        private static void ServerMonitorTask() { 
            while (true) {
                if (!Utils.IsFormRunning("MainForm"))
                    break;
                 
                foreach (string k in ServerControllers.Keys) {
                    IProfile prf             = ServerControllers[k].GetProfile();

                    if (ServerControllers[k].IsValidFolder(prf.InstallationFolder)) {
                        Process proc      = Utils.GetProcessRunning(Path.Combine(prf.InstallationFolder, prf.ExecutablePath));
                        bool    isRunning = proc != null;
                        if (isRunning) {
                            int serverProcessId = proc.Id;
                            ServerControllers[k].SetServerStatus(ServerStatus.Running, serverProcessId);
                        }
                        else
                            ServerControllers[k].SetServerStatus(ServerStatus.Stopped, -1);
                    }
                    else
                        ServerControllers[k].SetServerStatus(ServerStatus.NotInstalled, -1);

                }

                Thread.Sleep(1000);
            }
        }

        private static void TaskMonitor() {
            while (true) {
                if (!Utils.IsFormRunning("MainForm"))
                    break;

                var lTasks = TaskService.Instance.GetRunningTasks().ToList();

                string taskName = "OphiussaServerManager\\AutoBackup_" + ConnectionController.Settings.GUID;
                var    task     = TaskService.Instance.GetTask(taskName);
                if (task != null) {
                    task.Definition.Principal.RunLevel = TaskRunLevel.Highest;
                    task.Definition.Settings.Priority  = ProcessPriorityClass.Normal;
                    var x = lTasks.Find(xc => xc?.Name == "AutoBackup_" + ConnectionController.Settings.GUID);

                    if (x != null) {
                        BackupTaskStatus = TaskStatus.Running;
                    }
                    else {
                        BackupTaskStatus = TaskStatus.Ready;
                    }
                }
                else {
                    BackupTaskStatus = TaskStatus.NotRunning;
                }

                string taskName2 = "OphiussaServerManager\\AutoUpdate_" + ConnectionController.Settings.GUID;

                var task2 = TaskService.Instance.GetTask(taskName2);
                if (task2 != null) {
                    var x = lTasks.Find(xc => xc?.Name == "AutoUpdate_" + ConnectionController.Settings.GUID);

                    if (x != null) {
                        AutoUpdateTaskStatus = TaskStatus.Running;
                    }
                    else {
                        AutoUpdateTaskStatus = TaskStatus.Ready;
                    }
                }
                else {
                    AutoUpdateTaskStatus = TaskStatus.NotRunning;
                }

                Thread.Sleep(1000);
            }
        }
    }
}