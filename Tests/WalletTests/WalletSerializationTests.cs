using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

using IndyDotNet.Wallet;
using IndyDotNet.Utils;
using Newtonsoft.Json.Linq;

namespace Tests.WalletTests
{
    [TestClass]
    public class WalletSerializationTests
    {
        [TestMethod]
        public void WalletConfigSerializesToJsonCorrectly()
        {
            WalletConfig config = new WalletConfig
            {
                Id = "Id",
                StorageType = "StorageType",
                StorageConfig = new WalletStorageConfig()
                {
                    Path = "Path"
                }
            };

            string json = config.ToJson();

            const string result = @"{
                    ""id"": ""Id"",
                    ""storage_type"": ""StorageType"",
                    ""storage_config"": {
                        ""path"": ""Path""
                        }
                    }";

            var resultObject = JValue.Parse(result);

            Assert.AreEqual(config.Id, resultObject["id"], "Id did not match");
            Assert.AreEqual(config.StorageType, resultObject["storage_type"], "StorageType did not match");

        }

        [TestMethod]
        public void WalletCredentialsSerializesToJsonCorrectly()
        {
            WalletCredentials credentials = new WalletCredentials
            {
                Key = "Key",
                StorageCredentials = "StorageCredentials"
            };

            string json = credentials.ToJson();

            const string result = @"{
                  ""key"": ""Key"",
                  ""storage_credentials"": ""StorageCredentials""
                  }";

            var resultObject = JValue.Parse(result);

            Assert.AreEqual(credentials.Key, resultObject["key"], "Key did not match");
            Assert.AreEqual(credentials.StorageCredentials, resultObject["storage_credentials"], "StorageCredentials did not match");
        }
    }

}
