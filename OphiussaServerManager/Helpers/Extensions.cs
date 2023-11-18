using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OphiussaServerManager.Helpers
{
    public static class RichTextBoxExtensions
    {
        public static void AppendTextWithTimeStamp(this RichTextBox box, string text, Color color)
        {
            text = "[" + DateTime.Now.ToString("u") + "] " + text + "\n";
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            text = text + "\n";
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}
