namespace client.Network
{
    /// <summary>
    /// Represents the op codes in the protocol.
    /// </summary>
    public enum OpCode : byte
    {
        Deny,
        Ack,
        LoginRequest,
        LoginAnswer,
        GetSubjectsAndGradesRequest,
        GetSubjectsAndGradesAnswer,
        SetGradesRequest,
        SetGradesAnswer
    }
}
