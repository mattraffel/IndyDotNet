using System;
using System.Collections.Generic;
using IndyDotNet.BlobStorage;
using IndyDotNet.Did;
using IndyDotNet.Wallet;

namespace IndyDotNet.AnonCreds
{
    public interface IIssuerAnonCreds
    {
        IssuerCredentialDefinition CreateStoreCredentialDef(IDid issuerDid, CredentialDefinitionSchema definition);
        IssuerCredentialOffer CreateCredentialOffer(string credentialId);
        string CreateCredential(IssuerCredentialOffer claimOffer, ProverCredentialRequest request, AttributeValuesList attributeValues, string revcationId, IBlobStorageReader reader);
    }

    public interface IProverAnonCreds
    {
        void CreateMasterSecret(string secret);
        /// <summary>
        /// TODO:  what about the metadata json also returned in ProverCreateCredentialRequestResult result = AnonCredsAsync.ProverCreateCredentialReqAsync...
        /// </summary>
        ProverCredentialRequest CreateCredentialRequest(IDid prover, IssuerCredentialOffer claimOffer, IssuerCredentialDefinition credentialDefinition, string masterSecret);
    }
}
