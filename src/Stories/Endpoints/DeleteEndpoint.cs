using Stories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stories.Endpoints
{
    public class DeleteEndpoint
    {
        public Dictionary<string, string> Erros { get; private set; }
        private readonly IEndpointPersistence endpointPersistence;

        public DeleteEndpoint(IEndpointPersistence endpointPersistence)
        {
            this.endpointPersistence = endpointPersistence;
        }

        public void Executar(string serialNumber)
        {
            Erros = new Dictionary<string, string>();
            try
            {
                var endpoint = this.endpointPersistence.SearchBySerialNumber(serialNumber);
                if(endpoint == null)
                    Erros.Add("Endpoint", "não encontrado.");

                this.endpointPersistence.DeleteEndpoint(serialNumber);
            }
            catch (Exception Ex)
            {

                Erros.Add("Aconteceu algo inesperado", Ex.ToString());
            }
        }
    }
}
