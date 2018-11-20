using System;
using Newtonsoft.Json;

namespace IndyDotNet.AnonCreds
{
    public class ProverCredentialRequest
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

    public class BlindedMs
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

    public class CommittedAttributes
    {
    }

    public class BlindedMsCorrectnessProof
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

    public class MCaps
    {
        [JsonProperty("master_secret")]
        public string MasterSecret { get; set; }
    }
}
