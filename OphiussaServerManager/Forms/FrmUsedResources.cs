using Newtonsoft.Json;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models.SupportedServers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OphiussaServerManager.Forms
{
    public partial class FrmUsedResources : Form
    {
        public FrmUsedResources()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //PerformanceCounter cpuCounter;
            //PerformanceCounter ramCounter;


            //cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            //ramCounter = new PerformanceCounter("Memory", "Available MBytes");

            //float test1 = getCurrentCpuUsage();
            //string test2 = getAvailableRAM();

            //float getCurrentCpuUsage()
            //{
            //    return cpuCounter.NextValue();
            //}

            //string getAvailableRAM()
            //{
            //    return ramCounter.NextValue() + "MB";
            //}

        }

        private void FrmUsedResources_Load(object sender, EventArgs e)
        {
            try
            {
                addform(new FrmUsedResourcesStatusWindow(new Common.Models.ResourceMonitor()
                {
                    Type = Common.Models.ResourceType.CPU,
                    Description = "",
                    CalculateUsage = false,
                    CategoryName = "Processor",
                    CounterName = "% Processor Time",
                    InstalationFolder = "",
                    InstanceName = "_Total",
                    ProcessExeLocatoin = "",
                    Usage = 0,
                    TotalAvaliable = 0,
                    Unit = "%"
                }));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                addform(new FrmUsedResourcesStatusWindow(new Common.Models.ResourceMonitor()
                {
                    Type = Common.Models.ResourceType.Memory,
                    Description = "",
                    CalculateUsage = true,
                    CategoryName = "Memory",
                    CounterName = "Available MBytes",
                    InstalationFolder = "",
                    InstanceName = "",
                    ProcessExeLocatoin = "",
                    Usage = 0,
                    TotalAvaliable = 0,
                    Unit = "MB"
                }));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                addform(new FrmUsedResourcesStatusWindow(new Common.Models.ResourceMonitor()
                {
                    Type = Common.Models.ResourceType.DiskUsed,
                    Description = "",
                    CalculateUsage = true,
                    CategoryName = "",
                    CounterName = "",
                    InstalationFolder = Path.GetPathRoot(MainForm.Settings.DataFolder),
                    InstanceName = "",
                    ProcessExeLocatoin = "",
                    Usage = 0,
                    TotalAvaliable = 0,
                    Unit = "GB",
                    Timer = 60000
                }));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                string dir = MainForm.Settings.DataFolder + "Profiles\\";
                if (!Directory.Exists(dir))
                {
                    return;
                }

                string[] files = System.IO.Directory.GetFiles(dir);
                foreach (string file in files)
                {
                    Profile p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(file));

                    addform(new FrmUsedResourcesStatusWindow(new Common.Models.ResourceMonitor()
                    {
                        Type = Common.Models.ResourceType.CPU,
                        Description = p.ARKConfiguration.Administration.ServerName,
                        CalculateUsage = false,
                        CategoryName = "Processor",
                        CounterName = "% Processor Time",
                        InstalationFolder = "",
                        InstanceName = "_Total",
                        ProcessExeLocatoin = Path.Combine(p.InstallLocation, p.Type.ExecutablePath),
                        Usage = 0,
                        TotalAvaliable = 0,
                        Unit = "%",
                        Timer = 30000
                    }));


                    addform(new FrmUsedResourcesStatusWindow(new Common.Models.ResourceMonitor()
                    {
                        Type = Common.Models.ResourceType.Memory,
                        Description = p.ARKConfiguration.Administration.ServerName,
                        CalculateUsage = true,
                        CategoryName = "Memory",
                        CounterName = "Available MBytes",
                        InstalationFolder = "",
                        InstanceName = "",
                        ProcessExeLocatoin = Path.Combine(p.InstallLocation, p.Type.ExecutablePath),
                        Usage = 0,
                        TotalAvaliable = 0,
                        Unit = "MB",
                        Timer = 30000
                    }));

                    addform(new FrmUsedResourcesStatusWindow(new Common.Models.ResourceMonitor()
                    {
                        Type = Common.Models.ResourceType.DiskUsed,
                        Description = p.ARKConfiguration.Administration.ServerName,
                        CalculateUsage = true,
                        CategoryName = "",
                        CounterName = "",
                        InstalationFolder = "",
                        InstanceName = "",
                        ProcessExeLocatoin = p.InstallLocation,
                        Usage = 0,
                        TotalAvaliable = 0,
                        Unit = "GB",
                        Timer = 60000
                    }));

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void addform(Form f)
        {
            Panel panel = new Panel();
            panel.Width = f.Width;
            panel.Height = f.Height;
            panel.BorderStyle = BorderStyle.Fixed3D;

            flPanel.Controls.Add(panel);

            f.TopLevel = false;
            //no border if needed
            f.FormBorderStyle = FormBorderStyle.None;
            f.AutoScaleMode = AutoScaleMode.Dpi;

            if (!panel.Controls.Contains(f))
            {
                panel.Controls.Add(f);
                f.Dock = DockStyle.Fill;
                f.Show();
                Refresh();
            }
            Refresh();
        }

    }
}
