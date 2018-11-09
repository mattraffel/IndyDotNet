using System;
using IndyDotNet.Did;
using IndyDotNet.Pool;
using IndyDotNet.Wallet;

namespace IndyDotNet.Ledger
{
    public interface ILedger
    {
        BuildNymRequestResult BuildNymRequest(IDid submitterDid, IDid targetDid, string verKey, string alias, NymRoles role);
        BuildNymRequestResult SignRequest(IWallet wallet, IDid submitterDid, BuildNymRequestResult nymRequest);
        SignAndSubmitRequestResult SignAndSubmitRequest(IPool pool, IWallet wallet, IDid submitterDid, BuildNymRequestResult nymRequest);
        string SubmitRequest(IPool pool, BuildNymRequestResult nymRequest, string nodes, int timeout = -1);
    }
}
