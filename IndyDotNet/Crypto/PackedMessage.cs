using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IndyDotNet.Crypto
{
    public class PackedMessage
    {
        [JsonProperty("protected", NullValueHandling = NullValueHandling.Ignore)]
        public Protected Protected { get; set; }

        [JsonProperty("iv", NullValueHandling = NullValueHandling.Ignore)]
        public string Iv { get; set; }

        [JsonProperty("ciphertext", NullValueHandling = NullValueHandling.Ignore)]
        public string Ciphertext { get; set; }

        [JsonProperty("tag", NullValueHandling = NullValueHandling.Ignore)]
        public string Tag { get; set; }
    }

    public class Protected
    {
        [JsonProperty("enc", NullValueHandling = NullValueHandling.Ignore)]
        public string Enc { get; set; }

        [JsonProperty("typ", NullValueHandling = NullValueHandling.Ignore)]
        public string Typ { get; set; }

        [JsonProperty("alg", NullValueHandling = NullValueHandling.Ignore)]
        public string Alg { get; set; }

        [JsonProperty("recipients", NullValueHandling = NullValueHandling.Ignore)]
        public List<Recipient> Recipients { get; set; }
    }

    public class Recipient
    {
        [JsonProperty("encrypted_key", NullValueHandling = NullValueHandling.Ignore)]
        public string EncryptedKey { get; set; }

        [JsonProperty("header", NullValueHandling = NullValueHandling.Ignore)]
        public Header Header { get; set; }
    }

    public class Header
    {
        [JsonProperty("kid", NullValueHandling = NullValueHandling.Ignore)]
        public string Kid { get; set; }

        [JsonProperty("sender", NullValueHandling = NullValueHandling.Ignore)]
        public string Sender { get; set; }

        [JsonProperty("iv", NullValueHandling = NullValueHandling.Ignore)]
        public string Iv { get; set; }
    }

}
