using System;
using System.Collections.Generic;
using IndyDotNet.Crypto;
using IndyDotNet.Did;
using IndyDotNet.Pool;
using IndyDotNet.Utils;
using IndyDotNet.Wallet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Utils;

namespace Tests.CryptoTests
{
    [TestClass]
    public class AllCryptoTests
    {
        const string MESSAGE_TO_SEND = "This is the message we are sending";

        private IDid _senderDid;
        private IPool _pool;
        private IWallet _wallet;
        private List<string> _filesCreated = new List<string>();

        [TestInitialize]
        public void Initialize()
        {
            string file = PoolUtils.GenerateGenesisFile();
            _filesCreated.Add(file);

            _pool = IndyDotNet.Pool.Factory.GetPool("AllCryptoTestsPool", file);
            _pool.Create();
            _pool.Open();

            WalletConfig config = new WalletConfig()
            {
                Id = "AllCryptoTestsWalletId"
            };

            WalletCredentials credentials = new WalletCredentials()
            {
                Key = "8dvfYSt5d1taSd6yJdpjq4emkwsPDDLYxkNFysFD2cZY",
                KeyDerivationMethod = KeyDerivationMethod.RAW
            };

            _wallet = IndyDotNet.Wallet.Factory.GetWallet(config, credentials);
            _wallet.Create();
            _wallet.Open();

            IdentitySeed seed = new IdentitySeed()
            {
                Seed = "00000000000000000000000000000My1"
            };

            _senderDid = IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, seed);
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
                Logger.Warn($"AllCrypto failed to cleanup {ex.Message}");
            }
        }

        [TestMethod]
        public void PackMessageSucceeds()
        {
            ICrypto crypto = IndyDotNet.Crypto.Factory.GetCrypto(_wallet);
            PackedMessage packedMessage = crypto.PackMessage(_senderDid, _senderDid, MESSAGE_TO_SEND);

            Assert.IsNotNull(packedMessage, "crypto.PackMessage failed to return PackMessage instance");
        }

        [TestMethod]
        public void PackMessageToTwoRecipientsSucceeds()
        {

            IdentitySeed seed = new IdentitySeed()
            {
                Seed = "00000000000000000000000000000My2"
            };

            ICrypto crypto = IndyDotNet.Crypto.Factory.GetCrypto(_wallet);
            List<IDid> recipients = new List<IDid>();
            recipients.Add(_senderDid);
            recipients.Add(IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, seed));

            PackedMessage packedMessage = crypto.PackMessage(recipients, _senderDid, MESSAGE_TO_SEND);

            Assert.IsNotNull(packedMessage, "crypto.PackMessage failed to return PackMessage instance");
        }

        [TestMethod]
        public void UnpackPackedMessageSucceeds()
        {
            ICrypto crypto = IndyDotNet.Crypto.Factory.GetCrypto(_wallet);
            PackedMessage packedMessage = crypto.PackMessage(_senderDid, _senderDid, MESSAGE_TO_SEND);
            UnpackedMessage unpackedMessage = crypto.UnpackMessage(packedMessage);

            Assert.IsNotNull(unpackedMessage, $"did not get back an unpacked message");
            Assert.AreEqual(MESSAGE_TO_SEND, unpackedMessage.Message, "unpacked message is not the same as what was sent");

        }


        [TestMethod]
        public void UnpackPackedMessageWithMultipleRecipientsSucceeds()
        {
            ICrypto crypto = IndyDotNet.Crypto.Factory.GetCrypto(_wallet);
            List<IDid> recipients = new List<IDid>();

            IdentitySeed seed = new IdentitySeed()
            {
                Seed = "00000000000000000000000000000My2"
            };
            recipients.Add(IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, seed));

            seed = new IdentitySeed()
            {
                Seed = "00000000000000000000000000000My3"
            };
            recipients.Add(IndyDotNet.Did.Factory.CreateMyDid(_pool, _wallet, seed));


            PackedMessage packedMessage = crypto.PackMessage(recipients, _senderDid, MESSAGE_TO_SEND);
            UnpackedMessage unpackedMessage = crypto.UnpackMessage(packedMessage);

            Assert.IsNotNull(unpackedMessage, $"did not get back an unpacked message");
            Assert.AreEqual(MESSAGE_TO_SEND, unpackedMessage.Message, "unpacked message is not the same as what was sent");
        }

    }
}
