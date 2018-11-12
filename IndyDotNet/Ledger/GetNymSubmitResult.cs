using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IndyDotNet.Ledger
{
    public partial class GetNymSubmitReponse
    {
        [JsonProperty("result")]
        public GetNymSubmitResult Result { get; set; }

        [JsonProperty("op")]
        public string Op { get; set; }
    }

    public partial class GetNymSubmitResult
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("state_proof")]
        public StateProof StateProof { get; set; }

        [JsonProperty("dest")]
        public string Dest { get; set; }

        [JsonProperty("reqId")]
        public double ReqId { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("seqNo")]
        public long SeqNo { get; set; }

        [JsonProperty("txnTime")]
        public long TxnTime { get; set; }
    }

    public partial class StateProof
    {
        [JsonProperty("proof_nodes")]
        public string ProofNodes { get; set; }

        [JsonProperty("multi_signature")]
        public MultiSignature MultiSignature { get; set; }

        [JsonProperty("root_hash")]
        public string RootHash { get; set; }
    }

    public partial class MultiSignature
    {
        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("value")]
        public MultiSignatureValue Value { get; set; }

        [JsonProperty("participants")]
        public List<string> Participants { get; set; }
    }

    public partial class MultiSignatureValue
    {
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("txn_root_hash")]
        public string TxnRootHash { get; set; }

        [JsonProperty("pool_state_root_hash")]
        public string PoolStateRootHash { get; set; }

        [JsonProperty("state_root_hash")]
        public string StateRootHash { get; set; }

        [JsonProperty("ledger_id")]
        public long LedgerId { get; set; }
    }
}
