using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Mocks
{
    public static class ModelsMock
    {
        public static Endpoint EndpointMock()
        {
            return new Endpoint("NSX1P2W",16, 1,"AE123",1);
        }

        public static List<Endpoint> ListEndpointMock()
        {
            var listEndpoint = new List<Endpoint>();
            listEndpoint.Add(new Endpoint("NSX1P2W", 16, 1, "AE123", 1));
            listEndpoint.Add(new Endpoint("NSX1P3W", 17, 1, "AE123", 2));
            return listEndpoint;
        }
    }
}
