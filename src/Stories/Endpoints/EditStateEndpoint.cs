using Stories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stories.Endpoints
{
    public class EditStateEndpoint
    {
        public Dictionary<string, string> Error { get; private set; }
        private readonly IEndpointPersistence endpointPersistence;

        public EditStateEndpoint(IEndpointPersistence endpointPersistence)
        {
            this.endpointPersistence = endpointPersistence;
        }

        public void Execute(string serialNumber, int state)
        {
            Error = new Dictionary<string, string>();

            try
            {
                var endpoint = this.endpointPersistence.SearchBySerialNumber(serialNumber);
                if (endpoint == null)
                    Error.Add("Endpoint", "not found.");

                endpoint.EditState(state);
                this.endpointPersistence.EditEndpoint(endpoint);
            }
            catch (Exception ex)
            {

                Error.Add("Something Unexpected Has Happened", ex.Message);
            }
            
        }

    }

}
