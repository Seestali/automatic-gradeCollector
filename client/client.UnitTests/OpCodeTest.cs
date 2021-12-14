using Xunit;
using client.Network;

namespace client.UnitTests
{
    public class OpCodeTest
    {
        //TODO: check Xunit for byte testing Equal = string
        //TODO: check for more test cases
        [Fact]
        public void Deny_DenyIsZero()
        {
            Assert.Equal<byte>(0,(byte)OpCode.Deny);
        }

        [Fact]
        public void Ack_AckIsOne()
        {
            Assert.Equal<byte>(1, (byte)OpCode.Ack);
        }

        [Fact]
        public void LoginReq_LoginReqIsTwo()
        {
            Assert.Equal<byte>(2,(byte)OpCode.LoginReq);
        }

        [Fact]
        public void LoginAns_LoginAnsIsThree()
        {
            Assert.Equal<byte>(3,(byte)OpCode.LoginAns);
        }

        [Fact]
        public void SubjectsAndGradesReq_SubjectsAndGradesReqIsFour()
        {
            Assert.Equal<byte>(4,(byte)OpCode.SubjectsAndGradesReq);
        }

        [Fact]
        public void SubjectsAndGradesAns_SubjectsAndGradesAnsIsFive()
        {
            Assert.Equal<byte>(5,(byte)OpCode.SubjectsAndGradesAns);
        }

        [Fact]
        public void SetGradesReq_SetGradesReqIsSix()
        {
            Assert.Equal<byte>(6,(byte)OpCode.SetGradesReq);
        }

        [Fact]
        public void SetGradesAns_SetGradesAnsIsSeven()
        {
            Assert.Equal<byte>(7,(byte)OpCode.SetGradesAns);
        }
    }
}
