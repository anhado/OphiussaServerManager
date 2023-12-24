using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace QueryMaster {
    internal class ServerSocket : IDisposable {
        internal static readonly int        UdpBufferSize = 1400;
        internal static readonly int        TcpBufferSize = 4110;
        internal                 IPEndPoint Address;
        protected internal       int        BufferSize;
        protected                bool       IsDisposed;

        internal ServerSocket(SocketType type) {
            switch (type) {
                case SocketType.Tcp:
                    Socket     = new Socket(AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, ProtocolType.Tcp);
                    BufferSize = TcpBufferSize;
                    break;
                case SocketType.Udp:
                    Socket     = new Socket(AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, ProtocolType.Udp);
                    BufferSize = UdpBufferSize;
                    break;
                default: throw new ArgumentException("An invalid SocketType was specified.");
            }

            Socket.SendTimeout    = 3000;
            Socket.ReceiveTimeout = 3000;

            IsDisposed = false;
        }

        internal Socket Socket { set; get; }

        public virtual void Dispose() {
            if (IsDisposed)
                return;
            if (Socket != null)
                Socket.Close();
            IsDisposed = true;
        }

        internal void Connect(IPEndPoint address) {
            Address = address;
            Socket.Connect(Address);
        }

        internal int SendData(byte[] data) {
            return Socket.Send(data);
        }

        internal byte[] ReceiveData() {
            byte[] recvData = new byte[BufferSize];
            int    recv     = 0;
            recv = Socket.Receive(recvData);
            return recvData.Take(recv).ToArray();
        }
    }
}