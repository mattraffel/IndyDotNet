using System;
using Newtonsoft.Json;

namespace IndyDotNet.Ledger
{
    /// <summary>
    /// {"reqId":1541788507858948000,"identifier":"V4SGRU86Z58d6TV7PBUe6f","operation":{"dest":"LnXR1rPnncTPZvRdmJKhJQ","type":"1"},"protocolVersion":2}
    /// </summary>
    public class BuildNymRequestResult
    {
        [JsonProperty("reqId")]
        public long ReqId { get; set; }
        /// <summary>
        /// in a Nym transaction, this will be the submitter DID
        /// </summary>
        public string Identifier { get; set; }
        public BuildNymRequestOperation Operation { get; set; }
        [JsonProperty("protocolVersion")]
        public int ProtocolVersion { get; set; }
    }

    public class BuildNymRequestOperation
    {
        /// <summary>
        /// In a nym transaction this will be the target DID
        /// </summary>
        public string Dest { get; set; }
        public string Type { get; set; }
    }
}
