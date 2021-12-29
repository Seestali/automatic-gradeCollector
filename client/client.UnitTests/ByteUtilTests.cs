using client.Utils;
using Xunit;

namespace client.UnitTests
{
    public class ByteUtilTests
    {
        [Fact]
        public void InsertUInt32ToByteArray_IntAtArrayBegin_Inplace()
        {
            int toInsert = 42069420;
            byte[] actual = new byte[8];
            byte[] expected = new byte[] { 0x02, 0x81, 0xED, 0xAC, 0, 0, 0, 0 };
            ByteUtil.InsertUInt32ToByteArray(ref actual, 0, toInsert);
            Assert.Equal(expected, array);
        }

        [Fact]
        public void InsertUInt32ToByteArray_IntAtIndex_Inplace()
        {
            int toInsert = 42069420;
            byte[] actual = new byte[8];
            byte[] expected = new byte[] { 0, 0, 0, 0, 0x02, 0x81, 0xED, 0xAC };
            ByteUtil.InsertUInt32ToByteArray(ref actual, 4, toInsert);
            Assert.Equal(expected, array);
        }

        [Fact]
        public void GetUInt32FromByteArray_IntAtArrayBegin_ReturnsCorrectInt()
        {
            byte[] array = new byte[] { 0x02, 0x81, 0xED, 0xAC, 0, 0, 0, 0 };
            Assert.Equal(42069420, ByteUtil.GetUInt32FromByteArray(ref array, 0));
        }

        [Fact]
        public void GetUInt32FromByteArray_IntAtIndex_ReturnsCorrectInt()
        {
            byte[] array = new byte[] { 0, 0, 0, 0, 0x02, 0x81, 0xED, 0xAC };
            Assert.Equal(42069420, ByteUtil.GetUInt32FromByteArray(ref array, 4));
        }
    }
}
