using System;
using IndyDotNet.PaymentHandler;
using IndyDotNet.Utils;

namespace DotNetPay
{
    public static class Initialization
    {
        private static IPaymentHandler _handler;

        /// <summary>
        /// Ensures LibIndy understand DotNetPay will respond to payment API handler 
        /// requests for payment address of "DNP"
        /// </summary>
        /// <returns>The register.</returns>
        public static bool Register()
        {
            Logger.Info("Registering DotNetPay Payment Handlers");
            _handler = new PaymentHandler();
            return Factory.RegisterPaymentHandler(_handler);
        }
    }
}
