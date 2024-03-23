using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaFramework.Extensions
{
    public static class DataTableExtensions
    { 
        public static List<T> ConvertDataTable<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = row.GetItem<T>();
                data.Add(item);
            }
            return data;
        }

        public static BindingList<T> ConvertDataTableB<T>(this DataTable dt)
        {
            BindingList<T> data = new BindingList<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = row.GetItem<T>();
                data.Add(item);
            }
            return data;
        }

        public static T GetItem<T>(this DataRow dr)
        {
            Type temp = typeof(T);
            T    obj  = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.PropertyType == typeof(bool))
                    {
                        if (pro.Name == column.ColumnName && dr[column.ColumnName] != DBNull.Value)
                            pro.SetValue(obj, dr[column.ColumnName].ToString() == "1", null);
                        else
                            continue;
                    }
                    else if(pro.PropertyType == typeof(Int32))
                    {
                        if (pro.Name == column.ColumnName && dr[column.ColumnName] != DBNull.Value)
                            pro.SetValue(obj, Int32.Parse(dr[column.ColumnName].ToString()), null);
                        else
                            continue;
                    }
                    else
                    {
                        if (pro.Name == column.ColumnName && dr[column.ColumnName] != DBNull.Value)
                            pro.SetValue(obj, dr[column.ColumnName], null);
                        else
                            continue;
                    }
                }
            }
            return obj;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable                    table      = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}
