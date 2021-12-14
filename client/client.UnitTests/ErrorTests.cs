using Xunit;
using client.Network;

namespace client.UnitTests
{
    public class ErrorTests
    {
        [Theory]
        [InlineData(0,Error.AuthFailed)] // Authentification failed
        [InlineData(1,Error.PayloadInvalid)] // Payload is invalid
        public void ErrorCode_IsEqual(byte expected, byte error)
        {
            Assert.Equal(expected,error);
        }
    }
}
