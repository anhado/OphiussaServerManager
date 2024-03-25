using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;

namespace OphiussaFramework.Extensions {
    public static class DataTableExtensions {

        public static object GetItem(this DataRow dr, Type type) {
            object obj = new RawProfile();                     ;
         //   if (type.Name == "IProfile") obj = new RawPlugin();
            if (type.Name == "IPlugin") obj = new RawPlugin();
            var temp                        = type;

            foreach (DataColumn column in dr.Table.Columns)
            foreach (var pro in temp.GetProperties())
                if (pro.PropertyType == typeof(bool)) {
                    if (pro.Name == column.ColumnName && dr[column.ColumnName] != DBNull.Value)
                        pro.SetValue(obj, dr[column.ColumnName].ToString() == "1", null);
                    else
                        continue;
                }
                else if (pro.PropertyType == typeof(int)) {
                    if (pro.Name == column.ColumnName && dr[column.ColumnName] != DBNull.Value)
                        pro.SetValue(obj, int.Parse(dr[column.ColumnName].ToString()), null);
                    else
                        continue;
                }
                else {
                    if (pro.Name == column.ColumnName && dr[column.ColumnName] != DBNull.Value)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }

            return obj;
        }

        public static List<T> ConvertDataTable<T>(this DataTable dt) {
            var data = new List<T>();
            foreach (DataRow row in dt.Rows) {
                var item = row.GetItem<T>();
                data.Add(item);
            }

            return data;
        }

        public static BindingList<T> ConvertDataTableB<T>(this DataTable dt) {
            var data = new BindingList<T>();
            foreach (DataRow row in dt.Rows) {
                var item = row.GetItem<T>();
                data.Add(item);
            }

            return data;
        }

        public static T GetItem<T>(this DataRow dr) {
            var temp = typeof(T);

            if (temp.IsInterface) return (T)GetItem(dr, temp);

            var obj = Activator.CreateInstance<T>();

             
            
            foreach (DataColumn column in dr.Table.Columns)
            foreach (var pro in temp.GetProperties())
                if (pro.PropertyType == typeof(bool)) {
                    if (pro.Name == column.ColumnName && dr[column.ColumnName] != DBNull.Value)
                        pro.SetValue(obj, dr[column.ColumnName].ToString() == "1", null);
                    else
                        continue;
                }
                else if (pro.PropertyType == typeof(int)) {
                    if (pro.Name == column.ColumnName && dr[column.ColumnName] != DBNull.Value)
                        pro.SetValue(obj, int.Parse(dr[column.ColumnName].ToString()), null);
                    else
                        continue;
                }
                else {
                    if (pro.Name == column.ColumnName && dr[column.ColumnName] != DBNull.Value)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }

            return obj;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> data) {
            var properties = TypeDescriptor.GetProperties(typeof(T));
            var table      = new DataTable();
            foreach (PropertyDescriptor prop in properties) table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (var item in data) {
                var row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }

            return table;
        }
    }
}