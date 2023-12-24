using System;
using System.Collections.Generic;

namespace OphiussaServerManager.Common.Models {
    public class CurseForgeFileLinksDetail {
        public string WebsiteUrl { get; set; }
        public string WikiUrl    { get; set; }
        public string IssuesUrl  { get; set; }
        public string SourceUrl  { get; set; }
    }

    public class CurseForgeFileCategoriesDetail {
        public long   Id               { get; set; }
        public long   GameId           { get; set; }
        public string Name             { get; set; }
        public string Slug             { get; set; }
        public string Url              { get; set; }
        public string IconUrl          { get; set; }
        public string DateModified     { get; set; }
        public bool   IsClass          { get; set; }
        public long   ClassId          { get; set; }
        public long   ParentCategoryId { get; set; }
    }

    public class CurseForgeFileLogoDetail {
        public long   Id           { get; set; }
        public long   ModId        { get; set; }
        public string Title        { get; set; }
        public string Description  { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Url          { get; set; }
    }

    public class CurseForgeFileAuthorsDetail {
        public long   Id   { get; set; }
        public string Name { get; set; }
        public string Url  { get; set; }
    }

    public class CurseForgeFileScreenshotsDetail {
        public long   Id           { get; set; }
        public long   ModId        { get; set; }
        public string Title        { get; set; }
        public string Description  { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Url          { get; set; }
    }

    public class CurseForgeFileLatestFilesDetail {
        public long                                           Id                   { get; set; }
        public long                                           GameId               { get; set; }
        public long                                           ModId                { get; set; }
        public bool                                           IsAvailable          { get; set; }
        public string                                         DisplayName          { get; set; }
        public string                                         FileName             { get; set; }
        public long                                           ReleaseType          { get; set; }
        public long                                           FileStatus           { get; set; }
        public List<CurseForgeFileHashesDetail>               Hashes               { get; set; }
        public DateTime                                       FileDate             { get; set; }
        public long                                           FileLength           { get; set; }
        public long                                           DownloadCount        { get; set; }
        public long                                           FileSizeOnDisk       { get; set; }
        public string                                         DownloadUrl          { get; set; }
        public string[]                                       GameVersions         { get; set; }
        public List<CurseForgeFileSortableGameVersionsDetail> SortableGameVersions { get; set; }
        public string[]                                       Dependencies         { get; set; }
        public long                                           AlternateFileId      { get; set; }
        public bool                                           IsServerPack         { get; set; }
        public long                                           FileFingerprint      { get; set; }
        public List<CurseForgeFileModulesDetail>              Modules              { get; set; }
    }

    public class CurseForgeFileModulesDetail {
        public long   Fingerprint { get; set; }
        public string Name        { get; set; }
    }

    public class CurseForgeFileSortableGameVersionsDetail {
        public long   Algo  { get; set; }
        public string Value { get; set; }
    }

    public class CurseForgeFileHashesDetail {
        public long   Algo  { get; set; }
        public string Value { get; set; }
    }

    public class CurseForgeFileLastestFilesIndexesDetail {
        public long   FileId            { get; set; }
        public string GameVersion       { get; set; }
        public string Filename          { get; set; }
        public long   ReleaseType       { get; set; }
        public long   GameVersionTypeId { get; set; }
    }

    public class CurseForgeFileDetail {
        public long Id { get; set; }

        public long GameId { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public CurseForgeFileLinksDetail Links { get; set; }

        public string Summary { get; set; }

        public long Status { get; set; }

        public long DownloadCount { get; set; }

        public bool IsFeatured { get; set; }

        public long PrimaryCategoryId { get; set; }

        public List<CurseForgeFileCategoriesDetail> Categories { get; set; }

        public long ClassId { get; set; }

        public List<CurseForgeFileAuthorsDetail> Authors { get; set; }

        public CurseForgeFileLogoDetail Logo { get; set; }

        public List<CurseForgeFileScreenshotsDetail> Screenshots { get; set; }
        public long                                  MainFileId  { get; set; }

        public List<CurseForgeFileLatestFilesDetail> LatestFiles       { get; set; }
        public long                                  SupportedPlatform { get; set; }

        public List<CurseForgeFileLastestFilesIndexesDetail> LatestFilesIndexes { get; set; }

        public List<CurseForgeFileLastestFilesIndexesDetail> LatestEarlyAccessFilesIndexes { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime DateReleased { get; set; }

        public bool AllowModDistribution { get; set; }

        public long GamePopularityRank { get; set; }

        public bool IsAvailable { get; set; }

        public long ThumbsUpCount { get; set; }
    }
}