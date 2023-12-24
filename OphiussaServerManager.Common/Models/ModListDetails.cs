using System;

namespace OphiussaServerManager.Common.Models {
    public class ModListDetails {
        public int      Order             { get; set; }
        public string   ModId             { get; set; }
        public string   Name              { get; set; }
        public string   ModType           { get; set; }
        public DateTime LastDownloaded    { get; set; }
        public DateTime LastUpdatedAuthor { get; set; }
        public long     TimeStamp         { get; set; }
        public string   FolderSize        { get; set; }
        public long     Subscriptions     { get; set; }
        public string   Link              { get; set; }
    }
}