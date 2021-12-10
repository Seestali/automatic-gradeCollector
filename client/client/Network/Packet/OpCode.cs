using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Network.Packet
{
    /// <summary>
    /// Represents the op codes in the protocol.
    /// </summary>
    public enum OpCode : byte
    {
        Deny,
        Ack,
        LoginReq,
        LoginAns,
        SubjectsAndGradesReq,
        SubjectsAndGradesAns,
        SetGradesReq,
        SetGradesAns
    }
}
