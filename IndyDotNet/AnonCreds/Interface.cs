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
        IssuerCredential CreateCredential(IssuerCredentialOffer claimOffer, ProverCredentialRequest request, AttributeValuesList attributeValues, string revcationId = null, IBlobStorageReader reader = null);
    }

    public interface IProverAnonCreds
    {
        void CreateMasterSecret(string secret);
        (ProverCredentialRequest, ProverCredentialRequestMetadata) CreateCredentialRequest(IDid prover, IssuerCredentialOffer claimOffer, IssuerCredentialDefinition credentialDefinition, string masterSecret);
        string SaveCredential(IssuerCredential issuerCredential, IssuerCredentialDefinition credentialDefinition, ProverCredentialRequestMetadata credentialRequestMetadata, string requestedCredentialId = "");
    }
}
