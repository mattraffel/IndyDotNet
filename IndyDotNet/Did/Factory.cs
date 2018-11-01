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
        /// <summary>
        /// replace string metadata with IMetadata and use "injected" factory for IMetadata
        /// </summary>
        /// <returns>The did.</returns>
        /// <param name="pool">Pool.</param>
        /// <param name="wallet">Wallet.</param>
        /// <param name="did">Did.</param>
        /// <param name="verkey">Verkey.</param>
        /// <param name="metadata">Metadata.</param>
        public static IDid GetDid(IPool pool, IWallet wallet, string did = "", string verkey = "", string metadata = "")
        {
            return new DidInstance(pool, wallet, did, verkey, metadata);
        }

        /// <summary>
        /// TODO: this method ignores tempVerkey from ListMyDidsWithMetaAsync result
        /// </summary>
        /// <returns>The all dids.</returns>
        /// <param name="pool">Pool.</param>
        /// <param name="wallet">Wallet.</param>
        public static List<IDid> GetAllDids(IPool pool, IWallet wallet)
        {
            // ListMyDidsWithMetaAsync

            List<IDid> dids = new List<IDid>();

            string didsJson = DidAsync.ListMyDidsWithMetaAsync(wallet).Result;

            /*
                [{"did":"VsKV7grR1BUE29mG2Fm2kX","verkey":"GjZWsBLgZCR18aL468JAT7w9CZRiBnpxUPPgyQxh4voa","tempVerkey":null,"metadata":null}]
             */

            var didList = JArray.Parse(didsJson);

            foreach(var didObject in didList)
            {
                string did = didObject["did"].Value<string>();
                string metaData = didObject["metadata"].Value<string>();
                string verKey = didObject["verkey"].Value<string>();

                IDid didType = Factory.GetDid(pool, wallet, did, verKey, metaData);
                dids.Add(didType);
            }

            return dids;
        }

        public static IEndPoint GetEndPoint()
        {
            throw new NotImplementedException();
        }
    }
}
