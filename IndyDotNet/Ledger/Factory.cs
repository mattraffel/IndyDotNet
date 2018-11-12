using System;
namespace IndyDotNet.Ledger
{
    public static class Factory
    {
        /// <summary>
        /// gets the API interface for Nym functions
        /// </summary>
        /// <returns></returns>
        public static INymLedger GetNymLedger()
        {
            return new LedgerInstance();
        }
    }
}
