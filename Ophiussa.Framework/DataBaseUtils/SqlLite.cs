using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.IO;
using OphiussaFramework.Extensions;
using OphiussaFramework.Interfaces;
using OphiussaFramework.Models;
using Exception = System.Exception;

namespace OphiussaFramework.DataBaseUtils {
    public class SqlLite {
        public SqlLite() {
            if (!File.Exists("database.sqlite")) SQLiteConnection.CreateFile(@"database.sqlite");

            using (var cmd = DbConnection().CreateCommand()) {
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS Settings(GUID Varchar(100) PRIMARY KEY, DataFolder Varchar(250), DefaultInstallFolder VarChar(250), SteamCMDFolder VarChar(250), SteamWepApiKey VarChar(250), CurseForgeApiKey VarChar(250), BackupFolder VarChar(250))";
                cmd.ExecuteNonQuery();
            }

            using (var cmd = DbConnection().CreateCommand()) {
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS Plugins(PluginName Varchar(100) PRIMARY KEY, GameType Varchar(250), GameName VarChar(250), Version VarChar(250), Loaded int)";
                cmd.ExecuteNonQuery();
            }

            using (var cmd = DbConnection().CreateCommand()) {
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS Profiles(Key Varchar(100) PRIMARY KEY, Name Varchar(250), Type Varchar(250), InstallationFolder VarChar(250), SteamServerId int, SteamApplicationID int, CurseForgeId int, StartOnBoot int, IncludeAutoBackup int, IncludeAutoUpdate int, RestartIfShutdown int, PluginVersion Varchar(250), ServerPort int, PeerPort int, QueryPort int, UseRCON int, RCONPort int, RCONPassword Varchar(250), ServerVersion Varchar(250), ServerBuildVersion Varchar(250), ExecutablePath Varchar(250), AdditionalSettings TEXT)";
                cmd.ExecuteNonQuery();
            }

            using (var cmd = DbConnection().CreateCommand()) {
                cmd.CommandText =
                    "CREATE TABLE IF NOT EXISTS AutoManagement(ServerKey Varchar(100), ShutdownServer int, ShutdownHour nvarchar(4), ShutdownSun int, ShutdownMon int, ShutdownTue int, ShutdownWed int, ShutdownThu int, ShutdownFri int, ShutdownSat int, ShutdownSunday int, UpdateServer int, RestartServer int)";
                cmd.ExecuteNonQuery();
            }

            if (!ColumnExists("Settings", "EnableLogs"))
                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = "ALTER TABLE Settings ADD EnableLogs int;";
                    cmd.ExecuteNonQuery();
                }

            if (!ColumnExists("Settings", "MaxLogFiles"))
                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = "ALTER TABLE Settings ADD MaxLogFiles int;";
                    cmd.ExecuteNonQuery();
                }

            if (!ColumnExists("Settings", "MaxLogsDays"))
                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = "ALTER TABLE Settings ADD MaxLogsDays int;";
                    cmd.ExecuteNonQuery();
                }
        }

        public SQLiteConnection sqliteConnection { get; private set; }

        private bool ColumnExists(string tableName, string columnName) {
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
                return false;
            }
        }


        public bool Upsert(IProfile profile) {
            try {
                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = @"INSERT INTO Profiles( Key,  Name,  Type,  InstallationFolder, SteamServerId, SteamApplicationID, CurseForgeId, StartOnBoot,  IncludeAutoBackup, IncludeAutoUpdate, RestartIfShutdown, PluginVersion, ServerPort, PeerPort, QueryPort, UseRCON, RCONPort, RCONPassword, ServerVersion, ServerBuildVersion, ExecutablePath, AdditionalSettings) 
                                                     values(@Key, @Name, @Type, @InstallationFolder,@SteamServerId,@SteamApplicationID,@CurseForgeId,@StartOnBoot, @IncludeAutoBackup,@IncludeAutoUpdate,@RestartIfShutdown,@PluginVersion,@ServerPort,@PeerPort,@QueryPort,@UseRCON,@RCONPort,@RCONPassword,@ServerVersion,@ServerBuildVersion,@ExecutablePath,@AdditionalSettings)
                                                     ON CONFLICT(Key) DO UPDATE SET
                                                            Name=excluded.Name,
                                                            Type=excluded.Type,
                                                            InstallationFolder=excluded.InstallationFolder,
                                                            SteamServerId=excluded.SteamServerId,
                                                            SteamApplicationID=excluded.SteamApplicationID,
                                                            CurseForgeId=excluded.CurseForgeId,
                                                            StartOnBoot=excluded.StartOnBoot,
                                                            IncludeAutoBackup=excluded.IncludeAutoBackup,
                                                            IncludeAutoUpdate=excluded.IncludeAutoUpdate,
                                                            RestartIfShutdown=excluded.RestartIfShutdown,
                                                            PluginVersion=excluded.PluginVersion,
                                                            ServerPort=excluded.ServerPort,
                                                            PeerPort=excluded.PeerPort,
                                                            QueryPort=excluded.QueryPort,
                                                            UseRCON=excluded.UseRCON,
                                                            RCONPort=excluded.RCONPort,
                                                            RCONPassword=excluded.RCONPassword,
                                                            ServerVersion=excluded.ServerVersion,
                                                            ServerBuildVersion=excluded.ServerBuildVersion,
                                                            ExecutablePath=excluded.ExecutablePath,
                                                            AdditionalSettings=excluded.AdditionalSettings;";
                    cmd.Parameters.AddWithValue("@Key",                profile.Key);
                    cmd.Parameters.AddWithValue("@Name",               profile.Name);
                    cmd.Parameters.AddWithValue("@Type",               profile.Type);
                    cmd.Parameters.AddWithValue("@InstallationFolder", profile.InstallationFolder);
                    cmd.Parameters.AddWithValue("@SteamServerId",      profile.SteamServerId);
                    cmd.Parameters.AddWithValue("@SteamApplicationID", profile.SteamApplicationID);
                    cmd.Parameters.AddWithValue("@CurseForgeId",       profile.CurseForgeId);
                    cmd.Parameters.AddWithValue("@StartOnBoot",        profile.StartOnBoot);
                    cmd.Parameters.AddWithValue("@IncludeAutoBackup",  profile.IncludeAutoBackup);
                    cmd.Parameters.AddWithValue("@IncludeAutoUpdate",  profile.IncludeAutoUpdate);
                    cmd.Parameters.AddWithValue("@RestartIfShutdown",  profile.RestartIfShutdown);
                    cmd.Parameters.AddWithValue("@PluginVersion",      profile.PluginVersion);
                    cmd.Parameters.AddWithValue("@ServerPort",         profile.ServerPort);
                    cmd.Parameters.AddWithValue("@PeerPort",           profile.PeerPort);
                    cmd.Parameters.AddWithValue("@QueryPort",          profile.QueryPort);
                    cmd.Parameters.AddWithValue("@UseRCON",            profile.UseRCON);
                    cmd.Parameters.AddWithValue("@RCONPort",           profile.RCONPort);
                    cmd.Parameters.AddWithValue("@RCONPassword",       profile.RCONPassword);
                    cmd.Parameters.AddWithValue("@ServerVersion",      profile.ServerVersion);
                    cmd.Parameters.AddWithValue("@ServerBuildVersion", profile.ServerBuildVersion);
                    cmd.Parameters.AddWithValue("@ExecutablePath",     profile.ExecutablePath);
                    cmd.Parameters.AddWithValue("@AdditionalSettings", profile.AdditionalSettings);
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return false;
            }
        }

        private SQLiteConnection DbConnection() {
            sqliteConnection = new SQLiteConnection("Data Source=database.sqlite; Version=3;");
            sqliteConnection.Open();
            return sqliteConnection;
        }

        public bool Upsert(PluginController ctrl) {
            try {
                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = @"INSERT INTO Plugins( PluginName,  GameType,  GameName,  Version, Loaded) 
                                                     values(@PluginName, @GameType, @GameName, @Version,@Loaded)
                                                     ON CONFLICT(PluginName) DO UPDATE SET
                                                            GameType=excluded.GameType,
                                                            GameName=excluded.GameName,
                                                            Version=excluded.Version,
                                                            Loaded=excluded.Loaded;";
                    cmd.Parameters.AddWithValue("@PluginName", ctrl.PluginName);
                    cmd.Parameters.AddWithValue("@GameType",   ctrl.GameType);
                    cmd.Parameters.AddWithValue("@GameName",   ctrl.GameName);
                    cmd.Parameters.AddWithValue("@Version",    ctrl.Version);
                    cmd.Parameters.AddWithValue("@Loaded",     ctrl.Loaded);
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool Upsert(PluginInfo info) {
            try {
                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = @"INSERT INTO Plugins( PluginName,  GameType,  GameName,  Version, Loaded) 
                                                     values(@PluginName, @GameType, @GameName, @Version,@Loaded)
                                                     ON CONFLICT(PluginName) DO UPDATE SET
                                                            GameType=excluded.GameType,
                                                            GameName=excluded.GameName,
                                                            Version=excluded.Version,
                                                            Loaded=excluded.Loaded;";
                    cmd.Parameters.AddWithValue("@PluginName", info.PluginName);
                    cmd.Parameters.AddWithValue("@GameType",   info.GameType);
                    cmd.Parameters.AddWithValue("@GameName",   info.GameName);
                    cmd.Parameters.AddWithValue("@Version",    info.Version);
                    cmd.Parameters.AddWithValue("@Loaded",     info.Loaded);
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool DeletePlugin(string pluginName) {
            try {
                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = @"DELETE FROM Plugins WHERE PluginName = @PluginName;";
                    cmd.Parameters.AddWithValue("@PluginName", pluginName);
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return false;
            }
        }


        public List<PluginInfo> GetPluginInfoList() {
            try {
                SQLiteDataAdapter da = null;
                var               dt = new DataTable();

                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = "SELECT * FROM Plugins";
                    da              = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                }

                if (dt.Rows.Count == 0) throw new Exception("No Record");
                return dt.ConvertDataTable<PluginInfo>();
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return null;
            }
        }


        public BindingList<PluginInfo> GetPluginInfoListB() {
            try {
                SQLiteDataAdapter da = null;
                var               dt = new DataTable();

                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = "SELECT * FROM Plugins";
                    da              = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                }

                if (dt.Rows.Count == 0) throw new Exception("No Record");
                return dt.ConvertDataTableB<PluginInfo>();
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return null;
            }
        }

        public bool Upsert(Settings settings) {
            try {
                if (GetSettings() == null)
                    using (var cmd = DbConnection().CreateCommand()) {
                        cmd.CommandText = @"INSERT INTO Settings(GUID,  DataFolder,   DefaultInstallFolder,  SteamCMDFolder, SteamWepApiKey, CurseForgeApiKey, BackupFolder, EnableLogs,MaxLogFiles,MaxLogsDays ) 
                                                         values (@GUID, @DataFolder, @DefaultInstallFolder, @SteamCMDFolder,@SteamWepApiKey,@CurseForgeApiKey,@BackupFolder, @EnableLogs,@MaxLogFiles,@MaxLogsDays)";
                        cmd.Parameters.AddWithValue("@GUID",                 settings.GUID);
                        cmd.Parameters.AddWithValue("@DataFolder",           settings.DataFolder);
                        cmd.Parameters.AddWithValue("@DefaultInstallFolder", settings.DefaultInstallFolder);
                        cmd.Parameters.AddWithValue("@SteamCMDFolder",       settings.SteamCMDFolder);
                        cmd.Parameters.AddWithValue("@SteamWepApiKey",       settings.SteamWepApiKey);
                        cmd.Parameters.AddWithValue("@CurseForgeApiKey",     settings.CurseForgeApiKey);
                        cmd.Parameters.AddWithValue("@EnableLogs",           settings.EnableLogs);
                        cmd.Parameters.AddWithValue("@MaxLogFiles",          settings.MaxLogFiles);
                        cmd.Parameters.AddWithValue("@MaxLogsDays",          settings.MaxLogsDays);
                        cmd.ExecuteNonQuery();
                    }
                else
                    using (var cmd = DbConnection().CreateCommand()) {
                        cmd.CommandText = @"update Settings SET DataFolder=@DataFolder,
                                                                DefaultInstallFolder=@DefaultInstallFolder,
                                                                SteamCMDFolder=@SteamCMDFolder,
                                                                SteamWepApiKey=@SteamWepApiKey,
                                                                CurseForgeApiKey=@CurseForgeApiKey,
                                                                EnableLogs=@EnableLogs,
                                                                MaxLogFiles=@MaxLogFiles,
                                                                MaxLogsDays=@MaxLogsDays,
                                                                BackupFolder=@BackupFolder;";
                        cmd.Parameters.AddWithValue("@DataFolder",           settings.DataFolder);
                        cmd.Parameters.AddWithValue("@DefaultInstallFolder", settings.DefaultInstallFolder);
                        cmd.Parameters.AddWithValue("@SteamCMDFolder",       settings.SteamCMDFolder);
                        cmd.Parameters.AddWithValue("@SteamWepApiKey",       settings.SteamWepApiKey);
                        cmd.Parameters.AddWithValue("@CurseForgeApiKey",     settings.CurseForgeApiKey);
                        cmd.Parameters.AddWithValue("@BackupFolder",         settings.BackupFolder);
                        cmd.Parameters.AddWithValue("@EnableLogs",           settings.EnableLogs);
                        cmd.Parameters.AddWithValue("@MaxLogFiles",          settings.MaxLogFiles);
                        cmd.Parameters.AddWithValue("@MaxLogsDays",          settings.MaxLogsDays);
                        cmd.ExecuteNonQuery();
                    }

                return true;
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return false;
            }
        }

        public Settings GetSettings() {
            try {
                SQLiteDataAdapter da = null;
                var               dt = new DataTable();

                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = "SELECT * FROM Settings";
                    da              = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                }

                if (dt.Rows.Count == 0) throw new Exception("No Record");
                return dt.Rows[0].GetItem<Settings>();
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return null;
            }
        }

        public List<RawProfile> GetProfiles() {
            try {
                SQLiteDataAdapter da = null;
                var               dt = new DataTable();

                using (var cmd = DbConnection().CreateCommand()) {
                    cmd.CommandText = "SELECT * FROM Profiles";
                    da              = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                }

                if (dt.Rows.Count == 0) throw new Exception("No Record");
                return dt.ConvertDataTable<RawProfile>();
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}