using System;
using IndyDotNet.Did;
using IndyDotNet.Wallet;

namespace IndyDotNet.AnonCreds
{
    public interface IIssuerAnonCreds
    {
        IssuerCredential CreateStoreCredentialDef(IDid issuerDid, CredentialDefinitionSchema definition);
        IssuerCredentialOffer CreateCredentialOffer(string credentialId);
    }

    public interface IProverAnonCreds
    {
        void CreateMasterSecret(string secret);
        ProverCredentialRequest CreateCredentialRequest(IDid prover, IssuerCredentialOffer claimOffer, IssuerCredential credentialDefinition, string masterSecret);
    }
}
