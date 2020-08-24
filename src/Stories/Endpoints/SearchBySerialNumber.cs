using Domain;
using Stories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stories.Endpoints
{
    public class SearchBySerialNumber
    {
        public Dictionary<string, string> Erros { get; private set; }
        private readonly IEndpointPersistence endpointPersistence;

        public SearchBySerialNumber(IEndpointPersistence endpointPersistence)
        {
            this.endpointPersistence = endpointPersistence;
        }

        public Endpoint Executar(string serialNumber)
        {
            Erros = new Dictionary<string, string>();
            var endpoint = this.endpointPersistence.SearchBySerialNumber(serialNumber);
            if(endpoint == null)
            {
                Erros.Add("Endpoint", "Não encontrado.");
            }
            return endpoint;
        }
    }
}
