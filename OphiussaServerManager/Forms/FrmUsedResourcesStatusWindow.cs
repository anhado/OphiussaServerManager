using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OphiussaServerManager.Forms
{
    public partial class FrmUsedResourcesStatusWindow : Form
    {
        ResourceMonitor ResourceMonitor;

        private static DateTime lastTime;
        private static TimeSpan lastTotalProcessorTime;
        private static DateTime curTime;
        private static TimeSpan curTotalProcessorTime;

        public FrmUsedResourcesStatusWindow(ResourceMonitor resourceMonitor)
        {
            InitializeComponent();
            this.ResourceMonitor = resourceMonitor;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;
                GetValues();

            }
            catch (Exception)
            {
            }
            timer1.Enabled = true;
        }

        bool keepRunning = false;
        private void UpdateValues()
        {
            while (true)
            {

                switch (ResourceMonitor.Type)
                {
                    case ResourceType.CPU:

                        ResourceMonitor.Usage = 0;
                        if (ResourceMonitor.ProcessExeLocatoin != "")
                        {
                            Process process = Utils.GetProcessRunning(ResourceMonitor.ProcessExeLocatoin);
                            if (process != null)
                            {
                                curTime = DateTime.Now;
                                curTotalProcessorTime = process.TotalProcessorTime;

                                ResourceMonitor.Usage = (curTotalProcessorTime.TotalMilliseconds - lastTotalProcessorTime.TotalMilliseconds) / curTime.Subtract(lastTime).TotalMilliseconds / Convert.ToDouble(Environment.ProcessorCount);

                                lastTime = curTime;
                                lastTotalProcessorTime = curTotalProcessorTime;
                                ResourceMonitor.isRunning = true;
                            }
                            else
                            {
                                ResourceMonitor.Usage = 0;
                                ResourceMonitor.isRunning = false;
                            }
                        }
                        else
                        {
                            /*ResourceMonitor.Usage =*/
                            //GetCPUUSage();
                        }

                        break;
                    case ResourceType.Memory:

                        if (ResourceMonitor.ProcessExeLocatoin != "")
                        {
                            Process process = Utils.GetProcessRunning(ResourceMonitor.ProcessExeLocatoin);

                            if (process != null)
                            {
                                ResourceMonitor.Usage = int.Parse(((ulong.Parse(process.PrivateMemorySize64.ToString())) / 1024 / 1024).ToString());
                                ResourceMonitor.CalculateUsage = true;
                                ResourceMonitor.isRunning = true;
                            }
                            else
                            {
                                ResourceMonitor.Usage = 0;
                                ResourceMonitor.CalculateUsage = false;
                                ResourceMonitor.isRunning = false;
                            }
                        }
                        else
                            ResourceMonitor.Usage = int.Parse(((GetTotalMemoryInBytes() - GetAvaliableMemoryInBytes()) / 1024 / 1024).ToString());
                        break;
                    case ResourceType.DiskUsed:

                        if (ResourceMonitor.ProcessExeLocatoin != "")
                        {
                            DriveInfo drive = new DriveInfo(ResourceMonitor.ProcessExeLocatoin);
                            ResourceMonitor.Usage = ((DirSize(new DirectoryInfo(ResourceMonitor.ProcessExeLocatoin))) / 1024.0 / 1024.0 / 1024.0);
                        }
                        else
                        {
                            DriveInfo drive = new DriveInfo(ResourceMonitor.InstalationFolder);
                            ResourceMonitor.Usage = ((drive.TotalSize - drive.TotalFreeSpace) / 1024.0 / 1024.0 / 1024.0);
                        }
                        break;
                    default:
                        break;
                }

                if (!keepRunning) break;
                if (!UsefullTools.isFormRunning("FrmUsedResources")) break;
                if (!UsefullTools.isFormRunning("MainForm")) break;
                Thread.Sleep(ResourceMonitor.Timer);
            }
        }

        private void GetValues()
        {
            bool ShowUsage = ResourceMonitor.CalculateUsage;
            switch (ResourceMonitor.Type)
            {
                case ResourceType.CPU:

                    if (ResourceMonitor.ProcessExeLocatoin != "")
                    {
                        if (ResourceMonitor.isRunning)
                        {
                            ShowUsage = false;
                            lblUsage.ForeColor = Color.Black;
                        }
                        else
                        {
                            ShowUsage = true;
                            lblUsage.Text = "NOT RUNNING";
                            lblUsage.ForeColor = Color.Red;
                        }
                    }
                    //else
                    //    ResourceMonitor.Usage = GetCPUUSage();

                    break;
                case ResourceType.Memory:

                    if (ResourceMonitor.ProcessExeLocatoin != "")
                    {

                        if (ResourceMonitor.isRunning)
                        {
                            ShowUsage = false;
                            lblUsage.ForeColor = Color.Black;
                        }
                        else
                        {
                            ShowUsage = true;
                            lblUsage.Text = "NOT RUNNING";
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

            if (ResourceMonitor.CalculateUsage)
            {
                lblUsage.Text = Math.Round(ResourceMonitor.Usage, 2) + ResourceMonitor.Unit + "/" + Math.Round(ResourceMonitor.TotalAvaliable, 2) + ResourceMonitor.Unit;

                int totalProgress = (int)(ResourceMonitor.Usage / ResourceMonitor.TotalAvaliable * 100);

                progressBar1.Value = totalProgress;// ((ResourceMonitor.Usage / ResourceMonitor.TotalAvaliable) * 100).ToString().ToInt();
                lblUsage.Visible = true;
            }
            else
            {
                progressBar1.Value = (ResourceMonitor.Usage > 100 ? 100 : ResourceMonitor.Usage).ToString().ToInt();
                lblUsage.Visible = ShowUsage;
            }
            lblUsagePerc.Text = Math.Round(ResourceMonitor.Usage, 2).ToString() + ResourceMonitor.Unit;

        }

        private void GetCPUUSage()
        {
            while (true)
            {
                int usage = 0;
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfOS_Processor");
                foreach (ManagementObject obj in searcher.Get())
                {
                    var name = obj["Name"];
                    if (name.ToString() != "_Total")
                    {

                        usage = int.Parse(obj["PercentProcessorTime"].ToString());
                    }
                }
                ResourceMonitor.Usage = usage;
                if (!UsefullTools.isFormRunning("FrmUsedResources")) break;
                if (!UsefullTools.isFormRunning("MainForm")) break;
                Thread.Sleep(ResourceMonitor.Timer);
            }
        }

        private void LoadData()
        {
            try
            {
                switch (ResourceMonitor.Type)
                {
                    case ResourceType.CPU:
                        LoadCPU();
                        timer1.Interval = ResourceMonitor.Timer;
                        timer1.Enabled = true;
                        break;
                    case ResourceType.Memory:
                        LoadMemory();
                        timer1.Interval = ResourceMonitor.Timer;
                        timer1.Enabled = true;
                        break;
                    case ResourceType.DiskUsed:
                        LoadDisk();
                        timer1.Interval = ResourceMonitor.Timer;
                        timer1.Enabled = true;
                        break;
                    default:
                        break;
                }

                Thread thread = new Thread(new ThreadStart(UpdateValues));
                thread.Start();
            }
            catch (Exception ex)
            {
                lblDescription.Text = "ERROR";
            }
        }

        static ulong GetTotalMemoryInBytes()
        {
            return new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;
        }
        static ulong GetAvaliableMemoryInBytes()
        {
            return new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory;
        }

        public static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }

        private void LoadDisk()
        {
            lblTypeDesc.Text = "DISK USAGE";
            if (ResourceMonitor.ProcessExeLocatoin != "")
            {
                lblDescription.Text = ResourceMonitor.Description;
                DriveInfo drive = new DriveInfo(ResourceMonitor.ProcessExeLocatoin);
                ResourceMonitor.TotalAvaliable = (drive.TotalSize / 1024.0 / 1024.0 / 1024.0);
            }
            else
            {
                lblDescription.Text = ResourceMonitor.InstalationFolder;
                DriveInfo drive = new DriveInfo(ResourceMonitor.InstalationFolder);
                ResourceMonitor.TotalAvaliable = (drive.TotalSize / 1024.0 / 1024.0 / 1024.0);
            }
            keepRunning = false;
            UpdateValues();
            keepRunning = true;
            GetValues();
        }

        private void LoadMemory()
        {
            lblTypeDesc.Text = "MEMORY";

            if (ResourceMonitor.ProcessExeLocatoin != "")
            {
                lblDescription.Text = ResourceMonitor.Description;
            }
            else
            {
                lblDescription.Visible = false;
            }
            ResourceMonitor.TotalAvaliable = int.Parse((GetTotalMemoryInBytes() / 1024 / 1024).ToString());
            keepRunning = false;
            UpdateValues();
            keepRunning = true;
            GetValues();
        }

        private void LoadCPU()
        {
            lblTypeDesc.Text = "CPU";
            if (ResourceMonitor.ProcessExeLocatoin != "")
            {
                lblDescription.Text = ResourceMonitor.Description;
            }
            else
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
                foreach (ManagementObject mo in mos.Get())
                {
                    lblDescription.Text = mo["Name"].ToString();
                }

                Thread thread = new Thread(new ThreadStart(GetCPUUSage));
                thread.Start();
            }
            keepRunning = false;
            UpdateValues();
            keepRunning = true;
            GetValues();
        }

        private void FrmUsedResourcesStatusWindow_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
