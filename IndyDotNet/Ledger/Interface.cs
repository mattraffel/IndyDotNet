using System;
using IndyDotNet.Did;

namespace IndyDotNet.Ledger
{
    public interface ILedger
    {
        BuildNymRequestResult BuildNymRequest(IDid submitterDid, IDid targetDid, string verKey, string alias, NymRoles role);
    }
}
