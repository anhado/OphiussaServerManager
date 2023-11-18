using System;
using System.Text;

namespace SSQLib
{
    internal class Packet
    {
        internal int RequestId;
        internal string Data = "";

        internal Packet()
        {
        }

        internal byte[] outputAsBytes()
        {
            byte[] destinationArray;
            if (this.Data.Length > 0)
            {
                destinationArray = new byte[this.Data.Length + 5];
                destinationArray[0] = byte.MaxValue;
                destinationArray[1] = byte.MaxValue;
                destinationArray[2] = byte.MaxValue;
                destinationArray[3] = byte.MaxValue;
                Array.Copy((Array)Encoding.UTF8.GetBytes(this.Data), 0, (Array)destinationArray, 4, this.Data.Length);
            }
            else
                destinationArray = new byte[5]
                {
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          (byte) 87
                };
            return destinationArray;
        }
    }
}
