using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IndyDotNet.Ledger
{
    public class BuildSchemaResult
    {
        [JsonProperty("reqId")]
        public long ReqId { get; set; }
        [JsonProperty("identifier")]
        public string Identifier { get; set; }
        public SchemaResultOperation Operation { get; set; }
        [JsonProperty("protocolVersion")]
        public int ProtocolVersion { get; set; }
    }


    public class BuildSchemaResultData
    {
        public string Name { get; set; }
        public string Version { get; set; }
        [JsonProperty("attr_names")]
        public List<string> AttributeNames { get; set; }
    }

    public class SchemaResultOperation
    {
        [JsonProperty("type")]
        public string OperationType { get; set; }
        public BuildSchemaResultData Data { get; set; }
    }
}
