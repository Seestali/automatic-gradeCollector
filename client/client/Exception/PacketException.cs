namespace client.Exception
{
    public class PacketException : System.Exception
    {
        private readonly uint packetNumber;
        
        public PacketException(uint packetNumber)
        {
            this.packetNumber = packetNumber;
        }

        public uint GetPacketNumber()
        {
            return packetNumber;
        }
    }
}
