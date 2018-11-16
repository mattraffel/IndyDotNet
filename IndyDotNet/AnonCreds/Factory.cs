using System;
namespace IndyDotNet.AnonCreds
{
    public static class Factory
    {
        public static IIssuerAnonCreds GetIssuerAnonCreds()
        {
            return new IssuerAnonCreds();
        }

        public static IProverAnonCreds GetProverAnonCreds()
        {
            throw new NotImplementedException();
        }
    }
}
