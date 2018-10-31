using System;

namespace IndyDotNet.Wallet
{
    public static class Factory
    {
        public static IWallet GetWallet(WalletConfig config, WalletCredentials credentials)
        {
            IWallet wallet = new WalletInstance(config, credentials);

            return wallet;
        }
    }
}
