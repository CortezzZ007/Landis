using Domain;
using Stories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stories.Endpoints
{
    public class SearchBySerialNumber
    {
        public Dictionary<string, string> Error { get; private set; }
        private readonly IEndpointPersistence endpointPersistence;

        public SearchBySerialNumber(IEndpointPersistence endpointPersistence)
        {
            this.endpointPersistence = endpointPersistence;
        }

        public Endpoint Execute(string serialNumber)
        {
            Error = new Dictionary<string, string>();
            var endpoint = this.endpointPersistence.SearchBySerialNumber(serialNumber);
            if(endpoint == null)
            {
                Error.Add("Endpoint", "not found.");
            }
            return endpoint;
        }
    }
}
