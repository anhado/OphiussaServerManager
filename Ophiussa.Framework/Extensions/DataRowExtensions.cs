using System.Data;

namespace OphiussaFramework.Extensions {
    public static class DataRowExtensions {
        public static string GetString(this DataRow dr, string FieldNAme) {
            return dr.Field<string>(FieldNAme);
        }

        public static int GetInt(this DataRow dr, string FieldNAme) {
            return dr.Field<int>(FieldNAme);
        }
    }
}