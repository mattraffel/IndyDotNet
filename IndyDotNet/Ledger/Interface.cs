using System;
using IndyDotNet.Did;
using IndyDotNet.Pool;
using IndyDotNet.Wallet;

namespace IndyDotNet.Ledger
{
    /// <summary>
    /// Nym aka pseudonymous transactions
    /// </summary>
    public interface INymLedger
    {
        BuildRequestResult BuildRequest(IDid submitterDid, IDid targetDid, string verKey, string alias, NymRoles role);
        BuildRequestResult SignRequest(IWallet wallet, IDid submitterDid, BuildRequestResult nymRequest);
        SignAndSubmitRequestResponse SignAndSubmitRequest(IPool pool, IWallet wallet, IDid submitterDid, BuildRequestResult nymRequest);
        SignAndSubmitRequestResponse SubmitRequest(IPool pool, BuildRequestResult nymRequest);
    }

    public interface IGetNymLedger
    {
        BuildRequestResult BuildGetRequest(IDid submitterDid, IDid targetDid);
        GetNymSubmitReponse SubmitRequest(IPool pool, BuildRequestResult nymRequest);
    }

    public interface IDDOLedger
    {
        BuildRequestResult BuildGetRequest(IDid submitterDid, IDid targetDid);
        BuildRequestResult SignRequest(IWallet wallet, IDid submitterDid, BuildRequestResult nymRequest);
        SignAndSubmitRequestResponse SignAndSubmitRequest(IPool pool, IWallet wallet, IDid submitterDid, BuildRequestResult nymRequest);
        SignAndSubmitRequestResponse SubmitRequest(IPool pool, BuildRequestResult nymRequest);
    }

    public interface ISchemaLedger
    {
        BuildSchemaResult BuildSchemaRequest(IDid submitterDid, SchemaDefinition data);
        string BuildGetSchemaRequest(IDid submitterDid, string schemaId);
        string ParseGetSchemaResponse(string getSchemaResponse);

        SignAndSubmitRequestResponse SignAndSubmitRequest(IPool pool, IWallet wallet, IDid submitterDid, BuildSchemaResult nymRequest);
    }
}
