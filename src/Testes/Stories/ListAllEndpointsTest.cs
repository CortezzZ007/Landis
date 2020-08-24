using Stories;
using Stories.Endpoints;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Mocks;
using Xunit;

namespace Tests.Stories
{
    public class ListAllEndpointsTest
    {
        [Fact]
        public void MustReturnAlistWith2enpoints()
        {
            //arrange
            PersistenceMock persistenceMock = new PersistenceMock();
            persistenceMock.SearchBySerialNumber();
            var listAllEndpoints = new ListAllEndpoints(persistenceMock.ListAll());

            //action
            var endpoints = listAllEndpoints.Executar();

            //assert
            Assert.Equal(ModelsMock.ListEndpointMock().Count, endpoints.Count);
            Assert.NotNull(endpoints);
        }
    }
}
