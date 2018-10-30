using System;
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

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }
    }
}
