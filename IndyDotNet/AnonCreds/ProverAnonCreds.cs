using System;
using IndyDotNet.Did;
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

        public string CreateCredentialRequest(IDid prover, IssuerCredentialOffer claimOffer, IssuerCredential credentialDefinition, string masterSecret)
        {
            throw new NotImplementedException();
        }
    }
}
