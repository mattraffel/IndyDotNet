using System;
namespace IndyDotNet.Exceptions
{
    /// <summary>
    /// ErrorCode 212
    /// </summary>
    public class WalletItemNotFoundException : IndyException 
    { 
        public WalletItemNotFoundException() : base("WalletItemNotFoundError", ErrorCode.WalletItemNotFoundError) {}
    }
}
