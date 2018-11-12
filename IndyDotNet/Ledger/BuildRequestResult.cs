using System;
using Newtonsoft.Json;

namespace IndyDotNet.Ledger
{
    /// <summary>
    /// Build Nym transactions
    /// {"reqId":1541788507858948000,"identifier":"V4SGRU86Z58d6TV7PBUe6f","operation":{"dest":"LnXR1rPnncTPZvRdmJKhJQ","type":"1"},"protocolVersion":2}
    /// {"identifier":"V4SGRU86Z58d6TV7PBUe6f","operation":{"dest":"LnXR1rPnncTPZvRdmJKhJQ","type":"1"},"protocolVersion":2,"reqId":1541797143633427000,"signature":"2bZo86WCY19oxser7Mrt1FrNvhHNw7YYeWhkcZDhePHTN3NuDwKAhZos622ahZxNrwkLtx9LoS8cYx5Dmw2q6V6J"}
    /// 
    /// Get Nym transaction
    /// {"reqId":1542057303906932000,"identifier":"V4SGRU86Z58d6TV7PBUe6f","operation":{"type":"105","dest":"LnXR1rPnncTPZvRdmJKhJQ"},"protocolVersion":2}
    /// 
    /// DDO transactions
    /// {"reqId":1542054081577347000,"identifier":"V4SGRU86Z58d6TV7PBUe6f","operation":{"type":"120","dest":"LnXR1rPnncTPZvRdmJKhJQ"},"protocolVersion":2}
    /// </summary>
    public class BuildRequestResult
    {
        [JsonProperty("reqId")]
        public long ReqId { get; set; }
        /// <summary>
        /// in a Nym transaction, Identifier will be the submitter DID
        /// </summary>
        public string Identifier { get; set; }
        public BuildRequestOperation Operation { get; set; }
        [JsonProperty("signature")]
        public string Signature { get; set; }
        [JsonProperty("protocolVersion")]
        public int ProtocolVersion { get; set; }
    }

    public class BuildRequestOperation
    {
        /// <summary>
        /// In a nym transactions, Dest will be the target DID
        /// </summary>
        public string Dest { get; set; }
        public string Type { get; set; }
    }

    public enum RequestOperationTypes
    {
        BuildNym = 1,
        GetNym = 105,
        DDO = 120
    }
}
