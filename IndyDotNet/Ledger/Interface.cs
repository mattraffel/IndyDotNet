using System;
using IndyDotNet.Did;
using IndyDotNet.Pool;
using IndyDotNet.Wallet;

namespace IndyDotNet.Ledger
{
    public interface ILedger
    {

        BuildNymRequestResult BuildNymRequest(IDid submitterDid, IDid targetDid, string verKey, string alias, NymRoles role);

        // TODO: refactor these methods, perhaps through generics and/or class abstractions.  
        // Here's the need: every build method produces json that needs to be signed and sent to ledger.  
        // however, since we dont want consumers to have to deal with json directly we will want to use defined 
        // types.  Really shouldn't need a sign function for each type in the interface
        #region sign/submit methods
        BuildNymRequestResult SignRequest(IWallet wallet, IDid submitterDid, BuildNymRequestResult nymRequest);
        SignAndSubmitRequestResult SignAndSubmitRequest(IPool pool, IWallet wallet, IDid submitterDid, BuildNymRequestResult nymRequest);
        string SubmitRequest(IPool pool, BuildNymRequestResult nymRequest, string nodes, int timeout = -1);
        #endregion
    }
}
