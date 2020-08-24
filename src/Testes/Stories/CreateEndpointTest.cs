using Moq;
using Stories.Endpoints;
using Stories.Interfaces;
using System;
using Tests.Mocks;
using Xunit;

namespace Testes
{
    public class CreateEndpointTest
    {
        [Fact]
        public void MustCreateAendpoint()
        {
            //arrange
            PersistenceMock persistenceMock = new PersistenceMock();
            var createEndpoint = new CreateEndpoint(persistenceMock.CreateEndpoint());
            //action

            createEndpoint.Executar(ModelsMock.EndpointMock());

            //assert
            Assert.Empty(createEndpoint.Erros);

        }
    }
}
