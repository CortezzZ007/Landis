using Moq;
using Stories.Endpoints;
using Stories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tests.Mocks;
using Xunit;

namespace Tests.Stories
{
    public class ValidateSerialNumberOfEndpointTest
    {
        [Fact]
        public void MustReturnTheMeterModelIdOfTheSerialNumberNSX1P2W()
        {
            //arrange
            PersistenceMock persistenceMock = new PersistenceMock();
            var endpointPersistence = new Mock<IEndpointPersistence>();

            var validateSerialNumberOfEndpoint = new ValidateSerialNumberOfEndpoint(endpointPersistence.Object);

            //action
            var meterModelId = validateSerialNumberOfEndpoint.Executar(ModelsMock.EndpointMock().SerialNumber);

            //assert
            Assert.Empty(validateSerialNumberOfEndpoint.Erros);
            Assert.Equal(ModelsMock.EndpointMock().MeterModelId, meterModelId);
        }

        [Fact]
        public void MustReturnAnExistingEndpoint()
        {
            //arrange
            string key, value;
            PersistenceMock persistenceMock = new PersistenceMock();
            var validateSerialNumberOfEndpoint = new ValidateSerialNumberOfEndpoint(persistenceMock.SearchBySerialNumber());

            //action
            var meterModelId = validateSerialNumberOfEndpoint.Executar(ModelsMock.EndpointMock().SerialNumber);
            key = validateSerialNumberOfEndpoint.Erros.First().Key;
            value = validateSerialNumberOfEndpoint.Erros.First().Value;

            //assert
            Assert.True(validateSerialNumberOfEndpoint.Erros.Count > 0);
            Assert.True(meterModelId == 0);
            Assert.True(key == "Endpoint" && value == "Já existe um endpoint com este serial number");
        }


    }
}
