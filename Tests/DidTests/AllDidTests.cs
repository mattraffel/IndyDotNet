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
    public class AllDidTests
    {
        private IPool _pool;
        private IWallet _wallet;
        private List<string> _filesCreated = new List<string>();

        [TestInitialize]
        public void Initialize()
        {
            string file = PoolUtils.GenerateGenesisFile();
            _filesCreated.Add(file);

            _pool = IndyDotNet.Pool.Factory.GetPool("AllDidTestsPool", file);
            _pool.Create();
            _pool.Open();

            WalletConfig config = new WalletConfig()
            {
                Id = "AllDidTestsPoolWalletId"
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
            catch(Exception ex)
            {
                Logger.Warn($"AllDidTests failed to cleanup {ex.Message}");
            }
        }

        [TestMethod]
        public void CreateAndStoreDidWithSeedSuccessfully()
        {

            IdentitySeed seed = new IdentitySeed()
            {
                Seed = "00000000000000000000000000000My1"
            };

            IDid did = IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, seed);

            Assert.IsTrue(0 < did.Did.Length, $"Did was not set {did.Did}");
            Assert.IsTrue(0 < did.VerKey.Length, $"VerKey was not set {did.VerKey}");
        }

        [TestMethod]
        public void CreateAndStoreDidWithSeedCIDSuccessfully()
        {
            IdentitySeed seed = new IdentitySeed()
            {
                Seed = "00000000000000000000000000000My1",
                CID = true
            };

            IDid did = IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, seed);


            Assert.IsTrue(0 < did.Did.Length, $"Did was not set {did.Did}");
            Assert.IsTrue(0 < did.VerKey.Length, $"VerKey was not set {did.VerKey}");
        }

        [TestMethod]
        public void GetMyDidAbbreviatedVerKeySuccessfully()
        {
            IdentitySeed seed = new IdentitySeed()
            {
                Seed = "00000000000000000000000000000My1",
                CID = true
            };

            IDid did = IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, seed);
            string abbreviated = did.GetAbbreviateVerkey();

            Assert.IsTrue(0 < abbreviated.Length, $"abbreviated verkey was not returned {abbreviated}");
        }
    
        [TestMethod]
        public void GetListOfDidsSuccessfully()
        {
            IdentitySeed seed = new IdentitySeed()
            {
                Seed = "00000000000000000000000000000My1"
            };

            IDid did = IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, seed);

            List<IDid> dids = IndyDotNet.Did.Factory.GetAllMyDids(_pool, _wallet);

            Assert.IsNotNull(dids, "list was empty");
            Assert.IsTrue(0 < dids.Count, "Exptected to have more than 0 dids in list");
        }

        [TestMethod]
        public void SaveTheirDidSuccesfully()
        {
            IDid did = IndyDotNet.Did.Factory.CreateTheirDid(_pool, _wallet, "CnEDk9HrMnmiHXEV1WFgbVCRteYnPqsJwrTdcZaNhFVW");
            Assert.IsTrue(0 < did.Did.Length, $"Did was not set {did.Did}");
            Assert.IsTrue(0 < did.VerKey.Length, $"VerKey was not set {did.VerKey}");
        }

        [TestMethod]
        public void GetTheirDidSuccesfully()
        {
            IDid did = IndyDotNet.Did.Factory.CreateTheirDid(_pool, _wallet, "CnEDk9HrMnmiHXEV1WFgbVCRteYnPqsJwrTdcZaNhFVW");

            IDid did2 = IndyDotNet.Did.Factory.GetTheirDid(_pool, _wallet, did.Did);

            Assert.IsTrue(0 < did.Did.Length, $"Did was not set {did2.Did}");
            Assert.IsTrue(0 < did.VerKey.Length, $"VerKey was not set {did2.VerKey}");
        }
    }
}
