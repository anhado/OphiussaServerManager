﻿using System.Collections.Generic;
using System.Net;

namespace QueryMaster {
    internal class TcpQuery : ServerSocket {
        private byte[] _emptyPkt = { 0x0a, 0x00, 0x00, 0x00, 0x0A, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        internal TcpQuery(IPEndPoint address, int sendTimeOut, int receiveTimeOut)
            : base(SocketType.Tcp) {
            Connect(address);
            Socket.SendTimeout    = sendTimeOut;
            Socket.ReceiveTimeout = receiveTimeOut;
        }

        internal byte[] GetResponse(byte[] msg) {
            byte[] recvData;
            SendData(msg);
            recvData = ReceiveData(); //Response value packet
            //recvData = ReceiveData();//Auth response packet
            return recvData;
        }

        internal List<byte[]> GetMultiPacketResponse(byte[] msg) {
            var recvBytes = new List<byte[]>();
            //bool isRemaining = true;
            byte[] recvData;
            SendData(msg);
            //SendData(EmptyPkt);//Empty packet
            recvData = ReceiveData(); //reply
            recvBytes.Add(recvData);
#if false
            do
            {
                recvData = ReceiveData();//may or may not be an empty packet
                if (BitConverter.ToInt32(recvData, 4) == (int)PacketId.Empty)
                    isRemaining = false;
                else
                    recvBytes.Add(recvData);
            } while (isRemaining);
#endif
            return recvBytes;
        }
    }
}