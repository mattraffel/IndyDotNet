using System;
namespace IndyDotNet.Pool
{
    public static class Factory
    {
        public static IPool GetPool(string poolName, string genesisTxnFileName = "") 
        {
            return new PoolInstance()
            {
                Name = poolName, 
                GenesisFileName = genesisTxnFileName
            };
        }
    }
}
