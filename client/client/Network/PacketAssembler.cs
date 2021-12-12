namespace client.Network
{
    /// <summary>
    /// 
    /// </summary>
    public class PacketAssembler
    {
        // TODO: Handle invalid packets (manage assembling class) 

        private ushort packetNumber;
        private byte userID;

        public PacketAssembler()
        {
            packetNumber = 0U;
            userID = 0;
        }

        // TODO:    assemble packets

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packetNumber"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public Packet AssembleDENY(ushort packetNumber, Error error)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="packetNumber"></param>
        /// <returns></returns>
        public Packet AssembleACK(ushort packetNumber)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public Packet AssembleLogin(string auth)
        {
            return null;
        }

        public Packet AssembleGetSubjectsAndGrades()
        {
            return null;
        }

        public Packet AssembleSetGrades()
        {
            return null;
        }

        // TODO:    disassemble packets
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void Disassemble(byte[] data)
        {

        }

        private uint GetPacketNumberAndInc()
        {
            return packetNumber++;
        }

        private void SetUserID(byte userID)
        {
            this.userID = userID;
        }
    }
}
