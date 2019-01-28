using System;
using System.Collections.Generic;
using IndyDotNet.Did;

namespace IndyDotNet.Crypto
{
    public interface ICrypto
    {
        string PackMessage(List<IDid> recipients, IDid sender, string message);
        string PackMessage(IDid recipient, IDid sender, string message);
        UnpackedMessage UnpackMessage(string packedMessage);
    }
}
