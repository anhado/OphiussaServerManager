using System.Net;

namespace QueryMaster {
    internal class Source : Server {
        internal Source(IPEndPoint address, int sendTimeOut, int receiveTimeOut) : base(address, EngineType.Source, false, sendTimeOut, receiveTimeOut) {
        }

        public override Rcon GetControl(string pass) {
            RConObj = RconSource.Authorize(Socket.Address, pass, Socket.Socket.SendTimeout, Socket.Socket.ReceiveTimeout);
            return RConObj;
        }
    }
}