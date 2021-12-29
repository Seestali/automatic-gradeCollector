namespace client.Utils
{
    public class ByteUtil
    {
        public static void InsertUInt32ToByteArray(byte[] byteArray, int begin, uint value)
        {
            byteArray[begin++] = (byte)(value >> 24);
            byteArray[begin++] = (byte)(value >> 16);
            byteArray[begin++] = (byte)(value >> 8);
            byteArray[begin] = (byte)value;
        }

        public static uint GetUInt32FromByteArray(byte[] byteArray, int begin)
        {
            int number = byteArray[begin++] << 24;
            number += byteArray[begin++] << 16;
            number += byteArray[begin++] << 8;
            number += byteArray[begin];
            return (uint)number;
        }
    }
}
