﻿using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OphiussaServerManager.Components {
    public partial class AutomaticManagement : UserControl {
        internal AutoManageSettings _profile;
        public AutomaticManagement() {
            InitializeComponent();
        }

        public void LoadData(ref AutoManageSettings profile) {
            _profile = profile;
            Stopwatch sw = new Stopwatch();

            rbOnBoot.Checked = profile.AutoStartOn == AutoStart.OnBoot;
            rbOnLogin.Checked = profile.AutoStartOn == AutoStart.OnLogin;
            sw.Start();
            UsefullTools.LoadValuesToFields(_profile, this.Controls);

            sw.Stop();

            Console.WriteLine("Elapsed={0}", sw.Elapsed.TotalSeconds);
        }

        public void GetData(ref AutoManageSettings _profile) {
            Stopwatch sw = new Stopwatch();

            _profile.AutoStartOn = rbOnBoot.Checked ? AutoStart.OnBoot : AutoStart.OnLogin;
            sw.Start();
            UsefullTools.LoadFieldsToObject(ref _profile, this.Controls);

            sw.Stop();

            Console.WriteLine("Elapsed={0}", sw.Elapsed.TotalSeconds);

        }

        private void chkAutoStart_CheckedChanged(object sender, EventArgs e) {

            rbOnBoot.Enabled  = chkAutoStart.Checked;
            rbOnLogin.Enabled = chkAutoStart.Checked;
        }

        private void chkShutdown1_CheckedChanged(object sender, EventArgs e) {

            txtShutdow1.Enabled = chkShutdown1.Checked;
            chkSun1.Enabled     = chkShutdown1.Checked;
            chkMon1.Enabled     = chkShutdown1.Checked;
            chkTue1.Enabled     = chkShutdown1.Checked;
            chkWed1.Enabled     = chkShutdown1.Checked;
            chkThu1.Enabled     = chkShutdown1.Checked;
            chkFri1.Enabled     = chkShutdown1.Checked;
            chkSat1.Enabled     = chkShutdown1.Checked;
            chkUpdate1.Enabled  = chkShutdown1.Checked;
            chkRestart1.Enabled = chkShutdown1.Checked;
        }

        private void chkShutdown2_CheckedChanged(object sender, EventArgs e) {

            txtShutdow2.Enabled = chkShutdown2.Checked;
            chkSun2.Enabled     = chkShutdown2.Checked;
            chkMon2.Enabled     = chkShutdown2.Checked;
            chkTue2.Enabled     = chkShutdown2.Checked;
            chkWed2.Enabled     = chkShutdown2.Checked;
            chkThu2.Enabled     = chkShutdown2.Checked;
            chkFri2.Enabled     = chkShutdown2.Checked;
            chkSat2.Enabled     = chkShutdown2.Checked;
            chkUpdate2.Enabled  = chkShutdown2.Checked;
            chkRestart2.Enabled = chkShutdown2.Checked;
        }
    }
}
