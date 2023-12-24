using System.Collections.Generic;

namespace OphiussaServerManager.Common.Models {
    public class PublishedFileDetail {
        public string Publishedfileid { get; set; }

        public int Result { get; set; }

        public string Creator { get; set; }

        public string CreatorAppId { get; set; }

        public string ConsumerAppId { get; set; }

        public string Filename { get; set; }

        public string FileSize { get; set; }

        public string FileUrl { get; set; }

        public string HcontentFile { get; set; }

        public string PreviewUrl { get; set; }

        public string HcontentPreview { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int TimeCreated { get; set; }

        public int TimeUpdated { get; set; }

        public int Visibility { get; set; }

        public int Banned { get; set; }

        public string BanReason { get; set; }

        public int Subscriptions { get; set; }

        public int Favorited { get; set; }

        public int LifetimeSubscriptions { get; set; }

        public int LifetimeFavorited { get; set; }

        public int Views { get; set; }

        public List<object> Tags { get; set; }
    }
}