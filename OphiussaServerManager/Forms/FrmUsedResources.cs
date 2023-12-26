using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;

namespace OphiussaServerManager.Forms {
    public partial class FrmUsedResources : Form {
        public FrmUsedResources() {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e) {
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

        private void FrmUsedResources_Load(object sender, EventArgs e) {
            try {
                Addform(new FrmUsedResourcesStatusWindow(new ResourceMonitor {
                                                                                 Type               = ResourceType.Cpu,
                                                                                 Description        = "",
                                                                                 CalculateUsage     = false,
                                                                                 CategoryName       = "Processor",
                                                                                 CounterName        = "% Processor Time",
                                                                                 InstalationFolder  = "",
                                                                                 InstanceName       = "_Total",
                                                                                 ProcessExeLocatoin = "",
                                                                                 Usage              = 0,
                                                                                 TotalAvaliable     = 0,
                                                                                 Unit               = "%"
                                                                             }));
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            try {
                Addform(new FrmUsedResourcesStatusWindow(new ResourceMonitor {
                                                                                 Type               = ResourceType.Memory,
                                                                                 Description        = "",
                                                                                 CalculateUsage     = true,
                                                                                 CategoryName       = "Memory",
                                                                                 CounterName        = "Available MBytes",
                                                                                 InstalationFolder  = "",
                                                                                 InstanceName       = "",
                                                                                 ProcessExeLocatoin = "",
                                                                                 Usage              = 0,
                                                                                 TotalAvaliable     = 0,
                                                                                 Unit               = "MB"
                                                                             }));
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            try {
                Addform(new FrmUsedResourcesStatusWindow(new ResourceMonitor {
                                                                                 Type               = ResourceType.DiskUsed,
                                                                                 Description        = "",
                                                                                 CalculateUsage     = true,
                                                                                 CategoryName       = "",
                                                                                 CounterName        = "",
                                                                                 InstalationFolder  = Path.GetPathRoot(MainForm.Settings.DataFolder),
                                                                                 InstanceName       = "",
                                                                                 ProcessExeLocatoin = "",
                                                                                 Usage              = 0,
                                                                                 TotalAvaliable     = 0,
                                                                                 Unit               = "GB",
                                                                                 Timer              = 60000
                                                                             }));
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            try {
                string dir = MainForm.Settings.DataFolder + "Profiles\\";
                if (!Directory.Exists(dir)) return;

                string[] files = Directory.GetFiles(dir);
                foreach (string file in files) {
                    var p = JsonConvert.DeserializeObject<Profile>(File.ReadAllText(file));

                    Addform(new FrmUsedResourcesStatusWindow(new ResourceMonitor {
                                                                                     Type               = ResourceType.Cpu,
                                                                                     Description        = p.ArkConfiguration.ServerName,
                                                                                     CalculateUsage     = false,
                                                                                     CategoryName       = "Processor",
                                                                                     CounterName        = "% Processor Time",
                                                                                     InstalationFolder  = "",
                                                                                     InstanceName       = "_Total",
                                                                                     ProcessExeLocatoin = Path.Combine(p.InstallLocation, p.Type.ExecutablePath),
                                                                                     Usage              = 0,
                                                                                     TotalAvaliable     = 0,
                                                                                     Unit               = "%",
                                                                                     Timer              = 30000
                                                                                 }));


                    Addform(new FrmUsedResourcesStatusWindow(new ResourceMonitor {
                                                                                     Type               = ResourceType.Memory,
                                                                                     Description        = p.ArkConfiguration.ServerName,
                                                                                     CalculateUsage     = true,
                                                                                     CategoryName       = "Memory",
                                                                                     CounterName        = "Available MBytes",
                                                                                     InstalationFolder  = "",
                                                                                     InstanceName       = "",
                                                                                     ProcessExeLocatoin = Path.Combine(p.InstallLocation, p.Type.ExecutablePath),
                                                                                     Usage              = 0,
                                                                                     TotalAvaliable     = 0,
                                                                                     Unit               = "MB",
                                                                                     Timer              = 30000
                                                                                 }));

                    Addform(new FrmUsedResourcesStatusWindow(new ResourceMonitor {
                                                                                     Type               = ResourceType.DiskUsed,
                                                                                     Description        = p.ArkConfiguration.ServerName,
                                                                                     CalculateUsage     = true,
                                                                                     CategoryName       = "",
                                                                                     CounterName        = "",
                                                                                     InstalationFolder  = "",
                                                                                     InstanceName       = "",
                                                                                     ProcessExeLocatoin = p.InstallLocation,
                                                                                     Usage              = 0,
                                                                                     TotalAvaliable     = 0,
                                                                                     Unit               = "GB",
                                                                                     Timer              = 60000
                                                                                 }));
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        public void Addform(Form f) {
            var panel = new Panel();
            panel.Width       = f.Width;
            panel.Height      = f.Height;
            panel.BorderStyle = BorderStyle.Fixed3D;

            flPanel.Controls.Add(panel);

            f.TopLevel = false;
            //no border if needed
            f.FormBorderStyle = FormBorderStyle.None;
            f.AutoScaleMode   = AutoScaleMode.Dpi;

            if (!panel.Controls.Contains(f)) {
                panel.Controls.Add(f);
                f.Dock = DockStyle.Fill;
                f.Show();
                Refresh();
            }

            Refresh();
        }
    }
}