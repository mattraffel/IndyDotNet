using System;
using IndyDotNet.Did;
using IndyDotNet.Wallet;

namespace IndyDotNet.AnonCreds
{
    public interface IIssuerAnonCreds
    {
        string CreateStoreCredentialDef(IWallet wallet, IDid issuerDid, CredentialDefinition definition);
    }

    public interface IProverAnonCreds
    {

    }
}
