namespace client.Exception
{
    public class ChecksumMismatchException : PacketException
    {        
        public ChecksumMismatchException(uint packetNumber) : base(packetNumber)
        {
        }
    }
}
