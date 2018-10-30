using System;

namespace IndyDotNet.Pool
{
    /// <summary>
    /// Implementation of IPool. 
    /// <see cref="IPool"/> for description of each function
    /// </summary>
    public class PoolInstance : IPool
    {
        #region constructors/destructors/cleanup
        protected internal PoolInstance() { }

        public override string ToString()
        {
            return Name;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~PoolInstance() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
        #endregion

        private PoolAsync _asyncHandle = null;
        public IntPtr Handle { get; protected internal set; }
        public string Name { get; protected internal set; }
        public string GenesisFileName { get; protected internal set; }
        public int ProtocolVersion { get; protected internal set; }

        public void Close()
        {
            if (null == _asyncHandle) return;

            _asyncHandle.CloseAsync().Wait();
        }

        public void Create()
        {
            PoolAsync.SetProtocolVersionAsync(ProtocolVersion).Wait();
            PoolAsync.CreatePoolLedgerConfigAsync(Name, CreateConfigJson()).Wait();
        }

        public void Delete()
        {
            PoolAsync.DeletePoolLedgerConfigAsync(Name).Wait();
        }

        public void Open()
        {
            _asyncHandle = PoolAsync.OpenPoolLedgerAsync(Name, CreateConfigJson()).Result;
            Handle = _asyncHandle.Handle;
        }

        public void Refresh()
        {
            if (null == _asyncHandle) return;

            _asyncHandle.RefreshAsync().Wait();
        }

        #region private methods
        private string CreateConfigJson() 
        {
            if (string.IsNullOrEmpty(GenesisFileName)) return null;

            return "{ \"genesis_txn\" : \"" + GenesisFileName + "\" }";
        }
        #endregion
    }
}
