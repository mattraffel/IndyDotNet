using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DotNetPayTests
{
    [TestClass]
    public class AllDotNetPayTests
    {
        [TestMethod]
        public void RegisterDotNetPaySuccessfully()
        {
            bool registrationResult = DotNetPay.Initialization.Register();

            Assert.IsTrue(registrationResult, "DotNetPay.Initialization.Register failed");
        }
    }
}
