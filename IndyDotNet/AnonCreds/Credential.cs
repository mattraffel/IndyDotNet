using System;
using System.Collections.Generic;
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
        public Primary PrimaryValue { get; set; }
        [JsonProperty("revocation")]
        public Revocation RevocationValue { get; set; }
    }

    /// <summary>
    /// here's the problem with this class as currently defined
    /// age, height etc...are attributes given in the attributes
    /// CredentialDefinition 
    /// TODO:  serialization of this class will not work as designed
    /// </summary>
    public class R
    {
        [JsonProperty("master_secret")]
        public string MasterSecret { get; set; }
        public string age { get; set; }
        public string height { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
    }

    public class Primary
    {
        public string n { get; set; }
        public string s { get; set; }
        public R r { get; set; }
        public string rctxt { get; set; }
        public string z { get; set; }
    }

    public class Revocation
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
