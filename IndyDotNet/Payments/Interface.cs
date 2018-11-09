using System;
using System.Collections.Generic;

namespace IndyDotNet.Payments
{
    public interface IPayments
    {
        string CreatePaymentAddress(string paymentMethod, string seed);
        List<string> ListPaymentAddresses();
    }
}
