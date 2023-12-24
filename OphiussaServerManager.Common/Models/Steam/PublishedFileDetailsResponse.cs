using System.Collections.Generic;

namespace OphiussaServerManager.Common.Models {
    public class PublishedFileDetailsResponse {
        public int Result { get; set; }

        public int Resultcount { get; set; }

        public List<PublishedFileDetail> Publishedfiledetails { get; set; }
    }
}