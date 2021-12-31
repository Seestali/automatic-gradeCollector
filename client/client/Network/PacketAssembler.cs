using client.Utils;
using client.Exception;
using System.Text;

namespace client.Network
{
    /// <summary>
    /// Assembles and disassembles packets due to the communication protocol.
    /// </summary>
    public class PacketAssembler
    {
        public const byte DENY_PAYLOAD_LENGTH = 5;
        public const byte ACK_PAYLOAD_LENGTH = 4;

        private uint packetNumber;
        private byte userID;

        /// <summary>
        /// Ctor
        /// </summary>
        public PacketAssembler()
        {
            packetNumber = 0;
            userID = 0;
        }

        /// <summary>
        /// Builds a deny packet
        /// </summary>
        /// <param name="packetNumber">Packet to deny</param>
        /// <param name="error">Error code</param>
        /// <returns>returns a packet that contains only a deny of another packet</returns>
        public Packet BuildDeny(uint packetNumberToDeny, Error error)
        {
            byte[] payload = new byte[DENY_PAYLOAD_LENGTH];
            ByteUtil.InsertUInt32ToByteArray(payload, 0, packetNumberToDeny);
            payload[4] = (byte)error;
            return new Packet(GetPacketNumberAndInc(), userID, OpCode.Deny, payload);
        }

        /// <summary>
        /// Builds an acknowledgement packet.
        /// </summary>
        /// <param name="packetNumber">Packet to acknowledge</param>
        /// <returns>returns a packet to acknowledge another packet(number)</returns>
        public Packet BuildAck(uint packetNumberToAck)
        {
            byte[] payload = new byte[ACK_PAYLOAD_LENGTH];
            ByteUtil.InsertUInt32ToByteArray(payload, 0, packetNumberToAck);
            return new Packet(GetPacketNumberAndInc(), userID, OpCode.Ack, payload);
        }

        /// <summary>
        /// Builds the login request for the login window with credentials
        /// </summary>
        /// <param name="email">Email in readable format</param>
        /// <param name="passwordHash">password in hash format</param>
        /// <returns>returns the packet that is send to the server as a new login request</returns>
        public Packet BuildLoginRequest(string email, string passwordHash)
        {
            string auth = email  + "::" +  passwordHash;
            byte[] payload = Encoding.UTF8.GetBytes(auth);
            return new Packet(GetPacketNumberAndInc(), userID, OpCode.LoginRequest, payload);
        }

        /// <summary>
        /// Builds the the packet to request the subjects and grades for a student in a semester.
        /// </summary>
        /// <param name="auth">Is a combined string of email::password(hash)</param>
        /// <param name="semester">is an integer of the dedicated semester</param>
        /// <returns>Returns a packet to request information about the a semester of a student</returns>
        public Packet BuildGetSubjectsAndGradesRequest(string auth, int semester)
        {
            //TODO:Fix function
            string payload = auth + "::" + semester;
            byte[] payloadData = Encoding.UTF8.GetBytes(payload);
            return new Packet(GetPacketNumberAndInc(), userID, OpCode.GetSubjectsAndGradesRequest, payloadData);
        }
        
        /// <summary>
        /// Builds a packet to set the grades for the modified subjects
        /// </summary>
        /// <param name="auth">identification with email::password</param>
        /// <returns>returns the packet with the used ID, the request to change the grades and the grades itself</returns>
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
