namespace client.Exception
{
    public class ChecksumMismatchException : System.Exception
    {
        private readonly uint packetNumber;
        
        public ChecksumMismatchException(uint packetNumber)
        {
            this.packetNumber = packetNumber;
        }

        public uint GetPacketNumber()
        {
            return packetNumber;
        }
    }
}
