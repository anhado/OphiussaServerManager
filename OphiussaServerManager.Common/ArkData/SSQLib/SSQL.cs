using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SSQLib
{
    public class SSQL
    {
        public ServerInfo Server(IPEndPoint ip_end)
        {
            ServerInfo serverInfo1 = new ServerInfo();
            Packet packet = new Packet();
            packet.Data = "TSource Engine Query";
            byte[] info;
            try
            {
                info = SocketUtils.getInfo(ip_end, packet);
            }
            catch (SSQLServerException ex)
            {
                throw ex;
            }
            int num1 = 4;
            byte[] numArray1 = info;
            int index1 = num1;
            int num2 = index1 + 1;
            if (numArray1[index1] != (byte)73)
                return (ServerInfo)null;
            byte[] numArray2 = info;
            int index2 = num2;
            int index3 = index2 + 1;
            if (numArray2[index2] < (byte)7)
                return (ServerInfo)null;
            StringBuilder stringBuilder1 = new StringBuilder();
            for (; info[index3] != (byte)0; ++index3)
                stringBuilder1.Append((char)info[index3]);
            int index4 = index3 + 1;
            serverInfo1.Name = stringBuilder1.ToString();
            StringBuilder stringBuilder2 = new StringBuilder();
            for (; info[index4] != (byte)0; ++index4)
                stringBuilder2.Append((char)info[index4]);
            int index5 = index4 + 1;
            serverInfo1.Map = stringBuilder2.ToString();
            StringBuilder stringBuilder3 = new StringBuilder();
            for (; info[index5] != (byte)0; ++index5)
                stringBuilder3.Append((char)info[index5]);
            int index6 = index5 + 1;
            StringBuilder stringBuilder4 = new StringBuilder();
            for (; info[index6] != (byte)0; ++index6)
                stringBuilder4.Append((char)info[index6]);
            int startIndex = index6 + 1;
            serverInfo1.Game = stringBuilder4.ToString() + " (" + stringBuilder3.ToString() + ")";
            short int16 = BitConverter.ToInt16(info, startIndex);
            int num3 = startIndex + 2;
            serverInfo1.AppID = int16.ToString();
            ServerInfo serverInfo2 = serverInfo1;
            byte[] numArray3 = info;
            int index7 = num3;
            int num4 = index7 + 1;
            string str1 = numArray3[index7].ToString();
            serverInfo2.PlayerCount = str1;
            ServerInfo serverInfo3 = serverInfo1;
            byte[] numArray4 = info;
            int index8 = num4;
            int num5 = index8 + 1;
            string str2 = numArray4[index8].ToString();
            serverInfo3.MaxPlayers = str2;
            ServerInfo serverInfo4 = serverInfo1;
            byte[] numArray5 = info;
            int index9 = num5;
            int index10 = index9 + 1;
            string str3 = numArray5[index9].ToString();
            serverInfo4.BotCount = str3;
            if (info[index10] == (byte)108)
                serverInfo1.Dedicated = ServerInfo.DedicatedType.LISTEN;
            else if (info[index10] == (byte)100)
                serverInfo1.Dedicated = ServerInfo.DedicatedType.DEDICATED;
            else if (info[index10] == (byte)112)
                serverInfo1.Dedicated = ServerInfo.DedicatedType.SOURCETV;
            int index11 = index10 + 1;
            if (info[index11] == (byte)108)
                serverInfo1.OS = ServerInfo.OSType.LINUX;
            else if (info[index11] == (byte)119)
                serverInfo1.OS = ServerInfo.OSType.WINDOWS;
            int num6 = index11 + 1;
            byte[] numArray6 = info;
            int index12 = num6;
            int num7 = index12 + 1;
            if (numArray6[index12] == (byte)1)
                serverInfo1.Password = true;
            byte[] numArray7 = info;
            int index13 = num7;
            int index14 = index13 + 1;
            if (numArray7[index13] == (byte)1)
                serverInfo1.VAC = true;
            StringBuilder stringBuilder5 = new StringBuilder();
            for (; info[index14] != (byte)0; ++index14)
                stringBuilder5.Append((char)info[index14]);
            int num8 = index14 + 1;
            serverInfo1.Version = stringBuilder5.ToString();
            return serverInfo1;
        }

        public ArrayList Players(IPEndPoint ip_end)
        {
            ArrayList arrayList = new ArrayList();
            byte[] request1 = new byte[9]
            {
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        (byte) 85,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0
            };
            byte[] info1;
            try
            {
                info1 = SocketUtils.getInfo(ip_end, request1);
            }
            catch (SSQLServerException ex)
            {
                throw ex;
            }
            int num1 = 4;
            byte[] numArray1 = info1;
            int index1 = num1;
            int num2 = index1 + 1;
            if (numArray1[index1] != (byte)65)
                return (ArrayList)null;
            byte[] request2 = new byte[9]
            {
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        byte.MaxValue,
        (byte) 85,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0
            };
            byte[] numArray2 = request2;
            byte[] numArray3 = info1;
            int index2 = num2;
            int num3 = index2 + 1;
            int num4 = (int)numArray3[index2];
            numArray2[5] = (byte)num4;
            byte[] numArray4 = request2;
            byte[] numArray5 = info1;
            int index3 = num3;
            int num5 = index3 + 1;
            int num6 = (int)numArray5[index3];
            numArray4[6] = (byte)num6;
            byte[] numArray6 = request2;
            byte[] numArray7 = info1;
            int index4 = num5;
            int num7 = index4 + 1;
            int num8 = (int)numArray7[index4];
            numArray6[7] = (byte)num8;
            byte[] numArray8 = request2;
            byte[] numArray9 = info1;
            int index5 = num7;
            int num9 = index5 + 1;
            int num10 = (int)numArray9[index5];
            numArray8[8] = (byte)num10;
            byte[] info2;
            try
            {
                info2 = SocketUtils.getInfo(ip_end, request2);
            }
            catch (SSQLServerException ex)
            {
                return (ArrayList)null;
            }
            int num11 = 4;
            byte[] numArray10 = info2;
            int index6 = num11;
            int num12 = index6 + 1;
            if (numArray10[index6] != (byte)68)
                return (ArrayList)null;
            byte[] numArray11 = info2;
            int index7 = num12;
            int num13 = index7 + 1;
            byte num14 = numArray11[index7];
            for (int index8 = 0; index8 < (int)num14; ++index8)
            {
                PlayerInfo playerInfo1 = new PlayerInfo();
                PlayerInfo playerInfo2 = playerInfo1;
                byte[] numArray12 = info2;
                int index9 = num13;
                int index10 = index9 + 1;
                int num15 = (int)numArray12[index9];
                playerInfo2.Index = num15;
                List<byte> byteList = new List<byte>();
                while (info2[index10] != (byte)0)
                    byteList.Add(info2[index10++]);
                int index11 = index10 + 1;
                playerInfo1.Name = Encoding.UTF8.GetString(byteList.ToArray());
                playerInfo1.Kills = (int)info2[index11] & (int)byte.MaxValue | ((int)info2[index11 + 1] & (int)byte.MaxValue) << 8 | ((int)info2[index11 + 2] & (int)byte.MaxValue) << 16 | ((int)info2[index11 + 3] & (int)byte.MaxValue) << 24;
                int index12 = index11 + 5;
                playerInfo1.Time = (float)((int)info2[index12] & (int)byte.MaxValue | ((int)info2[index12 + 1] & (int)byte.MaxValue) << 8);
                num13 = index12 + 3;
                arrayList.Add((object)playerInfo1);
            }
            return arrayList;
        }
    }
}
