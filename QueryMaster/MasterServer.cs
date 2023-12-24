using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace QueryMaster {
    /// <summary>
    ///     Encapsulates a method that has a parameter of type ReadOnlyCollection which accepts IPEndPoint instances.
    ///     Invoked when a reply from Master Server is received.
    /// </summary>
    /// <param name="endPoints">Server Sockets</param>
    public delegate void MasterIpCallback(ReadOnlyCollection<IPEndPoint> endPoints);

    /// <summary>
    ///     Provides methods to query master server.
    /// </summary>
    public class MasterServer : IDisposable {
        private readonly Socket           _udpSocket;
        public readonly  IPEndPoint       SeedEndpoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 0);
        private          MasterIpCallback _callback;
        private          IpFilter         _filter;
        private          bool             _isListening;
        private          byte[]           _msg;
        private          byte[]           _recvData;
        private          Region           _regionCode;
        private          IPEndPoint       _remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

        internal MasterServer(IPEndPoint endPoint) {
            _udpSocket = new Socket(AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, ProtocolType.Udp);
            _udpSocket.Connect(endPoint);
        }

        /// <summary>
        ///     Disposes all the resources used MasterServer instance
        /// </summary>
        public void Dispose() {
            if (_udpSocket != null && _isListening)
                _udpSocket.Close();
        }

        /// <summary>
        ///     Starts receiving socket addresses of servers.
        /// </summary>
        /// <param name="region">The region of the world that you wish to find servers in.</param>
        /// <param name="callback">Called when a batch of Socket addresses are received.</param>
        /// <param name="filter">Used to set filter on the type of server required.</param>
        public void GetAddresses(Region region, MasterIpCallback callback, IpFilter filter = null) {
            if (_isListening) return;

            _regionCode  = region;
            _callback    = callback;
            _filter      = filter;
            _isListening = true;
            var endPoint = SeedEndpoint;
            _msg = MasterUtil.BuildPacket(endPoint.ToString(), _regionCode, _filter);
            _udpSocket.Send(_msg);
            _recvData = new byte[1400];
            _udpSocket.BeginReceive(_recvData, 0, _recvData.Length, SocketFlags.None, Recv, null);
        }

        private void Recv(IAsyncResult res) {
            int bytesRev = 0;
            try {
                bytesRev = _udpSocket.EndReceive(res);
            }
            catch (ObjectDisposedException) {
                return;
            }

            var endpoints = MasterUtil.ProcessPacket(_recvData.Take(bytesRev).ToArray());
            //ThreadPool.QueueUserWorkItem(x => Callback(endpoints));
            _callback(endpoints);
            if (!endpoints.Last().Equals(SeedEndpoint)) {
                _msg = MasterUtil.BuildPacket(endpoints.Last().ToString(), _regionCode, _filter);
                _udpSocket.Send(_msg);
                _udpSocket.BeginReceive(_recvData, 0, _recvData.Length, SocketFlags.None, Recv, null);
            }
            else {
                _isListening = false;
            }
        }
    }
}