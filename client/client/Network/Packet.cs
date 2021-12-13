﻿using client.Utils;

namespace client.Network
{
    /// <summary>
    /// Represents a packet to sent or to receive.
    /// </summary>
    //TODO: finish comments
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

        private byte[] data;

        /// <summary>
        /// Ctor to create a packet out of the single contents.
        /// </summary>
        /// <param name="number">Packet number</param>
        /// <param name="userID">User ID</param>
        /// <param name="opCode">Op code</param>
        /// <param name="payloadLength">Length of payload data</param>
        /// <param name="payloadData">Payload data</param>
        public Packet(uint number, byte userID, OpCode opCode, uint payloadLength, byte[] payloadData)
        {
            byte[] content = new byte[BEFORE_CRC + payloadLength];
            ByteUtil.InsertUInt32ToByteArray(ref content, 0, number);
            //content[0] = (byte)(number >> 24);
            //content[1] = (byte)(number >> 16);
            //content[2] = (byte)(number >> 8);
            //content[3] = (byte)number;
            content[4] = userID;
            content[5] = (byte)opCode;
            ByteUtil.InsertUInt32ToByteArray(ref content, 6, payloadLength);
            //content[6] = (byte)(payloadLength >> 24);
            //content[7] = (byte)(payloadLength >> 16);
            //content[8] = (byte)(payloadLength >> 8);
            //content[9] = (byte)payloadLength;

            for (uint i = 0; i < payloadLength; i++)
                content[BEFORE_CRC + i] = payloadData[i];
            
            uint crc = CRC32.calculateChecksum(ref content);

            data = new byte[HEADER_LENGTH + payloadLength];
            
            for (byte i = 0; i < 6; i++)
                data[i] = content[i];
            
            ByteUtil.InsertUInt32ToByteArray(ref data, 6, crc);
            //data[6] = (byte)(crc >> 24);
            //data[7] = (byte)(crc >> 16);
            //data[8] = (byte)(crc >> 8);
            //data[9] = (byte)crc;
            
            for (byte i = 10; i < 14; i++)
                data[i] = content[i];
            
            for (ushort i = 0; i < payloadLength; i++)
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
            return ByteUtil.GetUInt32FromByteArray(ref data, NUMBER_BEGIN);
            //return (uint)(data[0] << 24 + data[1] << 16 + data[2] << 8 + data[3]);
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
            return ByteUtil.GetUInt32FromByteArray(ref data, CRC32_BEGIN);
            //return (uint)(data[6] << 24 + data[7] << 16 + data[8] << 8 + data[9]);
        }

        /// <summary>
        /// Extracts the length of the payload data from the packet byte array.
        /// </summary>
        /// <returns>Length of the Payload data</returns>
        public uint GetPayLoadLength()
        {
            return ByteUtil.GetUInt32FromByteArray(ref data, PAYLOAD_LENGTH_BEGIN)
            //return (uint)(data[10] << 24 + data[11] << 16 + data[12] << 8 + data[13]);
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
    }
}
