using System;
using IndyDotNet.Did;
using IndyDotNet.Utils;
using IndyDotNet.Wallet;
using Newtonsoft.Json;

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

        public ProverCredentialRequest CreateCredentialRequest(IDid prover, IssuerCredentialOffer claimOffer, IssuerCredential credentialDefinition, string masterSecret)
        {
            string claimOfferJson = claimOffer.ToJson();
            string credDefJson = credentialDefinition.ToJson();

            Logger.Info($"     claimOfferJson = {claimOfferJson}");
            Logger.Info($"     credDefJson = {credDefJson}");
            Logger.Info($"     masterSecret = {masterSecret}");

            ProverCreateCredentialRequestResult result = AnonCredsAsync.ProverCreateCredentialReqAsync(_wallet, prover, claimOfferJson, credDefJson, masterSecret).Result;

            return JsonConvert.DeserializeObject< ProverCredentialRequest>(result.CredentialRequestJson);
        }
    }
}
