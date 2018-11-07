using System;
using IndyDotNet.Pool;
using IndyDotNet.Wallet;

namespace IndyDotNet.Did
{
    internal class EndpointInstance : IEndPoint
    {
        private IPool _pool;
        private IWallet _wallet;

        protected internal EndpointInstance(IPool pool, IWallet wallet, IDid did)
        {
            _pool = pool;
            _wallet = wallet;
            Did = did;
        }

        public IDid Did { get; internal set; }
        public string Address { get; internal set; }
        public string TransportKey { get; internal set; }

        public void Open()
        {
            EndpointForDidResult result = DidAsync.GetEndpointForDidAsync(_pool, _wallet, Did).Result;
            Address = result.Address;
            TransportKey = result.TransportKey;
        }

        public void Save(string address, string transportKey)
        {
            DidAsync.SetEndpointForDidAsync(_wallet, Did, address, transportKey).Wait();
            Address = address;
            TransportKey = transportKey;
        }
    }
}
