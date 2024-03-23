using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
//using OphiussaServerManager.Common.Helpers;

namespace OphiussaFramework.Components {
    public partial class exTrackBar : UserControl {
        public exTrackBar() {
            InitializeComponent();
        }

        //public float Scale { get; set; } = 1f;

        //public string Units {
        //    get => lblUnits.Text;
        //    set => lblUnits.Text = value;
        //}

        //public int Minimum {
        //    get => tbUC.Minimum;
        //    set => tbUC.Minimum = value;
        //}

        //public int Maximum {
        //    get => tbUC.Maximum;
        //    set => tbUC.Maximum = value;
        //}

        //public float Value {
        //    get => txtUC.Text.ToFloat();
        //    set {
        //        if (Scale == 1f)
        //            txtUC.Text = value.ToString(CultureInfo.InvariantCulture).ToInt().ToString(CultureInfo.InvariantCulture);
        //        else
        //            txtUC.Text = value.ToString(CultureInfo.InvariantCulture);
        //    }
        //}

        //public int TickFrequency {
        //    get => tbUC.TickFrequency;
        //    set => tbUC.TickFrequency = value;
        //}

        //public bool DisableTextBox {
        //    get => !txtUC.Enabled;
        //    set => txtUC.Enabled = !value;
        //}

        //public bool DisableTrackBar {
        //    get => !tbUC.Enabled;
        //    set => tbUC.Enabled = !value;
        //}

        //[EditorBrowsable(EditorBrowsableState.Always)]
        //[Browsable(true)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        //[Bindable(true)]
        //public override string Text {
        //    get => lblDesc.Text;
        //    set => lblDesc.Text = value;
        //}

        private void txtUC_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if (Scale == 1f)
            //    {
            //        int iValue = ((TextBox)sender).Text.ToInt();
            //        tbUC.SetValueEx(iValue);
            //    }
            //    else
            //    {
            //        float fValue = ((TextBox)sender).Text.ToFloat() * Scale;
            //        string value = Math.Round(fValue, 0).ToString(CultureInfo.InvariantCulture);
            //        tbUC.SetValueEx(value.ToInt());
            //    }
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //    MessageBox.Show(exception.Message);
            //}
        }

        private void tbUC_Scroll(object sender, EventArgs e)
        {
            //txtUC.Text = (((TrackBar)sender).Value / Scale).ToString(CultureInfo.InvariantCulture);
        }

        private void UcTrackBar_Resize(object sender, EventArgs e)
        {
            Height = 26;
        }

        private void txtUC_Enter(object sender, EventArgs e)
        {
            txtUC.BackColor = Color.LightSkyBlue;
        }

        private void txtUC_Leave(object sender, EventArgs e)
        {
            txtUC.BackColor = Color.White;
        }
    }
}