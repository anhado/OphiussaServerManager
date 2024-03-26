using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaFramework.Models {
    [TableAttributes(TableName = "AutoManagement")]
    public class AutoManagement {
        [FieldAttributes(PrimaryKey = true, AutoIncrement = true)] public int    Id             { get; set; }
        [FieldAttributes(DataType = "Varchar(100)")]               public string ServerKey      { get; set; }
        public                                                            int    ShutdownServer { get; set; }
        [FieldAttributes(DataType = "Varchar(4)")] public                 string ShutdownHour   { get; set; }
        public                                                            bool   ShutdownSun    { get; set; }
        public                                                            bool   ShutdownMon    { get; set; }
        public                                                            bool   ShutdownTue    { get; set; }
        public                                                            bool   ShutdownWed    { get; set; }
        public                                                            bool   ShutdownThu    { get; set; }
        public                                                            bool   ShutdownFri    { get; set; }
        public                                                            bool   ShutdownSat    { get; set; }
        public                                                            bool   ShutdownSunday { get; set; }
        public                                                            bool   UpdateServer   { get; set; }
        public                                                            bool   RestartServer  { get; set; }
    }
}
