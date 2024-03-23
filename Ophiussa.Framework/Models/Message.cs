using System;

namespace OphiussaFramework.Models {
    public class Message {
        public bool      Success     { get; set; }
        public string    MessageText { get; set; }
        public Exception Exception   { get; set; }
    }
}