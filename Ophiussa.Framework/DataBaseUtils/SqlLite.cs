using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
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
             
            //using (var cmd = DbConnection().CreateCommand()) {
            //    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Settings(GUID Varchar(100) PRIMARY KEY, DataFolder Varchar(250), DefaultInstallFolder VarChar(250), SteamCMDFolder VarChar(250), SteamWepApiKey VarChar(250), CurseForgeApiKey VarChar(250), BackupFolder VarChar(250))";
            //    cmd.ExecuteNonQuery();
            //}

            //using (var cmd = DbConnection().CreateCommand()) {
            //    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Plugins(PluginName Varchar(100) PRIMARY KEY, GameType Varchar(250), GameName VarChar(250), Version VarChar(250), Loaded int)";
            //    cmd.ExecuteNonQuery();
            //}

            //using (var cmd = DbConnection().CreateCommand()) {
            //    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Profiles(Key Varchar(100) PRIMARY KEY, Name Varchar(250), Type Varchar(250), InstallationFolder VarChar(250), SteamServerId int, SteamApplicationID int, CurseForgeId int, StartOnBoot int, IncludeAutoBackup int, IncludeAutoUpdate int, RestartIfShutdown int, PluginVersion Varchar(250), ServerPort int, PeerPort int, QueryPort int, UseRCON int, RCONPort int, RCONPassword Varchar(250), ServerVersion Varchar(250), ServerBuildVersion Varchar(250), ExecutablePath Varchar(250), AdditionalSettings TEXT)";
            //    cmd.ExecuteNonQuery();
            //}

            //using (var cmd = DbConnection().CreateCommand()) {
            //    cmd.CommandText =
            //        "CREATE TABLE IF NOT EXISTS AutoManagement(ServerKey Varchar(100), ShutdownServer int, ShutdownHour nvarchar(4), ShutdownSun int, ShutdownMon int, ShutdownTue int, ShutdownWed int, ShutdownThu int, ShutdownFri int, ShutdownSat int, ShutdownSunday int, UpdateServer int, RestartServer int)";
            //    cmd.ExecuteNonQuery();
            //}

            //if (!ColumnExists("Settings", "EnableLogs"))
            //    using (var cmd = DbConnection().CreateCommand()) {
            //        cmd.CommandText = "ALTER TABLE Settings ADD EnableLogs int;";
            //        cmd.ExecuteNonQuery();
            //    }

            //if (!ColumnExists("Settings", "MaxLogFiles"))
            //    using (var cmd = DbConnection().CreateCommand()) {
            //        cmd.CommandText = "ALTER TABLE Settings ADD MaxLogFiles int;";
            //        cmd.ExecuteNonQuery();
            //    }

            //if (!ColumnExists("Settings", "MaxLogsDays"))
            //    using (var cmd = DbConnection().CreateCommand()) {
            //        cmd.CommandText = "ALTER TABLE Settings ADD MaxLogsDays int;";
            //        cmd.ExecuteNonQuery();
            //    }

            //if (!ColumnExists("Profiles", "AdditionalCommands"))
            //    using (var cmd = DbConnection().CreateCommand()) {
            //        cmd.CommandText = "ALTER TABLE Profiles ADD AdditionalCommands TEXT;";
            //        cmd.ExecuteNonQuery();
            //    }


            //if (!ColumnExists("Plugins", "ModProvider"))
            //    using (var cmd = DbConnection().CreateCommand()) {
            //        cmd.CommandText = "ALTER TABLE Plugins ADD ModProvider int;";
            //        cmd.ExecuteNonQuery();
            //    }

        }

        public SQLiteConnection SqliteConnection { get; private set; }

        public bool TableExists(string tableName) {
            try {
                SQLiteDataAdapter da = null;
                var dt = new DataTable();


                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = $"SELECT name FROM sqlite_master WHERE type='table' AND name='{tableName}';";
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
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
                var dt = new DataTable();


                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = $"pragma table_info({tableName})";
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
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
                var dt = new DataTable();


                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = $"pragma table_info({tableName})";
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                }

                if (dt.Rows.Count == 0) return "";

                foreach (DataRow dr in dt.Rows)
                    if (dr.GetLong("pk") == 1)
                        return dr.GetString("name");

                return "";
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                return "";
            }
        } 

        private SQLiteConnection DbConnection() {
            SqliteConnection = new SQLiteConnection("Data Source=database.sqlite; Version=3;");
            SqliteConnection.Open();
            return SqliteConnection;
        }

        public List<T> GetRecords<T>(string condition = "") {
            try {
                var temp = typeof(T);
                SQLiteDataAdapter da = null;
                var dt = new DataTable();
                string tableName = temp.Name;

                List<object> classAttr = temp.GetCustomAttributes(true).ToList();
                if (classAttr.Count > 0) {
                    bool foundAttribute = false;
                    classAttr.ForEach(attr => {
                        if (attr is TableAttributes atr) {
                            tableName = atr.TableName;
                            foundAttribute = true;
                        }
                    });
                    if (!foundAttribute) tableName = temp.Name;
                }

                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = $"SELECT * FROM {tableName}" + (string.IsNullOrEmpty(condition) ? "" : $"WHERE {condition}");
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                }

                if (dt.Rows.Count == 0) throw new Exception("No Record");
                return dt.ConvertDataTable<T>();
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                return null;
            }
        }

        public BindingList<T> GetRecordsB<T>(string condition = "") {
            try {
                var temp = typeof(T);
                SQLiteDataAdapter da = null;
                var dt = new DataTable();
                string tableName = temp.Name;

                List<object> classAttr = temp.GetCustomAttributes(true).ToList();
                if (classAttr.Count > 0) {
                    bool foundAttribute = false;
                    classAttr.ForEach(attr => {
                        if (attr is TableAttributes atr) {
                            tableName = atr.TableName;
                            foundAttribute = true;
                        }
                    });
                    if (!foundAttribute) tableName = temp.Name;
                }

                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = $"SELECT * FROM {tableName}" + (string.IsNullOrEmpty(condition) ? "" : $"WHERE {condition}");
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                }

                if (dt.Rows.Count == 0) throw new Exception("No Record");
                return dt.ConvertDataTableB<T>();
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                return null;
            }
        }

        public T GetRecord<T>(string condition = "") {
            try {
                var temp = typeof(T);
                SQLiteDataAdapter da = null;
                var dt = new DataTable();
                string tableName = temp.Name;

                List<object> classAttr = temp.GetCustomAttributes(true).ToList();
                if (classAttr.Count > 0) {
                    bool foundAttribute = false;
                    classAttr.ForEach(attr => {
                        if (attr is TableAttributes atr) {
                            tableName = atr.TableName;
                            foundAttribute = true;
                        }
                    });
                    if (!foundAttribute) tableName = temp.Name;
                }

                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = $"SELECT * FROM {tableName}" + (string.IsNullOrEmpty(condition) ? "" : $"WHERE {condition}");
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                }

                if (dt.Rows.Count == 0) throw new Exception("No Record");
                return dt.Rows[0].GetItem<T>();
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                return default;
            }
        }

        public bool CreateTable<T>() {
            try {
                var temp = typeof(T);

                string tableName = temp.Name;
                bool tableExists = false;
                List<object> classAttr = temp.GetCustomAttributes(true).ToList();

                if (classAttr.Count > 0) {
                    bool foundAttribute = false;
                    classAttr.ForEach(attr => {
                        if (attr is TableAttributes atr) {
                            tableName = atr.TableName;
                            foundAttribute = true;
                        }
                    });
                    if (!foundAttribute) tableName = temp.Name;
                }

                tableExists = TableExists(tableName);

                List<string> fieldList = new List<string>();

                foreach (var pro in temp.GetProperties()) {
                    bool foundAttribute = false;
                    List<object> propAttr = pro.GetCustomAttributes(true).ToList();
                    propAttr.ForEach(attr => {
                        if (attr is FieldAttributes atr) {
                            if ((!ColumnExists(tableName, pro.Name) && tableExists) || !tableExists) {
                                if (!atr.Ignore) fieldList.Add($"{pro.Name} {GetDataType(pro, atr.DataType)}");
                                foundAttribute = true;
                            }
                        }
                    });
                    if (!foundAttribute && ((!ColumnExists(tableName, pro.Name) && tableExists) || !tableExists)) {
                        fieldList.Add($"{pro.Name} {GetDataType(pro)}");
                    }
                }

                if (fieldList.Count == 0) return true;

                if (tableExists) {
                    using (var cmd = DbConnection().CreateCommand()) {
                        cmd.CommandText = $"ALTER TABLE {tableName} ADD {string.Join(",", fieldList.ToArray())};";
                        cmd.ExecuteNonQuery();
                    }
                }
                else {
                    using (var cmd = DbConnection().CreateCommand()) {
                        cmd.CommandText = $"CREATE TABLE IF NOT EXISTS {tableName}({string.Join(",", fieldList.ToArray())})";
                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception e) {
                OphiussaLogger.Logger.Error(e);
                return false;
            }
        }

        private string GetDataType(PropertyInfo pro, string userConfig = "") {
            if (!string.IsNullOrEmpty(userConfig)) return userConfig;
            if (pro.PropertyType == typeof(double)) {
                return "FLOAT";
            }
            else if (pro.PropertyType == typeof(bool) ||
                     pro.PropertyType == typeof(Enum) ||
                     pro.PropertyType == typeof(int) ||
                     pro.PropertyType == typeof(Int32) ||
                     pro.PropertyType == typeof(Int64)) {
                return "int";
            }
            else {
                return "VarChar(250)";
            }

        }
        private object GetValue(PropertyInfo pro, object obj) {

            var fields = obj.GetType().GetProperties();

            var pInfo = fields.FirstOrDefault(f => f.Name == pro.Name);
            if (pInfo == null) return null;

            if (pro.PropertyType == typeof(double)) {

                string v = pInfo.GetValue(obj).ToString();

                if (float.TryParse(v, NumberStyles.Any, CultureInfo.InvariantCulture, out float res)) return res;
                return 0;
            }
            else if (pro.PropertyType == typeof(Enum) ||
                     pro.PropertyType == typeof(int) ||
                     pro.PropertyType == typeof(Int32) ||
                     pro.PropertyType == typeof(Int64)) {

                string v = pInfo.GetValue(obj).ToString();

                if (int.TryParse(v, NumberStyles.Any, CultureInfo.InvariantCulture, out int res)) return res;
                return 0;
            } else if (pro.PropertyType == typeof(bool)) {
                string v                                = pInfo.GetValue(obj).ToString();
                if (bool.TryParse(v, out bool res)) return res ? 1:0;
                return 0;
            }
            else {
                return pInfo.GetValue(obj).ToString();
            }

        }

        public bool Upsert<T>(object obj) {
            try {
                var temp = typeof(T); 

                string tableName = temp.Name;
                List<object> classAttr = temp.GetCustomAttributes(true).ToList();

                if (classAttr.Count > 0) {
                    bool foundAttribute = false;
                    classAttr.ForEach(attr => {
                        if (attr is TableAttributes atr) {
                            tableName = atr.TableName;
                            foundAttribute = true;
                        }
                    });
                    if (!foundAttribute) tableName = temp.Name;
                }

                List<string> columnList = new List<string>();
                List<string> valueColumnList = new List<string>();
                List<string> updateColumnList = new List<string>();

                string primaryKey = GetPrimaryKey(tableName);
                if (string.IsNullOrEmpty(primaryKey)) throw new Exception("No PrimaryKey");

                using (var cmd = DbConnection().CreateCommand()) {
                    foreach (var pro in temp.GetProperties()) {

                        bool ignore = false;
                        List<object> propAttr = pro.GetCustomAttributes(true).ToList();
                        propAttr.ForEach(attr => {
                            if (attr is FieldAttributes atr) ignore = atr.Ignore;
                        });
                        if (ignore) continue;
                        columnList.Add(pro.Name);
                        valueColumnList.Add("@" + pro.Name);
                        if (pro.Name != primaryKey) updateColumnList.Add($"{pro.Name}=excluded.{pro.Name}");
                        cmd.Parameters.AddWithValue("@" + pro.Name, GetValue(pro, obj));
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

                var temp   = typeof(T); 

                string       tableName = temp.Name;
                List<object> classAttr = temp.GetCustomAttributes(true).ToList();

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