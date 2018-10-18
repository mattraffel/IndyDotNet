using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IndyDotNet.Pool;

namespace Tests.PoolTests
{
    [TestClass]
    public class CreatePoolTests
    {
        [TestMethod]
        public void CreateDefaultPool()
        {
            IPool pool = Factory.GetPool("CreatePool");
            pool.Create();

        }

        [TestMethod]
        public void CreateDefaultPoolWithGenesisFile()
        {
            IPool pool = Factory.GetPool("CreatePool", "Bob.txn");
            pool.Create();
        }
    }
}
