using System;
using IndyDotNet.AnonCreds;
using Newtonsoft.Json;

namespace IndyDotNet.Internal.Json
{
    public class RequestedAttributesListConverter : JsonConverter
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
        ///         "requested_attributes": {
        ///              "attr1_referent": {
        ///                  "name": "name",
        ///                  "restrictions": {
        ///                      "issuer_did": "trustee_did",
        ///                      "schema_id": "schema_id"
        ///                  }
        ///              }
        ///         }
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="value">Value.</param>
        /// <param name="serializer">Serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var list = value as RequestedAttributesList;

            //writer.WriteStartObject();
            //writer.WritePropertyName("requested_attributes");
            writer.WriteStartObject();

            int count = 1;
            foreach(RequestedAttribute attribute in list)
            {
                string objectName = $"attr{count}_referent";
                writer.WritePropertyName(objectName);
                writer.WriteStartObject();
                writer.WritePropertyName("name");
                writer.WriteValue(attribute.Name);
                if (null != attribute.Restrictions)
                {
                    writer.WritePropertyName("restrictions");
                    serializer.Serialize(writer, attribute.Restrictions);
                }

                writer.WriteEndObject();
                count++;
            }
            writer.WriteEndObject();
            //writer.WriteEnd();
        }
    }
}
