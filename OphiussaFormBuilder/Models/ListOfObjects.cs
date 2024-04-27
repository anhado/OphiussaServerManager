using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaFormBuilder.Models {

    internal enum ObjectType {
        TextBox,
        exTrackBar,
        CheckBox,
        ComboBox
    }

    internal class ListOfObjects {
        public string     Group { get; set; }
        public string     PropertyName { get; set; }
        public ObjectType ObjectType   { get; set; }
    }
}
