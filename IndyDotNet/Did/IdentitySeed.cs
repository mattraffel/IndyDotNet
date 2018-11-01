using System;
namespace IndyDotNet.Did
{
    public class IdentitySeed
    {
        public bool CID { get; set; }
        public string CryptoType { get; set; }
        public string Did { get; set; }
        /// <summary>
        /// TODO:  there is a legal size for seed.  add validation
        /// </summary>
        /// <value>The seed.</value>
        public string Seed { get; set; }
    }
}
