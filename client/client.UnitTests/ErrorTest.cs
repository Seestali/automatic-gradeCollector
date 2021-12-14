using Xunit;
using client.Network;

namespace client.UnitTests
{
    public class ErrorTest
    {
        [Fact]
        public void AuthFailed_AuthFailedIsZero()
        {
            Assert.Equal(0,(byte)OpCode.Deny);
        }

        [Fact]
        public void PayloadInvalid_PayloadInvalidIsOne()
        {
            Assert.Equal(1, (byte)OpCode.Ack);
        }
    }
}
