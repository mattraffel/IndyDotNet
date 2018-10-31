using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace IndyDotNet.Utils
{
    /// <summary>
    /// Functions to support serialization and deserialization of 
    /// JSON to POCO types
    /// </summary>
    public static class JsonSerialization
    {
        public static string ToJson<T>(this T instance, bool indentedFormatting = false)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            Formatting formattingStyle = (indentedFormatting ? Formatting.Indented : Formatting.None);


            return JsonConvert.SerializeObject(instance, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = formattingStyle
            });
        }
    }
}
