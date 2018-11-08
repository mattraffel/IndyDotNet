using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using IndyDotNet.Pool;
using IndyDotNet.Wallet;
using IndyDotNet.Utils;
using Tests.Utils;
using System.Collections.Generic;
using IndyDotNet.Did;


namespace Tests.DidTests
{
    [TestClass]
    public class AllEndpointTests
    {
        private IPool _pool;
        private IWallet _wallet;
        private List<string> _filesCreated = new List<string>();

        [TestInitialize]
        public void Initialize()
        {
            string file = PoolUtils.GenerateGenesisFile();
            _filesCreated.Add(file);

            _pool = IndyDotNet.Pool.Factory.GetPool("AllEndpointTestsPool", file);
            _pool.Create();
            _pool.Open();

            WalletConfig config = new WalletConfig()
            {
                Id = "AllEndpointTestsPoolWalletId"
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
                Logger.Warn($"AllEndpointTests failed to cleanup {ex.Message}");
            }
        }

        [TestMethod]
        public void SetAndGetEndpointSuccessfully()
        {
            IdentitySeed seed = new IdentitySeed()
            {
                Seed = "00000000000000000000000000000My1"
            };

            IDid did = IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, seed);

            IEndPoint endPoint = IndyDotNet.Did.Factory.GetEndPoint(_pool, _wallet, did);
            endPoint.Save("192.168.1.1:9000", "CnEDk9HrMnmiHXEV1WFgbVCRteYnPqsJwrTdcZaNhFVW");

            IEndPoint resultEndPoint = IndyDotNet.Did.Factory.GetEndPoint(_pool, _wallet, did);
            resultEndPoint.Open();

            Assert.AreEqual(endPoint.Address, resultEndPoint.Address);
            Assert.AreEqual(endPoint.TransportKey, resultEndPoint.TransportKey);
        }
    }
}
