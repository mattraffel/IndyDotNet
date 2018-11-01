using System;

namespace IndyDotNet.Did
{
    /// <summary>
    /// Result of creating and storing my DID.
    /// </summary>
    internal sealed class CreateAndStoreMyDidResult
    {
        /// <summary>
        /// Initializes a new CreateAndStoreMyDidResult.
        /// </summary>
        /// <param name="did">The DID created.</param>
        /// <param name="verKey">The verification key to use for verifying signatures.</param>
        internal CreateAndStoreMyDidResult(string did, string verKey)
        {
            Did = did ?? throw new ArgumentNullException("did");
            VerKey = verKey ?? throw new ArgumentNullException("verKey");
        }

        /// <summary>
        /// Gets the DID.
        /// </summary>
        public string Did { get; }

        /// <summary>
        /// Gets the verification key.
        /// </summary>
        public string VerKey { get; }
    }

    /// <summary>
    /// Result of getting the endpoint for a DID.
    /// </summary>
    internal class EndpointForDidResult
    {
        /// <summary>
        /// Initializes a new EndpointForDidResult.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="transportKey">The transport verification key.</param>
        internal EndpointForDidResult(string address, string transportKey)
        {
            Address = address;
            TransportKey = transportKey;
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// Gets the transport verification key.
        /// </summary>
        public string TransportKey { get; private set; }
    }
}
