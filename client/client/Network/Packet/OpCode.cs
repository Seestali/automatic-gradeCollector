﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Network.Packet
{
    enum OpCode
    {
        DENY,
        ACK,
        Login,
        ReqSubjects,
        ResSubjects,
        SetGrades
    }
}