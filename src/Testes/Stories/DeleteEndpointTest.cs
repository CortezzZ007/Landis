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
        public void MustDeleteAendpoint()
        {
            //arrange
            PersistenceMock persistenceMock = new PersistenceMock();
            var deleteEndpoint = new DeleteEndpoint(persistenceMock.DeleteEndpoint());
            //action

            deleteEndpoint.Execute(ModelsMock.EndpointMock().SerialNumber);

            //assert
            Assert.Empty(deleteEndpoint.Error);

        }
    }
}
