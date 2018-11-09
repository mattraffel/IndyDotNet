using System;
using IndyDotNet.Wallet;

namespace IndyDotNet.Payments
{
    public static class Factory
    {
        public static IPayments CreatePayment(IWallet wallet)
        {
            return new Payments(wallet);
        }
    }
}
