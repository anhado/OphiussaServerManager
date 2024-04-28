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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OphiussaFormBuilder.Models;

namespace OphiussaFormBuilder {
    public partial class Main : Form {
        public Main() {
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
            int end = locationString.IndexOf(")");

            int location = int.Parse(locationString.Substring(startindex + 1, end - startindex - 1));

            var p = JsonConvert.DeserializeObject(richTextBox1.Text);

            JObject j = JObject.FromObject(p);

            Type myType = j.GetType();

            var pp = j.Properties();

            List<ListOfObjects> list = new List<ListOfObjects>();

            //IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

            GenerateObjectList(pp, j, ref list, "Main Settings");

            (new FrmAnalyzedData(list)).ShowDialog();

        }

        private void GenerateObjectList(IEnumerable<JProperty> pp, JObject j, ref List<ListOfObjects> list, string Group) {

            foreach (JProperty prop in pp) {

                var prop1 = prop.Value;

                switch (prop.Type) {
                    case JTokenType.None:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Object:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Array:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Constructor:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Property when prop1.Type == JTokenType.Integer:
                        list.Add(new ListOfObjects() { Group = Group, ObjectType = ObjectType.ExTrackBar, PropertyName = prop.Name, Description = Regex.Replace(prop.Name, "(\\B[A-Z])", " $1"), Value = prop.Value });
                        break;
                    case JTokenType.Property when prop1.Type == JTokenType.Boolean:
                        list.Add(new ListOfObjects() { Group = Group, ObjectType = ObjectType.CheckBox, PropertyName = prop.Name, Description = Regex.Replace(prop.Name, "(\\B[A-Z])", " $1"), Value = prop.Value });
                        break;
                    case JTokenType.Property when prop1.Type == JTokenType.Float:
                        list.Add(new ListOfObjects() { Group = Group, ObjectType = ObjectType.ExTrackBar, PropertyName = prop.Name, Description = Regex.Replace(prop.Name, "(\\B[A-Z])", " $1") , Value = prop.Value });
                        break;
                    case JTokenType.Property when prop1.Type == JTokenType.String:
                        list.Add(new ListOfObjects() { Group = Group, ObjectType = ObjectType.TextBox, PropertyName = prop.Name, Description = Regex.Replace(prop.Name, "(\\B[A-Z])", " $1"), Value = prop.Value });

                        break;
                    case JTokenType.Property when prop1.Type == JTokenType.Array:
                        //GenerateObjectList(pp, prop.ToObject(), ref list, prop.Name);
                        break;
                    case JTokenType.Property when prop1.Type == JTokenType.Object:
                        GenerateObjectListParent(pp, prop.Value, ref list, Regex.Replace(prop.Name, "(\\B[A-Z])", " $1"));
                        break;
                    case JTokenType.Comment:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Integer:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Float:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.String:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Boolean:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Null:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Undefined:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Date:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Raw:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Bytes:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Guid:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Uri:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.TimeSpan:
                        throw new NotImplementedException();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                // Do something with propValue
            }
        }

        private void GenerateObjectListParent(IEnumerable<JProperty> pp, JToken value, ref List<ListOfObjects> list, string name) {

            foreach (JProperty prop in value) {

                var prop1 = prop.Value;

                switch (prop.Type) {
                    case JTokenType.None:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Object:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Array:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Constructor:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Property when prop1.Type == JTokenType.Integer:
                        list.Add(new ListOfObjects() { Group = name, ObjectType = ObjectType.ExTrackBar, PropertyName = prop.Name, Description = Regex.Replace(prop.Name, "(\\B[A-Z])", " $1"), Value = prop.Value });
                        break;
                    case JTokenType.Property when prop1.Type == JTokenType.Boolean:
                        list.Add(new ListOfObjects() { Group = name, ObjectType = ObjectType.CheckBox, PropertyName = prop.Name , Description = Regex.Replace(prop.Name, "(\\B[A-Z])", " $1") , Value = prop.Value });
                        break;
                    case JTokenType.Property when prop1.Type == JTokenType.Float:
                        list.Add(new ListOfObjects() { Group = name, ObjectType = ObjectType.ExTrackBar, PropertyName = prop.Name, Description = Regex.Replace(prop.Name, "(\\B[A-Z])", " $1") , Value = prop.Value });
                        break;
                    case JTokenType.Property when prop1.Type == JTokenType.String:
                        list.Add(new ListOfObjects() { Group = name, ObjectType = ObjectType.TextBox, PropertyName = prop.Name , Description = Regex.Replace(prop.Name, "(\\B[A-Z])", " $1") , Value = prop.Value });
                        break;
                    case JTokenType.Property when prop1.Type == JTokenType.Array:
                        //GenerateObjectList(pp, prop.ToObject(), ref list, prop.Name);
                        break;
                    case JTokenType.Property when prop1.Type == JTokenType.Object:
                        GenerateObjectListParent(pp, prop.Value, ref list, name + " - " + Regex.Replace(prop.Name, "(\\B[A-Z])", " $1"));
                        break;
                    case JTokenType.Comment:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Integer:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Float:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.String:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Boolean:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Null:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Undefined:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Date:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Raw:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Bytes:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Guid:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.Uri:
                        throw new NotImplementedException();
                        break;
                    case JTokenType.TimeSpan:
                        throw new NotImplementedException();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                // Do something with propValue
            }
        }
    }
}
