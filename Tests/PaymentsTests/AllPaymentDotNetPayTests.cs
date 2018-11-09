using System;
using System.Collections.Generic;
using IndyDotNet.Payments;
using IndyDotNet.Pool;
using IndyDotNet.Utils;
using IndyDotNet.Wallet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Utils;

namespace Tests.PaymentsTests
{
    /// <summary>
    /// these tests ensure that the call chain is correctly implemented in IndyDotNet
    /// by using DotNetPay and calling IndyDotNet Payments API
    /// 
    /// These test results are similar to DotNetPayTests but the level of execution
    /// is more involved
    /// </summary>
    [TestClass]
    public class AllPaymentDotNetPayTests
    {
        private IPool _pool;
        private IWallet _wallet;
        private List<string> _filesCreated = new List<string>();

        [TestInitialize]
        public void Initialize()
        {
            string file = PoolUtils.GenerateGenesisFile();
            _filesCreated.Add(file);

            _pool = IndyDotNet.Pool.Factory.GetPool("AllPaymentsTestsPool", file);
            _pool.Create();
            _pool.Open();

            WalletConfig config = new WalletConfig()
            {
                Id = "AllPaymentsTestWalletId"
            };

            WalletCredentials credentials = new WalletCredentials()
            {
                Key = "8dvfYSt5d1taSd6yJdpjq4emkwsPDDLYxkNFysFD2cZY",
                KeyDerivationMethod = KeyDerivationMethod.RAW
            };

            _wallet = IndyDotNet.Wallet.Factory.GetWallet(config, credentials);
            _wallet.Create();
            _wallet.Open();
        }

        [TestCleanup]
        public void Cleanup()
        {
            try
            {
                _wallet.Close();
                _wallet.Delete();
                _wallet = null;

                _pool.Close();
                _pool.Delete();
                _pool = null;

                foreach (string fileName in _filesCreated)
                {
                    System.IO.File.Delete(fileName);
                }

            }
            catch (Exception ex)
            {
                Logger.Warn($"AllPaymentDotNetPayTests failed to cleanup {ex.Message}");
            }
        }

        [TestMethod]
        public void CreatePaymentAddressSuccessfully()
        {
            bool registrationResult = DotNetPay.Initialization.Register();
            Assert.IsTrue(registrationResult, "DotNetPay.Initialization.Register failed");

            IPayments payments = IndyDotNet.Payments.Factory.CreatePayment(_wallet);

            string paymentAddress = payments.CreatePaymentAddress("DNP", "");
            Assert.IsFalse(string.IsNullOrEmpty(paymentAddress), "IPayments.CreatePaymentAddress did not return an address");
            string paymentPrefix = paymentAddress.Substring(0, 8);
            Assert.AreEqual("pay:DNP:", paymentPrefix, $"Payment address not formatted as expected {paymentAddress}");
        }
    }
}
