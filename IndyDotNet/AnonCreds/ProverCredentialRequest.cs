using System;
using Newtonsoft.Json;

namespace IndyDotNet.AnonCreds
{
    /// <summary>
    /// TODO: this is exactly the same as <seealso cref="IssuerCredential"/>
    /// </summary>
    public class ProverCredentialRequest
    {
        [JsonProperty("prover_did")]
        public string ProverDid { get; set; }

        [JsonProperty("cred_def_id")]
        public string CredDefId { get; set; }

        [JsonProperty("blinded_ms")]
        public ProverBlindedMs BlindedMs { get; set; }

        [JsonProperty("blinded_ms_correctness_proof")]
        public ProverBlindedMsCorrectnessProof BlindedMsCorrectnessProof { get; set; }

        [JsonProperty("nonce")]
        public string Nonce { get; set; }
    }

    public class ProverBlindedMs
    {
        [JsonProperty("u")]
        public string U { get; set; }

        [JsonProperty("ur")]
        public string Ur { get; set; }

        [JsonProperty("hidden_attributes")]
        public string[] HiddenAttributes { get; set; }

        [JsonProperty("committed_attributes")]
        public ProverCommittedAttributes CommittedAttributes { get; set; }
    }

    public class ProverCommittedAttributes
    {
    }

    public class ProverBlindedMsCorrectnessProof
    {
        [JsonProperty("c")]
        public string C { get; set; }

        [JsonProperty("v_dash_cap")]
        public string VDashCap { get; set; }

        [JsonProperty("m_caps")]
        public ProverMCaps MCaps { get; set; }

        [JsonProperty("r_caps")]
        public ProverCommittedAttributes RCaps { get; set; }
    }

    public class ProverMCaps
    {
        [JsonProperty("master_secret")]
        public string MasterSecret { get; set; }
    }
}
