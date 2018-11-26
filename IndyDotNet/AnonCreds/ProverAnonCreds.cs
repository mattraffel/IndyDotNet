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

        public (ProverCredentialRequest, ProverCredentialRequestMetadata) CreateCredentialRequest(IDid prover, IssuerCredentialOffer claimOffer, IssuerCredentialDefinition credentialDefinition, string masterSecret)
        {
            string claimOfferJson = claimOffer.ToJson();
            string credDefJson = credentialDefinition.ToJson();

            //Logger.Info($"     claimOfferJson = {claimOfferJson}");
            //Logger.Info($"     credDefJson = {credDefJson}");
            //Logger.Info($"     masterSecret = {masterSecret}");

            ProverCreateCredentialRequestResult result = AnonCredsAsync.ProverCreateCredentialReqAsync(_wallet, prover, claimOfferJson, credDefJson, masterSecret).Result;

            ProverCredentialRequest request = JsonConvert.DeserializeObject<ProverCredentialRequest>(result.CredentialRequestJson);
            ProverCredentialRequestMetadata metadata = JsonConvert.DeserializeObject<ProverCredentialRequestMetadata>(result.CredentialRequestMetadataJson);

            return (request, metadata);
        }

        public string SaveCredential(IssuerCredential issuerCredential, IssuerCredentialDefinition credentialDefinition, ProverCredentialRequestMetadata credentialRequestMetadata, string requestedCredentialId = "")
        {
            string credentialJson = issuerCredential.ToJson();
            string credDefinitionJson = credentialDefinition.ToJson();
            string metadataJson = credentialRequestMetadata.ToJson();

            //Logger.Info($"\n ------- SaveCredential ------------- ");
            //Logger.Info($"\n     credentialJson = {credentialJson}");
            //Logger.Info($"\n credDefinitionJson = {credDefinitionJson}");
            //Logger.Info($"\n       metadataJson = {metadataJson}");
            //Logger.Info($"\n ------------------------------------ ");

            string credentialId = AnonCredsAsync.ProverStoreCredentialAsync(_wallet, requestedCredentialId, metadataJson, credentialJson, credDefinitionJson, null).Result;

            return credentialId;
        }
    }
}
