using OphiussaServerManager.Helpers;
using System;
using System.Collections.Generic;
using System.IO;

namespace OphiussaServerManager.Models
{
    public class WorkshopFileDetailResponse
    {
        public DateTime cached = DateTime.UtcNow;

        public int total { get; set; }

        public List<WorkshopFileDetail> publishedfiledetails { get; set; }

        public static WorkshopFileDetailResponse Load(string file) => string.IsNullOrWhiteSpace(file) || !File.Exists(file) ? (WorkshopFileDetailResponse)null : JsonUtils.DeserializeFromFile<WorkshopFileDetailResponse>(file);

        public bool Save(string file) => !string.IsNullOrWhiteSpace(file) && JsonUtils.SerializeToFile<WorkshopFileDetailResponse>(this, file);
    }
}
