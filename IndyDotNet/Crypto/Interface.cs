using System;
using System.Collections.Generic;
using IndyDotNet.Did;

namespace IndyDotNet.Crypto
{
    public interface ICrypto
    {
        PackedMessage PackMessage(List<IDid> recipients, IDid sender, string message);
        PackedMessage PackMessage(IDid recipient, IDid sender, string message);
        UnpackedMessage UnpackMessage(PackedMessage packedMessage);
    }
}
