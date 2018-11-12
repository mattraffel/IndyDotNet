using System;
namespace IndyDotNet.Ledger
{
    public static class Factory
    {
        /// <summary>
        /// gets the API interface for Nym functions
        /// </summary>
        /// <returns></returns>
        public static INymLedger CreateBuildNymLedger()
        {
            return new LedgerInstance();
        }

        public static IGetNymLedger CreateGetNymLedger()
        {
            return new GetNymLedgerInstance();
        }

        public static IDDOLedger CreateDDOLedger()
        {
            return new LedgerInstance();
        }
    }
}
