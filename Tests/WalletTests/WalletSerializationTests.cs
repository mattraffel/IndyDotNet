using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

using IndyDotNet.Wallet;
using IndyDotNet.Utils;

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
                StorageType = "StorageType"
            };
            config.StorageConfig.Path = "Path";

            string json = config.ToJson();

            const string result = @"{
                    ""id"": ""Id"",
                    ""storage_type"": ""StorageType"",
                    ""storage_config"": {
                        ""path"": ""Path""
                        }
                    }";


            Assert.AreEqual(0, string.Compare(json, result), $"json {json} did not match to result {result}");
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


            Assert.AreEqual(0, string.Compare(json, result), $"json {json} did not match to result {result}");
        }
    }

}
