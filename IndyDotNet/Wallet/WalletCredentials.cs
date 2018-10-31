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
        public string Key { get; set; }
        public string StorageCredentials { get; set; }
        [JsonProperty("reKey")]
        public string Rekey { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public KeyDerivationMethod KeyDerivationMethod { get; set; } = KeyDerivationMethod.ARGON2I_MOD;
        [JsonConverter(typeof(StringEnumConverter))]
        public KeyDerivationMethod RekeyDerivationMethod { get; set; } = KeyDerivationMethod.ARGON2I_MOD;
    }
}
