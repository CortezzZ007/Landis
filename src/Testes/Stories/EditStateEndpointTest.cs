using Stories.Endpoints;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Mocks;
using Xunit;

namespace Tests.Stories
{
    public class EditStateEndpointTest
    {
        [Fact]
        public void MustEditStateAendpoint()
        {
            //arrange
            PersistenceMock persistenceMock = new PersistenceMock();
            persistenceMock.SearchBySerialNumber();
            var editEndpoint = new EditStateEndpoint(persistenceMock.EditStateEndpoint());
            //action

            editEndpoint.Execute(ModelsMock.EndpointMock().SerialNumber, ModelsMock.EndpointMock().State);
            //assert
            Assert.Empty(editEndpoint.Error);
        }
    }
}
