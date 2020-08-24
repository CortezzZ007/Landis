using Stories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stories.Endpoints
{
    public class EditStateEndpoint
    {
        public Dictionary<string, string> Erros { get; private set; }
        private readonly IEndpointPersistence endpointPersistence;

        public EditStateEndpoint(IEndpointPersistence endpointPersistence)
        {
            this.endpointPersistence = endpointPersistence;
        }

        public void Executar(string serialNumber, int state)
        {
            Erros = new Dictionary<string, string>();

            try
            {
                var endpoint = this.endpointPersistence.SearchBySerialNumber(serialNumber);
                if (endpoint == null)
                    Erros.Add("Endpoint", "não encontrado.");

                endpoint.EditState(state);
                this.endpointPersistence.EditEndpoint(endpoint);
            }
            catch (Exception ex)
            {

                Erros.Add("Algo inesperado aconteceu", ex.Message);
            }
            
        }

    }

}
