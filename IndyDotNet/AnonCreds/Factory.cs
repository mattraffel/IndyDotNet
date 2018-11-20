using System;
using IndyDotNet.Wallet;

namespace IndyDotNet.AnonCreds
{
    public static class Factory
    {
        public static IIssuerAnonCreds GetIssuerAnonCreds(IWallet wallet)
        {
            return new IssuerAnonCreds(wallet);
        }

        public static IProverAnonCreds GetProverAnonCreds(IWallet wallet)
        {
            return new ProverAnonCreds(wallet);
        }
    }
}
