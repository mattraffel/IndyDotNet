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
        /// <param name="did">Did</param>
        /// <param name="verkey">Verkey</param>
        /// <param name="metadata">Metadata</param>
        /// <param name="tempVerkey"></param>
        protected internal DidInstance(IPool pool, IWallet wallet, string did = "", string verkey = "", string metadata = "", string tempVerkey = "")
        {
            _wallet = wallet;
            _pool = pool;

            if (!string.IsNullOrEmpty(did)) Did = did;
            if (!string.IsNullOrEmpty(verkey)) VerKey = verkey;
            if (!string.IsNullOrEmpty(metadata)) Metadata = metadata;
            if (!string.IsNullOrEmpty(tempVerkey)) TempVerKey = tempVerkey;

            _isDirty = false;
        }

        public string Did { get; internal set; }
        public string VerKey { get; internal set; }
        public string TempVerKey { get; internal set; }
        public string Metadata { get; internal set; }

        public string GetAbbreviateVerkey()
        {
            return DidAsync.AbbreviateVerkeyAsync(Did, VerKey).Result;
        }

        public void Refresh(bool localOnly = false)
        {
            if (localOnly)
            {
                VerKey = DidAsync.KeyForLocalDidAsync(_wallet, Did).Result;
                return;
            }

            VerKey = DidAsync.KeyForDidAsync(_pool, _wallet, Did).Result;
        }
    }
}
