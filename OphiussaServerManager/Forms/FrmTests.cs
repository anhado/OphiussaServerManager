using OphiussaServerManager.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms; 

namespace OphiussaServerManager.Forms
{
    public partial class FrmTests : Form
    {
        public FrmTests()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            SteamUtils steam = new SteamUtils(MainForm.Settings);
            //
            //var details = steam.GetSteamModDetails("346110");

            var s = steam.GetSteamModDetails(textBox1.Text.Split(',').ToList());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            CurseForgeUtils curseForgeUtils =  new CurseForgeUtils(MainForm.Settings);

            //var s = curseForgeUtils.GetCurseForgeModDetails("83374");

            var s = curseForgeUtils.GetCurseForgeModDetails(textBox2.Text.Split(',').ToList());
        }

        private void FrmTests_Load(object sender, EventArgs e)
        {

            exListBox1.Items.Add(new exListBoxItem("14", "John, the Tester", @"First details text is used to check it out, if text fits correctly the bounds of an item.
As you can see, everything fits nicely.
If it's shown correctly, that's should be last line, that you see.
If you can see this line, it looks like it overlaps something and there's a bug in the code.
"));
            exListBox1.Items.Add(new exListBoxItem("99", "Bill", "phone +345645464\n fax +6546546546\n email email@email.com"));
            exListBox1.Items.Add(new exListBoxItem("71", "Peter", "ICQ 56465464\n msn hot@hotmail.com\n phone +5465464654"));

            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
            {
                Console.WriteLine("Number Of Logical Processors: {0}", item["NumberOfLogicalProcessors"]);
            }
        }
    }
}
