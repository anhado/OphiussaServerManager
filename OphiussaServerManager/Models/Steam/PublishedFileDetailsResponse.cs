using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Models
{
    public class PublishedFileDetailsResponse
    {
        public int result { get; set; }

        public int resultcount { get; set; }

        public List<PublishedFileDetail> publishedfiledetails { get; set; }
    }
}
