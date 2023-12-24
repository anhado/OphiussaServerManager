﻿using System;
using System.Linq;
using System.Net;
using System.Text;

namespace QueryMaster {
    internal class RconSource : Rcon {
        internal TcpQuery Socket;

        private RconSource(IPEndPoint address, int sendTimeOut, int receiveTimeOut) {
            Socket = new TcpQuery(address, sendTimeOut, receiveTimeOut);
        }

        internal static Rcon Authorize(IPEndPoint address, string msg, int sendTimeOut, int receiveTimeOut) {
            var    obj      = new RconSource(address, sendTimeOut, receiveTimeOut);
            byte[] recvData = new byte[50];
            var    packet   = new RconSrcPacket { Body = msg, Id = (int)PacketId.ExecCmd, Type = (int)PacketType.Auth };
            recvData = obj.Socket.GetResponse(RconUtil.GetBytes(packet));
            int header;
            try {
                header = BitConverter.ToInt32(recvData, 4);
            }
            catch (Exception e) {
                e.Data.Add("ReceivedData", recvData);
                throw;
            }

            if (header != -1) return obj;
            obj.Socket.Dispose();
            return obj;
        }

        public override string SendCommand(string command) {
            var senPacket = new RconSrcPacket { Body = command, Id = (int)PacketId.ExecCmd, Type = (int)PacketType.Exec };
            var recvData  = Socket.GetMultiPacketResponse(RconUtil.GetBytes(senPacket));
            var str       = new StringBuilder();
            try {
                for (int i = 0; i < recvData.Count; i++) {
                    //consecutive rcon command replies start with an empty packet 
                    if (BitConverter.ToInt32(recvData[i], 4) == (int)PacketId.Empty)
                        continue;
                    str.Append(RconUtil.ProcessPacket(recvData[i]).Body);
                }
            }
            catch (Exception e) {
                e.Data.Add("ReceivedData", recvData.SelectMany(x => x).ToArray());
                throw;
            }

            return str.ToString();
        }

        public override void AddlogAddress(string ip, ushort port) {
            SendCommand("logaddress_add " + ip + ":" + port);
        }

        public override void RemovelogAddress(string ip, ushort port) {
            SendCommand("logaddress_del " + ip + ":" + port);
        }

        public override void Dispose() {
            if (Socket != null)
                Socket.Dispose();
        }
    }
}