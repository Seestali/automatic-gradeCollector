namespace client.Utils
{
    /// <summary>
    /// Provides byte array manipulating methods.
    /// </summary>
    public class ByteUtil
    {
        /// <summary>
        /// Writes a UInt32 number to 4 bytes in a byte array.
        /// </summary>
        /// <param name="byteArray">Byte array to write to</param>
        /// <param name="begin">Where to start inserting</param>
        /// <param name="value">UInt32  number to insert</param>
        public static void InsertUInt32ToByteArray(byte[] byteArray, int begin, uint value)
        {
            byteArray[begin++] = (byte)(value >> 24);
            byteArray[begin++] = (byte)(value >> 16);
            byteArray[begin++] = (byte)(value >> 8);
            byteArray[begin] = (byte)value;
        }

        /// <summary>
        /// Get a UInt32 number from 4 bytes.
        /// </summary>
        /// <param name="byteArray">Byte array to get the number from</param>
        /// <param name="begin">Where to start reading</param>
        /// <returns></returns>
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
