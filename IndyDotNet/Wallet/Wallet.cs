using System;

using IndyDotNet.Utils;

namespace IndyDotNet.Wallet
{
    public class WalletInstance : IWallet
    {
        private WalletConfig _config;
        private WalletCredentials _credentials;

        protected internal WalletInstance(WalletConfig config, WalletCredentials credentials) 
        {
            _config = config;
            _credentials = credentials;
        }

        public void Close() {}

        public void Create()
        {
            string config = _config.ToJson();
            string credentials = _credentials.ToJson();


            Logger.Info($"config is {config}");
            Logger.Info($"credentials is {credentials}");

            WalletAsync.CreateWalletAsync(config, credentials).Wait();
        }

        public void Delete()
        {
            string config = _config.ToJson();
            string credentials = _credentials.ToJson();

            WalletAsync.DeleteWalletAsync(config, credentials).Wait();
        }

        public void Open()
        {
            string config = _config.ToJson();
            string credentials = _credentials.ToJson();
            WalletAsync.OpenWalletAsync(config, credentials).Wait();
        }
    }
}
