namespace OphiussaFramework.Models {
    [TableAttributes(TableName = "Branches")]
    internal class Branches {
        [FieldAttributes(PrimaryKey = true, AutoIncrement = true)]
        public int Id { get; set; }

        [FieldAttributes(DataType = "Varchar(100)")]
        public string Code { get; set; }

        [FieldAttributes(DataType = "Varchar(250)")]
        public string Name { get; set; }
    }
}