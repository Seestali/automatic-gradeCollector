namespace client.Exception
{
    public class PayloadInvalidException : PacketException
    {
        public PayloadInvalidException(uint packetNumber) : base(packetNumber)
        {
        }
    }
}
