using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IndyDotNet.AnonCreds
{

    public partial class IssuerCredential
    {
        [JsonProperty("prover_did")]
        public string ProverDid { get; set; }

        [JsonProperty("cred_def_id")]
        public string CredDefId { get; set; }

        [JsonProperty("blinded_ms")]
        public BlindedMs BlindedMs { get; set; }

        [JsonProperty("blinded_ms_correctness_proof")]
        public BlindedMsCorrectnessProof BlindedMsCorrectnessProof { get; set; }

        [JsonProperty("nonce")]
        public string Nonce { get; set; }
    }

    public partial class BlindedMs
    {
        [JsonProperty("u")]
        public string U { get; set; }

        [JsonProperty("ur")]
        public string Ur { get; set; }

        [JsonProperty("hidden_attributes")]
        public string[] HiddenAttributes { get; set; }

        [JsonProperty("committed_attributes")]
        public CommittedAttributes CommittedAttributes { get; set; }
    }

    public partial class CommittedAttributes
    {
    }

    public partial class BlindedMsCorrectnessProof
    {
        [JsonProperty("c")]
        public string C { get; set; }

        [JsonProperty("v_dash_cap")]
        public string VDashCap { get; set; }

        [JsonProperty("m_caps")]
        public MCaps MCaps { get; set; }

        [JsonProperty("r_caps")]
        public CommittedAttributes RCaps { get; set; }
    }

    public partial class MCaps
    {
        [JsonProperty("master_secret")]
        public string MasterSecret { get; set; }
    }
}
