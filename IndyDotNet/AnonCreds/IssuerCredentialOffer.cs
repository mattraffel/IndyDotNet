using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IndyDotNet.AnonCreds
{
    public class IssuerCredentialOffer
    {
        [JsonProperty("schema_id")]
        public string SchemaId { get; set; }
        [JsonProperty("cred_def_id")]
        public string CredDefId { get; set; }
        [JsonProperty("key_correctness_proof")]
        public KeyCorrectnessProof KeyCorrectnessProof { get; set; }
        public string Nonce { get; set; }
    }

    public class KeyCorrectnessProof
    {
        [JsonProperty("c")]
        public string C { get; set; }
        [JsonProperty("xz_cap")]
        public string XZCap { get; set; }
        [JsonProperty("xr_cap")]
        public List<List<string>> XRCap { get; set; }
    }
}
