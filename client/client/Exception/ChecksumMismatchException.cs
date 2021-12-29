namespace client.Exception
{
    public class ChecksumMismatchException : System.Exception
    {
        private uint packetnumber;
        public ChecksumMismatchException(uint packetNumber)
        {
            this.packetnumber = packetnumber;
        }

        public uint GetPacketNumber()
        {
            return packetnumber;
        }
    }
}
