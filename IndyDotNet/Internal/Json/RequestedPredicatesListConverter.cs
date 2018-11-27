using System;
using IndyDotNet.AnonCreds;
using Newtonsoft.Json;

namespace IndyDotNet.Internal.Json
{
    public class RequestedPredicatesListConverter : JsonConverter
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
        /// 
        /// </summary>
        /// <param name="writer">Writer.</param>
        /// <param name="value">Value.</param>
        /// <param name="serializer">Serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var list = value as RequestedPredicatesList;

            writer.WriteStartObject();

            int count = 1;
            foreach (RequestedPredicate predicate in list)
            {
                string objectName = $"predicate{count}_referent";
                writer.WritePropertyName(objectName);
                writer.WriteStartObject();
                writer.WritePropertyName("name");
                writer.WriteValue(predicate.Name);

                writer.WritePropertyName("p_type");
                writer.WriteValue(predicate.PType);

                writer.WritePropertyName("p_value");
                writer.WriteValue(predicate.PValue);

                if (null != predicate.Restrictions)
                {
                    writer.WritePropertyName("restrictions");
                    serializer.Serialize(writer, predicate.Restrictions);
                }

                writer.WriteEndObject();
                count++;
            }
            writer.WriteEndObject();
        }
    }
}
