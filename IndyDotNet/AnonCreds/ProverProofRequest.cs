using System;
using System.Collections.Generic;
using IndyDotNet.Internal.Json;
using Newtonsoft.Json;

namespace IndyDotNet.AnonCreds
{
    public class ProverProofRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nonce")]
        public string Nonce { get; set; }

        [JsonProperty("requested_attributes")]
        public RequestedAttributesList RequestedAttributes { get; set; }

        [JsonProperty("requested_predicates")]
        public RequestedPredicatesList RequestedPredicates { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }

    [JsonConverter(typeof(RequestedAttributesListConverter))]
    public class RequestedAttributesList : List<RequestedAttribute> {}

    public class RequestedAttribute
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("restrictions", NullValueHandling = NullValueHandling.Ignore)]
        public RequestRestrictions Restrictions { get; set; }
    }

    [JsonConverter(typeof(RequestedPredicatesListConverter))]
    public class RequestedPredicatesList : List<RequestedPredicate> { }

    public class RequestedPredicate
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("p_type")]
        public string PType { get; set; }

        [JsonProperty("p_value")]
        public string PValue { get; set; }

        [JsonProperty("restrictions", NullValueHandling = NullValueHandling.Ignore)]
        public RequestRestrictions Restrictions { get; set; }
    }

    [JsonConverter(typeof(RequestRestrictionsConverter))]
    public class RequestRestrictions : Dictionary<string, string> {}
}
