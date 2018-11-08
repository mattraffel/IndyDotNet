using System;
using IndyDotNet.Utils;

namespace DotNetPay
{
    public static class Initialization
    {
        /// <summary>
        /// Ensures LibIndy understand DotNetPay will respond to payment API handler 
        /// requests for payment address of "DNP"
        /// </summary>
        /// <returns>The register.</returns>
        public static bool Register()
        {
            Logger.Info("Registering DotNetPay Payment Handlers");

            return false;
        }
    }
}
