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
        BuildRequestResult BuildNymRequest(IDid submitterDid, IDid targetDid, string verKey, string alias, NymRoles role);
        BuildRequestResult BuildGetNymRequest(IDid submitterDid, IDid targetDid);
        BuildRequestResult SignRequest(IWallet wallet, IDid submitterDid, BuildRequestResult nymRequest);
        SignAndSubmitRequestResult SignAndSubmitRequest(IPool pool, IWallet wallet, IDid submitterDid, BuildRequestResult nymRequest);
        SignAndSubmitRequestResult SubmitRequest(IPool pool, BuildRequestResult nymRequest);
    }

    public interface IDDOLedger
    {
        BuildRequestResult BuildGetDdoRequest(IDid submitterDid, IDid targetDid);
        BuildRequestResult SignRequest(IWallet wallet, IDid submitterDid, BuildRequestResult nymRequest);
        SignAndSubmitRequestResult SignAndSubmitRequest(IPool pool, IWallet wallet, IDid submitterDid, BuildRequestResult nymRequest);
        SignAndSubmitRequestResult SubmitRequest(IPool pool, BuildRequestResult nymRequest);
    }
}
