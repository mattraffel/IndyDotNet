using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IndyDotNet.AnonCreds
{
    /// <summary>
    /// TODO: can these be merged
    /// <seealso cref="IndyDotNet.Ledger.SchemaDefinition"/>
    /// </summary>
    public class CredentialDefinition
    {
        [JsonProperty("seqNo")]
        public int SequenceNo { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Ver { get; set; } = "1.0";

        [JsonProperty("attrNames")]
        public List<string> AttributeNames { get; set; } = new List<string>();

        [JsonIgnore]
        public string SignatureType { get; set; } = "CL";

        /// <summary>
        /// there is a bug in LibIndy, if this is passed as null or empty string
        /// it throws a CommonInvalidParam error despite documentation stating
        /// its optional
        /// </summary>
        /// <value>The tag.</value>
        [JsonIgnore]
        public string Tag { get; set; }

        [JsonIgnore]
        public CredentialDefinitionConfig Config { get; set; } = new CredentialDefinitionConfig();
    }

    public class CredentialDefinitionConfig
    {
        public bool SupportRevocation { get; set; } = true;
    }
}
