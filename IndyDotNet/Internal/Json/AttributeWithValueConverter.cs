using System;
using System.Collections.Generic;
using IndyDotNet.AnonCreds;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IndyDotNet.Internal.Json
{
    public class AttributeWithValueConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {

            JObject jsonObject = JObject.Load(reader);
            AttributeWithValue attribute = new AttributeWithValue();
            var properties = jsonObject.Properties();
            foreach (JProperty property in properties)
            {

            }

            return attribute;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var attributeWithValue = value as IndyDotNet.AnonCreds.AttributeWithValue;
            writer.WriteStartObject();

            writer.WritePropertyName(attributeWithValue.Name);
            serializer.Serialize(writer, attributeWithValue.Name);

            writer.WriteStartArray();
            

            writer.WriteEndArray();
            writer.WriteEnd();
        }
    }
}
