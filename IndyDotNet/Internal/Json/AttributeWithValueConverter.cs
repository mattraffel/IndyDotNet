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
            throw new NotImplementedException();
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
            var attributeWithValue = value as IndyDotNet.AnonCreds.AttributeWithValue;

            writer.WritePropertyName(attributeWithValue.Name);

            writer.WriteStartArray();

            serializer.Serialize(writer, attributeWithValue.Value);
            serializer.Serialize(writer, attributeWithValue.CheckValue);

            writer.WriteEndArray();
        }
    }

    public class AttributeValuesListConverter : JsonConverter
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
        /// {"age":["28","28"],"height":["175","175"],"name":["Alex","99262857098057710338306967609588410025648622308394250666849665532448612202874"],"sex":["male","5944657099558967239210949258394887428692050081607692519917050011144233115103"]}
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="value">Value.</param>
        /// <param name="serializer">Serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var list = value as IndyDotNet.AnonCreds.AttributeValuesList;

            writer.WriteStartObject();

            foreach (AttributeWithValue item in list)
            {
                writer.WritePropertyName(item.Name);

                writer.WriteStartArray();

                serializer.Serialize(writer, item.Value);
                serializer.Serialize(writer, item.CheckValue);

                writer.WriteEndArray();
            }

            writer.WriteEnd();
        }
    }
}
