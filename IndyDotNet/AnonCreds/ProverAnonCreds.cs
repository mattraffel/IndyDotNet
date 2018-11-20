using System;
using IndyDotNet.Wallet;

namespace IndyDotNet.AnonCreds
{
    internal class ProverAnonCreds : IProverAnonCreds
    {
        private IWallet _wallet;

        internal ProverAnonCreds(IWallet wallet) 
        {
            _wallet = wallet;
        }

        public void CreateMasterSecret(string secret)
        {
            AnonCredsAsync.ProverCreateMasterSecretAsync(_wallet, secret).Wait();
        }
    }
}
