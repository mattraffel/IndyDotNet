using System;
using System.Collections.Generic;
using IndyDotNet.BlobStorage;
using IndyDotNet.Did;
using IndyDotNet.Wallet;

namespace IndyDotNet.AnonCreds
{
    public interface IIssuerAnonCreds
    {
        IssuerCredential CreateStoreCredentialDef(IDid issuerDid, CredentialDefinitionSchema definition);
        IssuerCredentialOffer CreateCredentialOffer(string credentialId);
        void CreateCredential(IssuerCredentialOffer claimOffer, ProverCredentialRequest request, AttributeValuesList attributeValues, object revcationId, IBlobStorageReader reader);
    }

    public interface IProverAnonCreds
    {
        void CreateMasterSecret(string secret);
        /// <summary>
        /// TODO:  what about the metadata json also returned in ProverCreateCredentialRequestResult result = AnonCredsAsync.ProverCreateCredentialReqAsync...
        /// </summary>
        ProverCredentialRequest CreateCredentialRequest(IDid prover, IssuerCredentialOffer claimOffer, IssuerCredential credentialDefinition, string masterSecret);
    }
}
