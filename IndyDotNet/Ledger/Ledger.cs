using System;
using IndyDotNet.Did;
using IndyDotNet.Pool;
using IndyDotNet.Utils;
using IndyDotNet.Wallet;
using Newtonsoft.Json;

namespace IndyDotNet.Ledger
{
    internal class LedgerInstance : INymLedger, IDDOLedger
    {
        public LedgerInstance() { }

        #region internal Submit/Sign functions 
        private string SignRequest(IWallet wallet, IDid submitterDid, string requestJson)
        {
            return LedgerAsync.SignRequestAsync(wallet, submitterDid, requestJson).Result;
        }

        private string SignAndSubmitRequest(IPool pool, IWallet wallet, IDid submitterDid, string requestJson)
        {
            return LedgerAsync.SignAndSubmitRequestAsync(pool, wallet, submitterDid, requestJson).Result;
        }

        private string SubmitRequest(IPool pool, string requestJson)
        {
            return LedgerAsync.SubmitRequestAsync(pool, requestJson).Result;
        }
        #endregion

        #region nym functions
        public BuildRequestResult BuildNymRequest(IDid submitterDid, IDid targetDid, string verKey, string alias, NymRoles role)
        {
            string json = LedgerAsync.BuildNymRequestAsync(submitterDid, targetDid, verKey, alias, role.AsString()).Result;
            Logger.Info($"BuildNymRequestAsync returned: {json}");

            return JsonConvert.DeserializeObject<BuildRequestResult>(json);
        }

        public BuildRequestResult BuildGetNymRequest(IDid submitterDid, IDid targetDid)
        {
            string json = LedgerAsync.BuildGetNymRequestAsync(submitterDid, targetDid).Result;
            Logger.Info($"BuildGetNymRequestAsync returned: {json}");

            return JsonConvert.DeserializeObject<BuildRequestResult>(json);
        }

        public BuildRequestResult SignRequest(IWallet wallet, IDid submitterDid, BuildRequestResult nymRequest)
        {
            string nymRequestJson = nymRequest.ToJson();
            Logger.Info($"BuildNymRequestResult as json: {nymRequestJson}");
            string json = SignRequest(wallet, submitterDid, nymRequestJson);
            Logger.Info($"SignRequestAsync returned: {json}");

            return JsonConvert.DeserializeObject<BuildRequestResult>(json);
        }

        public SignAndSubmitRequestResult SignAndSubmitRequest(IPool pool, IWallet wallet, IDid submitterDid, BuildRequestResult nymRequest)
        {
            string nymRequestJson = nymRequest.ToJson();
            Logger.Info($"BuildNymRequestResult as json: {nymRequestJson}");
            string json = SignAndSubmitRequest(pool, wallet, submitterDid, nymRequestJson);
            Logger.Info($"SignAndSubmitRequestAsync returned: {json}");

            return JsonConvert.DeserializeObject<SignAndSubmitRequestResult>(json);
        }

        public SignAndSubmitRequestResult SubmitRequest(IPool pool, BuildRequestResult nymRequest)
        {
            string requestJson = nymRequest.ToJson();
            Logger.Info($"BuildNymRequestResult as json: {requestJson}");
            string json = SubmitRequest(pool, requestJson);
            Logger.Info($"SubmitRequest as json: {json}");

            return JsonConvert.DeserializeObject<SignAndSubmitRequestResult>(json);
        }
        #endregion

        #region DDO functions
        public BuildRequestResult BuildGetDdoRequest(IDid submitterDid, IDid targetDid)
        {
            string json = LedgerAsync.BuildGetDdoRequestAsync(submitterDid, targetDid).Result;
            Logger.Info($"BuildGetDdoRequestAsync returned: {json}");

            return JsonConvert.DeserializeObject<BuildRequestResult>(json);
        }
        #endregion

    }
}
