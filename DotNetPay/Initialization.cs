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
        ///
        /// The LibIndy pattern is consumers explicityly tell the payment 
        /// libraries to register with LibIndy.  LibIndy does not automatically
        /// discover payment libraries.
        /// 
        /// THEREFORE to use DotNetPay, call this method
        /// </summary>
        /// <returns>true if regisration of DotNetPay succeeds with LibIndy</returns>
        public static bool Register()
        {
            if (null != _handler)
            {
                Logger.Warn("DotNetPay Payment Handler is already registered");
                return false;
            }

            Logger.Info("Registering DotNetPay Payment Handlers");
            _handler = new PaymentHandler();
            return Factory.RegisterPaymentHandler(_handler);
        }
    }
}
