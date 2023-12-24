using System;
using System.Runtime.Serialization;

namespace QueryMaster {
    /// <summary>
    ///     The exception that is thrown by the QueryMaster library
    /// </summary>
    [Serializable]
    public class QueryMasterException : Exception {
        public QueryMasterException() {
        }

        public QueryMasterException(string message) : base(message) {
        }

        public QueryMasterException(string message, Exception innerException) : base(message, innerException) {
        }

        protected QueryMasterException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }

    /// <summary>
    ///     The exception that is thrown when an invalid message header is received
    /// </summary>
    [Serializable]
    public class InvalidHeaderException : QueryMasterException {
        public InvalidHeaderException() {
        }

        public InvalidHeaderException(string message) : base(message) {
        }

        public InvalidHeaderException(string message, Exception innerException) : base(message, innerException) {
        }

        protected InvalidHeaderException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }

    /// <summary>
    ///     The exception that is thrown when an invalid packet is received
    /// </summary>
    [Serializable]
    public class InvalidPacketException : QueryMasterException {
        public InvalidPacketException() {
        }

        public InvalidPacketException(string message) : base(message) {
        }

        public InvalidPacketException(string message, Exception innerException) : base(message, innerException) {
        }

        protected InvalidPacketException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }

    /// <summary>
    ///     The exception that is thrown when there is an error while parsing received packets
    /// </summary>
    [Serializable]
    public class ParseException : QueryMasterException {
        public ParseException() {
        }

        public ParseException(string message) : base(message) {
        }

        public ParseException(string message, Exception innerException) : base(message, innerException) {
        }

        protected ParseException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}