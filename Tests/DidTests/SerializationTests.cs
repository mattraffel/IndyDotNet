using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using IndyDotNet.Did;
using IndyDotNet.Utils;

namespace Tests.DidTests
{
    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        public void IdentitySeedSerializesCorrectly()
        {
            IdentitySeed seed = new IdentitySeed()
            {
                Seed = "1234567890"
            };

            string json = seed.ToJson();
            const string expected = @"{""cid"":false,""seed"":""1234567890""}";

            Assert.AreEqual(expected, json, "IdentitySeed serialization didn't match");
        }

        [TestMethod]
        public void IdentitySeedWithCryptoTypeSerializesCorrectly()
        {
            IdentitySeed seed = new IdentitySeed()
            {
                Seed = "1234567890",
                CryptoType = "ed25519"
            };

            string json = seed.ToJson();
            const string expected = @"{""cid"":false,""crypto_type"":""ed25519"",""seed"":""1234567890""}";

            Assert.AreEqual(expected, json, "IdentitySeed serialization didn't match");
        }

        [TestMethod]
        public void IdentitySeedWithDidSerializesCorrectly()
        {
            IdentitySeed seed = new IdentitySeed()
            {
                Seed = "1234567890",
                Did = "CnEDk9HrMnmiHXEV1WFgbVCRteYnPqsJwrTdcZaNhFVW"
            };

            string json = seed.ToJson();
            const string expected = @"{""cid"":false,""did"":""CnEDk9HrMnmiHXEV1WFgbVCRteYnPqsJwrTdcZaNhFVW"",""seed"":""1234567890""}";

            Assert.AreEqual(expected, json, "IdentitySeed serialization didn't match");
        }
    }
}
