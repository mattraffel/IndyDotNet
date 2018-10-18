using System;

namespace IndyDotNet.Pool
{
    public class PoolInstance : IPool
    {
        #region constructors/destructors/cleanup
        protected internal PoolInstance() { }

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

        public string Name { get; protected internal set; }
        public string GenesisFileName { get; protected internal set; }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Create()
        {
            PoolAsync.CreatePoolLedgerConfigAsync(Name, CreateConfigJson()).Wait();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        #region private methods
        private string CreateConfigJson() 
        {
            return "{ \"genesis_txn\" : \"" + GenesisFileName + "\" }";
        }
        #endregion
    }
}
