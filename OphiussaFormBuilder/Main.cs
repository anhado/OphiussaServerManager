﻿using System;
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
          

        private void button2_Click(object sender, EventArgs e) {


            //List<string> content = System.IO.File.ReadAllLines(textBox1.Text).ToList();

            //string locationString = content.First(l => l.Contains("LBLSTART.Location ="));

            //int startindex = locationString.IndexOf(",");
            //int end = locationString.IndexOf(")");

            //int location = int.Parse(locationString.Substring(startindex + 1, end - startindex - 1));

            var p = JsonConvert.DeserializeObject(richTextBox1.Text);

            JObject j = JObject.FromObject(p);

            Type myType = j.GetType();

            var pp = j.Properties();

            List<ObjectDefinition> list = new List<ObjectDefinition>();

            //IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());

            GenerateObjectList(pp, ref list, "Main Settings");

            FrmAnalyzedData frm = new FrmAnalyzedData(list);
            frm.Show();
            frm.GenerateCode += objectsList => {
                                    FrmResult frmResult = new FrmResult(objectsList);
                                    frmResult.ShowDialog();
                                };
        }

        private void GenerateObjectList(IEnumerable<JProperty> pp, ref List<ObjectDefinition> list, string group) {

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
                        list.Add(new ObjectDefinition() { Group = Regex.Replace(group.Replace("_",""), "(\\B[A-Z])", " $1")  , ObjectType = ObjectType.ExTrackBar, PropertyName = prop.Name, Description = Regex.Replace(prop.Name, "(\\B[A-Z])", " $1"), Value = prop.Value,Minimum = (int.Parse(prop.Value.ToString()) == 0 ? 0 :1), Maximum = int.Parse( prop.Value.ToString()) * 10, BindingLocation = "Profile" });
                        break;
                    case JTokenType.Property when prop1.Type == JTokenType.Boolean:
                        list.Add(new ObjectDefinition() { Group = Regex.Replace(group.Replace("_", ""), "(\\B[A-Z])", " $1"), ObjectType = ObjectType.CheckBox, PropertyName = prop.Name, Description = Regex.Replace(prop.Name, "(\\B[A-Z])", " $1"), Value = prop.Value, BindingLocation = "Profile" });
                        break;
                    case JTokenType.Property when prop1.Type == JTokenType.Float:
                        list.Add(new ObjectDefinition() { Group = Regex.Replace(group.Replace("_", ""), "(\\B[A-Z])", " $1"), ObjectType = ObjectType.ExTrackBar, PropertyName = prop.Name, Description = Regex.Replace(prop.Name, "(\\B[A-Z])", " $1") , Value = prop.Value, Minimum = (double.Parse(prop.Value.ToString()) == 0 ? 0 : 1), Maximum = double.Parse(prop.Value.ToString()) * 10, BindingLocation = "Profile" });
                        break;
                    case JTokenType.Property when prop1.Type == JTokenType.String:
                        list.Add(new ObjectDefinition() { Group = Regex.Replace(group.Replace("_", ""), "(\\B[A-Z])", " $1"), ObjectType = ObjectType.TextBox, PropertyName = prop.Name, Description = Regex.Replace(prop.Name, "(\\B[A-Z])", " $1"), Value = prop.Value, BindingLocation = "Profile" });

                        break;
                    case JTokenType.Property when prop1.Type == JTokenType.Array:
                        //GenerateObjectList(pp, prop.ToObject(), ref list, prop.Name);
                        break;
                    case JTokenType.Property when prop1.Type == JTokenType.Object:
                        GenerateObjectListParent(prop.Value, ref list, prop.Name);
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

        private void GenerateObjectListParent(JToken value, ref List<ObjectDefinition> list, string group) {

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
                        list.Add(new ObjectDefinition() { Group = Regex.Replace(group.Replace("_", ""), "(\\B[A-Z])", " $1"), ObjectType = ObjectType.ExTrackBar, PropertyName = prop.Name, Description = Regex.Replace(prop.Name.Replace("_", ""), "(\\B[A-Z])", " $1"), Value = prop.Value, Minimum = (int.Parse(prop.Value.ToString()) == 0 ? 0 : 1), Maximum = int.Parse(prop.Value.ToString()) * 10, BindingLocation = $"Profile.{group.Replace("-", ".")}" });
                        break;
                    case JTokenType.Property when prop1.Type == JTokenType.Boolean:
                        list.Add(new ObjectDefinition() { Group = Regex.Replace(group.Replace("_", ""), "(\\B[A-Z])", " $1"), ObjectType = ObjectType.CheckBox, PropertyName = prop.Name, Description = Regex.Replace(prop.Name.Replace("_", ""), "(\\B[A-Z])", " $1") , Value = prop.Value, BindingLocation = $"Profile.{group.Replace("-", ".")}" });
                        break;
                    case JTokenType.Property when prop1.Type == JTokenType.Float:
                        list.Add(new ObjectDefinition() { Group = Regex.Replace(group.Replace("_", ""), "(\\B[A-Z])", " $1"), ObjectType = ObjectType.ExTrackBar, PropertyName = prop.Name, Description = Regex.Replace(prop.Name.Replace("_", ""), "(\\B[A-Z])", " $1") , Value = prop.Value, Minimum = (double.Parse(prop.Value.ToString()) == 0 ? 0 : 1), Maximum = double.Parse(prop.Value.ToString()) * 10, BindingLocation = $"Profile.{group.Replace("-", ".")}" });
                        break;
                    case JTokenType.Property when prop1.Type == JTokenType.String:
                        list.Add(new ObjectDefinition() { Group = Regex.Replace(group.Replace("_", ""), "(\\B[A-Z])", " $1"), ObjectType = ObjectType.TextBox, PropertyName = prop.Name, Description = Regex.Replace(prop.Name.Replace("_", ""), "(\\B[A-Z])", " $1") , Value = prop.Value, BindingLocation =  $"Profile.{group.Replace("-", ".")}" });
                        break;
                    case JTokenType.Property when prop1.Type == JTokenType.Array:
                        //GenerateObjectList(pp, prop.ToObject(), ref list, prop.Name);
                        break;
                    case JTokenType.Property when prop1.Type == JTokenType.Object:
                        GenerateObjectListParent(prop.Value, ref list, group + "-" + prop.Name);
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
