using client.Network;
using Xunit;

namespace client.UnitTests
{
    public class PacketTest
    {
        [Fact]
        public void CreatePacket_FromByteArray_CorrectPacket()
        {
            byte[] data = { 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x03, 0x03, 0x03, 0x03, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00 };
            //Packet packet = new Packet()
        }
    }
}
