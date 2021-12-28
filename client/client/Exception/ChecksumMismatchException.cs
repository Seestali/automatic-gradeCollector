namespace client.Exception
{
    public class ChecksumMismatchException : System.Exception
    {
        public ChecksumMismatchException()
        {
        }

        public ChecksumMismatchException(string message) : base(message)
        {
        }
    }
}
