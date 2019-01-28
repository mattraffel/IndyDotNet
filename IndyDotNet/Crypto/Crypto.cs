using System;
using System.Collections.Generic;
using IndyDotNet.Did;
using IndyDotNet.Utils;
using IndyDotNet.Wallet;
using Newtonsoft.Json;

namespace IndyDotNet.Crypto
{
    internal class Crypto : ICrypto
    {
        private IWallet _wallet;

        internal Crypto(IWallet wallet)
        {
            _wallet = wallet;
        }

        public string PackMessage(List<IDid> recipients, IDid sender, string message)
        {

            List<string> recipientsVerkey = new List<string>();
            foreach(IDid did in recipients)
            {
                recipientsVerkey.Add(did.VerKey);
            }

            string recipientsJson = recipientsVerkey.ToJson();
            byte[] messageBytes = System.Text.UTF8Encoding.UTF8.GetBytes(message);

            var byteResult = CryptoAsync.PackMessageAsync(_wallet, recipientsJson, sender.VerKey, messageBytes).Result;

            string jsonResult = System.Text.UTF8Encoding.UTF8.GetString(byteResult);

            return jsonResult;
        }

        public string PackMessage(IDid recipient, IDid sender, string message)
        {
            List<IDid> list = new List<IDid>();
            list.Add(recipient);
            return PackMessage(list, sender, message);
        }

        public UnpackedMessage UnpackMessage(string packedMessage)
        {
            byte[] messageBytes = System.Text.UTF8Encoding.UTF8.GetBytes(packedMessage);
            var byteResult = CryptoAsync.UnpackMessageAsync(_wallet, messageBytes).Result;

            string jsonResult = System.Text.UTF8Encoding.UTF8.GetString(byteResult);

            return JsonConvert.DeserializeObject<UnpackedMessage>(jsonResult);

        }
    }
}
