namespace client.Network
{
    /// <summary>
    /// Represents the error codes in the protocol.
    /// </summary>
    public enum Error : byte
    {
        AuthFailed,
        PayloadInvalid
    }
}