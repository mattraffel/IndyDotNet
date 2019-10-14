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
    public class WriteSchemaClaimDefDemo
    {
        private IPool _pool;
        private IWallet _wallet;
        private List<string> _filesCreated = new List<string>();

        [TestInitialize]
        public void Initialize()
        {
            string file = Environment.GetEnvironmentVariable("POOL_GENESIS_FILE");

            if (string.IsNullOrEmpty(file))
            {
                file = PoolUtils.GenerateGenesisFile();
                _filesCreated.Add(file);
            }

            _pool = IndyDotNet.Pool.Factory.GetPool("WriteDidAndQueryVeryKeyDemoPool", file);
            _pool.Create();
            _pool.Open();

            WalletConfig config = new WalletConfig()
            {
                Id = "WriteDidAndQueryVeryKeyDemoWalletId"
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
            CredentialDefinitionSchema credentialDefinition = new CredentialDefinitionSchema()
            {
                SequenceNo = 1,
                Id = "id",
                Name = "name",
                Version = "1.1",
                Tag = "TAG"
            };
            credentialDefinition.AttributeNames.Add("age");
            credentialDefinition.AttributeNames.Add("height");
            credentialDefinition.AttributeNames.Add("sex");
            credentialDefinition.AttributeNames.Add("name");

            IssuerCredentialDefinition result = issuer.CreateStoreCredentialDef(trustAnchor, credentialDefinition);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.SchemaId);
            Assert.IsNotNull(result.Tag);
            Assert.AreEqual(credentialDefinition.Tag, result.Tag);
            Assert.IsNotNull(result.Value);
            Assert.IsNotNull(result.Value.Primary);
            Assert.IsNotNull(result.Value.Primary.r);
            Assert.IsNotNull(result.Value.Primary.r.MasterSecret);
            Assert.AreEqual(credentialDefinition.AttributeNames.Count, result.Value.Primary.r.Attributes.Count);

            // clean up
            // Close and delete wallet
            // Close pool
            // Delete pool ledger config
            // SEE Cleanup() above
        }
    }
}
