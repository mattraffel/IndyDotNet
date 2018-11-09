using System;
using System.Collections.Generic;
using IndyDotNet.Did;
using IndyDotNet.Ledger;
using IndyDotNet.Pool;
using IndyDotNet.Utils;
using IndyDotNet.Wallet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Tests.Utils;

namespace Tests.LedgerTests
{
    [TestClass]
    public class AllLedgerTests
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
        public void BuildNymRequestSuccessfully()
        {
            ILedger ledger = IndyDotNet.Ledger.Factory.GetLedger();

            IDid submitter = IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, new IdentitySeed()
                {
                    Seed = "000000000000000000000000Trustee1"
                });

            IDid target = IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, new IdentitySeed()
                {
                    Seed = "000000000000000000000000Trustee2"
                });

            BuildNymRequestResult result = ledger.BuildNymRequest(submitter, target, null, null, NymRoles.NA);


            Assert.IsNotNull(result, $"failed to create BuildNymRequestResult");
            Assert.AreEqual(result.Identifier, submitter.Did, $"Identifer failed match to submitter: {submitter.Did}");
            Assert.AreEqual(result.Operation.Dest, target.Did, $"Dest failed match to target: {target.Did}");

        }
    }
}
