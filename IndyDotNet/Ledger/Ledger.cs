using System;
using IndyDotNet.Did;
using Newtonsoft.Json;

namespace IndyDotNet.Ledger
{
    internal class LedgerInstance : ILedger
    {
        public LedgerInstance() {}

        public BuildNymRequestResult BuildNymRequest(IDid submitterDid, IDid targetDid, string verKey, string alias, NymRoles role)
        {
            string nymJson = LedgerAsync.BuildNymRequestAsync(submitterDid, targetDid, verKey, alias, role.AsString()).Result;
            BuildNymRequestResult result = JsonConvert.DeserializeObject<BuildNymRequestResult>(nymJson);

            return result;
        }
    }
}
