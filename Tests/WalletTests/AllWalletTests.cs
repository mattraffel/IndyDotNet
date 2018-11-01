using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using IndyDotNet.Pool;
using IndyDotNet.Wallet;
using IndyDotNet.Utils;

using Tests.Utils;

namespace Tests.WalletTests
{
    [TestClass]
    public class AllWalletTests
    {
        private List<string> _filesCreated = new List<string>();
        private List<IPool> _poolsCreated = new List<IPool>();
        private List<IWallet> _walletsCreated = new List<IWallet>();
        private string _genesisFileName = string.Empty;

        [TestInitialize]
        public void Initialize()
        {
            string file = PoolUtils.GenerateGenesisFile();
            _filesCreated.Add(file);
            _genesisFileName = file;
        }

        [TestCleanup]
        public void Cleanup()
        {
            try
            {
                foreach (string fileName in _filesCreated)
                {
                    System.IO.File.Delete(fileName);
                }

                foreach (IWallet wallet in _walletsCreated)
                {
                    wallet.Close();
                    wallet.Delete();
                }

                foreach (IPool pool in _poolsCreated)
                {
                    pool.Close();
                    pool.Delete();
                }
            }
            catch (Exception ex)
            {
                Logging.Error($"Filed to clean up tests {ex.Message}");
            }

        }

        [TestMethod]
        public void CreateWalletSuccessfully()
        {
            IPool pool = IndyDotNet.Pool.Factory.GetPool("CreateWalletPool", _genesisFileName);
            pool.Create();
            pool.Open();

            _poolsCreated.Add(pool);

            WalletConfig config = new WalletConfig()
            {
                Id = "CreateWalletID"
            };

            WalletCredentials credentials = new WalletCredentials()
            {
                Key = "8dvfYSt5d1taSd6yJdpjq4emkwsPDDLYxkNFysFD2cZY",
                KeyDerivationMethod = KeyDerivationMethod.RAW
            };

            IWallet wallet = IndyDotNet.Wallet.Factory.GetWallet(config, credentials);
            wallet.Create();
            _walletsCreated.Add(wallet);
        }

        [TestMethod]
        public void OpenCreatedWalletSuccessfully()
        {
            IPool pool = IndyDotNet.Pool.Factory.GetPool("OpenCreatedWalletPool", _genesisFileName);
            pool.Create();
            pool.Open();

            _poolsCreated.Add(pool);

            WalletConfig config = new WalletConfig()
            {
                Id = "OpenCreateWalletID"
            };

            WalletCredentials credentials = new WalletCredentials()
            {
                Key = "8dvfYSt5d1taSd6yJdpjq4emkwsPDDLYxkNFysFD2cZY",
                KeyDerivationMethod = KeyDerivationMethod.RAW
            };

            IWallet wallet = IndyDotNet.Wallet.Factory.GetWallet(config, credentials);
            wallet.Create();
            _walletsCreated.Add(wallet);

            wallet.Open();
        }

        /// <summary>
        /// rm -rf ~/.indy_client/pool && rm -rf ~/.indy_client/wallet
        /// </summary>
        [TestMethod]
        public void DeleteOpenWalletSuccessfully()
        {
            IPool pool = IndyDotNet.Pool.Factory.GetPool("DeleteCreatedWalletPool", _genesisFileName);
            pool.Create();
            pool.Open();

            _poolsCreated.Add(pool);

            WalletConfig config = new WalletConfig()
            {
                Id = "DeleteCreateWalletID"
            };

            WalletCredentials credentials = new WalletCredentials()
            {
                Key = "8dvfYSt5d1taSd6yJdpjq4emkwsPDDLYxkNFysFD2cZY",
                KeyDerivationMethod = KeyDerivationMethod.RAW
            };

            IWallet wallet = IndyDotNet.Wallet.Factory.GetWallet(config, credentials);
            wallet.Create();
            wallet.Open();
            wallet.Close();
            wallet.Delete();
        }

        [TestMethod]
        public void ExportWalletSuccessfully()
        {
            IPool pool = IndyDotNet.Pool.Factory.GetPool("DeleteCreatedWalletPool", _genesisFileName);
            pool.Create();
            pool.Open();

            _poolsCreated.Add(pool);

            WalletConfig config = new WalletConfig()
            {
                Id = "DeleteCreateWalletID"
            };

            WalletCredentials credentials = new WalletCredentials()
            {
                Key = "8dvfYSt5d1taSd6yJdpjq4emkwsPDDLYxkNFysFD2cZY",
                KeyDerivationMethod = KeyDerivationMethod.RAW
            };

            IWallet wallet = IndyDotNet.Wallet.Factory.GetWallet(config, credentials);
            wallet.Create();
            wallet.Open();
            _walletsCreated.Add(wallet);

            WalletImportExportConfig exportConfig = new WalletImportExportConfig()
            {
                Key = "8dvfYSt5d1taSd6yJdpjq4emkwsPDDLYxkNFysFD2cZY",
                Path = System.IO.Path.GetTempFileName()
            };

            wallet.Export(exportConfig);
            _filesCreated.Add(exportConfig.Path);

            Assert.IsTrue(System.IO.File.Exists(exportConfig.Path));
        }
    }
}
