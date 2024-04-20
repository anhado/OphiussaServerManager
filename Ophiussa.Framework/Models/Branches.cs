using System;
using System.CodeDom;
using System.Collections.Generic;
using OphiussaFramework.Interfaces;

namespace OphiussaFramework.Models {
    [TableAttributes(TableName = "Branches")]
    public class Branches {
        [FieldAttributes(PrimaryKey = true, AutoIncrement = true)]
        public int Id { get; set; }

        [FieldAttributes(DataType = "Varchar(100)")]
        [FieldDependesOn(typeof(IProfile), "Branch")] 
        public string Code { get; set; }

        [FieldAttributes(DataType = "Varchar(250)")]
        public string Name { get; set; }

        [FieldAttributes(Ignore = true)]
        public string OldCode { get; set; }
    }
}