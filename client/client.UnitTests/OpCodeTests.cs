using Xunit;
using client.Network;

namespace client.UnitTests
{
    public class OpCodeTests
    {
        /// <summary>
        /// This Theory checks for correct return of opcode byte value
        /// 
        /// </summary>
        /// <param name="expected">Expected byte value for opcode</param>
        /// <param name="opCode">opcode for expected byte</param>
        [Theory]
        [InlineData(0,OpCode.Deny)]
        [InlineData(1,OpCode.Ack)]
        [InlineData(2,OpCode.LoginRequest)]
        [InlineData(3,OpCode.LoginAnswer)]
        [InlineData(4,OpCode.GetSubjectsAndGradesRequest)]
        [InlineData(5,OpCode.GetSubjectsAndGradesAnswer)]
        [InlineData(6,OpCode.SetGradesRequest)]
        [InlineData(7,OpCode.SetGradesAnswer)]
        public void CheckOpCodes_OpCodesPass(byte expected, OpCode opCode)
        {
            Assert.Equal(expected,(byte)opCode);
        }
    }
}
