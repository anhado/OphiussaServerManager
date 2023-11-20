using System;
using System.Collections.Generic;

namespace OphiussaServerManager.Common.Models
{
    public class CurseForgeFileLinksDetail
    {
        public string websiteUrl { get; set; }
        public string wikiUrl { get; set; }
        public string issuesUrl { get; set; }
        public string sourceUrl { get; set; }
    }
    public class CurseForgeFileCategoriesDetail
    {
        public Int64 id { get; set; }
        public Int64 gameId { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string url { get; set; }
        public string iconUrl { get; set; }
        public string dateModified { get; set; }
        public bool isClass { get; set; }
        public Int64 classId { get; set; }
        public Int64 parentCategoryId { get; set; }
    }
    public class CurseForgeFileLogoDetail
    {
        public Int64 id { get; set; }
        public Int64 modId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string thumbnailUrl { get; set; }
        public string url { get; set; }
    }
    public class CurseForgeFileAuthorsDetail
    {
        public Int64 id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }
    public class CurseForgeFileScreenshotsDetail
    {
        public Int64 id { get; set; }
        public Int64 modId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string thumbnailUrl { get; set; }
        public string url { get; set; }
    }
    public class CurseForgeFileLatestFilesDetail
    {
        public Int64 id { get; set; }
        public Int64 gameId { get; set; }
        public Int64 modId { get; set; }
        public bool isAvailable { get; set; }
        public string displayName { get; set; }
        public string fileName { get; set; }
        public Int64 releaseType { get; set; }
        public Int64 fileStatus { get; set; }
        public List<CurseForgeFileHashesDetail> hashes { get; set; }
        public DateTime fileDate { get; set; }
        public Int64 fileLength { get; set; }
        public Int64 downloadCount { get; set; }
        public Int64 fileSizeOnDisk { get; set; }
        public string downloadUrl { get; set; }
        public string[] gameVersions { get; set; }
        public List<CurseForgeFileSortableGameVersionsDetail> sortableGameVersions { get; set; }
        public string[] dependencies { get; set; }
        public Int64 alternateFileId { get; set; }
        public bool isServerPack { get; set; }
        public Int64 fileFingerprint { get; set; }
        public List<CurseForgeFileModulesDetail> modules { get; set; }
    }

    public class CurseForgeFileModulesDetail
    {
        public Int64 fingerprint { get; set; }
        public string name { get; set; }
    }

    public class CurseForgeFileSortableGameVersionsDetail
    {
        public Int64 algo { get; set; }
        public string value { get; set; }
    }

    public class CurseForgeFileHashesDetail
    {
        public Int64 algo { get; set; }
        public string value { get; set; }
    }

    public class CurseForgeFileLastestFilesIndexesDetail
    {
        public Int64 fileId { get; set; }
        public string gameVersion { get; set; }
        public string filename { get; set; }
        public Int64 releaseType { get; set; }
        public Int64 gameVersionTypeId { get; set; }
    }

    public class CurseForgeFileDetail
    {
        public Int64 id { get; set; }

        public Int64 gameId { get; set; }

        public string name { get; set; }

        public string slug { get; set; }

        public CurseForgeFileLinksDetail links { get; set; }

        public string summary { get; set; }

        public Int64 status { get; set; }

        public Int64 downloadCount { get; set; }

        public bool isFeatured { get; set; }

        public Int64 primaryCategoryId { get; set; }

        public List<CurseForgeFileCategoriesDetail> categories { get; set; }

        public Int64 classId { get; set; }

        public List<CurseForgeFileAuthorsDetail> authors { get; set; }

        public CurseForgeFileLogoDetail logo { get; set; }

        public List<CurseForgeFileScreenshotsDetail> screenshots { get; set; }
        public Int64 mainFileId { get; set; }

        public List<CurseForgeFileLatestFilesDetail> latestFiles { get; set; }
        public Int64 supportedPlatform { get; set; }
        
        public List<CurseForgeFileLastestFilesIndexesDetail> latestFilesIndexes { get; set; }

        public List<CurseForgeFileLastestFilesIndexesDetail> latestEarlyAccessFilesIndexes { get; set; }

        public DateTime dateCreated { get; set; }

        public DateTime dateModified { get; set; }

        public DateTime dateReleased { get; set; }

        public bool allowModDistribution { get; set; }

        public Int64 gamePopularityRank { get; set; }

        public bool isAvailable { get; set; }

        public Int64 thumbsUpCount { get; set; }
         
    }
}
