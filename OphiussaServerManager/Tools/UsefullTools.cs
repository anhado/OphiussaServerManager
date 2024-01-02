using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using OphiussaServerManager.Common.Helpers;
using OphiussaServerManager.Common.Models;
using OphiussaServerManager.Common.Models.Profiles;
using OphiussaServerManager.Components;

namespace OphiussaServerManager.Tools {
    internal static class UsefullTools {
        public static MainForm MainForm;

        internal static bool IsFormRunning(string formName) {
            var fc = Application.OpenForms;

            foreach (Form frm in fc)
                //iterate through
                if (frm.Name == formName)
                    return true;
            return false;
        }

        internal static void ManageCheckGroupBox(CheckBox chk, GroupBox grp) {
            // Make sure the CheckBox isn't in the GroupBox.
            // This will only happen the first time.
            if (chk.Parent == grp) {
                // Reparent the CheckBox so it's not in the GroupBox.
                grp.Parent.Controls.Add(chk);

                // Adjust the CheckBox's location.
                chk.Location = new Point(
                                         chk.Left + grp.Left,
                                         chk.Top  + grp.Top);

                // Move the CheckBox to the top of the stacking order.
                chk.BringToFront();
            }

            // Enable or disable the GroupBox.
            grp.Enabled = chk.Checked;
        }

        internal static string GetValueString(AutoManageSettings profile, string fieldName) {
            string ret             = "";
            var    fields          = profile.GetType().GetProperties();
            var    pInfo           = fields.FirstOrDefault(f => f.Name == fieldName);
            if (pInfo != null) ret = pInfo.GetValue(profile).ToString();

            return ret;
        }

        internal static float GetValueFloat(AutoManageSettings profile, string fieldName) {
            float ret    = 0;
            var   fields = profile.GetType().GetProperties();
            var   pInfo  = fields.FirstOrDefault(f => f.Name == fieldName);
            if (pInfo != null) {
                string v                                                                                  = pInfo.GetValue(profile).ToString();
                if (float.TryParse(v, NumberStyles.Any, CultureInfo.InvariantCulture, out float res)) ret = res;
            }

            return ret;
        }

        internal static bool GetValueBool(AutoManageSettings profile, string fieldName) {
            bool ret    = false;
            var  fields = profile.GetType().GetProperties();
            var  pInfo  = fields.FirstOrDefault(f => f.Name == fieldName);
            if (pInfo != null) {
                string v                                = pInfo.GetValue(profile).ToString();
                if (bool.TryParse(v, out bool res)) ret = res;
            }

            return ret;
        }


        private static void txt_Enter(object sender, EventArgs e) {
            var txt = (TextBox)sender;

            if (txt.ReadOnly == false && txt.Enabled) txt.BackColor = Color.LightSkyBlue;
        }

        private static void txt_Leave(object sender, EventArgs e) {
            var txt = (TextBox)sender;

            if (txt.ReadOnly == false && txt.Enabled) txt.BackColor = Color.White;
        }

        internal static void LoadValuesToFields(AutoManageSettings profile, Control.ControlCollection controls) {
            foreach (Control item in controls)
                if (item is System.Windows.Forms.TextBox txt) {
                    txt.Text  =  GetValueString(profile, txt.Tag?.ToString());
                    txt.Enter += txt_Enter;
                    txt.Leave += txt_Leave;
                }
                else if (item is System.Windows.Forms.MaskedTextBox mtxt) {
                    mtxt.Text = GetValueString(profile, mtxt.Tag?.ToString());
                }
                else if (item is exTrackBar trackBar) {
                    trackBar.Value = GetValueFloat(profile, trackBar.Tag?.ToString());
                }
                else if (item is System.Windows.Forms.CheckBox chk) {
                    chk.Checked = GetValueBool(profile, chk.Tag?.ToString());
                }
                else if (item is System.Windows.Forms.ComboBox cbo) {
                    cbo.SelectedValue = GetValueString(profile, cbo.Tag?.ToString());
                }
                else if (item is System.Windows.Forms.Label lbl  ||
                         item is System.Windows.Forms.Button btn ||
                         item is System.Windows.Forms.RadioButton btn2) {
                    //do nothing
                }
                else if (item.HasChildren) {
                    LoadValuesToFields(profile, item.Controls);
                }
                else {
                    throw new Exception("Not Supported");
                }
        }

        internal static void LoadValuesToFields(ArkProfile profile, Control.ControlCollection controls) {
            //foreach (Control item in controls) {
            for (int i = 0; i < controls.Count; i++) {
                var item = controls[i];
                try { 
                    if (item is System.Windows.Forms.TextBox txt) {
                        txt.Text  =  GetValueString(profile, txt.Tag?.ToString());
                        txt.Enter += txt_Enter;
                        txt.Leave += txt_Leave;
                    }
                    else if (item is System.Windows.Forms.MaskedTextBox mtxt) {
                        mtxt.Text = GetValueString(profile, mtxt.Tag?.ToString());
                    }
                    else if (item is exTrackBar trackBar) {
                        if (trackBar.Scale == 1)
                            trackBar.Value = GetValueInt(profile, trackBar.Tag?.ToString());
                        else
                            trackBar.Value = GetValueFloat(profile, trackBar.Tag?.ToString());
                    }
                    else if (item is System.Windows.Forms.CheckBox chk) {
                        chk.Checked = GetValueBool(profile, chk.Tag?.ToString());
                    }
                    else if (item is System.Windows.Forms.ComboBox cbo) {
                        cbo.SelectedValue = GetValueString(profile, cbo.Tag?.ToString());
                    }
                    else if (item is System.Windows.Forms.Label lbl      ||
                             item is System.Windows.Forms.Button btn     ||
                             item is System.Windows.Forms.HScrollBar Hsb ||
                             item is System.Windows.Forms.VScrollBar Vsb ||
                             item is System.Windows.Forms.GroupBox grp   ||
                             item is System.Windows.Forms.TrackBar tb    ||
                             item is System.Windows.Forms.DataGridView grd) {
                        //do nothing 
                    }
                    else if (item.HasChildren) {
                        LoadValuesToFields(profile, item.Controls);
                    }
                    else {
                        throw new Exception("Not Supported");
                    }
                }
                catch (Exception e) {
                    Console.WriteLine(e);
                }
            }
        }

        internal static void LoadValuesToFields(Profile profile, Control.ControlCollection controls) {
            foreach (Control item in controls)
                try {
                    if (item is System.Windows.Forms.TextBox txt) {
                        txt.Text  =  GetValueString(profile, txt.Tag?.ToString());
                        txt.Enter += txt_Enter;
                        txt.Leave += txt_Leave;
                    }
                    else if (item is exTrackBar trackBar) {
                        trackBar.Value = GetValueFloat(profile, trackBar.Tag?.ToString());
                    }
                    else if (item is System.Windows.Forms.CheckBox chk) {
                        chk.Checked = GetValueBool(profile, chk.Tag?.ToString());
                    }
                    else if (item is System.Windows.Forms.ComboBox cbo) {
                        cbo.SelectedValue = GetValueString(profile, cbo.Tag?.ToString());
                    }
                    else if (item is System.Windows.Forms.Label lbl ||
                             item is System.Windows.Forms.Button btn) {
                        //do nothing
                    }
                    else if (item.HasChildren) {
                        LoadValuesToFields(profile, item.Controls);
                    }
                    else {
                        throw new Exception("Not Supported");
                    }
                }
                catch (Exception e) {
                    OphiussaLogger.Logger.Error(e);
                }
        }

        internal static string GetValueString(ArkProfile profile, string fieldName) {
            string ret             = "";
            var    fields          = profile.GetType().GetProperties();
            var    pInfo           = fields.FirstOrDefault(f => f.Name == fieldName);
            if (pInfo != null) ret = pInfo.GetValue(profile)?.ToString();

            return ret;
        }

        internal static float GetValueFloat(ArkProfile profile, string fieldName) {
            float ret    = 0;
            var   fields = profile.GetType().GetProperties();
            var   pInfo  = fields.FirstOrDefault(f => f.Name == fieldName);
            if (pInfo != null) {
                string v                                                                                  = pInfo.GetValue(profile)?.ToString().Replace(",", ".");
                if (float.TryParse(v, NumberStyles.Any, CultureInfo.InvariantCulture, out float res)) ret = res;
            }

            return ret;
        }

        internal static int GetValueInt(ArkProfile profile, string fieldName) {
            int ret    = 0;
            var fields = profile.GetType().GetProperties();
            var pInfo  = fields.FirstOrDefault(f => f.Name == fieldName);
            if (pInfo != null) {
                string v                                                                              = pInfo.GetValue(profile)?.ToString().Replace(",", ".");
                if (int.TryParse(v, NumberStyles.Any, CultureInfo.InvariantCulture, out int res)) ret = res;
            }

            return ret;
        }


        internal static bool GetValueBool(ArkProfile profile, string fieldName) {
            bool ret    = false;
            var  fields = profile.GetType().GetProperties();
            var  pInfo  = fields.FirstOrDefault(f => f.Name == fieldName);
            if (pInfo != null) {
                string v                                = pInfo.GetValue(profile)?.ToString();
                if (bool.TryParse(v, out bool res)) ret = res;
            }

            return ret;
        }


        internal static string GetValueString(Profile profile, string fieldName) {
            string ret             = "";
            var    fields          = profile.GetType().GetProperties();
            var    pInfo           = fields.FirstOrDefault(f => f.Name == fieldName);
            if (pInfo != null) ret = pInfo.GetValue(profile)?.ToString();

            return ret;
        }

        internal static float GetValueFloat(Profile profile, string fieldName) {
            float ret    = 0;
            var   fields = profile.GetType().GetProperties();
            var   pInfo  = fields.FirstOrDefault(f => f.Name == fieldName);
            if (pInfo != null) {
                string v                                                                                  = pInfo.GetValue(profile)?.ToString();
                if (float.TryParse(v, NumberStyles.Any, CultureInfo.InvariantCulture, out float res)) ret = res;
            }

            return ret;
        }

        internal static bool GetValueBool(Profile profile, string fieldName) {
            bool ret    = false;
            var  fields = profile.GetType().GetProperties();
            var  pInfo  = fields.FirstOrDefault(f => f.Name == fieldName);
            if (pInfo != null) {
                string v                                = pInfo.GetValue(profile)?.ToString();
                if (bool.TryParse(v, out bool res)) ret = res;
            }

            return ret;
        }

        internal static void SetValueString(ref ArkProfile profile, string fieldName, string value) {
            try {
                var fields = profile.GetType().GetProperties();
                var pInfo  = fields.FirstOrDefault(f => f.Name == fieldName);
                if (pInfo != null) {
                    if (pInfo.PropertyType.Name == "String")
                        pInfo.SetValue(profile, value);
                    else if (pInfo.PropertyType.Name == "Single")
                        pInfo.SetValue(profile, value);
                    else if (pInfo.PropertyType.Name == "Int32")
                        pInfo.SetValue(profile, value.ToString(CultureInfo.InvariantCulture).ToInt());
                    else
                        throw new Exception("Not Supported");
                }
            }
            catch (Exception e) {
                Console.WriteLine($"{fieldName}:" + e);
            }
        }


        internal static void SetValueFloat(ref ArkProfile profile, string fieldName, float value) {
            try {
                var fields = profile.GetType().GetProperties();
                var pInfo  = fields.FirstOrDefault(f => f.Name == fieldName);
                if (pInfo != null) {
                    if (pInfo.PropertyType.Name == "Single")
                        pInfo.SetValue(profile, value);
                    else if (pInfo.PropertyType.Name == "Int32")
                        pInfo.SetValue(profile, value.ToString(CultureInfo.InvariantCulture).ToInt());
                    else
                        throw new Exception("Not Supported");
                }
            }
            catch (Exception e) {
                Console.WriteLine($"{fieldName}:" + e);
            }
        }

        internal static void SetValueBool(ref ArkProfile profile, string fieldName, bool value) {
            try {
                var fields = profile.GetType().GetProperties();
                var pInfo  = fields.FirstOrDefault(f => f.Name == fieldName);
                if (pInfo != null) pInfo.SetValue(profile, value);
            }
            catch (Exception e) {
                Console.WriteLine($"{fieldName}:" + e);
            }
        }


        internal static void SetValueString(ref AutoManageSettings profile, string fieldName, string value) {
            try {
                var fields = profile.GetType().GetProperties();
                var pInfo  = fields.FirstOrDefault(f => f.Name == fieldName);
                if (pInfo != null) {
                    if (pInfo.PropertyType.Name == "String")
                        pInfo.SetValue(profile, value);
                    else if (pInfo.PropertyType.Name == "Single")
                        pInfo.SetValue(profile, value);
                    else if (pInfo.PropertyType.Name == "Int32")
                        pInfo.SetValue(profile, value.ToString(CultureInfo.InvariantCulture).ToInt());
                    else
                        throw new Exception("Not Supported");
                }
            }
            catch (Exception e) {
                Console.WriteLine($"{fieldName}:" + e);
            }
        }


        internal static void SetValueFloat(ref AutoManageSettings profile, string fieldName, float value) {
            try {
                var fields = profile.GetType().GetProperties();
                var pInfo  = fields.FirstOrDefault(f => f.Name == fieldName);
                if (pInfo != null) {
                    if (pInfo.PropertyType.Name == "Single")
                        pInfo.SetValue(profile, value);
                    else if (pInfo.PropertyType.Name == "Int32")
                        pInfo.SetValue(profile, value.ToString(CultureInfo.InvariantCulture).ToInt());
                    else
                        throw new Exception("Not Supported");
                }
            }
            catch (Exception e) {
                Console.WriteLine($"{fieldName}:" + e);
            }
        }

        internal static void SetValueBool(ref AutoManageSettings profile, string fieldName, bool value) {
            try {
                var fields = profile.GetType().GetProperties();
                var pInfo  = fields.FirstOrDefault(f => f.Name == fieldName);
                if (pInfo != null) pInfo.SetValue(profile, value);
            }
            catch (Exception e) {
                Console.WriteLine($"{fieldName}:" + e);
            }
        }

        internal static void SetValueString(ref Profile profile, string fieldName, string value) {
            try {
                var fields = profile.GetType().GetProperties();
                var pInfo  = fields.FirstOrDefault(f => f.Name == fieldName);
                if (pInfo != null) {
                    if (pInfo.PropertyType.Name == "String")
                        pInfo.SetValue(profile, value);
                    else if (pInfo.PropertyType.Name == "Single")
                        pInfo.SetValue(profile, value);
                    else if (pInfo.PropertyType.Name == "Int32")
                        pInfo.SetValue(profile, value.ToString(CultureInfo.InvariantCulture).ToInt());
                    else
                        throw new Exception("Not Supported");
                }
            }
            catch (Exception e) {
                Console.WriteLine($"{fieldName}:" + e);
            }
        }

        internal static void SetValueFloat(ref Profile profile, string fieldName, float value) {
            try {
                var fields = profile.GetType().GetProperties();
                var pInfo  = fields.FirstOrDefault(f => f.Name == fieldName);
                if (pInfo != null) {
                    if (pInfo.PropertyType.Name == "Single")
                        pInfo.SetValue(profile, value);
                    else if (pInfo.PropertyType.Name == "Int32")
                        pInfo.SetValue(profile, value.ToString(CultureInfo.InvariantCulture).ToInt());
                    else
                        throw new Exception("Not Supported");
                }
            }
            catch (Exception e) {
                Console.WriteLine($"{fieldName}:" + e);
            }
        }

        internal static void SetValueBool(ref Profile profile, string fieldName, bool value) {
            try {
                var fields = profile.GetType().GetProperties();
                var pInfo  = fields.FirstOrDefault(f => f.Name == fieldName);
                if (pInfo != null) pInfo.SetValue(profile, value);
            }
            catch (Exception e) {
                Console.WriteLine($"{fieldName}:" + e);
            }
        }

        internal static void LoadFieldsToObject(ref ArkProfile profile, Control.ControlCollection controls) {
            foreach (Control item in controls)
                if (item is System.Windows.Forms.TextBox txt) {
                    SetValueString(ref profile, txt.Tag?.ToString(), txt.Text);
                }
                else if (item is exTrackBar trackBar) {
                    SetValueFloat(ref profile, trackBar.Tag?.ToString(), trackBar.Value);
                }
                else if (item is System.Windows.Forms.CheckBox chk) {
                    SetValueBool(ref profile, chk.Tag?.ToString(), chk.Checked);
                }
                else {
                    if (item.HasChildren) LoadFieldsToObject(ref profile, item.Controls);
                }
        }

        internal static void LoadFieldsToObject(ref AutoManageSettings profile, Control.ControlCollection controls) {
            foreach (Control item in controls)
                if (item is System.Windows.Forms.TextBox txt) {
                    SetValueString(ref profile, txt.Tag?.ToString(), txt.Text);
                }
                else if (item is exTrackBar trackBar) {
                    SetValueFloat(ref profile, trackBar.Tag?.ToString(), trackBar.Value);
                }
                else if (item is System.Windows.Forms.CheckBox chk) {
                    SetValueBool(ref profile, chk.Tag?.ToString(), chk.Checked);
                }
                else {
                    if (item.HasChildren) LoadFieldsToObject(ref profile, item.Controls);
                }
        }

        internal static void LoadFieldsToObject(ref Profile profile, Control.ControlCollection controls) {
            foreach (Control item in controls) {
                if (item is System.Windows.Forms.TextBox txt) {
                    SetValueString(ref profile, txt.Tag?.ToString(), txt.Text);
                }
                else if (item is exTrackBar trackBar) {
                    SetValueFloat(ref profile, trackBar.Tag?.ToString(), trackBar.Value);
                }
                else if (item is System.Windows.Forms.CheckBox chk) {
                    SetValueBool(ref profile, chk.Tag?.ToString(), chk.Checked);
                }
                else {
                    throw new Exception("Not Supported");
                }

                if (item.HasChildren) LoadFieldsToObject(ref profile, item.Controls);
            }
        }
    }
}