using System;
using Newtonsoft.Json;

namespace IndyDotNet.Crypto
{
     // {"message":"This is the message we are sending","recipient_verkey":"GjZWsBLgZCR18aL468JAT7w9CZRiBnpxUPPgyQxh4voa","sender_verkey":"GjZWsBLgZCR18aL468JAT7w9CZRiBnpxUPPgyQxh4voa"}
    public class UnpackedMessage
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("recipient_verkey")]
        public string EncryptedRecipients { get; set; }
        [JsonProperty("sender_verkey")]
        public string SenderVerkey { get; set; }
    }
}
