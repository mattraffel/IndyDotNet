using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IndyDotNet.AnonCreds
{
    /// <summary>
    /// TODO:  this is exactly the same as <seealso cref="ProverCredentialRequest"/>
    /// </summary>
    public class IssuerCredential
    {
        [JsonProperty("prover_did")]
        public string ProverDid { get; set; }

        [JsonProperty("cred_def_id")]
        public string CredDefId { get; set; }

        [JsonProperty("blinded_ms")]
        public IssuerBlindedMs BlindedMs { get; set; }

        [JsonProperty("blinded_ms_correctness_proof")]
        public IssuerBlindedMsCorrectnessProof BlindedMsCorrectnessProof { get; set; }

        [JsonProperty("nonce")]
        public string Nonce { get; set; }
    }

    public class IssuerBlindedMs
    {
        [JsonProperty("u")]
        public string U { get; set; }

        [JsonProperty("ur")]
        public string Ur { get; set; }

        [JsonProperty("hidden_attributes")]
        public string[] HiddenAttributes { get; set; }

        [JsonProperty("committed_attributes")]
        public IssuerCommittedAttributes CommittedAttributes { get; set; }
    }

    public class IssuerCommittedAttributes
    {
    }

    public class IssuerBlindedMsCorrectnessProof
    {
        [JsonProperty("c")]
        public string C { get; set; }

        [JsonProperty("v_dash_cap")]
        public string VDashCap { get; set; }

        [JsonProperty("m_caps")]
        public IssuerMCaps MCaps { get; set; }

        [JsonProperty("r_caps")]
        public IssuerCommittedAttributes RCaps { get; set; }
    }

    public class IssuerMCaps
    {
        [JsonProperty("master_secret")]
        public string MasterSecret { get; set; }
    }
}
