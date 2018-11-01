using System;
using IndyDotNet.Pool;
using IndyDotNet.Wallet;

namespace IndyDotNet.Did
{
    internal class DidInstance : IDid
    {
        private readonly IWallet _wallet;
        private readonly IPool _pool;

        protected internal DidInstance(IPool pool, IWallet wallet)
        {
            _wallet = wallet;
            _pool = pool;
        }

        public string Did { get; internal set; }
        public string VerKey { get; internal set; }
    }
}
