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

        /// <summary>
        /// Abbreviateds the ver key.
        /// Maps to AbbreviateVerkeyAsync
        /// </summary>
        /// <returns>The ver key.</returns>
        string AbbreviatedVerKey();
        /// <summary>
        /// Maps to CreateAndStoreMyDidAsync
        /// </summary>
        void Create(IdentitySeed seed);
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
