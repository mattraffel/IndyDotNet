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
    /// CreateAndStoreMyDidAsync - done
    /// GetMyDidWithMetaAsync - done
    /// ListMyDidsWithMetaAsync - done, pending changes for metadata
    /// AbbreviateVerkeyAsync - done
    /// StoreTheirDidAsync - done
    /// KeyForDidAsync  - done
    /// KeyForLocalDidAsync - done
    /// ReplaceKeysStartAsync - done
    /// ReplaceKeysApplyAsync - done
    /// 
    /// SetDidMetadataAsync
    /// GetDidMetadataAsync
    /// 
    /// TODO:  ReplaceKeys functions could be merged into a single function.
    /// there may be no need to require implementors to know the specifics
    /// of how a key is replaced
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

        string GetAbbreviateVerkey();
        void Refresh(bool localOnly = false);
        void ReplaceStart(IdentitySeed seed = null);
        void ReplaceApply();

    }

    /// <summary>
    /// End point.
    /// </summary>
    public interface IEndPoint
    {
        IDid Did { get; }
        /// <summary>
        /// 
        /// </summary>
        string Address { get; }

        /// <summary>
        /// Gets the transport verification key.
        /// </summary>
        string TransportKey { get; }

        /// <summary>
        /// maps to GetEndpointForDidAsync
        /// </summary>
        void Open();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address">Address.</param>
        /// <param name="transportKey">Transport key.</param>
        void Save(string address, string transportKey);
    }
}
