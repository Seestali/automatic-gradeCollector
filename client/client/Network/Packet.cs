using client.Utils;

namespace client.Network
{
    /// <summary>
    /// Represents a packet to sent or to receive.
    /// </summary>

    //TODO: finish class if needed
    public class Packet
    {
        public const byte HEADER_LENGTH = 14;
        public const byte CRC32_LENGTH = 4;
        public const byte BEFORE_CRC = HEADER_LENGTH - CRC32_LENGTH;
        public const byte NUMBER_BEGIN = 0;
        public const byte USER_ID_BEGIN = 4;
        public const byte OP_CODE_BEGIN = 5;
        public const byte CRC32_BEGIN = 6;
        public const byte PAYLOAD_LENGTH_BEGIN = 10;

        private readonly byte[] data;

        /// <summary>
        /// Ctor to create a packet out of the single contents.
        /// </summary>
        /// <param name="number">Packet number</param>
        /// <param name="userID">User ID</param>
        /// <param name="opCode">Op code</param>
        /// <param name="payloadLength">Length of payload data</param>
        /// <param name="payloadData">Payload data</param>
        public Packet(uint number, byte userID, OpCode opCode, byte[] payloadData)
        {
            byte[] content = new byte[BEFORE_CRC + payloadData.Length];
            ByteUtil.InsertUInt32ToByteArray(content, 0, number);
            content[4] = userID;
            content[5] = (byte)opCode;
            ByteUtil.InsertUInt32ToByteArray(content, 6, (uint)payloadData.Length);

            for (uint i = 0; i < payloadData.Length; i++)
                content[BEFORE_CRC + i] = payloadData[i];
            
            uint crc = CRC32.CalculateChecksum(content);

            data = new byte[HEADER_LENGTH + payloadData.Length];
            
            for (byte i = 0; i < CRC32_BEGIN; i++)
                data[i] = content[i];
            
            ByteUtil.InsertUInt32ToByteArray(data, 6, crc);
            
            for (byte i = BEFORE_CRC; i < HEADER_LENGTH; i++)
                data[i] = content[i - CRC32_LENGTH];
            
            for (ushort i = 0; i < payloadData.Length; i++)
                data[HEADER_LENGTH + i] = payloadData[i];
        }

        /// <summary>
        /// Conversion ctor to create a packet from a existing byte array.
        /// </summary>
        /// <param name="data"></param>
        public Packet(byte[] data)
        {
            this.data = data;
        }

        /// <summary>
        /// Extracts the packet number from the packet byte array.
        /// </summary>
        /// <returns>Packet number</returns>
        public uint GetNumber()
        {
            return ByteUtil.GetUInt32FromByteArray(data, NUMBER_BEGIN);
        }

        /// <summary>
        /// Extracts the user ID from the packet byte array.
        /// </summary>
        /// <returns>User ID</returns>
        public byte GetUserID()
        {
            return data[USER_ID_BEGIN];
        }

        /// <summary>
        /// Extracts the op code from the packet byte array.
        /// </summary>
        /// <returns>Op code</returns>
        public OpCode GetOpCode()
        {
            return (OpCode)data[OP_CODE_BEGIN];
        }

        /// <summary>
        /// Extracts the CRC32 checksum from the packet byte array.
        /// </summary>
        /// <returns>CRC32 checksum</returns>
        public uint GetCRC()
        {
            return ByteUtil.GetUInt32FromByteArray(data, CRC32_BEGIN);
        }

        /// <summary>
        /// Extracts the length of the payload data from the packet byte array.
        /// </summary>
        /// <returns>Length of the Payload data</returns>
        public uint GetPayLoadLength()
        {
            return ByteUtil.GetUInt32FromByteArray(data, PAYLOAD_LENGTH_BEGIN);
        }

        /// <summary>
        /// Extracts the payload data from the packet byte array.
        /// </summary>
        /// <returns>Payload data</returns>
        public byte[] GetPayloadData()
        {
            byte[] payload = new byte[data.Length - HEADER_LENGTH];
            for (uint i = 0; i < payload.Length; i++)
            {
                payload[i] = data[HEADER_LENGTH + i];
            }
            return payload;
        }

        /// <summary>
        /// Returns the whole packet represented as byte array.
        /// </summary>
        /// <returns>Whole packet</returns>
        public byte[] ToByteArray()
        {
            return data;
        }

        public byte[] GetContentWithoutCRC()
        {
            byte[] content = new byte[data.Length - CRC32_LENGTH];
            for (byte i = 0; i < CRC32_BEGIN; i++)
                content[i] = data[i];
            for (int i = CRC32_BEGIN + CRC32_LENGTH; i < data.Length; i++)
                content[i - CRC32_LENGTH] = data[i];
            return content;
        }
    }
}
