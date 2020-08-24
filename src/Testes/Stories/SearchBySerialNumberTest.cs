using Stories.Endpoints;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Mocks;
using Xunit;

namespace Tests.Stories
{
    public class SearchBySerialNumberTest
    {
        [Fact]
        public void MustSearchForAnEndpointByTheSerialNumberAndReturnAnotNullObject()
        {
            //arrange
            PersistenceMock persistenceMock = new PersistenceMock();
            persistenceMock.SearchBySerialNumber();
            var searchBySerialNumber = new SearchBySerialNumber(persistenceMock.SearchBySerialNumber());
            
            //action
            var endpoint = searchBySerialNumber.Executar(ModelsMock.EndpointMock().SerialNumber);

            //assert
            Assert.Empty(searchBySerialNumber.Erros);
            Assert.Equal(ModelsMock.EndpointMock().SerialNumber, endpoint.SerialNumber);
            Assert.NotNull(endpoint);
        }
    }
}
