using System;
using System.Collections.Generic;
using IndyDotNet.Pool;
using IndyDotNet.Utils;
using IndyDotNet.Wallet;

namespace IndyDotNet.Did
{
    public static class Factory
    {
        public static IDid GetDid(IPool pool, IWallet wallet)
        {
            return new DidInstance(pool, wallet);
        }

        public static List<IDid> GetAllDids(IPool pool, IWallet wallet)
        {
            // ListMyDidsWithMetaAsync

            List<IDid> dids = new List<IDid>();

            string didsJson = DidAsync.ListMyDidsWithMetaAsync(wallet).Result;

            Logger.Info($"*** {didsJson}");

            return dids;
        }

        public static IEndPoint GetEndPoint()
        {
            throw new NotImplementedException();
        }
    }
}
