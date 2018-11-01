using System;
namespace IndyDotNet.Did
{
    internal class EndpointInstance : IEndPoint
    {
        protected internal EndpointInstance()
        {
        }

        public string Address { get; internal set; }
        public string TransportKey { get; internal set; }
    }
}
