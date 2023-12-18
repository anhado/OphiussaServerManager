﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common.Models
{
    public enum ResourceType
    {
        CPU,
        Memory,
        DiskUsed
    }
    public class ResourceMonitor
    {
        public ResourceType Type { get; set; }
        public string Description { get; set; }
        public string ProcessExeLocatoin { get; set; }
        public string InstalationFolder { get; set; }
        public double Usage { get; set; }
        public double TotalAvaliable { get; set; }
        public bool CalculateUsage { get; set; }
        public string CategoryName { get; set; }
        public string CounterName { get; set; }
        public string InstanceName { get; set; }
        public string Unit { get; set; }
        public int Timer { get; set; } = 1000;
        public bool isRunning { get; set; } = false;
    }
}
