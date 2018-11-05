using System;
namespace IndyDotNet.Exceptions
{
    /// <summary>
    /// ErrorCode 212, a call to retrieve something from the wallet failed on the
    /// key look up
    /// </summary>
    public class WalletItemNotFoundException : IndyException 
    { 
        public WalletItemNotFoundException() : base("WalletItemNotFoundError", ErrorCode.WalletItemNotFoundError) {}
    }
}
