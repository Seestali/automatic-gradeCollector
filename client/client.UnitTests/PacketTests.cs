using client.Network;
using System.Text;
using Xunit;

namespace client.UnitTests
{
    public class PacketTests
    {
        [Fact]
        public void ConversionCtor_ByteArray_Equal()
        {
            byte[] data = { 
                0x00, 0x00, 0x00, 0x00, // Packet number
                0x2A,                   // User ID
                0x01,                   // Op code
                0x6B, 0x3F, 0x58, 0x5D, // CRC32 checksum
                0x00, 0x00, 0x00, 0x04, // Payload length
                0x00, 0x00, 0x00, 0x00  // Payload data
            };
            Packet packet = new Packet(data);
            Assert.Equal(data, packet.ToByteArray());
        }

        [Fact]
        public void NormalCtor_Values_ValuesAreCorrect()
        {
            uint packetNumber = 69;
            byte userID = 42;
            OpCode opCode = OpCode.LoginReq;
            string auth = "max.mustermann@email.com::643F6BA68C9333859694078A905B90C4F036D01CF7E81D6EF0A6CA79A344B6B7";
            byte[] payload = Encoding.UTF8.GetBytes(auth);

            Packet packet = new Packet(packetNumber, userID, opCode, payload);
            Assert.Equal(packetNumber, packet.GetNumber());
            Assert.Equal(userID, packet.GetUserID());
            Assert.Equal(opCode, packet.GetOpCode());
            Assert.Equal((uint)payload.Length, packet.GetPayLoadLength());
            Assert.Equal(payload, packet.GetPayloadData());
        }

        [Fact]
        public void GetNumber_ReturnCorrectPacketNumber()
        {

        }

        [Fact]
        public void GetUserID_ReturnCorrectUserID()
        {

        }

        [Fact]
        public void GetOpCode_ReturnCorrectOpCode()
        {

        }

        [Fact]
        public void GetCRC_ReturnCorrectCRC()
        {

        }

        [Fact]
        public void GetPayloadLength_ReturnCorrectPayloadLength()
        {

        }

        [Fact]
        public void GetPayloadData_ReturnCorrectPayloadData()
        {
            byte[] data = {
                0x00, 0x00, 0x00, 0x00, // Packet number
                0x2A,                   // User ID
                0x01,                   // Op code
                0x6B, 0x3F, 0x58, 0x5D, // CRC32 checksum
                0x00, 0x00, 0x00, 0x04, // Payload length
                0x00, 0x00, 0x00, 0x00  // Payload data
            };
            Packet packet = new Packet(data);
            Assert.Equal(new byte[] { 0x00, 0x00, 0x00, 0x00 }, packet.GetPayloadData());
        }

        [Fact]
        public void ToByteArray_ReturnCorrectArray()
        {
            byte[] data = {
                0x00, 0x00, 0x00, 0x00, // Packet number
                0x2A,                   // User ID
                0x01,                   // Op code
                0x6B, 0x3F, 0x58, 0x5D, // CRC32 checksum
                0x00, 0x00, 0x00, 0x04, // Payload length
                0x00, 0x00, 0x00, 0x00  // Payload data
            };
            Packet packet = new Packet(data);
            Assert.Equal(data, packet.ToByteArray());
        }

        [Fact]
        public void GetContentWithoutCRC_ReturnCorrectContentArray()
        {
            byte[] data = {
                0x00, 0x00, 0x00, 0x00, // Packet number
                0x2A,                   // User ID
                0x01,                   // Op code
                0x6B, 0x3F, 0x58, 0x5D, // CRC32 checksum
                0x00, 0x00, 0x00, 0x04, // Payload length
                0x00, 0x00, 0x00, 0x00  // Payload data
            };
            Packet packet = new Packet(data);
            byte[] content = {
                0x00, 0x00, 0x00, 0x00, // Packet number
                0x2A,                   // User ID
                0x01,                   // Op code
                0x00, 0x00, 0x00, 0x04, // Payload length
                0x00, 0x00, 0x00, 0x00  // Payload data
            };
            Assert.Equal(content, packet.GetContentWithoutCRC());
        }
    }
}
