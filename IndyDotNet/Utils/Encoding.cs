using System;
using System.Security.Cryptography;
using System.Text;
using IndyDotNet.Internal.OpenSSL;

namespace IndyDotNet.Utils
{
    public static class Encoding
    {
        /// <summary>
        /// SHA256 encoded string converted to decimals
        /// </summary>
        /// <returns>The encode.</returns>
        /// <param name="asci">Asci.</param>
        public static string AsSha256Decimal(this string asci)
        {
            if (asci.IsDigitsOnly())
                return asci;

            byte[] hashBytes = asci.AsHashSha256();
            return new BigNumber(hashBytes).ToString();
        }

        public static byte[] AsHashSha256(this string text)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            return hashstring.ComputeHash(bytes);            
        }

        /// <summary>
        /// taken from https://stackoverflow.com/questions/7461080/fastest-way-to-check-if-string-contains-only-digits
        /// </summary>
        public static bool IsDigitsOnly(this string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }

}
