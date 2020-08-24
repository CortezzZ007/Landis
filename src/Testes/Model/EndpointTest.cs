using System;
using Tests.Mocks;
using Xunit;

namespace Tests.Model
{
    public class EndpointTest
    {
        [Fact]
        public void MustEditStateAendpoint()
        {
            //arrange
            var state = 3;
            var endpoint = ModelsMock.EndpointMock();
            //action

            endpoint.EditState(state);
            
            //assert
            Assert.Equal(state, endpoint.State);
        }
    }
}
