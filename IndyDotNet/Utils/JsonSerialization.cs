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
        public static string ToJson<T>(this T instance)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };

            return JsonConvert.SerializeObject(instance, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });
        }
    }
}
