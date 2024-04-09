using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using OphiussaFramework.CommonUtils;
using OphiussaFramework.Extensions;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;
using Exception = System.Exception;

namespace OphiussaFramework.DataBaseUtils {
    public class SqlLite {
        public SqlLite() {
            if (!File.Exists("database.sqlite")) SQLiteConnection.CreateFile(@"database.sqlite");

            CreateTable<Settings>();
            CreateTable<IPlugin>();
            CreateTable<IProfile>();
            CreateTable<AutoManagement>();

            CreateTable<Branches>();
        }

        public SQLiteConnection SqliteConnection { get; private set; }

        public bool TableExists(string tableName) {
            try {
                SQLiteDataAdapter da = null;
                var               dt = new DataTable();


                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}';";
                    da              = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                }

                if (dt.Rows.Count == 0) return false;
                return true;
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                return false;
            }
        }

        public bool ColumnExists(string tableName, string columnName) {
            try {
                SQLiteDataAdapter da = null;
                var               dt = new DataTable();


                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = $"pragma table_info({tableName})";
                    da              = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                }

                if (dt.Rows.Count == 0) return false;

                foreach (DataRow dr in dt.Rows)
                    if (dr.GetString("name") == columnName)
                        return true;

                return false;
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                return false;
            }
        }

        public string GetPrimaryKey(string tableName) {
            try {
                SQLiteDataAdapter da = null;
                var               dt = new DataTable();


                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = $"pragma table_info({tableName})";
                    da              = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                }

                if (dt.Rows.Count == 0) return "";

                foreach (DataRow dr in dt.Rows)
                    if (dr.GetInt64("pk") == 1)
                        return dr.GetString("name");

                return "";
            }
            catch (Exception e) {
                OphiussaLogger.Logger?.Error(e);
                Console.WriteLine(e.Message);
                return "";
            }
        }

        private SQLiteConnection DbConnection() {
            string fullPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            SqliteConnection = new SQLiteConnection($"Data Source={fullPath}\\database.sqlite; Version=3;");
            SqliteConnection.Open();
            return SqliteConnection;
        }

        public List<T> GetRecords<T>(string condition = "") {
            try {
                var               temp      = typeof(T);
                SQLiteDataAdapter da        = null;
                var               dt        = new DataTable();
                string            tableName = temp.Name;

                var classAttr = temp.GetCustomAttributes(true).ToList();
                if (classAttr.Count > 0) {
                    bool foundAttribute = false;
                    classAttr.ForEach(attr => {
                                          if (attr is TableAttributes atr) {
                                              tableName      = atr.TableName;
                                              foundAttribute = true;
                                          }
                                      });
                    if (!foundAttribute) tableName = temp.Name;
                }

                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = $"SELECT * FROM {tableName} " + (string.IsNullOrEmpty(condition) ? "" : $" WHERE {condition}");
                    da              = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                }

                if (dt.Rows.Count == 0) throw new Exception("No Record");
                return dt.ConvertDataTable<T>();
            }
            catch (Exception e) {
                OphiussaLogger.Logger?.Error(e);
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public BindingList<T> GetRecordsB<T>(string condition = "") {
            try {
                var               temp      = typeof(T);
                SQLiteDataAdapter da        = null;
                var               dt        = new DataTable();
                string            tableName = temp.Name;

                var classAttr = temp.GetCustomAttributes(true).ToList();
                if (classAttr.Count > 0) {
                    bool foundAttribute = false;
                    classAttr.ForEach(attr => {
                                          if (attr is TableAttributes atr) {
                                              tableName      = atr.TableName;
                                              foundAttribute = true;
                                          }
                                      });
                    if (!foundAttribute) tableName = temp.Name;
                }

                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = $"SELECT * FROM {tableName} " + (string.IsNullOrEmpty(condition) ? "" : $" WHERE {condition}");
                    da              = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                }

                //    if (dt.Rows.Count == 0) throw new Exception("No Record");
                return dt.ConvertDataTableB<T>();
            }
            catch (Exception e) {
                OphiussaLogger.Logger?.Error(e);
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public T GetRecord<T>(string condition = "") {
            string tmpFile = Path.GetTempFileName();
            if (!File.Exists(tmpFile)) File.Create(tmpFile);
            try {
                var               temp      = typeof(T);
                SQLiteDataAdapter da        = null;
                var               dt        = new DataTable();
                string            tableName = temp.Name;

                var classAttr = temp.GetCustomAttributes(true).ToList();
                if (classAttr.Count > 0) {
                    bool foundAttribute = false;
                    classAttr.ForEach(attr => {
                                          if (attr is TableAttributes atr) {
                                              tableName      = atr.TableName;
                                              foundAttribute = true;
                                          }
                                      });
                    if (!foundAttribute) tableName = temp.Name;
                }

                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = $"SELECT * FROM {tableName}" + (string.IsNullOrEmpty(condition) ? "" : $" WHERE {condition}");
                    File.AppendAllText(tmpFile, "\nQuery:" + cmd.CommandText);
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                }

                if (dt.Rows.Count == 0) throw new Exception("No Record");
                return dt.Rows[0].GetItem<T>();
            }
            catch (Exception e) {
                File.AppendAllText(tmpFile, "\nerror:" + e.Message);
                OphiussaLogger.Logger?.Error(e);
                Console.WriteLine(e.Message);
                return default;
            }
        }

        public bool CreateTable<T>() {
            try {
                var temp = typeof(T);

                string tableName   = temp.Name;
                bool   tableExists = false;
                var    classAttr   = temp.GetCustomAttributes(true).ToList();

                if (classAttr.Count > 0) {
                    bool foundAttribute = false;
                    classAttr.ForEach(attr => {
                                          if (attr is TableAttributes atr) {
                                              tableName      = atr.TableName;
                                              foundAttribute = true;
                                          }
                                      });
                    if (!foundAttribute) tableName = temp.Name;
                }

                tableExists = TableExists(tableName);

                var fieldList = new List<string>();

                foreach (var pro in temp.GetProperties()) {
                    bool foundAttribute = false;
                    var  propAttr       = pro.GetCustomAttributes(true).ToList();
                    propAttr.ForEach(attr => {
                                         if (attr is FieldAttributes atr)
                                             if ((!ColumnExists(tableName, pro.Name) && tableExists) || !tableExists) {
                                                 if (!atr.Ignore) fieldList.Add($"{pro.Name} {GetDataType(pro, atr.DataType)} {(atr.PrimaryKey ? "PRIMARY KEY" : "")} {(atr.AutoIncrement ? "AUTOINCREMENT" : "")}");
                                                 foundAttribute = true;
                                             }
                                     });
                    if (!foundAttribute && ((!ColumnExists(tableName, pro.Name) && tableExists) || !tableExists)) fieldList.Add($"{pro.Name} {GetDataType(pro)}");
                }

                if (fieldList.Count == 0) return true;

                if (tableExists)
                    foreach (string fld in fieldList)
                        using (var cmd = DbConnection().CreateCommand()) {
                            cmd.CommandText = $"ALTER TABLE {tableName} ADD {fld};";
                            cmd.ExecuteNonQuery();
                        }
                else
                    using (var cmd = DbConnection().CreateCommand()) {
                        cmd.CommandText = $"CREATE TABLE IF NOT EXISTS {tableName}({string.Join(",", fieldList.ToArray())})";
                        cmd.ExecuteNonQuery();
                    }

                return true;
            }
            catch (Exception e) {
                OphiussaLogger.Logger?.Error(e);
                return false;
            }
        }

        private string GetDataType(PropertyInfo pro, string userConfig = "") {
            if (!string.IsNullOrEmpty(userConfig)) return userConfig;
            if (pro.PropertyType.IsEnum)
                return "integer";
            if (pro.PropertyType == typeof(double))
                return "FLOAT";
            if (pro.PropertyType == typeof(bool) ||
                pro.PropertyType == typeof(int)  ||
                pro.PropertyType == typeof(int)  ||
                pro.PropertyType == typeof(long))
                return "integer";
            return "VarChar(250)";
        }

        private object GetValue(PropertyInfo pro, object obj) {
            var fields = obj.GetType().GetProperties();

            var pInfo = fields.FirstOrDefault(f => f.Name == pro.Name);
            if (pInfo == null) return null;

            if (pro.PropertyType.IsEnum) {
                int v = (int)pInfo.GetValue(obj);
                return v;
            }

            if (pro.PropertyType.IsClass && pro.PropertyType != typeof(string)) {
                string v = JsonConvert.SerializeObject(pInfo.GetValue(obj), Formatting.Indented);

                return v;
            }

            if (pro.PropertyType == typeof(double)) {
                string v = pInfo.GetValue(obj).ToString();

                if (float.TryParse(v, NumberStyles.Any, CultureInfo.InvariantCulture, out float res)) return res;
                return 0;
            }

            if (pro.PropertyType == typeof(int) ||
                pro.PropertyType == typeof(int) ||
                pro.PropertyType == typeof(long)) {
                string v = pInfo.GetValue(obj).ToString();

                if (int.TryParse(v, NumberStyles.Any, CultureInfo.InvariantCulture, out int res)) return res;
                return 0;
            }

            if (pro.PropertyType == typeof(bool)) {
                string v = pInfo.GetValue(obj).ToString();
                if (bool.TryParse(v, out bool res)) return res ? 1 : 0;
                return 0;
            }

            return pInfo.GetValue(obj)?.ToString();
        }

        public bool Upsert<T>(object obj) {
            try {
                var temp = typeof(T);

                string tableName = temp.Name;
                var    classAttr = temp.GetCustomAttributes(true).ToList();

                if (classAttr.Count > 0) {
                    bool foundAttribute = false;
                    classAttr.ForEach(attr => {
                                          if (attr is TableAttributes atr) {
                                              tableName      = atr.TableName;
                                              foundAttribute = true;
                                          }
                                      });
                    if (!foundAttribute) tableName = temp.Name;
                }

                var columnList       = new List<string>();
                var valueColumnList  = new List<string>();
                var updateColumnList = new List<string>();

                string primaryKey = GetPrimaryKey(tableName);
                if (string.IsNullOrEmpty(primaryKey)) throw new Exception("No PrimaryKey");

                using (var cmd = DbConnection().CreateCommand()) {
                    foreach (var pro in temp.GetProperties()) {
                        bool ignore       = false;
                        bool autoIncrment = false;
                        var  propAttr     = pro.GetCustomAttributes(true).ToList();
                        propAttr.ForEach(attr => {
                                             if (!(attr is FieldAttributes atr)) return;
                                             ignore       = atr.Ignore;
                                             autoIncrment = atr.AutoIncrement;
                                         });
                        if (ignore) continue;
                        columnList.Add(pro.Name);
                        valueColumnList.Add("@" + pro.Name);
                        if (pro.Name != primaryKey) updateColumnList.Add($"{pro.Name}=excluded.{pro.Name}");

                        object value = GetValue(pro, obj);

                        if (autoIncrment && value.ToString() == "0") cmd.Parameters.AddWithValue("@" + pro.Name, null);
                        else cmd.Parameters.AddWithValue("@"                                         + pro.Name, value);
                    }

                    cmd.CommandText = $@"INSERT INTO {tableName}({string.Join(",", columnList.ToArray())}) 
                                                     values({string.Join(",", valueColumnList.ToArray())})
                                                     ON CONFLICT({primaryKey}) DO UPDATE SET
                                                            {string.Join(",\n", updateColumnList.ToArray())};";
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                return false;
            }
        }

        public bool Delete<T>(string keyValue) {
            try {
                var temp = typeof(T);

                string tableName = temp.Name;
                var    classAttr = temp.GetCustomAttributes(true).ToList();

                if (classAttr.Count > 0) {
                    bool foundAttribute = false;
                    classAttr.ForEach(attr => {
                                          if (attr is TableAttributes atr) {
                                              tableName      = atr.TableName;
                                              foundAttribute = true;
                                          }
                                      });
                    if (!foundAttribute) tableName = temp.Name;
                }

                string primaryKey = GetPrimaryKey(tableName);

                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = $@"DELETE FROM {tableName} WHERE {primaryKey} = @{primaryKey};";
                    cmd.Parameters.AddWithValue($"@{primaryKey}", keyValue);
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                return false;
            }
        }
    }
}