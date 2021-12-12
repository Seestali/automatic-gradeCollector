namespace client.Network
{
    /// <summary>
    /// Represents a packet to sent or to receive.
    /// </summary>
    //TODO: finish comments
    //TODO: finish class if needed
    public class Packet
    {
        private readonly byte[] data;

        /// <summary>
        /// Create a packet.
        /// </summary>
        /// <param name="number">Packet number</param>
        /// <param name="userID">User ID</param>
        /// <param name="opCode">Op code</param>
        /// <param name="payloadLength">Length of payload data</param>
        /// <param name="payloadData">Payload data</param>
        public Packet(ushort number, byte userID, OpCode opCode, ushort payloadLength, byte[] payloadData)
        {
            byte[] content = new byte[6 + payloadLength];
            content[0] = (byte)number;
            content[1] = (byte)(number >> 8);
            content[2] = userID;
            content[3] = (byte)opCode;
            content[4] = (byte)payloadLength;
            content[5] = (byte)(payloadLength >> 8);
            for (ushort i = 0; i < payloadLength; i++)
            {
                content[6 + i] = payloadData[i];
            }
            uint crc = CRC32.calculateChecksum(ref content);

            data = new byte[10 + payloadLength];
            data[0] = content[0];
            data[1] = content[1];
            data[2] = content[2];
            data[3] = content[3];
            data[4] = (byte)crc;
            data[5] = (byte)crc;
            data[6] = (byte)crc;
            data[7] = (byte)crc;
            data[8] = content[4];
            data[9] = content[5];
            for (ushort i = 0; i < payloadLength; i++)
            {
                content[10 + i] = payloadData[i];
            }
        }

        /// <summary>
        /// Extracts the packet number from the packet byte array.
        /// </summary>
        /// <returns>Packet number</returns>
        public ushort GetNumber()
        {
            return (ushort)(data[0] + data[1] << 8);
        }

        /// <summary>
        /// Extracts the user ID from the packet byte array.
        /// </summary>
        /// <returns>User ID</returns>
        public byte GetUserID()
        {
            return data[2];
        }

        /// <summary>
        /// Extracts the op code from the packet byte array.
        /// </summary>
        /// <returns>Op code</returns>
        public OpCode GetOpCode()
        {
            return (OpCode)data[3];
        }

        /// <summary>
        /// Extracts the CRC32 checksum from the packet byte array.
        /// </summary>
        /// <returns>CRC32 checksum</returns>
        public uint GetCRC()
        {
            return (uint)(data[4] + data[5] << 8 + data[6] << 16 + data[7] << 24);
        }

        /// <summary>
        /// Extracts the length of the payload data from the packet byte array.
        /// </summary>
        /// <returns>Length of the Payload data</returns>
        public ushort GetPayLoadLength()
        {
            return (ushort)(data[8] + data[9] << 8);
        }

        /// <summary>
        /// Extracts the payload data from the packet byte array.
        /// </summary>
        /// <returns>Payload data</returns>
        public byte[] GetPayloadData()
        {
            byte[] payload = new byte[data.Length - 10];
            for (ushort i = 0; i < payload.Length; i++)
            {
                payload[i] = data[10 + i];
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
    }
}
