using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OphiussaServerManager.Forms
{
    public partial class FrmProcessors : Form
    {
        public Action<bool, List<ProcessorAffinity>> updateCpuAffinity;
        public FrmProcessors(bool all, List<ProcessorAffinity> processors)
        {
            InitializeComponent();
            if (all) { chkAll.Checked = true; } else { chkAll.Checked = false; };
            processorAffinityList.Clear();
            for (int i = 0; i < Utils.GetProcessorCount(); i++)
            {
                processorAffinityList.Add(
                    new ProcessorAffinity()
                    {
                        ProcessorNumber = i,
                        Selected = processors.DefaultIfEmpty(new ProcessorAffinity() { Selected = true, ProcessorNumber = i }).FirstOrDefault(x => x.ProcessorNumber == i).Selected
                    }
                    );
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            btAll.Enabled = !chkAll.Checked;
            btNone.Enabled = !chkAll.Checked;
            grd.Enabled = !chkAll.Checked;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            List<ProcessorAffinity> processors = new List<ProcessorAffinity>();
            foreach (ProcessorAffinity aff in processorAffinityList)
            {
                if (chkAll.Checked) aff.Selected = true;
                processors.Add(aff);
                
            }

            updateCpuAffinity.Invoke(chkAll.Checked, processors);
            this.Close();
        }

        private void btAll_Click(object sender, EventArgs e)
        {
            foreach (ProcessorAffinity item in processorAffinityList)
            {
                item.Selected = true;
            }
            grd.Refresh();
        }

        private void btNone_Click(object sender, EventArgs e)
        {
            foreach (ProcessorAffinity item in processorAffinityList)
            {
                item.Selected = false;
            }
            grd.Refresh();
        }

        private void btCancel_Click(object sender, EventArgs e)
        { 
            this.Close();
        }
    }
}
