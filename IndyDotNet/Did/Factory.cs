using System;
using System.Collections.Generic;
using IndyDotNet.Pool;
using IndyDotNet.Utils;
using IndyDotNet.Wallet;
using Newtonsoft.Json.Linq;

namespace IndyDotNet.Did
{
    public static class Factory
    {

        #region IMetaDataFactory
        /// <summary>
        /// Gets or sets the meta data factory.
        /// </summary>
        /// <value>The meta data factory.</value>
        // public static IMetaDataFactory MetaDataFactory { get; set; } 

        /// <summary>
        /// Initializes the <see cref="T:IndyDotNet.Did.Factory"/> class.
        /// </summary>
        //static Factory()
        //{
        //    MetaDataFactory = new DefaultStringMetaDataFactory();
        //}
        #endregion

        #region TheirDid Handlers
        /// <summary>
        /// Gets their did.
        /// </summary>
        /// <returns>The their did.</returns>
        /// <param name="pool">Pool.</param>
        /// <param name="wallet">Wallet.</param>
        /// <param name="did">Did.</param>
        public static IDid GetTheirDid(IPool pool, IWallet wallet, string did)
        {
            // if truly needed for optimzation purposes we could parallel these two tasks
            string verkey = DidAsync.KeyForDidAsync(pool, wallet, did).Result;
            string metaData = string.Empty;

            try
            {
                metaData = DidAsync.GetDidMetadataAsync(wallet, did).Result;
            }
            catch (System.AggregateException aex)
            {
                Logger.Info($"metadata error, likely there is no metadata {aex.Message}");
            }

            return new DidInstance(pool, wallet, did, verkey, metaData);
        }

        /// <summary>
        /// Creates their did.
        /// </summary>
        /// <returns>The their did.</returns>
        /// <param name="pool">Pool.</param>
        /// <param name="wallet">Wallet.</param>
        /// <param name="did">Did.</param>
        public static IDid CreateTheirDid(IPool pool, IWallet wallet, string did, string verkey = "")
        {
            var identityJson = new JObject();
            identityJson["did"] = did;
            if (!string.IsNullOrEmpty(verkey)) identityJson["verkey"] = verkey;

            DidAsync.StoreTheirDidAsync(wallet, identityJson.ToString()).Wait();

            return Factory.GetTheirDid(pool, wallet, did);
        }
        #endregion

        #region MyDid Handlers
        /// <summary>
        /// Loads MyDid from ledger
        /// TODO: replace string metadata with IMetadata and use "injected" factory for IMetadata
        /// </summary>
        /// <returns>The did.</returns>
        /// <param name="pool">Pool.</param>
        /// <param name="wallet">Wallet.</param>
        /// <param name="did">Did.</param>
        public static IDid GetMyDid(IPool pool, IWallet wallet, string did)
        {
            string result = DidAsync.GetMyDidWithMetaAsync(wallet, did).Result;

            //   returned  {
            //     "did": string - DID stored in the wallet,
            //     "verkey": string - The DIDs transport key (ver key, key id),
            //     "tempVerkey": string - Temporary DIDs transport key (ver key, key id), exist only during the rotation of the keys.
            //                            After rotation is done, it becomes a new verkey.
            //     "metadata": string - The meta information stored with the DID
            //   }

            var json = JObject.Parse(result);
            did = json["did"].Value<string>();
            string verkey = json["verkey"].Value<string>();
            string tempVerkey = json["tempVerkey"].Value<string>();
            string metadata = json["metadata"].Value<string>();

            return new DidInstance(pool, wallet, did, verkey, metadata, tempVerkey);
        }

        /// <summary>
        /// Creates my did.
        /// </summary>
        /// <returns>The my did.</returns>
        /// <param name="pool">Pool.</param>
        /// <param name="wallet">Wallet.</param>
        /// <param name="seed">Seed.</param>
        public static IDid CreateMyDid(IPool pool, IWallet wallet, IdentitySeed seed)
        {
            // LibIndy doesnt follow rules of json.  an empty seed value is 
            // communicated by "{}".  TODO:  override ToJson for IdentitySeed
            string seedJson = (null != seed ? seed.ToJson() : "{}");
            CreateAndStoreMyDidResult result = DidAsync.CreateAndStoreMyDidAsync(wallet, seedJson).Result;

            return new DidInstance(pool, wallet, result.Did, result.VerKey, string.Empty);
        }

        /// <summary>
        /// TODO: this method ignores tempVerkey from ListMyDidsWithMetaAsync result
        /// </summary>
        /// <returns>The all dids.</returns>
        /// <param name="pool">Pool.</param>
        /// <param name="wallet">Wallet.</param>
        public static List<IDid> GetAllMyDids(IPool pool, IWallet wallet)
        {
            // ListMyDidsWithMetaAsync

            List<IDid> dids = new List<IDid>();

            string didsJson = DidAsync.ListMyDidsWithMetaAsync(wallet).Result;

            /*
                [{"did":"VsKV7grR1BUE29mG2Fm2kX","verkey":"GjZWsBLgZCR18aL468JAT7w9CZRiBnpxUPPgyQxh4voa","tempVerkey":null,"metadata":null}]
             */

            var didList = JArray.Parse(didsJson);

            foreach (var didObject in didList)
            {
                string did = didObject["did"].Value<string>();
                string metadata = didObject["metadata"].Value<string>();
                string verkey = didObject["verkey"].Value<string>();

                DidInstance didType = new DidInstance(pool, wallet, did, verkey, metadata);
                dids.Add(didType);
            }

            return dids;
        }
        #endregion

        #region Endpoint Handlers
        public static IEndPoint GetEndPoint(IPool pool, IWallet wallet, IDid did)
        {
            IEndPoint endPoint = new EndpointInstance(pool, wallet, did);

            return endPoint;
        }
        #endregion
    }
}
