using System;
using IndyDotNet.Did;
using IndyDotNet.Pool;
using IndyDotNet.Utils;
using IndyDotNet.Wallet;
using Newtonsoft.Json;

namespace IndyDotNet.Ledger
{
    /// <summary>
    /// contains common functionality shared by different ledger instances
    /// </summary>
    internal abstract class BaseLedgerInstance
    {
        #region internal Submit/Sign functions 
        protected string SignRequest(IWallet wallet, IDid submitterDid, string requestJson)
        {
            return LedgerAsync.SignRequestAsync(wallet, submitterDid, requestJson).Result;
        }

        protected string SignAndSubmitRequest(IPool pool, IWallet wallet, IDid submitterDid, string requestJson)
        {
            return LedgerAsync.SignAndSubmitRequestAsync(pool, wallet, submitterDid, requestJson).Result;
        }

        protected string SubmitRequest(IPool pool, string requestJson)
        {
            return LedgerAsync.SubmitRequestAsync(pool, requestJson).Result;
        }
        #endregion
    }

    /// <summary>
    /// Ledger instance.
    /// </summary>
    internal class LedgerInstance : BaseLedgerInstance, INymLedger, IDDOLedger
    {
        public LedgerInstance() { }

        #region nym functions
        public BuildRequestResult BuildRequest(IDid submitterDid, IDid targetDid, string verKey, string alias, NymRoles role)
        {
            string json = LedgerAsync.BuildNymRequestAsync(submitterDid, targetDid, verKey, alias, role.AsString()).Result;
            Logger.Info($"BuildNymRequestAsync returned: {json}");

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

        public SignAndSubmitRequestResponse SignAndSubmitRequest(IPool pool, IWallet wallet, IDid submitterDid, BuildRequestResult nymRequest)
        {
            string nymRequestJson = nymRequest.ToJson();
            Logger.Info($"BuildNymRequestResult as json: {nymRequestJson}");
            string json = SignAndSubmitRequest(pool, wallet, submitterDid, nymRequestJson);
            Logger.Info($"SignAndSubmitRequestAsync returned: {json}");

            return JsonConvert.DeserializeObject<SignAndSubmitRequestResponse>(json);
        }

        public SignAndSubmitRequestResponse SubmitRequest(IPool pool, BuildRequestResult nymRequest)
        {
            string requestJson = nymRequest.ToJson();
            Logger.Info($"BuildNymRequestResult as json: {requestJson}");
            string json = SubmitRequest(pool, requestJson);
            Logger.Info($"SubmitRequest as json: {json}");

            return JsonConvert.DeserializeObject<SignAndSubmitRequestResponse>(json);
        }
        #endregion

        #region DDO functions
        public BuildRequestResult BuildGetRequest(IDid submitterDid, IDid targetDid)
        {
            string json = LedgerAsync.BuildGetDdoRequestAsync(submitterDid, targetDid).Result;
            Logger.Info($"BuildGetDdoRequestAsync returned: {json}");

            return JsonConvert.DeserializeObject<BuildRequestResult>(json);
        }
        #endregion
    }

    /// <summary>
    /// API calls for GetNym functions
    /// </summary>
    internal class GetNymLedgerInstance : BaseLedgerInstance, IGetNymLedger
    {
        public BuildRequestResult BuildGetRequest(IDid submitterDid, IDid targetDid)
        {
            string json = LedgerAsync.BuildGetNymRequestAsync(submitterDid, targetDid).Result;
            Logger.Info($"BuildGetNymRequestAsync returned: {json}");

            return JsonConvert.DeserializeObject<BuildRequestResult>(json);
        }

        public GetNymSubmitReponse SubmitRequest(IPool pool, BuildRequestResult nymRequest)
        {
            string requestJson = nymRequest.ToJson();
            Logger.Info($"BuildNymRequestResult as json: {requestJson}");
            string json = SubmitRequest(pool, requestJson);
            Logger.Info($"SubmitRequest as json: {json}");

            return JsonConvert.DeserializeObject<GetNymSubmitReponse>(json);
        }
    }
}
