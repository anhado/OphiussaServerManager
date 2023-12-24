namespace OphiussaServerManager.Common.Models {
    public class WorkshopFileDetail {
        public int Result { get; set; }

        public string Publishedfileid { get; set; }

        public string Creator { get; set; }

        public string CreatorAppid { get; set; }

        public string ConsumerAppid { get; set; }

        public int ConsumerShortcutid { get; set; }

        public string Filename { get; set; }

        public string FileSize { get; set; }

        public string PreviewFileSize { get; set; }

        public string FileUrl { get; set; }

        public string PreviewUrl { get; set; }

        public string Url { get; set; }

        public string HcontentFile { get; set; }

        public string HcontentPreview { get; set; }

        public string Title { get; set; }

        public string FileDescription { get; set; }

        public int TimeCreated { get; set; }

        public int TimeUpdated { get; set; }

        public int Visibility { get; set; }

        public int Flags { get; set; }

        public bool WorkshopFile { get; set; }

        public bool WorkshopAccepted { get; set; }

        public bool ShowSubscribeAll { get; set; }

        public int NumCommentsDeveloper { get; set; }

        public int NumCommentsPublic { get; set; }

        public bool Banned { get; set; }

        public string BanReason { get; set; }

        public string Banner { get; set; }

        public bool CanBeDeleted { get; set; }

        public bool Incompatible { get; set; }

        public string AppName { get; set; }

        public int FileType { get; set; }

        public bool CanSubscribe { get; set; }

        public int Subscriptions { get; set; }

        public int Favorited { get; set; }

        public int Followers { get; set; }

        public int LifetimeSubscriptions { get; set; }

        public int LifetimeFavorited { get; set; }

        public int LifetimeFollowers { get; set; }

        public int Views { get; set; }

        public bool SpoilerTag { get; set; }

        public int NumChildren { get; set; }

        public int NumReports { get; set; }

        public int Language { get; set; }

        public bool IsAdded { get; set; }
    }
}