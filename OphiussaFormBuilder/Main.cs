using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq; 

namespace OphiussaFormBuilder
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e) {
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e) {


            List<string> content = System.IO.File.ReadAllLines(textBox1.Text).ToList();

            string locationString = content.First(l => l.Contains("LBLSTART.Location ="));

            int startindex = locationString.IndexOf(",");
            int end        = locationString.IndexOf(")");

            int location =  int.Parse(locationString.Substring(startindex+1, end - startindex-1));

            var                 p             = JsonConvert.DeserializeObject(richTextBox1.Text);
            
            JObject j = JObject.FromObject(p);

            Type                myType        = j.GetType();

            var                 pp    = j.Properties();
             

            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            foreach (JProperty prop in pp) {

                object propValue = prop.Type;

                // Do something with propValue
            }

        }
    }
}
