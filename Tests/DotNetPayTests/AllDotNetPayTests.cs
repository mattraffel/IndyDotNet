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

        [TestMethod]
        public void CreatePaymentAddressSuccessfully()
        {
            bool registrationResult = DotNetPay.Initialization.Register();
            Assert.Fail("CreatePaymentAddressSuccessfully isn't finished");
        }
    }
}
