using System;
namespace IndyDotNet.Did
{
    /// <summary>
    /// Decentralized Identifier.  It is made of two parts.  The did, which is 
    /// like the public part of the key, and verkey.
    /// 
    /// There's two types of dids:  MyDid (aka its the Users DID) and TheirDid (which
    /// is other Dids the user has reference)
    /// 
    /// MyDid is saved to the wallet with CreateAndStoreMyDidAsync
    ///       and retrieved with GetMyDidWithMetaAsync
    /// 
    /// TheirDid is saved to the wallet with StoreTheirDidAsync
    ///       and retrieved with KeyForDidAsync or KeyForLocalDidAsync
    ///       along with GetDidMetadataAsync
    /// 
    /// development notes:
    /// CreateAndStoreMyDidAsync - done. Called Create
    /// ListMyDidsWithMetaAsync - done, pending changes for metadata
    /// AbbreviateVerkeyAsync - done
    /// GetMyDidWithMetaAsync - Called Open
    /// 
    /// ReplaceKeysStartAsync
    /// ReplaceKeysApplyAsync
    /// StoreTheirDidAsync - called save
    /// KeyForDidAsync - called refresh
    /// KeyForLocalDidAsync
    /// SetDidMetadataAsync
    /// GetDidMetadataAsync
    /// 
    /// 
    /// </summary>
    public interface IDid
    {
        /// <summary>
        /// public identifer
        /// </summary>
        string Did { get; }

        /// <summary>
        /// 
        /// </summary>
        string VerKey { get; }
        /// <summary>
        /// 
        /// </summary>
        string TempVerKey { get; }
        /// <summary>
        /// 
        /// </summary>
        string Metadata { get; }

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
        /// <summary>
        /// Open makes two calls: GetMyDidWithMetaAsync
        /// </summary>
        void Open();
        /// <summary>
        /// Save this instance.
        /// </summary>
        void Save();
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
