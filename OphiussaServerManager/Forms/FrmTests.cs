using System;
using System.Linq;
using System.Management;
using System.Windows.Forms;
using OphiussaServerManager.Common;
using OphiussaServerManager.Tools;

namespace OphiussaServerManager.Forms {
    public partial class FrmTests : Form {
        public FrmTests() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            var steam = new SteamUtils(MainForm.Settings);
            //
            //var details = steam.GetSteamModDetails("346110");

            var s = steam.GetSteamModDetails(textBox1.Text.Split(',').ToList());
        }

        private void button2_Click(object sender, EventArgs e) {
            var curseForgeUtils = new CurseForgeUtils(MainForm.Settings);

            //var s = curseForgeUtils.GetCurseForgeModDetails("83374");

            var s = curseForgeUtils.GetCurseForgeModDetails(textBox2.Text.Split(',').ToList());
        }

        private void FrmTests_Load(object sender, EventArgs e) {
            exListBox2.Items.Add(new ExListBoxItem("14", "John, the Tester", @"First details text is used to check it out, if text fits correctly the bounds of an item.
As you can see, everything fits nicely.
If it's shown correctly, that's should be last line, that you see.
If you can see this line, it looks like it overlaps something and there's a bug in the code.
", null));
            exListBox2.Items.Add(new ExListBoxItem("99", "Bill", "phone +345645464\n fax +6546546546\n email email@email.com", null));
            exListBox2.Items.Add(new ExListBoxItem("71", "Peter", "ICQ 56465464\n msn hot@hotmail.com\n phone +5465464654", null));

            foreach (var item in new ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get()) Console.WriteLine("Number Of Logical Processors: {0}", item["NumberOfLogicalProcessors"]);
        }

        private void button3_Click(object sender, EventArgs e) {
            var x = new NotificationController();
            x.ConnectClient();
            x.SendCloseCommand();
        }

        private void button4_Click(object sender, EventArgs e) {
            var x = new NotificationController();
            x.StartServer();
        }
    }
}