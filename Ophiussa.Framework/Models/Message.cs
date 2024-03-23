using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaFramework.Models
{
    public class Message
    {
        public bool      Success     { get; set; }
        public string    MessageText { get; set; }
        public Exception Exception   { get; set; }
    }
}
