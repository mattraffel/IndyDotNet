using System;
namespace IndyDotNet.Wallet
{
    public interface IWallet
    {
        void Close();
        void Create();
        void Delete();
        void Open();
    }
}
