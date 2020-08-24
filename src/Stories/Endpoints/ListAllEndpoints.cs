using Domain;
using Stories.Interfaces;
using System;
using System.Collections.Generic;

namespace Stories
{
    public class ListAllEndpoints
    {
        private readonly IEndpointPersistence endpointPersistence;

        public ListAllEndpoints(IEndpointPersistence endpointPersistence)
        {
            this.endpointPersistence = endpointPersistence;
        }

        public List<Endpoint> Executar()
        {
            var endpoints = this.endpointPersistence.ListAll();
            return endpoints;
        }
    }
}
