using System;
using IndyDotNet.Did;
using IndyDotNet.Pool;
using IndyDotNet.Utils;
using IndyDotNet.Wallet;
using Newtonsoft.Json;

namespace IndyDotNet.Ledger
{
    internal class LedgerInstance : ILedger
    {
        public LedgerInstance() {}

        public BuildNymRequestResult BuildNymRequest(IDid submitterDid, IDid targetDid, string verKey, string alias, NymRoles role)
        {
            string json = LedgerAsync.BuildNymRequestAsync(submitterDid, targetDid, verKey, alias, role.AsString()).Result;
            Logger.Info($"BuildNymRequestAsync returned: {json}");

            return JsonConvert.DeserializeObject<BuildNymRequestResult>(json); 
        }

        public BuildNymRequestResult SignRequest(IWallet wallet, IDid submitterDid, BuildNymRequestResult nymRequest)
        {
            string nymRequestJson = nymRequest.ToJson();
            Logger.Info($"BuildNymRequestResult as json: {nymRequestJson}");
            string json = LedgerAsync.SignRequestAsync(wallet, submitterDid, nymRequestJson).Result;
            Logger.Info($"SignRequestAsync returned: {json}");

            return JsonConvert.DeserializeObject<BuildNymRequestResult>(json);
        }

        public SignAndSubmitRequestResult SignAndSubmitRequest(IPool pool, IWallet wallet, IDid submitterDid, BuildNymRequestResult nymRequest)
        {
            string nymRequestJson = nymRequest.ToJson();
            Logger.Info($"BuildNymRequestResult as json: {nymRequestJson}");
            string json = LedgerAsync.SignAndSubmitRequestAsync(pool, wallet, submitterDid, nymRequestJson).Result;
            Logger.Info($"SignAndSubmitRequestAsync returned: {json}");

            return JsonConvert.DeserializeObject<SignAndSubmitRequestResult>(json);
        }

        public string Submit(IPool pool, BuildNymRequestResult nymRequest, string nodes, int timeout = -1)
        {


            throw new NotImplementedException();
        }
    }
}
