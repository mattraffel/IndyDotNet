using System;
using IndyDotNet.AnonCreds;
using IndyDotNet.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.InternalTests
{
    [TestClass]
    public class RequestProofConverterTests
    {

        [TestMethod]
        public void ProverProofRequestConvertsSuccessfully()
        {
            ProverProofRequest request = new ProverProofRequest()
            {
                Name = "proof",
                Version = "1.0",
                Nonce = "1234567890",
                RequestedAttributes = new RequestedAttributesList()
            };

            request.RequestedAttributes.Add(new RequestedAttribute()
            {
                Name = "bob",
                Restrictions = null
            });
            request.RequestedAttributes.Add(new RequestedAttribute()
            {
                Name = "joe",
                Restrictions = null
            });

            string json = request.ToJson();
            string resultJson = "{\"name\":\"proof\",\"nonce\":\"1234567890\",\"requested_attributes\":{\"attr1_referent\":{\"name\":\"bob\"},\"attr2_referent\":{\"name\":\"joe\"}},\"version\":\"1.0\"}";

            Assert.AreEqual(resultJson, json, $"ProverProofRequest was converted to {json}");

        }

        [TestMethod]
        public void ProverProofRequestWithRestrictionsConvertsSuccessfully()
        {
            ProverProofRequest request = new ProverProofRequest()
            {
                Name = "proof",
                Version = "1.0",
                Nonce = "1234567890",
                RequestedAttributes = new RequestedAttributesList()
            };

            RequestedAttribute attribute = new RequestedAttribute()
            {
                Name = "bob",
                Restrictions = new RequestRestrictions()
            };
            attribute.Restrictions.Add("issuer_did", "YWpmwLpTjxxieBPUxztnXo");


            request.RequestedAttributes.Add(attribute);

            string json = request.ToJson();
            string resultJson = "{\"name\":\"proof\",\"nonce\":\"1234567890\",\"requested_attributes\":{\"attr1_referent\":{\"name\":\"bob\",\"restrictions\":{\"restrictions\":{\"issuer_did\":\"YWpmwLpTjxxieBPUxztnXo\"}}}},\"version\":\"1.0\"}";

            Assert.AreEqual(resultJson, json, $"ProverProofRequest was converted to {json}");

        }

        [TestMethod]
        public void RestrictionsWithOneItemConvertsSuccessfully()
        {
            RequestRestrictions list = new RequestRestrictions();
            list.Add("issuer_did", "YWpmwLpTjxxieBPUxztnXo");

            string json = list.ToJson();
            string resultJson = "{\"restrictions\":{\"issuer_did\":\"YWpmwLpTjxxieBPUxztnXo\"}}";

            Assert.AreEqual(resultJson, json, $"RequestRestrictions was converted to {json}");
        }

        [TestMethod]
        public void RestrictionsWithTwoItemsConvertsSuccessfully()
        {
            RequestRestrictions list = new RequestRestrictions();
            list.Add("issuer_did", "YWpmwLpTjxxieBPUxztnXo");
            list.Add("issuer_did2", "YWpmwLpTjxxieBPUxztnXo");

            string json = list.ToJson();
            string resultJson = "{\"restrictions\":{\"issuer_did\":\"YWpmwLpTjxxieBPUxztnXo\",\"issuer_did2\":\"YWpmwLpTjxxieBPUxztnXo\"}}";

            Assert.AreEqual(resultJson, json, $"RequestRestrictions was converted to {json}");
        }

        [TestMethod]
        public void RequestedAttributesListWithOneItemConvertsSuccessfully()
        {
            RequestedAttributesList list = new RequestedAttributesList();
            list.Add(new RequestedAttribute() { 
                Name = "bob",
                Restrictions = null
            });

            string json = list.ToJson();
            string resultJson = "{\"attr1_referent\":{\"name\":\"bob\"}}";

            Assert.AreEqual(resultJson, json, $"RequestedAttributesList was converted to {json}");
        }
    }
}
