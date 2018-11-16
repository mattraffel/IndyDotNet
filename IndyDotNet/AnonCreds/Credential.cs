using System;
using System.Collections.Generic;
using IndyDotNet.Internal.Json;
using Newtonsoft.Json;

namespace IndyDotNet.AnonCreds
{
    public class Credential
    {
        public string Ver { get; set; }
        public string Id { get; set; }
        [JsonProperty("schemaId")]
        public string SchemaId { get; set; }
        public string Type { get; set; }
        public string Tag { get; set; }
        public CredentialValue Value { get; set; }
    }

    public class CredentialValue
    {
        [JsonProperty("primary")]
        public PrimaryValue Primary { get; set; }
        [JsonProperty("revocation")]
        public RevocationValue Revocation { get; set; }
    }

    /// <summary>
    /// json from LibIndy looks like this
    /// "{"master_secret":"stufstuff","item1":"value1","item2":"value2"}"
    /// 
    /// The json is a master key along with properties for each of the attributes
    /// in the credental schema (attrNames array).  We use the custom converter
    /// to build the Attributes property with the credential attributes.  
    /// This allows this class to work with any schema
    /// <seealso cref="CredentialRConverter"/>
    /// </summary>
    [JsonConverter(typeof(CredentialRConverter))]
    public class R
    {
        [JsonProperty("master_secret")]
        public string MasterSecret { get; set; }
        public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();
    }

    public class PrimaryValue
    {
        public string n { get; set; }
        public string s { get; set; }
        [JsonConverter(typeof(CredentialRConverter))] 
        public R r { get; set; }
        public string rctxt { get; set; }
        public string z { get; set; }
    }

    public class RevocationValue
    {
        public string g { get; set; }
        public string g_dash { get; set; }
        public string h { get; set; }
        public string h0 { get; set; }
        public string h1 { get; set; }
        public string h2 { get; set; }
        public string htilde { get; set; }
        public string h_cap { get; set; }
        public string u { get; set; }
        public string pk { get; set; }
        public string y { get; set; }
    }
}
