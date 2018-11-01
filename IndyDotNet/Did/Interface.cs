using System;
namespace IndyDotNet.Did
{
    public interface IDid
    {
        /// <summary>
        /// 
        /// </summary>
        string Did { get; }

        /// <summary>
        /// 
        /// </summary>
        string VerKey { get; }
    }

    public interface IEndPoint
    {

        /// <summary>
        /// 
        /// </summary>
        string Address { get; }

        /// <summary>
        /// Gets the transport verification key.
        /// </summary>
        string TransportKey { get; }
    }
}
