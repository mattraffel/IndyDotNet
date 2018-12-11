using System;

namespace IndyDotNet.Internal.OpenSSL
{
    public enum PrimeStatus : int
    {
        Generated = 0,
        Testing = 1,
        Found = 2,
    }

    public delegate void PrimeGenerator(PrimeStatus status, int i, IntPtr cb_arg);

    public class OpenSSLException : Exception
    {

        internal OpenSSLException() : base("Generic OpenSSL Exception") { }
        internal OpenSSLException(string message) : base(message) { }
        internal OpenSSLException(string message, Exception innerException) : base(message, innerException) { }
    }
}
