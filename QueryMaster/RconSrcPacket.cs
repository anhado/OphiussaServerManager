namespace QueryMaster {
    internal class RconSrcPacket {
        internal int    Size { get; set; }
        internal int    Id   { get; set; }
        internal int    Type { get; set; }
        internal string Body { get; set; }
    }
}