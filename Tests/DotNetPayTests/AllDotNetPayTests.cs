using System;
using IndyDotNet.PaymentHandler;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DotNetPayTests
{
    /// <summary>
    /// These tests ensure that DotNetPay works as expected.  
    /// </summary>
    [TestClass]
    public class AllDotNetPayTests
    {

        private static bool _registrationResult;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            _registrationResult = DotNetPay.Initialization.Register();
        }

        [TestMethod]
        public void RegisterDotNetPaySuccessfully()
        {
            Assert.IsTrue(_registrationResult, "DotNetPay.Initialization.Register failed");
        }

        [TestMethod]
        public void CreatePaymentAddressSuccessfully()
        {
            IPaymentHandler handler = new DotNetPay.PaymentHandler();
            string paymentAddress = handler.CreatePaymentAddress();
            Assert.IsFalse(string.IsNullOrEmpty(paymentAddress), "IPaymentHandler.CreatePaymentAddress did not return an address");

            string paymentPrefix = paymentAddress.Substring(0, 8);
            Assert.AreEqual("pay:DNP:", paymentPrefix, $"Payment address not formatted as expected {paymentAddress}");
        }
    }
}
