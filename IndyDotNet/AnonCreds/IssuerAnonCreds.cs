using System;
using IndyDotNet.BlobStorage;
using IndyDotNet.Did;
using IndyDotNet.Utils;
using IndyDotNet.Wallet;
using Newtonsoft.Json;

namespace IndyDotNet.AnonCreds
{
    internal class IssuerAnonCreds : IIssuerAnonCreds
    {
        private IWallet _wallet;

        internal IssuerAnonCreds(IWallet wallet)
        {
            _wallet = wallet;
        }

        public IssuerCredentialDefinition CreateStoreCredentialDef(IDid issuerDid, CredentialDefinitionSchema definition)
        {
            string schemaJson = definition.ToJson();
            string tag = definition.Tag;
            string configJson = definition.Config.ToJson();
            string signatureType = definition.SignatureType;

            //Logger.Info($"     schemaJson = {schemaJson}");
            //Logger.Info($"     configJson = {configJson}");
            //Logger.Info($"     tag = {tag}");
            //Logger.Info($"     signatureType = {signatureType}");

            IssuerCreateAndStoreCredentialDefResult result = AnonCredsAsync.IssuerCreateAndStoreCredentialDefAsync(_wallet, issuerDid, schemaJson, tag, signatureType, configJson).Result;

            return JsonConvert.DeserializeObject<IssuerCredentialDefinition>(result.CredDefJson);
        }

        public IssuerCredentialOffer CreateCredentialOffer(string credentialId)
        {
            string result = AnonCredsAsync.IssuerCreateCredentialOfferAsync(_wallet, credentialId).Result;

            return JsonConvert.DeserializeObject<IssuerCredentialOffer>(result);
        }

        public IssuerCredential CreateCredential(IssuerCredentialOffer claimOffer, ProverCredentialRequest request, AttributeValuesList attributeValues, string revcationId = null, IBlobStorageReader reader = null)
        {

            string claimOfferJson = claimOffer.ToJson();
            string credentialRequestJson = request.ToJson();
            string attributeValuesJson = attributeValues.ToJson();

            //Logger.Info($"\n     claimOfferJson = {claimOfferJson}");
            //Logger.Info($"\n     credentialRequestJson = {credentialRequestJson}");
            //Logger.Info($"\n     attributeValuesJson = {attributeValuesJson}");

            IssuerCreateCredentialResult credentialResult = AnonCredsAsync.IssuerCreateCredentialAsync(_wallet, claimOfferJson, credentialRequestJson, attributeValuesJson, revcationId, reader).Result;

            return JsonConvert.DeserializeObject<IssuerCredential>(credentialResult.CredentialJson);
        }
    }
}
