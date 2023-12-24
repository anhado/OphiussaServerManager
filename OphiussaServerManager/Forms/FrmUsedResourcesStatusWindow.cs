using System;
using System.Drawing;
using System.IO;
using System.Management;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Tools;

namespace OphiussaServerManager.Forms {
    public partial class FrmUsedResourcesStatusWindow : Form {
        private static   DateTime        _lastTime;
        private static   TimeSpan        _lastTotalProcessorTime;
        private static   DateTime        _curTime;
        private static   TimeSpan        _curTotalProcessorTime;
        private readonly ResourceMonitor _resourceMonitor;

        private bool _keepRunning;

        public FrmUsedResourcesStatusWindow(ResourceMonitor resourceMonitor) {
            InitializeComponent();
            _resourceMonitor = resourceMonitor;
        }

        private void timer1_Tick(object sender, EventArgs e) {
            try {
                timer1.Enabled = false;
                GetValues();
            }
            catch (Exception) {
            }

            timer1.Enabled = true;
        }

        private void UpdateValues() {
            while (true) {
                switch (_resourceMonitor.Type) {
                    case ResourceType.Cpu:

                        _resourceMonitor.Usage = 0;
                        if (_resourceMonitor.ProcessExeLocatoin != "") {
                            var process = Utils.GetProcessRunning(_resourceMonitor.ProcessExeLocatoin);
                            if (process != null) {
                                _curTime               = DateTime.Now;
                                _curTotalProcessorTime = process.TotalProcessorTime;

                                _resourceMonitor.Usage = (_curTotalProcessorTime.TotalMilliseconds - _lastTotalProcessorTime.TotalMilliseconds) / _curTime.Subtract(_lastTime).TotalMilliseconds / Convert.ToDouble(Environment.ProcessorCount);

                                _lastTime                  = _curTime;
                                _lastTotalProcessorTime    = _curTotalProcessorTime;
                                _resourceMonitor.IsRunning = true;
                            }
                            else {
                                _resourceMonitor.Usage     = 0;
                                _resourceMonitor.IsRunning = false;
                            }
                        }

                        /*ResourceMonitor.Usage =*/
                        //GetCPUUSage();
                        break;
                    case ResourceType.Memory:

                        if (_resourceMonitor.ProcessExeLocatoin != "") {
                            var process = Utils.GetProcessRunning(_resourceMonitor.ProcessExeLocatoin);

                            if (process != null) {
                                _resourceMonitor.Usage          = int.Parse((ulong.Parse(process.PrivateMemorySize64.ToString()) / 1024 / 1024).ToString());
                                _resourceMonitor.CalculateUsage = true;
                                _resourceMonitor.IsRunning      = true;
                            }
                            else {
                                _resourceMonitor.Usage          = 0;
                                _resourceMonitor.CalculateUsage = false;
                                _resourceMonitor.IsRunning      = false;
                            }
                        }
                        else {
                            _resourceMonitor.Usage = int.Parse(((GetTotalMemoryInBytes() - GetAvaliableMemoryInBytes()) / 1024 / 1024).ToString());
                        }

                        break;
                    case ResourceType.DiskUsed:

                        if (_resourceMonitor.ProcessExeLocatoin != "") {
                            var drive = new DriveInfo(_resourceMonitor.ProcessExeLocatoin);
                            _resourceMonitor.Usage = DirSize(new DirectoryInfo(_resourceMonitor.ProcessExeLocatoin)) / 1024.0 / 1024.0 / 1024.0;
                        }
                        else {
                            var drive = new DriveInfo(_resourceMonitor.InstalationFolder);
                            _resourceMonitor.Usage = (drive.TotalSize - drive.TotalFreeSpace) / 1024.0 / 1024.0 / 1024.0;
                        }

                        break;
                }

                if (!_keepRunning) break;
                if (!UsefullTools.IsFormRunning("FrmUsedResources")) break;
                if (!UsefullTools.IsFormRunning("MainForm")) break;
                Thread.Sleep(_resourceMonitor.Timer);
            }
        }

        private void GetValues() {
            bool showUsage = _resourceMonitor.CalculateUsage;
            switch (_resourceMonitor.Type) {
                case ResourceType.Cpu:

                    if (_resourceMonitor.ProcessExeLocatoin != "") {
                        if (_resourceMonitor.IsRunning) {
                            showUsage          = false;
                            lblUsage.ForeColor = Color.Black;
                        }
                        else {
                            showUsage          = true;
                            lblUsage.Text      = "NOT RUNNING";
                            lblUsage.ForeColor = Color.Red;
                        }
                    }
                    //else
                    //    ResourceMonitor.Usage = GetCPUUSage();

                    break;
                case ResourceType.Memory:

                    if (_resourceMonitor.ProcessExeLocatoin != "") {
                        if (_resourceMonitor.IsRunning) {
                            showUsage          = false;
                            lblUsage.ForeColor = Color.Black;
                        }
                        else {
                            showUsage          = true;
                            lblUsage.Text      = "NOT RUNNING";
                            lblUsage.ForeColor = Color.Red;
                        }
                    }

                    //else
                    //ResourceMonitor.Usage = int.Parse(((GetTotalMemoryInBytes() - GetAvaliableMemoryInBytes()) / 1024 / 1024).ToString());
                    break;
                case ResourceType.DiskUsed:

                //if (ResourceMonitor.ProcessExeLocatoin != "")
                //{
                //    DriveInfo drive = new DriveInfo(ResourceMonitor.ProcessExeLocatoin);
                //    ResourceMonitor.Usage = ((DirSize(new DirectoryInfo(ResourceMonitor.ProcessExeLocatoin))) / 1024.0 / 1024.0 / 1024.0);
                //}
                //else
                //{
                //    DriveInfo drive = new DriveInfo(ResourceMonitor.InstalationFolder);
                //    ResourceMonitor.Usage = ((drive.TotalSize - drive.TotalFreeSpace) / 1024.0 / 1024.0 / 1024.0);
                //}
                //break;
                default:
                    break;
            }

            if (_resourceMonitor.CalculateUsage) {
                lblUsage.Text = Math.Round(_resourceMonitor.Usage, 2) + _resourceMonitor.Unit + "/" + Math.Round(_resourceMonitor.TotalAvaliable, 2) + _resourceMonitor.Unit;

                int totalProgress = (int)(_resourceMonitor.Usage / _resourceMonitor.TotalAvaliable * 100);

                progressBar1.Value = totalProgress; // ((ResourceMonitor.Usage / ResourceMonitor.TotalAvaliable) * 100).ToString().ToInt();
                lblUsage.Visible   = true;
            }
            else {
                progressBar1.Value = (_resourceMonitor.Usage > 100 ? 100 : _resourceMonitor.Usage).ToString().ToInt();
                lblUsage.Visible   = showUsage;
            }

            lblUsagePerc.Text = Math.Round(_resourceMonitor.Usage, 2) + _resourceMonitor.Unit;
        }

        private void GetCpuuSage() {
            while (true) {
                int usage    = 0;
                var searcher = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfOS_Processor");
                foreach (ManagementObject obj in searcher.Get()) {
                    object name                            = obj["Name"];
                    if (name.ToString() != "_Total") usage = int.Parse(obj["PercentProcessorTime"].ToString());
                }

                _resourceMonitor.Usage = usage;
                if (!UsefullTools.IsFormRunning("FrmUsedResources")) break;
                if (!UsefullTools.IsFormRunning("MainForm")) break;
                Thread.Sleep(_resourceMonitor.Timer);
            }
        }

        private void LoadData() {
            try {
                switch (_resourceMonitor.Type) {
                    case ResourceType.Cpu:
                        LoadCpu();
                        timer1.Interval = _resourceMonitor.Timer;
                        timer1.Enabled  = true;
                        break;
                    case ResourceType.Memory:
                        LoadMemory();
                        timer1.Interval = _resourceMonitor.Timer;
                        timer1.Enabled  = true;
                        break;
                    case ResourceType.DiskUsed:
                        LoadDisk();
                        timer1.Interval = _resourceMonitor.Timer;
                        timer1.Enabled  = true;
                        break;
                }

                var thread = new Thread(UpdateValues);
                thread.Start();
            }
            catch (Exception ex) {
                lblDescription.Text = "ERROR";
            }
        }

        private static ulong GetTotalMemoryInBytes() {
            return new ComputerInfo().TotalPhysicalMemory;
        }

        private static ulong GetAvaliableMemoryInBytes() {
            return new ComputerInfo().AvailablePhysicalMemory;
        }

        public static long DirSize(DirectoryInfo d) {
            long size = 0;
            // Add file sizes.
            var fis                      = d.GetFiles();
            foreach (var fi in fis) size += fi.Length;
            // Add subdirectory sizes.
            var dis                      = d.GetDirectories();
            foreach (var di in dis) size += DirSize(di);
            return size;
        }

        private void LoadDisk() {
            lblTypeDesc.Text = "DISK USAGE";
            if (_resourceMonitor.ProcessExeLocatoin != "") {
                lblDescription.Text = _resourceMonitor.Description;
                var drive = new DriveInfo(_resourceMonitor.ProcessExeLocatoin);
                _resourceMonitor.TotalAvaliable = drive.TotalSize / 1024.0 / 1024.0 / 1024.0;
            }
            else {
                lblDescription.Text = _resourceMonitor.InstalationFolder;
                var drive = new DriveInfo(_resourceMonitor.InstalationFolder);
                _resourceMonitor.TotalAvaliable = drive.TotalSize / 1024.0 / 1024.0 / 1024.0;
            }

            _keepRunning = false;
            UpdateValues();
            _keepRunning = true;
            GetValues();
        }

        private void LoadMemory() {
            lblTypeDesc.Text = "MEMORY";

            if (_resourceMonitor.ProcessExeLocatoin != "")
                lblDescription.Text = _resourceMonitor.Description;
            else
                lblDescription.Visible = false;
            _resourceMonitor.TotalAvaliable = int.Parse((GetTotalMemoryInBytes() / 1024 / 1024).ToString());
            _keepRunning                    = false;
            UpdateValues();
            _keepRunning = true;
            GetValues();
        }

        private void LoadCpu() {
            lblTypeDesc.Text = "CPU";
            if (_resourceMonitor.ProcessExeLocatoin != "") {
                lblDescription.Text = _resourceMonitor.Description;
            }
            else {
                var mos                                                        = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
                foreach (ManagementObject mo in mos.Get()) lblDescription.Text = mo["Name"].ToString();

                var thread = new Thread(GetCpuuSage);
                thread.Start();
            }

            _keepRunning = false;
            UpdateValues();
            _keepRunning = true;
            GetValues();
        }

        private void FrmUsedResourcesStatusWindow_Load(object sender, EventArgs e) {
            LoadData();
        }
    }
}