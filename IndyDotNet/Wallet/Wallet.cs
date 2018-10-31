using System;

using IndyDotNet.Utils;

namespace IndyDotNet.Wallet
{
    public class WalletInstance : IWallet
    {
        private WalletAsync _asyncHandle = null;
        private readonly WalletConfig _config;
        private readonly WalletCredentials _credentials;

        protected internal WalletInstance(WalletConfig config, WalletCredentials credentials) 
        {
            _config = config;
            _credentials = credentials;
        }

        public void Close() 
        {
            if (null == _asyncHandle) return;

            _asyncHandle.CloseAsync().Wait();
            _asyncHandle = null;
        }

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
            _asyncHandle = WalletAsync.OpenWalletAsync(config, credentials).Result;
        }
    }
}
