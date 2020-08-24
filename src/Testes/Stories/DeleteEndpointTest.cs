using Stories.Endpoints;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Mocks;
using Xunit;

namespace Tests.Stories
{
    public class DeleteEndpointTest
    {
        [Fact]
        public void MustCreateAendpoint()
        {
            //arrange
            PersistenceMock persistenceMock = new PersistenceMock();
            var deleteEndpoint = new DeleteEndpoint(persistenceMock.DeleteEndpoint());
            //action

            deleteEndpoint.Executar(ModelsMock.EndpointMock().SerialNumber);

            //assert
            Assert.Empty(deleteEndpoint.Erros);

        }
    }
}
