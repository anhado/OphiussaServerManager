using OphiussaServerManager.Common;
using OphiussaServerManager.Common.Helpers;
using System;
using System.Collections.Generic;
using System.IO;

namespace OphiussaServerManager.Common.Models
{
    public class CurseForgeFileDetailPaginationResponse
    {
        public int index { get; set; }
        public int pageSize { get; set; }
        public int resultCount { get; set; }
        public int totalCount { get; set; }
    }

    public class CurseForgeFileDetailResponse
    {
        public DateTime cached = DateTime.UtcNow;

        public CurseForgeFileDetailPaginationResponse pagination { get; set; }

        public List<CurseForgeFileDetail> data { get; set; }

        public static CurseForgeFileDetailResponse Load(string file) => string.IsNullOrWhiteSpace(file) || !File.Exists(file) ? (CurseForgeFileDetailResponse)null : JsonUtils.DeserializeFromFile<CurseForgeFileDetailResponse>(file);

        public bool Save(string file) => !string.IsNullOrWhiteSpace(file) && JsonUtils.SerializeToFile<CurseForgeFileDetailResponse>(this, file);
    }
}
