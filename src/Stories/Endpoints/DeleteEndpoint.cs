using Stories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stories.Endpoints
{
    public class DeleteEndpoint
    {
        public Dictionary<string, string> Error { get; private set; }
        private readonly IEndpointPersistence endpointPersistence;

        public DeleteEndpoint(IEndpointPersistence endpointPersistence)
        {
            this.endpointPersistence = endpointPersistence;
        }

        public void Execute(string serialNumber)
        {
            Error = new Dictionary<string, string>();
            try
            {
                var endpoint = this.endpointPersistence.SearchBySerialNumber(serialNumber);
                if(endpoint == null)
                    Error.Add("Enpoint", "not found.");

                this.endpointPersistence.DeleteEndpoint(serialNumber);
            }
            catch (Exception Ex)
            {

                Error.Add("Something Unexpected Has Happened", Ex.ToString());
            }
        }
    }
}
