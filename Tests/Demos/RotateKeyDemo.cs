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
    [TestClass]
    public class RotateKeyDemo
    {
        private IPool _pool;
        private IWallet _wallet;
        private List<string> _filesCreated = new List<string>();

        [TestInitialize]
        public void Initialize()
        {
            string file = PoolUtils.GenerateGenesisFile();
            _filesCreated.Add(file);

            _pool = IndyDotNet.Pool.Factory.GetPool("RotateKeyDemoPool", file);
            _pool.Create();
            _pool.Open();

            WalletConfig config = new WalletConfig()
            {
                Id = "RotateKeyDemoWalletId"
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
                Logger.Warn($"RotateKeyDemo failed to cleanup {ex.Message}");
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

            // 5. Generating and storing steward DID and Verkey
            IDid stewardDid = IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, new IdentitySeed()
            {
                Seed = "000000000000000000000000Steward1"
            });

            // 6. Generating and storing Trust Anchor DID and Verkey
            IDid trustAnchor = IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, null);

            // 7. Build NYM request to add Trust Anchor to the ledger
            INymLedger nymLedger = IndyDotNet.Ledger.Factory.CreateNymLedger();
            BuildRequestResult nymRequest = nymLedger.BuildRequest(stewardDid, trustAnchor, trustAnchor.VerKey, "", NymRoles.TrustAnchor);

            // 8. Sending the nym request to ledger
            SignAndSubmitRequestResponse nymResult = nymLedger.SignAndSubmitRequest(_pool, _wallet, stewardDid, nymRequest);

            // 9. Generating new Verkey of Trust Anchor in the wallet
            trustAnchor.ReplaceStart();

            // 10. Building NYM request to update new verkey to ledger
            //     Note:  using TemnpVerKey which was set in step #9
            BuildRequestResult updateNymRequest = nymLedger.BuildRequest(trustAnchor, trustAnchor, trustAnchor.TempVerKey, "", NymRoles.TrustAnchor);

            // 11. Sending NYM request to the ledger
            SignAndSubmitRequestResponse replaceKeyResult = nymLedger.SignAndSubmitRequest(_pool, _wallet, trustAnchor, updateNymRequest);

            // 12. Applying new Trust Anchor's Verkey in wallet
            trustAnchor.ReplaceApply();

            // 13. Reading new Verkey from wallet
            trustAnchor.Refresh();

            // 14. Building GET_NYM request to get Trust Anchor from Verkey
            BuildRequestResult refreshNymRequest = nymLedger.BuildRequest(trustAnchor, trustAnchor, null, "", NymRoles.NA);

            // 15.Sending GET_NYM request to ledger
            SignAndSubmitRequestResponse refreshNymResult = nymLedger.SubmitRequest(_pool, refreshNymRequest);

            // 16. Comparing Trust Anchor verkeys
            Assert.AreEqual(trustAnchor.VerKey, refreshNymResult.Result.Transaction.TxnData.Dest);

            // clean up
            // Close and delete wallet
            // Close pool
            // Delete pool ledger config
            // SEE Cleanup() above
        }
    }
}
