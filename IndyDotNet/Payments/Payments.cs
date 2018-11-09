using System;
using System.Collections.Generic;
using IndyDotNet.Wallet;
using Newtonsoft.Json.Linq;

namespace IndyDotNet.Payments
{
    internal class Payments : IPayments
    {
        private IWallet _wallet;

        internal Payments(IWallet wallet)
        {
            _wallet = wallet;
        }

        public string CreatePaymentAddress(string paymentMethod, string seed)
        {
            JObject config = new JObject
            {
                ["seed"] = seed
            };
            return PaymentsAsync.CreatePaymentAddressAsync(_wallet, paymentMethod, config.ToString()).Result;
        }

        public List<string> ListPaymentAddresses()
        {
            throw new NotImplementedException();
        }
    }
}
