using System;
namespace IndyDotNet.Pool
{
    /// <summary>
    /// Instance methods for a pool.   
    /// 
    /// TODO: are these static methods, instance or what?
    /// ListPools
    /// SetProtocolVersion
    /// </summary>
    public interface IPool : IDisposable
    {
        /// <summary>
        /// The pool name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }
        /// <summary>
        /// The name of the genesis file.
        /// </summary>
        /// <value>The name of the genesis file.</value>
        string GenesisFileName { get; }
        /// <summary>
        /// Maps to CreatePoolLedgerConfigAsync
        /// </summary>
        void Create();
        /// <summary>
        /// Maps OpenPoolLedgerAsync
        /// </summary>
        void Open();
        /// <summary>
        /// Maps DeletePoolLedgerConfigAsync
        /// </summary>
        void Delete();
        /// <summary>
        /// Maps to CloseAsync
        /// </summary>
        void Close();
        /// <summary>
        /// Maps to RefreshAsync
        /// </summary>
        void Refresh();
    }
}
