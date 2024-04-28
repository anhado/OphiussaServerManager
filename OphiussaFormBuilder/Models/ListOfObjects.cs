using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace OphiussaFormBuilder.Models {

    public enum ObjectType {
        TextBox,
        ExTrackBar,
        CheckBox,
        ComboBox
    }

    public class ListOfObjects {
        public string Group        { get; set; }
        public string PropertyName { get; set; }
        public string ObjectName {
            get {
                string name = "";
                switch (ObjectType) {
                    case ObjectType.TextBox:
                        name = "txt";
                        break;
                    case ObjectType.ExTrackBar:
                        name = "tb";
                        break;
                    case ObjectType.CheckBox:
                        name = "chk";
                        break;
                    case ObjectType.ComboBox:
                        name = "cbo";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                name += Group.Replace(" ", "") + PropertyName;

                return name;
            }
        }
        public string     Description { get; set; }
        public ObjectType ObjectType  { get; set; }
        public string ObjectTypeColumn {
            get {
                return ObjectType.ToString();
            }
            set {
                Enum.TryParse(value, out ObjectType myStatus);
                ObjectType = myStatus;
            }
        }
        public float  Minimum { get; set; }
        public float  Maximum { get; set; }
        public JToken Value   { get; set; }
    }
}
