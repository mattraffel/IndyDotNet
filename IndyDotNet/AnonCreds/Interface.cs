using System;
using IndyDotNet.Did;
using IndyDotNet.Wallet;

namespace IndyDotNet.AnonCreds
{
    public interface IIssuerAnonCreds
    {
        Credential CreateStoreCredentialDef(IDid issuerDid, CredentialDefinition definition);
    }

    public interface IProverAnonCreds
    {
        void CreateMasterSecret(string secret);
    }
}
