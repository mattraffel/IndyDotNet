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
            Logger.Info("\n\n***************** Step 11: getNymSubmitRequest\n\n");
            GetNymSubmitReponse getNymSubmitRequest = getNymLedger.SubmitRequest(_pool, getNymRequest);

            /* NOTE: this is what we get back
            {  
               "result":{  
                  "type":"105",
                  "data":"{\"dest\":\"7TCtk84rc8nuCPAed6gQLu\",\"identifier\":\"Th7MpTaRZVRYnPiabds81Y\",\"role\":null,\"seqNo\":19,\"txnTime\":1542061488,\"verkey\":null}",
                  "dest":"7TCtk84rc8nuCPAed6gQLu",
                  "identifier":"FhU8A46Afy7WwssQNAnko",
                  "state_proof":{  
                     "proof_nodes":"+QHe+IigPf8Wo24L8qgak4e4rUKm9i41bXqHBKOPoFmYp3exnK64ZfhjuGF7ImlkZW50aWZpZXIiOiJUaDdNcFRhUlpWUlluUGlhYmRzODFZIiwicm9sZSI6bnVsbCwic2VxTm8iOjE5LCJ0eG5UaW1lIjoxNTQyMDYxNDg4LCJ2ZXJrZXkiOm51bGx9+QFRoNPSP24JsVps7QufK62cHm4MLrVBpYu1VMlThcJrixajgKAbc\/Sy8usm4GZWRD4p3F8XPXas0ri5Luf+8Gi4JBP+HqCamro+9EH\/vrNION29c750BMkCYdpXzasRUbZmgA75MKCY86tp91Ze1HhynvcSwZGPMvmkqlA7mj0iz7YnSafoB6ACG8f0w2NsuDibWYibc1TYySAgUKSeIevHF6wVZdMBL6C2VI1QLp0G3pYnkRhImlTL3WqhsQP23Xp\/QKV+DR90KoCAgICgJHIm3oUOYlDrQlw95UDkRdOc2tGIsE9g2r12AjpJiUKAoH0lXE47VtUlFvwnCC5rgY878m6TpeEZTJIKd4SUxXtqoBvSoTludXD0XkhTPm4YxfCcAdCaiDvkzM8w6O4v5\/e1oDs6GXxRL8inD2b3RY1v\/ufksDHNqfFKaK2MEIjNIZwagA==",
                     "multi_signature":{  
                        "signature":"RLVggoseps7osSyQ9F3cjvhiQoLc1Dbj9QwGUJhMbfnGNC2xTsuEU7HWUYvf6fmivkZ954n7q8kt66bCGRaCK9Hx8U8YETMUGurDvZkpikEa4rW988YpSujRFMB9XnLWU6ycgTA5FMVW2WatmsbuNALULkMB7NNR9u2RarvBwe74oh",
                        "participants":[  
                           "Node4",
                           "Node3",
                           "Node2"
                        ],
                        "value":{  
                           "state_root_hash":"68qc6wDWyKHiWW6YdJbYCgVUcrT3tnrrm3a1MVpWtc4f",
                           "txn_root_hash":"DqfdnzrLK145YnB68D2iZWsvzVQ2Jx9UZJ93h97YVU5F",
                           "timestamp":1542061488,
                           "pool_state_root_hash":"NCGqbfRWDWtLB2bDuL6TC5BhrRdQMc5MyKdXQqXii44",
                           "ledger_id":1
                        }
                     },
                     "root_hash":"68qc6wDWyKHiWW6YdJbYCgVUcrT3tnrrm3a1MVpWtc4f"
                  },
                  "seqNo":19,
                  "reqId":1542061488708038000,
                  "txnTime":1542061488
               },
               "op":"REPLY"
            }
            */

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
