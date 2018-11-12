using System;
using System.Collections.Generic;
using IndyDotNet.Did;
using IndyDotNet.Ledger;
using IndyDotNet.Pool;
using IndyDotNet.Utils;
using IndyDotNet.Wallet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Utils;

namespace Tests.Demos
{
    /// <summary>
    /// follows the indy-sdk how-to
    /// doc/how-tos/write-did-and-query-verkey/java/WriteDIDAndQueryVerkey.java
    /// </summary>
    [TestClass]
    public class WriteDidAndQueryVeryKeyDemo
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
        public void Demo()
        {
            // Initialization
            // 1. Creating a new local pool ledger configuration that can be used later to connect pool nodes.
            // 2. Open pool ledger and get the pool handle from libindy.
            // 3. Creates a new identity wallet
            // 4. Open identity wallet and get the wallet handle from libindy
            // SEE Initialize() above

            // PART 2
            // 5. Generating and storing steward DID and Verkey
            // 6. Generating and storing Trust Anchor DID and Verkey
            IDid stewardDid = IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, new IdentitySeed()
            {
                Seed = "000000000000000000000000Steward1"
            });
            IDid trustAnchor = IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, null);

            // PART 3
            // 7. Build NYM request to add Trust Anchor to the ledger
            INymLedger nymLedger = IndyDotNet.Ledger.Factory.CreateBuildNymLedger();
            BuildRequestResult nymRequest = nymLedger.BuildRequest(stewardDid, trustAnchor, trustAnchor.VerKey, "", NymRoles.TrustAnchor);

            // 8. Sending the nym request to ledger
            SignAndSubmitRequestResponse nymResult = nymLedger.SignAndSubmitRequest(_pool, _wallet, stewardDid, nymRequest);

            // PART 4
            // 9. Generating and storing DID and Verkey to query the ledger with
            IDid clientDid = IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, null);

            // 10. Building the GET_NYM request to query Trust Anchor's Verkey as the Client
            IGetNymLedger getNymLedger = IndyDotNet.Ledger.Factory.CreateGetNymLedger();
            BuildRequestResult getNymRequest = getNymLedger.BuildGetRequest(clientDid, trustAnchor);

            // 11. Sending the GET_NYM request to the ledger
            GetNymSubmitReponse getNymSubmitRequest = getNymLedger.SubmitRequest(_pool, getNymRequest);

            // 12. Comparing Trust Anchor did as written by Steward and as retrieved in Client's query
            Assert.AreEqual(trustAnchor.Did, getNymSubmitRequest.Result.Dest, $"trustAnchor: Did = {trustAnchor.Did} VerKey = {trustAnchor.VerKey} and Result.Dest = {getNymSubmitRequest.Result.Dest}");

            // clean up
            // 13. Close and delete wallet
            // 14. Close pool
            // 15. Delete pool ledger config
            // SEE Cleanup() above
        }
    }
}
