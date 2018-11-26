using System;
using System.Collections.Generic;
using IndyDotNet.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IndyDotNet.Internal.Json
{

    public class CredentialValuesListConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The json.</returns>
        /// <param name="reader">Reader.</param>
        /// <param name="objectType">Object type.</param>
        /// <param name="existingValue">Existing value.</param>
        /// <param name="serializer">Serializer.</param>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            AnonCreds.CredentialValuesList list = new AnonCreds.CredentialValuesList();
            JObject something = serializer.Deserialize(reader) as JObject;

            foreach(KeyValuePair<string, JToken> jsonItem in something)
            {
                AnonCreds.IssuerCredentialValue credentialValue = new AnonCreds.IssuerCredentialValue()
                {
                    Name = jsonItem.Key,
                    Raw = jsonItem.Value["raw"].ToString(),
                    Encoded = jsonItem.Value["encoded"].ToString()
                };

                list.Add(credentialValue);
            }

            return list;
        }

        /// <summary>
        /// output needs to look like this
        /// {"age":["28","28"],"height":["175","175"],"name":["Alex","99262857098057710338306967609588410025648622308394250666849665532448612202874"],"sex":["male","5944657099558967239210949258394887428692050081607692519917050011144233115103"]}
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="value">Value.</param>
        /// <param name="serializer">Serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var list = value as IndyDotNet.AnonCreds.CredentialValuesList;

            writer.WriteStartObject();

            foreach (AnonCreds.IssuerCredentialValue item in list)
            {
                writer.WritePropertyName(item.Name);

                writer.WriteStartObject();
                writer.WritePropertyName("raw");
                serializer.Serialize(writer, item.Raw);
                writer.WritePropertyName("encoded");
                serializer.Serialize(writer, item.Encoded);

                writer.WriteEndObject();
            }

            writer.WriteEnd();
        }
    }
}
