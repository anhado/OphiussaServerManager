using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaFramework.Extensions
{
    public static class DataRowExtensions
    {
        public static string GetString(this DataRow dr, string FieldNAme) => dr.Field<string>(FieldNAme);
        public static int    GetInt(this    DataRow dr, string FieldNAme) => dr.Field<int>(FieldNAme);
    }
} 
