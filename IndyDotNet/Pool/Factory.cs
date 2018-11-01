using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace IndyDotNet.Pool
{
    public static class Factory
    {
        public static IPool GetPool(string poolName, string genesisTxnFileName = "", int protocolVersion = 2) 
        {
            return new PoolInstance()
            {
                Name = poolName, 
                GenesisFileName = genesisTxnFileName,
                ProtocolVersion = protocolVersion
            };
        }

        public static List<IPool> ListPools() 
        {
            List<IPool> pools = new List<IPool>();

            var poolArray = JArray.Parse(PoolAsync.ListPoolsAsync().Result);

            /*
                 json will be like this: [{\"pool\":\"qPKbDswvLm\"},{\"pool\":\"m4f5A8ADMk\"}
                 we want to convert those into IPool instances
             */
            foreach (var onePool in poolArray)
            {
                IPool pool = new PoolInstance()
                {
                    Name = onePool["pool"].ToString()
                };

                pools.Add(pool);
            }

            return pools;
        }
    }
}
