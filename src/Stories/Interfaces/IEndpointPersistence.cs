using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stories.Interfaces
{
    public interface IEndpointPersistence
    {
        void CreateEndpoint(Endpoint endpoint);
        List<Endpoint> ListAll();
        Endpoint SearchBySerialNumber(string serialNumber);
        void EditEndpoint(Endpoint endpoint);
        void DeleteEndpoint(string serialNumber);
    }
}
