using System;
using IndyDotNet.AnonCreds;
using IndyDotNet.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Tests.InternalTests
{
    [TestClass]
    public class CredentialRConverterTests
    {
        [TestMethod]
        public void ConvertToJSonSuccessfully()
        {
            string expectedResult = "{\"master_secret\":\"stufstuff\",\"item1\":\"value1\",\"item2\":\"value2\"}";
            R credentialR = new R()
            {
                MasterSecret = "stufstuff"
            };

            credentialR.Attributes.Add("item1", "value1");
            credentialR.Attributes.Add("item2", "value2");

            string json = credentialR.ToJson();

            Assert.AreEqual(expectedResult, json, $"generated {json} did not match expected {expectedResult}");
        }

        [TestMethod]
        public void ConvertToRObjectSuccesfully()
        {
            string json = "{\"master_secret\":\"stufstuff\",\"item1\":\"value1\",\"item2\":\"value2\"}";

            R credentialR = JsonConvert.DeserializeObject<R>(json);

            Assert.IsNotNull(credentialR);
            Assert.AreEqual("stufstuff", credentialR.MasterSecret);
            Assert.AreEqual(2, credentialR.Attributes.Count);

        }
    }
}
