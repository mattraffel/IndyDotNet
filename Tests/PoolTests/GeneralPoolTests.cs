using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using IndyDotNet.Pool;

namespace Tests.PoolTests
{
    [TestClass]
    public class GeneralPoolTests
    {
        /// <summary>
        /// Simple test to make sure ListPools works.  
        /// TODO: because Pool.Create isn't working the results may be an empty 
        /// list.  So once Pool.Create works then update test to make sure the
        /// ListPools call returns the pool created
        /// </summary>
        [TestMethod]
        public void ListPoolTest()
        {
            List<IPool> pools = Factory.ListPools();
            Assert.IsNotNull(pools);
        }
    }
}
