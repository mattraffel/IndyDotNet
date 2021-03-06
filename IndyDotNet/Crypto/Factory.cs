﻿using System;
using IndyDotNet.Wallet;

namespace IndyDotNet.Crypto
{
    public static class Factory
    {
        public static ICrypto GetCrypto(IWallet wallet)
        {
            return new Crypto(wallet);
        }
    }
}
