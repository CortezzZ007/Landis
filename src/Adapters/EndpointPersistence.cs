using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Stories.Interfaces;

namespace Adapters
{
    public class EndpointPersistence : IEndpointPersistence
    {
        private readonly List<Endpoint> endpoints;

        public EndpointPersistence()
        {
            this.endpoints = endpoints == null ? new List<Endpoint>() : endpoints;
        }

        public void CreateEndpoint(Endpoint endpoint)
        {
            this.endpoints.Add(endpoint);    
        }

        public void EditEndpoint(Endpoint endpoint)
        {
            var endpointEncontrado = this.endpoints.FirstOrDefault(x => x.SerialNumber == endpoint.SerialNumber);
            endpointEncontrado = endpoint;
        }

        public Endpoint SearchBySerialNumber(string serialNumber)
        {
            var endpoint = this.endpoints.FirstOrDefault(x => x.SerialNumber == serialNumber);
            return endpoint;
        }

        public List<Endpoint> ListAll()
        {
            return this.endpoints;
        }

        public void DeleteEndpoint(string serialNumber)
        {
            var endpoint = this.endpoints.FirstOrDefault(x => x.SerialNumber == serialNumber);
            this.endpoints.Remove(endpoint);
        }

    }
}
