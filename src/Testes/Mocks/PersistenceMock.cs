using Adapters;
using Moq;
using Stories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace Tests.Mocks
{
    public class PersistenceMock
    {
        public IEndpointPersistence CreateEndpoint()
        {
            var endpoint = ModelsMock.EndpointMock();

            var endpointPersistence = new Mock<IEndpointPersistence>();
            endpointPersistence.Setup(x => x.CreateEndpoint(endpoint));
            return endpointPersistence.Object;
        }

        public IEndpointPersistence EditStateEndpoint()
        {
            var endpoint = ModelsMock.EndpointMock();
            var endpointPersistence = new Mock<IEndpointPersistence>();

            endpointPersistence.Setup(x => x.EditEndpoint(endpoint));
            endpointPersistence.Setup(x => x.SearchBySerialNumber(endpoint.SerialNumber)).Returns(endpoint);
            return endpointPersistence.Object;
        }
        public IEndpointPersistence SearchBySerialNumber()
        {
            var endpoint = ModelsMock.EndpointMock();
            var endpointPersistence = new Mock<IEndpointPersistence>();
            endpointPersistence.Setup(x => x.SearchBySerialNumber(endpoint.SerialNumber)).Returns(endpoint);
            return endpointPersistence.Object;
        }

        public IEndpointPersistence DeleteEndpoint()
        {
            var endpoint = ModelsMock.EndpointMock();
            var endpointPersistence = new Mock<IEndpointPersistence>();
            endpointPersistence.Setup(x => x.DeleteEndpoint(endpoint.SerialNumber));
            endpointPersistence.Setup(x => x.SearchBySerialNumber(endpoint.SerialNumber)).Returns(endpoint);
            return endpointPersistence.Object;
        }

        public IEndpointPersistence ListAll()
        {
            var endpoints = ModelsMock.ListEndpointMock();

            var endpointPersistence = new Mock<IEndpointPersistence>();
            endpointPersistence.Setup(x => x.ListAll()).Returns(endpoints);
            return endpointPersistence.Object;
        }
    }
}
