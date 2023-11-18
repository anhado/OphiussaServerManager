using System;
using System.Text;

namespace OphiussaServerManager.Common
{
    internal class HelpersArkData
    {
        public static int GetInt(byte[] data, string name)
        {
            byte[] bytes1 = Encoding.Default.GetBytes(name);
            byte[] bytes2 = Encoding.Default.GetBytes("IntProperty");
            int offset = data.LocateFirst(bytes1);
            int num = data.LocateFirst(bytes2, offset);
            return num > -1 ? BitConverter.ToInt32(data, num + bytes2.Length + 9) : -1;
        }

        public static ushort GetUInt16(byte[] data, string name)
        {
            byte[] bytes1 = Encoding.Default.GetBytes(name);
            byte[] bytes2 = Encoding.Default.GetBytes("UInt16Property");
            int offset = data.LocateFirst(bytes1);
            int num = data.LocateFirst(bytes2, offset);
            return num >= 0 ? BitConverter.ToUInt16(data, num + bytes2.Length + 9) : (ushort)0;
        }

        public static string GetString(byte[] data, string name)
        {
            byte[] bytes1 = Encoding.Default.GetBytes(name);
            byte[] bytes2 = Encoding.Default.GetBytes("StrProperty");
            int offset = data.LocateFirst(bytes1);
            int num = data.LocateFirst(bytes2, offset);
            if (num < 0)
                return string.Empty;
            byte[] destinationArray = new byte[1];
            Array.Copy((Array)data, num + bytes2.Length + 1, (Array)destinationArray, 0, 1);
            int length = (int)destinationArray[0] - (data[num + bytes2.Length + 12] == byte.MaxValue ? 6 : 5);
            byte[] numArray = new byte[length];
            Array.Copy((Array)data, num + bytes2.Length + 13, (Array)numArray, 0, length);
            return data[num + bytes2.Length + 12] == byte.MaxValue ? Encoding.Unicode.GetString(numArray) : Encoding.Default.GetString(numArray);
        }
    }
}
