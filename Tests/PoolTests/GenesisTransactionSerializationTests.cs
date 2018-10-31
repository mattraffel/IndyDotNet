using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IndyDotNet.Utils;
using IndyDotNet.Pool;
using Tests.Utils;

namespace Tests.PoolTests
{
    [TestClass]
    public class GenesisTransactionSerializationTests
    {
        [TestMethod]
        public void SingleTransactionGeneratedCorrectly()
        {
            GenesisTransaction genesisTransaction = PoolUtils.GetNode1GenesisTransaction();

            string json = genesisTransaction.ToJson();

            Assert.IsFalse(true, $"{json}");
        }
    }
}
