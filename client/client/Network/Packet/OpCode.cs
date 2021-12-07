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
        DENY,
        ACK,
        Login,
        ReqSubjects,
        ResSubjects,
        SetGrades
    }
}
