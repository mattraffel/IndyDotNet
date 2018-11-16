using System;
using System.Collections.Generic;
using IndyDotNet.AnonCreds;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IndyDotNet.Internal.Json
{
    internal class CredentialRConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {

            JObject jsonObject = JObject.Load(reader);
            R credential = new R();
            var properties = jsonObject.Properties();
            foreach(JProperty property in properties)
            {
                if (property.Name == "master_secret")
                {
                    credential.MasterSecret = (string)property.Value;
                    continue;
                }

                string key = property.Name;
                string value = (string)property.Value;

                credential.Attributes.Add(key, value);
            }

            return credential;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var r = value as IndyDotNet.AnonCreds.R;
            writer.WriteStartObject();

            writer.WritePropertyName("master_secret");
            serializer.Serialize(writer, r.MasterSecret);

            foreach(KeyValuePair<string, string> item in r.Attributes)
            {
                writer.WritePropertyName(item.Key);
                serializer.Serialize(writer, item.Value);
            }
        }
    }
}
