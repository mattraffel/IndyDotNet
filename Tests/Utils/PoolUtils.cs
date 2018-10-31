using System;
using System.Collections.Generic;
using IndyDotNet.Pool;

namespace Tests.Utils
{
    public static class PoolUtils
    {
        private static string LOCALHOST = "127.0.0.1";

        public static void GenerateGenesisFile(string fileName)
        {

        }

        public static GenesisTransaction GetNode1GenesisTransaction()
        {
            GenesisTransaction transaction = new GenesisTransaction()
            {
                reqSignature = new ReqSignature(),
                txn = new Txn()
                {
                    data = new TxnData()
                    {
                        data = new Data() 
                        {
                            alias = "Node1",
                            blskey = "4N8aUNHSgjQVgkpm8nhNEfDf6txHznoYREg9kirmJrkivgL4oSEimFF6nsQ6M41QvhM2Z33nves5vfSn9n1UwNFJBYtWVnHYMATn76vLuL3zU88KyeAYcHfsih3He6UHcXDxcaecHVz6jhCYz1P2UZn2bDVruL5wXpehgBfBaLKm3Ba",
                            blskey_pop = "RahHYiCvoNCtPTrVtP7nMC5eTYrsUA8WjXbdhNc8debh1agE9bGiJxWBXYNFbnJXoXhWFMvyqhqhRoq737YQemH5ik9oL7R4NTTCz2LEZhkgLJzB3QRQqJyBNyv7acbdHrAT8nQ9UkLbaVL9NBpnWXBTw4LEMePaSHEw66RzPNdAX1",
                            client_ip = LOCALHOST,
                            client_port = 9702,
                            node_ip = LOCALHOST,
                            node_port = 9701,
                            services = new List<string>()
                        },
                        dest = "Gw6pDLhcBcoQesN72qfotTgFa7cbuqZpkX3Xo6pLhPhv"
                    },
                    metadata = new Metadata()
                    {
                        from = "Th7MpTaRZVRYnPiabds81Y"
                    },
                    type = "0"
                },
                txnMetadata = new TxnMetadata()
                {
                    seqNo = 1,
                    txnId = "fea82e10e894419fe2bea7d96296a6d46f50f93f9eeda954ec461b2ed2950b62"
                },
                ver = "1"
            };

            return transaction;
        }
    }
}
