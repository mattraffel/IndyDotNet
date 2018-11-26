using System;
using System.Collections.Generic;

using System.Globalization;
using IndyDotNet.Internal.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IndyDotNet.AnonCreds
{
    /// <summary>
    /// 
    /// </summary>
    public class IssuerCredential
    {
        [JsonProperty("schema_id")]
        public string SchemaId { get; set; }

        [JsonProperty("cred_def_id")]
        public string CredDefId { get; set; }

        [JsonProperty("rev_reg_id")]
        public object RevRegId { get; set; }

        [JsonProperty("values")]
        public CredentialValuesList Values { get; set; }

        [JsonProperty("signature")]
        public Signature Signature { get; set; }

        [JsonProperty("signature_correctness_proof")]
        public SignatureCorrectnessProof SignatureCorrectnessProof { get; set; }

        [JsonProperty("rev_reg")]
        public object RevReg { get; set; }

        [JsonProperty("witness")]
        public object Witness { get; set; }
    }

    public class Signature
    {
        [JsonProperty("p_credential")]
        public PCredential PCredential { get; set; }

        [JsonProperty("r_credential")]
        public object RCredential { get; set; }
    }

    public class PCredential
    {
        [JsonProperty("m_2")]
        public string M2 { get; set; }

        [JsonProperty("a")]
        public string A { get; set; }

        [JsonProperty("e")]
        public string E { get; set; }

        [JsonProperty("v")]
        public string V { get; set; }
    }

    public class SignatureCorrectnessProof
    {
        [JsonProperty("se")]
        public string Se { get; set; }

        [JsonProperty("c")]
        public string C { get; set; }
    }

    /// <summary>
    /// json from LibIndy looks like this
    /// "height":{  "raw":"175","encoded":"175"}
    /// 
    /// and it needs to converted to IssuerCredentialValue where
    ///    Name = "height"
    ///    Raw = "175"
    ///    Encoded = "175"
    /// 
    /// This allows this class to work with any schema
    /// <seealso cref="CredentialValuesListConverter"/>
    /// <seealso cref="CredentialRConverter"/>
    /// </summary>
    [JsonConverter(typeof(CredentialValuesListConverter))]
    public class CredentialValuesList : List<IssuerCredentialValue> { }

    /// <summary>
    /// TODO: very similar to <seealso cref="AttributeWithValue"/>
    /// </summary>
    public class IssuerCredentialValue
    {
        public string Name { get; set; }
        public string Raw { get; set; }
        public string Encoded { get; set; }
    }
}
