using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IndyDotNet.Pool;

namespace Tests.PoolTests
{
    [TestClass]
    public class CreatePoolTests
    {
        [TestMethod]
        public void CreatePoolWithNoGenesisFileSpecified()
        {
            IPool pool = Factory.GetPool("CreatePool");
            pool.Create();

        }

        [TestMethod]
        public void CreatePoolWithInvalidGenesisFile()
        {
            IPool pool = Factory.GetPool("CreatePool", "Bob.txn");
            pool.Create();
        }

        [TestMethod]
        public void CreatePoolWithValidGenesisFile()
        {
            IPool pool = Factory.GetPool("CreatePool", "Bob.txn");
            pool.Create();
        }
    }
}
