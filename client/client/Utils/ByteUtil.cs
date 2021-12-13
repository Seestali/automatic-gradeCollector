using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Utils
{
    public class ByteUtil
    {
        public static void InsertUInt32ToByteArray(ref byte[] byteArray, int begin, uint value)
        {
            byteArray[begin]     = (byte)(value >> 24);
            byteArray[begin + 1] = (byte)(value >> 16);
            byteArray[begin + 2] = (byte)(value >> 8);
            byteArray[begin + 3] = (byte)value;
        }

        public static uint GetUInt32FromByteArray(ref byte[] byteArray, int begin)
        {
            return (uint)(
                byteArray[begin] << 24 + 
                byteArray[begin + 1] << 16 + 
                byteArray[begin + 2] << 8 + 
                byteArray[begin + 3]
                );
        }
    }
}
