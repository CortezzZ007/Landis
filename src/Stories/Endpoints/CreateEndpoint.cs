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
        public Dictionary<string, string> Error { get; private set; }
        private readonly IEndpointPersistence endpointPersistence;

        public CreateEndpoint(IEndpointPersistence endpointPersistence)
        {
            this.endpointPersistence = endpointPersistence;
        }

        public void Execute(Endpoint endpoint)
        {
            Error = new Dictionary<string, string>();
            try
            {
                this.endpointPersistence.CreateEndpoint(endpoint);
            }
            catch (Exception ex)
            {

                Error.Add("Something Unexpected Has Happened", ex.Message);
            }
        }
    }
}
