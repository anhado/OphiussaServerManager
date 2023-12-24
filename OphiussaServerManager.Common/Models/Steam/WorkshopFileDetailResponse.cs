using System;
using System.Collections.Generic;
using System.IO;
using OphiussaServerManager.Common.Helpers;

namespace OphiussaServerManager.Common.Models {
    public class WorkshopFileDetailResponse {
        public DateTime Cached = DateTime.UtcNow;

        public int Total { get; set; }

        public List<WorkshopFileDetail> Publishedfiledetails { get; set; }

        public static WorkshopFileDetailResponse Load(string file) {
            return string.IsNullOrWhiteSpace(file) || !File.Exists(file) ? null : JsonUtils.DeserializeFromFile<WorkshopFileDetailResponse>(file);
        }

        public bool Save(string file) {
            return !string.IsNullOrWhiteSpace(file) && JsonUtils.SerializeToFile(this, file);
        }
    }
}