using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using IndyDotNet.Pool;
using Tests.Utils;
using NLog;

namespace Tests.PoolTests
{
    [TestClass]
    public class CreatePoolTests
    {
        private List<string> _filesCreated = new List<string>();

        [TestCleanup]
        public void Cleanup() 
        {
            try
            {
                foreach (string fileName in _filesCreated)
                {
                    System.IO.File.Delete(fileName);
                }
            }
            catch (Exception ex)
            {
                Logging.Error($"Filed to clean up tests {ex.Message}");
            }

        }

        [TestMethod]
        public void CreatePoolWithNoGenesisFileSpecified()
        {
            IPool pool = Factory.GetPool("CreatePool");

            // because genesis transaction file of name "CreatePool.txn" does not
            // exist in the application working directory, this call will fail
            Assert.ThrowsException<System.AggregateException>(() => pool.Create()); 

        }

        [TestMethod]
        public void CreatePoolWithInvalidGenesisFile()
        {
            IPool pool = Factory.GetPool("CreatePool", "Bob.txn");
            Assert.ThrowsException<System.AggregateException> (() => pool.Create());
        }

        [TestMethod]
        public void CreatePoolWithValidGenesisFile()
        {
            string file = PoolUtils.GenerateGenesisFile();

            IPool pool = Factory.GetPool("CreatePool", file);
            pool.Create();

            Assert.IsTrue(System.IO.File.Exists(file), "expected to find genesis file created.");

            pool.Delete();
        }

    }
}
