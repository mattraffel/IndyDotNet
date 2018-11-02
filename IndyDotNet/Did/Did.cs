using System;
using IndyDotNet.Pool;
using IndyDotNet.Utils;
using IndyDotNet.Wallet;
using Newtonsoft.Json.Linq;

namespace IndyDotNet.Did
{
    internal class DidInstance : IDid
    {
        private readonly IWallet _wallet;
        private readonly IPool _pool;
        private bool _isDirty = false;

        /// <summary>
        /// Initializes a new instance of DidInstance <see cref="T:IndyDotNet.Did.IDid"/> class.  
        /// 
        /// Use Factory <see cref="T:IndyDotNet.Did.Factory"/> to create the instance
        /// </summary>
        /// <param name="pool">Pool.</param>
        /// <param name="wallet">Wallet.</param>
        /// <param name="did">Did (optional)</param>
        /// <param name="verkey">Verkey (optional)</param>
        /// <param name="metadata">Metadata (optional)</param>
        protected internal DidInstance(IPool pool, IWallet wallet, string did = "", string verkey = "", string metadata = "")
        {
            _wallet = wallet;
            _pool = pool;

            if (!string.IsNullOrEmpty(did)) Did = did;
            if (!string.IsNullOrEmpty(verkey)) VerKey = verkey;
            if (!string.IsNullOrEmpty(metadata)) Metadata = metadata;

            _isDirty = true;
        }

        public string Did { get; internal set; }
        public string VerKey { get; internal set; }
        public string TempVerKey { get; internal set; }
        public string Metadata { get; internal set; }

        public string AbbreviatedVerKey()
        {
            return DidAsync.AbbreviateVerkeyAsync(Did, VerKey).Result;
        }

        public void Create(IdentitySeed seed)
        {
            CreateAndStoreMyDidResult result = DidAsync.CreateAndStoreMyDidAsync(_wallet, seed.ToJson()).Result;

            Did = result.Did;
            VerKey = result.VerKey;
            _isDirty = false;
        }

        /// <summary>
        /// loads did from the ledger using GetMyDidWithMetaAsync
        /// expectation is that Did property was set on creation
        /// </summary>
        public void Open()
        {
            string result = DidAsync.GetMyDidWithMetaAsync(_wallet, Did).Result;

            //   returned  {
            //     "did": string - DID stored in the wallet,
            //     "verkey": string - The DIDs transport key (ver key, key id),
            //     "tempVerkey": string - Temporary DIDs transport key (ver key, key id), exist only during the rotation of the keys.
            //                            After rotation is done, it becomes a new verkey.
            //     "metadata": string - The meta information stored with the DID
            //   }

            var json = JObject.Parse(result);
            Did = json["did"].Value<string>();
            VerKey = json["verkey"].Value<string>();
            TempVerKey = json["tempVerkey"].Value<string>();
            Metadata = json["metadata"].Value<string>();

            _isDirty = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Save()
        {
            if (false == _isDirty) return;

            _isDirty = false;

            JObject identity = new JObject();
            identity["did"] = Did;
            identity["verkey"] = VerKey;

            string json = identity.ToString();

            DidAsync.StoreTheirDidAsync(_wallet, json).Wait();
        }
    }
}
