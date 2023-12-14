using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OphiussaServerManager.Common
{
    public class ReadFileInChunksAndCompareVector : ReadIntoByteBufferInChunks
    {
        public ReadFileInChunksAndCompareVector(string filePath01, string filePath02, int chunkSize)
            : base(filePath01, filePath02, chunkSize)
        {
        }

        protected override bool OnCompare()
        {
            return StreamAreEqual(FileInfo1.OpenRead(), FileInfo2.OpenRead());
        }

        private bool StreamAreEqual(in Stream stream1, in Stream stream2)
        {
            var buffer1 = new byte[ChunkSize];
            var buffer2 = new byte[ChunkSize];

            while (true)
            {
                var count1 = ReadIntoBuffer(stream1, buffer1);
                var count2 = ReadIntoBuffer(stream2, buffer2);

                if (count1 != count2)
                {
                    return false;
                }

                if (count1 == 0)
                {
                    return true;
                }

                var totalProcessed = 0;
                while (totalProcessed < buffer1.Length)
                {
                    var v1 = new Vector<byte>(buffer1, totalProcessed);
                    var v2 = new Vector<byte>(buffer2, totalProcessed);

                    if (Vector.EqualsAll(v1, v2) == false)
                    {
                        return false;
                    }
                    totalProcessed += Vector<byte>.Count;
                }
            }
        }
    }
}
