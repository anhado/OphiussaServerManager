using System;
using System.Collections.Generic;
using System.IO;
using OphiussaServerManager.Common.Helpers;

namespace OphiussaServerManager.Common.Models {
    public class CurseForgeFileDetailPaginationResponse {
        public int Index       { get; set; }
        public int PageSize    { get; set; }
        public int ResultCount { get; set; }
        public int TotalCount  { get; set; }
    }

    public class CurseForgeFileDetailResponse {
        public DateTime Cached = DateTime.UtcNow;

        public CurseForgeFileDetailPaginationResponse Pagination { get; set; }

        public List<CurseForgeFileDetail> Data { get; set; }

        public static CurseForgeFileDetailResponse Load(string file) {
            return string.IsNullOrWhiteSpace(file) || !File.Exists(file) ? null : JsonUtils.DeserializeFromFile<CurseForgeFileDetailResponse>(file);
        }

        public bool Save(string file) {
            return !string.IsNullOrWhiteSpace(file) && JsonUtils.SerializeToFile(this, file);
        }
    }
}