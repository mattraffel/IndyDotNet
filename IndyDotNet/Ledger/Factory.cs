using System;
namespace IndyDotNet.Ledger
{
    public static class Factory
    {
        public static ILedger GetLedger()
        {
            return new LedgerInstance();
        }
    }
}
