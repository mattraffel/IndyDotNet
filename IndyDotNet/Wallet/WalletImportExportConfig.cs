using System;
namespace IndyDotNet.Wallet
{
    /// <summary>
    /// For Wallet Export/Import functions
    /// </summary>
    public class WalletImportExportConfig
    {
        /// <summary>
        /// Path of the file for/of wallet content
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Passphrase used to derive import/export key
        /// </summary>
        public string Key { get; set; }
    }
}
