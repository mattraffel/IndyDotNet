using System;
using System.Collections.Generic;

namespace IndyDotNet.Pool
{
    /// <summary>
    /// Genesis transaction data is required to create a pool.  
    /// <code>
    /// {
    ///   "reqSignature":{},
    ///   "txn":{
    ///      "data":{
    ///         "data":{
    ///            "alias":"Node1",
    ///            "blskey":"4N8aUNHSgjQVgkpm8nhNEfDf6txHznoYREg9kirmJrkivgL4oSEimFF6nsQ6M41QvhM2Z33nves5vfSn9n1UwNFJBYtWVnHYMATn76vLuL3zU88KyeAYcHfsih3He6UHcXDxcaecHVz6jhCYz1P2UZn2bDVruL5wXpehgBfBaLKm3Ba",
    ///            "blskey_pop":"RahHYiCvoNCtPTrVtP7nMC5eTYrsUA8WjXbdhNc8debh1agE9bGiJxWBXYNFbnJXoXhWFMvyqhqhRoq737YQemH5ik9oL7R4NTTCz2LEZhkgLJzB3QRQqJyBNyv7acbdHrAT8nQ9UkLbaVL9NBpnWXBTw4LEMePaSHEw66RzPNdAX1",
    ///            "client_ip":"{0}",
    ///            "client_port":9702,
    ///            "node_ip":"{1}",
    ///            "node_port":9701,
    ///            "services":[
    ///               "VALIDATOR"
    ///            ]
    ///         },
    ///         "dest":"Gw6pDLhcBcoQesN72qfotTgFa7cbuqZpkX3Xo6pLhPhv"
    ///      },
    ///      "metadata":{
    ///         "from":"Th7MpTaRZVRYnPiabds81Y"
    ///      },
    ///      "type":"0"
    ///   },
    ///   "txnMetadata":{
    ///      "seqNo":1,
    ///      "txnId":"fea82e10e894419fe2bea7d96296a6d46f50f93f9eeda954ec461b2ed2950b62"
    ///   },
    ///   "ver":"1"
    /// }
    /// </code>
    /// </summary>
    public class GenesisTransaction
    {
        public ReqSignature reqSignature { get; set; }
        public Txn txn { get; set; }
        public TxnMetadata txnMetadata { get; set; }
        public string ver { get; set; }
    }

    public class TxnMetadata
    {
        public int seqNo { get; set; }
        public string txnId { get; set; }
    }

    public class Txn
    {
        public TxnData data { get; set; }
        public Metadata metadata { get; set; }
        public string type { get; set; }
    }

    public class ReqSignature
    {
    }

    public class Data
    {
        public string alias { get; set; }
        public string blskey { get; set; }
        public string blskey_pop { get; set; }
        public string client_ip { get; set; }
        public int client_port { get; set; }
        public string node_ip { get; set; }
        public int node_port { get; set; }
        public List<string> services { get; set; }
    }

    public class TxnData
    {
        public Data data { get; set; }
        public string dest { get; set; }
    }

    public class Metadata
    {
        public string from { get; set; }
    }

}

