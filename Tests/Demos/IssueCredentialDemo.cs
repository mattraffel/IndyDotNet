using System;
using System.Collections.Generic;
using IndyDotNet.AnonCreds;
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
    public class IssueCredentialDemo
    {
        private IPool _pool;
        private IWallet _wallet;
        private IWallet _proverWallet;
        private List<string> _filesCreated = new List<string>();
        private WalletCredentials _credentials = new WalletCredentials()
        {
            Key = "8dvfYSt5d1taSd6yJdpjq4emkwsPDDLYxkNFysFD2cZY",
            KeyDerivationMethod = KeyDerivationMethod.RAW
        };

        [TestInitialize]
        public void Initialize()
        {
            string file = PoolUtils.GenerateGenesisFile();
            _filesCreated.Add(file);

            _pool = IndyDotNet.Pool.Factory.GetPool("IssueCredentialDemoPool", file);
            _pool.Create();
            _pool.Open();

            WalletConfig config = new WalletConfig()
            {
                Id = "IssueCredentialDemoWalletId"
            };

            _wallet = IndyDotNet.Wallet.Factory.GetWallet(config, _credentials);
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

                if (null != _proverWallet)
                {
                    _proverWallet.Close();
                    _proverWallet.Delete();
                    _proverWallet = null;
                }

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
                Logger.Warn($"WriteDidAndQueryVeryKeyDemo failed to cleanup {ex.Message}");
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

            // 9. build the schema definition request
            SchemaDefinition schemaDefinition = new SchemaDefinition()
            {
                Name = "name",
                Version = "1.1",
                Id = "id"
            };

            schemaDefinition.AttributeNames.Add("age");
            schemaDefinition.AttributeNames.Add("height");
            schemaDefinition.AttributeNames.Add("sex");
            schemaDefinition.AttributeNames.Add("name");

            ISchemaLedger schemaLedger = IndyDotNet.Ledger.Factory.CreateSchemaLedger();
            BuildSchemaResult buildSchema = schemaLedger.BuildSchemaRequest(stewardDid, schemaDefinition);

            // 10. Sending the SCHEMA request to the ledger
            SignAndSubmitRequestResponse signAndSubmitRequestResponse = schemaLedger.SignAndSubmitRequest(_pool, _wallet, stewardDid, buildSchema);

            // 11. Creating and storing CLAIM DEFINITION using anoncreds as Trust Anchor, for the given Schema
            IIssuerAnonCreds issuer = IndyDotNet.AnonCreds.Factory.GetIssuerAnonCreds(_wallet);
            CredentialDefinitionSchema credentialschema = new CredentialDefinitionSchema()
            {
                SequenceNo = 1,
                Id = "id",
                Name = "name",
                Version = "1.1",
                Tag = "TAG"
            };
            credentialschema.AttributeNames.Add("age");
            credentialschema.AttributeNames.Add("height");
            credentialschema.AttributeNames.Add("sex");
            credentialschema.AttributeNames.Add("name");

            IssuerCredential credentialDefinition = issuer.CreateStoreCredentialDef(trustAnchor, credentialschema);

            // 12. Creating Prover wallet and opening it to get the handle
            WalletConfig config = new WalletConfig()
            {
                Id = "ProverIssueCredentialDemoWalletId"
            };

            _proverWallet = IndyDotNet.Wallet.Factory.GetWallet(config, _credentials);
            _proverWallet.Create();
            _proverWallet.Open();

            // 13. Prover is creating Master Secret
            IProverAnonCreds prover = IndyDotNet.AnonCreds.Factory.GetProverAnonCreds(_proverWallet);
            prover.CreateMasterSecret("master_secret");

            // 14. Issuer(Trust Anchor) is creating a Claim Offer for Prover
            IssuerCredentialOffer claimOffer = issuer.CreateCredentialOffer(credentialDefinition.Id);

            // 15. Prover creates Claim Request
            IDid proverDID = IndyDotNet.Did.Factory.CreateMyDid(_pool, _proverWallet, null);
            ProverCredentialRequest credentialRequest = prover.CreateCredentialRequest(proverDID, claimOffer, credentialDefinition, "master_secret");

            Assert.AreEqual(credentialRequest.CredDefId, claimOffer.CredDefId);
            Assert.AreEqual(credentialRequest.ProverDid, proverDID.Did);

            // 16. Issuer(Trust Anchor) creates Claim for Claim Request

            // 17. Prover processes and stores Claim

            // clean up
            // Close and delete wallet
            // Close pool
            // Delete pool ledger config
            // SEE Cleanup() above
        }
    }
}
