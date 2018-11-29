using System;
using Newtonsoft.Json;

namespace IndyDotNet.BlobStorage
{
    public enum BlobStorageTypes
    {
        Default
    }

    public static class BlobStorageTypesToStringConverters
    {
        public static string AsString(this BlobStorageTypes storageType)
        {
            switch (storageType)
            {
                case BlobStorageTypes.Default:
                    return "default";
                default:
                    return null;
            }
        }
    }

    public class BlobStorageConfig
    {
        [JsonProperty("base_dir")]
        public string BaseDir { get; set; }
        [JsonProperty("uri_pattern")]
        public string UriPattern { get; set; } = string.Empty;    
    }
}
