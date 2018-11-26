using System;
using Newtonsoft.Json;

namespace IndyDotNet.AnonCreds
{
    public class ProverCredentialRequestMetadata
    {
        [JsonProperty("master_secret_blinding_data")]
        public MasterSecretBlindingData MasterSecretBlindingData { get; set; }

        [JsonProperty("nonce")]
        public string Nonce { get; set; }

        [JsonProperty("master_secret_name")]
        public string MasterSecretName { get; set; }
    }

    public class MasterSecretBlindingData
    {
        [JsonProperty("v_prime")]
        public string VPrime { get; set; }

        [JsonProperty("vr_prime")]
        public string VrPrime { get; set; }
    }
}
