using Xunit;
using client.Network;

namespace client.UnitTests
{
    public class OpCodeTest
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
        [InlineData(2,OpCode.LoginReq)]
        [InlineData(3,OpCode.LoginAns)]
        [InlineData(4,OpCode.SubjectsAndGradesReq)]
        [InlineData(5,OpCode.SubjectsAndGradesAns)]
        [InlineData(6,OpCode.SetGradesReq)]
        [InlineData(7,OpCode.SetGradesAns)]
        public void CheckOpCodes_OpCodesPass(byte expected, byte opCode)
        {
            Assert.Equal<byte>(expected,opCode);
        }
    }
}
