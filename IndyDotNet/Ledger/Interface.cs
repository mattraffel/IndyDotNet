using System;
using IndyDotNet.Did;
using IndyDotNet.Pool;
using IndyDotNet.Wallet;

namespace IndyDotNet.Ledger
{
    public interface INymLedger
    {
        BuildNymRequestResult BuildNymRequest(IDid submitterDid, IDid targetDid, string verKey, string alias, NymRoles role);
        BuildNymRequestResult SignRequest(IWallet wallet, IDid submitterDid, BuildNymRequestResult nymRequest);
        SignAndSubmitRequestResult SignAndSubmitRequest(IPool pool, IWallet wallet, IDid submitterDid, BuildNymRequestResult nymRequest);
        SignAndSubmitRequestResult SubmitRequest(IPool pool, BuildNymRequestResult nymRequest);
    }
}
