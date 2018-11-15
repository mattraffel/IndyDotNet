using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IndyDotNet.Ledger
{
    public class SchemaDefinition
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Ver { get; set; } = "1.0";
        [JsonProperty("attrNames")]
        public List<string> AttributeNames { get; set; } = new List<string>();
    }
}
