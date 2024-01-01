using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Common.Models.SupportedServers;
using OphiussaServerManager.Forms;
using OphiussaServerManager.Tools;

namespace OphiussaServerManager.Components {
    public partial class ArkAdministration : UserControl {
        internal ArkProfile _profile;
        internal Profile    _Serverprofile;

        public ArkAdministration() {
            InitializeComponent();
        }

        private void LoadDefaultFieldValues() {
            try {
                var ret = NetworkTools.GetAllHostIp();

                txtLocalIP.DataSource    = ret;
                txtLocalIP.ValueMember   = "IP";
                txtLocalIP.DisplayMember = "Description";

                MainForm.Settings.Branchs.Distinct().ToList().ForEach(branch => { cbBranch.Items.Add(branch); });

                chkEnableCrossPlay.Enabled   = _Serverprofile.Type.ServerType == EnumServerType.ArkSurviveEvolved;
                chkEnablPublicIPEpic.Enabled = _Serverprofile.Type.ServerType == EnumServerType.ArkSurviveEvolved;
                ChkEpicOnly.Enabled          = _Serverprofile.Type.ServerType == EnumServerType.ArkSurviveEvolved;
                chkUseApi.Enabled            = _Serverprofile.Type.ServerType == EnumServerType.ArkSurviveAscended;
                txtBanUrl.Enabled            = chkUseBanUrl.Checked;
                var affinityModel = new List<ProcessorAffinityModel>();

                Enum.GetNames(typeof(ProcessPriority)).ToList().ForEach(e => {
                                                                            affinityModel.Add(new ProcessorAffinityModel {
                                                                                                                             Code = e,
                                                                                                                             Name = e
                                                                                                                         });
                                                                        });

                cboPriority.DataSource    = affinityModel;
                cboPriority.ValueMember   = "Code";
                cboPriority.DisplayMember = "Name";

                cboMap.DataSource    = SupportedServers.GetMapLists(_Serverprofile.Type.ServerType);
                cboMap.ValueMember   = "Key";
                cboMap.DisplayMember = "Description";
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                MessageBox.Show($"{MethodBase.GetCurrentMethod()?.Name}: {e.Message}");
            }
        }

        public void LoadData(ref ArkProfile profile, ref Profile serverprofile) {
            _profile       = profile;
            _Serverprofile = serverprofile;
            var sw = new Stopwatch();

            sw.Start();

            LoadDefaultFieldValues();
            UsefullTools.LoadValuesToFields(_profile, Controls);

            txtCommand.Text =
                _profile.GetCommandLinesArguments(MainForm.Settings, _Serverprofile, MainForm.LocaIp);
            sw.Stop();

            Console.WriteLine("ArkAdministration={0}", sw.Elapsed.TotalSeconds);
        }

        public void GetData(ref ArkProfile profile) {
            var sw = new Stopwatch();

            sw.Start();
            UsefullTools.LoadFieldsToObject(ref _profile, Controls);

            sw.Stop();

            Console.WriteLine("ArkAdministration={0}", sw.Elapsed.TotalSeconds);
        }

        private void txtServerPWD_DoubleClick(object sender, EventArgs e) {
            txtServerPWD.PasswordChar = txtServerPWD.PasswordChar == '\0' ? '*' : '\0';
        }

        private void txtAdminPass_DoubleClick(object sender, EventArgs e) {
            txtAdminPass.PasswordChar = txtServerPWD.PasswordChar == '\0' ? '*' : '\0';
        }

        private void txtSpePwd_DoubleClick(object sender, EventArgs e) {
            txtSpePwd.PasswordChar = txtServerPWD.PasswordChar == '\0' ? '*' : '\0';
        }

        private void btMods_Click(object sender, EventArgs e) {
            var frm = new FrmModManager();
            frm.UpdateModList = lst => { txtMods.Text = string.Join(",", lst.Select(x => x.ModId.ToString()).ToArray()); };

            frm.LoadMods(ref _Serverprofile, txtMods.Text);
        }

        private void button2_Click(object sender, EventArgs e) {
            txtCommand.Text =
                _profile.GetCommandLinesArguments(MainForm.Settings, _Serverprofile, MainForm.LocaIp);
        }

        private void btProcessorAffinity_Click(object sender, EventArgs e) {
            var frm = new FrmProcessors(_profile.CpuAffinity == "All",
                                        _profile.CpuAffinityList);
            frm.UpdateCpuAffinity = (all, lst) => {
                                        _profile.CpuAffinity = all
                                                                   ? "All"
                                                                   : string.Join(",", lst.FindAll(x => x.Selected).Select(x => x.ProcessorNumber.ToString()));
                                        _profile.CpuAffinityList = lst;
                                        txtAffinity.Text         = _profile.CpuAffinity;
                                    };
            frm.ShowDialog();
        }
    }
}