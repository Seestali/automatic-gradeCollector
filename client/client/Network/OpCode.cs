namespace client.Network
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
