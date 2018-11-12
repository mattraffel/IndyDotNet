using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IndyDotNet.Ledger
{
    public class SignAndSubmitRequestResponse
    {
        public Result Result { get; set; }
        public string OP { get; set; }
    }

    public class Metadata
    {
        public long ReqId { get; set; }
        public string From { get; set; }
        public string Digest { get; set; }
    }

    public class Data
    {
        public string Dest { get; set; }
    }

    public class Txn
    {
        public Metadata Metadata { get; set; }
        [JsonProperty("data")]
        public Data TxnData { get; set; }
        [JsonProperty("type")]
        public string TxnType { get; set; }
        [JsonProperty("protocolVersion")]
        public int ProtocolVersion { get; set; }
    }

    public class Signature
    {
        [JsonProperty("value")]
        public string SignatureValue { get; set; }
        public string From { get; set; }
    }

    public class ReqSignature
    {
        [JsonProperty("type")]
        public string SignatureType { get; set; }
        [JsonProperty("values")]
        public List<Signature> Signatures { get; set; }
    }

    public class TxnMetadata
    {
        public int TxnTime { get; set; }
        public int SeqNo { get; set; }
        public string TxnId { get; set; }
    }

    public class Result
    {
        public List<string> AuditPath { get; set; }
        [JsonProperty("txn")]
        public Txn Transaction { get; set; }
        public ReqSignature ReqSignature { get; set; }
        [JsonProperty("ver")]
        public string Version { get; set; }
        public TxnMetadata TxnMetadata { get; set; }
        public string RootHash { get; set; }
    }

}
