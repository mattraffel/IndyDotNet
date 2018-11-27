using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IndyDotNet.Internal.Json
{
    public class RequestRestrictionsConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// output needs to look like this
        ///     "restrictions":{  
        ///         "issuer_did":"YWpmwLpTjxxieBPUxztnXo"
        ///     }
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="value">Value.</param>
        /// <param name="serializer">Serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var list = value as IndyDotNet.AnonCreds.RequestRestrictions;

            writer.WriteStartObject();
            writer.WritePropertyName("restrictions");
            writer.WriteStartObject();
            foreach (KeyValuePair<string, string> item in list)
            {
                writer.WritePropertyName(item.Key);
                writer.WriteValue(item.Value);
            }
            writer.WriteEndObject();
            writer.WriteEnd();
        }
    }
}
