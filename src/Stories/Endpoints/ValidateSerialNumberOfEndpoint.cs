using Stories.Interfaces;
using System.Collections.Generic;

namespace Stories.Endpoints
{
    public class ValidateSerialNumberOfEndpoint
    {
        public Dictionary<string, string> Error { get; private set; } 
        private readonly IEndpointPersistence endpointPersistence;

        public ValidateSerialNumberOfEndpoint(IEndpointPersistence endpointPersistence)
        {
            this.endpointPersistence = endpointPersistence;
        }

        public int Execute(string serialNumber)
        {
            Error = new Dictionary<string, string>();
            var endpoint = this.endpointPersistence.SearchBySerialNumber(serialNumber);
            
            if (endpoint != null)
            {
                this.Error.Add("Endpoint", "An endpoint with this serial number already exists");
                return 0;
            }

            switch (serialNumber)
            {
                case "NSX1P2W":
                    return 16;

                case "NSX1P3W":
                    return 17;

                case "NSX2P3W":
                    return 18;

                case "NSX3P4W":
                    return 19;

                default:
                    return 0;
            }

        }
    }
}
