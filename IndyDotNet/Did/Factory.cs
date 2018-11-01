using System;
using IndyDotNet.Pool;
using IndyDotNet.Wallet;

namespace IndyDotNet.Did
{
    public static class Factory
    {
        public static IDid GetDid(IPool pool, IWallet wallet)
        {
            return new DidInstance(pool, wallet);
        }

        public static IEndPoint GetEndPoint()
        {
            throw new NotImplementedException();
        }
    }
}
