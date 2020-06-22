using System;
namespace IndyDotNet.Wallet
{
    /// <summary>
    /// Instance methods for wallet.
    /// 
    /// A wallet stores key value records.
    /// 
    /// TODO:  
    /// GenerateKey
    /// </summary>
    public interface IWallet
    {
        /// <summary>
        /// Wallet handle.  Consumers shouldn't need to directly reference this
        /// </summary>
        /// <value>The handle.</value>
        int Handle { get; }
        /// <summary>
        /// Closes a wallet
        /// </summary>
        void Close();
        /// <summary>
        /// Creates a new wallet with the name provided in the WalletConfig.Id
        /// field provided to the factory
        /// </summary>
        void Create();
        /// <summary>
        /// Deletes a wallet.  There is no confirmation here.  Consumers are
        /// expected to handle confirmation prior to calling Delete();
        /// </summary>
        void Delete();
        /// <summary>
        /// "backup" wallet function, copies wallet data to a file
        /// </summary>
        void Export(WalletImportExportConfig exportConfig);
        /// <summary>
        /// "backup" wallet function, copies data from file to wallet
        /// </summary>
        void Import(WalletImportExportConfig importExportConfig);
        /// <summary>
        /// Opens a wallet
        /// </summary>
        void Open();
    }
}
