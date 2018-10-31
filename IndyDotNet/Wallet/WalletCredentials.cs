using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IndyDotNet.Wallet
{

    public enum KeyDerivationMethod
    {
        RAW,
        ARGON2I_MOD,
        ARGON2I_INT
    }

    public class WalletCredentials
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        public string StorageCredentials { get; set; }
        [JsonProperty("reKey")]
        public string Rekey { get; set; }
        [JsonProperty("key_derivation_method")]
        [JsonConverter(typeof(StringEnumConverter))]
        public KeyDerivationMethod KeyDerivationMethod { get; set; } = KeyDerivationMethod.ARGON2I_MOD;
        //[JsonProperty("rekey_derivation_method")]
        //[JsonConverter(typeof(StringEnumConverter))]
        //public KeyDerivationMethod RekeyDerivationMethod { get; set; } = KeyDerivationMethod.ARGON2I_MOD;
    }
}
