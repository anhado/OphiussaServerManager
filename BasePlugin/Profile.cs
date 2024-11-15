﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Web.Security;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;

namespace VRisingPlugin {
    public class Profile : IProfile {
        public string                  TestProperty1      { get; set; } = "";
        public bool                    TestProperty2      { get; set; } = true;
        public int                     TestProperty3      { get; set; } = 999;
        public string                  Key                { get; set; } = Guid.NewGuid().ToString();
        public string                  Name               { get; set; } = "New Server";
        public string                  Type               { get; set; }
        public string                  PluginVersion      { get; set; } = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
        public string                  InstallationFolder { get; set; } = "";
        public string                  AdditionalSettings { get; set; } = "";
        public string                  AdditionalCommands { get; set; } = "";
        public string                  Branch             { get; set; }
        public string                  BetaName           { get; set; }
        public string                  BetaPassword       { get; set; }
        public int                     SteamServerId      { get; set; } = 0;
        public int                     SteamApplicationID { get; set; } = 0;
        public int                     CurseForgeId       { get; set; } = 0;
        public int                     ServerPort         { get; set; } = 0;
        public int                     PeerPort           { get; set; } = 0;
        public int                     QueryPort          { get; set; } = 0;
        public int                     RCONPort           { get; set; } = 0;
        public string                  ServerVersion      { get; set; } = "";
        public string                  ServerBuildVersion { get; set; } = "";
        public bool                    AutoStartServer    { get; set; } = false;
        public bool                    StartOnBoot        { get; set; } = false;
        public bool                    IncludeAutoBackup  { get; set; } = false;
        public bool                    IncludeAutoUpdate  { get; set; } = false;
        public bool                    RestartIfShutdown  { get; set; } = false;
        public string                  RCONPassword       { get; set; } = Membership.GeneratePassword(10, 2);
        public bool                    UseRCON            { get; set; } = false;
        public string                  ExecutablePath     { get; set; } = "Dummy.exe";
        public string                  ServerPassword     { get; set; }
        public ProcessPriority         CpuPriority        { get; set; } = ProcessPriority.Normal;
        public string                  CpuAffinity        { get; set; } = "All";
        public List<ProcessorAffinity> CpuAffinityList    { get; set; } = new List<ProcessorAffinity>();
        public List<AutoManagement>    AutoManagement     { get; set; } = new List<AutoManagement>();
    }
}