using System.Net;
using System.Net.Sockets;

namespace SSQLib
{
    internal class SocketUtils
    {
        private SocketUtils()
        {
        }

        internal static byte[] getInfo(IPEndPoint ipe, Packet packet)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            int length = 12288;
            socket.SendTimeout = 3000;
            socket.ReceiveTimeout = 3000;
            try
            {
                socket.SendTo(packet.outputAsBytes(), (EndPoint)ipe);
            }
            catch (SocketException ex)
            {
                throw new SSQLServerException("Could not send packet to server {" + ex.Message + "}");
            }
            byte[] buffer = new byte[length];
            EndPoint remoteEP = (EndPoint)ipe;
            try
            {
                socket.ReceiveFrom(buffer, ref remoteEP);
            }
            catch (SocketException ex)
            {
                throw new SSQLServerException("Could not receive packet from server {" + ex.Message + "}");
            }
            return buffer;
        }

        internal static byte[] getInfo(IPEndPoint ipe, byte[] request)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            int length = 12288;
            socket.SendTimeout = 3000;
            socket.ReceiveTimeout = 3000;
            try
            {
                socket.SendTo(request, (EndPoint)ipe);
            }
            catch (SocketException ex)
            {
                throw new SSQLServerException("Could not send packet to server {" + ex.Message + "}");
            }
            byte[] buffer = new byte[length];
            EndPoint remoteEP = (EndPoint)ipe;
            try
            {
                socket.ReceiveFrom(buffer, ref remoteEP);
            }
            catch (SocketException ex)
            {
                throw new SSQLServerException("Could not receive packet from server {" + ex.Message + "}");
            }
            return buffer;
        }
    }
}
