using Domain;
using Stories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stories.Endpoints
{
    public class CreateEndpoint
    {
        public Dictionary<string, string> Erros { get; private set; }
        private readonly IEndpointPersistence endpointPersistence;

        public CreateEndpoint(IEndpointPersistence endpointPersistence)
        {
            this.endpointPersistence = endpointPersistence;
        }

        public void Executar(Endpoint endpoint)
        {
            Erros = new Dictionary<string, string>();
            try
            {
                this.endpointPersistence.CreateEndpoint(endpoint);
            }
            catch (Exception ex)
            {

                Erros.Add("Algo Inesperado Aconteceu", ex.Message);
            }
        }
    }
}
