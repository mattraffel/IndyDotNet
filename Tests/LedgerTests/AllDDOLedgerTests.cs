﻿using System;
using System.Collections.Generic;
using IndyDotNet.Did;
using IndyDotNet.Ledger;
using IndyDotNet.Pool;
using IndyDotNet.Utils;
using IndyDotNet.Wallet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Utils;

namespace Tests.LedgerTests
{
    [TestClass]
    public class AllDDOLedgerTests
    {
        private IPool _pool;
        private IWallet _wallet;
        private List<string> _filesCreated = new List<string>();

        [TestInitialize]
        public void Initialize()
        {
            string file = PoolUtils.GenerateGenesisFile();
            _filesCreated.Add(file);

            _pool = IndyDotNet.Pool.Factory.GetPool("AllLedgerTestsPool", file);
            _pool.Create();
            _pool.Open();

            WalletConfig config = new WalletConfig()
            {
                Id = "AllLedgerTestsWalletId"
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
                Logger.Warn($"AllDDOLedgerTests failed to cleanup {ex.Message}");
            }
        }

        [TestMethod]
        public void BuildDDORequestSuccessfully()
        {
            IDDOLedger ledger = IndyDotNet.Ledger.Factory.CreateDDOLedger();
            IDid submitter = IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, new IdentitySeed()
            {
                Seed = "000000000000000000000000Trustee1"
            });

            IDid target = IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, new IdentitySeed()
            {
                Seed = "000000000000000000000000Trustee2"
            });

            BuildRequestResult result = ledger.BuildGetRequest(submitter, target);

            Assert.IsNotNull(result, "failed to create BuildRequestResult");
            Assert.AreEqual(result.Operation.Type, "120", $"BuildGetDdoRequest request type did not 120: received {result.Operation.Type}");
            Assert.AreEqual(result.Identifier, submitter.Did, $"Identifer failed match to submitter: {submitter.Did}");
            Assert.AreEqual(result.Operation.Dest, target.Did, $"Dest failed match to target: {target.Did}");
        }

        [TestMethod]
        public void SignAndSubmitRequestDDORequestSuccessfully()
        {
            IDDOLedger ledger = IndyDotNet.Ledger.Factory.CreateDDOLedger();

            IDid submitter = IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, new IdentitySeed()
            {
                Seed = "000000000000000000000000Trustee1"
            });

            IDid target = IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, new IdentitySeed()
            {
                Seed = "000000000000000000000000Trustee2"
            });

            BuildRequestResult result = ledger.BuildGetRequest(submitter, target);
            SignAndSubmitRequestResponse signResult = ledger.SignAndSubmitRequest(_pool, _wallet, submitter, result);

            Assert.IsNotNull(signResult, "failed to create SignAndSubmitRequestResult");

            // Dids are submitter: V4SGRU86Z58d6TV7PBUe6f and target: LnXR1rPnncTPZvRdmJKhJQ
            Assert.AreEqual(signResult.Result.Transaction.Metadata.From, submitter.Did, $"txn.metadata.from failed to match submitter: {submitter.Did}");
            Assert.AreEqual(signResult.Result.Transaction.TxnData.Dest, target.Did, $"txn.data.dest failed to match target: {target.Did}");

        }

        [TestMethod]
        public void SignRequestDDORequestSuccessfully()
        {
            IDDOLedger ledger = IndyDotNet.Ledger.Factory.CreateDDOLedger();

            IDid submitter = IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, new IdentitySeed()
            {
                Seed = "000000000000000000000000Trustee1"
            });

            IDid target = IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, new IdentitySeed()
            {
                Seed = "000000000000000000000000Trustee2"
            });

            BuildRequestResult result = ledger.BuildGetRequest(submitter, target);
            BuildRequestResult signResult = ledger.SignRequest(_wallet, submitter, result);

            Assert.IsNotNull(signResult, "failed to create SignAndSubmitRequestResult");

            // Dids are submitter: V4SGRU86Z58d6TV7PBUe6f and target: LnXR1rPnncTPZvRdmJKhJQ
            Assert.IsNotNull(signResult, "failed to create BuildNymRequestResult");
            Assert.IsTrue(0 < signResult.ReqId, "ReqId not set");
            Assert.AreEqual(signResult.Identifier, submitter.Did, $"Identifer failed match to submitter: {submitter.Did}");
            Assert.AreEqual(signResult.Operation.Dest, target.Did, $"Dest failed match to target: {target.Did}");
            Assert.IsFalse(string.IsNullOrEmpty(signResult.Signature), $"Signature not found: '{signResult.Signature}'");

        }

        [TestMethod]
        public void SubmitRequestDDORequestSuccessfully()
        {
            IDDOLedger ledger = IndyDotNet.Ledger.Factory.CreateDDOLedger();

            IDid submitter = IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, new IdentitySeed()
            {
                Seed = "000000000000000000000000Trustee1"
            });

            IDid target = IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, new IdentitySeed()
            {
                Seed = "000000000000000000000000Trustee2"
            });

            BuildRequestResult result = ledger.BuildGetRequest(submitter, target);
            BuildRequestResult signedResult = ledger.SignRequest(_wallet, submitter, result);
            SignAndSubmitRequestResponse signResult = ledger.SubmitRequest(_pool, signedResult);

            Assert.IsNotNull(signResult, "failed to create SignAndSubmitRequestResult");

            // Dids are submitter: V4SGRU86Z58d6TV7PBUe6f and target: LnXR1rPnncTPZvRdmJKhJQ
            Assert.AreEqual(signResult.Result.Transaction.Metadata.From, submitter.Did, $"txn.metadata.from failed to match submitter: {submitter.Did}");
            Assert.AreEqual(signResult.Result.Transaction.TxnData.Dest, target.Did, $"txn.data.dest failed to match target: {target.Did}");

        }
    }
}
