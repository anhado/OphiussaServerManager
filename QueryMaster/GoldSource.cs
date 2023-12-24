using System.Net;

namespace QueryMaster {
    internal class GoldSource : Server {
        internal GoldSource(IPEndPoint address, bool? isObsolete, int sendTimeOut, int receiveTimeOut) : base(address, EngineType.GoldSource, isObsolete, sendTimeOut, receiveTimeOut) {
        }

        public override Rcon GetControl(string pass) {
            RConObj = RconGoldSource.Authorize(Socket.Address, pass);
            return RConObj;
        }
    }
}