using client.Network;
using Xunit;

namespace client.UnitTests
{
    public class CRC32Tests
    {
        //TODO: add test cases for CRC32 function in CRC.cs
        //TODO: add test crctab for consistency
        [Fact]
        public void CalculateChecksum_testByte_CheckSumIsSame()
        {
            byte[] array = {0x12, 0x34, 0x56, 0x78, 0x90};
            Assert.Equal(3700649649, CRC32.CalculateChecksum(ref array));
        }
    }
}