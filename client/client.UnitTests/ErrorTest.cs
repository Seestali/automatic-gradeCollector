using Xunit;
using client.Network.Packet;

namespace client.UnitTests
{
    public class ErrorTest
    {
        [Fact]
        public void AuthFailed_AuthFailedIsZero()
        {
            Assert.Equal(0,OpCode.Deny);
        }

        [Fact]
        public void PayloadInvalid_PayloadInvalidIsOne()
        {
            Assert.Equal(OpCode.Ack, 1);
        }
    }
}
