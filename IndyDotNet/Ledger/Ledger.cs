using System;
using IndyDotNet.Did;
using IndyDotNet.Pool;
using IndyDotNet.Utils;
using IndyDotNet.Wallet;
using Newtonsoft.Json;

namespace IndyDotNet.Ledger
{
    internal class LedgerInstance : INymLedger
    {
        public LedgerInstance() {}

        #region internal Submit/Sign functions 
        private string SignRequest(IWallet wallet, IDid submitterDid, string requestJson)
        {
            return LedgerAsync.SignRequestAsync(wallet, submitterDid, requestJson).Result;
        }

        private string SignAndSubmitRequest(IPool pool, IWallet wallet, IDid submitterDid, string requestJson)
        {
            return LedgerAsync.SignAndSubmitRequestAsync(pool, wallet, submitterDid, requestJson).Result;
        }
        
        private string SubmitRequest(IPool pool, string requestJSon, string nodes, int timeout = -1)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region nym functions
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
            string json = SignRequest(wallet, submitterDid, nymRequestJson);
            Logger.Info($"SignRequestAsync returned: {json}");

            return JsonConvert.DeserializeObject<BuildNymRequestResult>(json);
        }

        public SignAndSubmitRequestResult SignAndSubmitRequest(IPool pool, IWallet wallet, IDid submitterDid, BuildNymRequestResult nymRequest)
        {
            string nymRequestJson = nymRequest.ToJson();
            Logger.Info($"BuildNymRequestResult as json: {nymRequestJson}");
            string json = SignAndSubmitRequest(pool, wallet, submitterDid, nymRequestJson);
            Logger.Info($"SignAndSubmitRequestAsync returned: {json}");

            return JsonConvert.DeserializeObject<SignAndSubmitRequestResult>(json);
        }

        public string SubmitRequest(IPool pool, BuildNymRequestResult nymRequest, string nodes, int timeout = -1)
        {
            string requestJson = nymRequest.ToJson();
            return SubmitRequest(pool, requestJson, nodes, timeout);
        }
        #endregion

    }
}
