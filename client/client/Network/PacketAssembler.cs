using client.Utils;
using client.Exception;
using System.Text;

namespace client.Network
{
    /// <summary>
    /// 
    /// </summary>
    public class PacketAssembler
    {
        // TODO: Handle invalid packets (manage assembling class) 

        public const byte DENY_PAYLOAD_LENGTH = 5;
        public const byte ACK_PAYLOAD_LENGTH = 4;

        private uint packetNumber;
        private byte userID;

        public PacketAssembler()
        {
            packetNumber = 0;
            userID = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packetNumber"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public Packet BuildDeny(uint packetNumberToDeny, Error error)
        {
            byte[] payload = new byte[DENY_PAYLOAD_LENGTH];
            ByteUtil.InsertUInt32ToByteArray(payload, 0, packetNumberToDeny);
            payload[4] = (byte)error;
            return new Packet(GetPacketNumberAndInc(), userID, OpCode.Deny, payload);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packetNumber"></param>
        /// <returns></returns>
        public Packet BuildAck(uint packetNumberToAck)
        {
            byte[] payload = new byte[ACK_PAYLOAD_LENGTH];
            ByteUtil.InsertUInt32ToByteArray(payload, 0, packetNumberToAck);
            return new Packet(GetPacketNumberAndInc(), userID, OpCode.Ack, payload);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public Packet BuildLoginRequest(string email, string passwordHash)
        {
            string auth = email  + "::" +  passwordHash;
            byte[] payload = Encoding.UTF8.GetBytes(auth);
            return new Packet(GetPacketNumberAndInc(), userID, OpCode.LoginRequest, payload);
        }

        public Packet BuildGetSubjectsAndGradesRequest(string auth, int semester)
        {
            string payload = auth + semester;
            byte[] payloadData = Encoding.UTF8.GetBytes(payload);
            return new Packet(GetPacketNumberAndInc(), userID, OpCode.GetSubjectsAndGradesRequest, payloadData);
        }

        public Packet BuildSetGradesRequest(string auth/*, ... */)
        {
            /* ... */
            return new Packet(GetPacketNumberAndInc(), userID, OpCode.SetGradesRequest, null);
        }

        // TODO:    disassemble packets
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public Packet DisassemblePacket(byte[] data)
        {
            Packet packet = new Packet(data);
            byte[] content = packet.GetContentWithoutCRC();
            if (packet.GetCRC() != CRC32.CalculateChecksum(content))
            {
                throw new ChecksumMismatchException(packet.GetNumber());
            }
            if (userID == 0)    // If local user ID is still zero, take ID from received packet.
            {
                userID = packet.GetUserID();
            }
            return packet;
        }

        private uint GetPacketNumberAndInc()
        {
            return packetNumber++;
        }
    }
}
