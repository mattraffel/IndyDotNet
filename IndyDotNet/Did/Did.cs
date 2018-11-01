using System;
using IndyDotNet.Pool;
using IndyDotNet.Utils;
using IndyDotNet.Wallet;

namespace IndyDotNet.Did
{
    internal class DidInstance : IDid
    {
        private readonly IWallet _wallet;
        private readonly IPool _pool;

        protected internal DidInstance(IPool pool, IWallet wallet, string did = "", string verkey = "", string metadata = "")
        {
            _wallet = wallet;
            _pool = pool;

            if (!string.IsNullOrEmpty(did)) Did = did;
            if (!string.IsNullOrEmpty(verkey)) VerKey = verkey;
            if (!string.IsNullOrEmpty(metadata)) Metadata = metadata;
        }

        public string Did { get; internal set; }
        public string VerKey { get; internal set; }
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
        }
    }
}
