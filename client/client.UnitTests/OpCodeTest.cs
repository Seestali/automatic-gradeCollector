using Xunit;
using client.Network.Packet;

namespace client.UnitTests
{
    public class OpCodeTest
    {
        [Fact]
        public void Deny_DenyIsZero()
        {
            Assert.Equal(OpCode.Deny, 0);
        }

        [Fact]
        public void Ack_AckIsOne()
        {
            Assert.Equal(OpCode.Ack, 1);
        }

        [Fact]
        public void LoginReq_LoginReqIsTwo()
        {
            Assert.Equal(OpCode.LoginReq, 2);
        }

        [Fact]
        public void LoginAns_LoginAnsIsThree()
        {
            Assert.Equal(OpCode.LoginAns, 3);
        }

        [Fact]
        public void SubjectsAndGradesReq_SubjectsAndGradesReqIsFour()
        {
            Assert.Equal(OpCode.SubjectsAndGradesReq, 4);
        }

        [Fact]
        public void SubjectsAndGradesAns_SubjectsAndGradesAnsIsFive()
        {
            Assert.Equal(OpCode.SubjectsAndGradesAns, 5);
        }

        [Fact]
        public void SetGradesReq_SetGradesReqIsSix()
        {
            Assert.Equal(OpCode.SetGradesReq, 6);
        }

        [Fact]
        public void SetGradesAns_SetGradesAnsIsSeven()
        {
            Assert.Equal(OpCode.Deny, 7);
        }
    }
}
