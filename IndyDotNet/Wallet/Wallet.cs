using System;

using IndyDotNet.Utils;

namespace IndyDotNet.Wallet
{
    /// <summary>
    /// Implementation of IWallet providing syncronous calls for all wallet methods  
    /// 
    /// Facade to the async methods.
    /// <see cref="IWallet"/> for description of each function
    /// </summary>
    public class WalletInstance : IWallet
    {
        private WalletAsync _asyncHandle = null;
        private readonly WalletConfig _config;
        private readonly WalletCredentials _credentials;

        protected internal WalletInstance(WalletConfig config, WalletCredentials credentials) 
        {
            _config = config;
            _credentials = credentials;
        }

        public IntPtr Handle { get; internal set; } = IntPtr.Zero;

        public void Close() 
        {
            if (null == _asyncHandle) return;

            _asyncHandle.CloseAsync().Wait();
            _asyncHandle = null;
            Handle = IntPtr.Zero;
        }

        public void Create()
        {
            (string config, string credentials) = GetConfigCredentialJson();

            Logger.Info($"config is {config}");
            Logger.Info($"credentials is {credentials}");

            WalletAsync.CreateWalletAsync(config, credentials).Wait();
        }

        public void Delete()
        {
            (string config, string credentials) = GetConfigCredentialJson();

            WalletAsync.DeleteWalletAsync(config, credentials).Wait();
        }

        public void Export(WalletImportExportConfig exportConfig)
        {
            if (null == _asyncHandle) return;

            string export = exportConfig.ToJson();

            _asyncHandle.ExportAsync(export);
        }

        public void Import(WalletImportExportConfig importExportConfig)
        {
            (string config, string credentials) = GetConfigCredentialJson();
            string import = importExportConfig.ToJson();

            WalletAsync.ImportAsync(config, credentials, import).Wait();
        }

        public void Open()
        {
            (string config, string credentials) = GetConfigCredentialJson();
            _asyncHandle = WalletAsync.OpenWalletAsync(config, credentials).Result;
            Handle = _asyncHandle.Handle;
        }

        private (string, string) GetConfigCredentialJson()
        {
            string config = _config.ToJson();
            string credentials = _credentials.ToJson();

            return (config, credentials);
        }
    }
}
